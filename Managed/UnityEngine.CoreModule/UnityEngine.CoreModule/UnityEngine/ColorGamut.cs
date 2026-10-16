using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BB RID: 443
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public enum ColorGamut
	{
		// Token: 0x040005BD RID: 1469
		sRGB,
		// Token: 0x040005BE RID: 1470
		Rec709,
		// Token: 0x040005BF RID: 1471
		Rec2020,
		// Token: 0x040005C0 RID: 1472
		DisplayP3,
		// Token: 0x040005C1 RID: 1473
		HDR10,
		// Token: 0x040005C2 RID: 1474
		DolbyHDR,
		// Token: 0x040005C3 RID: 1475
		P3D65G22
	}
}
