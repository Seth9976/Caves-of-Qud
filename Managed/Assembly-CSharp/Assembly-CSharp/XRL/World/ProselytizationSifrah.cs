using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200088F RID: 2191
	[Serializable]
	public class ProselytizationSifrah : SocialSifrah
	{
		// Token: 0x060056C9 RID: 22217 RVA: 0x001A24C8 File Offset: 0x001A06C8
		public ProselytizationSifrah(GameObject ContextObject, int Rating, int Difficulty)
		{
			string text = "Proselytizing ";
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
			if (!GetSocialSifrahSetupEvent.GetFor(The.Player, ContextObject, "Proselytization", ref Difficulty, ref Rating, ref this.MaxTurns))
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.Rating = Rating;
			this.Difficulty = Difficulty + ContextObject.GetIntProperty("ProselytizationSifrahDifficultyModifier", 0);
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
			if (num3 > ProselytizationSifrah.slotConfigs.Count)
			{
				num3 = ProselytizationSifrah.slotConfigs.Count;
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
			if (list.Count < num2 && Rating >= Difficulty + 3)
			{
				list.Add(new SocialSifrahTokenSociableChat());
			}
			if (list.Count < num2 && Rating >= Difficulty + 2)
			{
				list.Add(new SocialSifrahTokenListenSympathetically());
			}
			if (list.Count < num2 && Rating >= Difficulty + 4)
			{
				list.Add(new SocialSifrahTokenDebateRationally());
			}
			if (list.Count < num2 && Rating >= Difficulty + 1)
			{
				list.Add(new SocialSifrahTokenFlirtSuggestively());
			}
			if (list.Count < num2 && Rating >= Difficulty)
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
			if (list.Count < num2 && The.Player.IsTrueKin() && ContextObject.HasPart<Robot>())
			{
				list.Add(new SocialSifrahTokenInvokeAncientCompacts());
			}
			if (list.Count < num2 && The.Player.HasSkill("Tinkering_Repair") && ContextObject.HasPart<Robot>())
			{
				list.Add(new SocialSifrahTokenOfferMaintenanceServices());
			}
			if (list.Count < num2 && Rating >= Difficulty - 1)
			{
				list.Add(new SocialSifrahTokenFlatterInsincerely());
			}
			if (list.Count < num2 && Rating >= Difficulty - 2)
			{
				list.Add(new SocialSifrahTokenTellAnInspiringTale());
			}
			if (list.Count < num2 && Rating >= Difficulty - 3)
			{
				list.Add(new SocialSifrahTokenRailAgainstInjustice());
			}
			if (list.Count < num2 && The.Player.Stat("Level", 0) + Rating > ContextObject.Stat("Level", 0) + Difficulty)
			{
				list.Add(new SocialSifrahTokenBoastOfAccomplishments());
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
				if (Rating >= Difficulty / 2 && (ContextObject.IsMemberOfFaction("Mechanimists") || ContextObject.IsMemberOfFaction("MechaSub") || Factions.GetFeelingFactionToObject("Mechanimists", ContextObject, null) >= 0))
				{
					list.Add(new SocialSifrahTokenReadFromTheCanticlesChromaic());
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
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(ProselytizationSifrah.slotConfigs, num3, num2);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				string text4 = "You have no usable options to employ for proselytizing ";
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
				Popup.ShowFail(text4 + ContextObject.t(maxValue2, text5, text6, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, text7, flag21, flag22, flag23, flag12, true, null, false) + ", giving you no chance of success. You can remedy this situation by improving your Ego and social skills, or by obtaining items useful in social situations.", true, true, true);
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
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered proselytization at this level of discourse. Do you want to perform detailed proselytization anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically succeed at proselytization if that is possible.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x060056CA RID: 22218 RVA: 0x001A2D9C File Offset: 0x001A0F9C
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish the proselytization attempt as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish the proselytization attempt as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x060056CB RID: 22219 RVA: 0x001A2E11 File Offset: 0x001A1011
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x001A2E2C File Offset: 0x001A102C
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
				int num2 = num - ContextObject.GetIntProperty("ProselytizationXPAwarded", 0);
				if (num2 > 0)
				{
					ContextObject.SetIntProperty("ProselytizationXPAwarded", num, false);
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
				int num4 = num3 - ContextObject.GetIntProperty("ProselytizationXPAwarded", 0);
				if (num4 > 0)
				{
					ContextObject.SetIntProperty("ProselytizationXPAwarded", num3, false);
					The.Player.AwardXP(num4, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
				}
			}
			SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, this.AsMaster, flag);
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x001A3010 File Offset: 0x001A1210
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			Popup.Show(ContextObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " offended by your impertinence.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			ContextObject.AddOpinion<OpinionPoorReasoning>(The.Player, 1f);
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x001A3074 File Offset: 0x001A1274
		public virtual void ResultFailure(GameObject ContextObject)
		{
			Popup.Show(ContextObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unconvinced by your pleas.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x001A30C8 File Offset: 0x001A12C8
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			Popup.Show(ContextObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unconvinced by your pleas, but interested in hearing more.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			ContextObject.ModIntProperty("ProselytizationSifrahDifficultyModifier", -Stat.Random(1, 5), false);
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x001A3130 File Offset: 0x001A1330
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			if (!ContextObject.ApplyEffect(new Proselytized(The.Player), null))
			{
				Popup.Show(ContextObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " sympathetic, but unable to join you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x001A3198 File Offset: 0x001A1398
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			ContextObject.LikeBetter(The.Player, 20);
			if (ContextObject.ApplyEffect(new Proselytized(The.Player), null))
			{
				ContextObject.AwardXP(ContextObject.Stat("Level", 0) * 100, -1, 0, int.MaxValue, null, ContextObject, null, The.Player, null, null);
				return;
			}
			Popup.Show(ContextObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " sympathetic, but unable to join you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x001A3234 File Offset: 0x001A1434
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

		// Token: 0x04002BCA RID: 11210
		public int Difficulty;

		// Token: 0x04002BCB RID: 11211
		public int Rating;

		// Token: 0x04002BCC RID: 11212
		public bool Abort;

		// Token: 0x04002BCD RID: 11213
		public bool AsMaster;

		// Token: 0x04002BCE RID: 11214
		public bool Bypass;

		// Token: 0x04002BCF RID: 11215
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("opening a conversation", "Items/sw_leather_gloves.bmp", "\u0015", "&y", 'W', 0),
			new SifrahSlotConfiguration("identifying motivations", "Items/sw_heightenedhearing.bmp", "?", "&Y", 'W', 5),
			new SifrahSlotConfiguration("constructing a shared vision", "Mutations/night_vision_mutation.bmp", "é", "&B", 'M', 3),
			new SifrahSlotConfiguration("overcoming reservations", "Items/sw_microscope.bmp", ">", "&c", 'R', 2),
			new SifrahSlotConfiguration("justifying social realignment", "Items/sw_jackhammer.bmp", "ö", "&c", 'K', 4),
			new SifrahSlotConfiguration("eliciting the leap of faith", "Items/sw_longblade_3.bmp", "\u001a", "&Y", 'C', 1)
		};
	}
}
