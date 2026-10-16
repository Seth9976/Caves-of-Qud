using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Skills;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008B5 RID: 2229
	[Serializable]
	public class PsychicCombatSifrah : PsionicSifrah
	{
		// Token: 0x06005710 RID: 22288 RVA: 0x001A3ACC File Offset: 0x001A1CCC
		public PsychicCombatSifrah(GameObject ContextObject, string Subtype, int Rating, int Difficulty, string Description = null)
		{
			string text = Description;
			bool? flag12;
			if (Description == null)
			{
				string text2 = "engaging in psychic combat with ";
				int maxValue = int.MaxValue;
				string text3 = null;
				string text4 = null;
				bool flag = false;
				bool flag2 = true;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = true;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = true;
				bool flag11 = false;
				flag12 = null;
				text = text2 + ContextObject.an(maxValue, text3, text4, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, false, null, false);
			}
			this.Description = text;
			string primaryFaction = ContextObject.GetPrimaryFaction(false);
			int num = ((!primaryFaction.IsNullOrEmpty()) ? The.Game.PlayerReputation.GetLevel(primaryFaction) : 0);
			this.MaxTurns = 3;
			bool flag13 = false;
			bool flag14 = false;
			GetPsionicSifrahSetupEvent.GetFor(The.Player, ContextObject, "PsychicCombat", Subtype, false, ref Difficulty, ref Rating, ref this.MaxTurns, ref flag13, ref flag14);
			if (flag13)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.Rating = Rating;
			this.Difficulty = Difficulty;
			int num2 = Math.Max(Difficulty, 4);
			int num3 = 4;
			if (Difficulty > Rating)
			{
				num3++;
			}
			if (Difficulty >= Rating * 2)
			{
				num3++;
			}
			if (num3 > PsychicCombatSifrah.slotConfigs.Count)
			{
				num3 = PsychicCombatSifrah.slotConfigs.Count;
			}
			List<SifrahToken> list = new List<SifrahToken>(num2);
			if (Difficulty < 1)
			{
				Difficulty = 1;
			}
			if (Rating < 1)
			{
				Rating = 1;
			}
			if (this.MaxTurns < 1)
			{
				this.MaxTurns = 1;
			}
			if (list.Count < num2 && SocialSifrahTokenThePowerOfLove.IsAvailable())
			{
				list.Add(new PsionicSifrahTokenThePowerOfLove());
			}
			if (list.Count < num2)
			{
				int num4 = 0;
				foreach (KeyValuePair<string, PowerEntry> keyValuePair in SkillFactory.Factory.PowersByClass)
				{
					if (keyValuePair.Key.StartsWith("Discipline_") && The.Player.HasSkill(keyValuePair.Value.Class))
					{
						num4++;
					}
				}
				if (num4 > 0 && num4 >= Difficulty / 2)
				{
					list.Add(new PsionicSifrahTokenDiscipline());
				}
			}
			if (list.Count < num2 && The.Player.CanMakeTelepathicContactWith(ContextObject))
			{
				list.Add(new PsionicSifrahTokenTelepathy());
			}
			if (list.Count < num2 && The.Player.CanMakeEmpathicContactWith(ContextObject))
			{
				list.Add(new PsionicSifrahTokenEmpathy());
			}
			if (list.Count < num2 && The.Player.Stat("Ego", 0) >= 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenExertWill());
			}
			if (list.Count < num2 && The.Player.Stat("Intelligence", 0) >= 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenApplyIntellect());
			}
			if (list.Count < num2 && The.Player.Stat("Willpower", 0) >= 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenCalmMind());
			}
			if (list.Count < num2 && !The.Player.HasEquippedItem("Cyclopean Prism") && The.Player.Stat("Ego", 0) < 10 + Difficulty * 3)
			{
				list.Add(new PsionicSifrahTokenPrayHumbly());
			}
			if (list.Count < num2 && The.Player.HasSkill("Customs_Sharer") && Rating > Difficulty)
			{
				list.Add(new PsionicSifrahTokenApplyAncientLore());
			}
			if (list.Count < num2 && flag14)
			{
				list.Add(new TinkeringSifrahTokenPsychometry("leverage psychic impressions of opponent's history"));
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Khu") && Rating / 4 + The.Player.Stat("Willpower", 0) - ContextObject.Stat("Willpower", 0) - (ContextObject.HasSkill("TenfoldPath_Khu") ? 10 : 0) > Difficulty * 4)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathKhu());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Yis") && Rating / 4 + The.Player.Stat("Willpower", 0) - ContextObject.Stat("Willpower", 0) - (ContextObject.HasSkill("TenfoldPath_Yis") ? 10 : 0) > Difficulty * 3)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathYis());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Hod") && Rating / 4 + The.Player.Stat("Intelligence", 0) - ContextObject.Stat("Intelligence", 0) - (ContextObject.HasSkill("TenfoldPath_Hod") ? 10 : 0) > Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathHod());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Tza") && Rating / 4 + The.Player.Stat("Ego", 0) - ContextObject.Stat("Ego", 0) - (ContextObject.HasSkill("TenfoldPath_Tza") ? 10 : 0) > Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathTza());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Ret") && Rating / 2 + The.Player.Stat("Willpower", 0) - ContextObject.Stat("Willpower", 0) - (ContextObject.HasSkill("TenfoldPath_Ret") ? 10 : 0) > Difficulty * 3 / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathRet());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Vur") && Rating / 2 + The.Player.Stat("Intelligence", 0) - ContextObject.Stat("Intelligence", 0) - (ContextObject.HasSkill("TenfoldPath_Vur") ? 10 : 0) > Difficulty * 3 / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathVur());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Sed") && Rating / 2 + The.Player.Stat("Ego", 0) - ContextObject.Stat("Ego", 0) - (ContextObject.HasSkill("TenfoldPath_Sed") ? 10 : 0) > Difficulty * 3 / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathSed());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Bin") && Rating + The.Player.Stat("Intelligence", 0) - ContextObject.Stat("Intelligence", 0) - (ContextObject.HasSkill("TenfoldPath_Bin") ? 10 : 0) > Difficulty / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathBin());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Hok") && Rating + The.Player.Stat("Ego", 0) - ContextObject.Stat("Ego", 0) - (ContextObject.HasSkill("TenfoldPath_Hok") ? 10 : 0) > Difficulty / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathHok());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Ket") && Rating + The.Player.Stat("Willpower", 0) - ContextObject.Stat("Willpower", 0) - (ContextObject.HasSkill("TenfoldPath_Ket") ? 10 : 0) > Difficulty / 4)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathKet());
			}
			if (num != 1)
			{
				if (num == 2 && list.Count < num2)
				{
					list.Add(new SocialSifrahTokenLeverageBeingLoved(primaryFaction));
				}
			}
			else if (list.Count < num2 && Difficulty < Rating * 2)
			{
				list.Add(new SocialSifrahTokenLeverageBeingFavored(primaryFaction));
			}
			int num5 = num2 - list.Count;
			if (num5 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor(ContextObject))
				{
					list2.Add(new PsionicSifrahTokenCreationKnowledge(ContextObject));
				}
				List<Worshippable> worshippables = Factions.GetWorshippables();
				foreach (Worshippable worshippable in worshippables)
				{
					if (worshippable.GetCounterRelevance(ContextObject, Rating) >= Difficulty * 2)
					{
						list2.Add(new RitualSifrahTokenInvokeHigherBeing(worshippable, worshippables));
					}
				}
				if (!The.Player.HasEffect<Lovesick>() && The.Player.CanApplyEffect("Lovesick", 0, null) && The.Player.IsOrganic)
				{
					int num6 = Math.Max(10 + (Difficulty - Rating) * 5, 5);
					if (num6 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectLovesick(num6));
					}
				}
				if (!The.Player.HasEffect<Shamed>() && The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num7 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num7 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectShamed(num7));
					}
				}
				if (!The.Player.HasEffect<Dazed>() && !The.Player.HasEffect<Stun>() && The.Player.CanApplyEffect("Dazed", 0, null))
				{
					int num8 = Math.Max(50 + (Difficulty - Rating) * 5, 5);
					if (num8 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectDazed(num8));
					}
				}
				if (!The.Player.HasEffect<Shaken>() && The.Player.CanApplyEffect("Shaken", 0, null))
				{
					int num9 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num9 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectShaken(num9));
					}
				}
				if (!The.Player.HasEffect<Exhausted>() && The.Player.CanApplyEffect("Exhausted", 0, null))
				{
					int num10 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num10 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectExhausted(num10));
					}
				}
				if (!The.Player.HasEffect<Terrified>() && The.Player.CanApplyEffect("Terrified", 0, "CanApplyFear"))
				{
					int num11 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num11 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectTerrified(num11));
					}
				}
				if (!The.Player.HasEffect<Asleep>() && The.Player.CanApplyEffect("Asleep", 0, "CanApplySleep"))
				{
					int num12 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num12 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectAsleep(num12));
					}
				}
				if (Rating >= 5 && !The.Player.HasEffect<Disoriented>() && The.Player.CanApplyEffect("Disoriented", 0, null))
				{
					int num13 = Math.Max(25 + (Difficulty - Rating) * 10, 5);
					if (num13 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectDisoriented(num13));
					}
				}
				if (Rating >= 3 && !The.Player.HasEffect<Confused>() && The.Player.CanApplyEffect("Confused", 0, null))
				{
					int num14 = Math.Max(20 + (Difficulty - Rating) * 10, 5);
					if (num14 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectConfused(num14));
					}
				}
				if (!The.Player.HasEffect<Nosebleed>() && The.Player.CanApplyEffect("Nosebleed", 0, null) && The.Player.CanApplyEffect("Bleeding", 0, null))
				{
					int num15 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num15 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectNosebleed(num15));
					}
				}
				if (!The.Player.HasEffect<CardiacArrest>() && The.Player.CanApplyEffect("CardiacArrest", 0, null))
				{
					int num16 = Math.Max(10 + (Difficulty - Rating) / 2 * 5, 5);
					if (num16 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectCardiacArrest(num16));
					}
				}
				if (!The.Player.HasEffect<ShatterMentalArmor>() && The.Player.CanApplyEffect("ShatterMentalArmor", 0, null))
				{
					int num17 = Math.Max(40 + (Difficulty - Rating) / 2 * 5, 5);
					if (num17 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectShatterMentalArmor(num17));
					}
				}
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Ego"));
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Intelligence"));
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Willpower"));
				base.AssignPossibleTokens(list2, list, num5, num2);
			}
			if (num2 > list.Count)
			{
				num2 = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(PsychicCombatSifrah.slotConfigs, num3, num2);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for " + this.Description + ", giving you no chance of performing well. You can remedy this situation by improving your Ego, Willpower, Intelligence, and esoteric skills.", true, true, true);
				this.Finished = true;
				this.Abort = true;
				return;
			}
			flag12 = new bool?(true);
			int? num18 = new int?(this.Slots.Count);
			int? num19 = new int?(this.Tokens.Count);
			if (base.GetGameCount(null, num18, num19, flag12, null) > 0)
			{
				this.AsMaster = true;
			}
			else
			{
				num19 = new int?(this.Slots.Count);
				num18 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome = null;
				int? num20 = num19;
				int? num21 = num18;
				flag12 = null;
				bool? flag15 = flag12;
				flag12 = null;
				int gameCount = base.GetGameCount(gameOutcome, num20, num21, flag15, flag12);
				num18 = new int?(this.Slots.Count);
				num19 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome2 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.Failure);
				int? num22 = num18;
				int? num23 = num19;
				flag12 = null;
				bool? flag16 = flag12;
				flag12 = null;
				int gameCount2 = base.GetGameCount(gameOutcome2, num22, num23, flag16, flag12);
				num19 = new int?(this.Slots.Count);
				num18 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome3 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.CriticalFailure);
				int? num24 = num19;
				int? num25 = num18;
				flag12 = null;
				bool? flag17 = flag12;
				flag12 = null;
				int gameCount3 = base.GetGameCount(gameOutcome3, num24, num25, flag17, flag12);
				int num26 = this.Tokens.Count + gameCount2 / 2 + gameCount3 + gameCount / 10;
				num18 = new int?(this.Slots.Count);
				num19 = new int?(this.Tokens.Count);
				flag12 = new bool?(true);
				SifrahGame.GameOutcome? gameOutcome4 = null;
				int? num27 = num18;
				int? num28 = num19;
				bool? flag18 = null;
				int gameCount4 = base.GetGameCount(gameOutcome4, num27, num28, flag18, flag12);
				flag12 = new bool?(true);
				flag18 = new bool?(false);
				SifrahGame.GameOutcome? gameOutcome5 = null;
				num19 = null;
				int? num29 = num19;
				num19 = null;
				int gameCount5 = base.GetGameCount(gameOutcome5, num29, num19, flag18, flag12);
				if (gameCount4 + gameCount5 / 100 >= num26)
				{
					this.AsMaster = true;
				}
			}
			if (this.AsMaster)
			{
				string sifrahRealityDistortionAuto = Options.SifrahRealityDistortionAuto;
				if (sifrahRealityDistortionAuto == "Always")
				{
					this.Finished = true;
					this.Bypass = true;
					return;
				}
				if (!(sifrahRealityDistortionAuto == "Never"))
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered psychic combat at this level of difficulty. Do you want to guide the process in detail anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically receive the results of strong but unexceptional performance.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
					if (dialogResult != DialogResult.Yes)
					{
						this.Finished = true;
						if (dialogResult == DialogResult.No)
						{
							this.Bypass = true;
							return;
						}
						this.Abort = true;
					}
				}
			}
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x001A4934 File Offset: 0x001A2B34
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish psychic combat as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
			}
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			if (num <= 0 && num4 <= 0)
			{
				this.Abort = true;
				return true;
			}
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish psychic combat as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
			if (dialogResult == DialogResult.Yes)
			{
				return true;
			}
			if (dialogResult == DialogResult.No)
			{
				this.Abort = true;
				return true;
			}
			return false;
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x001A49A9 File Offset: 0x001A2BA9
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x001A49C4 File Offset: 0x001A2BC4
		public override void Finish(GameObject ContextObject)
		{
			if (this.Bypass)
			{
				base.PlayOutcomeSound(SifrahGame.GameOutcome.Success);
				this.ResultSuccess(ContextObject);
				return;
			}
			if (!this.Abort)
			{
				this.Outcome = base.DetermineOutcome();
				base.PlayOutcomeSound(this.Outcome);
				bool flag = false;
				if (this.Outcome == SifrahGame.GameOutcome.CriticalFailure)
				{
					this.ResultCriticalFailure(ContextObject);
				}
				else if (this.Outcome == SifrahGame.GameOutcome.Failure)
				{
					this.ResultFailure(ContextObject);
				}
				else if (this.Outcome == SifrahGame.GameOutcome.PartialSuccess)
				{
					this.ResultPartialSuccess(ContextObject);
				}
				else if (this.Outcome == SifrahGame.GameOutcome.Success)
				{
					if (base.PercentSolved >= 100)
					{
						flag = true;
					}
					this.ResultSuccess(ContextObject);
					int num = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty / 3;
					if (base.PercentSolved < 100)
					{
						num = num * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
					if (num > 0)
					{
						The.Player.AwardXP(num, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
					}
				}
				else if (this.Outcome == SifrahGame.GameOutcome.CriticalSuccess)
				{
					if (base.PercentSolved >= 100)
					{
						flag = true;
					}
					this.ResultExceptionalSuccess(ContextObject);
					PsionicSifrah.AwardInsight();
					int num2 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty;
					if (base.PercentSolved < 100)
					{
						num2 = num2 * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
					if (num2 > 0)
					{
						The.Player.AwardXP(num2, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
					}
				}
				SifrahGame.RecordOutcome(this, this.Outcome, this.Slots.Count, this.Tokens.Count, this.AsMaster, flag);
			}
		}

		// Token: 0x06005714 RID: 22292 RVA: 0x001A4B8A File Offset: 0x001A2D8A
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			this.Performance = -100;
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x001A4B94 File Offset: 0x001A2D94
		public virtual void ResultFailure(GameObject ContextObject)
		{
			this.Performance = 0;
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x001A4B9D File Offset: 0x001A2D9D
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			this.Performance = 50;
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x001A4BA7 File Offset: 0x001A2DA7
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			this.Performance = 100;
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x001A4BB1 File Offset: 0x001A2DB1
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			this.Performance = 300;
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x001A4BC0 File Offset: 0x001A2DC0
		public override void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure)
		{
			double num = (double)(this.Rating + base.PercentSolved) * 0.01;
			double num2 = num;
			if (num2 < 1.0 && this.Difficulty > this.Rating / 2)
			{
				int i = this.Rating;
				int num3 = (this.Difficulty - this.Rating / 2).DiminishingReturns(1.0);
				while (i <= num3)
				{
					num2 *= num;
					i++;
				}
			}
			if (num2 < 0.0)
			{
				num2 = 0.0;
			}
			double num4 = 0.02 + (double)this.Powerup * 0.01;
			if (this.Turn > 1)
			{
				num4 += 0.01 * (double)(this.MaxTurns - this.Turn);
			}
			if (this.AsMaster)
			{
				num4 *= 3.0;
			}
			double num5 = num2 * num4;
			if (num2 > 1.0)
			{
				num2 = 1.0;
			}
			double num6 = 1.0 - num2;
			double num7 = num6 * 0.5;
			double num8 = num6 * 0.1;
			num2 -= num5;
			num6 -= num7;
			num6 -= num8;
			Success = (int)(num2 * 100.0);
			Failure = (int)(num6 * 100.0);
			PartialSuccess = (int)(num7 * 100.0);
			CriticalSuccess = (int)(num5 * 100.0);
			CriticalFailure = (int)(num8 * 100.0);
			while (Success + Failure + PartialSuccess + CriticalSuccess + CriticalFailure < 100)
			{
				if (this.AsMaster)
				{
					CriticalSuccess++;
				}
				else
				{
					PartialSuccess++;
				}
			}
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x001A4D75 File Offset: 0x001A2F75
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002BD2 RID: 11218
		public int Difficulty;

		// Token: 0x04002BD3 RID: 11219
		public int Rating;

		// Token: 0x04002BD4 RID: 11220
		public bool Abort;

		// Token: 0x04002BD5 RID: 11221
		public bool AsMaster;

		// Token: 0x04002BD6 RID: 11222
		public bool Bypass;

		// Token: 0x04002BD7 RID: 11223
		public bool InterfaceExitRequested;

		// Token: 0x04002BD8 RID: 11224
		public SifrahGame.GameOutcome Outcome;

		// Token: 0x04002BD9 RID: 11225
		public int Performance;

		// Token: 0x04002BDA RID: 11226
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("clearing the mind", "Items/sw_cherubic.bmp", "\t", "&B", 'K', 2),
			new SifrahSlotConfiguration("defending the psyche", "Items/sw_cyber_reactive_cranial_plating.bmp", "è", "&b", 'm', 1),
			new SifrahSlotConfiguration("marshaling the will to victory", "Items/sw_cyber_medassist_module.bmp", "\u000f", "&y", 'K', 3),
			new SifrahSlotConfiguration("focusing the mind to a single point", "Items/sw_cyber_anchor_spikes.bmp", "\a", "&W", 'B', 5),
			new SifrahSlotConfiguration("charging the destructive intention", "Items/sw_power_cut_large.png", "\u001e", "&M", 'R', 4),
			new SifrahSlotConfiguration("delivering the psychic strike", "Items/sw_esper.bmp", "\u001a", "&W", 'M', 0)
		};
	}
}
