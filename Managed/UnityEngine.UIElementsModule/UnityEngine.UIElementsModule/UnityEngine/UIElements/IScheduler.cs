using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000338 RID: 824
	internal interface IScheduler
	{
		// Token: 0x060017E7 RID: 6119
		ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs);

		// Token: 0x060017E8 RID: 6120
		ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition = null);

		// Token: 0x060017E9 RID: 6121
		ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs);

		// Token: 0x060017EA RID: 6122
		void Unschedule(ScheduledItem item);

		// Token: 0x060017EB RID: 6123
		void Schedule(ScheduledItem item);

		// Token: 0x060017EC RID: 6124
		void UpdateScheduledEvents();

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060017ED RID: 6125
		long FrameCount { get; }
	}
}
