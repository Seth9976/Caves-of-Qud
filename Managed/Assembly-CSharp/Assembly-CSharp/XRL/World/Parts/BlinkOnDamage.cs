using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E4D RID: 3661
	[Serializable]
	public class BlinkOnDamage : IPart
	{
		// Token: 0x0600945C RID: 37980 RVA: 0x0035665D File Offset: 0x0035485D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600945D RID: 37981 RVA: 0x00356674 File Offset: 0x00354874
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage" && !this.ParentObject.OnWorldMap() && !(E.GetParameter("Damage") as Damage).HasAttribute("Unavoidable") && IComponent<GameObject>.CheckRealityDistortionUsability(this.ParentObject, null, this.ParentObject, null, null, null, 0))
			{
				base.DidX("blink", "away from the danger", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				this.ParentObject.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
			}
			return base.FireEvent(E);
		}
	}
}
