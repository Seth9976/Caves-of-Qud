using System;

namespace XRL.World
{
	// Token: 0x02000733 RID: 1843
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetCleaveMaxPenaltyEvent : PooledEvent<GetCleaveMaxPenaltyEvent>
	{
		// Token: 0x06004C5E RID: 19550 RVA: 0x001799A1 File Offset: 0x00177BA1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C5F RID: 19551 RVA: 0x001799AA File Offset: 0x00177BAA
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Target = null;
			this.BaseAmount = 0;
			this.Amount = 0;
			this.IsChargingStrike = false;
		}

		// Token: 0x06004C60 RID: 19552 RVA: 0x001799DC File Offset: 0x00177BDC
		public static int GetFor(GameObject Object, GameObject Actor, GameObject Target, int BaseAmount = 1, bool IsChargingStrike = false)
		{
			bool flag = true;
			int num = BaseAmount;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetCleaveMaxPenalty"))
			{
				Event @event = Event.New("GetCleaveMaxPenalty", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Target", Target);
				@event.SetParameter("BaseAmount", BaseAmount);
				@event.SetParameter("Amount", num);
				@event.SetParameter("IsChargingStrike", IsChargingStrike);
				flag = Object.FireEvent(@event);
				num = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetCleaveMaxPenaltyEvent>.ID, MinEvent.CascadeLevel))
			{
				GetCleaveMaxPenaltyEvent getCleaveMaxPenaltyEvent = PooledEvent<GetCleaveMaxPenaltyEvent>.FromPool();
				getCleaveMaxPenaltyEvent.Object = Object;
				getCleaveMaxPenaltyEvent.Actor = Actor;
				getCleaveMaxPenaltyEvent.Target = Target;
				getCleaveMaxPenaltyEvent.BaseAmount = BaseAmount;
				getCleaveMaxPenaltyEvent.Amount = num;
				getCleaveMaxPenaltyEvent.IsChargingStrike = IsChargingStrike;
				flag = Object.HandleEvent(getCleaveMaxPenaltyEvent);
				num = getCleaveMaxPenaltyEvent.Amount;
			}
			return num;
		}

		// Token: 0x040024F5 RID: 9461
		public GameObject Object;

		// Token: 0x040024F6 RID: 9462
		public GameObject Actor;

		// Token: 0x040024F7 RID: 9463
		public GameObject Target;

		// Token: 0x040024F8 RID: 9464
		public int BaseAmount;

		// Token: 0x040024F9 RID: 9465
		public int Amount;

		// Token: 0x040024FA RID: 9466
		public bool IsChargingStrike;
	}
}
