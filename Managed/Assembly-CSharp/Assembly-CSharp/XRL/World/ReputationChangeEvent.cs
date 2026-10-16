using System;

namespace XRL.World
{
	// Token: 0x02000849 RID: 2121
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class ReputationChangeEvent : PooledEvent<ReputationChangeEvent>
	{
		// Token: 0x06005426 RID: 21542 RVA: 0x001914A2 File Offset: 0x0018F6A2
		public override int GetCascadeLevel()
		{
			return ReputationChangeEvent.CascadeLevel;
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x001914A9 File Offset: 0x0018F6A9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005428 RID: 21544 RVA: 0x001914B2 File Offset: 0x0018F6B2
		public override void Reset()
		{
			base.Reset();
			this.Faction = null;
			this.BaseAmount = 0;
			this.Amount = 0;
			this.Type = null;
			this.Silent = false;
			this.Transient = false;
			this.Prospective = false;
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x001914EC File Offset: 0x0018F6EC
		public static int GetFor(Faction Faction, int BaseAmount, string Type = null, bool Silent = false, bool Transient = false, bool Prospective = false)
		{
			int num = BaseAmount;
			bool flag = true;
			if (The.Player != null)
			{
				if (flag && The.Player.HasRegisteredEvent("ReputationChange"))
				{
					Event @event = Event.New("ReputationChange", 0, 0, 0);
					@event.SetParameter("Faction", Faction);
					@event.SetParameter("BaseAmount", BaseAmount);
					@event.SetParameter("Amount", num);
					@event.SetParameter("Type", Type);
					@event.SetParameter("Silent", Silent);
					@event.SetParameter("Transient", Transient);
					@event.SetParameter("Prospective", Prospective);
					flag = The.Player.FireEvent(@event);
					num = @event.GetIntParameter("Amount", 0);
				}
				if (flag)
				{
					bool flag2 = The.Game.WantEvent(PooledEvent<ReputationChangeEvent>.ID, ReputationChangeEvent.CascadeLevel);
					bool flag3 = The.Player.WantEvent(PooledEvent<ReputationChangeEvent>.ID, ReputationChangeEvent.CascadeLevel);
					if (flag2 || flag3)
					{
						ReputationChangeEvent reputationChangeEvent = PooledEvent<ReputationChangeEvent>.FromPool();
						reputationChangeEvent.Faction = Faction;
						reputationChangeEvent.BaseAmount = BaseAmount;
						reputationChangeEvent.Amount = num;
						reputationChangeEvent.Type = Type;
						reputationChangeEvent.Silent = Silent;
						reputationChangeEvent.Transient = Transient;
						reputationChangeEvent.Prospective = Prospective;
						if (flag2)
						{
							flag = The.Game.HandleEvent(reputationChangeEvent);
							num = reputationChangeEvent.Amount;
						}
						if (flag && flag3)
						{
							flag = The.Player.HandleEvent(reputationChangeEvent);
							num = reputationChangeEvent.Amount;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x04002A48 RID: 10824
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002A49 RID: 10825
		public Faction Faction;

		// Token: 0x04002A4A RID: 10826
		public int BaseAmount;

		// Token: 0x04002A4B RID: 10827
		public int Amount;

		// Token: 0x04002A4C RID: 10828
		public string Type;

		// Token: 0x04002A4D RID: 10829
		public bool Silent;

		// Token: 0x04002A4E RID: 10830
		public bool Transient;

		// Token: 0x04002A4F RID: 10831
		public bool Prospective;
	}
}
