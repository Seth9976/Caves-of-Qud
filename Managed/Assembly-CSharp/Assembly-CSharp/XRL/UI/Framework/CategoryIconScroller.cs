using System;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x020014F4 RID: 5364
	public class CategoryIconScroller : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600D19B RID: 53659 RVA: 0x004DE09C File Offset: 0x004DC29C
		public FrameworkScroller scroller
		{
			get
			{
				FrameworkScroller frameworkScroller;
				if ((frameworkScroller = this._scroller) == null)
				{
					frameworkScroller = (this._scroller = base.GetComponent<FrameworkScroller>());
				}
				return frameworkScroller;
			}
		}

		// Token: 0x0600D19C RID: 53660 RVA: 0x004DE0C4 File Offset: 0x004DC2C4
		public void setData(FrameworkDataElement data)
		{
			CategoryIcons categoryIcons = data as CategoryIcons;
			this.scroller.BeforeShow(null, categoryIcons.Choices);
			this.scroller.titleText.color = Color.white;
			this.scroller.titleText.SetText(categoryIcons.Title);
		}

		// Token: 0x0600D19D RID: 53661 RVA: 0x004DE116 File Offset: 0x004DC316
		public NavigationContext GetNavigationContext()
		{
			FrameworkScroller scroller = this.scroller;
			if (scroller == null)
			{
				return null;
			}
			return scroller.GetNavigationContext();
		}

		// Token: 0x040052D4 RID: 21204
		private FrameworkScroller _scroller;
	}
}
