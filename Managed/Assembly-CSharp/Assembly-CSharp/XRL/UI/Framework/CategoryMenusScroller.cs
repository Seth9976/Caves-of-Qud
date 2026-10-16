using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL.CharacterBuilds;

namespace XRL.UI.Framework
{
	// Token: 0x02001505 RID: 5381
	public class CategoryMenusScroller : FrameworkScroller
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600D1E0 RID: 53728 RVA: 0x004DF055 File Offset: 0x004DD255
		public bool breakDescription
		{
			get
			{
				return this.allowVerticalLayout && this.lastSafeAreaWidth < (float)this.descriptionBottomBreakpoint;
			}
		}

		// Token: 0x0600D1E1 RID: 53729 RVA: 0x004DF070 File Offset: 0x004DD270
		public void UpdateDescriptions(FrameworkDataElement dataElement)
		{
			PrefixMenuOption prefixMenuOption = dataElement as PrefixMenuOption;
			if (prefixMenuOption != null)
			{
				this.selectedTitleText.SetText(prefixMenuOption.Description);
				this.selectedDescriptionText.SetText(prefixMenuOption.LongDescription);
				this.selectedIcon.FromRenderable(prefixMenuOption.Renderable, true);
			}
		}

		// Token: 0x0600D1E2 RID: 53730 RVA: 0x004DF0BD File Offset: 0x004DD2BD
		public NavigationContext ContextFor(int index, int subMenuIndex)
		{
			return this.GetPrefabForIndex(index).GetComponent<FrameworkScroller>().scrollContext.GetContextAt(subMenuIndex);
		}

		// Token: 0x0600D1E3 RID: 53731 RVA: 0x004DF0D6 File Offset: 0x004DD2D6
		public override void ScrollSelectedIntoView()
		{
		}

		// Token: 0x0600D1E4 RID: 53732 RVA: 0x004DF0D8 File Offset: 0x004DD2D8
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor, IEnumerable<FrameworkDataElement> selections = null)
		{
			base.BeforeShow(descriptor, selections);
			if (!this.hasShown)
			{
				try
				{
					this.UpdateDescriptions((this.scrollContext.GetDataAt(0) as CategoryMenuData).menuOptions[0]);
					this.hasShown = true;
				}
				catch
				{
				}
			}
			this.onHighlight.RemoveAllListeners();
			this.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.UpdateDescriptions));
		}

		// Token: 0x0600D1E5 RID: 53733 RVA: 0x004DF158 File Offset: 0x004DD358
		public override ScrollChildContext MakeContextFor(FrameworkDataElement data, int index)
		{
			return new ScrollChildContext
			{
				proxyTo = this.GetPrefabForIndex(index).GetComponent<FrameworkScroller>().scrollContext
			};
		}

		// Token: 0x0600D1E6 RID: 53734 RVA: 0x004DF176 File Offset: 0x004DD376
		public override void SetupPrefab(FrameworkUnityScrollChild newChild, ScrollChildContext context, FrameworkDataElement data, int index)
		{
			base.SetupPrefab(newChild, context, data, index);
			FrameworkScroller component = newChild.GetComponent<FrameworkScroller>();
			component.onSelected = this.onSelected;
			component.onHighlight = this.onHighlight;
		}

		// Token: 0x0600D1E7 RID: 53735 RVA: 0x004DF1A0 File Offset: 0x004DD3A0
		public override void Update()
		{
			base.Update();
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				this.safeArea.anchoredPosition = new Vector2(0f, 0f);
				this.safeArea.sizeDelta = new Vector2(0f, -150f);
				this.centeringFrame.GetComponent<VerticalLayoutGroup>().padding.bottom = 25;
			}
			else
			{
				this.safeArea.anchoredPosition = new Vector2(0f, -25f);
				this.safeArea.sizeDelta = new Vector2(-300f, -250f);
				this.centeringFrame.GetComponent<VerticalLayoutGroup>().padding.bottom = 0;
			}
			if (this.safeArea.rect.width != this.lastSafeAreaWidth)
			{
				if (this.iconLayoutElement != null)
				{
					if (Media.sizeClass < Media.SizeClass.Medium)
					{
						this.iconLayoutElement.minWidth = 40f;
						this.iconLayoutElement.minHeight = 60f;
					}
					else
					{
						this.iconLayoutElement.minWidth = 80f;
						this.iconLayoutElement.minHeight = 120f;
					}
				}
				this.lastSafeAreaWidth = (float)((int)Math.Floor((double)this.safeArea.rect.width));
				if (this.breakDescription)
				{
					this.DescriptionArea.SetParent(this.OuterVLayout, false);
					this.DescriptionArea.SetSiblingIndex(this.DescriptionHLayout.GetSiblingIndex() + 1);
					return;
				}
				this.DescriptionArea.SetParent(this.DescriptionHLayout, false);
				this.DescriptionArea.SetSiblingIndex(2);
			}
		}

		// Token: 0x0400530A RID: 21258
		public UITextSkin selectedTitleText;

		// Token: 0x0400530B RID: 21259
		public UIThreeColorProperties selectedIcon;

		// Token: 0x0400530C RID: 21260
		public UITextSkin selectedDescriptionText;

		// Token: 0x0400530D RID: 21261
		public RectTransform safeArea;

		// Token: 0x0400530E RID: 21262
		public RectTransform OuterVLayout;

		// Token: 0x0400530F RID: 21263
		public RectTransform DescriptionHLayout;

		// Token: 0x04005310 RID: 21264
		public RectTransform DescriptionArea;

		// Token: 0x04005311 RID: 21265
		public float lastSafeAreaWidth;

		// Token: 0x04005312 RID: 21266
		public int descriptionBottomBreakpoint;

		// Token: 0x04005313 RID: 21267
		public bool allowVerticalLayout = true;

		// Token: 0x04005314 RID: 21268
		public LayoutElement iconLayoutElement;

		// Token: 0x04005315 RID: 21269
		public UITextSkin categoryScrollerText;

		// Token: 0x04005316 RID: 21270
		public bool hasShown;
	}
}
