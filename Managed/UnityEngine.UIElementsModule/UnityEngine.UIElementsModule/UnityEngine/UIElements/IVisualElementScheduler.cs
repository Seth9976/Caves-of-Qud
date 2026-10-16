using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005C8 RID: 1480
	public interface IVisualElementScheduler
	{
		// Token: 0x06002BB6 RID: 11190
		IVisualElementScheduledItem Execute(Action<TimerState> timerUpdateEvent);

		// Token: 0x06002BB7 RID: 11191
		IVisualElementScheduledItem Execute(Action updateEvent);
	}
}
