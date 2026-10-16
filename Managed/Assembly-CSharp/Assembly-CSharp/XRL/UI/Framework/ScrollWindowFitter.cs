using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x020014E9 RID: 5353
	[ExecuteAlways]
	public class ScrollWindowFitter : MonoBehaviour, ILayoutElement
	{
		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600D136 RID: 53558 RVA: 0x004DAB2D File Offset: 0x004D8D2D
		public float maxHeight
		{
			get
			{
				return (float)((double)Screen.height / Options.StageScale) - this.verticalSpaceReserved;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600D137 RID: 53559 RVA: 0x004DAB43 File Offset: 0x004D8D43
		public bool isScrolling
		{
			get
			{
				return this.Scroller.verticalScrollbar.enabled;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x0600D138 RID: 53560 RVA: 0x004DAB55 File Offset: 0x004D8D55
		public float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x0600D139 RID: 53561 RVA: 0x004DAB5C File Offset: 0x004D8D5C
		public float preferredWidth
		{
			get
			{
				return this.contentWidth + (this.isScrolling ? this.extraWidthForScroll : 0f);
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x0600D13A RID: 53562 RVA: 0x004DAB7A File Offset: 0x004D8D7A
		public float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x0600D13B RID: 53563 RVA: 0x004DAB81 File Offset: 0x004D8D81
		public float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x0600D13C RID: 53564 RVA: 0x004DAB88 File Offset: 0x004D8D88
		public float preferredHeight
		{
			get
			{
				return Math.Min(this.maxHeight, this.contentHeight);
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600D13D RID: 53565 RVA: 0x004DAB9B File Offset: 0x004D8D9B
		public float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600D13E RID: 53566 RVA: 0x004DABA2 File Offset: 0x004D8DA2
		public int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600D13F RID: 53567 RVA: 0x004DABA5 File Offset: 0x004D8DA5
		public void CalculateLayoutInputHorizontal()
		{
			this.contentWidth = LayoutUtility.GetPreferredWidth(this.Content);
		}

		// Token: 0x0600D140 RID: 53568 RVA: 0x004DABB8 File Offset: 0x004D8DB8
		public void CalculateLayoutInputVertical()
		{
			this.contentHeight = LayoutUtility.GetPreferredHeight(this.Content);
		}

		// Token: 0x04005286 RID: 21126
		public RectTransform Content;

		// Token: 0x04005287 RID: 21127
		public ScrollRect Scroller;

		// Token: 0x04005288 RID: 21128
		public float verticalSpaceReserved;

		// Token: 0x04005289 RID: 21129
		public float extraWidthForScroll;

		// Token: 0x0400528A RID: 21130
		public float contentHeight;

		// Token: 0x0400528B RID: 21131
		public float contentWidth;
	}
}
