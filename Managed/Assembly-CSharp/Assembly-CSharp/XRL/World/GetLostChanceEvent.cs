using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000760 RID: 1888
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetLostChanceEvent : ITravelEvent
	{
		// Token: 0x06004DD6 RID: 19926 RVA: 0x0017E16E File Offset: 0x0017C36E
		public GetLostChanceEvent()
		{
			this.ID = GetLostChanceEvent.ID;
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x0017E181 File Offset: 0x0017C381
		public static int CountPool()
		{
			if (GetLostChanceEvent.Pool != null)
			{
				return GetLostChanceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x0017E196 File Offset: 0x0017C396
		public static void ResetPool()
		{
			while (GetLostChanceEvent.PoolCounter > 0)
			{
				GetLostChanceEvent.Pool[--GetLostChanceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0017E1BE File Offset: 0x0017C3BE
		public static void ResetTo(ref GetLostChanceEvent E)
		{
			MinEvent.ResetTo<GetLostChanceEvent>(E, GetLostChanceEvent.Pool, ref GetLostChanceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0017E1D4 File Offset: 0x0017C3D4
		public static GetLostChanceEvent FromPool()
		{
			return MinEvent.FromPool<GetLostChanceEvent>(ref GetLostChanceEvent.Pool, ref GetLostChanceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0017E1EA File Offset: 0x0017C3EA
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0017E1FE File Offset: 0x0017C3FE
		public override void Reset()
		{
			base.Reset();
			this.DefaultLimit = 0;
			this.OverrideDefaultLimit = false;
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0017E214 File Offset: 0x0017C414
		public static int GetFor(GameObject Actor, string TravelClass = null, int PercentageBonus = 0, int DefaultLimit = 95, bool OverrideDefaultLimit = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetLostChance"))
			{
				Event @event = Event.New("GetLostChance", 0, 0, 0);
				@event.SetParameter("Object", Actor);
				@event.SetParameter("TravelClass", TravelClass);
				@event.SetParameter("PercentageBonus", PercentageBonus);
				@event.SetFlag("OverrideDefaultLimit", OverrideDefaultLimit);
				flag = Actor.FireEvent(@event);
				PercentageBonus = @event.GetIntParameter("PercentageBonus", 0);
				OverrideDefaultLimit = @event.HasFlag("OverrideDefaultLimit");
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(GetLostChanceEvent.ID, ITravelEvent.CascadeLevel))
			{
				GetLostChanceEvent getLostChanceEvent = GetLostChanceEvent.FromPool();
				getLostChanceEvent.Actor = Actor;
				getLostChanceEvent.TravelClass = TravelClass;
				getLostChanceEvent.PercentageBonus = PercentageBonus;
				flag = Actor.HandleEvent(getLostChanceEvent);
				PercentageBonus = getLostChanceEvent.PercentageBonus;
				OverrideDefaultLimit = getLostChanceEvent.OverrideDefaultLimit;
			}
			if (!OverrideDefaultLimit && PercentageBonus > DefaultLimit)
			{
				PercentageBonus = DefaultLimit;
			}
			return PercentageBonus;
		}

		// Token: 0x040025D8 RID: 9688
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(GetLostChanceEvent), null, new MinEvent.EventPoolCount(GetLostChanceEvent.CountPool), new MinEvent.EventPoolReset(GetLostChanceEvent.ResetPool));

		// Token: 0x040025D9 RID: 9689
		private static List<GetLostChanceEvent> Pool;

		// Token: 0x040025DA RID: 9690
		private static int PoolCounter;

		// Token: 0x040025DB RID: 9691
		public int DefaultLimit;

		// Token: 0x040025DC RID: 9692
		public bool OverrideDefaultLimit;
	}
}
