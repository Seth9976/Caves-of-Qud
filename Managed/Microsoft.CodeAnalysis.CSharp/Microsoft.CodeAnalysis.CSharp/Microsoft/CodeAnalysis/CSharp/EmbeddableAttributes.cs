using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200021D RID: 541
	[Flags]
	internal enum EmbeddableAttributes
	{
		// Token: 0x0400112C RID: 4396
		IsReadOnlyAttribute = 1,
		// Token: 0x0400112D RID: 4397
		IsByRefLikeAttribute = 2,
		// Token: 0x0400112E RID: 4398
		IsUnmanagedAttribute = 4,
		// Token: 0x0400112F RID: 4399
		NullableAttribute = 8,
		// Token: 0x04001130 RID: 4400
		NullableContextAttribute = 16,
		// Token: 0x04001131 RID: 4401
		NullablePublicOnlyAttribute = 32,
		// Token: 0x04001132 RID: 4402
		NativeIntegerAttribute = 64
	}
}
