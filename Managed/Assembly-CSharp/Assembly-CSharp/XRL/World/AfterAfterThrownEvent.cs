using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000655 RID: 1621
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AfterAfterThrownEvent : IActOnItemEvent
	{
		// Token: 0x06004629 RID: 17961 RVA: 0x00165CC8 File Offset: 0x00163EC8
		public AfterAfterThrownEvent()
		{
			this.ID = AfterAfterThrownEvent.ID;
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x00165CDB File Offset: 0x00163EDB
		public static int CountPool()
		{
			if (AfterAfterThrownEvent.Pool != null)
			{
				return AfterAfterThrownEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x00165CF0 File Offset: 0x00163EF0
		public static void ResetPool()
		{
			while (AfterAfterThrownEvent.PoolCounter > 0)
			{
				AfterAfterThrownEvent.Pool[--AfterAfterThrownEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x00165D18 File Offset: 0x00163F18
		public static void ResetTo(ref AfterAfterThrownEvent E)
		{
			MinEvent.ResetTo<AfterAfterThrownEvent>(E, AfterAfterThrownEvent.Pool, ref AfterAfterThrownEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x00165D2E File Offset: 0x00163F2E
		public static AfterAfterThrownEvent FromPool()
		{
			return MinEvent.FromPool<AfterAfterThrownEvent>(ref AfterAfterThrownEvent.Pool, ref AfterAfterThrownEvent.PoolCounter, 8192);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x00165D44 File Offset: 0x00163F44
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x00165D58 File Offset: 0x00163F58
		public override void Reset()
		{
			base.Reset();
			this.ApparentTarget = null;
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x00165D67 File Offset: 0x00163F67
		public static AfterAfterThrownEvent FromPool(GameObject Actor, GameObject Item, GameObject ApparentTarget)
		{
			AfterAfterThrownEvent afterAfterThrownEvent = AfterAfterThrownEvent.FromPool();
			afterAfterThrownEvent.Actor = Actor;
			afterAfterThrownEvent.Item = Item;
			afterAfterThrownEvent.ApparentTarget = ApparentTarget;
			return afterAfterThrownEvent;
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x00165D84 File Offset: 0x00163F84
		public static bool Check(GameObject Actor, GameObject Item, GameObject ApparentTarget)
		{
			if (GameObject.Validate(ref Item) && Item.HasRegisteredEvent("AfterAfterThrown"))
			{
				Event @event = Event.New("AfterAfterThrown", 0, 0, 0);
				@event.SetParameter("Owner", Actor);
				@event.SetParameter("Object", Item);
				@event.SetParameter("ApparentTarget", ApparentTarget);
				if (!Item.FireEvent(@event))
				{
					return false;
				}
			}
			if (GameObject.Validate(ref Item) && Item.WantEvent(AfterAfterThrownEvent.ID, MinEvent.CascadeLevel))
			{
				AfterAfterThrownEvent afterAfterThrownEvent = AfterAfterThrownEvent.FromPool(Actor, Item, ApparentTarget);
				if (!Item.HandleEvent(afterAfterThrownEvent))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400214D RID: 8525
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AfterAfterThrownEvent), null, new MinEvent.EventPoolCount(AfterAfterThrownEvent.CountPool), new MinEvent.EventPoolReset(AfterAfterThrownEvent.ResetPool));

		// Token: 0x0400214E RID: 8526
		private static List<AfterAfterThrownEvent> Pool;

		// Token: 0x0400214F RID: 8527
		private static int PoolCounter;

		// Token: 0x04002150 RID: 8528
		public GameObject ApparentTarget;
	}
}
