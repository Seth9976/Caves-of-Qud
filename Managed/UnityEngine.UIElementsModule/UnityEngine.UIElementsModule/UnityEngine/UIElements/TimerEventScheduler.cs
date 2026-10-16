using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200033B RID: 827
	internal class TimerEventScheduler : IScheduler
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x0005C8E4 File Offset: 0x0005AAE4
		public void Schedule(ScheduledItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				bool flag2 = item == null;
				if (flag2)
				{
					throw new NotSupportedException("Scheduled Item type is not supported by this scheduler");
				}
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag3 = this.m_UnscheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item) || this.m_ScheduleTransactions.Contains(item);
						if (flag4)
						{
							throw new ArgumentException("Cannot schedule function " + item + " more than once");
						}
						this.m_ScheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = this.m_ScheduledItems.Contains(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot schedule function " + item + " more than once");
					}
					this.m_ScheduledItems.Add(item);
				}
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0005C9BC File Offset: 0x0005ABBC
		public ScheduledItem ScheduleOnce(Action<TimerState> timerUpdateEvent, long delayMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0005C9E8 File Offset: 0x0005ABE8
		public ScheduledItem ScheduleUntil(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, Func<bool> stopCondition)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = stopCondition
			};
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0005CA24 File Offset: 0x0005AC24
		public ScheduledItem ScheduleForDuration(Action<TimerState> timerUpdateEvent, long delayMs, long intervalMs, long durationMs)
		{
			TimerEventScheduler.TimerEventSchedulerItem timerEventSchedulerItem = new TimerEventScheduler.TimerEventSchedulerItem(timerUpdateEvent)
			{
				delayMs = delayMs,
				intervalMs = intervalMs,
				timerUpdateStopCondition = null
			};
			timerEventSchedulerItem.SetDuration(durationMs);
			this.Schedule(timerEventSchedulerItem);
			return timerEventSchedulerItem;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0005CA68 File Offset: 0x0005AC68
		private bool RemovedScheduledItemAt(int index)
		{
			bool flag = index >= 0;
			bool flag3;
			if (flag)
			{
				bool flag2 = index <= this.m_LastUpdatedIndex;
				if (flag2)
				{
					this.m_LastUpdatedIndex--;
				}
				this.m_ScheduledItems.RemoveAt(index);
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0005CAB8 File Offset: 0x0005ACB8
		public void Unschedule(ScheduledItem item)
		{
			bool flag = item != null;
			if (flag)
			{
				bool transactionMode = this.m_TransactionMode;
				if (transactionMode)
				{
					bool flag2 = this.m_UnscheduleTransactions.Contains(item);
					if (flag2)
					{
						throw new ArgumentException("Cannot unschedule scheduled function twice" + ((item != null) ? item.ToString() : null));
					}
					bool flag3 = this.m_ScheduleTransactions.Remove(item);
					if (!flag3)
					{
						bool flag4 = this.m_ScheduledItems.Contains(item);
						if (!flag4)
						{
							throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
						}
						this.m_UnscheduleTransactions.Add(item);
					}
				}
				else
				{
					bool flag5 = !this.PrivateUnSchedule(item);
					if (flag5)
					{
						throw new ArgumentException("Cannot unschedule unknown scheduled function " + ((item != null) ? item.ToString() : null));
					}
				}
				item.OnItemUnscheduled();
			}
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0005CBA4 File Offset: 0x0005ADA4
		private bool PrivateUnSchedule(ScheduledItem sItem)
		{
			return this.m_ScheduleTransactions.Remove(sItem) || this.RemovedScheduledItemAt(this.m_ScheduledItems.IndexOf(sItem));
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005CBDC File Offset: 0x0005ADDC
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
		public long FrameCount
		{
			get
			{
				return this.frameCount;
			}
			set
			{
				this.frameCount = value;
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0005CC00 File Offset: 0x0005AE00
		public void UpdateScheduledEvents()
		{
			bool flag = true;
			try
			{
				this.m_TransactionMode = true;
				long num = Panel.TimeSinceStartupMs();
				int count = this.m_ScheduledItems.Count;
				int num2 = this.m_LastUpdatedIndex + 1;
				bool flag2 = num2 >= count;
				if (flag2)
				{
					num2 = 0;
				}
				for (int i = 0; i < count; i++)
				{
					int num3 = num2 + i;
					bool flag3 = num3 >= count;
					if (flag3)
					{
						num3 -= count;
					}
					ScheduledItem scheduledItem = this.m_ScheduledItems[num3];
					bool flag4 = false;
					bool flag5 = num - scheduledItem.delayMs >= scheduledItem.startMs;
					if (flag5)
					{
						TimerState timerState = new TimerState
						{
							start = scheduledItem.startMs,
							now = num
						};
						bool flag6 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag6)
						{
							scheduledItem.PerformTimerUpdate(timerState);
						}
						scheduledItem.startMs = num;
						scheduledItem.delayMs = scheduledItem.intervalMs;
						bool flag7 = scheduledItem.ShouldUnschedule();
						if (flag7)
						{
							flag4 = true;
						}
					}
					bool flag8 = flag4 || (scheduledItem.endTimeMs > 0L && num > scheduledItem.endTimeMs);
					if (flag8)
					{
						bool flag9 = !this.m_UnscheduleTransactions.Contains(scheduledItem);
						if (flag9)
						{
							this.Unschedule(scheduledItem);
						}
					}
					this.m_LastUpdatedIndex = num3;
				}
			}
			finally
			{
				this.m_TransactionMode = false;
				foreach (ScheduledItem scheduledItem2 in this.m_UnscheduleTransactions)
				{
					this.PrivateUnSchedule(scheduledItem2);
				}
				this.m_UnscheduleTransactions.Clear();
				foreach (ScheduledItem scheduledItem3 in this.m_ScheduleTransactions)
				{
					this.Schedule(scheduledItem3);
				}
				this.m_ScheduleTransactions.Clear();
				bool flag10 = flag;
				if (flag10)
				{
					long num4 = this.FrameCount + 1L;
					this.FrameCount = num4;
				}
			}
		}

		// Token: 0x04000D1B RID: 3355
		private readonly List<ScheduledItem> m_ScheduledItems = new List<ScheduledItem>();

		// Token: 0x04000D1C RID: 3356
		private bool m_TransactionMode;

		// Token: 0x04000D1D RID: 3357
		private readonly List<ScheduledItem> m_ScheduleTransactions = new List<ScheduledItem>();

		// Token: 0x04000D1E RID: 3358
		private readonly HashSet<ScheduledItem> m_UnscheduleTransactions = new HashSet<ScheduledItem>();

		// Token: 0x04000D1F RID: 3359
		internal bool disableThrottling = false;

		// Token: 0x04000D20 RID: 3360
		private int m_LastUpdatedIndex = -1;

		// Token: 0x04000D21 RID: 3361
		private long frameCount;

		// Token: 0x0200033C RID: 828
		private class TimerEventSchedulerItem : ScheduledItem
		{
			// Token: 0x0600180C RID: 6156 RVA: 0x0005CEB0 File Offset: 0x0005B0B0
			public TimerEventSchedulerItem(Action<TimerState> updateEvent)
			{
				this.m_TimerUpdateEvent = updateEvent;
			}

			// Token: 0x0600180D RID: 6157 RVA: 0x0005CEC1 File Offset: 0x0005B0C1
			public override void PerformTimerUpdate(TimerState state)
			{
				Action<TimerState> timerUpdateEvent = this.m_TimerUpdateEvent;
				if (timerUpdateEvent != null)
				{
					timerUpdateEvent(state);
				}
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x0005CED8 File Offset: 0x0005B0D8
			public override string ToString()
			{
				return this.m_TimerUpdateEvent.ToString();
			}

			// Token: 0x04000D22 RID: 3362
			private readonly Action<TimerState> m_TimerUpdateEvent;
		}
	}
}
