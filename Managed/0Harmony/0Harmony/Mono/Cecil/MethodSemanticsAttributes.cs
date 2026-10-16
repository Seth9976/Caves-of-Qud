using System;

namespace Mono.Cecil
{
	// Token: 0x0200023E RID: 574
	[Flags]
	public enum MethodSemanticsAttributes : ushort
	{
		// Token: 0x040003DD RID: 989
		None = 0,
		// Token: 0x040003DE RID: 990
		Setter = 1,
		// Token: 0x040003DF RID: 991
		Getter = 2,
		// Token: 0x040003E0 RID: 992
		Other = 4,
		// Token: 0x040003E1 RID: 993
		AddOn = 8,
		// Token: 0x040003E2 RID: 994
		RemoveOn = 16,
		// Token: 0x040003E3 RID: 995
		Fire = 32
	}
}
