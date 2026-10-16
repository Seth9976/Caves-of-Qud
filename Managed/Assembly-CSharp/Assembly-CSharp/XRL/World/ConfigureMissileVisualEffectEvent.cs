using System;

namespace XRL.World
{
	// Token: 0x020006EE RID: 1774
	[GameEvent]
	public class ConfigureMissileVisualEffectEvent : PooledEvent<ConfigureMissileVisualEffectEvent>
	{
		// Token: 0x06004A68 RID: 19048 RVA: 0x001743B2 File Offset: 0x001725B2
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x001743BB File Offset: 0x001725BB
		public override void Reset()
		{
			base.Reset();
			this.Configuration = null;
			this.Path = null;
			this.Actor = null;
			this.Launcher = null;
			this.Projectile = null;
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x001743E8 File Offset: 0x001725E8
		public static void Send(MissileWeaponVFXConfiguration Configuration, MissileWeaponVFXConfiguration.MissileVFXPathDefinition Path, GameObject Actor, GameObject Launcher, GameObject Projectile)
		{
			ConfigureMissileVisualEffectEvent configureMissileVisualEffectEvent = PooledEvent<ConfigureMissileVisualEffectEvent>.FromPool();
			configureMissileVisualEffectEvent.Configuration = Configuration;
			configureMissileVisualEffectEvent.Path = Path;
			configureMissileVisualEffectEvent.Actor = Actor;
			configureMissileVisualEffectEvent.Launcher = Launcher;
			configureMissileVisualEffectEvent.Projectile = Projectile;
			if (Actor != null && Actor != Launcher)
			{
				Actor.HandleEvent(configureMissileVisualEffectEvent);
			}
			if (Launcher != null && Launcher != Projectile)
			{
				Launcher.HandleEvent(configureMissileVisualEffectEvent);
			}
			if (Projectile != null)
			{
				Projectile.HandleEvent(configureMissileVisualEffectEvent);
			}
			PooledEvent<ConfigureMissileVisualEffectEvent>.ResetTo(ref configureMissileVisualEffectEvent);
		}

		// Token: 0x040023DC RID: 9180
		public MissileWeaponVFXConfiguration Configuration;

		// Token: 0x040023DD RID: 9181
		public MissileWeaponVFXConfiguration.MissileVFXPathDefinition Path;

		// Token: 0x040023DE RID: 9182
		public GameObject Actor;

		// Token: 0x040023DF RID: 9183
		public GameObject Launcher;

		// Token: 0x040023E0 RID: 9184
		public GameObject Projectile;
	}
}
