using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027B RID: 635
	public class NavigationSubmitEvent : NavigationEventBase<NavigationSubmitEvent>
	{
		// Token: 0x0600127A RID: 4730 RVA: 0x0004C75B File Offset: 0x0004A95B
		static NavigationSubmitEvent()
		{
			EventBase<NavigationSubmitEvent>.SetCreateFunction(() => new NavigationSubmitEvent());
		}
	}
}
