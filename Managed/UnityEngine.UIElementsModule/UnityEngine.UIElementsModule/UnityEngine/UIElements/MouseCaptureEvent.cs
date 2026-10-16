using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021F RID: 543
	public class MouseCaptureEvent : MouseCaptureEventBase<MouseCaptureEvent>
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x00047667 File Offset: 0x00045867
		static MouseCaptureEvent()
		{
			EventBase<MouseCaptureEvent>.SetCreateFunction(() => new MouseCaptureEvent());
		}
	}
}
