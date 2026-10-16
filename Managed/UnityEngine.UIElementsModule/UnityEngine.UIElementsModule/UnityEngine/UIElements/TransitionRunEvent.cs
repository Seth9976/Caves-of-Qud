using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AB RID: 683
	public sealed class TransitionRunEvent : TransitionEventBase<TransitionRunEvent>
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x0004F188 File Offset: 0x0004D388
		static TransitionRunEvent()
		{
			EventBase<TransitionRunEvent>.SetCreateFunction(() => new TransitionRunEvent());
		}
	}
}
