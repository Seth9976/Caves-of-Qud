using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI
{
	// Token: 0x0200149D RID: 5277
	public class ScrollViewCalcs
	{
		// Token: 0x0600CF54 RID: 53076 RVA: 0x004BB1E8 File Offset: 0x004B93E8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("contentHeight: " + this.contentHeight.ToString() + " ");
			stringBuilder.Append("scrollHeight: " + this.scrollHeight.ToString() + " ");
			stringBuilder.Append("contentTop: " + this.contentTop.ToString() + " ");
			stringBuilder.Append("contentBottom: " + this.contentBottom.ToString() + " ");
			stringBuilder.Append("scrollTop: " + this.scrollTop.ToString() + " ");
			stringBuilder.Append("scrollBottom: " + this.scrollBottom.ToString() + " ");
			stringBuilder.Append("scrollPercent: " + this.scrollPercent.ToString() + " ");
			return stringBuilder.ToString();
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600CF55 RID: 53077 RVA: 0x004BB2E6 File Offset: 0x004B94E6
		public bool isAnyAboveView
		{
			get
			{
				return this.scrollTop > this.contentTop;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600CF56 RID: 53078 RVA: 0x004BB2F6 File Offset: 0x004B94F6
		public bool isAnyBelowView
		{
			get
			{
				return this.scrollBottom < this.contentBottom;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600CF57 RID: 53079 RVA: 0x004BB306 File Offset: 0x004B9506
		public bool isAnyInView
		{
			get
			{
				return this.contentBottom > this.scrollTop && this.contentTop < this.scrollBottom;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x0600CF58 RID: 53080 RVA: 0x004BB326 File Offset: 0x004B9526
		public bool isInFullView
		{
			get
			{
				return !this.isAnyAboveView && !this.isAnyBelowView;
			}
		}

		// Token: 0x0600CF59 RID: 53081 RVA: 0x004BB33B File Offset: 0x004B953B
		public void ScrollPageDown()
		{
			this.scroller.verticalNormalizedPosition = this.scrollPercent - this.scrollHeight / this.contentHeight / 2f;
		}

		// Token: 0x0600CF5A RID: 53082 RVA: 0x004BB362 File Offset: 0x004B9562
		public void ScrollPageUp()
		{
			this.scroller.verticalNormalizedPosition = this.scrollPercent + this.scrollHeight / this.contentHeight / 2f;
		}

		// Token: 0x0600CF5B RID: 53083 RVA: 0x004BB389 File Offset: 0x004B9589
		public void ScrollToContentTop(float contentTop)
		{
			this.scroller.verticalNormalizedPosition = 1f - contentTop / (this.contentHeight - this.scrollHeight);
		}

		// Token: 0x0600CF5C RID: 53084 RVA: 0x004BB3AC File Offset: 0x004B95AC
		public static ScrollViewCalcs GetScrollViewCalcs(RectTransform rt, ScrollViewCalcs reuse = null)
		{
			ScrollViewCalcs scrollViewCalcs = reuse ?? new ScrollViewCalcs();
			if (scrollViewCalcs.scroller == null)
			{
				scrollViewCalcs.scroller = rt.GetComponentInParent<ScrollRect>();
			}
			if (scrollViewCalcs.scroller != null && scrollViewCalcs.scroller.vertical)
			{
				scrollViewCalcs.scroller.content.GetWorldCorners(ScrollViewCalcs.contentCorners);
				rt.GetWorldCorners(ScrollViewCalcs.myCorners);
				scrollViewCalcs.contentHeight = scrollViewCalcs.scroller.content.rect.height * (float)Options.StageScale;
				scrollViewCalcs.scrollHeight = scrollViewCalcs.scroller.viewport.rect.height * (float)Options.StageScale;
				scrollViewCalcs.contentTop = ScrollViewCalcs.contentCorners[2].y - ScrollViewCalcs.myCorners[2].y;
				scrollViewCalcs.contentBottom = ScrollViewCalcs.contentCorners[2].y - ScrollViewCalcs.myCorners[0].y;
				scrollViewCalcs.scrollPercent = scrollViewCalcs.scroller.verticalNormalizedPosition;
				scrollViewCalcs.scrollTop = (1f - scrollViewCalcs.scrollPercent) * (scrollViewCalcs.contentHeight - scrollViewCalcs.scrollHeight);
				scrollViewCalcs.scrollBottom = scrollViewCalcs.scrollTop + scrollViewCalcs.scrollHeight;
			}
			return scrollViewCalcs;
		}

		// Token: 0x0600CF5D RID: 53085 RVA: 0x004BB4FC File Offset: 0x004B96FC
		public static void ScrollIntoView(RectTransform rectTransform, ScrollViewCalcs reuse = null)
		{
			ScrollRect componentInParent = rectTransform.GetComponentInParent<ScrollRect>();
			if (componentInParent != null && componentInParent.vertical)
			{
				ScrollViewCalcs scrollViewCalcs = ScrollViewCalcs.GetScrollViewCalcs(rectTransform, reuse);
				if (scrollViewCalcs.scrollHeight < scrollViewCalcs.contentHeight)
				{
					if (scrollViewCalcs.isAnyAboveView)
					{
						componentInParent.verticalNormalizedPosition = 1f - scrollViewCalcs.contentTop / (scrollViewCalcs.contentHeight - scrollViewCalcs.scrollHeight);
						return;
					}
					if (scrollViewCalcs.isAnyBelowView)
					{
						componentInParent.verticalNormalizedPosition = 1f - (scrollViewCalcs.contentBottom - scrollViewCalcs.scrollHeight) / (scrollViewCalcs.contentHeight - scrollViewCalcs.scrollHeight);
					}
				}
			}
		}

		// Token: 0x0600CF5E RID: 53086 RVA: 0x004BB590 File Offset: 0x004B9790
		public static void ScrollToTopOfRect(RectTransform rectTransform, ScrollViewCalcs reuse = null)
		{
			ScrollViewCalcs scrollViewCalcs = ScrollViewCalcs.GetScrollViewCalcs(rectTransform, reuse);
			if (scrollViewCalcs.scrollHeight < scrollViewCalcs.contentHeight)
			{
				scrollViewCalcs.scroller.verticalNormalizedPosition = 1f - scrollViewCalcs.contentTop / (scrollViewCalcs.contentHeight - scrollViewCalcs.scrollHeight);
			}
		}

		// Token: 0x0600CF5F RID: 53087 RVA: 0x004BB5D8 File Offset: 0x004B97D8
		public static void ScrollToBottomOfRect(RectTransform rectTransform, ScrollViewCalcs reuse = null)
		{
			ScrollRect componentInParent = rectTransform.GetComponentInParent<ScrollRect>();
			if (componentInParent != null && componentInParent.vertical)
			{
				ScrollViewCalcs scrollViewCalcs = ScrollViewCalcs.GetScrollViewCalcs(rectTransform, reuse);
				if (scrollViewCalcs.scrollHeight < scrollViewCalcs.contentHeight)
				{
					componentInParent.verticalNormalizedPosition = 1f - (scrollViewCalcs.contentBottom - scrollViewCalcs.scrollHeight) / (scrollViewCalcs.contentHeight - scrollViewCalcs.scrollHeight);
				}
			}
		}

		// Token: 0x04005083 RID: 20611
		public ScrollRect scroller;

		// Token: 0x04005084 RID: 20612
		public float contentHeight;

		// Token: 0x04005085 RID: 20613
		public float scrollHeight;

		// Token: 0x04005086 RID: 20614
		public float contentTop;

		// Token: 0x04005087 RID: 20615
		public float contentBottom;

		// Token: 0x04005088 RID: 20616
		public float scrollTop;

		// Token: 0x04005089 RID: 20617
		public float scrollBottom;

		// Token: 0x0400508A RID: 20618
		public float scrollPercent;

		// Token: 0x0400508B RID: 20619
		private static Vector3[] contentCorners = new Vector3[4];

		// Token: 0x0400508C RID: 20620
		private static Vector3[] myCorners = new Vector3[4];
	}
}
