using System;
using Qud.API;
using XRL.World.Conversations;

namespace XRL.World
{
	// Token: 0x020007A6 RID: 1958
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetWaterRitualSecretWeightEvent : PooledEvent<GetWaterRitualSecretWeightEvent>
	{
		// Token: 0x06004FC3 RID: 20419 RVA: 0x001852BC File Offset: 0x001834BC
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x001852C8 File Offset: 0x001834C8
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Speaker = null;
			this.Secret = null;
			this.BaseWeight = 0;
			this.Weight = 0;
			this.Source = null;
			this.Context = null;
			this.Buy = false;
			this.Sell = false;
		}

		// Token: 0x0400276F RID: 10095
		public GameObject Actor;

		// Token: 0x04002770 RID: 10096
		public GameObject Speaker;

		// Token: 0x04002771 RID: 10097
		public IBaseJournalEntry Secret;

		// Token: 0x04002772 RID: 10098
		public int BaseWeight;

		// Token: 0x04002773 RID: 10099
		public int Weight;

		// Token: 0x04002774 RID: 10100
		public IConversationPart Source;

		// Token: 0x04002775 RID: 10101
		public string Context;

		// Token: 0x04002776 RID: 10102
		public bool Buy;

		// Token: 0x04002777 RID: 10103
		public bool Sell;
	}
}
