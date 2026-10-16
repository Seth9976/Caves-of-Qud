using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020000CC RID: 204
	internal struct ProcSym32
	{
		// Token: 0x040003A3 RID: 931
		internal uint parent;

		// Token: 0x040003A4 RID: 932
		internal uint end;

		// Token: 0x040003A5 RID: 933
		internal uint next;

		// Token: 0x040003A6 RID: 934
		internal uint len;

		// Token: 0x040003A7 RID: 935
		internal uint dbgStart;

		// Token: 0x040003A8 RID: 936
		internal uint dbgEnd;

		// Token: 0x040003A9 RID: 937
		internal uint typind;

		// Token: 0x040003AA RID: 938
		internal uint off;

		// Token: 0x040003AB RID: 939
		internal ushort seg;

		// Token: 0x040003AC RID: 940
		internal byte flags;

		// Token: 0x040003AD RID: 941
		internal string name;
	}
}
