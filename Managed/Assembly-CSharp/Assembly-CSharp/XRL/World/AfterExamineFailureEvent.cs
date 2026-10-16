using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200065F RID: 1631
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterExamineFailureEvent : IExamineEvent
	{
		// Token: 0x06004673 RID: 18035 RVA: 0x00166AE7 File Offset: 0x00164CE7
		public AfterExamineFailureEvent()
		{
			this.ID = AfterExamineFailureEvent.ID;
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00166AFA File Offset: 0x00164CFA
		public static int CountPool()
		{
			if (AfterExamineFailureEvent.Pool != null)
			{
				return AfterExamineFailureEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00166B0F File Offset: 0x00164D0F
		public static void ResetPool()
		{
			while (AfterExamineFailureEvent.PoolCounter > 0)
			{
				AfterExamineFailureEvent.Pool[--AfterExamineFailureEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00166B37 File Offset: 0x00164D37
		public static void ResetTo(ref AfterExamineFailureEvent E)
		{
			MinEvent.ResetTo<AfterExamineFailureEvent>(E, AfterExamineFailureEvent.Pool, ref AfterExamineFailureEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004677 RID: 18039 RVA: 0x00166B4D File Offset: 0x00164D4D
		public static AfterExamineFailureEvent FromPool()
		{
			return MinEvent.FromPool<AfterExamineFailureEvent>(ref AfterExamineFailureEvent.Pool, ref AfterExamineFailureEvent.PoolCounter, 8192);
		}

		// Token: 0x06004678 RID: 18040 RVA: 0x00166B63 File Offset: 0x00164D63
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x00166B78 File Offset: 0x00164D78
		public static void Send(GameObject Actor, GameObject Item, bool ConfusionBased = false)
		{
			bool flag = true;
			AfterExamineFailureEvent afterExamineFailureEvent = null;
			if (flag && (Actor.HasRegisteredEvent("AfterExamineFailure") || Item.HasRegisteredEvent("AfterExamineFailure")))
			{
				if (afterExamineFailureEvent == null)
				{
					afterExamineFailureEvent = AfterExamineFailureEvent.FromPool();
					afterExamineFailureEvent.Actor = Actor;
					afterExamineFailureEvent.Item = Item;
					afterExamineFailureEvent.Setup();
				}
				Event @event = Event.New("AfterExamineFailure", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Item", Item);
				@event.SetFlag("Identify", afterExamineFailureEvent.Identify);
				@event.SetFlag("IdentifyIfDestroyed", afterExamineFailureEvent.IdentifyIfDestroyed);
				@event.SetFlag("ConfusionBased", ConfusionBased);
				flag = Actor.FireEvent(@event) && Item.FireEvent(@event);
				afterExamineFailureEvent.Identify = @event.HasFlag("Identify");
				afterExamineFailureEvent.IdentifyIfDestroyed = @event.HasFlag("IdentifyIfDestroyed");
			}
			if (flag)
			{
				bool flag2 = Actor.WantEvent(AfterExamineFailureEvent.ID, MinEvent.CascadeLevel);
				bool flag3 = Item.WantEvent(AfterExamineFailureEvent.ID, MinEvent.CascadeLevel);
				if (flag2 || flag3)
				{
					if (afterExamineFailureEvent == null)
					{
						afterExamineFailureEvent = AfterExamineFailureEvent.FromPool();
						afterExamineFailureEvent.Actor = Actor;
						afterExamineFailureEvent.Item = Item;
						afterExamineFailureEvent.Setup();
					}
					afterExamineFailureEvent.ConfusionBased = ConfusionBased;
					bool flag4 = (!flag2 || Actor.HandleEvent(afterExamineFailureEvent)) && (!flag3 || Item.HandleEvent(afterExamineFailureEvent));
				}
			}
			if (afterExamineFailureEvent != null)
			{
				afterExamineFailureEvent.ProcessIdentify();
			}
		}

		// Token: 0x04002179 RID: 8569
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterExamineFailureEvent), null, new MinEvent.EventPoolCount(AfterExamineFailureEvent.CountPool), new MinEvent.EventPoolReset(AfterExamineFailureEvent.ResetPool));

		// Token: 0x0400217A RID: 8570
		private static List<AfterExamineFailureEvent> Pool;

		// Token: 0x0400217B RID: 8571
		private static int PoolCounter;
	}
}
