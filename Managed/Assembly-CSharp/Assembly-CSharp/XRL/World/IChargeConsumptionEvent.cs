using System;

namespace XRL.World
{
	// Token: 0x020007BE RID: 1982
	[GameEvent(Base = true)]
	public abstract class IChargeConsumptionEvent : IChargeEvent
	{
		// Token: 0x06005049 RID: 20553 RVA: 0x0018642A File Offset: 0x0018462A
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}
	}
}
