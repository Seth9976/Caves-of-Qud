using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A6 RID: 422
	internal static class MetadataReaderExtensions
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x00032E30 File Offset: 0x00031030
		internal static bool GetWinMdVersion(this MetadataReader reader, out int majorVersion, out int minorVersion)
		{
			if (reader.MetadataKind == MetadataKind.WindowsMetadata)
			{
				string metadataVersion = reader.MetadataVersion;
				if (metadataVersion.StartsWith("WindowsRuntime ", 4))
				{
					string[] array = metadataVersion.Substring("WindowsRuntime ".Length).Split(new char[] { '.' });
					if (array.Length == 2 && int.TryParse(array[0], 0, CultureInfo.InvariantCulture, ref majorVersion) && int.TryParse(array[1], 0, CultureInfo.InvariantCulture, ref minorVersion))
					{
						return true;
					}
				}
			}
			majorVersion = 0;
			minorVersion = 0;
			return false;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00032EB0 File Offset: 0x000310B0
		internal static AssemblyIdentity ReadAssemblyIdentityOrThrow(this MetadataReader reader)
		{
			if (!reader.IsAssembly)
			{
				return null;
			}
			AssemblyDefinition assemblyDefinition = reader.GetAssemblyDefinition();
			return reader.CreateAssemblyIdentityOrThrow(assemblyDefinition.Version, assemblyDefinition.Flags, assemblyDefinition.PublicKey, assemblyDefinition.Name, assemblyDefinition.Culture, false);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00032EF8 File Offset: 0x000310F8
		internal static ImmutableArray<AssemblyIdentity> GetReferencedAssembliesOrThrow(this MetadataReader reader)
		{
			ArrayBuilder<AssemblyIdentity> instance = ArrayBuilder<AssemblyIdentity>.GetInstance(reader.AssemblyReferences.Count);
			ImmutableArray<AssemblyIdentity> immutableArray;
			try
			{
				foreach (AssemblyReferenceHandle assemblyReferenceHandle in reader.AssemblyReferences)
				{
					AssemblyReference assemblyReference = reader.GetAssemblyReference(assemblyReferenceHandle);
					instance.Add(reader.CreateAssemblyIdentityOrThrow(assemblyReference.Version, assemblyReference.Flags, assemblyReference.PublicKeyOrToken, assemblyReference.Name, assemblyReference.Culture, true));
				}
				immutableArray = instance.ToImmutable();
			}
			finally
			{
				instance.Free();
			}
			return immutableArray;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00032FB4 File Offset: 0x000311B4
		internal static Guid GetModuleVersionIdOrThrow(this MetadataReader reader)
		{
			return reader.GetGuid(reader.GetModuleDefinition().Mvid);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00032FD8 File Offset: 0x000311D8
		private static AssemblyIdentity CreateAssemblyIdentityOrThrow(this MetadataReader reader, Version version, AssemblyFlags flags, BlobHandle publicKey, StringHandle name, StringHandle culture, bool isReference)
		{
			string @string = reader.GetString(name);
			if (!MetadataHelpers.IsValidMetadataIdentifier(@string))
			{
				throw new BadImageFormatException(string.Format(CodeAnalysisResources.InvalidAssemblyName, @string));
			}
			string text = (culture.IsNil ? null : reader.GetString(culture));
			if (text != null && !MetadataHelpers.IsValidMetadataIdentifier(text))
			{
				throw new BadImageFormatException(string.Format(CodeAnalysisResources.InvalidCultureName, text));
			}
			ImmutableArray<byte> immutableArray = reader.GetBlobContent(publicKey);
			bool flag;
			if (isReference)
			{
				flag = (flags & AssemblyFlags.PublicKey) > (AssemblyFlags)0;
				if (flag)
				{
					if (!MetadataHelpers.IsValidPublicKey(immutableArray))
					{
						throw new BadImageFormatException(CodeAnalysisResources.InvalidPublicKey);
					}
				}
				else if (!immutableArray.IsEmpty && immutableArray.Length != 8)
				{
					throw new BadImageFormatException(CodeAnalysisResources.InvalidPublicKeyToken);
				}
			}
			else
			{
				flag = !immutableArray.IsEmpty;
				if (flag && !MetadataHelpers.IsValidPublicKey(immutableArray))
				{
					throw new BadImageFormatException(CodeAnalysisResources.InvalidPublicKey);
				}
			}
			if (immutableArray.IsEmpty)
			{
				immutableArray = default(ImmutableArray<byte>);
			}
			return new AssemblyIdentity(true, @string, version, text, immutableArray, flag, (flags & AssemblyFlags.Retargetable) > (AssemblyFlags)0, (flags & AssemblyFlags.ContentTypeMask) >> 9);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000330D0 File Offset: 0x000312D0
		internal static bool DeclaresTheObjectClass(this MetadataReader reader)
		{
			return reader.DeclaresType(new Func<MetadataReader, TypeDefinition, bool>(MetadataReaderExtensions.IsTheObjectClass));
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000330E4 File Offset: 0x000312E4
		private static bool IsTheObjectClass(this MetadataReader reader, TypeDefinition typeDef)
		{
			return typeDef.BaseType.IsNil && reader.IsPublicNonInterfaceType(typeDef, "System", "Object");
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00033118 File Offset: 0x00031318
		internal static bool DeclaresType(this MetadataReader reader, Func<MetadataReader, TypeDefinition, bool> predicate)
		{
			foreach (TypeDefinitionHandle typeDefinitionHandle in reader.TypeDefinitions)
			{
				try
				{
					TypeDefinition typeDefinition = reader.GetTypeDefinition(typeDefinitionHandle);
					if (predicate.Invoke(reader, typeDefinition))
					{
						return true;
					}
				}
				catch (BadImageFormatException)
				{
				}
			}
			return false;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00033194 File Offset: 0x00031394
		internal static bool IsPublicNonInterfaceType(this MetadataReader reader, TypeDefinition typeDef, string namespaceName, string typeName)
		{
			return (typeDef.Attributes & 33) == 1 && reader.StringComparer.Equals(typeDef.Name, typeName) && reader.StringComparer.Equals(typeDef.Namespace, namespaceName);
		}
	}
}
