using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021D RID: 541
	public class MouseCaptureOutEvent : MouseCaptureEventBase<MouseCaptureOutEvent>
	{
		// Token: 0x06001031 RID: 4145 RVA: 0x00047615 File Offset: 0x00045815
		static MouseCaptureOutEvent()
		{
			EventBase<MouseCaptureOutEvent>.SetCreateFunction(() => new MouseCaptureOutEvent());
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004762E File Offset: 0x0004582E
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.elementTarget.UpdateCursorStyle(this.eventTypeId);
		}
	}
}
