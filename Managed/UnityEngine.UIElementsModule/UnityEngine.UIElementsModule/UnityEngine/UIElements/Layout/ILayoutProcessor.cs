using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000695 RID: 1685
	internal interface ILayoutProcessor
	{
		// Token: 0x060031B0 RID: 12720
		void CalculateLayout(LayoutNode node, float parentWidth, float parentHeight, LayoutDirection parentDirection);
	}
}
