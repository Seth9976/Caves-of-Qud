using System;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000006 RID: 6
	public struct AssemblyReferenceFromAssemblyObject : IMetadataReferenceProvider
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000027A4 File Offset: 0x000009A4
		public MetadataReference Reference
		{
			get
			{
				return this.reference.Reference;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027B4 File Offset: 0x000009B4
		public AssemblyReferenceFromAssemblyObject(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (string.IsNullOrEmpty(assembly.Location))
			{
				throw new ArgumentException("The specified assembly is not referencable because it's 'Location' property is empty. You will need to reference the assembly explicitly by filepath, filestream or assembly image data");
			}
			this.assembly = assembly;
			this.reference = new AssemblyReferenceFromFile(assembly.Location);
		}

		// Token: 0x04000007 RID: 7
		private AssemblyReferenceFromFile reference;

		// Token: 0x04000008 RID: 8
		private Assembly assembly;
	}
}
