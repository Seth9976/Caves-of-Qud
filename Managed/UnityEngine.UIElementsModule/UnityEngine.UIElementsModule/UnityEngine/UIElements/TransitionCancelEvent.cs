using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B1 RID: 689
	public sealed class TransitionCancelEvent : TransitionEventBase<TransitionCancelEvent>
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0004F227 File Offset: 0x0004D427
		static TransitionCancelEvent()
		{
			EventBase<TransitionCancelEvent>.SetCreateFunction(() => new TransitionCancelEvent());
		}
	}
}
