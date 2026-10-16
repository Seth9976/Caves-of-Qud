using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CAF RID: 3247
	public class CookingDomainHeat_OnDealingHeatDamage : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060081F9 RID: 33273 RVA: 0x002F7084 File Offset: 0x002F5284
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature deal@s fire damage, there's a 10% chance";
		}

		// Token: 0x060081FA RID: 33274 RVA: 0x002F708B File Offset: 0x002F528B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerDealtDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x002F70A0 File Offset: 0x002F52A0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerDealtDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && (damage.HasAttribute("Fire") || damage.HasAttribute("Heat")) && Stat.Random(1, 100) <= 10)
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
