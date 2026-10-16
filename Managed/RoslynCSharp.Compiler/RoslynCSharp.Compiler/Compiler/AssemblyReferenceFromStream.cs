using System;
using System.IO;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000009 RID: 9
	public struct AssemblyReferenceFromStream : IMetadataReferenceProvider
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000288E File Offset: 0x00000A8E
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002898 File Offset: 0x00000A98
		public MetadataReference Reference
		{
			get
			{
				return MetadataReference.CreateFromStream(this.stream, default(MetadataReferenceProperties), null, null);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028BB File Offset: 0x00000ABB
		public AssemblyReferenceFromStream(Stream assemblyStream)
		{
			if (assemblyStream == null)
			{
				throw new ArgumentNullException("assemblyStream");
			}
			if (!assemblyStream.CanRead)
			{
				throw new ArgumentException("Assembly stream must be readable");
			}
			this.stream = assemblyStream;
		}

		// Token: 0x0400000B RID: 11
		private Stream stream;
	}
}
