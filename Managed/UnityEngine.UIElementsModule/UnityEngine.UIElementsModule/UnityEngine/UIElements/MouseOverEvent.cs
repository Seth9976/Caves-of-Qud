using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000267 RID: 615
	[EventCategory(EventCategory.EnterLeave)]
	public class MouseOverEvent : MouseEventBase<MouseOverEvent>
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0004B93E File Offset: 0x00049B3E
		static MouseOverEvent()
		{
			EventBase<MouseOverEvent>.SetCreateFunction(() => new MouseOverEvent());
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0004B957 File Offset: 0x00049B57
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateCursorStyle(this.eventTypeId);
		}
	}
}
