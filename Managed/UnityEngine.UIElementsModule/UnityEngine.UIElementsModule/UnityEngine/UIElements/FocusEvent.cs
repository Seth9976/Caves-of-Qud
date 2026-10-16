using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000247 RID: 583
	public class FocusEvent : FocusEventBase<FocusEvent>
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x0004A675 File Offset: 0x00048875
		static FocusEvent()
		{
			EventBase<FocusEvent>.SetCreateFunction(() => new FocusEvent());
		}
	}
}
