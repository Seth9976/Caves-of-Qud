using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AF RID: 687
	public sealed class TransitionEndEvent : TransitionEventBase<TransitionEndEvent>
	{
		// Token: 0x060013A5 RID: 5029 RVA: 0x0004F1F2 File Offset: 0x0004D3F2
		static TransitionEndEvent()
		{
			EventBase<TransitionEndEvent>.SetCreateFunction(() => new TransitionEndEvent());
		}
	}
}
