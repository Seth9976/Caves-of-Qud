using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000265 RID: 613
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseLeaveEvent : MouseEventBase<MouseLeaveEvent>
	{
		// Token: 0x06001213 RID: 4627 RVA: 0x0004B8F0 File Offset: 0x00049AF0
		static MouseLeaveEvent()
		{
			EventBase<MouseLeaveEvent>.SetCreateFunction(() => new MouseLeaveEvent());
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004B909 File Offset: 0x00049B09
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004B8B6 File Offset: 0x00049AB6
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004B91A File Offset: 0x00049B1A
		public MouseLeaveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
