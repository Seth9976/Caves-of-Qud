using System;

namespace XRL.World.Parts
{
	// Token: 0x0200110A RID: 4362
	[Serializable]
	public class AmmoGrenade : IAmmo
	{
		// Token: 0x0600ACE9 RID: 44265 RVA: 0x003F803F File Offset: 0x003F623F
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600ACEA RID: 44266 RVA: 0x003F8042 File Offset: 0x003F6242
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetProjectileObjectEvent>.ID || ID == QueryEquippableListEvent.ID;
		}

		// Token: 0x0600ACEB RID: 44267 RVA: 0x003F8060 File Offset: 0x003F6260
		public override bool HandleEvent(QueryEquippableListEvent E)
		{
			if (E.SlotType.Contains("AmmoGrenade") && !E.List.Contains(this.ParentObject))
			{
				E.List.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ACEC RID: 44268 RVA: 0x003F809F File Offset: 0x003F629F
		public override bool HandleEvent(GetProjectileObjectEvent E)
		{
			E.Projectile = this.ParentObject;
			return false;
		}

		// Token: 0x0600ACED RID: 44269 RVA: 0x003F80AE File Offset: 0x003F62AE
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
