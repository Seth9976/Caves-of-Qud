using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000CD RID: 205
	internal struct ProcSymIa64
	{
		// Token: 0x040003AE RID: 942
		internal uint parent;

		// Token: 0x040003AF RID: 943
		internal uint end;

		// Token: 0x040003B0 RID: 944
		internal uint next;

		// Token: 0x040003B1 RID: 945
		internal uint len;

		// Token: 0x040003B2 RID: 946
		internal uint dbgStart;

		// Token: 0x040003B3 RID: 947
		internal uint dbgEnd;

		// Token: 0x040003B4 RID: 948
		internal uint typind;

		// Token: 0x040003B5 RID: 949
		internal uint off;

		// Token: 0x040003B6 RID: 950
		internal ushort seg;

		// Token: 0x040003B7 RID: 951
		internal ushort retReg;

		// Token: 0x040003B8 RID: 952
		internal byte flags;

		// Token: 0x040003B9 RID: 953
		internal string name;
	}
}
