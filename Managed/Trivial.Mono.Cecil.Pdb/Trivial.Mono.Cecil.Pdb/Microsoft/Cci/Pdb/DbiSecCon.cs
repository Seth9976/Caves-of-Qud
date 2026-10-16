using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000059 RID: 89
	internal struct DbiSecCon
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000046BC File Offset: 0x000028BC
		internal DbiSecCon(BitAccess bits)
		{
			bits.ReadInt16(out this.section);
			bits.ReadInt16(out this.pad1);
			bits.ReadInt32(out this.offset);
			bits.ReadInt32(out this.size);
			bits.ReadUInt32(out this.flags);
			bits.ReadInt16(out this.module);
			bits.ReadInt16(out this.pad2);
			bits.ReadUInt32(out this.dataCrc);
			bits.ReadUInt32(out this.relocCrc);
		}

		// Token: 0x04000196 RID: 406
		internal short section;

		// Token: 0x04000197 RID: 407
		internal short pad1;

		// Token: 0x04000198 RID: 408
		internal int offset;

		// Token: 0x04000199 RID: 409
		internal int size;

		// Token: 0x0400019A RID: 410
		internal uint flags;

		// Token: 0x0400019B RID: 411
		internal short module;

		// Token: 0x0400019C RID: 412
		internal short pad2;

		// Token: 0x0400019D RID: 413
		internal uint dataCrc;

		// Token: 0x0400019E RID: 414
		internal uint relocCrc;
	}
}
