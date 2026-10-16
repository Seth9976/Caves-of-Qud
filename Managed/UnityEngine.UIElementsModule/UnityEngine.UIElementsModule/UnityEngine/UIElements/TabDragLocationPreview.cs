using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019F RID: 415
	internal class TabDragLocationPreview : VisualElement
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003A52E File Offset: 0x0003872E
		internal VisualElement preview
		{
			get
			{
				return this.m_Preview;
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0003A538 File Offset: 0x00038738
		public TabDragLocationPreview()
		{
			base.AddToClassList(TabDragLocationPreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_Preview = new VisualElement();
			this.m_Preview.AddToClassList(TabDragLocationPreview.visualUssClassName);
			this.m_Preview.pickingMode = PickingMode.Ignore;
			base.Add(this.m_Preview);
		}

		// Token: 0x04000828 RID: 2088
		public static readonly string ussClassName = TabView.ussClassName + "__drag-location-preview";

		// Token: 0x04000829 RID: 2089
		public static readonly string visualUssClassName = TabDragLocationPreview.ussClassName + "__visual";

		// Token: 0x0400082A RID: 2090
		public static readonly string verticalUssClassName = TabDragLocationPreview.ussClassName + "__vertical";

		// Token: 0x0400082B RID: 2091
		public static readonly string horizontalUssClassName = TabDragLocationPreview.ussClassName + "__horizontal";

		// Token: 0x0400082C RID: 2092
		private VisualElement m_Preview;
	}
}
