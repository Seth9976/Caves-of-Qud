using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BE RID: 446
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public enum TransferFunction
	{
		// Token: 0x040005CD RID: 1485
		Unknown = -1,
		// Token: 0x040005CE RID: 1486
		sRGB,
		// Token: 0x040005CF RID: 1487
		BT1886,
		// Token: 0x040005D0 RID: 1488
		PQ,
		// Token: 0x040005D1 RID: 1489
		Linear,
		// Token: 0x040005D2 RID: 1490
		Gamma22
	}
}
