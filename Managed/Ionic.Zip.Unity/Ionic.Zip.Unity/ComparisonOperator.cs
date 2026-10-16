using System;
using System.ComponentModel;

namespace Ionic
{
	// Token: 0x02000018 RID: 24
	internal enum ComparisonOperator
	{
		// Token: 0x0400003A RID: 58
		[Description(">")]
		GreaterThan,
		// Token: 0x0400003B RID: 59
		[Description(">=")]
		GreaterThanOrEqualTo,
		// Token: 0x0400003C RID: 60
		[Description("<")]
		LesserThan,
		// Token: 0x0400003D RID: 61
		[Description("<=")]
		LesserThanOrEqualTo,
		// Token: 0x0400003E RID: 62
		[Description("=")]
		EqualTo,
		// Token: 0x0400003F RID: 63
		[Description("!=")]
		NotEqualTo
	}
}
