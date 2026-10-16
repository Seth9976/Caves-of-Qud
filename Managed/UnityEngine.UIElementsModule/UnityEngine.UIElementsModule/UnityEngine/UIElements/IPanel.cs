using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000306 RID: 774
	public interface IPanel : IDisposable
	{
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060015BF RID: 5567
		VisualElement visualTree { get; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060015C0 RID: 5568
		EventDispatcher dispatcher { get; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060015C1 RID: 5569
		ContextType contextType { get; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060015C2 RID: 5570
		FocusController focusController { get; }

		// Token: 0x060015C3 RID: 5571
		VisualElement Pick(Vector2 point);

		// Token: 0x060015C4 RID: 5572
		VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060015C5 RID: 5573
		ContextualMenuManager contextualMenuManager { get; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060015C6 RID: 5574
		bool isDirty { get; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060015C7 RID: 5575
		float scaledPixelsPerPoint { get; }
	}
}
