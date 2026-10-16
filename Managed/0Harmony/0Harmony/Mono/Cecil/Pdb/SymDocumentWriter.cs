using System;

namespace Mono.Cecil.Pdb
{
	// Token: 0x02000335 RID: 821
	internal class SymDocumentWriter
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0004336B File Offset: 0x0004156B
		public ISymUnmanagedDocumentWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00043373 File Offset: 0x00041573
		public SymDocumentWriter(ISymUnmanagedDocumentWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00043382 File Offset: 0x00041582
		public void SetSource(byte[] source)
		{
			this.writer.SetSource((uint)source.Length, source);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00043393 File Offset: 0x00041593
		public void SetCheckSum(Guid hashAlgo, byte[] checkSum)
		{
			this.writer.SetCheckSum(hashAlgo, (uint)checkSum.Length, checkSum);
		}

		// Token: 0x04000A97 RID: 2711
		private readonly ISymUnmanagedDocumentWriter writer;
	}
}
