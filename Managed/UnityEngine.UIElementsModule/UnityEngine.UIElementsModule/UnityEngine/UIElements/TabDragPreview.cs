using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019E RID: 414
	internal class TabDragPreview : VisualElement
	{
		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003A4FA File Offset: 0x000386FA
		public TabDragPreview()
		{
			base.AddToClassList(TabDragPreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
		}

		// Token: 0x04000827 RID: 2087
		public static readonly string ussClassName = TabView.ussClassName + "__drag-preview";
	}
}
