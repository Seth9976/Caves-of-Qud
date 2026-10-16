using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003F5 RID: 1013
	internal struct DbiHeader
	{
		// Token: 0x060015A4 RID: 5540 RVA: 0x00043F44 File Offset: 0x00042144
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

		// Token: 0x04000F0B RID: 3851
		internal int sig;

		// Token: 0x04000F0C RID: 3852
		internal int ver;

		// Token: 0x04000F0D RID: 3853
		internal int age;

		// Token: 0x04000F0E RID: 3854
		internal short gssymStream;

		// Token: 0x04000F0F RID: 3855
		internal ushort vers;

		// Token: 0x04000F10 RID: 3856
		internal short pssymStream;

		// Token: 0x04000F11 RID: 3857
		internal ushort pdbver;

		// Token: 0x04000F12 RID: 3858
		internal short symrecStream;

		// Token: 0x04000F13 RID: 3859
		internal ushort pdbver2;

		// Token: 0x04000F14 RID: 3860
		internal int gpmodiSize;

		// Token: 0x04000F15 RID: 3861
		internal int secconSize;

		// Token: 0x04000F16 RID: 3862
		internal int secmapSize;

		// Token: 0x04000F17 RID: 3863
		internal int filinfSize;

		// Token: 0x04000F18 RID: 3864
		internal int tsmapSize;

		// Token: 0x04000F19 RID: 3865
		internal int mfcIndex;

		// Token: 0x04000F1A RID: 3866
		internal int dbghdrSize;

		// Token: 0x04000F1B RID: 3867
		internal int ecinfoSize;

		// Token: 0x04000F1C RID: 3868
		internal ushort flags;

		// Token: 0x04000F1D RID: 3869
		internal ushort machine;

		// Token: 0x04000F1E RID: 3870
		internal int reserved;
	}
}
