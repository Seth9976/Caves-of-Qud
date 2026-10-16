using System;

namespace XRL.World
{
	// Token: 0x02000779 RID: 1913
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetProjectileObjectEvent : PooledEvent<GetProjectileObjectEvent>
	{
		// Token: 0x06004E96 RID: 20118 RVA: 0x00180F49 File Offset: 0x0017F149
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00180F52 File Offset: 0x0017F152
		public override void Reset()
		{
			base.Reset();
			this.Ammo = null;
			this.Launcher = null;
			this.Projectile = null;
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00180F70 File Offset: 0x0017F170
		public static GameObject GetFor(GameObject Ammo, GameObject Launcher)
		{
			bool flag = true;
			GameObject gameObject = null;
			if (flag && GameObject.Validate(ref Ammo) && Ammo.WantEvent(PooledEvent<GetProjectileObjectEvent>.ID, MinEvent.CascadeLevel))
			{
				GetProjectileObjectEvent getProjectileObjectEvent = PooledEvent<GetProjectileObjectEvent>.FromPool();
				getProjectileObjectEvent.Ammo = Ammo;
				getProjectileObjectEvent.Launcher = Launcher;
				getProjectileObjectEvent.Projectile = gameObject;
				flag = Ammo.HandleEvent(getProjectileObjectEvent);
				gameObject = getProjectileObjectEvent.Projectile;
			}
			if (flag && GameObject.Validate(ref Ammo) && Ammo.HasRegisteredEvent("GetProjectileObject"))
			{
				Event @event = Event.New("GetProjectileObject", 0, 0, 0);
				@event.SetParameter("Ammo", Ammo);
				@event.SetParameter("Launcher", Launcher);
				@event.SetParameter("Projectile", gameObject);
				flag = Ammo.FireEvent(@event);
				gameObject = @event.GetGameObjectParameter("Projectile");
			}
			return gameObject;
		}

		// Token: 0x0400265D RID: 9821
		public GameObject Ammo;

		// Token: 0x0400265E RID: 9822
		public GameObject Launcher;

		// Token: 0x0400265F RID: 9823
		public GameObject Projectile;
	}
}
