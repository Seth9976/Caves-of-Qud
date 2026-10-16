using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000703 RID: 1795
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class EncounterChanceEvent : ITravelEvent
	{
		// Token: 0x06004B01 RID: 19201 RVA: 0x00175C14 File Offset: 0x00173E14
		public EncounterChanceEvent()
		{
			this.ID = EncounterChanceEvent.ID;
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x00175C27 File Offset: 0x00173E27
		public static int CountPool()
		{
			if (EncounterChanceEvent.Pool != null)
			{
				return EncounterChanceEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x00175C3C File Offset: 0x00173E3C
		public static void ResetPool()
		{
			while (EncounterChanceEvent.PoolCounter > 0)
			{
				EncounterChanceEvent.Pool[--EncounterChanceEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00175C64 File Offset: 0x00173E64
		public static void ResetTo(ref EncounterChanceEvent E)
		{
			MinEvent.ResetTo<EncounterChanceEvent>(E, EncounterChanceEvent.Pool, ref EncounterChanceEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00175C7A File Offset: 0x00173E7A
		public static EncounterChanceEvent FromPool()
		{
			return MinEvent.FromPool<EncounterChanceEvent>(ref EncounterChanceEvent.Pool, ref EncounterChanceEvent.PoolCounter, 8192);
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00175C90 File Offset: 0x00173E90
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00175CA4 File Offset: 0x00173EA4
		public override void Reset()
		{
			base.Reset();
			this.Encounter = null;
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00175CB4 File Offset: 0x00173EB4
		public static int GetFor(GameObject Actor, string TravelClass = null, int PercentageBonus = 0, EncounterEntry Encounter = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("EncounterChance"))
			{
				Event @event = Event.New("EncounterChance", 0, 0, 0);
				@event.SetParameter("Object", Actor);
				@event.SetParameter("TravelClass", TravelClass);
				@event.SetParameter("PercentageBonus", PercentageBonus);
				@event.SetParameter("Encounter", Encounter);
				flag = Actor.FireEvent(@event);
				PercentageBonus = @event.GetIntParameter("PercentageBonus", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(EncounterChanceEvent.ID, ITravelEvent.CascadeLevel))
			{
				EncounterChanceEvent encounterChanceEvent = EncounterChanceEvent.FromPool();
				encounterChanceEvent.Actor = Actor;
				encounterChanceEvent.TravelClass = TravelClass;
				encounterChanceEvent.PercentageBonus = PercentageBonus;
				encounterChanceEvent.Encounter = Encounter;
				flag = Actor.HandleEvent(encounterChanceEvent);
				PercentageBonus = encounterChanceEvent.PercentageBonus;
			}
			return PercentageBonus;
		}

		// Token: 0x04002436 RID: 9270
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(EncounterChanceEvent), null, new MinEvent.EventPoolCount(EncounterChanceEvent.CountPool), new MinEvent.EventPoolReset(EncounterChanceEvent.ResetPool));

		// Token: 0x04002437 RID: 9271
		private static List<EncounterChanceEvent> Pool;

		// Token: 0x04002438 RID: 9272
		private static int PoolCounter;

		// Token: 0x04002439 RID: 9273
		public EncounterEntry Encounter;
	}
}
