using System;

namespace XRL.World.Parts
{
	// Token: 0x02001110 RID: 4368
	[Serializable]
	public class ExplodeOnHit : IPart
	{
		// Token: 0x0600AD10 RID: 44304 RVA: 0x003F86CC File Offset: 0x003F68CC
		public override bool SameAs(IPart p)
		{
			ExplodeOnHit explodeOnHit = p as ExplodeOnHit;
			return explodeOnHit.Force == this.Force && !(explodeOnHit.Damage != this.Damage) && base.SameAs(p);
		}

		// Token: 0x0600AD11 RID: 44305 RVA: 0x003F870C File Offset: 0x003F690C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterThrownEvent.ID || ID == PooledEvent<IsExplosiveEvent>.ID;
		}

		// Token: 0x0600AD12 RID: 44306 RVA: 0x003F872A File Offset: 0x003F692A
		public override bool HandleEvent(AfterThrownEvent E)
		{
			this.Detonate(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD13 RID: 44307 RVA: 0x003F873F File Offset: 0x003F693F
		public override bool HandleEvent(IsExplosiveEvent E)
		{
			return false;
		}

		// Token: 0x0600AD14 RID: 44308 RVA: 0x003F8742 File Offset: 0x003F6942
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD15 RID: 44309 RVA: 0x003F8757 File Offset: 0x003F6957
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileHit")
			{
				this.Detonate(E.GetGameObjectParameter("Owner"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD16 RID: 44310 RVA: 0x003F8784 File Offset: 0x003F6984
		public void Detonate(GameObject Owner = null)
		{
			base.DidX("explode", null, "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.ParentObject.Explode(this.Force, Owner, this.Damage, 1f, false, false, false, 0, null);
		}

		// Token: 0x04004774 RID: 18292
		public int Force = 10000;

		// Token: 0x04004775 RID: 18293
		public string Damage = "0";
	}
}
