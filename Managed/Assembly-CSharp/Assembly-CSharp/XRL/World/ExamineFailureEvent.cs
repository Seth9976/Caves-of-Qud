using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200070F RID: 1807
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ExamineFailureEvent : IExamineEvent
	{
		// Token: 0x06004B5E RID: 19294 RVA: 0x001769DB File Offset: 0x00174BDB
		public ExamineFailureEvent()
		{
			this.ID = ExamineFailureEvent.ID;
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x001769EE File Offset: 0x00174BEE
		public static int CountPool()
		{
			if (ExamineFailureEvent.Pool != null)
			{
				return ExamineFailureEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00176A03 File Offset: 0x00174C03
		public static void ResetPool()
		{
			while (ExamineFailureEvent.PoolCounter > 0)
			{
				ExamineFailureEvent.Pool[--ExamineFailureEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B61 RID: 19297 RVA: 0x00176A2B File Offset: 0x00174C2B
		public static void ResetTo(ref ExamineFailureEvent E)
		{
			MinEvent.ResetTo<ExamineFailureEvent>(E, ExamineFailureEvent.Pool, ref ExamineFailureEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00176A41 File Offset: 0x00174C41
		public static ExamineFailureEvent FromPool()
		{
			return MinEvent.FromPool<ExamineFailureEvent>(ref ExamineFailureEvent.Pool, ref ExamineFailureEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00176A57 File Offset: 0x00174C57
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B64 RID: 19300 RVA: 0x00176A6C File Offset: 0x00174C6C
		public static bool Check(GameObject Actor, GameObject Item, bool ConfusionBased = false)
		{
			bool flag = true;
			ExamineFailureEvent examineFailureEvent = null;
			Event @event = null;
			Actor.HasRegisteredEvent("ExamineFailure");
			Item.HasRegisteredEvent("ExamineFailure");
			bool flag2 = Actor.WantEvent(ExamineFailureEvent.ID, MinEvent.CascadeLevel);
			bool flag3 = Item.WantEvent(ExamineFailureEvent.ID, MinEvent.CascadeLevel);
			for (int i = 1; i <= ExamineFailureEvent.PASSES; i++)
			{
				if (flag)
				{
					if (examineFailureEvent == null)
					{
						examineFailureEvent = ExamineFailureEvent.FromPool();
						examineFailureEvent.Actor = Actor;
						examineFailureEvent.Item = Item;
						examineFailureEvent.Setup();
					}
					if (@event == null)
					{
						@event = Event.New("ExamineFailure", 0, 0, 0);
					}
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Item", Item);
					@event.SetFlag("Identify", examineFailureEvent.Identify);
					@event.SetFlag("IdentifyIfDestroyed", examineFailureEvent.IdentifyIfDestroyed);
					@event.SetFlag("ConfusionBased", ConfusionBased);
					flag = Actor.FireEvent(@event) && Item.FireEvent(@event);
					examineFailureEvent.Identify = @event.HasFlag("Identify");
					examineFailureEvent.IdentifyIfDestroyed = @event.HasFlag("IdentifyIfDestroyed");
				}
				if (flag && (flag2 || flag3))
				{
					if (examineFailureEvent == null)
					{
						examineFailureEvent = ExamineFailureEvent.FromPool();
						examineFailureEvent.Actor = Actor;
						examineFailureEvent.Item = Item;
						examineFailureEvent.Setup();
					}
					examineFailureEvent.ConfusionBased = ConfusionBased;
					flag = (!flag2 || Actor.HandleEvent(examineFailureEvent)) && (!flag3 || Item.HandleEvent(examineFailureEvent));
				}
			}
			if (examineFailureEvent != null)
			{
				examineFailureEvent.ProcessIdentify();
			}
			return flag;
		}

		// Token: 0x0400245D RID: 9309
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ExamineFailureEvent), null, new MinEvent.EventPoolCount(ExamineFailureEvent.CountPool), new MinEvent.EventPoolReset(ExamineFailureEvent.ResetPool));

		// Token: 0x0400245E RID: 9310
		private static List<ExamineFailureEvent> Pool;

		// Token: 0x0400245F RID: 9311
		private static int PoolCounter;

		// Token: 0x04002460 RID: 9312
		public static readonly int PASSES = 2;
	}
}
