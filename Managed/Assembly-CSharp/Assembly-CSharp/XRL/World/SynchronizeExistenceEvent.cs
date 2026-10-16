using System;

namespace XRL.World
{
	// Token: 0x02000864 RID: 2148
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.None)]
	public class SynchronizeExistenceEvent : MinEvent
	{
		// Token: 0x06005581 RID: 21889 RVA: 0x00198B19 File Offset: 0x00196D19
		public SynchronizeExistenceEvent()
		{
			this.ID = SynchronizeExistenceEvent.ID;
		}

		// Token: 0x06005582 RID: 21890 RVA: 0x00198B2C File Offset: 0x00196D2C
		public override int GetCascadeLevel()
		{
			return SynchronizeExistenceEvent.CascadeLevel;
		}

		// Token: 0x06005583 RID: 21891 RVA: 0x00198B33 File Offset: 0x00196D33
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x04002B18 RID: 11032
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(SynchronizeExistenceEvent), null, null, null);

		// Token: 0x04002B19 RID: 11033
		public new static readonly int CascadeLevel = 15;
	}
}
