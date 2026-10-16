using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000492 RID: 1170
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum StencilOp
	{
		// Token: 0x04000D22 RID: 3362
		Keep,
		// Token: 0x04000D23 RID: 3363
		Zero,
		// Token: 0x04000D24 RID: 3364
		Replace,
		// Token: 0x04000D25 RID: 3365
		IncrementSaturate,
		// Token: 0x04000D26 RID: 3366
		DecrementSaturate,
		// Token: 0x04000D27 RID: 3367
		Invert,
		// Token: 0x04000D28 RID: 3368
		IncrementWrap,
		// Token: 0x04000D29 RID: 3369
		DecrementWrap
	}
}
