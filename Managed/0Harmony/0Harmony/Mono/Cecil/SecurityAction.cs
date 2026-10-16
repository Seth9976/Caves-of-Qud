using System;

namespace Mono.Cecil
{
	// Token: 0x02000260 RID: 608
	public enum SecurityAction : ushort
	{
		// Token: 0x040004C7 RID: 1223
		Request = 1,
		// Token: 0x040004C8 RID: 1224
		Demand,
		// Token: 0x040004C9 RID: 1225
		Assert,
		// Token: 0x040004CA RID: 1226
		Deny,
		// Token: 0x040004CB RID: 1227
		PermitOnly,
		// Token: 0x040004CC RID: 1228
		LinkDemand,
		// Token: 0x040004CD RID: 1229
		InheritDemand,
		// Token: 0x040004CE RID: 1230
		RequestMinimum,
		// Token: 0x040004CF RID: 1231
		RequestOptional,
		// Token: 0x040004D0 RID: 1232
		RequestRefuse,
		// Token: 0x040004D1 RID: 1233
		PreJitGrant,
		// Token: 0x040004D2 RID: 1234
		PreJitDeny,
		// Token: 0x040004D3 RID: 1235
		NonCasDemand,
		// Token: 0x040004D4 RID: 1236
		NonCasLinkDemand,
		// Token: 0x040004D5 RID: 1237
		NonCasInheritance
	}
}
