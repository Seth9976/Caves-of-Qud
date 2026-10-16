using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200065E RID: 1630
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterExamineCriticalFailureEvent : IExamineEvent
	{
		// Token: 0x0600466B RID: 18027 RVA: 0x001668E4 File Offset: 0x00164AE4
		public AfterExamineCriticalFailureEvent()
		{
			this.ID = AfterExamineCriticalFailureEvent.ID;
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x001668F7 File Offset: 0x00164AF7
		public static int CountPool()
		{
			if (AfterExamineCriticalFailureEvent.Pool != null)
			{
				return AfterExamineCriticalFailureEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x0016690C File Offset: 0x00164B0C
		public static void ResetPool()
		{
			while (AfterExamineCriticalFailureEvent.PoolCounter > 0)
			{
				AfterExamineCriticalFailureEvent.Pool[--AfterExamineCriticalFailureEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600466E RID: 18030 RVA: 0x00166934 File Offset: 0x00164B34
		public static void ResetTo(ref AfterExamineCriticalFailureEvent E)
		{
			MinEvent.ResetTo<AfterExamineCriticalFailureEvent>(E, AfterExamineCriticalFailureEvent.Pool, ref AfterExamineCriticalFailureEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600466F RID: 18031 RVA: 0x0016694A File Offset: 0x00164B4A
		public static AfterExamineCriticalFailureEvent FromPool()
		{
			return MinEvent.FromPool<AfterExamineCriticalFailureEvent>(ref AfterExamineCriticalFailureEvent.Pool, ref AfterExamineCriticalFailureEvent.PoolCounter, 8192);
		}

		// Token: 0x06004670 RID: 18032 RVA: 0x00166960 File Offset: 0x00164B60
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x00166974 File Offset: 0x00164B74
		public static void Send(GameObject Actor, GameObject Item)
		{
			bool flag = true;
			AfterExamineCriticalFailureEvent afterExamineCriticalFailureEvent = null;
			if (flag && (Actor.HasRegisteredEvent("AfterExamineCriticalFailure") || Item.HasRegisteredEvent("AfterExamineCriticalFailure")))
			{
				if (afterExamineCriticalFailureEvent == null)
				{
					afterExamineCriticalFailureEvent = AfterExamineCriticalFailureEvent.FromPool();
					afterExamineCriticalFailureEvent.Actor = Actor;
					afterExamineCriticalFailureEvent.Item = Item;
					afterExamineCriticalFailureEvent.Setup();
				}
				Event @event = Event.New("AfterExamineCriticalFailure", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Item", Item);
				@event.SetFlag("Identify", afterExamineCriticalFailureEvent.Identify);
				@event.SetFlag("IdentifyIfDestroyed", afterExamineCriticalFailureEvent.IdentifyIfDestroyed);
				flag = Actor.FireEvent(@event) && Item.FireEvent(@event);
				afterExamineCriticalFailureEvent.Identify = @event.HasFlag("Identify");
				afterExamineCriticalFailureEvent.IdentifyIfDestroyed = @event.HasFlag("IdentifyIfDestroyed");
			}
			if (flag)
			{
				bool flag2 = Actor.WantEvent(AfterExamineCriticalFailureEvent.ID, MinEvent.CascadeLevel);
				bool flag3 = Item.WantEvent(AfterExamineCriticalFailureEvent.ID, MinEvent.CascadeLevel);
				if (flag2 || flag3)
				{
					if (afterExamineCriticalFailureEvent == null)
					{
						afterExamineCriticalFailureEvent = AfterExamineCriticalFailureEvent.FromPool();
						afterExamineCriticalFailureEvent.Actor = Actor;
						afterExamineCriticalFailureEvent.Item = Item;
						afterExamineCriticalFailureEvent.Setup();
					}
					bool flag4 = (!flag2 || Actor.HandleEvent(afterExamineCriticalFailureEvent)) && (!flag3 || Item.HandleEvent(afterExamineCriticalFailureEvent));
				}
			}
			if (afterExamineCriticalFailureEvent != null)
			{
				afterExamineCriticalFailureEvent.ProcessIdentify();
			}
		}

		// Token: 0x04002176 RID: 8566
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterExamineCriticalFailureEvent), null, new MinEvent.EventPoolCount(AfterExamineCriticalFailureEvent.CountPool), new MinEvent.EventPoolReset(AfterExamineCriticalFailureEvent.ResetPool));

		// Token: 0x04002177 RID: 8567
		private static List<AfterExamineCriticalFailureEvent> Pool;

		// Token: 0x04002178 RID: 8568
		private static int PoolCounter;
	}
}
