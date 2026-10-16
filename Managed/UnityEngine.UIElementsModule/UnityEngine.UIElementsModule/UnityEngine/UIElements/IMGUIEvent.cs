using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B3 RID: 691
	[EventCategory(EventCategory.IMGUI)]
	public class IMGUIEvent : EventBase<IMGUIEvent>
	{
		// Token: 0x060013AF RID: 5039 RVA: 0x0004F25C File Offset: 0x0004D45C
		static IMGUIEvent()
		{
			EventBase<IMGUIEvent>.SetCreateFunction(() => new IMGUIEvent());
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004F278 File Offset: 0x0004D478
		public static IMGUIEvent GetPooled(Event systemEvent)
		{
			IMGUIEvent pooled = EventBase<IMGUIEvent>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004F299 File Offset: 0x0004D499
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004A573 File Offset: 0x00048773
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0004F2AA File Offset: 0x0004D4AA
		public IMGUIEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0004F2BB File Offset: 0x0004D4BB
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToPanelRoot(this, panel);
		}
	}
}
