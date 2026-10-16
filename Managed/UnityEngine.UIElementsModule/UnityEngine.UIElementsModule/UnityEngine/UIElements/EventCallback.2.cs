using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000231 RID: 561
	// (Invoke) Token: 0x060010BA RID: 4282
	public delegate void EventCallback<in TEventType, in TCallbackArgs>(TEventType evt, TCallbackArgs userArgs);
}
