using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020007A5 RID: 1957
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetWaterRitualReputationAmountEvent : PooledEvent<GetWaterRitualReputationAmountEvent>
	{
		// Token: 0x06004FBD RID: 20413 RVA: 0x00185169 File Offset: 0x00183369
		public override int GetCascadeLevel()
		{
			return GetWaterRitualReputationAmountEvent.CascadeLevel;
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x00185170 File Offset: 0x00183370
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00185179 File Offset: 0x00183379
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.SpeakingWith = null;
			this.Record = null;
			this.Faction = null;
			this.BaseAmount = 0;
			this.Amount = 0;
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x001851AC File Offset: 0x001833AC
		public static int GetFor(GameObject Actor, GameObject SpeakingWith, WaterRitualRecord Record, string Faction, int BaseAmount)
		{
			int num = BaseAmount;
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetWaterRitualReputationAmount"))
			{
				Event @event = Event.New("GetWaterRitualReputationAmount", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("SpeakingWith", SpeakingWith);
				@event.SetParameter("Record", Record);
				@event.SetParameter("Faction", Faction);
				@event.SetParameter("BaseAmount", BaseAmount);
				@event.SetParameter("Amount", num);
				flag = Actor.FireEvent(@event);
				num = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetWaterRitualReputationAmountEvent>.ID, GetWaterRitualReputationAmountEvent.CascadeLevel))
			{
				GetWaterRitualReputationAmountEvent getWaterRitualReputationAmountEvent = PooledEvent<GetWaterRitualReputationAmountEvent>.FromPool();
				getWaterRitualReputationAmountEvent.Actor = Actor;
				getWaterRitualReputationAmountEvent.SpeakingWith = SpeakingWith;
				getWaterRitualReputationAmountEvent.Record = Record;
				getWaterRitualReputationAmountEvent.Faction = Faction;
				getWaterRitualReputationAmountEvent.BaseAmount = BaseAmount;
				getWaterRitualReputationAmountEvent.Amount = num;
				flag = Actor.HandleEvent(getWaterRitualReputationAmountEvent);
				num = getWaterRitualReputationAmountEvent.Amount;
			}
			return num;
		}

		// Token: 0x04002768 RID: 10088
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002769 RID: 10089
		public GameObject Actor;

		// Token: 0x0400276A RID: 10090
		public GameObject SpeakingWith;

		// Token: 0x0400276B RID: 10091
		public WaterRitualRecord Record;

		// Token: 0x0400276C RID: 10092
		public string Faction;

		// Token: 0x0400276D RID: 10093
		public int BaseAmount;

		// Token: 0x0400276E RID: 10094
		public int Amount;
	}
}
