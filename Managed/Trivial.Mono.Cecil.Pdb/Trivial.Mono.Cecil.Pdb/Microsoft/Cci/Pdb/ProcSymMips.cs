using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000CE RID: 206
	internal struct ProcSymMips
	{
		// Token: 0x040003BA RID: 954
		internal uint parent;

		// Token: 0x040003BB RID: 955
		internal uint end;

		// Token: 0x040003BC RID: 956
		internal uint next;

		// Token: 0x040003BD RID: 957
		internal uint len;

		// Token: 0x040003BE RID: 958
		internal uint dbgStart;

		// Token: 0x040003BF RID: 959
		internal uint dbgEnd;

		// Token: 0x040003C0 RID: 960
		internal uint regSave;

		// Token: 0x040003C1 RID: 961
		internal uint fpSave;

		// Token: 0x040003C2 RID: 962
		internal uint intOff;

		// Token: 0x040003C3 RID: 963
		internal uint fpOff;

		// Token: 0x040003C4 RID: 964
		internal uint typind;

		// Token: 0x040003C5 RID: 965
		internal uint off;

		// Token: 0x040003C6 RID: 966
		internal ushort seg;

		// Token: 0x040003C7 RID: 967
		internal byte retReg;

		// Token: 0x040003C8 RID: 968
		internal byte frameReg;

		// Token: 0x040003C9 RID: 969
		internal string name;
	}
}
