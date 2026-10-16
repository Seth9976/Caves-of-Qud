using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000217 RID: 535
	public class PointerCaptureOutEvent : PointerCaptureEventBase<PointerCaptureOutEvent>
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x00047542 File Offset: 0x00045742
		static PointerCaptureOutEvent()
		{
			EventBase<PointerCaptureOutEvent>.SetCreateFunction(() => new PointerCaptureOutEvent());
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0004755B File Offset: 0x0004575B
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateHoverPseudoStateAfterCaptureChange(base.pointerId);
		}
	}
}
