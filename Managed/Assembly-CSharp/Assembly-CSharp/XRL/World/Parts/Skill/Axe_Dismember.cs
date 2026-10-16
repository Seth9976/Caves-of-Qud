using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200131F RID: 4895
	[Serializable]
	public class Axe_Dismember : BaseSkill
	{
		// Token: 0x0600BF66 RID: 48998 RVA: 0x00461B99 File Offset: 0x0045FD99
		public Axe_Dismember()
		{
		}

		// Token: 0x0600BF67 RID: 48999 RVA: 0x00461BAC File Offset: 0x0045FDAC
		public Axe_Dismember(GameObject ParentObject)
			: this()
		{
			this.ParentObject = ParentObject;
		}

		// Token: 0x0600BF68 RID: 49000 RVA: 0x00461BBC File Offset: 0x0045FDBC
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("BleedDamage", Axe_Dismember.BleedDamage + " per round", false, 0);
			stats.Set("BleedSave", "Toughness " + Axe_Dismember.BleedSave.ToString(), false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Axe_Dismember.Cooldown, 0);
		}

		// Token: 0x0600BF69 RID: 49001 RVA: 0x00461C22 File Offset: 0x0045FE22
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BF6A RID: 49002 RVA: 0x00461C38 File Offset: 0x0045FE38
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BF6B RID: 49003 RVA: 0x00461C5B File Offset: 0x0045FE5B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("CommandDismember");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BF6C RID: 49004 RVA: 0x00461C7C File Offset: 0x0045FE7C
		public static bool Dismember(GameObject Attacker, GameObject Defender, Cell Where = null, BodyPart LostPart = null, GameObject Weapon = null, GameObject Projectile = null, string Sound = "sfx_characterTrigger_dismember", bool assumeDecapitate = false, bool suppressDecapitate = false, bool weaponActing = false, bool UsePopups = false)
		{
			if (LostPart == null)
			{
				LostPart = Axe_Dismember.GetDismemberableBodyPart(Defender, Attacker, Weapon, assumeDecapitate, suppressDecapitate);
				if (LostPart == null)
				{
					return false;
				}
			}
			if (Where != null && (!GameObject.Validate(ref Defender) || Defender.CurrentCell != Where) && Where.IsOccluding())
			{
				Where = Where.GetFirstNonOccludingAdjacentCell(true) ?? Where;
			}
			if (Attacker != null && Attacker.IsPlayer() && Options.DismemberAsPopup)
			{
				UsePopups = true;
			}
			if (LostPart.SeverRequiresDecapitate())
			{
				return Axe_Decapitate.Decapitate(Attacker, Defender, Where, LostPart, Weapon, Projectile, weaponActing, UsePopups);
			}
			string ordinalName = LostPart.GetOrdinalName();
			if (Defender.Body.Dismember(LostPart, Attacker, Where, false, UsePopups, null) == null)
			{
				return false;
			}
			if (weaponActing && Weapon != null && Attacker != null)
			{
				IComponent<GameObject>.XDidYToZ(Weapon, "chop", "off", Defender, ordinalName, "!", null, null, null, Defender, false, false, false, false, true, Attacker, null, null, false, false, false, false, UsePopups, null);
			}
			else
			{
				IComponent<GameObject>.XDidYToZ(Attacker, "chop", "off", Defender, ordinalName, "!", null, null, null, Defender, false, false, false, false, true, null, null, null, false, false, false, false, UsePopups, null);
			}
			Defender.PlayWorldSound(Sound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			Defender.ParticleText("*dismembered!*", IComponent<GameObject>.ConsequentialColorChar(null, Defender), false, 1.5f, -8f);
			Defender.ApplyEffect(new Bleeding(Axe_Dismember.BleedDamage, Axe_Dismember.BleedSave, Attacker, true, false, UsePopups, false), null);
			Defender.RemoveAllEffects<Hooked>(true);
			if (((Attacker != null && Attacker.IsPlayer()) || (Defender != null && Defender.IsPlayer())) && CombatJuice.enabled)
			{
				CombatJuice.cameraShake(0.25f, false);
			}
			return true;
		}

		// Token: 0x0600BF6D RID: 49005 RVA: 0x00461E0D File Offset: 0x0046000D
		public GameObject GetPrimaryAxe()
		{
			return this.ParentObject.GetPrimaryWeaponOfType("Axe");
		}

		// Token: 0x0600BF6E RID: 49006 RVA: 0x00461E1F File Offset: 0x0046001F
		public bool IsPrimaryAxeEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("Axe");
		}

		// Token: 0x0600BF6F RID: 49007 RVA: 0x00461E31 File Offset: 0x00460031
		public static bool BodyPartIsDismemberable(BodyPart Part, GameObject Actor = null, bool assumeDecapitate = false, bool suppressDecapitate = false)
		{
			if (!Part.IsSeverable())
			{
				return false;
			}
			if (Part.SeverRequiresDecapitate())
			{
				if (suppressDecapitate)
				{
					return false;
				}
				if (!assumeDecapitate && !Axe_Decapitate.ShouldDecapitate(Actor))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600BF70 RID: 49008 RVA: 0x00461E58 File Offset: 0x00460058
		public static BodyPart GetDismemberableBodyPart(GameObject obj, GameObject Actor = null, GameObject Weapon = null, bool assumeDecapitate = false, bool suppressDecapitate = false)
		{
			Body body = obj.Body;
			if (body == null)
			{
				return null;
			}
			if (!obj.CanBeDismembered(Weapon))
			{
				return null;
			}
			Axe_Dismember.DismemberableBodyParts.Clear();
			foreach (BodyPart bodyPart in body.GetParts())
			{
				if (Axe_Dismember.BodyPartIsDismemberable(bodyPart, Actor, assumeDecapitate, suppressDecapitate))
				{
					Axe_Dismember.DismemberableBodyParts.Add(bodyPart);
				}
			}
			return Axe_Dismember.DismemberableBodyParts.GetRandomElement<BodyPart>(null);
		}

		// Token: 0x0600BF71 RID: 49009 RVA: 0x00461EE8 File Offset: 0x004600E8
		public static bool HasAnyDismemberableBodyPart(GameObject obj, GameObject Actor = null, GameObject Weapon = null, bool assumeDecapitate = false, bool suppressDecapitate = false)
		{
			Body body = obj.Body;
			if (body == null)
			{
				return false;
			}
			if (!obj.CanBeDismembered(Weapon))
			{
				return false;
			}
			using (List<BodyPart>.Enumerator enumerator = body.GetParts().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Axe_Dismember.BodyPartIsDismemberable(enumerator.Current, Actor, assumeDecapitate, suppressDecapitate))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600BF72 RID: 49010 RVA: 0x00461F5C File Offset: 0x0046015C
		public static bool CastForceSuccess(GameObject attacker, Axe_Dismember skill = null, GameObject weapon = null)
		{
			if (skill == null)
			{
				skill = new Axe_Dismember(attacker);
			}
			if (weapon == null)
			{
				weapon = skill.GetPrimaryAxe();
			}
			Cell cell = skill.PickDirection("Dismember", null);
			if (cell != null)
			{
				GameObject combatTarget = cell.GetCombatTarget(attacker, false, false, false, 0, null, null, null, null, null, true, false, null);
				if (combatTarget == attacker && attacker.IsPlayer() && Popup.ShowYesNo("Are you sure you want to dismember " + attacker.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return true;
				}
				if (combatTarget != null)
				{
					Axe_Dismember.Dismember(attacker, combatTarget, null, null, weapon, null, "sfx_ability_dismember", false, false, false, false);
				}
			}
			return true;
		}

		// Token: 0x0600BF73 RID: 49011 RVA: 0x00461FF0 File Offset: 0x004601F0
		public static bool Cast(GameObject attacker, Axe_Dismember skill = null, GameObject weapon = null)
		{
			bool flag = false;
			if (skill == null)
			{
				skill = new Axe_Dismember(attacker);
				attacker.RegisterPartEvent(skill, "AttackerAfterDamage");
				flag = true;
			}
			if (weapon == null)
			{
				weapon = skill.GetPrimaryAxe();
			}
			if (attacker.CanMoveExtremities("Dismember", true, false, false) && attacker.CanChangeBodyPosition("Dismember", true, false, false, false))
			{
				Cell cell = skill.PickDirection("Dismember", null);
				if (cell != null)
				{
					GameObject combatTarget = cell.GetCombatTarget(attacker, false, false, false, 0, null, null, null, null, null, true, false, null);
					if (combatTarget == null)
					{
						if (attacker.IsPlayer())
						{
							if (cell.HasObjectWithPart("Combat"))
							{
								Popup.Show("There's nothing there you can dismember.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else
							{
								Popup.Show("There's nothing there to dismember.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
					}
					else
					{
						try
						{
							skill.bDismembering = true;
							if (combatTarget == attacker && attacker.IsPlayer() && Popup.ShowYesNo("Are you sure you want to dismember " + attacker.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
							{
								return true;
							}
							Combat.MeleeAttackWithWeapon(attacker, combatTarget, weapon, attacker.Body.FindDefaultOrEquippedItem(weapon), "Dismembering", 0, 0, 0, 0, 0, weapon.IsEquippedOrDefaultOfPrimary(attacker), true);
							attacker.UseEnergy(1000, "Skill Axe Dismember", null, null, false);
							if (!combatTarget.IsHostileTowards(attacker))
							{
								combatTarget.AddOpinion<OpinionAttack>(attacker, weapon, 1f);
							}
							skill.CooldownMyActivatedAbility(skill.ActivatedAbilityID, Axe_Dismember.Cooldown, null, null, false);
						}
						catch (Exception ex)
						{
							XRLCore.LogError("Dismember", ex);
						}
						finally
						{
							skill.bDismembering = false;
						}
					}
				}
			}
			if (flag)
			{
				attacker.UnregisterPartEvent(skill, "AttackerAfterDamage");
			}
			return true;
		}

		// Token: 0x0600BF74 RID: 49012 RVA: 0x004621B8 File Offset: 0x004603B8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerAfterDamage")
			{
				if (E.GetIntParameter("Penetrations", 0) > 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Weapon");
					GameObject gameObjectParameter3 = E.GetGameObjectParameter("Defender");
					if (GameObject.Validate(ref gameObjectParameter2))
					{
						MeleeWeapon part = gameObjectParameter2.GetPart<MeleeWeapon>();
						if (part != null && part.Skill == "Axe")
						{
							Cell cell = E.GetParameter("Cell") as Cell;
							if (this.bDismembering || gameObjectParameter.HasEffect<Berserk>())
							{
								return Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter3, cell, null, gameObjectParameter2, null, "sfx_ability_dismember", false, false, false, false);
							}
							int num = 3;
							string stringParameter = E.GetStringParameter("Properties", null);
							if (stringParameter != null && stringParameter.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) && gameObjectParameter.HasSkill("Cudgel_ChargingStrike"))
							{
								num *= 2;
							}
							if (gameObjectParameter2.UsesTwoSlots)
							{
								num *= 2;
							}
							GameObject gameObject = gameObjectParameter;
							GameObject gameObject2 = gameObjectParameter2;
							string text = "Skill Dismember";
							GameObject gameObject3 = gameObjectParameter3;
							num = GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, num, gameObject3, null, true, false);
							if (num.in100())
							{
								return Axe_Dismember.Dismember(gameObjectParameter, gameObjectParameter3, null, null, null, null, "sfx_ability_dismember", false, false, false, false);
							}
						}
					}
				}
			}
			else if (E.ID == "CommandDismember")
			{
				GameObject primaryAxe = this.GetPrimaryAxe();
				if (primaryAxe == null)
				{
					return this.ParentObject.Fail("You must have an axe equipped in your primary hand to dismember.");
				}
				if (!this.ParentObject.CheckFrozen(false, false, false, null))
				{
					return false;
				}
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				return Axe_Dismember.Cast(this.ParentObject, this, primaryAxe);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BF75 RID: 49013 RVA: 0x0046236C File Offset: 0x0046056C
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Dismember", "CommandDismember", "Skills", "You make an attack with your axe at an adjacent opponent. If you hit and penetrate at least once, you dismember one of their limbs and they start bleeding (1-2 damage per turn. toughness save; difficulty 35). Additionally, your axe attacks that penetrate have a percentage chance to dismember: 3% for one-handed axes and 6% for two-handed axes.", "-", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600BF76 RID: 49014 RVA: 0x004623B2 File Offset: 0x004605B2
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D18 RID: 19736
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D19 RID: 19737
		public bool bDismembering;

		// Token: 0x04004D1A RID: 19738
		public static readonly int Cooldown = 30;

		// Token: 0x04004D1B RID: 19739
		public static readonly string BleedDamage = "1d2";

		// Token: 0x04004D1C RID: 19740
		public static readonly int BleedSave = 35;

		// Token: 0x04004D1D RID: 19741
		[NonSerialized]
		private static List<BodyPart> DismemberableBodyParts = new List<BodyPart>(8);
	}
}
