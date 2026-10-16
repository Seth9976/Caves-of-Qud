using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BC RID: 444
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ColorGamut.h")]
	public enum ColorPrimaries
	{
		// Token: 0x040005C5 RID: 1477
		Unknown = -1,
		// Token: 0x040005C6 RID: 1478
		Rec709,
		// Token: 0x040005C7 RID: 1479
		Rec2020,
		// Token: 0x040005C8 RID: 1480
		P3
	}
}
