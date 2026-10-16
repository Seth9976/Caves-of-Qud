using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001115 RID: 4373
	[Serializable]
	public class StickyOnHit : IPart
	{
		// Token: 0x0600AD31 RID: 44337 RVA: 0x003F8C78 File Offset: 0x003F6E78
		public override bool SameAs(IPart p)
		{
			StickyOnHit stickyOnHit = p as StickyOnHit;
			return !(stickyOnHit.Duration != this.Duration) && !(stickyOnHit.SaveTarget != this.SaveTarget) && stickyOnHit.MaxWeight == this.MaxWeight && base.SameAs(p);
		}

		// Token: 0x0600AD32 RID: 44338 RVA: 0x003F8CCD File Offset: 0x003F6ECD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterThrownEvent.ID;
		}

		// Token: 0x0600AD33 RID: 44339 RVA: 0x003F8CE3 File Offset: 0x003F6EE3
		public override bool HandleEvent(AfterThrownEvent E)
		{
			this.Entangle(null);
			this.ParentObject.Destroy(null, false, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD34 RID: 44340 RVA: 0x003F8D03 File Offset: 0x003F6F03
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD35 RID: 44341 RVA: 0x003F8D18 File Offset: 0x003F6F18
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileHit")
			{
				this.Entangle(E.GetGameObjectParameter("Defender"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD36 RID: 44342 RVA: 0x003F8D44 File Offset: 0x003F6F44
		public void Entangle(GameObject Target = null)
		{
			if (Target == null)
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null)
				{
					Target = currentCell.GetCombatTarget(this.ParentObject, true, false, false, 0, null, null, null, null, null, false, false, null);
				}
			}
			if (Target != null && Target.ApplyEffect(new Stuck(this.Duration.RollCached(), this.SaveTarget.RollCached(), this.SaveVs, this.ParentObject, "stuck", "in", null, "Move", 0.25f, 1f, 1f, false, false), null))
			{
				IComponent<GameObject>.XDidYToZ(Target, "get", "entangled in", this.ParentObject, null, "!", null, null, null, Target, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x0400477A RID: 18298
		public string Duration = "12";

		// Token: 0x0400477B RID: 18299
		public string SaveTarget = "25";

		// Token: 0x0400477C RID: 18300
		public string SaveVs = "Entangle Stuck Restraint";

		// Token: 0x0400477D RID: 18301
		public int MaxWeight = 1000;
	}
}
