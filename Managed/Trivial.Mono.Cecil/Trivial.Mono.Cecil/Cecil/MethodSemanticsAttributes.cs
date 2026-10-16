using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000084 RID: 132
	[Flags]
	public enum MethodSemanticsAttributes : ushort
	{
		// Token: 0x0400018F RID: 399
		None = 0,
		// Token: 0x04000190 RID: 400
		Setter = 1,
		// Token: 0x04000191 RID: 401
		Getter = 2,
		// Token: 0x04000192 RID: 402
		Other = 4,
		// Token: 0x04000193 RID: 403
		AddOn = 8,
		// Token: 0x04000194 RID: 404
		RemoveOn = 16,
		// Token: 0x04000195 RID: 405
		Fire = 32
	}
}
