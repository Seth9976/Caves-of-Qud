using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200048E RID: 1166
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendOp
	{
		// Token: 0x04000CE9 RID: 3305
		Add,
		// Token: 0x04000CEA RID: 3306
		Subtract,
		// Token: 0x04000CEB RID: 3307
		ReverseSubtract,
		// Token: 0x04000CEC RID: 3308
		Min,
		// Token: 0x04000CED RID: 3309
		Max,
		// Token: 0x04000CEE RID: 3310
		LogicalClear,
		// Token: 0x04000CEF RID: 3311
		LogicalSet,
		// Token: 0x04000CF0 RID: 3312
		LogicalCopy,
		// Token: 0x04000CF1 RID: 3313
		LogicalCopyInverted,
		// Token: 0x04000CF2 RID: 3314
		LogicalNoop,
		// Token: 0x04000CF3 RID: 3315
		LogicalInvert,
		// Token: 0x04000CF4 RID: 3316
		LogicalAnd,
		// Token: 0x04000CF5 RID: 3317
		LogicalNand,
		// Token: 0x04000CF6 RID: 3318
		LogicalOr,
		// Token: 0x04000CF7 RID: 3319
		LogicalNor,
		// Token: 0x04000CF8 RID: 3320
		LogicalXor,
		// Token: 0x04000CF9 RID: 3321
		LogicalEquivalence,
		// Token: 0x04000CFA RID: 3322
		LogicalAndReverse,
		// Token: 0x04000CFB RID: 3323
		LogicalAndInverted,
		// Token: 0x04000CFC RID: 3324
		LogicalOrReverse,
		// Token: 0x04000CFD RID: 3325
		LogicalOrInverted,
		// Token: 0x04000CFE RID: 3326
		Multiply,
		// Token: 0x04000CFF RID: 3327
		Screen,
		// Token: 0x04000D00 RID: 3328
		Overlay,
		// Token: 0x04000D01 RID: 3329
		Darken,
		// Token: 0x04000D02 RID: 3330
		Lighten,
		// Token: 0x04000D03 RID: 3331
		ColorDodge,
		// Token: 0x04000D04 RID: 3332
		ColorBurn,
		// Token: 0x04000D05 RID: 3333
		HardLight,
		// Token: 0x04000D06 RID: 3334
		SoftLight,
		// Token: 0x04000D07 RID: 3335
		Difference,
		// Token: 0x04000D08 RID: 3336
		Exclusion,
		// Token: 0x04000D09 RID: 3337
		HSLHue,
		// Token: 0x04000D0A RID: 3338
		HSLSaturation,
		// Token: 0x04000D0B RID: 3339
		HSLColor,
		// Token: 0x04000D0C RID: 3340
		HSLLuminosity
	}
}
