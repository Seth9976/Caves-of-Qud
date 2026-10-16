using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AD RID: 685
	public sealed class TransitionStartEvent : TransitionEventBase<TransitionStartEvent>
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0004F1BD File Offset: 0x0004D3BD
		static TransitionStartEvent()
		{
			EventBase<TransitionStartEvent>.SetCreateFunction(() => new TransitionStartEvent());
		}
	}
}
