using System;

namespace XRL.World
{
	// Token: 0x0200085C RID: 2140
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Singleton)]
	public class StartTradeEvent : SingletonEvent<StartTradeEvent>
	{
		// Token: 0x06005557 RID: 21847 RVA: 0x001985EA File Offset: 0x001967EA
		public override int GetCascadeLevel()
		{
			return StartTradeEvent.CascadeLevel;
		}

		// Token: 0x06005558 RID: 21848 RVA: 0x001985F1 File Offset: 0x001967F1
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x001985FA File Offset: 0x001967FA
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Trader = null;
			this.IdentifyLevel = 0;
			this.Companion = false;
			this.Identify = false;
			this.Repair = false;
			this.Recharge = false;
			this.Read = false;
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x0019863C File Offset: 0x0019683C
		public static void Send(GameObject Actor, GameObject Trader, int IdentifyLevel = 0, bool Companion = false, bool Identify = false, bool Repair = false, bool Recharge = false, bool Read = false)
		{
			SingletonEvent<StartTradeEvent>.Instance.Actor = Actor;
			SingletonEvent<StartTradeEvent>.Instance.Trader = Trader;
			SingletonEvent<StartTradeEvent>.Instance.IdentifyLevel = IdentifyLevel;
			SingletonEvent<StartTradeEvent>.Instance.Companion = Companion;
			SingletonEvent<StartTradeEvent>.Instance.Identify = Identify;
			SingletonEvent<StartTradeEvent>.Instance.Repair = Repair;
			SingletonEvent<StartTradeEvent>.Instance.Recharge = Recharge;
			SingletonEvent<StartTradeEvent>.Instance.Read = Read;
			if (Actor.HandleEvent(SingletonEvent<StartTradeEvent>.Instance))
			{
				Trader.HandleEvent(SingletonEvent<StartTradeEvent>.Instance);
			}
			SingletonEvent<StartTradeEvent>.Instance.Reset();
		}

		// Token: 0x04002AF8 RID: 11000
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002AF9 RID: 11001
		public GameObject Actor;

		// Token: 0x04002AFA RID: 11002
		public GameObject Trader;

		// Token: 0x04002AFB RID: 11003
		public int IdentifyLevel;

		// Token: 0x04002AFC RID: 11004
		public bool Companion;

		// Token: 0x04002AFD RID: 11005
		public bool Identify;

		// Token: 0x04002AFE RID: 11006
		public bool Repair;

		// Token: 0x04002AFF RID: 11007
		public bool Recharge;

		// Token: 0x04002B00 RID: 11008
		public bool Read;
	}
}
