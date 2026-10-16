using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001332 RID: 4914
	[Serializable]
	public class Cudgel_Expertise : BaseSkill
	{
		// Token: 0x0600C007 RID: 49159 RVA: 0x004654E1 File Offset: 0x004636E1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID;
		}

		// Token: 0x0600C008 RID: 49160 RVA: 0x004654F8 File Offset: 0x004636F8
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Actor == this.ParentObject && E.Checking == "Actor" && E.Skill == "Cudgel" && E.Melee)
			{
				E.Modifier += this.HitBonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C009 RID: 49161 RVA: 0x00465559 File Offset: 0x00463759
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C00A RID: 49162 RVA: 0x0046555C File Offset: 0x0046375C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C00B RID: 49163 RVA: 0x00465574 File Offset: 0x00463774
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Weapon");
				if (gameObjectParameter != null)
				{
					MeleeWeapon part = gameObjectParameter.GetPart<MeleeWeapon>();
					if (part != null && part.Skill == "Cudgel" && this.ParentObject.HasSkill("Cudgel_Bonecrusher"))
					{
						(E.GetParameter("Damage") as Damage).Amount *= 2;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004D40 RID: 19776
		public int HitBonus = 2;
	}
}
