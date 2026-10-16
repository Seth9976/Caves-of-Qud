using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A5 RID: 677
	[EventCategory(EventCategory.Tooltip)]
	public class TooltipEvent : EventBase<TooltipEvent>
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x0004EED4 File Offset: 0x0004D0D4
		static TooltipEvent()
		{
			EventBase<TooltipEvent>.SetCreateFunction(() => new TooltipEvent());
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004EEED File Offset: 0x0004D0ED
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0004EEF5 File Offset: 0x0004D0F5
		public string tooltip { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x0004EEFE File Offset: 0x0004D0FE
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x0004EF06 File Offset: 0x0004D106
		public Rect rect { get; set; }

		// Token: 0x0600137F RID: 4991 RVA: 0x0004EF0F File Offset: 0x0004D10F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004EF20 File Offset: 0x0004D120
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.rect = default(Rect);
			this.tooltip = string.Empty;
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0004EF54 File Offset: 0x0004D154
		internal static TooltipEvent GetPooled(string tooltip, Rect rect)
		{
			TooltipEvent pooled = EventBase<TooltipEvent>.GetPooled();
			pooled.tooltip = tooltip;
			pooled.rect = rect;
			return pooled;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0004EF7D File Offset: 0x0004D17D
		public TooltipEvent()
		{
			this.LocalInit();
		}
	}
}
