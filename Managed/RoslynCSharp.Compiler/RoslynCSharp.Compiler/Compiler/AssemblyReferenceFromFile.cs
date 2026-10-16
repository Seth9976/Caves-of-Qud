using System;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000007 RID: 7
	public struct AssemblyReferenceFromFile : IMetadataReferenceProvider
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002805 File Offset: 0x00000A05
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002810 File Offset: 0x00000A10
		public MetadataReference Reference
		{
			get
			{
				return MetadataReference.CreateFromFile(this.filePath, default(MetadataReferenceProperties), null);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002832 File Offset: 0x00000A32
		public AssemblyReferenceFromFile(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			this.filePath = assemblyFile;
		}

		// Token: 0x04000009 RID: 9
		private string filePath;
	}
}
