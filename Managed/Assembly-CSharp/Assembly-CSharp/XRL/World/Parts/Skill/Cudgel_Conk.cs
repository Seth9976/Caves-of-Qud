using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001330 RID: 4912
	[Serializable]
	public class Cudgel_Conk : BaseSkill
	{
		// Token: 0x0600BFF5 RID: 49141 RVA: 0x00465003 File Offset: 0x00463203
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), Cudgel_Conk.COOLDOWN, 0);
		}

		// Token: 0x0600BFF6 RID: 49142 RVA: 0x0046501E File Offset: 0x0046321E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600BFF7 RID: 49143 RVA: 0x00465044 File Offset: 0x00463244
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFF8 RID: 49144 RVA: 0x00465068 File Offset: 0x00463268
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && GameObject.Validate(E.Target) && this.IsPrimaryCudgelEquipped() && E.Actor.CanMoveExtremities(null, false, false, false) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add(Cudgel_Conk.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BFF9 RID: 49145 RVA: 0x004650CB File Offset: 0x004632CB
		public override bool HandleEvent(CommandEvent E)
		{
			return (!(E.Command == Cudgel_Conk.COMMAND_NAME) || this.PerformConk()) && base.HandleEvent(E);
		}

		// Token: 0x0600BFFA RID: 49146 RVA: 0x004650F0 File Offset: 0x004632F0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BFFB RID: 49147 RVA: 0x004650F3 File Offset: 0x004632F3
		public GameObject GetPrimaryCudgel()
		{
			return this.ParentObject.GetPrimaryWeaponOfType("Cudgel");
		}

		// Token: 0x0600BFFC RID: 49148 RVA: 0x00465105 File Offset: 0x00463305
		public bool IsPrimaryCudgelEquipped()
		{
			return this.ParentObject.HasPrimaryWeaponOfType("Cudgel");
		}

		// Token: 0x0600BFFD RID: 49149 RVA: 0x00465118 File Offset: 0x00463318
		public string GetConkLocation(GameObject Object)
		{
			Body body = Object.Body;
			if (body == null)
			{
				return null;
			}
			int partCount = body.GetPartCount("Head");
			if (partCount <= 0)
			{
				return null;
			}
			if (partCount == 1)
			{
				return "the " + body.GetFirstPart("Head").Name;
			}
			List<string> list = new List<string>();
			foreach (BodyPart bodyPart in body.GetPart("Head"))
			{
				string text = Grammar.Pluralize(bodyPart.VariantTypeModel().Name);
				if (!list.Contains(text))
				{
					list.Add(text);
				}
			}
			return "one of " + Object.its + " " + Grammar.MakeOrList(list, true);
		}

		// Token: 0x0600BFFE RID: 49150 RVA: 0x004651EC File Offset: 0x004633EC
		public bool PerformConk()
		{
			if (!this.IsPrimaryCudgelEquipped())
			{
				this.ParentObject.Fail("You must have a cudgel equipped in your primary hand to conk.");
				return false;
			}
			if (!this.ParentObject.CanMoveExtremities("Conk", true, false, false))
			{
				return false;
			}
			Cell cell = base.PickDirection("Conk", null);
			if (cell == null)
			{
				return false;
			}
			GameObject combatTarget = cell.GetCombatTarget(this.ParentObject, false, false, false, 0, null, null, null, null, null, true, false, null);
			if (combatTarget == null)
			{
				this.ParentObject.Fail("There's nothing there you can conk.");
				return false;
			}
			bool flag;
			try
			{
				string conkLocation = this.GetConkLocation(combatTarget);
				if (conkLocation == null)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail(combatTarget.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " have anything like a head to conk.", true, true, true);
					}
					flag = false;
				}
				else if (combatTarget == this.ParentObject && this.ParentObject.IsPlayer() && Popup.ShowYesNo(string.Concat(new string[] { "Are you sure you want to conk ", combatTarget.itself, " on ", conkLocation, "?" }), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					flag = false;
				}
				else
				{
					base.DidXToY("attempt", "to conk", combatTarget, "on " + conkLocation, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					GameObject primaryCudgel = this.GetPrimaryCudgel();
					if (Combat.MeleeAttackWithWeapon(this.ParentObject, combatTarget, primaryCudgel, this.ParentObject.Body.FindDefaultOrEquippedItem(primaryCudgel), "Conking", 0, 0, 0, 0, 0, true, true).Hits > 0)
					{
						base.PlayWorldSound("sfx_ability_conk", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					}
					this.ParentObject.UseEnergy(1000, "Skill Cudgel Conk", null, null, false);
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, Cudgel_Conk.COOLDOWN, null, null, false);
					flag = true;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Conk", ex, "game_exception");
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600BFFF RID: 49151 RVA: 0x00465430 File Offset: 0x00463630
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Conk", Cudgel_Conk.COMMAND_NAME, "Skills", null, "-", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600C000 RID: 49152 RVA: 0x00465472 File Offset: 0x00463672
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return true;
		}

		// Token: 0x04004D3D RID: 19773
		public static readonly string COMMAND_NAME = "CommandConk";

		// Token: 0x04004D3E RID: 19774
		public static readonly int COOLDOWN = 10;

		// Token: 0x04004D3F RID: 19775
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
