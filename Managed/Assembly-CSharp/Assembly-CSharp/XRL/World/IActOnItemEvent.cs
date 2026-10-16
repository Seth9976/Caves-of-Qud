using System;

namespace XRL.World
{
	// Token: 0x020007B9 RID: 1977
	[GameEvent(Base = true)]
	public abstract class IActOnItemEvent : MinEvent
	{
		// Token: 0x06005036 RID: 20534 RVA: 0x0018620D File Offset: 0x0018440D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x00186216 File Offset: 0x00184416
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Item = null;
		}

		// Token: 0x040027B4 RID: 10164
		public GameObject Actor;

		// Token: 0x040027B5 RID: 10165
		public GameObject Item;
	}
}
