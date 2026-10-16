using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;
using Microsoft.DiaSymReader;

namespace Microsoft.Cci
{
	// Token: 0x020000BC RID: 188
	internal static class PeWriter
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00016648 File Offset: 0x00014848
		internal static bool WritePeToStream(EmitContext context, CommonMessageProvider messageProvider, Func<Stream> getPeStream, Func<Stream> getPortablePdbStreamOpt, PdbWriter nativePdbWriterOpt, string pdbPathOpt, bool metadataOnly, bool isDeterministic, bool emitTestCoverageData, RSAParameters? privateKeyOpt, CancellationToken cancellationToken)
		{
			MetadataWriter metadataWriter = FullMetadataWriter.Create(context, messageProvider, metadataOnly, isDeterministic, emitTestCoverageData, getPortablePdbStreamOpt != null, cancellationToken);
			ModulePropertiesForSerialization serializationProperties = context.Module.SerializationProperties;
			if (nativePdbWriterOpt != null)
			{
				nativePdbWriterOpt.SetMetadataEmitter(metadataWriter);
			}
			BlobBuilder blobBuilder = new BlobBuilder(32768);
			BlobBuilder blobBuilder2 = new BlobBuilder(256);
			BlobBuilder blobBuilder3 = new BlobBuilder(1024);
			Blob blob;
			Blob blob2;
			metadataWriter.BuildMetadataAndIL(nativePdbWriterOpt, blobBuilder, blobBuilder2, blobBuilder3, out blob, out blob2);
			MethodDefinitionHandle methodDefinitionHandle;
			MethodDefinitionHandle methodDefinitionHandle2;
			metadataWriter.GetEntryPoints(out methodDefinitionHandle, out methodDefinitionHandle2);
			if (!methodDefinitionHandle2.IsNil && nativePdbWriterOpt != null)
			{
				nativePdbWriterOpt.SetEntryPoint(MetadataTokens.GetToken(methodDefinitionHandle2));
			}
			if (nativePdbWriterOpt != null)
			{
				if (context.Module.SourceLinkStreamOpt != null)
				{
					nativePdbWriterOpt.EmbedSourceLink(context.Module.SourceLinkStreamOpt);
				}
				if (metadataWriter.Module.OutputKind == OutputKind.WindowsRuntimeMetadata)
				{
					nativePdbWriterOpt.WriteDefinitionLocations(metadataWriter.Module.GetSymbolToLocationMap());
				}
				nativePdbWriterOpt.WriteRemainingDebugDocuments(metadataWriter.Module.DebugDocumentsBuilder.DebugDocuments);
			}
			Stream stream = getPeStream.Invoke();
			if (stream == null)
			{
				return false;
			}
			BlobContentId blobContentId = ((nativePdbWriterOpt != null) ? nativePdbWriterOpt.GetContentId() : default(BlobContentId));
			nativePdbWriterOpt = null;
			ushort num = 0;
			MetadataRootBuilder rootBuilder = metadataWriter.GetRootBuilder();
			PEHeaderBuilder peheaderBuilder = new PEHeaderBuilder(serializationProperties.Machine, serializationProperties.SectionAlignment, serializationProperties.FileAlignment, serializationProperties.BaseAddress, serializationProperties.LinkerMajorVersion, serializationProperties.LinkerMinorVersion, 4, 0, 0, 0, serializationProperties.MajorSubsystemVersion, serializationProperties.MinorSubsystemVersion, serializationProperties.Subsystem, serializationProperties.DllCharacteristics, serializationProperties.ImageCharacteristics, serializationProperties.SizeOfStackReserve, serializationProperties.SizeOfStackCommit, serializationProperties.SizeOfHeapReserve, serializationProperties.SizeOfHeapCommit);
			Func<IEnumerable<Blob>, BlobContentId> func;
			if (!isDeterministic)
			{
				func = null;
			}
			else
			{
				func = (IEnumerable<Blob> content) => BlobContentId.FromHash(CryptographicHashProvider.ComputeSourceHash(content, SourceHashAlgorithm.Sha256));
			}
			Func<IEnumerable<Blob>, BlobContentId> func2 = func;
			ImmutableArray<byte> portablePdbContentHash = default(ImmutableArray<byte>);
			BlobBuilder blobBuilder4 = null;
			if (metadataWriter.EmitPortableDebugMetadata)
			{
				metadataWriter.AddRemainingDebugDocuments(metadataWriter.Module.DebugDocumentsBuilder.DebugDocuments);
				Func<IEnumerable<Blob>, BlobContentId> func3 = ((context.Module.PdbChecksumAlgorithm.Name != null) ? ((IEnumerable<Blob> content) => BlobContentId.FromHash(portablePdbContentHash = CryptographicHashProvider.ComputeHash(context.Module.PdbChecksumAlgorithm, content))) : null);
				BlobBuilder blobBuilder5 = new BlobBuilder(256);
				PortablePdbBuilder portablePdbBuilder = metadataWriter.GetPortablePdbBuilder(rootBuilder.Sizes.RowCounts, methodDefinitionHandle2, func3);
				blobContentId = portablePdbBuilder.Serialize(blobBuilder5);
				num = portablePdbBuilder.FormatVersion;
				if (getPortablePdbStreamOpt == null)
				{
					blobBuilder4 = blobBuilder5;
				}
				else
				{
					Stream stream2 = getPortablePdbStreamOpt.Invoke();
					if (stream2 != null)
					{
						try
						{
							blobBuilder5.WriteContentTo(stream2);
						}
						catch (Exception ex) when (!(ex is OperationCanceledException))
						{
							throw new SymUnmanagedWriterException(ex.Message, ex);
						}
					}
				}
			}
			DebugDirectoryBuilder debugDirectoryBuilder;
			if (pdbPathOpt != null || isDeterministic || blobBuilder4 != null)
			{
				debugDirectoryBuilder = new DebugDirectoryBuilder();
				if (pdbPathOpt != null)
				{
					string text = (isDeterministic ? pdbPathOpt : PeWriter.PadPdbPath(pdbPathOpt));
					debugDirectoryBuilder.AddCodeViewEntry(text, blobContentId, num);
					if (!portablePdbContentHash.IsDefault)
					{
						debugDirectoryBuilder.AddPdbChecksumEntry(context.Module.PdbChecksumAlgorithm.Name, portablePdbContentHash);
					}
				}
				if (isDeterministic)
				{
					debugDirectoryBuilder.AddReproducibleEntry();
				}
				if (blobBuilder4 != null)
				{
					debugDirectoryBuilder.AddEmbeddedPortablePdbEntry(blobBuilder4, num);
				}
			}
			else
			{
				debugDirectoryBuilder = null;
			}
			StrongNameProvider strongNameProvider = context.Module.CommonCompilation.Options.StrongNameProvider;
			CorFlags corFlags = serializationProperties.CorFlags;
			ExtendedPEBuilder extendedPEBuilder = new ExtendedPEBuilder(peheaderBuilder, rootBuilder, blobBuilder, blobBuilder2, blobBuilder3, PeWriter.CreateNativeResourceSectionSerializer(context.Module), debugDirectoryBuilder, SigningUtilities.CalculateStrongNameSignatureSize(context.Module, privateKeyOpt), methodDefinitionHandle, corFlags, func2, metadataOnly && !context.IncludePrivateMembers);
			BlobBuilder blobBuilder6 = new BlobBuilder(256);
			Blob blob3;
			BlobContentId blobContentId2 = extendedPEBuilder.Serialize(blobBuilder6, out blob3);
			PeWriter.PatchModuleVersionIds(blob, blob3, blob2, blobContentId2.Guid);
			if (privateKeyOpt != null && corFlags.HasFlag(CorFlags.StrongNameSigned))
			{
				strongNameProvider.SignBuilder(extendedPEBuilder, blobBuilder6, privateKeyOpt.Value);
			}
			try
			{
				blobBuilder6.WriteContentTo(stream);
			}
			catch (Exception ex2) when (!(ex2 is OperationCanceledException))
			{
				throw new PeWritingException(ex2);
			}
			return true;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00016AA4 File Offset: 0x00014CA4
		internal static uint CalculateChecksum(BlobBuilder peBlob, Blob checksumBlob)
		{
			if (PeWriter.s_calculateChecksumMethod == null)
			{
				PeWriter.s_calculateChecksumMethod = Enumerable.Single<MethodInfo>(Enumerable.Where<MethodInfo>(RuntimeReflectionExtensions.GetRuntimeMethods(typeof(PEBuilder)), (MethodInfo m) => m.Name == "CalculateChecksum" && m.GetParameters().Length == 2));
			}
			return (uint)PeWriter.s_calculateChecksumMethod.Invoke(null, new object[] { peBlob, checksumBlob });
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00016B20 File Offset: 0x00014D20
		private static void PatchModuleVersionIds(Blob guidFixup, Blob guidSectionFixup, Blob stringFixup, Guid mvid)
		{
			if (!guidFixup.IsDefault)
			{
				BlobWriter blobWriter = new BlobWriter(guidFixup);
				blobWriter.WriteGuid(mvid);
			}
			if (!guidSectionFixup.IsDefault)
			{
				BlobWriter blobWriter2 = new BlobWriter(guidSectionFixup);
				blobWriter2.WriteGuid(mvid);
			}
			if (!stringFixup.IsDefault)
			{
				BlobWriter blobWriter3 = new BlobWriter(stringFixup);
				blobWriter3.WriteUserString(mvid.ToString());
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00016B84 File Offset: 0x00014D84
		private static string PadPdbPath(string path)
		{
			return path + new string('\0', Math.Max(0, 260 - Encoding.UTF8.GetByteCount(path) - 1));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00016BAC File Offset: 0x00014DAC
		private static ResourceSectionBuilder CreateNativeResourceSectionSerializer(CommonPEModuleBuilder module)
		{
			ResourceSection win32ResourceSection = module.Win32ResourceSection;
			if (win32ResourceSection != null)
			{
				return new PeWriter.ResourceSectionBuilderFromObj(win32ResourceSection);
			}
			IEnumerable<IWin32Resource> win32Resources = module.Win32Resources;
			if (win32Resources != null && Enumerable.Any<IWin32Resource>(win32Resources))
			{
				return new PeWriter.ResourceSectionBuilderFromResources(win32Resources);
			}
			Stream rawWin32Resources = module.RawWin32Resources;
			if (rawWin32Resources != null)
			{
				return new PeWriter.ResourceSectionBuilderFromRaw(rawWin32Resources);
			}
			return null;
		}

		// Token: 0x040001EE RID: 494
		private static MethodInfo s_calculateChecksumMethod;

		// Token: 0x0200052D RID: 1325
		private sealed class ResourceSectionBuilderFromObj : ResourceSectionBuilder
		{
			// Token: 0x06003375 RID: 13173 RVA: 0x00091871 File Offset: 0x0008FA71
			public ResourceSectionBuilderFromObj(ResourceSection resourceSection)
			{
				this._resourceSection = resourceSection;
			}

			// Token: 0x06003376 RID: 13174 RVA: 0x00091880 File Offset: 0x0008FA80
			protected override void Serialize(BlobBuilder builder, SectionLocation location)
			{
				NativeResourceWriter.SerializeWin32Resources(builder, this._resourceSection, location.RelativeVirtualAddress);
			}

			// Token: 0x040014B9 RID: 5305
			private readonly ResourceSection _resourceSection;
		}

		// Token: 0x0200052E RID: 1326
		private sealed class ResourceSectionBuilderFromResources : ResourceSectionBuilder
		{
			// Token: 0x06003377 RID: 13175 RVA: 0x00091895 File Offset: 0x0008FA95
			public ResourceSectionBuilderFromResources(IEnumerable<IWin32Resource> resources)
			{
				this._resources = resources;
			}

			// Token: 0x06003378 RID: 13176 RVA: 0x000918A4 File Offset: 0x0008FAA4
			protected override void Serialize(BlobBuilder builder, SectionLocation location)
			{
				NativeResourceWriter.SerializeWin32Resources(builder, this._resources, location.RelativeVirtualAddress);
			}

			// Token: 0x040014BA RID: 5306
			private readonly IEnumerable<IWin32Resource> _resources;
		}

		// Token: 0x0200052F RID: 1327
		private sealed class ResourceSectionBuilderFromRaw : ResourceSectionBuilder
		{
			// Token: 0x06003379 RID: 13177 RVA: 0x000918B9 File Offset: 0x0008FAB9
			public ResourceSectionBuilderFromRaw(Stream resources)
			{
				this._resources = resources;
			}

			// Token: 0x0600337A RID: 13178 RVA: 0x000918C8 File Offset: 0x0008FAC8
			protected override void Serialize(BlobBuilder builder, SectionLocation location)
			{
				int num;
				while ((num = this._resources.ReadByte()) >= 0)
				{
					builder.WriteByte((byte)num);
				}
			}

			// Token: 0x040014BB RID: 5307
			private readonly Stream _resources;
		}
	}
}
