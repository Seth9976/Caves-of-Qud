using System;
using System.IO;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000C5 RID: 197
	internal class PdbReader
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000066D3 File Offset: 0x000048D3
		internal PdbReader(Stream reader, int pageSize)
		{
			this.pageSize = pageSize;
			this.reader = reader;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000066E9 File Offset: 0x000048E9
		internal void Seek(int page, int offset)
		{
			this.reader.Seek((long)(page * this.pageSize + offset), 0);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006703 File Offset: 0x00004903
		internal void Read(byte[] bytes, int offset, int count)
		{
			this.reader.Read(bytes, offset, count);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006714 File Offset: 0x00004914
		internal int PagesFromSize(int size)
		{
			return (size + this.pageSize - 1) / this.pageSize;
		}

		// Token: 0x04000384 RID: 900
		internal readonly int pageSize;

		// Token: 0x04000385 RID: 901
		internal readonly Stream reader;
	}
}
