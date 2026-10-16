using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000754 RID: 1876
	internal struct Win32_MIB_TCPSTATS
	{
		// Token: 0x0400236E RID: 9070
		public uint RtoAlgorithm;

		// Token: 0x0400236F RID: 9071
		public uint RtoMin;

		// Token: 0x04002370 RID: 9072
		public uint RtoMax;

		// Token: 0x04002371 RID: 9073
		public uint MaxConn;

		// Token: 0x04002372 RID: 9074
		public uint ActiveOpens;

		// Token: 0x04002373 RID: 9075
		public uint PassiveOpens;

		// Token: 0x04002374 RID: 9076
		public uint AttemptFails;

		// Token: 0x04002375 RID: 9077
		public uint EstabResets;

		// Token: 0x04002376 RID: 9078
		public uint CurrEstab;

		// Token: 0x04002377 RID: 9079
		public uint InSegs;

		// Token: 0x04002378 RID: 9080
		public uint OutSegs;

		// Token: 0x04002379 RID: 9081
		public uint RetransSegs;

		// Token: 0x0400237A RID: 9082
		public uint InErrs;

		// Token: 0x0400237B RID: 9083
		public uint OutRsts;

		// Token: 0x0400237C RID: 9084
		public uint NumConns;
	}
}
