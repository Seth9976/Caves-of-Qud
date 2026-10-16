using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x020014E8 RID: 5352
	public class HasSelectionCaret : MonoBehaviour
	{
		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600D132 RID: 53554 RVA: 0x004DA9E2 File Offset: 0x004D8BE2
		public bool selected
		{
			get
			{
				NavigationContext context = this.GetSelectable().context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x0600D133 RID: 53555 RVA: 0x004DA9FC File Offset: 0x004D8BFC
		public FrameworkContext GetSelectable()
		{
			FrameworkContext frameworkContext;
			if ((frameworkContext = this.selectable) == null)
			{
				frameworkContext = (this.selectable = base.GetComponentInParent<FrameworkContext>());
			}
			return frameworkContext;
		}

		// Token: 0x0600D134 RID: 53556 RVA: 0x004DAA24 File Offset: 0x004D8C24
		public void LateUpdate()
		{
			bool selected = this.selected;
			bool? flag = this.wasSelected;
			if (!((selected == flag.GetValueOrDefault()) & (flag != null)) && this.useSelectEnabled && this.image != null && this.image.enabled != this.selected)
			{
				this.image.enabled = this.selected;
			}
			bool selected2 = this.selected;
			flag = this.wasSelected;
			if (!((selected2 == flag.GetValueOrDefault()) & (flag != null)) && this.useSelectColor && this.image != null)
			{
				Color color = (this.selected ? this.selectedColor : this.unselectedColor);
				if (this.image.color != color)
				{
					this.image.color = color;
				}
			}
			this.wasSelected = new bool?(this.selected);
		}

		// Token: 0x0400527F RID: 21119
		public bool useSelectEnabled = true;

		// Token: 0x04005280 RID: 21120
		private bool? wasSelected;

		// Token: 0x04005281 RID: 21121
		public Image image;

		// Token: 0x04005282 RID: 21122
		public FrameworkContext selectable;

		// Token: 0x04005283 RID: 21123
		public bool useSelectColor;

		// Token: 0x04005284 RID: 21124
		public Color unselectedColor = Color.gray;

		// Token: 0x04005285 RID: 21125
		public Color selectedColor = Color.yellow;
	}
}
