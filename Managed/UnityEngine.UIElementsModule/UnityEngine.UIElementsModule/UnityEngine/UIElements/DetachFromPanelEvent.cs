using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000281 RID: 641
	public class DetachFromPanelEvent : PanelChangedEventBase<DetachFromPanelEvent>
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x0004C851 File Offset: 0x0004AA51
		static DetachFromPanelEvent()
		{
			EventBase<DetachFromPanelEvent>.SetCreateFunction(() => new DetachFromPanelEvent());
		}
	}
}
