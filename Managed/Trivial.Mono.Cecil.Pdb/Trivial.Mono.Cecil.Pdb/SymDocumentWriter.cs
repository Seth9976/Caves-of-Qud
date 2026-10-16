using System;

namespace Trivial.Mono.Cecil.Pdb
{
	// Token: 0x0200000F RID: 15
	internal class SymDocumentWriter
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000038E8 File Offset: 0x00001AE8
		public SymDocumentWriter(ISymUnmanagedDocumentWriter unmanagedDocumentWriter)
		{
			this.m_unmanagedDocumentWriter = unmanagedDocumentWriter;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000038F7 File Offset: 0x00001AF7
		public ISymUnmanagedDocumentWriter GetUnmanaged()
		{
			return this.m_unmanagedDocumentWriter;
		}

		// Token: 0x04000019 RID: 25
		private readonly ISymUnmanagedDocumentWriter m_unmanagedDocumentWriter;
	}
}
