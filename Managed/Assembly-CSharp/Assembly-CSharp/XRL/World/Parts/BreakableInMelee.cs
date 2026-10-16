using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001001 RID: 4097
	[Serializable]
	public class BreakableInMelee : IPart
	{
		// Token: 0x0600A353 RID: 41811 RVA: 0x003BB184 File Offset: 0x003B9384
		public override bool SameAs(IPart Part)
		{
			BreakableInMelee breakableInMelee = Part as BreakableInMelee;
			return breakableInMelee.Chance == this.Chance && breakableInMelee.WhileInInventory == this.WhileInInventory && base.SameAs(Part);
		}

		// Token: 0x0600A354 RID: 41812 RVA: 0x003BB1BF File Offset: 0x003B93BF
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || (ID == PooledEvent<DefendMeleeHitEvent>.ID && (this.WhileInInventory || this.ParentObject.Equipped != null));
		}

		// Token: 0x0600A355 RID: 41813 RVA: 0x003BB1F0 File Offset: 0x003B93F0
		public override bool HandleEvent(DefendMeleeHitEvent E)
		{
			if (E.Damage != null && E.Damage.Amount > 0 && (this.WhileInInventory || this.ParentObject.Equipped != null) && this.Chance.in100() && this.ParentObject.ApplyEffect(new Broken(), null))
			{
				base.DidX("break", null, null, null, null, null, this.ParentObject.Equipped, false, false, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A356 RID: 41814 RVA: 0x003BB274 File Offset: 0x003B9474
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040043B0 RID: 17328
		public int Chance = 1;

		// Token: 0x040043B1 RID: 17329
		public bool WhileInInventory;
	}
}
