using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000252 RID: 594
	public class KeyUpEvent : KeyboardEventBase<KeyUpEvent>
	{
		// Token: 0x0600118F RID: 4495 RVA: 0x0004AD48 File Offset: 0x00048F48
		static KeyUpEvent()
		{
			EventBase<KeyUpEvent>.SetCreateFunction(() => new KeyUpEvent());
		}
	}
}
