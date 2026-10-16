using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200069B RID: 1691
	internal struct LayoutCachedMeasurement
	{
		// Token: 0x04001840 RID: 6208
		public static LayoutCachedMeasurement Default = new LayoutCachedMeasurement
		{
			AvailableWidth = 0f,
			AvailableHeight = 0f,
			ParentWidth = 0f,
			ParentHeight = 0f,
			WidthMeasureMode = LayoutMeasureMode.Invalid,
			HeightMeasureMode = LayoutMeasureMode.Invalid,
			ComputedWidth = -1f,
			ComputedHeight = -1f
		};

		// Token: 0x04001841 RID: 6209
		public float AvailableWidth;

		// Token: 0x04001842 RID: 6210
		public float AvailableHeight;

		// Token: 0x04001843 RID: 6211
		public float ParentWidth;

		// Token: 0x04001844 RID: 6212
		public float ParentHeight;

		// Token: 0x04001845 RID: 6213
		public LayoutMeasureMode WidthMeasureMode;

		// Token: 0x04001846 RID: 6214
		public LayoutMeasureMode HeightMeasureMode;

		// Token: 0x04001847 RID: 6215
		public float ComputedWidth;

		// Token: 0x04001848 RID: 6216
		public float ComputedHeight;
	}
}
