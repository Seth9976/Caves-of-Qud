using System;

namespace XRL.World
{
	// Token: 0x020007C0 RID: 1984
	[GameEvent(Base = true)]
	public abstract class IChargeProductionEvent : IChargeEvent
	{
		// Token: 0x06005056 RID: 20566 RVA: 0x00186704 File Offset: 0x00184904
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}
	}
}
