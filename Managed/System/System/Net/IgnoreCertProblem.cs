using System;

namespace System.Net
{
	// Token: 0x020005ED RID: 1517
	internal enum IgnoreCertProblem
	{
		// Token: 0x04001B97 RID: 7063
		not_time_valid = 1,
		// Token: 0x04001B98 RID: 7064
		ctl_not_time_valid,
		// Token: 0x04001B99 RID: 7065
		not_time_nested = 4,
		// Token: 0x04001B9A RID: 7066
		invalid_basic_constraints = 8,
		// Token: 0x04001B9B RID: 7067
		all_not_time_valid = 7,
		// Token: 0x04001B9C RID: 7068
		allow_unknown_ca = 16,
		// Token: 0x04001B9D RID: 7069
		wrong_usage = 32,
		// Token: 0x04001B9E RID: 7070
		invalid_name = 64,
		// Token: 0x04001B9F RID: 7071
		invalid_policy = 128,
		// Token: 0x04001BA0 RID: 7072
		end_rev_unknown = 256,
		// Token: 0x04001BA1 RID: 7073
		ctl_signer_rev_unknown = 512,
		// Token: 0x04001BA2 RID: 7074
		ca_rev_unknown = 1024,
		// Token: 0x04001BA3 RID: 7075
		root_rev_unknown = 2048,
		// Token: 0x04001BA4 RID: 7076
		all_rev_unknown = 3840,
		// Token: 0x04001BA5 RID: 7077
		none = 4095
	}
}
