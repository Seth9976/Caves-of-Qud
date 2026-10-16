using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200048D RID: 1165
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum BlendMode
	{
		// Token: 0x04000CDD RID: 3293
		Zero,
		// Token: 0x04000CDE RID: 3294
		One,
		// Token: 0x04000CDF RID: 3295
		DstColor,
		// Token: 0x04000CE0 RID: 3296
		SrcColor,
		// Token: 0x04000CE1 RID: 3297
		OneMinusDstColor,
		// Token: 0x04000CE2 RID: 3298
		SrcAlpha,
		// Token: 0x04000CE3 RID: 3299
		OneMinusSrcColor,
		// Token: 0x04000CE4 RID: 3300
		DstAlpha,
		// Token: 0x04000CE5 RID: 3301
		OneMinusDstAlpha,
		// Token: 0x04000CE6 RID: 3302
		SrcAlphaSaturate,
		// Token: 0x04000CE7 RID: 3303
		OneMinusSrcAlpha
	}
}
