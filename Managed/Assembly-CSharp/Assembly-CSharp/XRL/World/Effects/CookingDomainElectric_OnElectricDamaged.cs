using System;
using XRL.Language;

namespace XRL.World.Effects
{
	// Token: 0x02000C9B RID: 3227
	public class CookingDomainElectric_OnElectricDamaged : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060081AC RID: 33196 RVA: 0x002F69C6 File Offset: 0x002F4BC6
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature take@s electric damage, there's " + Grammar.A(this.Tier, false) + "% chance";
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x002F69E3 File Offset: 0x002F4BE3
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature take@s electric damage, there's a 50% chance";
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x002F69EA File Offset: 0x002F4BEA
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x002F6A00 File Offset: 0x002F4C00
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.IsElectricDamage() && this.Tier.in100())
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003925 RID: 14629
		public int Tier = 50;
	}
}
