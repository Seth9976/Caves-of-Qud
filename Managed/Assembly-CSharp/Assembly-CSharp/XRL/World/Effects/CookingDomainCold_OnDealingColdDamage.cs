using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C8C RID: 3212
	public class CookingDomainCold_OnDealingColdDamage : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008175 RID: 33141 RVA: 0x002F64B8 File Offset: 0x002F46B8
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature deal@s cold damage, there's a 25% chance";
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x002F64BF File Offset: 0x002F46BF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerDealtDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x002F64D4 File Offset: 0x002F46D4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerDealtDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.HasAttribute("Cold") && Stat.Random(1, 100) <= 25)
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
