using System;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000003 RID: 3
	public struct AssemblyReference : IMetadataReferenceProvider
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000025CF File Offset: 0x000007CF
		public MetadataReference Reference
		{
			get
			{
				return this.reference.Reference;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025DC File Offset: 0x000007DC
		public AssemblyReference(string assemblyName, AppDomain domain = null)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (domain == null)
			{
				domain = AppDomain.CurrentDomain;
			}
			this.reference = null;
			foreach (Assembly assembly in domain.GetAssemblies())
			{
				if (assembly.GetName().Name == assemblyName)
				{
					this.reference = new AssemblyReferenceFromAssemblyObject(assembly);
					break;
				}
			}
			if (this.reference == null)
			{
				throw new ArgumentException(string.Format("Failed to resolve assembly reference '{0}'. Ensure that the assembly is loaded and that the specified name is correct", assemblyName));
			}
			this.assemblyName = assemblyName;
			this.domain = domain;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000266E File Offset: 0x0000086E
		public static IMetadataReferenceProvider FromNameOrFile(string assemblyNameOrFilePath, AppDomain searchDomain = null)
		{
			if (!File.Exists(assemblyNameOrFilePath))
			{
				return new AssemblyReference(Path.GetFileNameWithoutExtension(assemblyNameOrFilePath), searchDomain);
			}
			return new AssemblyReferenceFromFile(assemblyNameOrFilePath);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002695 File Offset: 0x00000895
		public static IMetadataReferenceProvider FromAssembly(Assembly assembly)
		{
			return new AssemblyReferenceFromAssemblyObject(assembly);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026A2 File Offset: 0x000008A2
		public static IMetadataReferenceProvider FromStream(Stream assemblyStream)
		{
			return new AssemblyReferenceFromStream(assemblyStream);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026AF File Offset: 0x000008AF
		public static IMetadataReferenceProvider FromImage(byte[] assemblyImage)
		{
			return new AssemblyReferenceFromImage(assemblyImage);
		}

		// Token: 0x04000002 RID: 2
		private IMetadataReferenceProvider reference;

		// Token: 0x04000003 RID: 3
		private string assemblyName;

		// Token: 0x04000004 RID: 4
		private AppDomain domain;
	}
}
