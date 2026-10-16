using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200055D RID: 1373
	[Flags]
	internal enum TypeParameterConstraintKind
	{
		// Token: 0x04001C2B RID: 7211
		None = 0,
		// Token: 0x04001C2C RID: 7212
		ReferenceType = 1,
		// Token: 0x04001C2D RID: 7213
		ValueType = 2,
		// Token: 0x04001C2E RID: 7214
		Constructor = 4,
		// Token: 0x04001C2F RID: 7215
		Unmanaged = 8,
		// Token: 0x04001C30 RID: 7216
		NullableReferenceType = 17,
		// Token: 0x04001C31 RID: 7217
		NotNullableReferenceType = 33,
		// Token: 0x04001C32 RID: 7218
		ObliviousNullabilityIfReferenceType = 64,
		// Token: 0x04001C33 RID: 7219
		NotNull = 128,
		// Token: 0x04001C34 RID: 7220
		Default = 256,
		// Token: 0x04001C35 RID: 7221
		PartialMismatch = 512,
		// Token: 0x04001C36 RID: 7222
		ValueTypeFromConstraintTypes = 1024,
		// Token: 0x04001C37 RID: 7223
		ReferenceTypeFromConstraintTypes = 2048,
		// Token: 0x04001C38 RID: 7224
		AllReferenceTypeKinds = 49,
		// Token: 0x04001C39 RID: 7225
		AllValueTypeKinds = 10,
		// Token: 0x04001C3A RID: 7226
		AllNonNullableKinds = 15
	}
}
