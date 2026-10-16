using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029F RID: 671
	[EventCategory(EventCategory.EnterLeave)]
	public sealed class PointerOutEvent : PointerEventBase<PointerOutEvent>
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x0004ECB5 File Offset: 0x0004CEB5
		static PointerOutEvent()
		{
			EventBase<PointerOutEvent>.SetCreateFunction(() => new PointerOutEvent());
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0004B8D2 File Offset: 0x00049AD2
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToAssignedTarget(this, panel);
		}
	}
}
