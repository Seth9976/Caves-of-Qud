using System;

namespace Mono.Cecil
{
	// Token: 0x02000251 RID: 593
	[Flags]
	public enum ParameterAttributes : ushort
	{
		// Token: 0x04000486 RID: 1158
		None = 0,
		// Token: 0x04000487 RID: 1159
		In = 1,
		// Token: 0x04000488 RID: 1160
		Out = 2,
		// Token: 0x04000489 RID: 1161
		Lcid = 4,
		// Token: 0x0400048A RID: 1162
		Retval = 8,
		// Token: 0x0400048B RID: 1163
		Optional = 16,
		// Token: 0x0400048C RID: 1164
		HasDefault = 4096,
		// Token: 0x0400048D RID: 1165
		HasFieldMarshal = 8192,
		// Token: 0x0400048E RID: 1166
		Unused = 53216
	}
}
