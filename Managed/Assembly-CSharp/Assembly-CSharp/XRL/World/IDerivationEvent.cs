using System;

namespace XRL.World
{
	// Token: 0x020007C6 RID: 1990
	[GameEvent(Base = true)]
	public abstract class IDerivationEvent : MinEvent
	{
		// Token: 0x06005073 RID: 20595 RVA: 0x00186AD6 File Offset: 0x00184CD6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x00186ADF File Offset: 0x00184CDF
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.Context = null;
		}

		// Token: 0x040027F6 RID: 10230
		public GameObject Object;

		// Token: 0x040027F7 RID: 10231
		public GameObject Actor;

		// Token: 0x040027F8 RID: 10232
		public string Context;
	}
}
