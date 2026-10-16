using System;

namespace XRL.World
{
	// Token: 0x02000772 RID: 1906
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetOverloadChargeEvent : PooledEvent<GetOverloadChargeEvent>
	{
		// Token: 0x06004E64 RID: 20068 RVA: 0x001804F5 File Offset: 0x0017E6F5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x001804FE File Offset: 0x0017E6FE
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Amount = 0;
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x00180514 File Offset: 0x0017E714
		public static int GetFor(GameObject Object, int Amount)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetOverloadCharge"))
			{
				Event @event = Event.New("GetOverloadCharge", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Amount", Amount);
				flag = Object.FireEvent(@event);
				Amount = @event.GetIntParameter("Amount", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetOverloadChargeEvent>.ID, MinEvent.CascadeLevel))
			{
				GetOverloadChargeEvent getOverloadChargeEvent = PooledEvent<GetOverloadChargeEvent>.FromPool();
				getOverloadChargeEvent.Object = Object;
				getOverloadChargeEvent.Amount = Amount;
				flag = Object.HandleEvent(getOverloadChargeEvent);
				Amount = getOverloadChargeEvent.Amount;
			}
			return Amount;
		}

		// Token: 0x04002645 RID: 9797
		public GameObject Object;

		// Token: 0x04002646 RID: 9798
		public int Amount;
	}
}
