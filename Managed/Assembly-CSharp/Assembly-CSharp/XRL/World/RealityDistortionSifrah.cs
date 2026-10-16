using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Skills;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008B6 RID: 2230
	[Serializable]
	public class RealityDistortionSifrah : PsionicSifrah
	{
		// Token: 0x0600571C RID: 22300 RVA: 0x001A4E64 File Offset: 0x001A3064
		public RealityDistortionSifrah(GameObject ContextObject, string Subtype, string Description, int Rating, int Difficulty)
		{
			this.Description = Description;
			this.MaxTurns = 3;
			bool flag = false;
			bool flag2 = false;
			GetPsionicSifrahSetupEvent.GetFor(The.Player, ContextObject, "RealityDistortion", Subtype, false, ref Difficulty, ref Rating, ref this.MaxTurns, ref flag, ref flag2);
			if (flag)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.Rating = Rating;
			this.Difficulty = Difficulty;
			int num = Math.Max(Difficulty, 4);
			int num2 = 4;
			if (Difficulty >= 5)
			{
				num2++;
			}
			if (Difficulty >= 8)
			{
				num2++;
			}
			if (num2 > RealityDistortionSifrah.slotConfigs.Count)
			{
				num2 = RealityDistortionSifrah.slotConfigs.Count;
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
				list.Add(new PsionicSifrahTokenThePowerOfLove());
			}
			if (list.Count < num)
			{
				int num3 = 0;
				foreach (KeyValuePair<string, PowerEntry> keyValuePair in SkillFactory.Factory.PowersByClass)
				{
					if (keyValuePair.Key.StartsWith("Discipline_") && The.Player.HasSkill(keyValuePair.Value.Class))
					{
						num3++;
					}
				}
				if (num3 > 0 && num3 >= Difficulty / 2)
				{
					list.Add(new PsionicSifrahTokenDiscipline());
				}
			}
			if (list.Count < num && The.Player.Stat("Ego", 0) >= 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenExertWill());
			}
			if (list.Count < num && The.Player.Stat("Intelligence", 0) >= 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenApplyIntellect());
			}
			if (list.Count < num && The.Player.Stat("Willpower", 0) >= 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenCalmMind());
			}
			if (list.Count < num && !The.Player.HasEquippedItem("Cyclopean Prism") && The.Player.Stat("Ego", 0) < 10 + Difficulty * 3)
			{
				list.Add(new PsionicSifrahTokenPrayHumbly());
			}
			if (list.Count < num && The.Player.HasSkill("Customs_Sharer") && Rating > Difficulty)
			{
				list.Add(new PsionicSifrahTokenApplyAncientLore());
			}
			if (list.Count < num && flag2)
			{
				list.Add(new TinkeringSifrahTokenPsychometry("leverage psychic impressions of item's history"));
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Khu") && Rating > Difficulty * 5 && The.Player.Stat("Willpoer", 0) > 10 + Difficulty * 4)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathKhu());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Yis") && Rating > Difficulty * 4 && The.Player.Stat("Willpower", 0) > 10 + Difficulty * 3)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathYis());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Hod") && Rating > Difficulty * 2 && The.Player.Stat("Intelligence", 0) > 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathHod());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Tza") && Rating > Difficulty * 2 && The.Player.Stat("Ego", 0) > 10 + Difficulty * 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathTza());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Ret") && Rating > Difficulty * 3 / 2 && The.Player.Stat("Willpower", 0) > 10 + Difficulty * 3 / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathRet());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Vur") && Rating > Difficulty * 3 / 2 && The.Player.Stat("Intelligence", 0) > 10 + Difficulty * 3 / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathVur());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Sed") && Rating > Difficulty * 3 / 2 && The.Player.Stat("Ego", 0) > 10 + Difficulty * 3 / 2)
			{
				list.Add(new PsionicSifrahTokenTenfoldPathSed());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Bin"))
			{
				list.Add(new PsionicSifrahTokenTenfoldPathBin());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Hok"))
			{
				list.Add(new PsionicSifrahTokenTenfoldPathHok());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Ket"))
			{
				list.Add(new PsionicSifrahTokenTenfoldPathKet());
			}
			int num4 = num - list.Count;
			if (num4 > 0)
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
				if (ContextObject.IsCreature && !The.Player.HasEffect<Lovesick>() && The.Player.CanApplyEffect("Lovesick", 0, null) && The.Player.IsOrganic)
				{
					int num5 = Math.Max(10 + (Difficulty - Rating) * 5, 5);
					if (num5 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectLovesick(num5));
					}
				}
				if (!The.Player.HasEffect<Shamed>() && The.Player.CanApplyEffect("Shamed", 0, null))
				{
					int num6 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num6 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectShamed(num6));
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
						list2.Add(new PsionicSifrahTokenEffectShaken(num8));
					}
				}
				if (!The.Player.HasEffect<Exhausted>() && The.Player.CanApplyEffect("Exhausted", 0, null))
				{
					int num9 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num9 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectExhausted(num9));
					}
				}
				if (!The.Player.HasEffect<Terrified>() && The.Player.CanApplyEffect("Terrified", 0, "CanApplyFear"))
				{
					int num10 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num10 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectTerrified(num10));
					}
				}
				if (!The.Player.HasEffect<Asleep>() && The.Player.CanApplyEffect("Asleep", 0, "CanApplySleep"))
				{
					int num11 = Math.Max(30 + (Difficulty - Rating) * 5, 5);
					if (num11 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectAsleep(num11));
					}
				}
				if (!The.Player.HasEffect<Disoriented>() && The.Player.CanApplyEffect("Disoriented", 0, null))
				{
					int num12 = Math.Max(25 + (Difficulty - Rating) * 10, 5);
					if (num12 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectDisoriented(num12));
					}
				}
				if (!The.Player.HasEffect<Confused>() && The.Player.CanApplyEffect("Confused", 0, null))
				{
					int num13 = Math.Max(20 + (Difficulty - Rating) * 10, 5);
					if (num13 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectConfused(num13));
					}
				}
				if (!The.Player.HasEffect<Nosebleed>() && The.Player.CanApplyEffect("Nosebleed", 0, null) && The.Player.CanApplyEffect("Bleeding", 0, null))
				{
					int num14 = Math.Max(20 + (Difficulty - Rating) * 5, 5);
					if (num14 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectNosebleed(num14));
					}
				}
				if (!The.Player.HasEffect<CardiacArrest>() && The.Player.CanApplyEffect("CardiacArrest", 0, null))
				{
					int num15 = Math.Max(10 + (Difficulty - Rating) / 2 * 5, 5);
					if (num15 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectCardiacArrest(num15));
					}
				}
				if (!The.Player.HasEffect<ShatterMentalArmor>() && The.Player.CanApplyEffect("ShatterMentalArmor", 0, null))
				{
					int num16 = Math.Max(40 + (Difficulty - Rating) / 2 * 5, 5);
					if (num16 <= 120)
					{
						list2.Add(new PsionicSifrahTokenEffectShatterMentalArmor(num16));
					}
				}
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Ego"));
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Intelligence"));
				list2.Add(new RitualSifrahTokenAttributeSacrifice("Willpower"));
				base.AssignPossibleTokens(list2, list, num4, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(RealityDistortionSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for " + Description + ", giving you no chance of performing well. You can remedy this situation by improving your Ego, Willpower, Intelligence, and esoteric skills.", true, true, true);
				this.Finished = true;
				this.Abort = true;
				return;
			}
			bool? flag3 = new bool?(true);
			int? num17 = new int?(this.Slots.Count);
			int? num18 = new int?(this.Tokens.Count);
			if (base.GetGameCount(null, num17, num18, flag3, null) > 0)
			{
				this.AsMaster = true;
			}
			else
			{
				num18 = new int?(this.Slots.Count);
				num17 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome = null;
				int? num19 = num18;
				int? num20 = num17;
				flag3 = null;
				bool? flag4 = flag3;
				flag3 = null;
				int gameCount = base.GetGameCount(gameOutcome, num19, num20, flag4, flag3);
				num17 = new int?(this.Slots.Count);
				num18 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome2 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.Failure);
				int? num21 = num17;
				int? num22 = num18;
				flag3 = null;
				bool? flag5 = flag3;
				flag3 = null;
				int gameCount2 = base.GetGameCount(gameOutcome2, num21, num22, flag5, flag3);
				num18 = new int?(this.Slots.Count);
				num17 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome3 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.CriticalFailure);
				int? num23 = num18;
				int? num24 = num17;
				flag3 = null;
				bool? flag6 = flag3;
				flag3 = null;
				int gameCount3 = base.GetGameCount(gameOutcome3, num23, num24, flag6, flag3);
				int num25 = this.Tokens.Count + gameCount2 / 2 + gameCount3 + gameCount / 10;
				num17 = new int?(this.Slots.Count);
				num18 = new int?(this.Tokens.Count);
				flag3 = new bool?(true);
				SifrahGame.GameOutcome? gameOutcome4 = null;
				int? num26 = num17;
				int? num27 = num18;
				bool? flag7 = null;
				int gameCount4 = base.GetGameCount(gameOutcome4, num26, num27, flag7, flag3);
				flag3 = new bool?(true);
				flag7 = new bool?(false);
				SifrahGame.GameOutcome? gameOutcome5 = null;
				num18 = null;
				int? num28 = num18;
				num18 = null;
				int gameCount5 = base.GetGameCount(gameOutcome5, num28, num18, flag7, flag3);
				if (gameCount4 + gameCount5 / 100 >= num25)
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
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered reality distortion at this level of difficulty. Do you want to guide the process in detail anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically receive the results of strong but unexceptional performance.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x0600571D RID: 22301 RVA: 0x001A5A80 File Offset: 0x001A3C80
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			if (this.Turn != 1)
			{
				return Popup.ShowYesNo("Exiting now will finish the attempt as matters stand. Are you sure you want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
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
			DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to finish the attempt as matters stand?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x0600571E RID: 22302 RVA: 0x001A5AF5 File Offset: 0x001A3CF5
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			if (this.Turn > 1)
			{
				Popup.ShowFail("You have no more usable options, so your performance so far will determine the outcome.", true, true, true);
			}
			return true;
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x001A5B10 File Offset: 0x001A3D10
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

		// Token: 0x06005720 RID: 22304 RVA: 0x001A5CD6 File Offset: 0x001A3ED6
		public virtual void ResultCriticalFailure(GameObject ContextObject)
		{
			this.Performance = -100;
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x001A5CE0 File Offset: 0x001A3EE0
		public virtual void ResultFailure(GameObject ContextObject)
		{
			this.Performance = 0;
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x001A5CE9 File Offset: 0x001A3EE9
		public virtual void ResultPartialSuccess(GameObject ContextObject)
		{
			this.Performance = 50;
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x001A5CF3 File Offset: 0x001A3EF3
		public virtual void ResultSuccess(GameObject ContextObject)
		{
			this.Performance = 100;
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x001A5CFD File Offset: 0x001A3EFD
		public virtual void ResultExceptionalSuccess(GameObject ContextObject)
		{
			this.Performance = 300;
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x001A5D0C File Offset: 0x001A3F0C
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

		// Token: 0x06005726 RID: 22310 RVA: 0x001A5EA3 File Offset: 0x001A40A3
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002BDB RID: 11227
		public int Difficulty;

		// Token: 0x04002BDC RID: 11228
		public int Rating;

		// Token: 0x04002BDD RID: 11229
		public bool Abort;

		// Token: 0x04002BDE RID: 11230
		public bool AsMaster;

		// Token: 0x04002BDF RID: 11231
		public bool Bypass;

		// Token: 0x04002BE0 RID: 11232
		public bool InterfaceExitRequested;

		// Token: 0x04002BE1 RID: 11233
		public SifrahGame.GameOutcome Outcome;

		// Token: 0x04002BE2 RID: 11234
		public int Performance;

		// Token: 0x04002BE3 RID: 11235
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("clearing the mind", "Items/sw_cherubic.bmp", "\t", "&B", 'K', 3),
			new SifrahSlotConfiguration("forming the intention", "Items/sw_cyber_medassist_module.bmp", "\u000f", "&y", 'K', 0),
			new SifrahSlotConfiguration("focusing the will", "Items/sw_cyber_anchor_spikes.bmp", "\a", "&W", 'B', 2),
			new SifrahSlotConfiguration("charging the intention", "Items/sw_power_cut_large.png", "\u001e", "&M", 'R', 5),
			new SifrahSlotConfiguration("directing the intention", "Items/sw_esper.bmp", "\u001a", "&W", 'M', 4),
			new SifrahSlotConfiguration("letting go of the intention", "Items/sw_adrenalcontrol.bmp", "ê", "&b", 'm', 1)
		};
	}
}
