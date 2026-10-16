using System;
using XRL.Collections;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200138F RID: 5007
	[Serializable]
	public class ShortBlades_Shank : BaseSkill
	{
		// Token: 0x0600C1FF RID: 49663 RVA: 0x0046E187 File Offset: 0x0046C387
		public GameObject GetShortblade()
		{
			return this.ParentObject.GetWeaponOfType("ShortBlades", true);
		}

		// Token: 0x0600C200 RID: 49664 RVA: 0x0046E19A File Offset: 0x0046C39A
		public bool IsShortbladeEquipped()
		{
			return this.ParentObject.HasWeaponOfType("ShortBlades", false);
		}

		// Token: 0x0600C201 RID: 49665 RVA: 0x0046E1AD File Offset: 0x0046C3AD
		public bool IsPrimaryShortbladeEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("ShortBlades");
		}

		// Token: 0x0600C202 RID: 49666 RVA: 0x0046E1C0 File Offset: 0x0046C3C0
		public static int GetShankEffectCount(GameObject obj)
		{
			int num = 0;
			using (Container<Effect>.Enumerator enumerator = obj.Effects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsOfType(33554432))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600C203 RID: 49667 RVA: 0x0046E220 File Offset: 0x0046C420
		public static bool Cast(GameObject ParentObject, ShortBlades_Shank skill = null, GameObject weapon = null)
		{
			if (skill == null)
			{
				skill = new ShortBlades_Shank();
				skill.ParentObject = ParentObject;
			}
			if (weapon == null)
			{
				weapon = skill.GetShortblade() ?? ParentObject.GetPrimaryWeapon();
				if (weapon == null)
				{
					return false;
				}
			}
			Cell cell = skill.PickDirection(true, "Shank", ParentObject);
			if (cell == null)
			{
				return false;
			}
			GameObject combatTarget = cell.GetCombatTarget(ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
			if (combatTarget == null)
			{
				if (ParentObject.IsPlayer())
				{
					if (cell.HasObjectWithPart("Combat"))
					{
						Popup.Show("There's nothing there you can shank.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("There's nothing there to shank.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				return false;
			}
			try
			{
				skill.Shanking = true;
				if (combatTarget == ParentObject && ParentObject.IsPlayer() && Popup.ShowYesNo("Are you sure you want to shank " + combatTarget.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return false;
				}
				ParentObject.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_skill_attack_generic_activate", null);
				int num = ShortBlades_Shank.GetShankEffectCount(combatTarget) * 2;
				if (IComponent<GameObject>.Visible(ParentObject))
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						ParentObject.Does("attempt", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" to take advantage of ",
						combatTarget.poss("misfortune", true),
						" and shank ",
						combatTarget.them,
						"."
					}), null, true);
				}
				if (num > 0)
				{
					combatTarget.Bloodsplatter();
				}
				int num2 = 0;
				while (num2 < num && num2 <= 12)
				{
					combatTarget.BloodsplatterBurst(true, Stat.Random(0, 359).toRadians(), 45);
					num2 += 4;
				}
				if (num > 12)
				{
					combatTarget.DustPuff("&y", 15);
				}
				Combat.MeleeAttackWithWeapon(ParentObject, combatTarget, weapon, ParentObject.Body.FindDefaultOrEquippedItem(weapon), "Shanking", 0, num, num, 0, 0, weapon.IsEquippedOrDefaultOfPrimary(ParentObject), true);
				int num3 = 1000;
				if (skill.IsPrimaryShortbladeEquipped() && ParentObject.HasSkill("ShortBlades_Expertise"))
				{
					num3 = (int)(0.75 * (double)num3);
				}
				ParentObject.UseEnergy(num3);
				if (skill.MyActivatedAbility(skill.ActivatedAbilityID, null) != null)
				{
					if (skill.IsPrimaryShortbladeEquipped())
					{
						skill.CooldownMyActivatedAbility(skill.ActivatedAbilityID, ShortBlades_Shank.PrimaryCooldown, null, "Agility", false);
					}
					else
					{
						skill.CooldownMyActivatedAbility(skill.ActivatedAbilityID, ShortBlades_Shank.OffhandCooldown, null, "Agility", false);
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Shank", ex);
			}
			finally
			{
				skill.Shanking = false;
			}
			return true;
		}

		// Token: 0x0600C204 RID: 49668 RVA: 0x0046E4DC File Offset: 0x0046C6DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600C205 RID: 49669 RVA: 0x0046E4FA File Offset: 0x0046C6FA
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("PrimaryHandCooldown", 10, false, 0);
			stats.Set("OffhandCooldown", 20, false, 0);
		}

		// Token: 0x0600C206 RID: 49670 RVA: 0x0046E51A File Offset: 0x0046C71A
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C207 RID: 49671 RVA: 0x0046E540 File Offset: 0x0046C740
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && GameObject.Validate(E.Target) && this.IsShortbladeEquipped() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.CanMoveExtremities("Shank", false, false, false) && Stat.Random(0, 4) < ShortBlades_Shank.GetShankEffectCount(E.Target))
			{
				E.Add("CommandShank", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C208 RID: 49672 RVA: 0x0046E5BB File Offset: 0x0046C7BB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandShank");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C209 RID: 49673 RVA: 0x0046E5D0 File Offset: 0x0046C7D0
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "CommandShank"))
			{
				return base.FireEvent(E);
			}
			GameObject shortblade = this.GetShortblade();
			if (shortblade == null)
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.Show("You must have a short blade equipped to shank.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			return this.ParentObject.CanMoveExtremities("Shank", true, false, false) && ShortBlades_Shank.Cast(this.ParentObject, this, shortblade);
		}

		// Token: 0x0600C20A RID: 49674 RVA: 0x0046E64C File Offset: 0x0046C84C
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Shank", "CommandShank", "Skills", "You make a melee attack with a short blade in your primary hand (preferred) or offhand. If you hit, the attack gets +2 to penetration rolls for each negative status effect your opponent suffers from.", "-", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C20B RID: 49675 RVA: 0x0046E692 File Offset: 0x0046C892
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D8B RID: 19851
		public bool Shanking;

		// Token: 0x04004D8C RID: 19852
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004D8D RID: 19853
		public static readonly int PrimaryCooldown = 10;

		// Token: 0x04004D8E RID: 19854
		public static readonly int OffhandCooldown = 20;
	}
}
