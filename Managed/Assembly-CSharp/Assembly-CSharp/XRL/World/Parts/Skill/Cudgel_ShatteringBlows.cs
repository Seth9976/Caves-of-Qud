using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001334 RID: 4916
	[Serializable]
	public class Cudgel_ShatteringBlows : BaseSkill
	{
		// Token: 0x0600C010 RID: 49168 RVA: 0x004656A9 File Offset: 0x004638A9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C011 RID: 49169 RVA: 0x004656C0 File Offset: 0x004638C0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				MeleeWeapon meleeWeapon = ((gameObjectParameter != null) ? gameObjectParameter.GetPart<MeleeWeapon>() : null);
				if (meleeWeapon != null && meleeWeapon.Skill == "Cudgel" && 10.in100())
				{
					E.GetGameObjectParameter("Defender").ApplyEffect(new ShatterArmor(2000), null);
				}
			}
			return base.FireEvent(E);
		}
	}
}
