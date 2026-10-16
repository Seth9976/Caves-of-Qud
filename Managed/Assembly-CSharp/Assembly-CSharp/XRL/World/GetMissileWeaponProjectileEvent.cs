using System;

namespace XRL.World
{
	// Token: 0x02000769 RID: 1897
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMissileWeaponProjectileEvent : PooledEvent<GetMissileWeaponProjectileEvent>
	{
		// Token: 0x06004E21 RID: 20001 RVA: 0x0017F4E6 File Offset: 0x0017D6E6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x0017F4EF File Offset: 0x0017D6EF
		public override void Reset()
		{
			base.Reset();
			this.Launcher = null;
			this.Projectile = null;
			this.Blueprint = null;
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x0017F50C File Offset: 0x0017D70C
		public static GetMissileWeaponProjectileEvent FromPool(GameObject Launcher)
		{
			GetMissileWeaponProjectileEvent getMissileWeaponProjectileEvent = PooledEvent<GetMissileWeaponProjectileEvent>.FromPool();
			getMissileWeaponProjectileEvent.Launcher = Launcher;
			return getMissileWeaponProjectileEvent;
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x0017F51C File Offset: 0x0017D71C
		public static bool GetFor(GameObject Launcher, ref GameObject Projectile, ref string Blueprint)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Launcher) && Launcher.WantEvent(PooledEvent<GetMissileWeaponProjectileEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMissileWeaponProjectileEvent getMissileWeaponProjectileEvent = PooledEvent<GetMissileWeaponProjectileEvent>.FromPool();
				getMissileWeaponProjectileEvent.Launcher = Launcher;
				getMissileWeaponProjectileEvent.Projectile = Projectile;
				getMissileWeaponProjectileEvent.Blueprint = Blueprint;
				flag = Launcher.HandleEvent(getMissileWeaponProjectileEvent);
				Projectile = getMissileWeaponProjectileEvent.Projectile;
				Blueprint = getMissileWeaponProjectileEvent.Blueprint;
			}
			if (flag && GameObject.Validate(ref Launcher) && Launcher.HasRegisteredEvent("GetMissileWeaponProjectile"))
			{
				Event @event = Event.New("GetMissileWeaponProjectile", 0, 0, 0);
				@event.SetParameter("Launcher", Launcher);
				@event.SetParameter("Projectile", Projectile);
				@event.SetParameter("Blueprint", Blueprint);
				flag = Launcher.FireEvent(@event);
				Projectile = @event.GetGameObjectParameter("Projectile");
				Blueprint = @event.GetStringParameter("Blueprint", null);
			}
			return flag;
		}

		// Token: 0x04002615 RID: 9749
		public GameObject Launcher;

		// Token: 0x04002616 RID: 9750
		public GameObject Projectile;

		// Token: 0x04002617 RID: 9751
		public string Blueprint;
	}
}
