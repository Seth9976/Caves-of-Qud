using System;

namespace Mono.Cecil
{
	// Token: 0x02000267 RID: 615
	[Flags]
	internal enum TypeDefinitionTreatment
	{
		// Token: 0x040004E8 RID: 1256
		None = 0,
		// Token: 0x040004E9 RID: 1257
		KindMask = 15,
		// Token: 0x040004EA RID: 1258
		NormalType = 1,
		// Token: 0x040004EB RID: 1259
		NormalAttribute = 2,
		// Token: 0x040004EC RID: 1260
		UnmangleWindowsRuntimeName = 3,
		// Token: 0x040004ED RID: 1261
		PrefixWindowsRuntimeName = 4,
		// Token: 0x040004EE RID: 1262
		RedirectToClrType = 5,
		// Token: 0x040004EF RID: 1263
		RedirectToClrAttribute = 6,
		// Token: 0x040004F0 RID: 1264
		RedirectImplementedMethods = 7,
		// Token: 0x040004F1 RID: 1265
		Abstract = 16,
		// Token: 0x040004F2 RID: 1266
		Internal = 32
	}
}
