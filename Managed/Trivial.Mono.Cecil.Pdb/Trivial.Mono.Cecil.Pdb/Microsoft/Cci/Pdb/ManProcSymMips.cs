using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000B4 RID: 180
	internal struct ManProcSymMips
	{
		// Token: 0x04000337 RID: 823
		internal uint parent;

		// Token: 0x04000338 RID: 824
		internal uint end;

		// Token: 0x04000339 RID: 825
		internal uint next;

		// Token: 0x0400033A RID: 826
		internal uint len;

		// Token: 0x0400033B RID: 827
		internal uint dbgStart;

		// Token: 0x0400033C RID: 828
		internal uint dbgEnd;

		// Token: 0x0400033D RID: 829
		internal uint regSave;

		// Token: 0x0400033E RID: 830
		internal uint fpSave;

		// Token: 0x0400033F RID: 831
		internal uint intOff;

		// Token: 0x04000340 RID: 832
		internal uint fpOff;

		// Token: 0x04000341 RID: 833
		internal uint token;

		// Token: 0x04000342 RID: 834
		internal uint off;

		// Token: 0x04000343 RID: 835
		internal ushort seg;

		// Token: 0x04000344 RID: 836
		internal byte retReg;

		// Token: 0x04000345 RID: 837
		internal byte frameReg;

		// Token: 0x04000346 RID: 838
		internal string name;
	}
}
