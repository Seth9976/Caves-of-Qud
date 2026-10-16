using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000257 RID: 599
	internal interface IMouseEventInternal
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060011AE RID: 4526
		// (set) Token: 0x060011AF RID: 4527
		bool triggeredByOS { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060011B0 RID: 4528
		// (set) Token: 0x060011B1 RID: 4529
		IPointerEvent sourcePointerEvent { get; set; }
	}
}
