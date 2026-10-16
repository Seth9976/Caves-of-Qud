using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000269 RID: 617
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseOutEvent : MouseEventBase<MouseOutEvent>
	{
		// Token: 0x06001222 RID: 4642 RVA: 0x0004B990 File Offset: 0x00049B90
		static MouseOutEvent()
		{
			EventBase<MouseOutEvent>.SetCreateFunction(() => new MouseOutEvent());
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004B9A9 File Offset: 0x00049BA9
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateCursorStyle(this.eventTypeId);
		}
	}
}
