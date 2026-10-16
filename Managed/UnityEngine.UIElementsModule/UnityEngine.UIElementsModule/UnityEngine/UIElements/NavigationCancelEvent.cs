using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000279 RID: 633
	public class NavigationCancelEvent : NavigationEventBase<NavigationCancelEvent>
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x0004C726 File Offset: 0x0004A926
		static NavigationCancelEvent()
		{
			EventBase<NavigationCancelEvent>.SetCreateFunction(() => new NavigationCancelEvent());
		}
	}
}
