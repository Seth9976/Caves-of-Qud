using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000884 RID: 2180
	[Serializable]
	public class BaetylOfferingSifrah : RitualSifrah
	{
		// Token: 0x06005666 RID: 22118 RVA: 0x0019B154 File Offset: 0x00199354
		public BaetylOfferingSifrah(GameObject ContextObject, int Rating, int Difficulty)
		{
			this.Description = "Making an offering to " + ContextObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			this.Difficulty = Difficulty;
			this.Rating = Rating;
			string primaryFaction = ContextObject.GetPrimaryFaction(false);
			int num = ((!primaryFaction.IsNullOrEmpty()) ? The.Game.PlayerReputation.GetLevel(primaryFaction) : 0);
			this.MaxTurns = 3;
			bool flag = false;
			bool flag2 = false;
			GetRitualSifrahSetupEvent.GetFor(The.Player, ContextObject, "BaetylOffering", false, ref Difficulty, ref Rating, ref this.MaxTurns, ref flag, ref flag2);
			if (flag)
			{
				this.Finished = true;
				this.Abort = true;
			}
			int num2 = Math.Max(Difficulty * 5 / 4, 4);
			int num3 = 4;
			if (Difficulty >= 4)
			{
				num3++;
			}
			if (Difficulty >= 7)
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
			if (num3 > BaetylOfferingSifrah.slotConfigs.Count)
			{
				num3 = BaetylOfferingSifrah.slotConfigs.Count;
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
				list.Add(new RitualSifrahTokenThePowerOfLove());
			}
			if (list.Count < num2 && Rating >= (The.Player.IsTrueKin() ? (Difficulty / 2) : Difficulty))
			{
				list.Add(new RitualSifrahTokenInvokeAncientCompacts());
			}
			if (list.Count < num2 && Rating >= Difficulty * 2)
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
			if (list.Count < num2 && The.Player.HasSkill("Customs_Sharer"))
			{
				list.Add(new RitualSifrahTokenSingAHistoricalEpic());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Bin"))
			{
				list.Add(new RitualSifrahTokenTenfoldPathBin());
			}
			if (list.Count < num2 && The.Player.HasSkill("TenfoldPath_Sed"))
			{
				list.Add(new RitualSifrahTokenTenfoldPathSed());
			}
			int num4 = num2 - list.Count;
			if (num4 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Rating >= Difficulty / 2 && ContextObject.Respires)
				{
					list2.Add(new SocialSifrahTokenHookah());
				}
				list2.Add(new TinkeringSifrahTokenComputePower(Difficulty * Difficulty));
				if (Rating >= Difficulty / 2)
				{
					list.Add(new SocialSifrahTokenReadFromTheCanticlesChromaic());
				}
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Difficulty)
					{
						break;
					}
					list2.Add(new SocialSifrahTokenBit(bitType));
				}
				list2.Add(new SocialSifrahTokenCharge(Difficulty * 2000));
				foreach (Worshippable worshippable in Factions.GetWorshippables())
				{
					if (Rating + worshippable.Power.DiminishingReturns(1.0) >= Difficulty * 2)
					{
						list2.Add(new RitualSifrahTokenInvokeHigherBeing(worshippable, null));
					}
				}
				if (!The.Player.HasEffect<Lovesick>() && The.Player.CanApplyEffect("Lovesick", 0, null) && The.Player.IsOrganic)
				{
					int num5 = Math.Max(10 + (Difficulty - Rating) * 5, 5);
					if (num5 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectLovesick(num5));
					}
				}
				if (!The.Player.HasEffect<Shamed>() && The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num6 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num6 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShamed(num6));
					}
				}
				if (!The.Player.HasEffect<Dazed>() && !The.Player.HasEffect<Stun>() && The.Player.CanApplyEffect("Dazed", 0, null))
				{
					int num7 = Math.Max(50 + (Difficulty - Rating) * 5, 5);
					if (num7 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectDazed(num7));
					}
				}
				if (!The.Player.HasEffect<Shaken>() && The.Player.CanApplyEffect("Shaken", 0, null))
				{
					int num8 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num8 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShaken(num8));
					}
				}
				if (Rating >= 4 && !The.Player.HasEffect<Exhausted>() && The.Player.CanApplyEffect("Exhausted", 0, null))
				{
					int num9 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num9 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectExhausted(num9));
					}
				}
				if (Rating >= 5 && !The.Player.HasEffect<Terrified>() && The.Player.CanApplyEffect("Terrified", 0, "CanApplyFear"))
				{
					int num10 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num10 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectTerrified(num10));
					}
				}
				if (Rating >= 6 && !The.Player.HasEffect<Asleep>() && The.Player.CanApplyEffect("Asleep", 0, "CanApplySleep"))
				{
					int num11 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num11 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectAsleep(num11));
					}
				}
				if (Rating >= 7 && !The.Player.HasEffect<Disoriented>() && The.Player.CanApplyEffect("Disoriented", 0, null))
				{
					int num12 = Math.Max(25 + (Difficulty - Rating) * 10, 5);
					if (num12 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectDisoriented(num12));
					}
				}
				if (Rating >= 4 && !The.Player.HasEffect<Confused>() && The.Player.CanApplyEffect("Confused", 0, null))
				{
					int num13 = Math.Max(20 + (Difficulty - Rating) * 10, 5);
					if (num13 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectConfused(num13));
					}
				}
				if (Rating >= 2 && !The.Player.HasEffect<Bleeding>() && The.Player.CanApplyEffect("Bleeding", 0, null))
				{
					int num14 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num14 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectBleeding(num14));
					}
				}
				if (Rating >= 2 && !The.Player.HasEffect<Poisoned>() && The.Player.CanApplyEffect("Poisoned", 0, "CanApplyPoison"))
				{
					int num15 = Math.Max(10 + (Difficulty - Rating) / 2 * 5, 5);
					if (num15 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectPoisoned(num15));
					}
				}
				if (Rating >= 3 && !The.Player.HasEffect<Ill>() && !The.Player.HasEffect<Poisoned>() && The.Player.CanApplyEffect("Ill", 0, null))
				{
					int num16 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num16 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectIll(num16));
					}
				}
				if (Rating >= 5 && !The.Player.HasEffect<CardiacArrest>() && The.Player.CanApplyEffect("CardiacArrest", 0, null))
				{
					int num17 = Math.Max(10 + (Difficulty - Rating) / 2 * 5, 5);
					if (num17 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectCardiacArrest(num17));
					}
				}
				if (Rating >= 12 && !The.Player.HasEffect<ShatterMentalArmor>() && The.Player.CanApplyEffect("ShatterMentalArmor", 0, null))
				{
					int num18 = Math.Max(40 + (Difficulty - Rating) * 5, 5);
					if (num18 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShatterMentalArmor(num18));
					}
				}
				SocialSifrah.AddTokenTokens(list2, ContextObject);
				list2.Add(new SocialSifrahTokenSecret(ContextObject));
				base.AssignPossibleTokens(list2, list, num4, num2);
			}
			if (num2 > list.Count)
			{
				num2 = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(BaetylOfferingSifrah.slotConfigs, num3, num2);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for performing an offering to " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", giving you no chance of doing so well. You can remedy this situation by improving your Intelligence or by obtaining items useful in such a ritual.", true, true, true);
				this.Finished = true;
				this.Abort = true;
			}
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x0019B9A0 File Offset: 0x00199BA0
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish the offering ritual as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish the offering ritual as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x06005668 RID: 22120 RVA: 0x0019BA15 File Offset: 0x00199C15
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x06005669 RID: 22121 RVA: 0x0019BA30 File Offset: 0x00199C30
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
			else if (gameOutcome == SifrahGame.GameOutcome.CriticalSuccess)
			{
				this.ResultExceptionalSuccess(ContextObject);
				RitualSifrah.AwardInsight();
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
			SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, false, false);
		}

		// Token: 0x0600566A RID: 22122 RVA: 0x0019BB90 File Offset: 0x00199D90
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			Popup.Show("Your abysmal ritual performance deeply shames you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			The.Player.ApplyEffect(new Shamed(Stat.Random(100, 500)), null);
			this.Performance = 10;
		}

		// Token: 0x0600566B RID: 22123 RVA: 0x0019BBCC File Offset: 0x00199DCC
		public virtual void ResultFailure(GameObject ContextObject)
		{
			Popup.Show("Your performance of the offering ritual was adequate, if barely.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 50;
		}

		// Token: 0x0600566C RID: 22124 RVA: 0x0019BBEB File Offset: 0x00199DEB
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the offering ritual was passable.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 100;
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x0019BC0A File Offset: 0x00199E0A
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the offering ritual was solemn and respectful.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 150;
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x0019BC2C File Offset: 0x00199E2C
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the offering ritual was gracious and dignified.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 300;
		}

		// Token: 0x0600566F RID: 22127 RVA: 0x0019BC50 File Offset: 0x00199E50
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
			double num4 = 0.03 + (double)this.Difficulty * 0.01 + (double)this.Powerup * 0.01;
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

		// Token: 0x04002B87 RID: 11143
		public int Difficulty;

		// Token: 0x04002B88 RID: 11144
		public int Rating;

		// Token: 0x04002B89 RID: 11145
		public int Performance;

		// Token: 0x04002B8A RID: 11146
		public bool Abort;

		// Token: 0x04002B8B RID: 11147
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("opening the ceremony", "Items/sw_cyber_reactive_cranial_plating.bmp", "\u000f", "&B", 'y', 1),
			new SifrahSlotConfiguration("cleansing the space", "Items/sw_skin_glitter.bmp", "\u0015", "&K", 'y', 4),
			new SifrahSlotConfiguration("preparing the offering", "Items/sw_shawl.bmp", "\n", "&Y", 'W', 3),
			new SifrahSlotConfiguration("performing the offering", "Items/sw_urn_1.bmp", "é", "&Y", 'B', 0),
			new SifrahSlotConfiguration("honoring the space", "Items/ms_face_heart.png", "§", "&y", 'M', 5),
			new SifrahSlotConfiguration("closing the ceremony", "Items/sw_orb.bmp", "\a", "&B", 'b', 2)
		};
	}
}
