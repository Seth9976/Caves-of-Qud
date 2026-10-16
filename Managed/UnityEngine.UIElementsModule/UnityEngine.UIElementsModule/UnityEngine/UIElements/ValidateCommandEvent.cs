using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000226 RID: 550
	public class ValidateCommandEvent : CommandEventBase<ValidateCommandEvent>
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x0004784A File Offset: 0x00045A4A
		static ValidateCommandEvent()
		{
			EventBase<ValidateCommandEvent>.SetCreateFunction(() => new ValidateCommandEvent());
		}
	}
}
