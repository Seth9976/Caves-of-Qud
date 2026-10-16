using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200138E RID: 5006
	[Serializable]
	public class ShortBlades_Rejoinder : BaseSkill
	{
		// Token: 0x0600C1F8 RID: 49656 RVA: 0x0046DDF9 File Offset: 0x0046BFF9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C1F9 RID: 49657 RVA: 0x0046DDFC File Offset: 0x0046BFFC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CommandToggleRejoinder");
			Registrar.Register("DefenderAfterAttackMissed");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1FA RID: 49658 RVA: 0x0046DE28 File Offset: 0x0046C028
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				this.Checked = false;
			}
			else if (E.ID == "DefenderAfterAttackMissed")
			{
				if (this.Checked)
				{
					return true;
				}
				this.Checked = true;
				if (!base.IsMyActivatedAbilityVoluntarilyUsable(this.ActivatedAbilityID, null) || !base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
				{
					return true;
				}
				if (!60.in100())
				{
					return true;
				}
				if (!this.ParentObject.CanMoveExtremities("Attack", false, false, false) || !this.ParentObject.CanChangeBodyPosition("Attack", false, false, false, false))
				{
					return true;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObject = null;
				List<GameObject> list = Event.NewGameObjectList();
				Body body = this.ParentObject.Body;
				foreach (BodyPart bodyPart in body.GetParts())
				{
					if (bodyPart.Primary && gameObject == null)
					{
						gameObject = bodyPart.GetFirstValidWeapon(null);
					}
					if (!(bodyPart.Type == "Thrown Weapon"))
					{
						GameObject equipped = bodyPart.Equipped;
						MeleeWeapon meleeWeapon = ((equipped != null) ? equipped.GetPart<MeleeWeapon>() : null);
						if (meleeWeapon != null && meleeWeapon.Skill == "ShortBlades")
						{
							list.Add(bodyPart.Equipped);
						}
						else
						{
							GameObject defaultBehavior = bodyPart.DefaultBehavior;
							meleeWeapon = ((defaultBehavior != null) ? defaultBehavior.GetPart<MeleeWeapon>() : null);
							if (meleeWeapon != null && meleeWeapon.Skill == "ShortBlades")
							{
								list.Add(bodyPart.DefaultBehavior);
							}
						}
					}
				}
				GameObject randomElement = list.GetRandomElement<GameObject>(null);
				if (randomElement != null)
				{
					this.ParentObject.ParticleText("*rejoinder*", IComponent<GameObject>.ConsequentialColorChar(this.ParentObject, null), false, 1.5f, -8f);
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You rejoinder with " + this.ParentObject.poss(randomElement, true, null) + ".", 'G', true);
					}
					else if (gameObjectParameter.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							this.ParentObject.The,
							this.ParentObject.ShortDisplayName,
							this.ParentObject.GetVerb("rejoinder", true, false, false, true, false),
							" with ",
							this.ParentObject.poss(randomElement, true, null),
							"."
						}), 'R', true);
					}
					Combat.MeleeAttackWithWeapon(this.ParentObject, gameObjectParameter, randomElement, body.FindDefaultOrEquippedItem(randomElement), null, 0, 0, 0, 0, 0, randomElement == gameObject, true);
				}
			}
			else if (E.ID == "CommandToggleRejoinder")
			{
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C1FB RID: 49659 RVA: 0x0046E118 File Offset: 0x0046C318
		private void AddAbility()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Rejoinder", "CommandToggleRejoinder", "Skills", null, "\u001b", null, true, true, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x0600C1FC RID: 49660 RVA: 0x0046E159 File Offset: 0x0046C359
		public override bool AddSkill(GameObject GO)
		{
			this.AddAbility();
			return base.AddSkill(GO);
		}

		// Token: 0x0600C1FD RID: 49661 RVA: 0x0046E168 File Offset: 0x0046C368
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D89 RID: 19849
		public Guid ActivatedAbilityID;

		// Token: 0x04004D8A RID: 19850
		public bool Checked;
	}
}
