using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200086B RID: 2155
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TookEvent : IActOnItemEvent
	{
		// Token: 0x060055B3 RID: 21939 RVA: 0x001993A4 File Offset: 0x001975A4
		public TookEvent()
		{
			this.ID = TookEvent.ID;
		}

		// Token: 0x060055B4 RID: 21940 RVA: 0x001993B7 File Offset: 0x001975B7
		public static int CountPool()
		{
			if (TookEvent.Pool != null)
			{
				return TookEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x001993CC File Offset: 0x001975CC
		public static void ResetPool()
		{
			while (TookEvent.PoolCounter > 0)
			{
				TookEvent.Pool[--TookEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055B6 RID: 21942 RVA: 0x001993F4 File Offset: 0x001975F4
		public static void ResetTo(ref TookEvent E)
		{
			MinEvent.ResetTo<TookEvent>(E, TookEvent.Pool, ref TookEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055B7 RID: 21943 RVA: 0x0019940A File Offset: 0x0019760A
		public static TookEvent FromPool()
		{
			return MinEvent.FromPool<TookEvent>(ref TookEvent.Pool, ref TookEvent.PoolCounter, 8192);
		}

		// Token: 0x060055B8 RID: 21944 RVA: 0x00199420 File Offset: 0x00197620
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x00199434 File Offset: 0x00197634
		public override void Reset()
		{
			base.Reset();
			this.Context = null;
		}

		// Token: 0x060055BA RID: 21946 RVA: 0x00199444 File Offset: 0x00197644
		public static void Send(GameObject Item, GameObject Actor, string Context = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("Took"))
			{
				Event @event = Event.New("Took", 0, 0, 0);
				@event.SetParameter("Item", Item);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Object", Item);
				@event.SetParameter("Context", Context);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(TookEvent.ID, MinEvent.CascadeLevel))
			{
				TookEvent tookEvent = TookEvent.FromPool();
				tookEvent.Item = Item;
				tookEvent.Actor = Actor;
				tookEvent.Context = Context;
				flag = Actor.HandleEvent(tookEvent);
			}
		}

		// Token: 0x04002B2D RID: 11053
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TookEvent), null, new MinEvent.EventPoolCount(TookEvent.CountPool), new MinEvent.EventPoolReset(TookEvent.ResetPool));

		// Token: 0x04002B2E RID: 11054
		private static List<TookEvent> Pool;

		// Token: 0x04002B2F RID: 11055
		private static int PoolCounter;

		// Token: 0x04002B30 RID: 11056
		public string Context;
	}
}
