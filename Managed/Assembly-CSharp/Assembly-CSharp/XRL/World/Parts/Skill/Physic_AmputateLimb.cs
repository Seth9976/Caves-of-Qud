using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001361 RID: 4961
	[Serializable]
	public class Physic_AmputateLimb : BaseSkill
	{
		// Token: 0x0600C12D RID: 49453 RVA: 0x0046AB3A File Offset: 0x00468D3A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C12E RID: 49454 RVA: 0x0046AB50 File Offset: 0x00468D50
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C12F RID: 49455 RVA: 0x0046AB73 File Offset: 0x00468D73
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandAmputateLimb");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C130 RID: 49456 RVA: 0x0046AB88 File Offset: 0x00468D88
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandAmputateLimb")
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject.AreHostilesNearby())
				{
					parentObject.ShowFailure("You can't perform field amputations with hostiles nearby!");
					return false;
				}
				if (!parentObject.CanMoveExtremities(null, true, false, true))
				{
					return false;
				}
				GameObject weapon = parentObject.GetWeapon(new Predicate<GameObject>(Physic_AmputateLimb.IsUsableWeapon));
				if (weapon == null)
				{
					parentObject.ShowFailure("You must have an axe or a weapon capable of dismemberment equipped in order to perform a field amputation.");
					return false;
				}
				Cell cell = base.PickDirection("Amputate whose limb?", null);
				if (cell == null)
				{
					return false;
				}
				GameObject gameObject = ((cell == parentObject.CurrentCell) ? parentObject : cell.GetCombatTarget(parentObject, false, true, true, 0, null, null, null, null, null, false, false, null));
				if (gameObject == null)
				{
					gameObject = cell.GetCombatTarget(parentObject, true, true, true, 0, null, null, null, null, null, false, false, null);
					if (gameObject != null)
					{
						parentObject.ShowFailure(string.Concat(new string[]
						{
							"You cannot reach ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" to amputate ",
							gameObject.its,
							" limb."
						}));
						return false;
					}
					parentObject.ShowFailure("There is no one there for you to amputate their limb.");
					return false;
				}
				else
				{
					if (gameObject != parentObject && !gameObject.IsLedBy(parentObject))
					{
						if (gameObject.CanMoveExtremities(null, false, false, false))
						{
							parentObject.ShowFailure(gameObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " won't let you do that.");
						}
						else
						{
							parentObject.ShowFailure("You cannot amputate " + gameObject.poss("limbs", true) + ".");
						}
						return false;
					}
					Body body = gameObject.Body;
					if (body == null)
					{
						parentObject.ShowFailure(gameObject.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no limbs.");
						return false;
					}
					List<BodyPart> list = new List<BodyPart>();
					foreach (BodyPart bodyPart in body.LoopParts())
					{
						if (bodyPart.IsSeverable())
						{
							list.Add(bodyPart);
						}
					}
					if (list.Count <= 0)
					{
						parentObject.ShowFailure(gameObject.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no limbs that can be amputated.");
						return false;
					}
					string[] array = new string[list.Count];
					char[] array2 = new char[list.Count];
					char c = 'a';
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						BodyPart bodyPart2 = list[i];
						string text = bodyPart2.GetOrdinalName();
						if (bodyPart2.Equipped != null)
						{
							text = text + " (" + bodyPart2.Equipped.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true) + ")";
						}
						array[i] = text;
						char[] array3 = array2;
						int num = i;
						int num2;
						if (c > 'z')
						{
							num2 = 32;
						}
						else
						{
							c = (char)((num2 = (int)c) + 1);
						}
						array3[num] = num2;
						i++;
					}
					int num3 = Popup.PickOption("Amputate which limb?", null, "", "Sounds/UI/ui_notification", array, array2, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
					if (num3 < 0)
					{
						return false;
					}
					BodyPart targetPart = list[num3];
					string ordinalName = targetPart.GetOrdinalName();
					if (gameObject != parentObject)
					{
						GameObject equipped = targetPart.Equipped;
						if (equipped == null || !equipped.IsAffliction())
						{
							GameObject cybernetics = targetPart.Cybernetics;
							if (cybernetics == null || !cybernetics.IsAffliction())
							{
								GameObject defaultBehavior = targetPart.DefaultBehavior;
								if (defaultBehavior == null || !defaultBehavior.IsAffliction())
								{
									goto IL_04E5;
								}
							}
						}
						if (!targetPart.Mortal || body.GetMortalPartCount() >= 2)
						{
							goto IL_054F;
						}
						IL_04E5:
						parentObject.ShowFailure(string.Concat(new string[]
						{
							gameObject.Does("see", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" no reason for you to amputate ",
							gameObject.its,
							" ",
							ordinalName,
							"."
						}));
						return false;
					}
					if (targetPart.Mortal && body.GetMortalPartCount() < 2)
					{
						parentObject.ShowFailure("You cannot bring yourself to amputate your " + ordinalName + ".");
						return false;
					}
					BodyPart bodyPart3 = weapon.EquippedOn();
					if (bodyPart3 != null && (bodyPart3 == targetPart || targetPart.IsParentPartOf(bodyPart3)))
					{
						GameObject weapon2 = parentObject.GetWeapon(delegate(GameObject obj)
						{
							if (obj == weapon)
							{
								return false;
							}
							if (!Physic_AmputateLimb.IsUsableWeapon(obj))
							{
								return false;
							}
							BodyPart bodyPart4 = obj.EquippedOn();
							return bodyPart4 == null || (bodyPart4 != targetPart && !targetPart.IsParentPartOf(bodyPart4));
						});
						if (weapon2 == null)
						{
							parentObject.ShowFailure(string.Concat(new string[]
							{
								"You cannot amputate the ",
								targetPart.TypeModel().Name,
								" holding ",
								parentObject.poss(weapon, true, null),
								"."
							}));
							return false;
						}
						weapon = weapon2;
					}
					IL_054F:
					if (!parentObject.PhaseMatches(gameObject))
					{
						IComponent<GameObject>.WDidXToYWithZ(parentObject, "try", "to amputate", gameObject, ordinalName + " with", weapon, ", but " + weapon.does("pass", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " through " + gameObject.them, "!", null, null, null, null, false, false, false, false, false, false, true, false, null, null, null, null, false, false, false, false, false, null);
						parentObject.UseEnergy(1000, "Physical Skill Physic AmputateLimb Failure PhaseFailure", null, null, false);
					}
					else if (gameObject.IsInStasis())
					{
						IComponent<GameObject>.WDidXToYWithZ(parentObject, "try", "to amputate", gameObject, ordinalName + " with", weapon, ", but " + weapon.does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " no effect", null, null, null, null, null, false, false, false, false, false, false, true, false, null, null, null, null, false, false, false, false, false, null);
						parentObject.UseEnergy(1000, "Physical Skill Physic AmputateLimb Failure StasisFailure", null, null, false);
					}
					else
					{
						this.ParentObject.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_attack_generic_activate", null);
						body.Dismember(targetPart, null, null, false, true, null);
						if (CombatJuice.enabled && (parentObject.IsPlayer() || gameObject.IsPlayer()))
						{
							CombatJuice.cameraShake(0.25f, false);
						}
						IComponent<GameObject>.WDidXToYWithZ(parentObject, "amputate", gameObject, ordinalName + " with", weapon, null, null, null, null, null, null, false, false, false, false, false, false, true, false, null, null, null, null, false, false, false, true, false, null);
						gameObject.ApplyEffect(new Bleeding("1d2", 35, parentObject, true, false, false, false), null);
						parentObject.UseEnergy(3000, "Physical Skill Physic AmputateLimb", null, null, false);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C131 RID: 49457 RVA: 0x0046B314 File Offset: 0x00469514
		public static bool IsUsableWeapon(GameObject Object)
		{
			if (Object == null)
			{
				return false;
			}
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			return part != null && (part.Skill == "Axe" || Object.HasPart<ModSerrated>() || Object.HasPart<ModGlazed>());
		}

		// Token: 0x0600C132 RID: 49458 RVA: 0x0046B35C File Offset: 0x0046955C
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Amputate Limb", "CommandAmputateLimb", "Skills", null, "ö", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C133 RID: 49459 RVA: 0x0046B3A4 File Offset: 0x004695A4
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.AddSkill(GO);
		}

		// Token: 0x04004D6E RID: 19822
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
