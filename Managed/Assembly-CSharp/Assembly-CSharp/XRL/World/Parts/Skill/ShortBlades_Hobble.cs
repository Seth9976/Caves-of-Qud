using System;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200138A RID: 5002
	[Serializable]
	public class ShortBlades_Hobble : BaseSkill
	{
		// Token: 0x0600C1E4 RID: 49636 RVA: 0x0046D86F File Offset: 0x0046BA6F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C1E5 RID: 49637 RVA: 0x0046D88D File Offset: 0x0046BA8D
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("HobbleEffect", "-50% move speed", false, 0);
			stats.Set("HobbleDuration", "16-20", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), ShortBlades_Hobble.Cooldown, 0);
		}

		// Token: 0x0600C1E6 RID: 49638 RVA: 0x0046D8CC File Offset: 0x0046BACC
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1E7 RID: 49639 RVA: 0x0046D8F0 File Offset: 0x0046BAF0
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && E.Actor.CanMoveExtremities("Hobble", false, false, false) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandHobble", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C1E8 RID: 49640 RVA: 0x0046D942 File Offset: 0x0046BB42
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("CommandHobble");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C1E9 RID: 49641 RVA: 0x0046D962 File Offset: 0x0046BB62
		public GameObject GetPrimaryShortblade()
		{
			return this.ParentObject.GetPrimaryWeaponOfType("ShortBlades");
		}

		// Token: 0x0600C1EA RID: 49642 RVA: 0x0046D974 File Offset: 0x0046BB74
		public bool IsShortbladeEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("ShortBlades");
		}

		// Token: 0x0600C1EB RID: 49643 RVA: 0x0046D988 File Offset: 0x0046BB88
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerAfterDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				bool gameObjectParameter2 = E.GetGameObjectParameter("Weapon") != null;
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter2 && this.Hobbling)
				{
					if (gameObjectParameter.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You find a weakness in " + Grammar.MakePossessive(gameObjectParameter3.the + gameObjectParameter3.ShortDisplayName) + " defenses.", 'g', true);
					}
					else if (gameObjectParameter3.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(gameObjectParameter.The + gameObjectParameter.ShortDisplayName + gameObjectParameter.GetVerb("find", true, false, false, true, false) + " a weakness in your defenses.", 'r', true);
					}
					gameObjectParameter3.ApplyEffect(new Hobbled(Stat.Random(16, 20)), null);
				}
			}
			else if (E.ID == "CommandHobble")
			{
				if (!this.IsShortbladeEquipped())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You must have a short blade equipped in your primary hand to hobble.", true, true, true);
					}
					return false;
				}
				if (!this.ParentObject.CanMoveExtremities("Hobble", true, false, false))
				{
					return false;
				}
				Cell cell = base.PickDirection("Hobble", null);
				if (cell == null)
				{
					return false;
				}
				GameObject combatTarget = cell.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
				if (combatTarget == null)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("There's nothing there to hobble.", true, true, true);
					}
					return false;
				}
				try
				{
					this.Hobbling = true;
					if (combatTarget == this.ParentObject && this.ParentObject.IsPlayer() && Popup.ShowYesNo("Are you sure you want to hobble " + combatTarget.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						return false;
					}
					this.ParentObject.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_attack_generic_activate", null);
					base.DidXToY("attempt", "to hobble", combatTarget, null, null, null, null, null, combatTarget, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					GameObject primaryShortblade = this.GetPrimaryShortblade();
					Combat.MeleeAttackWithWeapon(this.ParentObject, combatTarget, primaryShortblade, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryShortblade), "Autopen,Maxpen1", 0, 0, 0, 0, 0, true, true);
					int num = 1000;
					if (this.IsShortbladeEquipped() && this.ParentObject.HasSkill("ShortBlades_Expertise"))
					{
						num = (int)(0.75 * (double)num);
					}
					this.ParentObject.UseEnergy(num);
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, ShortBlades_Hobble.Cooldown, null, "Agility", false);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Hobble", ex, "game_exception");
				}
				finally
				{
					this.Hobbling = false;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C1EC RID: 49644 RVA: 0x0046DC74 File Offset: 0x0046BE74
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Hobble", "CommandHobble", "Skills", null, "-", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C1ED RID: 49645 RVA: 0x0046DCB6 File Offset: 0x0046BEB6
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D86 RID: 19846
		public bool Hobbling;

		// Token: 0x04004D87 RID: 19847
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D88 RID: 19848
		public static readonly int Cooldown = 30;
	}
}
