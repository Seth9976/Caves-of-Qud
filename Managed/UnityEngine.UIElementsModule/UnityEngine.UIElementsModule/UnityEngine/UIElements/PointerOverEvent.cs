using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029D RID: 669
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerOverEvent : PointerEventBase<PointerOverEvent>
	{
		// Token: 0x06001360 RID: 4960 RVA: 0x0004EC80 File Offset: 0x0004CE80
		static PointerOverEvent()
		{
			EventBase<PointerOverEvent>.SetCreateFunction(() => new PointerOverEvent());
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
