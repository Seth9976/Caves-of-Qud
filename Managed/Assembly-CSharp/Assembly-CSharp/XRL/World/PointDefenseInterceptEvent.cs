using System;

namespace XRL.World
{
	// Token: 0x02000825 RID: 2085
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class PointDefenseInterceptEvent : PooledEvent<PointDefenseInterceptEvent>
	{
		// Token: 0x060052FE RID: 21246 RVA: 0x0018E588 File Offset: 0x0018C788
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060052FF RID: 21247 RVA: 0x0018E591 File Offset: 0x0018C791
		public override void Reset()
		{
			base.Reset();
			this.TargetProjectile = null;
			this.InterceptProjectile = null;
			this.PointDefenseSystem = null;
		}

		// Token: 0x06005300 RID: 21248 RVA: 0x0018E5B0 File Offset: 0x0018C7B0
		public static bool Check(GameObject TargetProjectile, GameObject InterceptProjectile, GameObject PointDefenseSystem)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref TargetProjectile) && TargetProjectile.HasRegisteredEvent("PointDefenseIntercept"))
			{
				Event @event = Event.New("PointDefenseIntercept", 0, 0, 0);
				@event.SetParameter("TargetProjectile", TargetProjectile);
				@event.SetParameter("InterceptProjectile", InterceptProjectile);
				@event.SetParameter("PointDefenseSystem", PointDefenseSystem);
				flag = TargetProjectile.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref TargetProjectile) && TargetProjectile.WantEvent(PooledEvent<PointDefenseInterceptEvent>.ID, MinEvent.CascadeLevel))
			{
				PointDefenseInterceptEvent pointDefenseInterceptEvent = PooledEvent<PointDefenseInterceptEvent>.FromPool();
				pointDefenseInterceptEvent.TargetProjectile = TargetProjectile;
				pointDefenseInterceptEvent.InterceptProjectile = InterceptProjectile;
				pointDefenseInterceptEvent.PointDefenseSystem = PointDefenseSystem;
				flag = TargetProjectile.HandleEvent(pointDefenseInterceptEvent);
			}
			return flag;
		}

		// Token: 0x040029BF RID: 10687
		public GameObject TargetProjectile;

		// Token: 0x040029C0 RID: 10688
		public GameObject InterceptProjectile;

		// Token: 0x040029C1 RID: 10689
		public GameObject PointDefenseSystem;
	}
}
