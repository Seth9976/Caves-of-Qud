using System;

namespace Mono.Cecil
{
	// Token: 0x02000269 RID: 617
	[Flags]
	internal enum MethodDefinitionTreatment
	{
		// Token: 0x040004F9 RID: 1273
		None = 0,
		// Token: 0x040004FA RID: 1274
		Abstract = 2,
		// Token: 0x040004FB RID: 1275
		Private = 4,
		// Token: 0x040004FC RID: 1276
		Public = 8,
		// Token: 0x040004FD RID: 1277
		Runtime = 16,
		// Token: 0x040004FE RID: 1278
		InternalCall = 32
	}
}
