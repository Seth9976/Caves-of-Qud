using System;

namespace XRL.World.Parts
{
	// Token: 0x0200110D RID: 4365
	[Serializable]
	public class AmmoShotgunShell : IAmmo
	{
		// Token: 0x0600ACFB RID: 44283 RVA: 0x003F8221 File Offset: 0x003F6421
		public override bool SameAs(IPart p)
		{
			return !((p as AmmoShotgunShell).ProjectileObject != this.ProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600ACFC RID: 44284 RVA: 0x003F8244 File Offset: 0x003F6444
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600ACFD RID: 44285 RVA: 0x003F8262 File Offset: 0x003F6462
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains("AmmoShotgunShell") && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACFE RID: 44286 RVA: 0x003F82A1 File Offset: 0x003F64A1
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			if (!string.IsNullOrEmpty(this.ProjectileObject))
			{
				E.Projectile = GameObject.Create(this.ProjectileObject);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACFF RID: 44287 RVA: 0x003F82CA File Offset: 0x003F64CA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400476E RID: 18286
		public string ProjectileObject;
	}
}
