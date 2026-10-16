using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000057 RID: 87
	internal struct DbiHeader
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000044E0 File Offset: 0x000026E0
		internal DbiHeader(BitAccess bits)
		{
			bits.ReadInt32(out this.sig);
			bits.ReadInt32(out this.ver);
			bits.ReadInt32(out this.age);
			bits.ReadInt16(out this.gssymStream);
			bits.ReadUInt16(out this.vers);
			bits.ReadInt16(out this.pssymStream);
			bits.ReadUInt16(out this.pdbver);
			bits.ReadInt16(out this.symrecStream);
			bits.ReadUInt16(out this.pdbver2);
			bits.ReadInt32(out this.gpmodiSize);
			bits.ReadInt32(out this.secconSize);
			bits.ReadInt32(out this.secmapSize);
			bits.ReadInt32(out this.filinfSize);
			bits.ReadInt32(out this.tsmapSize);
			bits.ReadInt32(out this.mfcIndex);
			bits.ReadInt32(out this.dbghdrSize);
			bits.ReadInt32(out this.ecinfoSize);
			bits.ReadUInt16(out this.flags);
			bits.ReadUInt16(out this.machine);
			bits.ReadInt32(out this.reserved);
		}

		// Token: 0x04000175 RID: 373
		internal int sig;

		// Token: 0x04000176 RID: 374
		internal int ver;

		// Token: 0x04000177 RID: 375
		internal int age;

		// Token: 0x04000178 RID: 376
		internal short gssymStream;

		// Token: 0x04000179 RID: 377
		internal ushort vers;

		// Token: 0x0400017A RID: 378
		internal short pssymStream;

		// Token: 0x0400017B RID: 379
		internal ushort pdbver;

		// Token: 0x0400017C RID: 380
		internal short symrecStream;

		// Token: 0x0400017D RID: 381
		internal ushort pdbver2;

		// Token: 0x0400017E RID: 382
		internal int gpmodiSize;

		// Token: 0x0400017F RID: 383
		internal int secconSize;

		// Token: 0x04000180 RID: 384
		internal int secmapSize;

		// Token: 0x04000181 RID: 385
		internal int filinfSize;

		// Token: 0x04000182 RID: 386
		internal int tsmapSize;

		// Token: 0x04000183 RID: 387
		internal int mfcIndex;

		// Token: 0x04000184 RID: 388
		internal int dbghdrSize;

		// Token: 0x04000185 RID: 389
		internal int ecinfoSize;

		// Token: 0x04000186 RID: 390
		internal ushort flags;

		// Token: 0x04000187 RID: 391
		internal ushort machine;

		// Token: 0x04000188 RID: 392
		internal int reserved;
	}
}
