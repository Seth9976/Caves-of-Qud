using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Roslyn.Utilities
{
	// Token: 0x02000025 RID: 37
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AssemblyUtilities
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002F08 File Offset: 0x00001108
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<string> FindAssemblySet(string filePath)
		{
			Queue<string> queue = new Queue<string>();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			queue.Enqueue(filePath);
			while (queue.Count > 0)
			{
				string text = queue.Dequeue();
				if (hashSet.Add(text))
				{
					string directoryName = Path.GetDirectoryName(text);
					using (PEReader pereader = new PEReader(FileUtilities.OpenRead(text)))
					{
						MetadataReader metadataReader = pereader.GetMetadataReader();
						foreach (AssemblyReferenceHandle assemblyReferenceHandle in metadataReader.AssemblyReferences)
						{
							string @string = metadataReader.GetString(metadataReader.GetAssemblyReference(assemblyReferenceHandle).Name);
							string text2 = Path.Combine(directoryName, @string + ".dll");
							if (!hashSet.Contains(text2) && File.Exists(text2))
							{
								queue.Enqueue(text2);
							}
						}
					}
				}
			}
			return ImmutableArray.CreateRange<string>(hashSet);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000301C File Offset: 0x0000121C
		public static Guid ReadMvid(string filePath)
		{
			Guid guid;
			using (PEReader pereader = new PEReader(FileUtilities.OpenRead(filePath)))
			{
				MetadataReader metadataReader = pereader.GetMetadataReader();
				GuidHandle mvid = metadataReader.GetModuleDefinition().Mvid;
				guid = metadataReader.GetGuid(mvid);
			}
			return guid;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003070 File Offset: 0x00001270
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<string> FindSatelliteAssemblies(string filePath)
		{
			ImmutableArray<string>.Builder builder = ImmutableArray.CreateBuilder<string>();
			string directoryName = Path.GetDirectoryName(filePath);
			string text = Path.GetFileNameWithoutExtension(filePath) + ".resources";
			string text2 = text + ".dll";
			foreach (string text3 in Directory.EnumerateDirectories(directoryName, "*", 0))
			{
				string text4 = Path.Combine(text3, text2);
				if (File.Exists(text4))
				{
					builder.Add(text4);
				}
				text4 = Path.Combine(text3, text, text2);
				if (File.Exists(text4))
				{
					builder.Add(text4);
				}
			}
			return builder.ToImmutable();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003120 File Offset: 0x00001320
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<AssemblyIdentity> IdentifyMissingDependencies(string assemblyPath, IEnumerable<string> dependencyFilePaths)
		{
			HashSet<AssemblyIdentity> hashSet = new HashSet<AssemblyIdentity>();
			foreach (string text in dependencyFilePaths)
			{
				using (PEReader pereader = new PEReader(FileUtilities.OpenRead(text)))
				{
					AssemblyIdentity assemblyIdentity = pereader.GetMetadataReader().ReadAssemblyIdentityOrThrow();
					hashSet.Add(assemblyIdentity);
				}
			}
			HashSet<AssemblyIdentity> hashSet2 = new HashSet<AssemblyIdentity>();
			using (PEReader pereader2 = new PEReader(FileUtilities.OpenRead(assemblyPath)))
			{
				ImmutableArray<AssemblyIdentity> referencedAssembliesOrThrow = pereader2.GetMetadataReader().GetReferencedAssembliesOrThrow();
				hashSet2.AddAll<AssemblyIdentity>(referencedAssembliesOrThrow);
			}
			hashSet2.ExceptWith(hashSet);
			return ImmutableArray.CreateRange<AssemblyIdentity>(hashSet2);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031F0 File Offset: 0x000013F0
		public static AssemblyIdentity GetAssemblyIdentity(string assemblyPath)
		{
			AssemblyIdentity assemblyIdentity;
			using (PEReader pereader = new PEReader(FileUtilities.OpenRead(assemblyPath)))
			{
				assemblyIdentity = pereader.GetMetadataReader().ReadAssemblyIdentityOrThrow();
			}
			return assemblyIdentity;
		}
	}
}
