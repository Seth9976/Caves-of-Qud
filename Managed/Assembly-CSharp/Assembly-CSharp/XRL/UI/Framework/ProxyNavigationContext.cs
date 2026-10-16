using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	// Token: 0x02001526 RID: 5414
	public class ProxyNavigationContext : NavigationContext
	{
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600D2CA RID: 53962 RVA: 0x004E3ABB File Offset: 0x004E1CBB
		// (set) Token: 0x0600D2CB RID: 53963 RVA: 0x004E3AD3 File Offset: 0x004E1CD3
		public override bool disabled
		{
			get
			{
				NavigationContext navigationContext = this.proxyTo;
				if (navigationContext == null)
				{
					return base.disabled;
				}
				return navigationContext.disabled;
			}
			set
			{
				if (this.proxyTo != null)
				{
					this.proxyTo.disabled = value;
					return;
				}
				base.disabled = value;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600D2CC RID: 53964 RVA: 0x004E3AF1 File Offset: 0x004E1CF1
		// (set) Token: 0x0600D2CD RID: 53965 RVA: 0x004E3B0F File Offset: 0x004E1D0F
		public override List<MenuOption> menuOptionDescriptions
		{
			get
			{
				NavigationContext navigationContext = this.proxyTo;
				return ((navigationContext != null) ? navigationContext.menuOptionDescriptions : null) ?? base.menuOptionDescriptions;
			}
			set
			{
				if (this.proxyTo != null)
				{
					this.proxyTo.menuOptionDescriptions = value;
					return;
				}
				base.menuOptionDescriptions = value;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600D2CE RID: 53966 RVA: 0x004E3B2D File Offset: 0x004E1D2D
		public override bool hasChildren
		{
			get
			{
				return this.proxyTo != null || base.hasChildren;
			}
		}

		// Token: 0x0600D2CF RID: 53967 RVA: 0x004E3B3F File Offset: 0x004E1D3F
		public override void Setup()
		{
			if (this.proxyTo != null)
			{
				NavigationContext navigationContext = this.proxyTo;
				navigationContext.parentContext = this;
				navigationContext.Setup();
			}
			base.Setup();
		}

		// Token: 0x0600D2D0 RID: 53968 RVA: 0x004E3B64 File Offset: 0x004E1D64
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.proxyTo != null && !base.currentEvent.handled && !base.currentEvent.cancelled && (NavigationContext)base.currentEvent.data["to"] == this)
			{
				this.proxyTo.Activate(false);
				base.currentEvent.Cancel();
				return;
			}
		}

		// Token: 0x040053A0 RID: 21408
		public NavigationContext proxyTo;
	}
}
