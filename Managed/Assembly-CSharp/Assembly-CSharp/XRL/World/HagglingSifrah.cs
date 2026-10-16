using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200088C RID: 2188
	[Serializable]
	public class HagglingSifrah : SocialSifrah
	{
		// Token: 0x060056A7 RID: 22183 RVA: 0x001A0104 File Offset: 0x0019E304
		public HagglingSifrah(GameObject ContextObject)
		{
			this.Description = "Haggling with " + ContextObject.an(int.MaxValue, null, null, false, true, false, false, false, false, false, false, false, true, false, null, false, null, false);
			this.Difficulty = ContextObject.GetTier();
			string primaryFaction = ContextObject.GetPrimaryFaction(false);
			int num = ((!string.IsNullOrEmpty(primaryFaction)) ? The.Game.PlayerReputation.GetLevel(primaryFaction) : 0);
			this.Rating = GetTradePerformanceEvent.GetRatingFor(The.Player, ContextObject);
			this.MaxTurns = 3;
			if (!GetSocialSifrahSetupEvent.GetFor(The.Player, ContextObject, "Haggling", ref this.Difficulty, ref this.Rating, ref this.MaxTurns))
			{
				this.Finished = true;
				this.Abort = true;
			}
			int num2 = 3 + this.Difficulty;
			int num3 = 4;
			if (this.Difficulty >= 3)
			{
				num3++;
			}
			if (this.Difficulty >= 7)
			{
				num3++;
				num2 += 4;
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
			if (num3 > HagglingSifrah.slotConfigs.Count)
			{
				num3 = HagglingSifrah.slotConfigs.Count;
			}
			List<SifrahToken> list = new List<SifrahToken>(num2);
			if (this.Difficulty < 1)
			{
				this.Difficulty = 1;
			}
			if (this.Rating < 1)
			{
				this.Rating = 1;
			}
			if (this.MaxTurns < 1)
			{
				this.MaxTurns = 1;
			}
			if (list.Count < num2 && SocialSifrahTokenThePowerOfLove.IsAvailable())
			{
				list.Add(new SocialSifrahTokenThePowerOfLove());
			}
			if (list.Count < num2)
			{
				Scanning.Scan scanTypeFor = Scanning.GetScanTypeFor(ContextObject);
				if (Scanning.HasScanningFor(The.Player, scanTypeFor))
				{
					list.Add(new SocialSifrahTokenScanning(scanTypeFor));
				}
			}
			if (list.Count < num2 && this.Rating >= this.Difficulty + 3)
			{
				list.Add(new SocialSifrahTokenSociableChat());
			}
			if (list.Count < num2 && this.Rating >= this.Difficulty + 2)
			{
				list.Add(new SocialSifrahTokenListenSympathetically());
			}
			if (list.Count < num2 && this.Rating >= this.Difficulty + 1)
			{
				list.Add(new SocialSifrahTokenCrackAJoke());
			}
			if (list.Count < num2 && this.Rating >= this.Difficulty)
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
				list.Add(new SocialSifrahTokenLeverageBeingTrueKin());
			}
			if (list.Count < num2 && this.Rating >= this.Difficulty - 1)
			{
				list.Add(new SocialSifrahTokenFlatterInsincerely());
			}
			if (list.Count < num2 && this.Rating >= this.Difficulty - 2)
			{
				list.Add(new SocialSifrahTokenSpinATaleOfWoe());
			}
			if (list.Count < num2)
			{
				int? num4 = ContextObject.Con(The.Player, false);
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
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Sed"))
			{
				list.Add(new SocialSifrahTokenTenfoldPathSed());
			}
			int num6 = num2 - list.Count;
			if (num6 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (this.Rating >= this.Difficulty / 2 && ContextObject.Respires)
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
					int num7 = Math.Max(10 + (this.Difficulty - this.Rating) * 5, 5);
					if (num7 <= 120)
					{
						list2.Add(new SocialSifrahTokenEffectLovesick(num7));
					}
				}
				if (The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num8 = Math.Max(30 + (this.Difficulty - this.Rating) * 5, 5);
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
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(HagglingSifrah.slotConfigs, num3, num2);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for haggling with " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", giving you no chance of success. You can remedy this situation by improving your Ego and social skills, or by obtaining items useful in social situations.", true, true, true);
				this.Finished = true;
				this.Abort = true;
			}
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x001A06E4 File Offset: 0x0019E8E4
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish haggling as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish haggling as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x060056A9 RID: 22185 RVA: 0x001A0759 File Offset: 0x0019E959
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your haggling so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x060056AA RID: 22186 RVA: 0x001A0774 File Offset: 0x0019E974
		public override void Finish(GameObject ContextObject)
		{
			if (this.Abort)
			{
				return;
			}
			SifrahGame.GameOutcome gameOutcome = base.DetermineOutcome();
			base.PlayOutcomeSound(gameOutcome);
			if (gameOutcome == SifrahGame.GameOutcome.CriticalFailure)
			{
				this.ResultCriticalFailure();
			}
			else if (gameOutcome == SifrahGame.GameOutcome.Failure)
			{
				this.ResultFailure();
			}
			else if (gameOutcome == SifrahGame.GameOutcome.PartialSuccess)
			{
				this.ResultPartialSuccess();
			}
			else if (gameOutcome == SifrahGame.GameOutcome.Success)
			{
				this.ResultSuccess();
				int num = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty / 3;
				if (base.PercentSolved < 100)
				{
					num = num * (100 - (100 - base.PercentSolved) * 3) / 100;
				}
				int num2 = num - ContextObject.GetIntProperty("HagglingXPAwarded", 0);
				if (num2 > 0)
				{
					ContextObject.SetIntProperty("HagglingXPAwarded", num, false);
					The.Player.AwardXP(num2, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
				}
			}
			else if (gameOutcome == SifrahGame.GameOutcome.CriticalSuccess)
			{
				this.ResultExceptionalSuccess();
				SocialSifrah.AwardInsight();
				int num3 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty;
				if (base.PercentSolved < 100)
				{
					num3 = num3 * (100 - (100 - base.PercentSolved) * 3) / 100;
				}
				int num4 = num3 - ContextObject.GetIntProperty("HagglingXPAwarded", 0);
				if (num4 > 0)
				{
					ContextObject.SetIntProperty("HagglingXPAwarded", num3, false);
					The.Player.AwardXP(num4, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
				}
			}
			SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, false, false);
		}

		// Token: 0x060056AB RID: 22187 RVA: 0x001A0915 File Offset: 0x0019EB15
		public virtual void ResultCriticalFailure()
		{
			this.Performance = -Stat.Random(80, 90);
			this.Description = "Your haggling was an abysmal failure.";
		}

		// Token: 0x060056AC RID: 22188 RVA: 0x001A0932 File Offset: 0x0019EB32
		public virtual void ResultFailure()
		{
			this.Performance = -Stat.Random(40, 60);
			this.Description = "Your haggling went poorly.";
		}

		// Token: 0x060056AD RID: 22189 RVA: 0x001A094F File Offset: 0x0019EB4F
		public virtual void ResultPartialSuccess()
		{
			this.Performance = Stat.Random(0, 10);
			this.Description = "Your haggling was mediocre.";
		}

		// Token: 0x060056AE RID: 22190 RVA: 0x001A096A File Offset: 0x0019EB6A
		public virtual void ResultSuccess()
		{
			this.Performance = Stat.Random(20, 40);
			this.Description = "Your haggling went well.";
		}

		// Token: 0x060056AF RID: 22191 RVA: 0x001A0986 File Offset: 0x0019EB86
		public virtual void ResultExceptionalSuccess()
		{
			this.Performance = Stat.Random(60, 80);
			this.Description = "Your haggling was spectacular.";
		}

		// Token: 0x060056B0 RID: 22192 RVA: 0x001A09A4 File Offset: 0x0019EBA4
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
			double num4 = 0.01;
			if (this.Turn > 1)
			{
				num4 += 0.01 * (double)(this.MaxTurns - this.Turn);
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
				Success++;
			}
		}

		// Token: 0x04002BB7 RID: 11191
		public int Difficulty;

		// Token: 0x04002BB8 RID: 11192
		public int Rating;

		// Token: 0x04002BB9 RID: 11193
		public int Performance;

		// Token: 0x04002BBA RID: 11194
		public bool Abort;

		// Token: 0x04002BBB RID: 11195
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("establishing social relations", "Items/sw_leather_gloves.bmp", "\u0015", "&y", 'W', 0),
			new SifrahSlotConfiguration("setting expectations", "Items/sw_table_ornate_1.bmp", "Ò", "&W", 'r', 2),
			new SifrahSlotConfiguration("justifying desired outcomes", "Items/sw_flower_cut_4.bmp", "\u009c", "&g", 'M', 4),
			new SifrahSlotConfiguration("establishing willingness to disengage", "Items/sw_buckler2.bmp", "\u001f", "&W", 'r', 3),
			new SifrahSlotConfiguration("preserving mutual dignity", "Items/sw_bust1.bmp", "è", "&Y", 'w', 5),
			new SifrahSlotConfiguration("closing the deal", "Items/sw_bar.bmp", "$", "&W", 'w', 1)
		};
	}
}
