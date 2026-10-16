using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200133F RID: 4927
	[Serializable]
	public class Endurance_PoisonTolerance : BaseSkill
	{
		// Token: 0x0600C058 RID: 49240 RVA: 0x004676C4 File Offset: 0x004658C4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C059 RID: 49241 RVA: 0x004676DC File Offset: 0x004658DC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.HasAttribute("Poison"))
				{
					damage.Amount = damage.Amount * 3 / 4;
					if (damage.Amount <= 0)
					{
						return false;
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
