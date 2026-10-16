using System;

namespace XRL.World
{
	// Token: 0x020007BD RID: 1981
	[GameEvent(Base = true)]
	public abstract class IBootSequenceEvent : MinEvent
	{
		// Token: 0x06005046 RID: 20550 RVA: 0x0018640A File Offset: 0x0018460A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x00186413 File Offset: 0x00184613
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x040027C1 RID: 10177
		public GameObject Object;
	}
}
