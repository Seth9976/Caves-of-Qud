using System;

namespace XRL.World.Parts
{
	// Token: 0x02001109 RID: 4361
	[Serializable]
	public class AmmoGeneric : IAmmo
	{
		// Token: 0x0600ACE3 RID: 44259 RVA: 0x003F7F24 File Offset: 0x003F6124
		public override bool SameAs(IPart p)
		{
			AmmoGeneric ammoGeneric = p as AmmoGeneric;
			return !(ammoGeneric.ProjectileObject != this.ProjectileObject) && !(ammoGeneric.SpecializedProjectileObject != this.SpecializedProjectileObject) && base.SameAs(p);
		}

		// Token: 0x0600ACE4 RID: 44260 RVA: 0x003F7F69 File Offset: 0x003F6169
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600ACE5 RID: 44261 RVA: 0x003F7F88 File Offset: 0x003F6188
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains(this.ParentObject.GetTag("AmmoPartType", null)) && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACE6 RID: 44262 RVA: 0x003F7FE0 File Offset: 0x003F61E0
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			if (!string.IsNullOrEmpty(this.SpecializedProjectileObject))
			{
				E.Projectile = GameObject.Create(this.SpecializedProjectileObject);
				return false;
			}
			if (!string.IsNullOrEmpty(this.ProjectileObject))
			{
				E.Projectile = GameObject.Create(this.ProjectileObject);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACE7 RID: 44263 RVA: 0x003F8034 File Offset: 0x003F6234
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400476A RID: 18282
		public string ProjectileObject;

		// Token: 0x0400476B RID: 18283
		public string SpecializedProjectileObject;
	}
}
