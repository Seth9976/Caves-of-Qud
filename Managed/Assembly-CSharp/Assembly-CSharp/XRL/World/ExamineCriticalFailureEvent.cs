using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200070E RID: 1806
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ExamineCriticalFailureEvent : IExamineEvent
	{
		// Token: 0x06004B56 RID: 19286 RVA: 0x00176791 File Offset: 0x00174991
		public ExamineCriticalFailureEvent()
		{
			this.ID = ExamineCriticalFailureEvent.ID;
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x001767A4 File Offset: 0x001749A4
		public static int CountPool()
		{
			if (ExamineCriticalFailureEvent.Pool != null)
			{
				return ExamineCriticalFailureEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x001767B9 File Offset: 0x001749B9
		public static void ResetPool()
		{
			while (ExamineCriticalFailureEvent.PoolCounter > 0)
			{
				ExamineCriticalFailureEvent.Pool[--ExamineCriticalFailureEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x001767E1 File Offset: 0x001749E1
		public static void ResetTo(ref ExamineCriticalFailureEvent E)
		{
			MinEvent.ResetTo<ExamineCriticalFailureEvent>(E, ExamineCriticalFailureEvent.Pool, ref ExamineCriticalFailureEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x001767F7 File Offset: 0x001749F7
		public static ExamineCriticalFailureEvent FromPool()
		{
			return MinEvent.FromPool<ExamineCriticalFailureEvent>(ref ExamineCriticalFailureEvent.Pool, ref ExamineCriticalFailureEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x0017680D File Offset: 0x00174A0D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x00176824 File Offset: 0x00174A24
		public static bool Check(GameObject Actor, GameObject Item)
		{
			bool flag = true;
			ExamineCriticalFailureEvent examineCriticalFailureEvent = null;
			Event @event = null;
			bool flag2 = Actor.HasRegisteredEvent("ExamineCriticalFailure");
			bool flag3 = Item.HasRegisteredEvent("ExamineCriticalFailure");
			bool flag4 = Actor.WantEvent(ExamineCriticalFailureEvent.ID, MinEvent.CascadeLevel);
			bool flag5 = Item.WantEvent(ExamineCriticalFailureEvent.ID, MinEvent.CascadeLevel);
			for (int i = 1; i <= ExamineCriticalFailureEvent.PASSES; i++)
			{
				if (flag && (flag2 || flag3))
				{
					if (examineCriticalFailureEvent == null)
					{
						examineCriticalFailureEvent = ExamineCriticalFailureEvent.FromPool();
						examineCriticalFailureEvent.Actor = Actor;
						examineCriticalFailureEvent.Item = Item;
						examineCriticalFailureEvent.Setup();
					}
					if (@event == null)
					{
						@event = Event.New("ExamineCriticalFailure", 0, 0, 0);
					}
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Item", Item);
					@event.SetParameter("Pass", i);
					@event.SetFlag("Identify", examineCriticalFailureEvent.Identify);
					@event.SetFlag("IdentifyIfDestroyed", examineCriticalFailureEvent.IdentifyIfDestroyed);
					flag = Actor.FireEvent(@event) && Item.FireEvent(@event);
					examineCriticalFailureEvent.Identify = @event.HasFlag("Identify");
					examineCriticalFailureEvent.IdentifyIfDestroyed = @event.HasFlag("IdentifyIfDestroyed");
				}
				if (flag && (flag4 || flag5))
				{
					if (examineCriticalFailureEvent == null)
					{
						examineCriticalFailureEvent = ExamineCriticalFailureEvent.FromPool();
						examineCriticalFailureEvent.Actor = Actor;
						examineCriticalFailureEvent.Item = Item;
						examineCriticalFailureEvent.Setup();
					}
					examineCriticalFailureEvent.Pass = i;
					flag = (!flag4 || Actor.HandleEvent(examineCriticalFailureEvent)) && (!flag5 || Item.HandleEvent(examineCriticalFailureEvent));
				}
			}
			if (examineCriticalFailureEvent != null)
			{
				examineCriticalFailureEvent.ProcessIdentify();
			}
			return flag;
		}

		// Token: 0x04002459 RID: 9305
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ExamineCriticalFailureEvent), null, new MinEvent.EventPoolCount(ExamineCriticalFailureEvent.CountPool), new MinEvent.EventPoolReset(ExamineCriticalFailureEvent.ResetPool));

		// Token: 0x0400245A RID: 9306
		private static List<ExamineCriticalFailureEvent> Pool;

		// Token: 0x0400245B RID: 9307
		private static int PoolCounter;

		// Token: 0x0400245C RID: 9308
		public static readonly int PASSES = 2;
	}
}
