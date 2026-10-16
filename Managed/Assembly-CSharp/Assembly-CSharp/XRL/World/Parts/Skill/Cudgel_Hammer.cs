using System;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001333 RID: 4915
	[Serializable]
	public class Cudgel_Hammer : BaseSkill
	{
		// Token: 0x0600C00D RID: 49165 RVA: 0x00465603 File Offset: 0x00463803
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C00E RID: 49166 RVA: 0x00465618 File Offset: 0x00463818
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				MeleeWeapon meleeWeapon = ((gameObjectParameter != null) ? gameObjectParameter.GetPart<MeleeWeapon>() : null);
				if (((meleeWeapon != null) ? meleeWeapon.Skill : null) == "Cudgel" && 2.in100())
				{
					E.GetGameObjectParameter("Defender").Body.GetEquippedParts().GetRandomElement<BodyPart>(null).Equipped.ApplyEffect(new Broken(), null);
				}
			}
			return base.FireEvent(E);
		}
	}
}
