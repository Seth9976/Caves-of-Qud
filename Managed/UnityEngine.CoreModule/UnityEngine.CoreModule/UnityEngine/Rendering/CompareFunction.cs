using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200048F RID: 1167
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum CompareFunction
	{
		// Token: 0x04000D0E RID: 3342
		Disabled,
		// Token: 0x04000D0F RID: 3343
		Never,
		// Token: 0x04000D10 RID: 3344
		Less,
		// Token: 0x04000D11 RID: 3345
		Equal,
		// Token: 0x04000D12 RID: 3346
		LessEqual,
		// Token: 0x04000D13 RID: 3347
		Greater,
		// Token: 0x04000D14 RID: 3348
		NotEqual,
		// Token: 0x04000D15 RID: 3349
		GreaterEqual,
		// Token: 0x04000D16 RID: 3350
		Always
	}
}
