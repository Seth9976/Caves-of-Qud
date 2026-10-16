using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000674 RID: 1652
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AllowInventoryStackEvent : IActOnItemEvent
	{
		// Token: 0x06004705 RID: 18181 RVA: 0x00168367 File Offset: 0x00166567
		public AllowInventoryStackEvent()
		{
			this.ID = AllowInventoryStackEvent.ID;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x0016837A File Offset: 0x0016657A
		public static int CountPool()
		{
			if (AllowInventoryStackEvent.Pool != null)
			{
				return AllowInventoryStackEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x0016838F File Offset: 0x0016658F
		public static void ResetPool()
		{
			while (AllowInventoryStackEvent.PoolCounter > 0)
			{
				AllowInventoryStackEvent.Pool[--AllowInventoryStackEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x001683B7 File Offset: 0x001665B7
		public static void ResetTo(ref AllowInventoryStackEvent E)
		{
			MinEvent.ResetTo<AllowInventoryStackEvent>(E, AllowInventoryStackEvent.Pool, ref AllowInventoryStackEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x001683CD File Offset: 0x001665CD
		public static AllowInventoryStackEvent FromPool()
		{
			return MinEvent.FromPool<AllowInventoryStackEvent>(ref AllowInventoryStackEvent.Pool, ref AllowInventoryStackEvent.PoolCounter, 8192);
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x001683E3 File Offset: 0x001665E3
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x001683F7 File Offset: 0x001665F7
		public static AllowInventoryStackEvent FromPool(GameObject Actor, GameObject Item)
		{
			AllowInventoryStackEvent allowInventoryStackEvent = AllowInventoryStackEvent.FromPool();
			allowInventoryStackEvent.Actor = Actor;
			allowInventoryStackEvent.Item = Item;
			return allowInventoryStackEvent;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x0016840C File Offset: 0x0016660C
		public static bool Check(GameObject Actor, GameObject Item)
		{
			if (GameObject.Validate(ref Actor) && GameObject.Validate(ref Item) && Actor.HasRegisteredEvent("AllowInventoryStack"))
			{
				Event @event = Event.New("AllowInventoryStack", 0, 0, 0);
				@event.SetParameter("Object", Item);
				if (!Actor.FireEvent(@event))
				{
					return false;
				}
			}
			return !GameObject.Validate(ref Actor) || !GameObject.Validate(ref Item) || !Actor.WantEvent(AllowInventoryStackEvent.ID, MinEvent.CascadeLevel) || Actor.HandleEvent(AllowInventoryStackEvent.FromPool(Actor, Item));
		}

		// Token: 0x040021D8 RID: 8664
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(AllowInventoryStackEvent), null, new MinEvent.EventPoolCount(AllowInventoryStackEvent.CountPool), new MinEvent.EventPoolReset(AllowInventoryStackEvent.ResetPool));

		// Token: 0x040021D9 RID: 8665
		private static List<AllowInventoryStackEvent> Pool;

		// Token: 0x040021DA RID: 8666
		private static int PoolCounter;
	}
}
