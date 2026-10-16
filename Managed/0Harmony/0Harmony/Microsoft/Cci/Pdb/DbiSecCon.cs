using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F7 RID: 1015
	internal struct DbiSecCon
	{
		// Token: 0x060015A6 RID: 5542 RVA: 0x00044120 File Offset: 0x00042320
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

		// Token: 0x04000F2C RID: 3884
		internal short section;

		// Token: 0x04000F2D RID: 3885
		internal short pad1;

		// Token: 0x04000F2E RID: 3886
		internal int offset;

		// Token: 0x04000F2F RID: 3887
		internal int size;

		// Token: 0x04000F30 RID: 3888
		internal uint flags;

		// Token: 0x04000F31 RID: 3889
		internal short module;

		// Token: 0x04000F32 RID: 3890
		internal short pad2;

		// Token: 0x04000F33 RID: 3891
		internal uint dataCrc;

		// Token: 0x04000F34 RID: 3892
		internal uint relocCrc;
	}
}
