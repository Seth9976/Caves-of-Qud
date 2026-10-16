using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F4 RID: 1012
	internal struct DbiDbgHdr
	{
		// Token: 0x060015A3 RID: 5539 RVA: 0x00043EB0 File Offset: 0x000420B0
		internal DbiDbgHdr(BitAccess bits)
		{
			bits.ReadUInt16(out this.snFPO);
			bits.ReadUInt16(out this.snException);
			bits.ReadUInt16(out this.snFixup);
			bits.ReadUInt16(out this.snOmapToSrc);
			bits.ReadUInt16(out this.snOmapFromSrc);
			bits.ReadUInt16(out this.snSectionHdr);
			bits.ReadUInt16(out this.snTokenRidMap);
			bits.ReadUInt16(out this.snXdata);
			bits.ReadUInt16(out this.snPdata);
			bits.ReadUInt16(out this.snNewFPO);
			bits.ReadUInt16(out this.snSectionHdrOrig);
		}

		// Token: 0x04000F00 RID: 3840
		internal ushort snFPO;

		// Token: 0x04000F01 RID: 3841
		internal ushort snException;

		// Token: 0x04000F02 RID: 3842
		internal ushort snFixup;

		// Token: 0x04000F03 RID: 3843
		internal ushort snOmapToSrc;

		// Token: 0x04000F04 RID: 3844
		internal ushort snOmapFromSrc;

		// Token: 0x04000F05 RID: 3845
		internal ushort snSectionHdr;

		// Token: 0x04000F06 RID: 3846
		internal ushort snTokenRidMap;

		// Token: 0x04000F07 RID: 3847
		internal ushort snXdata;

		// Token: 0x04000F08 RID: 3848
		internal ushort snPdata;

		// Token: 0x04000F09 RID: 3849
		internal ushort snNewFPO;

		// Token: 0x04000F0A RID: 3850
		internal ushort snSectionHdrOrig;
	}
}
