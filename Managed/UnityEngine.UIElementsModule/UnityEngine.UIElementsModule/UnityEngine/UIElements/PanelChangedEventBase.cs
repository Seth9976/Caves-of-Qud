using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027E RID: 638
	[EventCategory(EventCategory.ChangePanel)]
	public abstract class PanelChangedEventBase<T> : EventBase<T>, IPanelChangedEvent where T : PanelChangedEventBase<T>, new()
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0004C790 File Offset: 0x0004A990
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x0004C798 File Offset: 0x0004A998
		public IPanel originPanel { get; private set; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0004C7A1 File Offset: 0x0004A9A1
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x0004C7A9 File Offset: 0x0004A9A9
		public IPanel destinationPanel { get; private set; }

		// Token: 0x06001283 RID: 4739 RVA: 0x0004C7B2 File Offset: 0x0004A9B2
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0004C7C3 File Offset: 0x0004A9C3
		private void LocalInit()
		{
			this.originPanel = null;
			this.destinationPanel = null;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0004C7D8 File Offset: 0x0004A9D8
		public static T GetPooled(IPanel originPanel, IPanel destinationPanel)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.originPanel = originPanel;
			pooled.destinationPanel = destinationPanel;
			return pooled;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0004C80B File Offset: 0x0004AA0B
		protected PanelChangedEventBase()
		{
			this.LocalInit();
		}
	}
}
