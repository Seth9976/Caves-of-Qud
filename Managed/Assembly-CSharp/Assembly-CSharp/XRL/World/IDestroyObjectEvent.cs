using System;

namespace XRL.World
{
	// Token: 0x020007C7 RID: 1991
	[GameEvent(Base = true)]
	public abstract class IDestroyObjectEvent : MinEvent
	{
		// Token: 0x06005076 RID: 20598 RVA: 0x00186B04 File Offset: 0x00184D04
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x00186B0D File Offset: 0x00184D0D
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Obliterate = false;
			this.Silent = false;
			this.Reason = null;
			this.ThirdPersonReason = null;
		}

		// Token: 0x040027F9 RID: 10233
		public GameObject Object;

		// Token: 0x040027FA RID: 10234
		public bool Obliterate;

		// Token: 0x040027FB RID: 10235
		public bool Silent;

		// Token: 0x040027FC RID: 10236
		public string Reason;

		// Token: 0x040027FD RID: 10237
		public string ThirdPersonReason;
	}
}
