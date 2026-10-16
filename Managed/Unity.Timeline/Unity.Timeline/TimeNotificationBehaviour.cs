using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003E RID: 62
	public class TimeNotificationBehaviour : PlayableBehaviour
	{
		// Token: 0x170000B9 RID: 185
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00009982 File Offset: 0x00007B82
		public Playable timeSource
		{
			set
			{
				this.m_TimeSource = value;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000998B File Offset: 0x00007B8B
		public static ScriptPlayable<TimeNotificationBehaviour> Create(PlayableGraph graph, double duration, DirectorWrapMode loopMode)
		{
			ScriptPlayable<TimeNotificationBehaviour> scriptPlayable = ScriptPlayable<TimeNotificationBehaviour>.Create(graph, 0);
			scriptPlayable.SetDuration(duration);
			scriptPlayable.SetTimeWrapMode(loopMode);
			scriptPlayable.SetPropagateSetTime(true);
			return scriptPlayable;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000099AC File Offset: 0x00007BAC
		public void AddNotification(double time, INotification payload, NotificationFlags flags = NotificationFlags.Retroactive)
		{
			this.m_Notifications.Add(new TimeNotificationBehaviour.NotificationEntry
			{
				time = time,
				payload = payload,
				flags = flags
			});
			this.m_NeedSortNotifications = true;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000099EC File Offset: 0x00007BEC
		public override void OnGraphStart(Playable playable)
		{
			this.SortNotifications();
			double time = playable.GetTime<Playable>();
			for (int i = 0; i < this.m_Notifications.Count; i++)
			{
				if (this.m_Notifications[i].time > time && !this.m_Notifications[i].triggerOnce)
				{
					TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[i];
					notificationEntry.notificationFired = false;
					this.m_Notifications[i] = notificationEntry;
				}
			}
			this.m_PreviousTime = playable.GetTime<Playable>();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009A74 File Offset: 0x00007C74
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (playable.IsDone<Playable>())
			{
				this.SortNotifications();
				for (int i = 0; i < this.m_Notifications.Count; i++)
				{
					TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[i];
					if (!notificationEntry.notificationFired)
					{
						double duration = playable.GetDuration<Playable>();
						if (this.m_PreviousTime <= notificationEntry.time && notificationEntry.time <= duration)
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref notificationEntry);
							this.m_Notifications[i] = notificationEntry;
						}
					}
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009AFC File Offset: 0x00007CFC
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (info.evaluationType == FrameData.EvaluationType.Evaluate)
			{
				return;
			}
			this.SyncDurationWithExternalSource(playable);
			this.SortNotifications();
			double time = playable.GetTime<Playable>();
			if (info.timeLooped)
			{
				double duration = playable.GetDuration<Playable>();
				this.TriggerNotificationsInRange(this.m_PreviousTime, duration, info, playable, true);
				double num = playable.GetDuration<Playable>() - this.m_PreviousTime;
				int num2 = (int)(((double)(info.deltaTime * info.effectiveSpeed) - num) / playable.GetDuration<Playable>());
				for (int i = 0; i < num2; i++)
				{
					this.TriggerNotificationsInRange(0.0, duration, info, playable, false);
				}
				this.TriggerNotificationsInRange(0.0, time, info, playable, false);
			}
			else
			{
				double time2 = playable.GetTime<Playable>();
				this.TriggerNotificationsInRange(this.m_PreviousTime, time2, info, playable, true);
			}
			for (int j = 0; j < this.m_Notifications.Count; j++)
			{
				TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[j];
				if (notificationEntry.notificationFired && TimeNotificationBehaviour.CanRestoreNotification(notificationEntry, info, time, this.m_PreviousTime))
				{
					TimeNotificationBehaviour.Restore_internal(ref notificationEntry);
					this.m_Notifications[j] = notificationEntry;
				}
			}
			this.m_PreviousTime = playable.GetTime<Playable>();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009C26 File Offset: 0x00007E26
		private void SortNotifications()
		{
			if (this.m_NeedSortNotifications)
			{
				this.m_Notifications.Sort((TimeNotificationBehaviour.NotificationEntry x, TimeNotificationBehaviour.NotificationEntry y) => x.time.CompareTo(y.time));
				this.m_NeedSortNotifications = false;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009C61 File Offset: 0x00007E61
		private static bool CanRestoreNotification(TimeNotificationBehaviour.NotificationEntry e, FrameData info, double currentTime, double previousTime)
		{
			return !e.triggerOnce && (info.timeLooped || (previousTime > currentTime && currentTime <= e.time));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009C8C File Offset: 0x00007E8C
		private void TriggerNotificationsInRange(double start, double end, FrameData info, Playable playable, bool checkState)
		{
			if (start <= end)
			{
				bool isPlaying = Application.isPlaying;
				for (int i = 0; i < this.m_Notifications.Count; i++)
				{
					TimeNotificationBehaviour.NotificationEntry notificationEntry = this.m_Notifications[i];
					if (!notificationEntry.notificationFired || (!checkState && !notificationEntry.triggerOnce))
					{
						double time = notificationEntry.time;
						if (notificationEntry.prewarm && time < end && (notificationEntry.triggerInEditor || isPlaying))
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref notificationEntry);
							this.m_Notifications[i] = notificationEntry;
						}
						else if (time >= start && time <= end && (notificationEntry.triggerInEditor || isPlaying))
						{
							TimeNotificationBehaviour.Trigger_internal(playable, info.output, ref notificationEntry);
							this.m_Notifications[i] = notificationEntry;
						}
					}
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009D52 File Offset: 0x00007F52
		private void SyncDurationWithExternalSource(Playable playable)
		{
			if (this.m_TimeSource.IsValid<Playable>())
			{
				playable.SetDuration(this.m_TimeSource.GetDuration<Playable>());
				playable.SetTimeWrapMode(this.m_TimeSource.GetTimeWrapMode<Playable>());
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009D83 File Offset: 0x00007F83
		private static void Trigger_internal(Playable playable, PlayableOutput output, ref TimeNotificationBehaviour.NotificationEntry e)
		{
			output.PushNotification(playable, e.payload, null);
			e.notificationFired = true;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00009D9A File Offset: 0x00007F9A
		private static void Restore_internal(ref TimeNotificationBehaviour.NotificationEntry e)
		{
			e.notificationFired = false;
		}

		// Token: 0x040000EB RID: 235
		private readonly List<TimeNotificationBehaviour.NotificationEntry> m_Notifications = new List<TimeNotificationBehaviour.NotificationEntry>();

		// Token: 0x040000EC RID: 236
		private double m_PreviousTime;

		// Token: 0x040000ED RID: 237
		private bool m_NeedSortNotifications;

		// Token: 0x040000EE RID: 238
		private Playable m_TimeSource;

		// Token: 0x0200007C RID: 124
		private struct NotificationEntry
		{
			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000384 RID: 900 RVA: 0x0000C518 File Offset: 0x0000A718
			public bool triggerInEditor
			{
				get
				{
					return (this.flags & NotificationFlags.TriggerInEditMode) > (NotificationFlags)0;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000385 RID: 901 RVA: 0x0000C525 File Offset: 0x0000A725
			public bool prewarm
			{
				get
				{
					return (this.flags & NotificationFlags.Retroactive) > (NotificationFlags)0;
				}
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000386 RID: 902 RVA: 0x0000C532 File Offset: 0x0000A732
			public bool triggerOnce
			{
				get
				{
					return (this.flags & NotificationFlags.TriggerOnce) > (NotificationFlags)0;
				}
			}

			// Token: 0x0400018B RID: 395
			public double time;

			// Token: 0x0400018C RID: 396
			public INotification payload;

			// Token: 0x0400018D RID: 397
			public bool notificationFired;

			// Token: 0x0400018E RID: 398
			public NotificationFlags flags;
		}
	}
}
