using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000219 RID: 537
	public class PointerCaptureEvent : PointerCaptureEventBase<PointerCaptureEvent>
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x00047594 File Offset: 0x00045794
		static PointerCaptureEvent()
		{
			EventBase<PointerCaptureEvent>.SetCreateFunction(() => new PointerCaptureEvent());
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000475AD File Offset: 0x000457AD
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateHoverPseudoStateAfterCaptureChange(base.pointerId);
		}
	}
}
