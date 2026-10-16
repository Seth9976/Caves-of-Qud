using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000191 RID: 401
	internal static class AssemblyIdentityUtils
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x0002FC18 File Offset: 0x0002DE18
		[NullableContext(1)]
		[return: Nullable(2)]
		public static AssemblyIdentity TryGetAssemblyIdentity(string filePath)
		{
			try
			{
				using (FileStream fileStream = new FileStream(filePath, 3, 1, 7))
				{
					using (PEReader pereader = new PEReader(fileStream))
					{
						MetadataReader metadataReader = pereader.GetMetadataReader();
						AssemblyDefinition assemblyDefinition = metadataReader.GetAssemblyDefinition();
						string @string = metadataReader.GetString(assemblyDefinition.Name);
						Version version = assemblyDefinition.Version;
						StringHandle culture = assemblyDefinition.Culture;
						string text = ((!culture.IsNil) ? metadataReader.GetString(culture) : null);
						bool flag = (assemblyDefinition.Flags & AssemblyFlags.PublicKey) > (AssemblyFlags)0;
						BlobHandle publicKey = assemblyDefinition.PublicKey;
						ImmutableArray<byte> immutableArray = ((!publicKey.IsNil) ? metadataReader.GetBlobBytes(publicKey).AsImmutableOrNull<byte>() : default(ImmutableArray<byte>));
						return new AssemblyIdentity(@string, version, text, immutableArray, flag);
					}
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
