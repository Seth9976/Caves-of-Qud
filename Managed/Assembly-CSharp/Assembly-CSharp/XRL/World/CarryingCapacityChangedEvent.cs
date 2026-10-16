using System;

namespace XRL.World
{
	// Token: 0x020006D1 RID: 1745
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CarryingCapacityChangedEvent : PooledEvent<CarryingCapacityChangedEvent>
	{
		// Token: 0x060049B1 RID: 18865 RVA: 0x00172591 File Offset: 0x00170791
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0017259A File Offset: 0x0017079A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x001725A9 File Offset: 0x001707A9
		public static CarryingCapacityChangedEvent FromPool(GameObject Object)
		{
			CarryingCapacityChangedEvent carryingCapacityChangedEvent = PooledEvent<CarryingCapacityChangedEvent>.FromPool();
			carryingCapacityChangedEvent.Object = Object;
			return carryingCapacityChangedEvent;
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x001725B7 File Offset: 0x001707B7
		public static void Send(GameObject Object)
		{
			if (Object.WantEvent(PooledEvent<CarryingCapacityChangedEvent>.ID, MinEvent.CascadeLevel))
			{
				Object.HandleEvent(CarryingCapacityChangedEvent.FromPool(Object));
			}
		}

		// Token: 0x0400236C RID: 9068
		public GameObject Object;
	}
}
