using System;

namespace XRL.World.Parts
{
	// Token: 0x0200110B RID: 4363
	[Serializable]
	public class AmmoMissile : IAmmo
	{
		// Token: 0x0600ACEF RID: 44271 RVA: 0x003F80B9 File Offset: 0x003F62B9
		public override bool SameAs(IPart p)
		{
			return !((p as AmmoMissile).ProjectileObject != this.ProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600ACF0 RID: 44272 RVA: 0x003F80DC File Offset: 0x003F62DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600ACF1 RID: 44273 RVA: 0x003F80FA File Offset: 0x003F62FA
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains("AmmoMissile") && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACF2 RID: 44274 RVA: 0x003F8139 File Offset: 0x003F6339
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			if (!string.IsNullOrEmpty(this.ProjectileObject))
			{
				E.Projectile = GameObject.Create(this.ProjectileObject);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACF3 RID: 44275 RVA: 0x003F8162 File Offset: 0x003F6362
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400476C RID: 18284
		public string ProjectileObject;
	}
}
