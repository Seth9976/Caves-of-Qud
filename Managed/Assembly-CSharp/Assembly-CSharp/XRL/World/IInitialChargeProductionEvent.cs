using System;

namespace XRL.World
{
	// Token: 0x020007CC RID: 1996
	[GameEvent(Base = true)]
	public abstract class IInitialChargeProductionEvent : IChargeProductionEvent
	{
		// Token: 0x0600508B RID: 20619 RVA: 0x00187028 File Offset: 0x00185228
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}
	}
}
