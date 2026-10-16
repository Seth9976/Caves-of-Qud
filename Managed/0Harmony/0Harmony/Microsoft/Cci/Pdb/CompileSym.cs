using System;

namespace Microsoft.Cci.Pdb
{
	// Token: 0x020003B5 RID: 949
	internal struct CompileSym
	{
		// Token: 0x04000DB8 RID: 3512
		internal uint flags;

		// Token: 0x04000DB9 RID: 3513
		internal ushort machine;

		// Token: 0x04000DBA RID: 3514
		internal ushort verFEMajor;

		// Token: 0x04000DBB RID: 3515
		internal ushort verFEMinor;

		// Token: 0x04000DBC RID: 3516
		internal ushort verFEBuild;

		// Token: 0x04000DBD RID: 3517
		internal ushort verMajor;

		// Token: 0x04000DBE RID: 3518
		internal ushort verMinor;

		// Token: 0x04000DBF RID: 3519
		internal ushort verBuild;

		// Token: 0x04000DC0 RID: 3520
		internal string verSt;

		// Token: 0x04000DC1 RID: 3521
		internal string[] verArgs;
	}
}
