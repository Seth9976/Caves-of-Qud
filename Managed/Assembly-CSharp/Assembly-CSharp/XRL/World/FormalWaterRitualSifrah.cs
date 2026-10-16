using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000889 RID: 2185
	[Serializable]
	public class FormalWaterRitualSifrah : RitualSifrah
	{
		// Token: 0x06005690 RID: 22160 RVA: 0x0019E9BC File Offset: 0x0019CBBC
		public FormalWaterRitualSifrah(GameObject ContextObject)
		{
			this.Description = "Performing the formal water ritual with " + ContextObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			this.Rating = The.Player.StatMod("Ego", 0);
			this.Difficulty = ContextObject.GetTier() + ContextObject.GetIntProperty("FormalWaterRitualSifrahDifficultyModifier", 0);
			this.MaxTurns = 3;
			bool flag = false;
			bool flag2 = false;
			GetRitualSifrahSetupEvent.GetFor(The.Player, ContextObject, "FormalWaterRitual", false, ref this.Difficulty, ref this.Rating, ref this.MaxTurns, ref flag, ref flag2);
			if (flag)
			{
				this.Finished = true;
				this.Abort = true;
			}
			int num = Math.Max(this.Difficulty * 6 / 5, 4);
			int num2 = 4;
			if (this.Difficulty >= 4)
			{
				num2++;
			}
			if (this.Difficulty >= 7)
			{
				num2++;
			}
			if (num2 > FormalWaterRitualSifrah.slotConfigs.Count)
			{
				num2 = FormalWaterRitualSifrah.slotConfigs.Count;
			}
			List<SifrahToken> list = new List<SifrahToken>(num);
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
			RitualSifrahTokenLiquid ritualSifrahTokenLiquid = null;
			if (list.Count < num)
			{
				ritualSifrahTokenLiquid = new RitualSifrahTokenLiquid(ContextObject, true);
				list.Add(ritualSifrahTokenLiquid);
			}
			if (list.Count < num && SocialSifrahTokenThePowerOfLove.IsAvailable())
			{
				list.Add(new RitualSifrahTokenThePowerOfLove());
			}
			if (list.Count < num && this.Rating >= this.Difficulty)
			{
				list.Add(new RitualSifrahTokenInvokeAncientCompacts());
			}
			if (list.Count < num && The.Player.HasSkill("Customs_Sharer"))
			{
				list.Add(new RitualSifrahTokenSingAHistoricalEpic());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Sed"))
			{
				list.Add(new RitualSifrahTokenTenfoldPathSed());
			}
			int num3 = num - list.Count;
			if (num3 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (this.Rating >= this.Difficulty / 2 && ContextObject.Respires)
				{
					list2.Add(new SocialSifrahTokenHookah());
				}
				RitualSifrahTokenLiquid ritualSifrahTokenLiquid2 = new RitualSifrahTokenLiquid(ContextObject, false);
				if (ritualSifrahTokenLiquid == null || ritualSifrahTokenLiquid.LiquidID != ritualSifrahTokenLiquid2.LiquidID)
				{
					list2.Add(ritualSifrahTokenLiquid2);
				}
				if (this.Rating >= this.Difficulty / 2 && (this.Rating >= this.Difficulty * 2 || ContextObject.IsMemberOfFaction("Mechanimists") || ContextObject.IsMemberOfFaction("MechaSub") || Factions.GetFeelingFactionToObject("Mechanimists", ContextObject, null) >= 0))
				{
					list.Add(new SocialSifrahTokenReadFromTheCanticlesChromaic());
				}
				if (ContextObject.HasPart<Robot>())
				{
					foreach (BitType bitType in BitType.BitTypes)
					{
						if (bitType.Level > this.Difficulty)
						{
							break;
						}
						list2.Add(new SocialSifrahTokenBit(bitType));
					}
					list2.Add(new SocialSifrahTokenCharge(this.Difficulty * 1000));
				}
				RitualSifrahTokenGift appropriate = RitualSifrahTokenGift.GetAppropriate(ContextObject);
				if (appropriate != null)
				{
					list2.Add(appropriate);
				}
				RitualSifrahTokenFood appropriate2 = RitualSifrahTokenFood.GetAppropriate(ContextObject);
				if (appropriate2 != null)
				{
					list2.Add(appropriate2);
				}
				List<Worshippable> worshippables = Factions.GetWorshippables();
				foreach (Worshippable worshippable in worshippables)
				{
					if (worshippable.GetRelevance(ContextObject, this.Rating) >= this.Difficulty * 2)
					{
						list2.Add(new RitualSifrahTokenInvokeHigherBeing(worshippable, worshippables));
					}
				}
				if (!The.Player.HasEffect<Lovesick>() && The.Player.CanApplyEffect("Lovesick", 0, null) && The.Player.IsOrganic)
				{
					int num4 = Math.Max(10 + (this.Difficulty - this.Rating) * 5, 5);
					if (num4 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectLovesick(num4));
					}
				}
				if (!The.Player.HasEffect<Shamed>() && The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num5 = Math.Max(20 + (this.Difficulty - this.Rating) * 5, 5);
					if (num5 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShamed(num5));
					}
				}
				if (!The.Player.HasEffect<Dazed>() && !The.Player.HasEffect<Stun>() && The.Player.CanApplyEffect("Dazed", 0, null))
				{
					int num6 = Math.Max(50 + (this.Difficulty - this.Rating) * 5, 5);
					if (num6 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectDazed(num6));
					}
				}
				if (!The.Player.HasEffect<Shaken>() && The.Player.CanApplyEffect("Shaken", 0, null))
				{
					int num7 = Math.Max(20 + (this.Difficulty - this.Rating) * 5, 5);
					if (num7 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShaken(num7));
					}
				}
				if (this.Rating >= 2 && !The.Player.HasEffect<Exhausted>() && The.Player.CanApplyEffect("Exhausted", 0, null))
				{
					int num8 = Math.Max(30 + (this.Difficulty - this.Rating) * 5, 5);
					if (num8 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectExhausted(num8));
					}
				}
				if (this.Rating >= 4 && !The.Player.HasEffect<Asleep>() && The.Player.CanApplyEffect("Asleep", 0, "CanApplySleep"))
				{
					int num9 = Math.Max(30 + (this.Difficulty - this.Rating) * 5, 5);
					if (num9 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectAsleep(num9));
					}
				}
				if (this.Rating >= 8 && !The.Player.HasEffect<ShatterMentalArmor>() && The.Player.CanApplyEffect("ShatterMentalArmor", 0, null))
				{
					int num10 = Math.Max(40 + (this.Difficulty - this.Rating) * 5, 5);
					if (num10 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShatterMentalArmor(num10));
					}
				}
				SocialSifrah.AddTokenTokens(list2, ContextObject);
				list2.Add(new SocialSifrahTokenSecret(ContextObject));
				base.AssignPossibleTokens(list2, list, num3, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(FormalWaterRitualSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for performing the formal water ritual with " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", giving you no chance of doing so well. You can remedy this situation by improving your Ego or by obtaining items useful in such a ritual.", true, true, true);
				this.Finished = true;
				this.Abort = true;
			}
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0019F06C File Offset: 0x0019D26C
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish the formal water ritual as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish the formal water ritual as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x06005692 RID: 22162 RVA: 0x0019F0E1 File Offset: 0x0019D2E1
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x06005693 RID: 22163 RVA: 0x0019F0FC File Offset: 0x0019D2FC
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

		// Token: 0x06005694 RID: 22164 RVA: 0x0019F25C File Offset: 0x0019D45C
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			Popup.Show("Your abysmal ritual performance deeply shames you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			The.Player.ApplyEffect(new Shamed(Stat.Random(100, 500)), null);
			this.Performance = 10;
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x0019F298 File Offset: 0x0019D498
		public virtual void ResultFailure(GameObject ContextObject)
		{
			Popup.Show("Your performance of the formal water ritual was adequate, if barely.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 50;
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x0019F2B7 File Offset: 0x0019D4B7
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the formal water ritual was passable.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 100;
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x0019F2D6 File Offset: 0x0019D4D6
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the formal water ritual was solemn and dignified.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 150;
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x0019F2F8 File Offset: 0x0019D4F8
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the formal water ritual was sublime and inspiring.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 300;
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x0019F31C File Offset: 0x0019D51C
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
			double num4 = 0.03 + (double)this.Powerup * 0.01;
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

		// Token: 0x04002BA8 RID: 11176
		public int Difficulty;

		// Token: 0x04002BA9 RID: 11177
		public int Rating;

		// Token: 0x04002BAA RID: 11178
		public int Performance;

		// Token: 0x04002BAB RID: 11179
		public bool Abort;

		// Token: 0x04002BAC RID: 11180
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("opening the ceremony", "Items/sw_cyber_reactive_cranial_plating.bmp", "\u000f", "&B", 'y', 1),
			new SifrahSlotConfiguration("cleansing the space", "Items/sw_skin_glitter.bmp", "\u0015", "&K", 'y', 4),
			new SifrahSlotConfiguration("preparing the sacrament", "Items/sw_shawl.bmp", "\n", "&Y", 'W', 3),
			new SifrahSlotConfiguration("consummating the rite", "Items/sw_urn_1.bmp", "é", "&Y", 'B', 0),
			new SifrahSlotConfiguration("honoring the space", "Items/ms_face_heart.png", "§", "&y", 'M', 5),
			new SifrahSlotConfiguration("closing the ceremony", "Items/sw_orb.bmp", "\a", "&B", 'b', 2)
		};
	}
}
