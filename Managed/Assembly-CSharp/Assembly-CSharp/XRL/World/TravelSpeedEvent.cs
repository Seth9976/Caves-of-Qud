using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200086D RID: 2157
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class TravelSpeedEvent : ITravelEvent
	{
		// Token: 0x060055C0 RID: 21952 RVA: 0x0019958A File Offset: 0x0019778A
		public TravelSpeedEvent()
		{
			this.ID = TravelSpeedEvent.ID;
		}

		// Token: 0x060055C1 RID: 21953 RVA: 0x0019959D File Offset: 0x0019779D
		public static int CountPool()
		{
			if (TravelSpeedEvent.Pool != null)
			{
				return TravelSpeedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060055C2 RID: 21954 RVA: 0x001995B2 File Offset: 0x001977B2
		public static void ResetPool()
		{
			while (TravelSpeedEvent.PoolCounter > 0)
			{
				TravelSpeedEvent.Pool[--TravelSpeedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060055C3 RID: 21955 RVA: 0x001995DA File Offset: 0x001977DA
		public static void ResetTo(ref TravelSpeedEvent E)
		{
			MinEvent.ResetTo<TravelSpeedEvent>(E, TravelSpeedEvent.Pool, ref TravelSpeedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060055C4 RID: 21956 RVA: 0x001995F0 File Offset: 0x001977F0
		public static TravelSpeedEvent FromPool()
		{
			return MinEvent.FromPool<TravelSpeedEvent>(ref TravelSpeedEvent.Pool, ref TravelSpeedEvent.PoolCounter, 8192);
		}

		// Token: 0x060055C5 RID: 21957 RVA: 0x00199606 File Offset: 0x00197806
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060055C6 RID: 21958 RVA: 0x0019961C File Offset: 0x0019781C
		public static int GetFor(GameObject Actor, string TravelClass = null, int PercentageBonus = 0)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("TravelSpeed"))
			{
				Event @event = Event.New("TravelSpeed", 0, 0, 0);
				@event.SetParameter("Object", Actor);
				@event.SetParameter("TravelClass", TravelClass);
				@event.SetParameter("PercentageBonus", PercentageBonus);
				flag = Actor.FireEvent(@event);
				PercentageBonus = @event.GetIntParameter("PercentageBonus", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(TravelSpeedEvent.ID, ITravelEvent.CascadeLevel))
			{
				TravelSpeedEvent travelSpeedEvent = TravelSpeedEvent.FromPool();
				travelSpeedEvent.Actor = Actor;
				travelSpeedEvent.TravelClass = TravelClass;
				travelSpeedEvent.PercentageBonus = PercentageBonus;
				flag = Actor.HandleEvent(travelSpeedEvent);
				PercentageBonus = travelSpeedEvent.PercentageBonus;
			}
			return PercentageBonus;
		}

		// Token: 0x04002B32 RID: 11058
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(TravelSpeedEvent), null, new MinEvent.EventPoolCount(TravelSpeedEvent.CountPool), new MinEvent.EventPoolReset(TravelSpeedEvent.ResetPool));

		// Token: 0x04002B33 RID: 11059
		private static List<TravelSpeedEvent> Pool;

		// Token: 0x04002B34 RID: 11060
		private static int PoolCounter;
	}
}
