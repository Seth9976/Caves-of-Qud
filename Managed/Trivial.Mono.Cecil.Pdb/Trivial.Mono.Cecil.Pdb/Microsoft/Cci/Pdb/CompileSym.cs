using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x02000025 RID: 37
	internal struct CompileSym
	{
		// Token: 0x04000066 RID: 102
		internal uint flags;

		// Token: 0x04000067 RID: 103
		internal ushort machine;

		// Token: 0x04000068 RID: 104
		internal ushort verFEMajor;

		// Token: 0x04000069 RID: 105
		internal ushort verFEMinor;

		// Token: 0x0400006A RID: 106
		internal ushort verFEBuild;

		// Token: 0x0400006B RID: 107
		internal ushort verMajor;

		// Token: 0x0400006C RID: 108
		internal ushort verMinor;

		// Token: 0x0400006D RID: 109
		internal ushort verBuild;

		// Token: 0x0400006E RID: 110
		internal string verSt;

		// Token: 0x0400006F RID: 111
		internal string[] verArgs;
	}
}
