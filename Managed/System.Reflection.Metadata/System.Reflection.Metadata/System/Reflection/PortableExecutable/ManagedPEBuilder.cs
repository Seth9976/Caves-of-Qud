using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	public class ManagedPEBuilder : PEBuilder
	{
		/// <param name="header" />
		/// <param name="metadataRootBuilder" />
		/// <param name="ilStream" />
		/// <param name="mappedFieldData" />
		/// <param name="managedResources" />
		/// <param name="nativeResources" />
		/// <param name="debugDirectoryBuilder" />
		/// <param name="strongNameSignatureSize" />
		/// <param name="entryPoint" />
		/// <param name="flags" />
		/// <param name="deterministicIdProvider" />
		// Token: 0x060000DF RID: 223 RVA: 0x0000320C File Offset: 0x0000140C
		[NullableContext(2)]
		public ManagedPEBuilder([Nullable(1)] PEHeaderBuilder header, [Nullable(1)] MetadataRootBuilder metadataRootBuilder, [Nullable(1)] BlobBuilder ilStream, BlobBuilder mappedFieldData = null, BlobBuilder managedResources = null, ResourceSectionBuilder nativeResources = null, DebugDirectoryBuilder debugDirectoryBuilder = null, int strongNameSignatureSize = 128, MethodDefinitionHandle entryPoint = default(MethodDefinitionHandle), CorFlags flags = CorFlags.ILOnly, [Nullable(new byte[] { 2, 1 })] Func<IEnumerable<Blob>, BlobContentId> deterministicIdProvider = null)
			: base(header, deterministicIdProvider)
		{
			if (header == null)
			{
				Throw.ArgumentNull("header");
			}
			if (metadataRootBuilder == null)
			{
				Throw.ArgumentNull("metadataRootBuilder");
			}
			if (ilStream == null)
			{
				Throw.ArgumentNull("ilStream");
			}
			if (strongNameSignatureSize < 0)
			{
				Throw.ArgumentOutOfRange("strongNameSignatureSize");
			}
			this._metadataRootBuilder = metadataRootBuilder;
			this._ilStream = ilStream;
			this._mappedFieldDataOpt = mappedFieldData;
			this._managedResourcesOpt = managedResources;
			this._nativeResourcesOpt = nativeResources;
			this._strongNameSignatureSize = strongNameSignatureSize;
			this._entryPointOpt = entryPoint;
			this._debugDirectoryBuilderOpt = debugDirectoryBuilder ?? this.CreateDefaultDebugDirectoryBuilder();
			this._corFlags = flags;
			this._peDirectoriesBuilder = new PEDirectoriesBuilder();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000032B4 File Offset: 0x000014B4
		private DebugDirectoryBuilder CreateDefaultDebugDirectoryBuilder()
		{
			if (base.IsDeterministic)
			{
				DebugDirectoryBuilder debugDirectoryBuilder = new DebugDirectoryBuilder();
				debugDirectoryBuilder.AddReproducibleEntry();
				return debugDirectoryBuilder;
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000032D8 File Offset: 0x000014D8
		[NullableContext(0)]
		protected override ImmutableArray<PEBuilder.Section> CreateSections()
		{
			ImmutableArray<PEBuilder.Section>.Builder builder = ImmutableArray.CreateBuilder<PEBuilder.Section>(3);
			builder.Add(new PEBuilder.Section(".text", SectionCharacteristics.ContainsCode | SectionCharacteristics.MemExecute | SectionCharacteristics.MemRead));
			if (this._nativeResourcesOpt != null)
			{
				builder.Add(new PEBuilder.Section(".rsrc", SectionCharacteristics.ContainsInitializedData | SectionCharacteristics.MemRead));
			}
			if (base.Header.Machine == Machine.I386 || base.Header.Machine == Machine.Unknown)
			{
				builder.Add(new PEBuilder.Section(".reloc", SectionCharacteristics.ContainsInitializedData | SectionCharacteristics.MemDiscardable | SectionCharacteristics.MemRead));
			}
			return builder.ToImmutable();
		}

		/// <param name="name" />
		/// <param name="location" />
		// Token: 0x060000E2 RID: 226 RVA: 0x00003358 File Offset: 0x00001558
		protected override BlobBuilder SerializeSection(string name, SectionLocation location)
		{
			BlobBuilder blobBuilder;
			if (!(name == ".text"))
			{
				if (!(name == ".rsrc"))
				{
					if (!(name == ".reloc"))
					{
						throw new ArgumentException(SR.Format(SR.UnknownSectionName, name), "name");
					}
					blobBuilder = this.SerializeRelocationSection(location);
				}
				else
				{
					blobBuilder = this.SerializeResourceSection(location);
				}
			}
			else
			{
				blobBuilder = this.SerializeTextSection(location);
			}
			return blobBuilder;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000033C4 File Offset: 0x000015C4
		private BlobBuilder SerializeTextSection(SectionLocation location)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			BlobBuilder blobBuilder2 = new BlobBuilder(256);
			MetadataSizes sizes = this._metadataRootBuilder.Sizes;
			Characteristics imageCharacteristics = base.Header.ImageCharacteristics;
			Machine machine = base.Header.Machine;
			int count = this._ilStream.Count;
			int metadataSize = sizes.MetadataSize;
			BlobBuilder managedResourcesOpt = this._managedResourcesOpt;
			int num = ((managedResourcesOpt != null) ? managedResourcesOpt.Count : 0);
			int strongNameSignatureSize = this._strongNameSignatureSize;
			DebugDirectoryBuilder debugDirectoryBuilderOpt = this._debugDirectoryBuilderOpt;
			int num2 = ((debugDirectoryBuilderOpt != null) ? debugDirectoryBuilderOpt.Size : 0);
			BlobBuilder mappedFieldDataOpt = this._mappedFieldDataOpt;
			ManagedTextSection managedTextSection = new ManagedTextSection(imageCharacteristics, machine, count, metadataSize, num, strongNameSignatureSize, num2, (mappedFieldDataOpt != null) ? mappedFieldDataOpt.Count : 0);
			int num3 = location.RelativeVirtualAddress + managedTextSection.OffsetToILStream;
			int num4 = location.RelativeVirtualAddress + managedTextSection.CalculateOffsetToMappedFieldDataStream();
			this._metadataRootBuilder.Serialize(blobBuilder2, num3, num4);
			BlobBuilder blobBuilder3;
			DirectoryEntry directoryEntry;
			if (this._debugDirectoryBuilderOpt != null)
			{
				int num5 = managedTextSection.ComputeOffsetToDebugDirectory();
				blobBuilder3 = new BlobBuilder(this._debugDirectoryBuilderOpt.TableSize);
				this._debugDirectoryBuilderOpt.Serialize(blobBuilder3, location, num5);
				directoryEntry = new DirectoryEntry(location.RelativeVirtualAddress + num5, this._debugDirectoryBuilderOpt.TableSize);
			}
			else
			{
				blobBuilder3 = null;
				directoryEntry = default(DirectoryEntry);
			}
			this._lazyEntryPointAddress = managedTextSection.GetEntryPointAddress(location.RelativeVirtualAddress);
			managedTextSection.Serialize(blobBuilder, location.RelativeVirtualAddress, this._entryPointOpt.IsNil ? 0 : MetadataTokens.GetToken(this._entryPointOpt), this._corFlags, base.Header.ImageBase, blobBuilder2, this._ilStream, this._mappedFieldDataOpt, this._managedResourcesOpt, blobBuilder3, out this._lazyStrongNameSignature);
			this._peDirectoriesBuilder.AddressOfEntryPoint = this._lazyEntryPointAddress;
			this._peDirectoriesBuilder.DebugTable = directoryEntry;
			this._peDirectoriesBuilder.ImportAddressTable = managedTextSection.GetImportAddressTableDirectoryEntry(location.RelativeVirtualAddress);
			this._peDirectoriesBuilder.ImportTable = managedTextSection.GetImportTableDirectoryEntry(location.RelativeVirtualAddress);
			this._peDirectoriesBuilder.CorHeaderTable = managedTextSection.GetCorHeaderDirectoryEntry(location.RelativeVirtualAddress);
			return blobBuilder;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000035BC File Offset: 0x000017BC
		private BlobBuilder SerializeResourceSection(SectionLocation location)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			this._nativeResourcesOpt.Serialize(blobBuilder, location);
			this._peDirectoriesBuilder.ResourceTable = new DirectoryEntry(location.RelativeVirtualAddress, blobBuilder.Count);
			return blobBuilder;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003600 File Offset: 0x00001800
		private BlobBuilder SerializeRelocationSection(SectionLocation location)
		{
			BlobBuilder blobBuilder = new BlobBuilder(256);
			ManagedPEBuilder.WriteRelocationSection(blobBuilder, base.Header.Machine, this._lazyEntryPointAddress);
			this._peDirectoriesBuilder.BaseRelocationTable = new DirectoryEntry(location.RelativeVirtualAddress, blobBuilder.Count);
			return blobBuilder;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003650 File Offset: 0x00001850
		private static void WriteRelocationSection(BlobBuilder builder, Machine machine, int entryPointAddress)
		{
			builder.WriteUInt32((uint)((entryPointAddress + 2) / 4096 * 4096));
			builder.WriteUInt32((machine == Machine.IA64) ? 14U : 12U);
			uint num = (uint)((entryPointAddress + 2) % 4096);
			uint num2 = ((machine == Machine.Amd64 || machine == Machine.IA64 || machine == Machine.Arm64) ? 10U : 3U);
			ushort num3 = (ushort)((num2 << 12) | num);
			builder.WriteUInt16(num3);
			if (machine == Machine.IA64)
			{
				builder.WriteUInt32(num2 << 12);
			}
			builder.WriteUInt16(0);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000036D6 File Offset: 0x000018D6
		protected internal override PEDirectoriesBuilder GetDirectories()
		{
			return this._peDirectoriesBuilder;
		}

		/// <param name="peImage" />
		/// <param name="signatureProvider" />
		// Token: 0x060000E8 RID: 232 RVA: 0x000036DE File Offset: 0x000018DE
		public void Sign(BlobBuilder peImage, Func<IEnumerable<Blob>, byte[]> signatureProvider)
		{
			if (peImage == null)
			{
				Throw.ArgumentNull("peImage");
			}
			if (signatureProvider == null)
			{
				Throw.ArgumentNull("signatureProvider");
			}
			base.Sign(peImage, this._lazyStrongNameSignature, signatureProvider);
		}

		// Token: 0x04000049 RID: 73
		public const int ManagedResourcesDataAlignment = 8;

		// Token: 0x0400004A RID: 74
		public const int MappedFieldDataAlignment = 8;

		// Token: 0x0400004B RID: 75
		private const int DefaultStrongNameSignatureSize = 128;

		// Token: 0x0400004C RID: 76
		private const string TextSectionName = ".text";

		// Token: 0x0400004D RID: 77
		private const string ResourceSectionName = ".rsrc";

		// Token: 0x0400004E RID: 78
		private const string RelocationSectionName = ".reloc";

		// Token: 0x0400004F RID: 79
		private readonly PEDirectoriesBuilder _peDirectoriesBuilder;

		// Token: 0x04000050 RID: 80
		private readonly MetadataRootBuilder _metadataRootBuilder;

		// Token: 0x04000051 RID: 81
		private readonly BlobBuilder _ilStream;

		// Token: 0x04000052 RID: 82
		private readonly BlobBuilder _mappedFieldDataOpt;

		// Token: 0x04000053 RID: 83
		private readonly BlobBuilder _managedResourcesOpt;

		// Token: 0x04000054 RID: 84
		private readonly ResourceSectionBuilder _nativeResourcesOpt;

		// Token: 0x04000055 RID: 85
		private readonly int _strongNameSignatureSize;

		// Token: 0x04000056 RID: 86
		private readonly MethodDefinitionHandle _entryPointOpt;

		// Token: 0x04000057 RID: 87
		private readonly DebugDirectoryBuilder _debugDirectoryBuilderOpt;

		// Token: 0x04000058 RID: 88
		private readonly CorFlags _corFlags;

		// Token: 0x04000059 RID: 89
		private int _lazyEntryPointAddress;

		// Token: 0x0400005A RID: 90
		private Blob _lazyStrongNameSignature;
	}
}
