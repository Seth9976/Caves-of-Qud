using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x0200088D RID: 2189
	[Serializable]
	public class ItemModdingSifrah : TinkeringSifrah
	{
		// Token: 0x060056B2 RID: 22194 RVA: 0x001A0BE8 File Offset: 0x0019EDE8
		public ItemModdingSifrah(GameObject ContextObject, int Complexity, int Difficulty, int Rating)
		{
			this.Description = "Modding " + ContextObject.an(int.MaxValue, null, null, false, true, false, false, false, false, false, false, false, true, false, null, false, null, false);
			this.Complexity = Complexity;
			this.Difficulty = Difficulty;
			this.Rating = Rating;
			int num = 3 + Complexity + Difficulty;
			int num2 = 4;
			if (Complexity >= 3)
			{
				num2++;
			}
			if (Complexity >= 7)
			{
				num2++;
				num += 4;
			}
			if (num2 > ItemModdingSifrah.slotConfigs.Count)
			{
				num2 = ItemModdingSifrah.slotConfigs.Count;
			}
			this.MaxTurns = 3;
			bool flag = false;
			bool flag2 = false;
			int num3 = 0;
			int @for = GetTinkeringBonusEvent.GetFor(The.Player, ContextObject, "ItemModding", this.MaxTurns, 0, ref num3, ref flag, ref flag2, true, true);
			if (flag)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.MaxTurns += @for;
			if (num3 != 0)
			{
				Rating += num3;
				this.Rating = Rating;
			}
			List<SifrahToken> list = new List<SifrahToken>(num);
			if (list.Count < num && Rating > 10 + Complexity * 3 + Difficulty * 2)
			{
				list.Add(new TinkeringSifrahTokenPhysicalManipulation());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Hok"))
			{
				list.Add(new TinkeringSifrahTokenTenfoldPathHok());
			}
			if (list.Count < num && The.Player.HasPart<Telekinesis>() && (Complexity < 3 || Rating > Difficulty + Complexity * 2))
			{
				list.Add(new TinkeringSifrahTokenTelekinesis());
			}
			int num4 = num - list.Count;
			if (num4 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Rating > 5 + Complexity * 3 + Difficulty)
				{
					list2.Add(new TinkeringSifrahTokenAdvancedToolkit());
				}
				if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor(ContextObject))
				{
					list2.Add(new TinkeringSifrahTokenCreationKnowledge(ContextObject));
				}
				list2.Add(new TinkeringSifrahTokenComputePower((Complexity + Difficulty) * (Complexity + Difficulty)));
				list2.Add(new TinkeringSifrahTokenCharge(Math.Min(1000 * (Complexity + Difficulty), 10000)));
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Complexity + Difficulty)
					{
						break;
					}
					list2.Add(new TinkeringSifrahTokenBit(bitType));
				}
				if (Rating > 5 + Complexity * 3 + Difficulty)
				{
					list2.Add(new TinkeringSifrahTokenLiquid("oil"));
				}
				if (Rating > 5 + Complexity * 4 + Difficulty * 2)
				{
					list2.Add(new TinkeringSifrahTokenLiquid("gel"));
				}
				if (Rating > 5 + Complexity * 5 + Difficulty * 3)
				{
					list2.Add(new TinkeringSifrahTokenLiquid("acid"));
				}
				if (Rating > 5 + Complexity + Difficulty / 2)
				{
					list2.Add(new TinkeringSifrahTokenLiquid("lava"));
				}
				list2.Add(new TinkeringSifrahTokenLiquid("brainbrine"));
				list2.Add(new TinkeringSifrahTokenLiquid("neutronflux"));
				list2.Add(new TinkeringSifrahTokenLiquid("sunslag"));
				list2.Add(new TinkeringSifrahTokenCopperWire());
				base.AssignPossibleTokens(list2, list, num4, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(ItemModdingSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for modding " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", giving you no chance of success. You can remedy this situation by improving your Intelligence and tinkering skills, or by obtaining items useful for tinkering.", true, true, true);
				this.Finished = true;
				this.Abort = true;
			}
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x001A0F78 File Offset: 0x0019F178
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			if (num <= 0 && num4 <= 0 && this.Turn == 1)
			{
				this.Abort = true;
				return true;
			}
			string text = "Do you want to try to complete the mod in its current state?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the mod, but will still have ",
						Grammar.IndefiniteArticleShouldBeAn(num7) ? "an" : "a",
						" {{C|",
						num7.ToString(),
						"%}} chance of causing a critical failure."
					});
				}
			}
			DialogResult dialogResult = Popup.ShowYesNoCancel(text, "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x060056B4 RID: 22196 RVA: 0x001A1050 File Offset: 0x0019F250
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			string text = "You have no more usable options. Do you want to try to complete the mod in its current state?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the mod, but will still have ",
						Grammar.IndefiniteArticleShouldBeAn(num7) ? "an" : "a",
						" {{C|",
						num7.ToString(),
						"%}} chance of causing a critical failure."
					});
				}
			}
			if (Popup.ShowYesNo(text, "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
			{
				this.Abort = true;
			}
			return true;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x001A1100 File Offset: 0x0019F300
		public override void Finish(GameObject ContextObject)
		{
			if (base.PercentSolved == 100)
			{
				ContextObject.ModIntProperty("ItemNamingBonus", 1, false);
			}
			if (!this.Abort)
			{
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
					int num = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty) * 2;
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
					this.ResultCriticalSuccess(ContextObject);
					TinkeringSifrah.AwardInsight();
					int num2 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty) * 10;
					if (base.PercentSolved < 100)
					{
						num2 = num2 * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
					if (num2 > 0)
					{
						The.Player.AwardXP(num2, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
					}
					ContextObject.ModIntProperty("ItemNamingBonus", (base.PercentSolved == 100) ? 2 : 1, false);
				}
				SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, false, false);
				return;
			}
			if (this.Turn > 1)
			{
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				this.CalculateOutcomeChances(out num3, out num4, out num5, out num6, out num7);
				int num8 = this.MaxTurns - this.Turn + 1;
				if ((num7 - num8).in100())
				{
					this.ResultCriticalFailure(ContextObject);
					this.RequestInterfaceExit();
					AutoAct.Interrupt(null, null, null, false);
				}
			}
		}

		// Token: 0x060056B6 RID: 22198 RVA: 0x001A12FC File Offset: 0x0019F4FC
		private void ResultCriticalFailure(GameObject ContextObject)
		{
			if (The.Player.HasPart<Dystechnia>())
			{
				Dystechnia.CauseExplosion(ContextObject, The.Player, null);
				this.RequestInterfaceExit();
				return;
			}
			Popup.Show("Your work applied the mod, but catastrophically poorly.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			ContextObject.ApplyEffect(new Broken(), null);
			this.Performance = -Stat.Random(1, 4);
			this.ApplyMod = true;
		}

		// Token: 0x060056B7 RID: 22199 RVA: 0x001A1360 File Offset: 0x0019F560
		private void ResultFailure(GameObject ContextObject)
		{
			Popup.Show("You abjectly failed to mod " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 0;
		}

		// Token: 0x060056B8 RID: 22200 RVA: 0x001A13B8 File Offset: 0x0019F5B8
		private void ResultPartialSuccess(GameObject ContextObject)
		{
			Popup.Show("Your work modding " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " was passable.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = 0;
			this.ApplyMod = true;
		}

		// Token: 0x060056B9 RID: 22201 RVA: 0x001A1418 File Offset: 0x0019F618
		private void ResultSuccess(GameObject ContextObject)
		{
			Popup.Show("Your work modding " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " was solid and craftsmanlike.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = (25.in100() ? 2 : 1);
			this.ApplyMod = true;
		}

		// Token: 0x060056BA RID: 22202 RVA: 0x001A1484 File Offset: 0x0019F684
		private void ResultCriticalSuccess(GameObject ContextObject)
		{
			Popup.Show("Your work modding " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " was outstanding.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			this.Performance = Stat.Random(3, 5);
			this.ApplyMod = true;
		}

		// Token: 0x060056BB RID: 22203 RVA: 0x001A14EC File Offset: 0x0019F6EC
		public override void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure)
		{
			double num = (double)(this.Rating + base.PercentSolved) * 0.01;
			double num2 = num;
			if (num2 < 1.0)
			{
				int i = 1;
				int num3 = this.Complexity.DiminishingReturns(1.0);
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
			double num4 = 0.02;
			if (this.Turn > 1)
			{
				num4 += 0.02 * (double)(this.MaxTurns - this.Turn);
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

		// Token: 0x060056BC RID: 22204 RVA: 0x001A164B File Offset: 0x0019F84B
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002BBC RID: 11196
		public int Complexity;

		// Token: 0x04002BBD RID: 11197
		public int Difficulty;

		// Token: 0x04002BBE RID: 11198
		public int Rating;

		// Token: 0x04002BBF RID: 11199
		public int Performance;

		// Token: 0x04002BC0 RID: 11200
		public bool Abort;

		// Token: 0x04002BC1 RID: 11201
		public bool ApplyMod;

		// Token: 0x04002BC2 RID: 11202
		public bool InterfaceExitRequested;

		// Token: 0x04002BC3 RID: 11203
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("upgrading gearworks", "Items/sw_gears2.bmp", "é", "&c", 'c', 0),
			new SifrahSlotConfiguration("retooling spark vines", "Items/sw_copper_wire.bmp", "í", "&W", 'G', 0),
			new SifrahSlotConfiguration("optimizing humour flows", "Items/sw_sparktick_plasma.bmp", "÷", "&Y", 'y', 0),
			new SifrahSlotConfiguration("enhancing glow modules", "Items/sw_lens.bmp", "\u001f", "&B", 'b', 0),
			new SifrahSlotConfiguration("befriending spirits", "Items/sw_computer.bmp", "Ñ", "&c", 'G', 0),
			new SifrahSlotConfiguration("invoking the beyond", "Items/sw_wind_turbine_3.bmp", "ì", "&m", 'K', 0)
		};
	}
}
