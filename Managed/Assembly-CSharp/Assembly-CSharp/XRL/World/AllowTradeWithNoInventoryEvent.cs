using System;

namespace XRL.World
{
	// Token: 0x02000677 RID: 1655
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AllowTradeWithNoInventoryEvent : PooledEvent<AllowTradeWithNoInventoryEvent>
	{
		// Token: 0x06004722 RID: 18210 RVA: 0x0016873E File Offset: 0x0016693E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x00168747 File Offset: 0x00166947
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Trader = null;
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x00168760 File Offset: 0x00166960
		public static bool Check(GameObject Actor, GameObject Trader)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Trader) && Trader.HasRegisteredEvent("AllowTradeWithNoInventory"))
			{
				Event @event = Event.New("AllowTradeWithNoInventory", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Trader", Trader);
				flag = Trader.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Trader) && Trader.WantEvent(PooledEvent<AllowTradeWithNoInventoryEvent>.ID, MinEvent.CascadeLevel))
			{
				AllowTradeWithNoInventoryEvent allowTradeWithNoInventoryEvent = PooledEvent<AllowTradeWithNoInventoryEvent>.FromPool();
				allowTradeWithNoInventoryEvent.Actor = Actor;
				allowTradeWithNoInventoryEvent.Trader = Trader;
				flag = Trader.HandleEvent(allowTradeWithNoInventoryEvent);
			}
			return !flag;
		}

		// Token: 0x040021E6 RID: 8678
		public GameObject Actor;

		// Token: 0x040021E7 RID: 8679
		public GameObject Trader;
	}
}
