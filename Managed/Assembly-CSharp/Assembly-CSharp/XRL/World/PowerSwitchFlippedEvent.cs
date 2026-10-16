using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000827 RID: 2087
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class PowerSwitchFlippedEvent : IActOnItemEvent
	{
		// Token: 0x06005307 RID: 21255 RVA: 0x0018E6F0 File Offset: 0x0018C8F0
		public PowerSwitchFlippedEvent()
		{
			this.ID = PowerSwitchFlippedEvent.ID;
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0018E703 File Offset: 0x0018C903
		public static int CountPool()
		{
			if (PowerSwitchFlippedEvent.Pool != null)
			{
				return PowerSwitchFlippedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0018E718 File Offset: 0x0018C918
		public static void ResetPool()
		{
			while (PowerSwitchFlippedEvent.PoolCounter > 0)
			{
				PowerSwitchFlippedEvent.Pool[--PowerSwitchFlippedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600530A RID: 21258 RVA: 0x0018E740 File Offset: 0x0018C940
		public static void ResetTo(ref PowerSwitchFlippedEvent E)
		{
			MinEvent.ResetTo<PowerSwitchFlippedEvent>(E, PowerSwitchFlippedEvent.Pool, ref PowerSwitchFlippedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600530B RID: 21259 RVA: 0x0018E756 File Offset: 0x0018C956
		public static PowerSwitchFlippedEvent FromPool()
		{
			return MinEvent.FromPool<PowerSwitchFlippedEvent>(ref PowerSwitchFlippedEvent.Pool, ref PowerSwitchFlippedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600530C RID: 21260 RVA: 0x0018E76C File Offset: 0x0018C96C
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x0018E780 File Offset: 0x0018C980
		public static void Send(GameObject Actor, GameObject Item)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Item) && Item.HasRegisteredEvent("PowerSwitchFlipped"))
			{
				Event @event = Event.New("PowerSwitchFlipped", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Item", Item);
				flag = Item.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Item) && Item.WantEvent(PowerSwitchFlippedEvent.ID, MinEvent.CascadeLevel))
			{
				PowerSwitchFlippedEvent powerSwitchFlippedEvent = PowerSwitchFlippedEvent.FromPool();
				powerSwitchFlippedEvent.Actor = Actor;
				powerSwitchFlippedEvent.Item = Item;
				flag = Item.HandleEvent(powerSwitchFlippedEvent);
			}
		}

		// Token: 0x040029C6 RID: 10694
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(PowerSwitchFlippedEvent), null, new MinEvent.EventPoolCount(PowerSwitchFlippedEvent.CountPool), new MinEvent.EventPoolReset(PowerSwitchFlippedEvent.ResetPool));

		// Token: 0x040029C7 RID: 10695
		private static List<PowerSwitchFlippedEvent> Pool;

		// Token: 0x040029C8 RID: 10696
		private static int PoolCounter;
	}
}
