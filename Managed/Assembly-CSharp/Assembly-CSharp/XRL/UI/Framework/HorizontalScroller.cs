using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL.CharacterBuilds;

namespace XRL.UI.Framework
{
	// Token: 0x02001519 RID: 5401
	public class HorizontalScroller : FrameworkScroller
	{
		// Token: 0x0600D248 RID: 53832 RVA: 0x004E0E74 File Offset: 0x004DF074
		public override void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor, IEnumerable<FrameworkDataElement> selections = null)
		{
			if (this.descriptionText != null)
			{
				this.descriptionText.GetComponent<LayoutElement>().preferredHeight = 0f;
				this.descriptionText.GetComponent<LayoutElement>().preferredHeight = (float)this.CalculateTallestDescription(selections);
			}
			base.BeforeShow(descriptor, selections);
			if (base.gridLayout != null)
			{
				this.scrollContext.SetAxis(this.NavigationAxis, true);
				this.scrollContext.calculateGridWidth = new Func<int>(this.CalculateGridColumns);
			}
			FastLayoutRebuilder.ForceRebuildLayoutImmediate(base.transform as RectTransform);
			int num = Math.Min(this.scrollContext.length - 1, this.scrollContext.selectedPosition);
			if (this.scrollContext.selectedPosition != num && this.scrollContext.length > 0)
			{
				this.scrollContext.SelectIndex(num);
			}
			UITextSkin uitextSkin = this.descriptionText;
			if (uitextSkin != null)
			{
				uitextSkin.SetText(this.scrollContext.data[this.scrollContext.selectedPosition].Description);
			}
			this.Update();
		}

		// Token: 0x0600D249 RID: 53833 RVA: 0x004E0F8C File Offset: 0x004DF18C
		public override void UpdateWidth()
		{
			base.UpdateWidth();
			ScrollRect scrollRect = this.scrollRect;
			LayoutElement layoutElement = ((scrollRect != null) ? scrollRect.GetComponent<LayoutElement>() : null);
			if (layoutElement == null)
			{
				return;
			}
			layoutElement.preferredWidth = 9999f;
			Canvas.ForceUpdateCanvases();
			if (this.CalculateGridColumns() > 0)
			{
				int num = (int)Math.Ceiling((double)this.choices.Count / (double)this.CalculateGridColumns());
				if (num > 1)
				{
					layoutElement.preferredWidth = this.WidthPerGrid() * (float)this.PreferredRows((int)Math.Ceiling((double)this.choices.Count / (double)num)) + 10f;
				}
			}
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x0600D24A RID: 53834 RVA: 0x004E102D File Offset: 0x004DF22D
		public IEnumerable<FrameworkDataElement> selectionAndChildren(FrameworkDataElement selection)
		{
			yield return selection;
			IFrameworkDataList frameworkDataList = selection as IFrameworkDataList;
			if (frameworkDataList != null)
			{
				foreach (FrameworkDataElement frameworkDataElement in frameworkDataList.getChildren())
				{
					yield return frameworkDataElement;
				}
				IEnumerator<FrameworkDataElement> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600D24B RID: 53835 RVA: 0x004E1040 File Offset: 0x004DF240
		public virtual int CalculateTallestDescription(IEnumerable<FrameworkDataElement> selections)
		{
			int num = 100;
			foreach (FrameworkDataElement frameworkDataElement in selections.SelectMany<FrameworkDataElement, FrameworkDataElement>(new Func<FrameworkDataElement, IEnumerable<FrameworkDataElement>>(this.selectionAndChildren)))
			{
				this.descriptionText.SetText(frameworkDataElement.Description);
				num = Math.Max(num, (int)this.descriptionText.preferredHeight);
			}
			return num;
		}

		// Token: 0x0600D24C RID: 53836 RVA: 0x004E10BC File Offset: 0x004DF2BC
		public float WidthPerGrid()
		{
			if (base.gridLayout == null)
			{
				return 0f;
			}
			if (this.scrollContext.length < 2)
			{
				return 0f;
			}
			return Math.Abs(base.gridLayout.transform.GetChild(0).localPosition.x - base.gridLayout.transform.GetChild(1).localPosition.x);
		}

		// Token: 0x0600D24D RID: 53837 RVA: 0x004E1130 File Offset: 0x004DF330
		public int CalculateGridColumns()
		{
			if (base.gridLayout == null)
			{
				return 0;
			}
			if (this.scrollContext.length == 0)
			{
				return 0;
			}
			int num = 0;
			Transform child = base.gridLayout.transform.GetChild(0);
			Vector3 vector = ((child != null) ? child.localPosition : Vector3.zero);
			foreach (object obj in base.gridLayout.transform)
			{
				Transform transform = (Transform)obj;
				if (!Mathf.Approximately(vector.y, transform.localPosition.y))
				{
					break;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600D24E RID: 53838 RVA: 0x004E11EC File Offset: 0x004DF3EC
		public override void UpdateSelection()
		{
			UITextSkin uitextSkin = this.descriptionText;
			if (uitextSkin != null)
			{
				uitextSkin.SetText(this.scrollContext.data[this.scrollContext.selectedPosition].Description);
			}
			base.UpdateSelection();
		}

		// Token: 0x04005360 RID: 21344
		public UITextSkin descriptionText;

		// Token: 0x04005361 RID: 21345
		public Func<int, int> PreferredRows = (int input) => input;
	}
}
