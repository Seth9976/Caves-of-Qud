using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000289 RID: 649
	internal interface IPointerEventInternal
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060012C4 RID: 4804
		// (set) Token: 0x060012C5 RID: 4805
		bool triggeredByOS { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060012C6 RID: 4806
		// (set) Token: 0x060012C7 RID: 4807
		IMouseEvent compatibilityMouseEvent { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060012C8 RID: 4808
		// (set) Token: 0x060012C9 RID: 4809
		int displayIndex { get; set; }
	}
}
