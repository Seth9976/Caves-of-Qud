using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008B8 RID: 2232
	[Serializable]
	public class RepairSifrah : TinkeringSifrah
	{
		// Token: 0x06005733 RID: 22323 RVA: 0x001A6D9C File Offset: 0x001A4F9C
		public RepairSifrah(GameObject ContextObject, int Complexity, int Difficulty, int RepairRating)
		{
			string text = "Repairing ";
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
			this.Complexity = Complexity;
			this.Difficulty = Difficulty;
			this.RepairRating = RepairRating;
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
			if (num2 > RepairSifrah.slotConfigs.Count)
			{
				num2 = RepairSifrah.slotConfigs.Count;
			}
			this.MaxTurns = 3;
			bool flag13 = false;
			bool flag14 = false;
			int num3 = 0;
			int @for = GetTinkeringBonusEvent.GetFor(The.Player, ContextObject, "Repair", this.MaxTurns, 0, ref num3, ref flag13, ref flag14, true, true);
			if (flag13)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.MaxTurns += @for;
			if (num3 != 0)
			{
				RepairRating += num3;
				this.RepairRating = RepairRating;
			}
			List<SifrahToken> list = new List<SifrahToken>(num);
			if (list.Count < num && (Complexity < 4 || RepairRating > 10 + Complexity * 2))
			{
				list.Add(new TinkeringSifrahTokenPhysicalManipulation());
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Hok"))
			{
				list.Add(new TinkeringSifrahTokenTenfoldPathHok());
			}
			if (list.Count < num && The.Player.HasPart<Telekinesis>() && (Complexity < 3 || RepairRating > Difficulty + Complexity * 2))
			{
				list.Add(new TinkeringSifrahTokenTelekinesis());
			}
			int num4 = num - list.Count;
			if (num4 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Complexity < 5 || RepairRating > 5 + Complexity * 3)
				{
					list2.Add(new TinkeringSifrahTokenToolkit());
				}
				if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor(ContextObject))
				{
					list2.Add(new TinkeringSifrahTokenCreationKnowledge(ContextObject));
				}
				list2.Add(new TinkeringSifrahTokenComputePower((Complexity + Difficulty) * (Complexity + Difficulty)));
				list2.Add(new TinkeringSifrahTokenCharge(Math.Max(Math.Min(250 * (Complexity + Difficulty), 10000), 250)));
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Complexity)
					{
						break;
					}
					list2.Add(new TinkeringSifrahTokenBit(bitType));
				}
				list2.Add(new TinkeringSifrahTokenLiquid("oil"));
				list2.Add(new TinkeringSifrahTokenLiquid("gel"));
				list2.Add(new TinkeringSifrahTokenLiquid("acid"));
				list2.Add(new TinkeringSifrahTokenCopperWire());
				base.AssignPossibleTokens(list2, list, num4, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(RepairSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				string text4 = "You have no usable options to employ for repairing ";
				int maxValue2 = int.MaxValue;
				string text5 = null;
				string text6 = null;
				bool flag15 = false;
				bool flag16 = true;
				bool flag17 = false;
				bool flag18 = false;
				bool flag19 = false;
				bool flag20 = true;
				bool flag21 = true;
				bool flag22 = false;
				string text7 = null;
				bool flag23 = false;
				bool flag24 = true;
				bool flag25 = false;
				flag12 = null;
				Popup.ShowFail(text4 + ContextObject.t(maxValue2, text5, text6, flag15, flag16, flag17, flag18, flag19, flag20, flag21, flag22, text7, flag23, flag24, flag25, flag12, true, null, false) + ", giving you no chance of success. You can remedy this situation by improving your Intelligence and tinkering skills, or by obtaining items useful for tinkering.", true, true, true);
				this.Finished = true;
				this.Abort = true;
				return;
			}
			flag12 = new bool?(true);
			int? num5 = new int?(this.Slots.Count);
			int? num6 = new int?(this.Tokens.Count);
			if (base.GetGameCount(null, num5, num6, flag12, null) > 0)
			{
				this.AsMaster = true;
			}
			else
			{
				num6 = new int?(this.Slots.Count);
				num5 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome = null;
				int? num7 = num6;
				int? num8 = num5;
				flag12 = null;
				bool? flag26 = flag12;
				flag12 = null;
				int gameCount = base.GetGameCount(gameOutcome, num7, num8, flag26, flag12);
				num5 = new int?(this.Slots.Count);
				num6 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome2 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.Failure);
				int? num9 = num5;
				int? num10 = num6;
				flag12 = null;
				bool? flag27 = flag12;
				flag12 = null;
				int gameCount2 = base.GetGameCount(gameOutcome2, num9, num10, flag27, flag12);
				num6 = new int?(this.Slots.Count);
				num5 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome3 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.CriticalFailure);
				int? num11 = num6;
				int? num12 = num5;
				flag12 = null;
				bool? flag28 = flag12;
				flag12 = null;
				int gameCount3 = base.GetGameCount(gameOutcome3, num11, num12, flag28, flag12);
				int num13 = this.Tokens.Count + gameCount2 / 2 + gameCount3 + gameCount / 20;
				num5 = new int?(this.Slots.Count);
				num6 = new int?(this.Tokens.Count);
				flag12 = new bool?(true);
				SifrahGame.GameOutcome? gameOutcome4 = null;
				int? num14 = num5;
				int? num15 = num6;
				bool? flag29 = null;
				int gameCount4 = base.GetGameCount(gameOutcome4, num14, num15, flag29, flag12);
				flag12 = new bool?(true);
				flag29 = new bool?(false);
				SifrahGame.GameOutcome? gameOutcome5 = null;
				num6 = null;
				int? num16 = num6;
				num6 = null;
				int gameCount5 = base.GetGameCount(gameOutcome5, num16, num6, flag29, flag12);
				if (gameCount4 + gameCount5 / 50 >= num13)
				{
					this.AsMaster = true;
				}
			}
			if (this.AsMaster)
			{
				string sifrahRepairAuto = Options.SifrahRepairAuto;
				if (sifrahRepairAuto == "Always")
				{
					this.Finished = true;
					this.Bypass = true;
					return;
				}
				if (!(sifrahRepairAuto == "Never"))
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered repairs of this complexity. Do you want to perform detailed repairs anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically succeed at the repairs.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x06005734 RID: 22324 RVA: 0x001A7320 File Offset: 0x001A5520
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
			string text = "Do you want to try to complete the repair in its current state?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the repair, but will still have ",
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

		// Token: 0x06005735 RID: 22325 RVA: 0x001A73F8 File Offset: 0x001A55F8
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			string text = "You have no more usable options. Do you want to try to complete the repair in its current state?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the repair, but will still have ",
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

		// Token: 0x06005736 RID: 22326 RVA: 0x001A74A8 File Offset: 0x001A56A8
		public override void Finish(GameObject ContextObject)
		{
			GameObject gameObject = GameObject.FindByID(this.HandlerID);
			if (gameObject == null)
			{
				return;
			}
			IRepairSifrahHandler repairSifrahHandler = gameObject.GetPart(this.HandlerPartName) as IRepairSifrahHandler;
			if (repairSifrahHandler == null)
			{
				return;
			}
			if (this.Bypass)
			{
				base.PlayOutcomeSound(SifrahGame.GameOutcome.Success);
				repairSifrahHandler.RepairResultSuccess(The.Player, ContextObject);
				return;
			}
			if (base.PercentSolved == 100)
			{
				ContextObject.ModIntProperty("ItemNamingBonus", 1, false);
			}
			if (!this.Abort)
			{
				SifrahGame.GameOutcome gameOutcome = base.DetermineOutcome();
				base.PlayOutcomeSound(gameOutcome);
				bool flag = false;
				if (gameOutcome == SifrahGame.GameOutcome.CriticalFailure)
				{
					repairSifrahHandler.RepairResultCriticalFailure(The.Player, ContextObject);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Failure)
				{
					repairSifrahHandler.RepairResultFailure(The.Player, ContextObject);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.PartialSuccess)
				{
					repairSifrahHandler.RepairResultPartialSuccess(The.Player, ContextObject);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Success)
				{
					if (base.PercentSolved >= 100)
					{
						flag = true;
					}
					repairSifrahHandler.RepairResultSuccess(The.Player, ContextObject);
					int num = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty);
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
					if (base.PercentSolved >= 100)
					{
						flag = true;
					}
					repairSifrahHandler.RepairResultExceptionalSuccess(The.Player, ContextObject);
					TinkeringSifrah.AwardInsight();
					int num2 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty) * 3;
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
				SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, this.AsMaster, flag);
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
					repairSifrahHandler.RepairResultCriticalFailure(The.Player, ContextObject);
					this.RequestInterfaceExit();
					AutoAct.Interrupt(null, null, null, false);
				}
			}
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x001A772C File Offset: 0x001A592C
		public override void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure)
		{
			double num = (double)(this.RepairRating + base.PercentSolved) * 0.01;
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
			double num4 = 0.01;
			if (this.Turn > 1)
			{
				num4 += 0.01 * (double)(this.MaxTurns - this.Turn);
			}
			if (this.AsMaster)
			{
				num4 *= 4.0;
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

		// Token: 0x06005738 RID: 22328 RVA: 0x001A78B1 File Offset: 0x001A5AB1
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002BEA RID: 11242
		public int Complexity;

		// Token: 0x04002BEB RID: 11243
		public int Difficulty;

		// Token: 0x04002BEC RID: 11244
		public int RepairRating;

		// Token: 0x04002BED RID: 11245
		public bool Abort;

		// Token: 0x04002BEE RID: 11246
		public bool AsMaster;

		// Token: 0x04002BEF RID: 11247
		public bool Bypass;

		// Token: 0x04002BF0 RID: 11248
		public bool InterfaceExitRequested;

		// Token: 0x04002BF1 RID: 11249
		public string HandlerID;

		// Token: 0x04002BF2 RID: 11250
		public string HandlerPartName;

		// Token: 0x04002BF3 RID: 11251
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("replacing gearworks", "Items/sw_gears2.bmp", "é", "&c", 'c', 0),
			new SifrahSlotConfiguration("reconnecting spark vines", "Items/sw_copper_wire.bmp", "í", "&W", 'G', 0),
			new SifrahSlotConfiguration("restoring humour flows", "Items/sw_sparktick_plasma.bmp", "÷", "&Y", 'y', 0),
			new SifrahSlotConfiguration("sealing glow modules", "Items/sw_lens.bmp", "\u001f", "&B", 'b', 0),
			new SifrahSlotConfiguration("appeasing spirits", "Items/sw_computer.bmp", "Ñ", "&c", 'G', 0),
			new SifrahSlotConfiguration("invoking the beyond", "Items/sw_wind_turbine_3.bmp", "ì", "&m", 'K', 0)
		};
	}
}
