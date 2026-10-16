using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028A RID: 650
	internal interface IPointerOrMouseEvent
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060012CA RID: 4810
		int pointerId { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060012CB RID: 4811
		Vector3 position { get; }
	}
}
