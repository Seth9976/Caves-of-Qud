using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000886 RID: 2182
	[Serializable]
	public class DisarmingSifrah : TinkeringSifrah
	{
		// Token: 0x0600567C RID: 22140 RVA: 0x0019CE38 File Offset: 0x0019B038
		public DisarmingSifrah(GameObject ContextObject, int Difficulty, int Rating, bool Auto)
		{
			string text = "Disarming ";
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
			this.Difficulty = Difficulty;
			this.Rating = Rating;
			this.Auto = Auto;
			int num = 3 + Difficulty;
			int num2 = 4;
			if (Difficulty >= 3)
			{
				num2++;
			}
			if (Difficulty >= 7)
			{
				num2++;
			}
			if (num2 > DisarmingSifrah.slotConfigs.Count)
			{
				num2 = DisarmingSifrah.slotConfigs.Count;
			}
			this.MaxTurns = 3;
			bool flag13 = false;
			bool flag14 = false;
			int @for = GetTinkeringBonusEvent.GetFor(The.Player, ContextObject, "Disarming", this.MaxTurns, 0, ref flag13, ref flag14, true, true);
			if (flag13)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.MaxTurns += @for;
			List<SifrahToken> list = new List<SifrahToken>(num);
			if (list.Count < num && (Difficulty < 3 || Rating > 20 + Difficulty * 3) && !The.Player.HasPart<Myopia>())
			{
				list.Add(new TinkeringSifrahTokenVisualInspection());
			}
			if (list.Count < num && (Difficulty < 4 || Rating > 10 + Difficulty * 2))
			{
				list.Add(new TinkeringSifrahTokenPhysicalManipulation());
			}
			if (list.Count < num && flag14)
			{
				list.Add(new TinkeringSifrahTokenPsychometry("read psychic impressions of arming process"));
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Bin"))
			{
				list.Add(new TinkeringSifrahTokenTenfoldPathBin());
			}
			if (list.Count < num)
			{
				Scanning.Scan scan = Scanning.GetScanTypeFor(ContextObject);
				if (scan == Scanning.Scan.Structure)
				{
					scan = Scanning.Scan.Tech;
				}
				if (Scanning.HasScanningFor(The.Player, scan))
				{
					list.Add(new TinkeringSifrahTokenScanning(scan));
					this.MaxTurns++;
				}
			}
			if (list.Count < num && The.Player.HasPart<Telekinesis>())
			{
				list.Add(new TinkeringSifrahTokenTelekinesis());
			}
			int num3 = num - list.Count;
			if (num3 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Difficulty < 5 || Rating > 5 + Difficulty * 3)
				{
					list2.Add(new TinkeringSifrahTokenToolkit());
				}
				list2.Add(new TinkeringSifrahTokenComputePower(Difficulty * Difficulty));
				list2.Add(new TinkeringSifrahTokenCharge(500 * Difficulty));
				Tinkering_Mine part = ContextObject.GetPart<Tinkering_Mine>();
				if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor((part != null) ? part.Explosive : null))
				{
					list2.Add(new TinkeringSifrahTokenCreationKnowledge(part.Explosive));
				}
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Difficulty)
					{
						break;
					}
					list2.Add(new TinkeringSifrahTokenBit(bitType));
				}
				list2.Add(new TinkeringSifrahTokenLiquid("oil"));
				list2.Add(new TinkeringSifrahTokenLiquid("gel"));
				list2.Add(new TinkeringSifrahTokenLiquid("acid"));
				list2.Add(new TinkeringSifrahTokenCopperWire());
				base.AssignPossibleTokens(list2, list, num3, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(DisarmingSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				string text4 = "You have no usable options to employ for disarming ";
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
			int? num4 = new int?(this.Slots.Count);
			int? num5 = new int?(this.Tokens.Count);
			if (base.GetGameCount(null, num4, num5, flag12, null) > 0)
			{
				this.AsMaster = true;
			}
			else
			{
				num5 = new int?(this.Slots.Count);
				num4 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome = null;
				int? num6 = num5;
				int? num7 = num4;
				flag12 = null;
				bool? flag26 = flag12;
				flag12 = null;
				int gameCount = base.GetGameCount(gameOutcome, num6, num7, flag26, flag12);
				num4 = new int?(this.Slots.Count);
				num5 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome2 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.Failure);
				int? num8 = num4;
				int? num9 = num5;
				flag12 = null;
				bool? flag27 = flag12;
				flag12 = null;
				int gameCount2 = base.GetGameCount(gameOutcome2, num8, num9, flag27, flag12);
				num5 = new int?(this.Slots.Count);
				num4 = new int?(this.Tokens.Count);
				SifrahGame.GameOutcome? gameOutcome3 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.CriticalFailure);
				int? num10 = num5;
				int? num11 = num4;
				flag12 = null;
				bool? flag28 = flag12;
				flag12 = null;
				int gameCount3 = base.GetGameCount(gameOutcome3, num10, num11, flag28, flag12);
				int num12 = this.Tokens.Count + gameCount2 / 2 + gameCount3 + gameCount / 15;
				num4 = new int?(this.Slots.Count);
				num5 = new int?(this.Tokens.Count);
				flag12 = new bool?(true);
				SifrahGame.GameOutcome? gameOutcome4 = null;
				int? num13 = num4;
				int? num14 = num5;
				bool? flag29 = null;
				int gameCount4 = base.GetGameCount(gameOutcome4, num13, num14, flag29, flag12);
				flag12 = new bool?(true);
				flag29 = new bool?(false);
				SifrahGame.GameOutcome? gameOutcome5 = null;
				num5 = null;
				int? num15 = num5;
				num5 = null;
				int gameCount5 = base.GetGameCount(gameOutcome5, num15, num5, flag29, flag12);
				if (gameCount4 + gameCount5 / 75 >= num12)
				{
					this.AsMaster = true;
				}
			}
			if (this.AsMaster)
			{
				string sifrahDisarmingAuto = Options.SifrahDisarmingAuto;
				if (sifrahDisarmingAuto == "Always")
				{
					this.Finished = true;
					this.Bypass = true;
					return;
				}
				if (!(sifrahDisarmingAuto == "Never"))
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered disarming operations of this complexity. Do you want to perform detailed disarming anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically succeed.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x0600567D RID: 22141 RVA: 0x0019D420 File Offset: 0x0019B620
		public static bool IsMastered(GameObject ContextObject, int Difficulty, int Rating)
		{
			int num = 3 + Difficulty;
			int num2 = 4;
			if (Difficulty >= 3)
			{
				num2++;
			}
			if (Difficulty >= 7)
			{
				num2++;
			}
			if (num2 > DisarmingSifrah.slotConfigs.Count)
			{
				num2 = DisarmingSifrah.slotConfigs.Count;
			}
			int num3 = 0;
			if ((Difficulty < 3 || Rating > 20 + Difficulty * 3) && !The.Player.HasPart<Myopia>())
			{
				num3++;
			}
			if (Difficulty < 4 || Rating > 10 + Difficulty * 2)
			{
				num3++;
			}
			if (The.Player.HasPart<Psychometry>())
			{
				num3++;
			}
			if (The.Player.HasSkill("TenfoldPath_Bin"))
			{
				num3++;
			}
			Scanning.Scan scan = Scanning.GetScanTypeFor(ContextObject);
			if (scan == Scanning.Scan.Structure)
			{
				scan = Scanning.Scan.Tech;
			}
			if (Scanning.HasScanningFor(The.Player, scan))
			{
				num3++;
			}
			if (The.Player.HasPart<Telekinesis>())
			{
				num3++;
			}
			if (Difficulty < 5 || Rating > 5 + Difficulty * 3)
			{
				num3++;
			}
			num3++;
			num3++;
			Tinkering_Mine part = ContextObject.GetPart<Tinkering_Mine>();
			if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor((part != null) ? part.Explosive : null))
			{
				num3++;
			}
			using (List<BitType>.Enumerator enumerator = BitType.BitTypes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Level > Difficulty)
					{
						break;
					}
					num3++;
				}
			}
			num3++;
			num3++;
			num3++;
			num3++;
			if (num > num3)
			{
				num = num3;
			}
			string gameType = SifrahGame.GetGameType(typeof(DisarmingSifrah));
			string text = gameType;
			bool? flag = new bool?(true);
			int? num4 = new int?(num2);
			int? num5 = new int?(num);
			if (SifrahGame.GetGameCount(text, null, num4, num5, flag, null) > 0)
			{
				return true;
			}
			string text2 = gameType;
			num5 = new int?(num2);
			num4 = new int?(num);
			SifrahGame.GameOutcome? gameOutcome = null;
			int? num6 = num5;
			int? num7 = num4;
			flag = null;
			bool? flag2 = flag;
			flag = null;
			int gameCount = SifrahGame.GetGameCount(text2, gameOutcome, num6, num7, flag2, flag);
			string text3 = gameType;
			num4 = new int?(num2);
			num5 = new int?(num);
			SifrahGame.GameOutcome? gameOutcome2 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.Failure);
			int? num8 = num4;
			int? num9 = num5;
			flag = null;
			bool? flag3 = flag;
			flag = null;
			int gameCount2 = SifrahGame.GetGameCount(text3, gameOutcome2, num8, num9, flag3, flag);
			string text4 = gameType;
			num5 = new int?(num2);
			num4 = new int?(num);
			SifrahGame.GameOutcome? gameOutcome3 = new SifrahGame.GameOutcome?(SifrahGame.GameOutcome.CriticalFailure);
			int? num10 = num5;
			int? num11 = num4;
			flag = null;
			bool? flag4 = flag;
			flag = null;
			int gameCount3 = SifrahGame.GetGameCount(text4, gameOutcome3, num10, num11, flag4, flag);
			int num12 = num + gameCount2 / 2 + gameCount3 + gameCount / 15;
			string text5 = gameType;
			num4 = new int?(num2);
			num5 = new int?(num);
			flag = new bool?(true);
			SifrahGame.GameOutcome? gameOutcome4 = null;
			int? num13 = num4;
			int? num14 = num5;
			bool? flag5 = null;
			int gameCount4 = SifrahGame.GetGameCount(text5, gameOutcome4, num13, num14, flag5, flag);
			string text6 = gameType;
			flag = new bool?(true);
			flag5 = new bool?(false);
			SifrahGame.GameOutcome? gameOutcome5 = null;
			num5 = null;
			int? num15 = num5;
			num5 = null;
			int gameCount5 = SifrahGame.GetGameCount(text6, gameOutcome5, num15, num5, flag5, flag);
			return gameCount4 + gameCount5 / 75 >= num12;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x0019D704 File Offset: 0x0019B904
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
			string text = "Do you want to try to complete the disarming attempt in its current state?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the disarming attempt, but will still have ",
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

		// Token: 0x0600567F RID: 22143 RVA: 0x0019D7DC File Offset: 0x0019B9DC
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			string text = "You have no more usable options. Do you want to try to complete the disarming attempt in its current state?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the disarming attempt, but will still have ",
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

		// Token: 0x06005680 RID: 22144 RVA: 0x0019D88C File Offset: 0x0019BA8C
		public override void Finish(GameObject ContextObject)
		{
			GameObject gameObject = GameObject.FindByID(this.HandlerID);
			if (gameObject == null)
			{
				return;
			}
			IDisarmingSifrahHandler disarmingSifrahHandler = gameObject.GetPart(this.HandlerPartName) as IDisarmingSifrahHandler;
			if (disarmingSifrahHandler == null)
			{
				return;
			}
			if (this.Bypass)
			{
				base.PlayOutcomeSound(SifrahGame.GameOutcome.Success);
				disarmingSifrahHandler.DisarmingResultSuccess(The.Player, ContextObject, this.Auto);
				return;
			}
			if (!this.Abort)
			{
				SifrahGame.GameOutcome gameOutcome = base.DetermineOutcome();
				base.PlayOutcomeSound(gameOutcome);
				bool flag = false;
				if (gameOutcome == SifrahGame.GameOutcome.CriticalFailure)
				{
					disarmingSifrahHandler.DisarmingResultCriticalFailure(The.Player, ContextObject, this.Auto);
					this.RequestInterfaceExit();
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Failure)
				{
					disarmingSifrahHandler.DisarmingResultFailure(The.Player, ContextObject, this.Auto);
					this.RequestInterfaceExit();
				}
				else if (gameOutcome == SifrahGame.GameOutcome.PartialSuccess)
				{
					disarmingSifrahHandler.DisarmingResultPartialSuccess(The.Player, ContextObject, this.Auto);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Success)
				{
					if (base.PercentSolved >= 100)
					{
						flag = true;
					}
					disarmingSifrahHandler.DisarmingResultSuccess(The.Player, ContextObject, this.Auto);
					int num = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty;
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
					disarmingSifrahHandler.DisarmingResultExceptionalSuccess(The.Player, ContextObject, this.Auto);
					TinkeringSifrah.AwardInsight();
					int num2 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * this.Difficulty * 3;
					if (base.PercentSolved < 100)
					{
						num2 = num2 * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
					if (num2 > 0)
					{
						The.Player.AwardXP(num2, -1, 0, int.MaxValue, null, ContextObject, null, null, null, null);
					}
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
					disarmingSifrahHandler.DisarmingResultCriticalFailure(The.Player, ContextObject, this.Auto);
					this.RequestInterfaceExit();
					AutoAct.Interrupt(null, null, null, false);
				}
			}
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x0019DB04 File Offset: 0x0019BD04
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
			if (this.AsMaster)
			{
				num4 *= 5.0;
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

		// Token: 0x06005682 RID: 22146 RVA: 0x0019DC89 File Offset: 0x0019BE89
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002B95 RID: 11157
		public int Difficulty;

		// Token: 0x04002B96 RID: 11158
		public int Rating;

		// Token: 0x04002B97 RID: 11159
		public bool Abort;

		// Token: 0x04002B98 RID: 11160
		public bool AsMaster;

		// Token: 0x04002B99 RID: 11161
		public bool Bypass;

		// Token: 0x04002B9A RID: 11162
		public bool Auto;

		// Token: 0x04002B9B RID: 11163
		public bool InterfaceExitRequested;

		// Token: 0x04002B9C RID: 11164
		public string HandlerID;

		// Token: 0x04002B9D RID: 11165
		public string HandlerPartName;

		// Token: 0x04002B9E RID: 11166
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("investigating gearworks", "Items/sw_gears2.bmp", "é", "&c", 'c', 0),
			new SifrahSlotConfiguration("tracing spark vines", "Items/sw_copper_wire.bmp", "í", "&W", 'G', 0),
			new SifrahSlotConfiguration("divining humour flows", "Items/sw_sparktick_plasma.bmp", "÷", "&Y", 'y', 0),
			new SifrahSlotConfiguration("investigating glow modules", "Items/sw_lens.bmp", "\u001f", "&B", 'b', 0),
			new SifrahSlotConfiguration("interpreting spirit messages", "Items/sw_computer.bmp", "Ñ", "&c", 'G', 0),
			new SifrahSlotConfiguration("meditating on the beyond", "Items/sw_wind_turbine_3.bmp", "ì", "&m", 'K', 0)
		};
	}
}
