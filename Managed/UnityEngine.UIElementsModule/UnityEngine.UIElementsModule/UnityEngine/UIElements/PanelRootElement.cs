using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000312 RID: 786
	internal class PanelRootElement : VisualElement
	{
		// Token: 0x060016A5 RID: 5797 RVA: 0x00057A29 File Offset: 0x00055C29
		public PanelRootElement()
		{
			base.name = VisualElementUtils.GetUniqueName("unity-panel-container");
			base.viewDataKey = "PanelContainer";
			base.pickingMode = PickingMode.Ignore;
			base.SetAsNextParentWithEventInterests();
		}
	}
}
