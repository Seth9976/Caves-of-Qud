using System;

namespace XRL.World
{
	// Token: 0x0200074E RID: 1870
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetFixedMissileSpreadEvent : PooledEvent<GetFixedMissileSpreadEvent>
	{
		// Token: 0x06004D57 RID: 19799 RVA: 0x0017CB43 File Offset: 0x0017AD43
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0017CB4C File Offset: 0x0017AD4C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Spread = 0;
		}

		// Token: 0x06004D59 RID: 19801 RVA: 0x0017CB64 File Offset: 0x0017AD64
		public static bool GetFor(GameObject Object, out int Spread)
		{
			Spread = 0;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetFixedMissileSpread"))
			{
				Event @event = Event.New("GetFixedMissileSpread", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Spread", Spread);
				flag = Object.FireEvent(@event);
				Spread = @event.GetIntParameter("Spread", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetFixedMissileSpreadEvent>.ID, MinEvent.CascadeLevel))
			{
				GetFixedMissileSpreadEvent getFixedMissileSpreadEvent = PooledEvent<GetFixedMissileSpreadEvent>.FromPool();
				getFixedMissileSpreadEvent.Object = Object;
				getFixedMissileSpreadEvent.Spread = Spread;
				flag = Object.HandleEvent(getFixedMissileSpreadEvent);
				Spread = getFixedMissileSpreadEvent.Spread;
			}
			return !flag;
		}

		// Token: 0x0400258A RID: 9610
		public GameObject Object;

		// Token: 0x0400258B RID: 9611
		public int Spread;
	}
}
