using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027F RID: 639
	public class AttachToPanelEvent : PanelChangedEventBase<AttachToPanelEvent>
	{
		// Token: 0x06001287 RID: 4743 RVA: 0x0004C81C File Offset: 0x0004AA1C
		static AttachToPanelEvent()
		{
			EventBase<AttachToPanelEvent>.SetCreateFunction(() => new AttachToPanelEvent());
		}
	}
}
