using System;

namespace XRL.World.Parts
{
	// Token: 0x02001108 RID: 4360
	[Serializable]
	public class AmmoDart : IAmmo
	{
		// Token: 0x0600ACDD RID: 44253 RVA: 0x003F7EA9 File Offset: 0x003F60A9
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600ACDE RID: 44254 RVA: 0x003F7EAC File Offset: 0x003F60AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600ACDF RID: 44255 RVA: 0x003F7ECA File Offset: 0x003F60CA
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains("AmmoDart") && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACE0 RID: 44256 RVA: 0x003F7F09 File Offset: 0x003F6109
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			E.Projectile = this.ParentObject;
			return false;
		}

		// Token: 0x0600ACE1 RID: 44257 RVA: 0x003F7F18 File Offset: 0x003F6118
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
