using System;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x02001504 RID: 5380
	public class CategoryMenuController : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x0600D1DC RID: 53724 RVA: 0x004DEFD8 File Offset: 0x004DD1D8
		public NavigationContext GetNavigationContext()
		{
			return base.GetComponent<FrameworkScroller>().scrollContext;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600D1DD RID: 53725 RVA: 0x004DEFE8 File Offset: 0x004DD1E8
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

		// Token: 0x0600D1DE RID: 53726 RVA: 0x004DF010 File Offset: 0x004DD210
		public void setData(FrameworkDataElement dataElement)
		{
			CategoryMenuData categoryMenuData = dataElement as CategoryMenuData;
			this.scroller.BeforeShow(null, categoryMenuData.menuOptions);
			this.scroller.titleText.SetText(categoryMenuData.Title);
		}

		// Token: 0x04005309 RID: 21257
		private FrameworkScroller _scroller;
	}
}
