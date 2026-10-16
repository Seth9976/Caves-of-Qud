using System;

namespace XRL.World
{
	// Token: 0x020007BA RID: 1978
	[GameEvent(Base = true)]
	public abstract class IActualEffectCheckEvent : IEffectCheckEvent
	{
		// Token: 0x06005039 RID: 20537 RVA: 0x00186234 File Offset: 0x00184434
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00186248 File Offset: 0x00184448
		public override void Reset()
		{
			base.Reset();
			this.Effect = null;
			this.Actor = null;
		}

		// Token: 0x040027B6 RID: 10166
		public Effect Effect;

		// Token: 0x040027B7 RID: 10167
		public GameObject Actor;
	}
}
