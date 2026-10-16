using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000AF RID: 175
	public enum SecurityAction : ushort
	{
		// Token: 0x0400025C RID: 604
		Request = 1,
		// Token: 0x0400025D RID: 605
		Demand,
		// Token: 0x0400025E RID: 606
		Assert,
		// Token: 0x0400025F RID: 607
		Deny,
		// Token: 0x04000260 RID: 608
		PermitOnly,
		// Token: 0x04000261 RID: 609
		LinkDemand,
		// Token: 0x04000262 RID: 610
		InheritDemand,
		// Token: 0x04000263 RID: 611
		RequestMinimum,
		// Token: 0x04000264 RID: 612
		RequestOptional,
		// Token: 0x04000265 RID: 613
		RequestRefuse,
		// Token: 0x04000266 RID: 614
		PreJitGrant,
		// Token: 0x04000267 RID: 615
		PreJitDeny,
		// Token: 0x04000268 RID: 616
		NonCasDemand,
		// Token: 0x04000269 RID: 617
		NonCasLinkDemand,
		// Token: 0x0400026A RID: 618
		NonCasInheritance
	}
}
