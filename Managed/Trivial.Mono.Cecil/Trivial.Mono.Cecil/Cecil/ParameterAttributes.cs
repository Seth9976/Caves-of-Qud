using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000098 RID: 152
	[Flags]
	public enum ParameterAttributes : ushort
	{
		// Token: 0x04000208 RID: 520
		None = 0,
		// Token: 0x04000209 RID: 521
		In = 1,
		// Token: 0x0400020A RID: 522
		Out = 2,
		// Token: 0x0400020B RID: 523
		Lcid = 4,
		// Token: 0x0400020C RID: 524
		Retval = 8,
		// Token: 0x0400020D RID: 525
		Optional = 16,
		// Token: 0x0400020E RID: 526
		HasDefault = 4096,
		// Token: 0x0400020F RID: 527
		HasFieldMarshal = 8192,
		// Token: 0x04000210 RID: 528
		Unused = 53216
	}
}
