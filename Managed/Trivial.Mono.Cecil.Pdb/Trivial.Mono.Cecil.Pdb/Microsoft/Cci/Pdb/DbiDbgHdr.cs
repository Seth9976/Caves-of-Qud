using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000056 RID: 86
	internal struct DbiDbgHdr
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000444C File Offset: 0x0000264C
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

		// Token: 0x0400016A RID: 362
		internal ushort snFPO;

		// Token: 0x0400016B RID: 363
		internal ushort snException;

		// Token: 0x0400016C RID: 364
		internal ushort snFixup;

		// Token: 0x0400016D RID: 365
		internal ushort snOmapToSrc;

		// Token: 0x0400016E RID: 366
		internal ushort snOmapFromSrc;

		// Token: 0x0400016F RID: 367
		internal ushort snSectionHdr;

		// Token: 0x04000170 RID: 368
		internal ushort snTokenRidMap;

		// Token: 0x04000171 RID: 369
		internal ushort snXdata;

		// Token: 0x04000172 RID: 370
		internal ushort snPdata;

		// Token: 0x04000173 RID: 371
		internal ushort snNewFPO;

		// Token: 0x04000174 RID: 372
		internal ushort snSectionHdrOrig;
	}
}
