using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000243 RID: 579
	public class BlurEvent : FocusEventBase<BlurEvent>
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x0004A5D5 File Offset: 0x000487D5
		static BlurEvent()
		{
			EventBase<BlurEvent>.SetCreateFunction(() => new BlurEvent());
		}
	}
}
