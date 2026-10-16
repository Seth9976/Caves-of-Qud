using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x02001521 RID: 5409
	public class SwitchingScroller : HorizontalScrollerScroller
	{
		// Token: 0x0600D27F RID: 53887 RVA: 0x004E2078 File Offset: 0x004E0278
		public override void LateUpdate()
		{
			base.LateUpdate();
			if (this.switchingContainer != null)
			{
				SwitchingScroller.SwitchDirection switchDirection = SwitchingScroller.SwitchDirection.Vertical;
				float num = 0f;
				foreach (object obj in this.switchingContainer.transform)
				{
					Transform transform = (Transform)obj;
					num += transform.GetComponent<RectTransform>().rect.width;
				}
				this.scrollRect.GetComponent<LayoutElement>().preferredHeight = this.childRoot.GetComponent<RectTransform>().rect.height + 50f;
				if (num < this.switchingContainer.GetComponent<RectTransform>().rect.width)
				{
					switchDirection = SwitchingScroller.SwitchDirection.Horizontal;
				}
				if (switchDirection != this.currentDirection)
				{
					this.switchTo(switchDirection);
				}
			}
		}

		// Token: 0x0600D280 RID: 53888 RVA: 0x004E2168 File Offset: 0x004E0368
		public void switchTo(SwitchingScroller.SwitchDirection direction)
		{
			this.currentDirection = direction;
			if (this.switchingContainer == null)
			{
				return;
			}
			VerticalLayoutGroup component = this.switchingContainer.GetComponent<VerticalLayoutGroup>();
			if (component != null)
			{
				component.DestroyImmediate();
			}
			HorizontalLayoutGroup component2 = this.switchingContainer.GetComponent<HorizontalLayoutGroup>();
			if (component2 != null)
			{
				component2.DestroyImmediate();
			}
			if (direction == SwitchingScroller.SwitchDirection.Vertical)
			{
				this.NavigationAxis = InputAxisTypes.NavigationYAxis;
				this.scrollContext.SetAxis(InputAxisTypes.NavigationYAxis, false);
				VerticalLayoutGroup verticalLayoutGroup = this.switchingContainer.AddComponent<VerticalLayoutGroup>();
				verticalLayoutGroup.padding.left = 10;
				verticalLayoutGroup.childForceExpandHeight = false;
				verticalLayoutGroup.childForceExpandWidth = false;
				verticalLayoutGroup.childControlWidth = false;
				verticalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
				return;
			}
			if (direction == SwitchingScroller.SwitchDirection.Horizontal)
			{
				this.NavigationAxis = InputAxisTypes.NavigationXAxis;
				this.scrollContext.SetAxis(InputAxisTypes.NavigationXAxis, false);
				HorizontalLayoutGroup horizontalLayoutGroup = this.switchingContainer.AddComponent<HorizontalLayoutGroup>();
				horizontalLayoutGroup.padding.left = 0;
				horizontalLayoutGroup.childForceExpandHeight = false;
				horizontalLayoutGroup.childForceExpandWidth = false;
				horizontalLayoutGroup.childControlWidth = false;
				horizontalLayoutGroup.childAlignment = TextAnchor.UpperCenter;
			}
		}

		// Token: 0x0600D281 RID: 53889 RVA: 0x004E2250 File Offset: 0x004E0450
		public override void SetupPrefab(FrameworkUnityScrollChild newChild, ScrollChildContext context, FrameworkDataElement data, int index)
		{
			base.SetupPrefab(newChild, context, data, index);
			FrameworkScroller component = newChild.GetComponent<FrameworkScroller>();
			if (component != null)
			{
				component.onSelected = this.onSelected;
				component.onHighlight = this.onHighlight;
			}
		}

		// Token: 0x04005377 RID: 21367
		public GameObject switchingContainer;

		// Token: 0x04005378 RID: 21368
		public SwitchingScroller.SwitchDirection currentDirection;

		// Token: 0x02001E2A RID: 7722
		public enum SwitchDirection
		{
			// Token: 0x040075B7 RID: 30135
			None,
			// Token: 0x040075B8 RID: 30136
			Vertical,
			// Token: 0x040075B9 RID: 30137
			Horizontal
		}
	}
}
