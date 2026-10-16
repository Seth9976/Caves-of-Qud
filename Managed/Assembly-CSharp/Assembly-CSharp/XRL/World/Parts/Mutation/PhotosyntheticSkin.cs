using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001451 RID: 5201
	[Serializable]
	public class PhotosyntheticSkin : BaseMutation
	{
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600CB4C RID: 52044 RVA: 0x0049EACE File Offset: 0x0049CCCE
		public ActivatedAbilityEntry Ability
		{
			get
			{
				if (this._Ability == null || this._Ability.ID != this.BaskActivatedAbilityID)
				{
					this._Ability = base.MyActivatedAbility(this.BaskActivatedAbilityID, null);
				}
				return this._Ability;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600CB4D RID: 52045 RVA: 0x0049EB09 File Offset: 0x0049CD09
		public bool HasSunlight
		{
			get
			{
				return this.ParentObject.IsUnderSky() && base.IsDay();
			}
		}

		// Token: 0x0600CB4F RID: 52047 RVA: 0x0049EB40 File Offset: 0x0049CD40
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("DurationDay", PhotosyntheticSkin.GetBonusDurationString(Level), !stats.mode.Contains("ability"), 0);
			stats.Set("BonusRegen", PhotosyntheticSkin.GetBonusRegeneration(Level).ToString() + "%", !stats.mode.Contains("ability"), 0);
			stats.Set("BonusQuickness", PhotosyntheticSkin.GetBonusQuickness(Level), !stats.mode.Contains("ability"), 0);
		}

		// Token: 0x0600CB50 RID: 52048 RVA: 0x0049EBCE File Offset: 0x0049CDCE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == AIGetPassiveAbilityListEvent.ID;
		}

		// Token: 0x0600CB51 RID: 52049 RVA: 0x0049EC04 File Offset: 0x0049CE04
		public override bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			if (this.Ability.Enabled && !this.ParentObject.HasEffectDescendedFrom<ProceduralCookingEffect>())
			{
				E.Add(PhotosyntheticSkin.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB52 RID: 52050 RVA: 0x0049EC38 File Offset: 0x0049CE38
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.BaskActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB53 RID: 52051 RVA: 0x0049EC5C File Offset: 0x0049CE5C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckCamouflage();
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB54 RID: 52052 RVA: 0x0049EC6C File Offset: 0x0049CE6C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.HasSunlight)
			{
				this.Ability.Enabled = true;
				if (!this.ParentObject.IsTemporary)
				{
					this.SoakCounter++;
					if (this.SoakCounter > 300)
					{
						this.SoakCounter = 0;
						int num = (base.Level - 1) / 4 + 1;
						Inventory inventory = this.ParentObject.Inventory;
						if (inventory != null)
						{
							if (inventory.Count("Starch") < num)
							{
								inventory.AddObject("Starch", true, false);
							}
							if (inventory.Count("Lignin") < num)
							{
								inventory.AddObject("Lignin", true, false);
							}
						}
					}
				}
			}
			else
			{
				this.Ability.Enabled = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB55 RID: 52053 RVA: 0x0049ED2C File Offset: 0x0049CF2C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == PhotosyntheticSkin.COMMAND_NAME)
			{
				if (this.ParentObject.AreHostilesNearby())
				{
					return this.ParentObject.Fail("You can't bask with hostiles nearby.");
				}
				if (!this.ParentObject.CanMoveExtremities("Bask", true, false, false))
				{
					return false;
				}
				if (!this.HasSunlight)
				{
					return this.ParentObject.Fail("You need sunlight to bask in.");
				}
				ProceduralCookingEffect proceduralCookingEffect = ProceduralCookingEffect.CreateSpecific(new List<string> { "CookingDomainPhotosyntheticSkin_RegenerationUnit", "CookingDomainPhotosyntheticSkin_UnitQuickness", "CookingDomainPhotosyntheticSkin_SatedUnit" }, null, null);
				this.ParentObject.FireEvent("ClearFoodEffects");
				this.ParentObject.CleanEffects();
				proceduralCookingEffect.Init(this.ParentObject);
				proceduralCookingEffect.Duration = 1200 * PhotosyntheticSkin.GetBonusDuration(base.Level);
				this.ParentObject.ApplyEffect(proceduralCookingEffect, null);
				Stomach part = this.ParentObject.GetPart<Stomach>();
				if (part != null)
				{
					part.ClearHunger();
				}
				this.ParentObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				"=subject.T= =verb:bask= in the sunlight and =verb:absorb= the nourishing rays.".StartReplace().AddObject(this.ParentObject, null).EmitMessage(' ', true, false, false, null, null);
				if (this.ParentObject.IsPlayer())
				{
					Popup.Show("You start to metabolize the meal, gaining the following effect for the rest of the day:\n\n{{W|" + proceduralCookingEffect.GetDetails() + "}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB56 RID: 52054 RVA: 0x0049EEB8 File Offset: 0x0049D0B8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CB57 RID: 52055 RVA: 0x0049EEBC File Offset: 0x0049D0BC
		private void CheckCamouflage()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			if (currentCell.HasObjectWithPartOtherThan(typeof(PlantProperties), this.ParentObject))
			{
				base.StatShifter.DefaultDisplayName = "camouflage";
				base.StatShifter.SetStatShift("DV", PhotosyntheticSkin.GetBonusCamouflage(base.Level), false);
				return;
			}
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x0600CB58 RID: 52056 RVA: 0x0049EF2A File Offset: 0x0049D12A
		public override string GetDescription()
		{
			return "You replenish yourself by absorbing sunlight through your hearty green skin.";
		}

		// Token: 0x0600CB59 RID: 52057 RVA: 0x0049EF34 File Offset: 0x0049D134
		public override string GetLevelText(int Level)
		{
			string text = "";
			text = string.Concat(new string[]
			{
				text,
				"You can bask in the sunlight instead of eating a meal to gain a special metabolizing effect for {{rules|",
				PhotosyntheticSkin.GetBonusDurationString(Level),
				"}}: +{{rules|",
				PhotosyntheticSkin.GetBonusRegeneration(Level).ToString(),
				"%}} to natural healing rate and +{{rules|",
				PhotosyntheticSkin.GetBonusQuickness(Level).ToString(),
				"}} Quickness\n"
			});
			text = text + "While in the sunlight, you accrue starch and lignin that you can use as ingredients in meals you cook (max {{rules|" + PhotosyntheticSkin.GetStarchServings(Level) + "}} of each).\n";
			text = text + "+{{rules|" + PhotosyntheticSkin.GetBonusCamouflage(Level).ToString() + "}} DV while occupying the same space as foliage\n";
			return text + "+200 reputation with {{w|roots}}, {{w|trees}}, {{w|vines}}, and {{w|the Consortium of Phyta}}";
		}

		// Token: 0x0600CB5A RID: 52058 RVA: 0x0049EFE5 File Offset: 0x0049D1E5
		public static int GetBonusCamouflage(int Level)
		{
			return Math.Min((Level - 1) / 4 + 1, 6);
		}

		// Token: 0x0600CB5B RID: 52059 RVA: 0x0049EFF4 File Offset: 0x0049D1F4
		public static int GetBonusRegeneration(int Level)
		{
			return 20 + Level * 10;
		}

		// Token: 0x0600CB5C RID: 52060 RVA: 0x0049EFFD File Offset: 0x0049D1FD
		public static int GetBonusQuickness(int Level)
		{
			return 13 + Level * 2;
		}

		// Token: 0x0600CB5D RID: 52061 RVA: 0x0049F005 File Offset: 0x0049D205
		public static string GetStarchServings(int Level)
		{
			return ((Level - 1) / 4 + 1).Things("serving", null);
		}

		// Token: 0x0600CB5E RID: 52062 RVA: 0x0049F019 File Offset: 0x0049D219
		public static int GetBonusDuration(int Level)
		{
			return (Level - 1) / 4 + 1;
		}

		// Token: 0x0600CB5F RID: 52063 RVA: 0x0049F024 File Offset: 0x0049D224
		public static string GetBonusDurationString(int Level)
		{
			int bonusDuration = PhotosyntheticSkin.GetBonusDuration(Level);
			if (bonusDuration != 1)
			{
				return bonusDuration.ToString() + " days";
			}
			return bonusDuration.ToString() + " day";
		}

		// Token: 0x0600CB60 RID: 52064 RVA: 0x0049F060 File Offset: 0x0049D260
		public override bool Render(RenderEvent E)
		{
			bool flag = true;
			if (this.ParentObject.IsPlayerControlled())
			{
				if ((XRLCore.FrameTimer.ElapsedMilliseconds & 127L) == 0L)
				{
					this.MutationColor = Options.MutationColor;
				}
				if (!this.MutationColor)
				{
					flag = false;
				}
			}
			if (flag)
			{
				E.ApplyColors("&g", PhotosyntheticSkin.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x0600CB61 RID: 52065 RVA: 0x0049F0BB File Offset: 0x0049D2BB
		public override bool ChangeLevel(int NewLevel)
		{
			this.CheckCamouflage();
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB62 RID: 52066 RVA: 0x0049F0CC File Offset: 0x0049D2CC
		public override bool Mutate(GameObject GO, int Level)
		{
			this.BaskActivatedAbilityID = base.AddMyActivatedAbility("Bask", PhotosyntheticSkin.COMMAND_NAME, "Physical Mutations", null, "\u000f", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.OldBleedLiquid = this.ParentObject.GetStringProperty("BleedLiquid", null);
			this.OldBleedPrefix = this.ParentObject.GetStringProperty("BleedPrefix", null);
			this.OldBleedColor = this.ParentObject.GetStringProperty("BleedColor", null);
			this.ParentObject.SetStringProperty("BleedLiquid", "blood-500,sap-500", false);
			this.ParentObject.SetStringProperty("BleedPrefix", "{{r|bloody}} and {{Y|sugary}}", false);
			this.ParentObject.SetStringProperty("BleedColor", "&r", false);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB63 RID: 52067 RVA: 0x0049F19C File Offset: 0x0049D39C
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.BaskActivatedAbilityID, null);
			this.ParentObject.SetStringProperty("BleedLiquid", this.OldBleedLiquid, true);
			this.ParentObject.SetStringProperty("BleedPrefix", this.OldBleedPrefix, true);
			this.ParentObject.SetStringProperty("BleedColor", this.OldBleedColor, true);
			base.StatShifter.RemoveStatShifts();
			return base.Unmutate(GO);
		}

		// Token: 0x04004F0D RID: 20237
		public static readonly string COMMAND_NAME = "CommandBask";

		// Token: 0x04004F0E RID: 20238
		public static readonly int ICON_COLOR_PRIORITY = 80;

		// Token: 0x04004F0F RID: 20239
		public Guid BaskActivatedAbilityID = Guid.Empty;

		// Token: 0x04004F10 RID: 20240
		public string OldBleedLiquid;

		// Token: 0x04004F11 RID: 20241
		public string OldBleedColor;

		// Token: 0x04004F12 RID: 20242
		public string OldBleedPrefix;

		// Token: 0x04004F13 RID: 20243
		public int SoakCounter;

		// Token: 0x04004F14 RID: 20244
		[NonSerialized]
		public ActivatedAbilityEntry _Ability;

		// Token: 0x04004F15 RID: 20245
		private bool MutationColor = Options.MutationColor;
	}
}
