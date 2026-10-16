using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200088E RID: 2190
	[Serializable]
	public class ItemNamingSifrah : RitualSifrah
	{
		// Token: 0x060056BE RID: 22206 RVA: 0x001A1738 File Offset: 0x0019F938
		public ItemNamingSifrah(GameObject ContextObject, int Rating, int Difficulty)
		{
			this.Description = "Ritually naming " + ContextObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false);
			this.MaxTurns = 3;
			bool flag = false;
			bool flag2 = false;
			GetRitualSifrahSetupEvent.GetFor(The.Player, ContextObject, "ItemNaming", false, ref Difficulty, ref Rating, ref this.MaxTurns, ref flag, ref flag2);
			if (flag)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.Rating = Rating;
			this.Difficulty = Difficulty + ContextObject.GetIntProperty("ItemNamingSifrahDifficultyModifier", 0);
			int num = Math.Max(Difficulty * 4 / 3, 4);
			int num2 = 4;
			if (Difficulty >= 5)
			{
				num2++;
			}
			if (Difficulty >= 8)
			{
				num2++;
			}
			if (num2 > ItemNamingSifrah.slotConfigs.Count)
			{
				num2 = ItemNamingSifrah.slotConfigs.Count;
			}
			List<SifrahToken> list = new List<SifrahToken>(num);
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
			if (list.Count < num && SocialSifrahTokenThePowerOfLove.IsAvailable())
			{
				list.Add(new RitualSifrahTokenThePowerOfLove());
			}
			if (list.Count < num && Rating >= Difficulty)
			{
				list.Add(new RitualSifrahTokenInvokeAncientCompacts());
			}
			if (list.Count < num && The.Player.Stat("Level", 0) >= Difficulty * 5)
			{
				list.Add(new RitualSifrahTokenRecountAccomplishments());
			}
			if (list.Count < num && !The.Player.HasEquippedItem("Cyclopean Prism"))
			{
				list.Add(new RitualSifrahTokenPrayHumbly());
			}
			if (list.Count < num && The.Player.HasSkill("Customs_Sharer"))
			{
				list.Add(new RitualSifrahTokenSingHymn());
			}
			if (list.Count < num && flag2)
			{
				list.Add(new TinkeringSifrahTokenPsychometry("recount psychic impressions of item's history"));
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Sed"))
			{
				list.Add(new RitualSifrahTokenTenfoldPathSed());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Hok"))
			{
				list.Add(new RitualSifrahTokenTenfoldPathHok());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Bin"))
			{
				list.Add(new RitualSifrahTokenTenfoldPathBin());
			}
			int num3 = num - list.Count;
			if (num3 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Rating >= Difficulty / 2 && The.Player.Respires)
				{
					list2.Add(new RitualSifrahTokenHookah());
				}
				if (Rating >= Difficulty + 1)
				{
					list2.Add(new RitualSifrahTokenLiquid("water"));
				}
				if (Rating >= Difficulty)
				{
					list2.Add(new RitualSifrahTokenLiquid("blood"));
				}
				if (Rating >= Difficulty - 1)
				{
					list2.Add(new RitualSifrahTokenLiquid("honey"));
				}
				list2.Add(new RitualSifrahTokenReadFromTheCanticlesChromaic());
				if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor(ContextObject))
				{
					list2.Add(new RitualSifrahTokenCreationKnowledge(ContextObject));
				}
				list2.Add(new RitualSifrahTokenLiquid("brainbrine"));
				list2.Add(new RitualSifrahTokenLiquid("neutronflux"));
				list2.Add(new RitualSifrahTokenLiquid("sunslag"));
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Difficulty)
					{
						break;
					}
					list2.Add(new RitualSifrahTokenBit(bitType));
				}
				list2.Add(new RitualSifrahTokenCharge(Math.Min(Difficulty * 1000, 10000)));
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
				list2.Add(new RitualSifrahTokenScourging());
				List<Worshippable> worshippables = Factions.GetWorshippables();
				foreach (Worshippable worshippable in worshippables)
				{
					if (worshippable.GetRelevance(ContextObject, Rating) >= Difficulty * 2)
					{
						list2.Add(new RitualSifrahTokenInvokeHigherBeing(worshippable, worshippables));
					}
				}
				if (!The.Player.HasEffect<Shamed>() && The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num4 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num4 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShamed(num4));
					}
				}
				if (!The.Player.HasEffect<Dazed>() && !The.Player.HasEffect<Stun>() && The.Player.CanApplyEffect("Dazed", 0, null))
				{
					int num5 = Math.Max(50 + (Difficulty - Rating) * 5, 5);
					if (num5 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectDazed(num5));
					}
				}
				if (!The.Player.HasEffect<Shaken>() && The.Player.CanApplyEffect("Shaken", 0, null))
				{
					int num6 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num6 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShaken(num6));
					}
				}
				if (Rating >= 4 && !The.Player.HasEffect<Exhausted>() && The.Player.CanApplyEffect("Exhausted", 0, null))
				{
					int num7 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num7 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectExhausted(num7));
					}
				}
				if (Rating >= 5 && !The.Player.HasEffect<Terrified>() && The.Player.CanApplyEffect("Terrified", 0, "CanApplyFear"))
				{
					int num8 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num8 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectTerrified(num8));
					}
				}
				if (Rating >= 6 && !The.Player.HasEffect<Asleep>() && The.Player.CanApplyEffect("Asleep", 0, "CanApplySleep"))
				{
					int num9 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num9 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectAsleep(num9));
					}
				}
				if (Rating >= 7 && !The.Player.HasEffect<Disoriented>() && The.Player.CanApplyEffect("Disoriented", 0, null))
				{
					int num10 = Math.Max(25 + (Difficulty - Rating) * 10, 5);
					if (num10 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectDisoriented(num10));
					}
				}
				if (Rating >= 4 && !The.Player.HasEffect<Confused>() && The.Player.CanApplyEffect("Confused", 0, null))
				{
					int num11 = Math.Max(20 + (Difficulty - Rating) * 10, 5);
					if (num11 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectConfused(num11));
					}
				}
				if (Rating >= 2 && !The.Player.HasEffect<Bleeding>() && The.Player.CanApplyEffect("Bleeding", 0, null))
				{
					int num12 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num12 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectBleeding(num12));
					}
				}
				if (Rating >= 2 && !The.Player.HasEffect<Poisoned>() && The.Player.CanApplyEffect("Poisoned", 0, "CanApplyPoison"))
				{
					int num13 = Math.Max(10 + (Difficulty - Rating) / 2 * 5, 5);
					if (num13 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectPoisoned(num13));
					}
				}
				if (Rating >= 3 && !The.Player.HasEffect<Ill>() && !The.Player.HasEffect<Poisoned>() && The.Player.CanApplyEffect("Ill", 0, null))
				{
					int num14 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num14 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectIll(num14));
					}
				}
				if (Rating >= 5 && !The.Player.HasEffect<CardiacArrest>() && The.Player.CanApplyEffect("CardiacArrest", 0, null))
				{
					int num15 = Math.Max(10 + (Difficulty - Rating) / 2 * 5, 5);
					if (num15 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectCardiacArrest(num15));
					}
				}
				if (Rating >= 12 && !The.Player.HasEffect<ShatterMentalArmor>() && The.Player.CanApplyEffect("ShatterMentalArmor", 0, null))
				{
					int num16 = Math.Max(40 + (Difficulty - Rating) * 5, 5);
					if (num16 <= 120)
					{
						list2.Add(new RitualSifrahTokenEffectShatterMentalArmor(num16));
					}
				}
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Ego"));
				base.AssignPossibleTokens(list2, list, num3, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(ItemNamingSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for ritually naming " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", giving you no chance of performing well. You can remedy this situation by improving your Ego, Willpower, and esoteric skills, or by obtaining items useful in ritual.", true, true, true);
				this.Finished = true;
				this.Abort = true;
			}
		}

		// Token: 0x060056BF RID: 22207 RVA: 0x001A1FB0 File Offset: 0x001A01B0
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish the naming ritual as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish the naming ritual as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x060056C0 RID: 22208 RVA: 0x001A2025 File Offset: 0x001A0225
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x001A2040 File Offset: 0x001A0240
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

		// Token: 0x060056C2 RID: 22210 RVA: 0x001A21A0 File Offset: 0x001A03A0
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			Popup.Show("Your abysmal ritual performance deeply shames you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			The.Player.ApplyEffect(new Shamed(Stat.Random(100, 500)), null);
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x001A21D4 File Offset: 0x001A03D4
		public virtual void ResultFailure(GameObject ContextObject)
		{
			Popup.Show("Your performance of the naming ritual was adequate, if barely.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x060056C4 RID: 22212 RVA: 0x001A21EB File Offset: 0x001A03EB
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the naming ritual was passable.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.BasicBestowals++;
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x001A2210 File Offset: 0x001A0410
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the naming ritual was solemn and dignified.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.BasicBestowals++;
			this.ElementBestowal = true;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x001A223C File Offset: 0x001A043C
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			Popup.Show("Your performance of the naming ritual was sublime and inspiring.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.BasicBestowals += Stat.Random(2, 4);
			this.ElementBestowal = true;
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x001A2270 File Offset: 0x001A0470
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

		// Token: 0x04002BC4 RID: 11204
		public int Difficulty;

		// Token: 0x04002BC5 RID: 11205
		public int Rating;

		// Token: 0x04002BC6 RID: 11206
		public int BasicBestowals;

		// Token: 0x04002BC7 RID: 11207
		public bool ElementBestowal;

		// Token: 0x04002BC8 RID: 11208
		public bool Abort;

		// Token: 0x04002BC9 RID: 11209
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("opening the circle", "Items/sw_cyber_reactive_cranial_plating.bmp", "\u000f", "&B", 'y', 0),
			new SifrahSlotConfiguration("cleansing the circle", "Items/sw_cyber_medassist_module.bmp", "\u0015", "&y", 'K', 4),
			new SifrahSlotConfiguration("calling the quarters", "Items/sw_cyber_anchor_spikes.bmp", "\n", "&Y", 'W', 5),
			new SifrahSlotConfiguration("making the petition", "Items/sw_mannequin.bmp", "\u0014", "&y", 'W', 2),
			new SifrahSlotConfiguration("giving thanks", "Items/ms_face_heart.png", "§", "&y", 'M', 3),
			new SifrahSlotConfiguration("closing the circle", "Items/sw_orb.bmp", "\a", "&B", 'b', 1)
		};
	}
}
