using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000339 RID: 825
	internal abstract class ScheduledItem
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0005C7F8 File Offset: 0x0005A9F8
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x0005C800 File Offset: 0x0005AA00
		public long startMs { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0005C809 File Offset: 0x0005AA09
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0005C811 File Offset: 0x0005AA11
		public long delayMs { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0005C81A File Offset: 0x0005AA1A
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x0005C822 File Offset: 0x0005AA22
		public long intervalMs { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0005C82B File Offset: 0x0005AA2B
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0005C833 File Offset: 0x0005AA33
		public long endTimeMs { get; private set; }

		// Token: 0x060017F6 RID: 6134 RVA: 0x0005C83C File Offset: 0x0005AA3C
		public ScheduledItem()
		{
			this.ResetStartTime();
			this.timerUpdateStopCondition = ScheduledItem.OnceCondition;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0005C858 File Offset: 0x0005AA58
		protected void ResetStartTime()
		{
			this.startMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0005C867 File Offset: 0x0005AA67
		public void SetDuration(long durationMs)
		{
			this.endTimeMs = this.startMs + durationMs;
		}

		// Token: 0x060017F9 RID: 6137
		public abstract void PerformTimerUpdate(TimerState state);

		// Token: 0x060017FA RID: 6138 RVA: 0x00002152 File Offset: 0x00000352
		internal virtual void OnItemUnscheduled()
		{
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0005C87C File Offset: 0x0005AA7C
		public virtual bool ShouldUnschedule()
		{
			bool flag = this.timerUpdateStopCondition != null;
			return flag && this.timerUpdateStopCondition();
		}

		// Token: 0x04000D13 RID: 3347
		public Func<bool> timerUpdateStopCondition;

		// Token: 0x04000D14 RID: 3348
		public static readonly Func<bool> OnceCondition = () => true;

		// Token: 0x04000D15 RID: 3349
		public static readonly Func<bool> ForeverCondition = () => false;
	}
}
