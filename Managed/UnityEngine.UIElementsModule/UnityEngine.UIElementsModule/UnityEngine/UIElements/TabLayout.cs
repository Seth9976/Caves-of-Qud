using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A0 RID: 416
	internal class TabLayout
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003A5F5 File Offset: 0x000387F5
		public TabLayout(TabView tabView, bool isVertical)
		{
			this.m_TabView = tabView;
			this.m_TabHeaders = tabView.tabHeaders;
			this.m_IsVertical = isVertical;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0003A61C File Offset: 0x0003881C
		public static float GetHeight(VisualElement t)
		{
			return t.boundingBox.height;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0003A63C File Offset: 0x0003883C
		public static float GetWidth(VisualElement t)
		{
			return t.boundingBox.width;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0003A65C File Offset: 0x0003885C
		public float GetTabOffset(VisualElement tab)
		{
			bool flag = !tab.visible;
			float num;
			if (flag)
			{
				num = float.NaN;
			}
			else
			{
				float num2 = 0f;
				int num3 = this.m_TabHeaders.IndexOf(tab);
				for (int i = 0; i < num3; i++)
				{
					VisualElement visualElement = this.m_TabHeaders[i];
					float num4 = (this.m_IsVertical ? TabLayout.GetHeight(visualElement) : TabLayout.GetWidth(visualElement));
					bool flag2 = float.IsNaN(num4);
					if (!flag2)
					{
						num2 += num4;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0003A6EC File Offset: 0x000388EC
		private void InitOrderTabs()
		{
			if (this.m_TabHeaders == null)
			{
				this.m_TabHeaders = new List<VisualElement>();
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0003A702 File Offset: 0x00038902
		public void ReorderDisplay(int from, int to)
		{
			this.InitOrderTabs();
			this.m_TabView.ReorderTab(from, to);
		}

		// Token: 0x0400082D RID: 2093
		private TabView m_TabView;

		// Token: 0x0400082E RID: 2094
		private List<VisualElement> m_TabHeaders;

		// Token: 0x0400082F RID: 2095
		private bool m_IsVertical;
	}
}
