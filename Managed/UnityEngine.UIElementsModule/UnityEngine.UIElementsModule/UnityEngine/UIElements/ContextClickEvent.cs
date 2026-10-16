using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025F RID: 607
	public class ContextClickEvent : MouseEventBase<ContextClickEvent>
	{
		// Token: 0x060011F9 RID: 4601 RVA: 0x0004B737 File Offset: 0x00049937
		static ContextClickEvent()
		{
			EventBase<ContextClickEvent>.SetCreateFunction(() => new ContextClickEvent());
		}
	}
}
