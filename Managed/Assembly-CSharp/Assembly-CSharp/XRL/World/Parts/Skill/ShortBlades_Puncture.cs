using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200138D RID: 5005
	[Serializable]
	public class ShortBlades_Puncture : BaseSkill
	{
		// Token: 0x0600C1F5 RID: 49653 RVA: 0x0046DD6C File Offset: 0x0046BF6C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GetAttackerHitDice");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1F6 RID: 49654 RVA: 0x0046DD84 File Offset: 0x0046BF84
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GetAttackerHitDice")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				if (gameObjectParameter != null)
				{
					MeleeWeapon part = gameObjectParameter.GetPart<MeleeWeapon>();
					if (((part != null) ? part.Skill : null) == "ShortBlades")
					{
						E.SetParameter("AV", E.GetIntParameter("AV", 0) - 2);
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
