using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x0200151A RID: 5402
	public class HorizontalScrollerScroller : HorizontalScroller
	{
		// Token: 0x0600D250 RID: 53840 RVA: 0x004E1253 File Offset: 0x004DF453
		public override ScrollChildContext MakeContextFor(FrameworkDataElement data, int index)
		{
			return new ScrollChildContext
			{
				proxyTo = this.GetPrefabForIndex(index).GetComponent<IFrameworkControl>().GetNavigationContext()
			};
		}

		// Token: 0x0600D251 RID: 53841 RVA: 0x004E1274 File Offset: 0x004DF474
		public void UpdateHighlightText()
		{
			int selectedPosition = this.scrollContext.selectedPosition;
			FrameworkScroller component = this.GetPrefabForIndex(selectedPosition).GetComponent<FrameworkScroller>();
			if (this.descriptionText != null)
			{
				string text = this.scrollContext.data[selectedPosition].Description;
				if (component != null)
				{
					int selectedPosition2 = component.scrollContext.selectedPosition;
					text = component.scrollContext.data[selectedPosition2].Description;
				}
				UITextSkin descriptionText = this.descriptionText;
				if (descriptionText == null)
				{
					return;
				}
				descriptionText.SetText(text);
			}
		}

		// Token: 0x0600D252 RID: 53842 RVA: 0x004E12FD File Offset: 0x004DF4FD
		public override void UpdateSelection()
		{
			base.UpdateSelection();
			this.UpdateHighlightText();
		}

		// Token: 0x0600D253 RID: 53843 RVA: 0x004E130C File Offset: 0x004DF50C
		public void checkSafeArea()
		{
			if (this.safeArea == null)
			{
				return;
			}
			if (Media.sizeClass != this.lastClass)
			{
				this.lastClass = Media.sizeClass;
				if (Media.sizeClass < Media.SizeClass.Medium)
				{
					this.safeArea.anchoredPosition = new Vector2(0f, 0f);
					this.safeArea.sizeDelta = new Vector2(0f, -150f);
				}
				else
				{
					this.safeArea.anchoredPosition = new Vector2(0f, -25f);
					this.safeArea.sizeDelta = new Vector2(-300f, -250f);
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.safeArea);
			}
		}

		// Token: 0x0600D254 RID: 53844 RVA: 0x004E13C1 File Offset: 0x004DF5C1
		public override void Awake()
		{
			this.checkSafeArea();
			base.Awake();
		}

		// Token: 0x0600D255 RID: 53845 RVA: 0x004E13CF File Offset: 0x004DF5CF
		public override void Update()
		{
			this.checkSafeArea();
			base.Update();
		}

		// Token: 0x04005362 RID: 21346
		public RectTransform safeArea;

		// Token: 0x04005363 RID: 21347
		private Media.SizeClass lastClass = Media.SizeClass.Unset;
	}
}
