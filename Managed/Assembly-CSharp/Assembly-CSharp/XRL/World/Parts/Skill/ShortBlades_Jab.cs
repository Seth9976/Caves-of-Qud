using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200138B RID: 5003
	[Serializable]
	public class ShortBlades_Jab : BaseSkill
	{
		// Token: 0x0600C1F0 RID: 49648 RVA: 0x0046DCE3 File Offset: 0x0046BEE3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(PooledEvent<GetMeleeAttackChanceEvent>.ID, EventOrder.EARLY, false);
		}

		// Token: 0x0600C1F1 RID: 49649 RVA: 0x0046DCF8 File Offset: 0x0046BEF8
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (!E.Primary)
			{
				GameObject weapon = E.Weapon;
				if (((weapon != null) ? weapon.GetWeaponSkill() : null) == "ShortBlades" && !E.Properties.HasDelimitedSubstring(',', "Flurrying", StringComparison.Ordinal))
				{
					E.Attempts++;
				}
			}
			return base.HandleEvent(E);
		}
	}
}
