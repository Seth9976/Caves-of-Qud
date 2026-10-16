using System;

namespace XRL.World
{
	// Token: 0x020007A7 RID: 1959
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetWaterRitualSellSecretBehaviorEvent : PooledEvent<GetWaterRitualSellSecretBehaviorEvent>
	{
		// Token: 0x06004FC6 RID: 20422 RVA: 0x00185322 File Offset: 0x00183522
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x0018532B File Offset: 0x0018352B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.SpeakingWith = null;
			this.Message = null;
			this.ReputationProvided = 0;
			this.BonusReputationProvided = 0;
			this.IsSecret = false;
			this.IsGossip = false;
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x00185364 File Offset: 0x00183564
		public static void Send(GameObject Actor, GameObject SpeakingWith, ref string Message, ref int ReputationProvided, ref int BonusReputationProvided, bool IsSecret = false, bool IsGossip = false)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetWaterRitualSellSecretBehavior"))
			{
				Event @event = Event.New("GetWaterRitualSellSecretBehavior", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("SpeakingWith", SpeakingWith);
				@event.SetParameter("Message", Message);
				@event.SetParameter("ReputationProvided", ReputationProvided);
				@event.SetParameter("BonusReputationProvided", BonusReputationProvided);
				@event.SetFlag("IsSecret", IsSecret);
				@event.SetFlag("IsGossip", IsGossip);
				flag = Actor.FireEvent(@event);
				Message = @event.GetStringParameter("Message", null);
				ReputationProvided = @event.GetIntParameter("ReputationProvided", 0);
				BonusReputationProvided = @event.GetIntParameter("BonusReputationProvided", 0);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetWaterRitualSellSecretBehaviorEvent>.ID, MinEvent.CascadeLevel))
			{
				GetWaterRitualSellSecretBehaviorEvent getWaterRitualSellSecretBehaviorEvent = PooledEvent<GetWaterRitualSellSecretBehaviorEvent>.FromPool();
				getWaterRitualSellSecretBehaviorEvent.Actor = Actor;
				getWaterRitualSellSecretBehaviorEvent.SpeakingWith = SpeakingWith;
				getWaterRitualSellSecretBehaviorEvent.Message = Message;
				getWaterRitualSellSecretBehaviorEvent.ReputationProvided = ReputationProvided;
				getWaterRitualSellSecretBehaviorEvent.BonusReputationProvided = BonusReputationProvided;
				getWaterRitualSellSecretBehaviorEvent.IsSecret = IsSecret;
				getWaterRitualSellSecretBehaviorEvent.IsGossip = IsGossip;
				flag = Actor.HandleEvent(getWaterRitualSellSecretBehaviorEvent);
				Message = getWaterRitualSellSecretBehaviorEvent.Message;
				ReputationProvided = getWaterRitualSellSecretBehaviorEvent.ReputationProvided;
				BonusReputationProvided = getWaterRitualSellSecretBehaviorEvent.BonusReputationProvided;
			}
		}

		// Token: 0x04002778 RID: 10104
		public GameObject Actor;

		// Token: 0x04002779 RID: 10105
		public GameObject SpeakingWith;

		// Token: 0x0400277A RID: 10106
		public string Message;

		// Token: 0x0400277B RID: 10107
		public int ReputationProvided;

		// Token: 0x0400277C RID: 10108
		public int BonusReputationProvided;

		// Token: 0x0400277D RID: 10109
		public bool IsSecret;

		// Token: 0x0400277E RID: 10110
		public bool IsGossip;
	}
}
