using System;

namespace XRL.World.Parts
{
	// Token: 0x0200110C RID: 4364
	[Serializable]
	public class AmmoSlug : IAmmo
	{
		// Token: 0x0600ACF5 RID: 44277 RVA: 0x003F816D File Offset: 0x003F636D
		public override bool SameAs(IPart p)
		{
			return !((p as AmmoSlug).ProjectileObject != this.ProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600ACF6 RID: 44278 RVA: 0x003F8190 File Offset: 0x003F6390
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600ACF7 RID: 44279 RVA: 0x003F81AE File Offset: 0x003F63AE
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains("AmmoSlug") && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACF8 RID: 44280 RVA: 0x003F81ED File Offset: 0x003F63ED
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			if (!string.IsNullOrEmpty(this.ProjectileObject))
			{
				E.Projectile = GameObject.Create(this.ProjectileObject);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACF9 RID: 44281 RVA: 0x003F8216 File Offset: 0x003F6416
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400476D RID: 18285
		public string ProjectileObject;
	}
}
