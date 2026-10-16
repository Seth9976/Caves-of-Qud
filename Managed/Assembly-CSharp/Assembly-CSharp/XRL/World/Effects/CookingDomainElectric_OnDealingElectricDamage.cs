using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C9C RID: 3228
	public class CookingDomainElectric_OnDealingElectricDamage : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060081B1 RID: 33201 RVA: 0x002F6A65 File Offset: 0x002F4C65
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature deal@s electric damage, there's a 25% chance";
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x002F6A6C File Offset: 0x002F4C6C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerDealtDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x002F6A84 File Offset: 0x002F4C84
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerDealtDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.IsElectricDamage() && 25.in100())
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
