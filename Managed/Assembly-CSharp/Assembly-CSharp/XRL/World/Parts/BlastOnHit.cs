using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200110E RID: 4366
	[Serializable]
	public class BlastOnHit : IPart
	{
		// Token: 0x0600AD01 RID: 44289 RVA: 0x003F82D5 File Offset: 0x003F64D5
		public override bool SameAs(IPart p)
		{
			return (p as ExplodeOnHit).Force == this.Force && base.SameAs(p);
		}

		// Token: 0x0600AD02 RID: 44290 RVA: 0x003F82F3 File Offset: 0x003F64F3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterThrownEvent.ID || ID == PooledEvent<IsExplosiveEvent>.ID;
		}

		// Token: 0x0600AD03 RID: 44291 RVA: 0x003F8314 File Offset: 0x003F6514
		public override bool HandleEvent(AfterThrownEvent E)
		{
			try
			{
				this.Detonate(null);
			}
			finally
			{
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD04 RID: 44292 RVA: 0x003F8354 File Offset: 0x003F6554
		public override bool HandleEvent(IsExplosiveEvent E)
		{
			return false;
		}

		// Token: 0x0600AD05 RID: 44293 RVA: 0x003F8357 File Offset: 0x003F6557
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AD06 RID: 44294 RVA: 0x003F835A File Offset: 0x003F655A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD07 RID: 44295 RVA: 0x003F836F File Offset: 0x003F656F
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileHit")
			{
				this.Detonate(E.GetGameObjectParameter("ApparentTarget"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD08 RID: 44296 RVA: 0x003F839C File Offset: 0x003F659C
		public void Detonate(GameObject ApparentTarget = null)
		{
			base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell != null)
			{
				try
				{
					StunningForce.Concussion(currentCell, this.ParentObject, 3, this.Force, this.ParentObject.GetPhase(), ApparentTarget, true, true);
				}
				catch (Exception ex)
				{
					MetricsManager.LogError(ex);
				}
			}
		}

		// Token: 0x0400476F RID: 18287
		public int Force = 2;
	}
}
