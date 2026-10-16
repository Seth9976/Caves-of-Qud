using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008B7 RID: 2231
	[Serializable]
	public class RebukingSifrah : SocialSifrah
	{
		// Token: 0x06005728 RID: 22312 RVA: 0x001A5F90 File Offset: 0x001A4190
		public RebukingSifrah(GameObject ContextObject, int Rating, int Difficulty)
		{
			string text = "Rebuking ";
			int maxValue = int.MaxValue;
			string text2 = null;
			string text3 = null;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = true;
			bool flag11 = false;
			bool? flag12 = null;
			this.Description = text + ContextObject.an(maxValue, text2, text3, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, false, null, false);
			string primaryFaction = ContextObject.GetPrimaryFaction(false);
			int num = ((!string.IsNullOrEmpty(primaryFaction)) ? The.Game.PlayerReputation.GetLevel(primaryFaction) : 0);
			this.MaxTurns = 3;
			if (!GetSocialSifrahSetupEvent.GetFor(The.Player, ContextObject, "Rebuking", ref Difficulty, ref Rating, ref this.MaxTurns))
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.Rating = Rating;
			this.Difficulty = Difficulty + ContextObject.GetIntProperty("RebukingSifrahDifficultyModifier", 0);
			int num2 = Math.Max(Difficulty / 5, 4);
			int num3 = 4;
			if (Difficulty >= 20)
			{
				num3++;
			}
			if (Difficulty >= 40)
			{
				num3++;
			}
			if (num != -2)
			{
				if (num == -1)
				{
					num2++;
				}
			}
			else
			{
				num2 += 2;
				num3++;
				this.MaxTurns--;
			}
			if (num3 > RebukingSifrah.slotConfigs.Count)
			{
				num3 = RebukingSifrah.slotConfigs.Count;
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
				list.Add(new SocialSifrahTokenThePowerOfLove());
			}
			if (list.Count < num2 && The.Player.IsTrueKin() && ContextObject.HasPart<Robot>())
			{
				list.Add(new SocialSifrahTokenInvokeAncientCompacts());
			}
			if (list.Count < num2 && The.Player.HasSkill("Tinkering_Repair") && ContextObject.HasPart<Robot>())
			{
				list.Add(new SocialSifrahTokenOfferMaintenanceServices());
			}
			if (list.Count < num2 && Rating >= Difficulty)
			{
				list.Add(new SocialSifrahTokenDebateRationally());
			}
			if (list.Count < num2 && Rating >= Difficulty + 2)
			{
				list.Add(new SocialSifrahTokenListenSympathetically());
			}
			if (list.Count < num2 && Rating >= Difficulty + 4)
			{
				list.Add(new SocialSifrahTokenPayACompliment());
			}
			if (num != 1)
			{
				if (num == 2)
				{
					this.MaxTurns++;
					if (list.Count < num2)
					{
						list.Add(new SocialSifrahTokenLeverageBeingLoved(primaryFaction));
					}
				}
			}
			else if (list.Count < num2)
			{
				list.Add(new SocialSifrahTokenLeverageBeingFavored(primaryFaction));
			}
			int? num4;
			if (list.Count < num2)
			{
				num4 = ContextObject.Con(The.Player, false);
				int num5 = -10;
				if ((num4.GetValueOrDefault() <= num5) & (num4 != null))
				{
					list.Add(new SocialSifrahTokenPostureIntimidatingly());
				}
			}
			if (list.Count < num2 && The.Player.CanMakeTelepathicContactWith(ContextObject))
			{
				list.Add(new SocialSifrahTokenTelepathy());
			}
			if (list.Count < num2 && The.Player.CanMakeEmpathicContactWith(ContextObject))
			{
				list.Add(new SocialSifrahTokenEmpathy());
			}
			if (list.Count < num2)
			{
				Scanning.Scan scanTypeFor = Scanning.GetScanTypeFor(ContextObject);
				if (Scanning.HasScanningFor(The.Player, scanTypeFor))
				{
					list.Add(new SocialSifrahTokenScanning(scanTypeFor));
				}
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Sed"))
			{
				list.Add(new SocialSifrahTokenTenfoldPathSed());
			}
			int num6 = num2 - list.Count;
			if (num6 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Rating >= Difficulty / 2 && ContextObject.Respires)
				{
					list2.Add(new SocialSifrahTokenHookah());
				}
				list2.Add(new SocialSifrahTokenLiquid(ContextObject, false));
				if (SocialSifrahTokenApplySocialCoprocessor.IsPotentiallyAvailable())
				{
					list2.Add(new SocialSifrahTokenApplySocialCoprocessor());
				}
				if (ContextObject.HasPart<Robot>())
				{
					int tier = ContextObject.GetTier();
					foreach (BitType bitType in BitType.BitTypes)
					{
						if (bitType.Level > tier)
						{
							break;
						}
						list2.Add(new SocialSifrahTokenBit(bitType));
					}
					list2.Add(new SocialSifrahTokenCharge(tier * 1000));
				}
				if (!The.Player.HasEffect<Lovesick>() && The.Player.CanApplyEffect("Lovesick", 0, null) && The.Player.IsOrganic)
				{
					int num7 = Math.Max(10 + (Difficulty - Rating) * 5, 5);
					if (num7 <= 120)
					{
						list2.Add(new SocialSifrahTokenEffectLovesick(num7));
					}
				}
				if (The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num8 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num8 <= 120)
					{
						list2.Add(new SocialSifrahTokenEffectShamed(num8));
					}
				}
				SocialSifrahTokenGift appropriate = SocialSifrahTokenGift.GetAppropriate(ContextObject);
				if (appropriate != null)
				{
					list2.Add(appropriate);
				}
				SocialSifrah.AddTokenTokens(list2, ContextObject);
				list2.Add(new SocialSifrahTokenSecret(ContextObject));
				base.AssignPossibleTokens(list2, list, num6, num2);
			}
			if (num2 > list.Count)
			{
				num2 = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(RebukingSifrah.slotConfigs, num3, num2);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				string text4 = "You have no usable options to employ for rebuking ";
				int maxValue2 = int.MaxValue;
				string text5 = null;
				string text6 = null;
				bool flag13 = false;
				bool flag14 = true;
				bool flag15 = false;
				bool flag16 = false;
				bool flag17 = false;
				bool flag18 = true;
				bool flag19 = true;
				bool flag20 = false;
				string text7 = null;
				bool flag21 = false;
				bool flag22 = true;
				bool flag23 = false;
				flag12 = null;
				Popup.ShowFail(text4 + ContextObject.t(maxValue2, text5, text6, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, text7, flag21, flag22, flag23, flag12, true, null, false) + ", giving you no chance of success. You can remedy this situation by improving your Ego and social skills, by implanting appropriate cybernetics, or by obtaining items useful in social situations.", true, true, true);
				this.Finished = true;
				this.Abort = true;
				return;
			}
			flag12 = new bool?(true);
			num4 = new int?(this.Slots.Count);
			int? num9 = new int?(this.Tokens.Count);
			if (base.GetGameCount(null, num4, num9, flag12, null) > 0)
			{
				this.AsMaster = true;
			}
			else
			{
				num9 = new int?(this.Slots.Count);
				num4 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome = null;
				int? num10 = num9;
				int? num11 = num4;
				flag12 = null;
				bool? flag24 = flag12;
				flag12 = null;
				int gameCount = base.GetGameCount(gameOutcome, num10, num11, flag24, flag12);
				num4 = new int?(this.Slots.Count);
				num9 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome2 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.Failure);
				int? num12 = num4;
				int? num13 = num9;
				flag12 = null;
				bool? flag25 = flag12;
				flag12 = null;
				int gameCount2 = base.GetGameCount(gameOutcome2, num12, num13, flag25, flag12);
				num9 = new int?(this.Slots.Count);
				num4 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome3 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.CriticalFailure);
				int? num14 = num9;
				int? num15 = num4;
				flag12 = null;
				bool? flag26 = flag12;
				flag12 = null;
				int gameCount3 = base.GetGameCount(gameOutcome3, num14, num15, flag26, flag12);
				int num16 = this.Tokens.Count + gameCount2 / 2 + gameCount3 + gameCount / 10;
				num4 = new int?(this.Slots.Count);
				num9 = new int?(this.Tokens.Count);
				flag12 = new bool?(true);
				SifrahGame.GameOutcome? gameOutcome4 = null;
				int? num17 = num4;
				int? num18 = num9;
				bool? flag27 = null;
				int gameCount4 = base.GetGameCount(gameOutcome4, num17, num18, flag27, flag12);
				flag12 = new bool?(true);
				flag27 = new bool?(false);
				SifrahGame.GameOutcome? gameOutcome5 = null;
				num9 = null;
				int? num19 = num9;
				num9 = null;
				int gameCount5 = base.GetGameCount(gameOutcome5, num19, num9, flag27, flag12);
				flag27 = new bool?(true);
				flag12 = new bool?(true);
				SifrahGame.GameOutcome? gameOutcome6 = null;
				num9 = null;
				int? num20 = num9;
				num9 = null;
				int gameCount6 = base.GetGameCount(gameOutcome6, num20, num9, flag12, flag27);
				if (gameCount4 + gameCount5 / 50 + gameCount6 / 75 >= num16)
				{
					this.AsMaster = true;
				}
			}
			if (this.AsMaster)
			{
				string sifrahRecruitmentAuto = Options.SifrahRecruitmentAuto;
				if (sifrahRecruitmentAuto == "Always")
				{
					this.Finished = true;
					this.Bypass = true;
					return;
				}
				if (!(sifrahRecruitmentAuto == "Never"))
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered rebuking robots of this grade. Do you want to perform a detailed rebuke anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically succeed t the rebuke if that is possible.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x06005729 RID: 22313 RVA: 0x001A6768 File Offset: 0x001A4968
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish the rebuking attempt as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish the rebuking attempt as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x0600572A RID: 22314 RVA: 0x001A67DD File Offset: 0x001A49DD
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x001A67F8 File Offset: 0x001A49F8
		public override void Finish(GameObject ContextObject)
		{
			if (this.Abort)
			{
				return;
			}
			if (this.Bypass)
			{
				base.PlayOutcomeSound(SifrahGame.GameOutcome.Success);
				this.ResultSuccess(ContextObject);
				return;
			}
			SifrahGame.GameOutcome gameOutcome = base.DetermineOutcome();
			base.PlayOutcomeSound(gameOutcome);
			bool flag = false;
			if (gameOutcome == SifrahGame.GameOutcome.CriticalFailure)
			{
				this.ResultCriticalFailure(ContextObject);
			}
			else if (gameOutcome == SifrahGame.GameOutcome.Failure)
			{
				this.ResultFailure(ContextObject);
			}
			else if (gameOutcome == SifrahGame.GameOutcome.PartialSuccess)
			{
				this.ResultPartialSuccess(ContextObject);
			}
			else if (gameOutcome == SifrahGame.GameOutcome.Success)
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
				int num2 = num - ContextObject.GetIntProperty("RebukingXPAwarded", 0);
				if (num2 > 0)
				{
					ContextObject.SetIntProperty("RebukingXPAwarded", num, false);
					The.Player.AwardXP(num2, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
				}
			}
			else if (gameOutcome == SifrahGame.GameOutcome.CriticalSuccess)
			{
				if (base.PercentSolved >= 100)
				{
					flag = true;
				}
				this.ResultExceptionalSuccess(ContextObject);
				SocialSifrah.AwardInsight();
				int num3 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty;
				if (base.PercentSolved < 100)
				{
					num3 = num3 * (100 - (100 - base.PercentSolved) * 3) / 100;
				}
				int num4 = num3 - ContextObject.GetIntProperty("RebukingXPAwarded", 0);
				if (num4 > 0)
				{
					ContextObject.SetIntProperty("RebukingXPAwarded", num3, false);
					The.Player.AwardXP(num4, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
				}
			}
			SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, this.AsMaster, flag);
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x001A69DC File Offset: 0x001A4BDC
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			Popup.Show(ContextObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ContextObject.Is + " enraged by your poor reasoning.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			ContextObject.AddOpinion<OpinionPoorReasoning>(The.Player, 1f);
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x001A6A3E File Offset: 0x001A4C3E
		public virtual void ResultFailure(GameObject ContextObject)
		{
			Popup.Show("Your argument does not compute.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x001A6A58 File Offset: 0x001A4C58
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			Popup.Show(ContextObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ContextObject.GetVerb("wander", true, false, false, true, false) + " away disinterestedly.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Persuasion_RebukeRobot.Neutralize(The.Player, ContextObject);
		}

		// Token: 0x0600572F RID: 22319 RVA: 0x001A6ABF File Offset: 0x001A4CBF
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			Persuasion_RebukeRobot.Rebuke(The.Player, ContextObject);
		}

		// Token: 0x06005730 RID: 22320 RVA: 0x001A6AD0 File Offset: 0x001A4CD0
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			ContextObject.LikeBetter(The.Player, 25);
			if (Persuasion_RebukeRobot.Rebuke(The.Player, ContextObject))
			{
				ContextObject.AwardXP(ContextObject.Stat("Level", 0) * 100, -1, 0, int.MaxValue, null, ContextObject, null, The.Player, null, null);
			}
		}

		// Token: 0x06005731 RID: 22321 RVA: 0x001A6B20 File Offset: 0x001A4D20
		public override void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure)
		{
			double num = (double)(this.Rating + base.PercentSolved) * 0.01;
			double num2 = num;
			if (num2 < 1.0)
			{
				int i = 1;
				int num3 = this.Difficulty.DiminishingReturns(1.0);
				while (i < num3)
				{
					num2 *= num;
					i++;
				}
			}
			if (num2 < 0.0)
			{
				num2 = 0.0;
			}
			double num4 = 0.01 + (double)this.Powerup * 0.01;
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
					Success++;
				}
			}
		}

		// Token: 0x04002BE4 RID: 11236
		public int Difficulty;

		// Token: 0x04002BE5 RID: 11237
		public int Rating;

		// Token: 0x04002BE6 RID: 11238
		public bool Abort;

		// Token: 0x04002BE7 RID: 11239
		public bool AsMaster;

		// Token: 0x04002BE8 RID: 11240
		public bool Bypass;

		// Token: 0x04002BE9 RID: 11241
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("initiating dialogue", "Items/sw_scanningmodule.bmp", "\u0013", "&c", 'C', 0),
			new SifrahSlotConfiguration("validating credentials", "Items/sw_security_card.bmp", "?", "&Y", 'W', 2),
			new SifrahSlotConfiguration("rationalizing authorization", "Items/sw_mask2.bmp", "é", "&B", 'R', 5),
			new SifrahSlotConfiguration("overcoming logical objections", "Items/sw_microscope.bmp", ">", "&c", 'R', 4),
			new SifrahSlotConfiguration("justifying operational changes", "Items/sw_jackhammer.bmp", "ö", "&c", 'K', 3),
			new SifrahSlotConfiguration("finalizing the process", "Items/sw_breadboard.bmp", "ê", "&c", 'C', 1)
		};
	}
}
