using System;
using System.IO;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x0200040C RID: 1036
	internal class PdbReader
	{
		// Token: 0x060015E7 RID: 5607 RVA: 0x0004641F File Offset: 0x0004461F
		internal PdbReader(Stream reader, int pageSize)
		{
			this.pageSize = pageSize;
			this.reader = reader;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00046435 File Offset: 0x00044635
		internal void Seek(int page, int offset)
		{
			this.reader.Seek((long)(page * this.pageSize + offset), SeekOrigin.Begin);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0004644F File Offset: 0x0004464F
		internal void Read(byte[] bytes, int offset, int count)
		{
			this.reader.Read(bytes, offset, count);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00046460 File Offset: 0x00044660
		internal int PagesFromSize(int size)
		{
			return (size + this.pageSize - 1) / this.pageSize;
		}

		// Token: 0x04000F74 RID: 3956
		internal readonly int pageSize;

		// Token: 0x04000F75 RID: 3957
		internal readonly Stream reader;
	}
}
