using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F2A RID: 3882
	[Serializable]
	public class CyberneticsForcefieldNullifier : IPart
	{
		// Token: 0x06009AC8 RID: 39624 RVA: 0x00382216 File Offset: 0x00380416
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AC9 RID: 39625 RVA: 0x00382219 File Offset: 0x00380419
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanMissilePassForcefieldEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009ACA RID: 39626 RVA: 0x0038223F File Offset: 0x0038043F
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.ModIntProperty("ForcefieldNullifier", 1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009ACB RID: 39627 RVA: 0x0038225B File Offset: 0x0038045B
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.ModIntProperty("ForcefieldNullifier", -1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009ACC RID: 39628 RVA: 0x00382277 File Offset: 0x00380477
		public override bool HandleEvent(CanMissilePassForcefieldEvent E)
		{
			GameObject projectile = E.Projectile;
			return (projectile == null || !projectile.HasTagOrProperty("ForcefieldNullifierCarryover") || E.Actor != this.ParentObject.Implantee) && base.HandleEvent(E);
		}

		// Token: 0x06009ACD RID: 39629 RVA: 0x003822AE File Offset: 0x003804AE
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
