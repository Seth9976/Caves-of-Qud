using System;

namespace XRL.World
{
	// Token: 0x020007CA RID: 1994
	[GameEvent(Base = true)]
	public abstract class IFinalChargeProductionEvent : IChargeProductionEvent
	{
		// Token: 0x06005083 RID: 20611 RVA: 0x00186E91 File Offset: 0x00185091
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}
	}
}
