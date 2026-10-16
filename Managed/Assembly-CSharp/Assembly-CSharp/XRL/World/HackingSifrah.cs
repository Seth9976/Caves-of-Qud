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
	// Token: 0x0200088A RID: 2186
	[Serializable]
	public class HackingSifrah : TinkeringSifrah
	{
		// Token: 0x0600569B RID: 22171 RVA: 0x0019F574 File Offset: 0x0019D774
		public HackingSifrah(GameObject ContextObject, int Complexity, int Difficulty, int HackingRating)
		{
			string text = "Hacking ";
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
			this.HackingRating = HackingRating;
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
			if (num2 > HackingSifrah.SlotConfigs.Count)
			{
				num2 = HackingSifrah.SlotConfigs.Count;
			}
			this.MaxTurns = 3;
			bool flag13 = false;
			bool flag14 = false;
			int num3 = 0;
			int @for = GetTinkeringBonusEvent.GetFor(The.Player, ContextObject, "Hacking", this.MaxTurns, 0, ref num3, ref flag13, ref flag14, true, true);
			if (flag13)
			{
				this.Finished = true;
				this.Abort = true;
			}
			this.MaxTurns += @for;
			if (num3 != 0)
			{
				HackingRating += num3;
				this.HackingRating = HackingRating;
			}
			List<SifrahToken> list = new List<SifrahToken>(num);
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
				}
			}
			if (list.Count < num && flag14)
			{
				list.Add(new TinkeringSifrahTokenPsychometry("read psychic impressions of security procedures"));
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Bin"))
			{
				list.Add(new TinkeringSifrahTokenTenfoldPathBin());
			}
			if (list.Count < num && The.Player.HasPart<Telekinesis>() && (Complexity < 5 || HackingRating > Difficulty + Complexity * 2))
			{
				list.Add(new TinkeringSifrahTokenTelekinesis());
			}
			int num4 = num - list.Count;
			if (num4 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				list2.Add(new TinkeringSifrahTokenAdvancedToolkit());
				if (TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor(ContextObject))
				{
					list2.Add(new TinkeringSifrahTokenCreationKnowledge(ContextObject));
				}
				list2.Add(new TinkeringSifrahTokenComputePower(Complexity + Difficulty * Difficulty));
				list2.Add(new TinkeringSifrahTokenCharge(10000));
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Complexity)
					{
						break;
					}
					list2.Add(new TinkeringSifrahTokenBit(bitType));
				}
				list2.Add(new TinkeringSifrahTokenLiquid("brainbrine"));
				list2.Add(new TinkeringSifrahTokenCopperWire());
				base.AssignPossibleTokens(list2, list, num4, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(HackingSifrah.SlotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				string text4 = "You have no usable options to employ for hacking ";
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
				int num13 = this.Tokens.Count * this.Tokens.Count / 2 + gameCount2 / 2 + gameCount3 * 2 + gameCount / 10;
				num5 = new int?(this.Slots.Count);
				num6 = new int?(this.Tokens.Count);
				flag12 = new bool?(true);
				if (base.GetGameCount(null, num5, num6, null, flag12) >= num13)
				{
					this.AsMaster = true;
				}
			}
			if (this.AsMaster)
			{
				string sifrahHackingAuto = Options.SifrahHackingAuto;
				if (sifrahHackingAuto == "Always")
				{
					this.Finished = true;
					this.Bypass = true;
					return;
				}
				if (!(sifrahHackingAuto == "Never"))
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel("You have mastered hacks of this complexity. Do you want to perform a detailed hack anyway, with an enhanced chance of exceptional success? If you answer 'No', you will automatically succeed at the hack.", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
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

		// Token: 0x0600569C RID: 22172 RVA: 0x0019FAB0 File Offset: 0x0019DCB0
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
			string text = "Do you want to try to complete the hack as matters stand?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the hack, but will still have ",
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

		// Token: 0x0600569D RID: 22173 RVA: 0x0019FB88 File Offset: 0x0019DD88
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			this.CalculateOutcomeChances(out num, out num2, out num3, out num4, out num5);
			string text = "You have no more usable options. Do you want to try to complete the hack as matters stand?";
			if (this.Turn > 1)
			{
				int num6 = this.MaxTurns - this.Turn + 1;
				int num7 = num5 - num6;
				if (num7 > 0)
				{
					text = string.Concat(new string[]
					{
						text,
						" Answering 'No' will abort the hack, but will still have ",
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

		// Token: 0x0600569E RID: 22174 RVA: 0x0019FC38 File Offset: 0x0019DE38
		public override void Finish(GameObject ContextObject)
		{
			GameObject gameObject = GameObject.FindByID(this.HandlerID);
			if (gameObject == null)
			{
				return;
			}
			IHackingSifrahHandler hackingSifrahHandler = gameObject.GetPart(this.HandlerPartName) as IHackingSifrahHandler;
			if (hackingSifrahHandler == null)
			{
				return;
			}
			if (this.Bypass)
			{
				base.PlayOutcomeSound(SifrahGame.GameOutcome.Success);
				hackingSifrahHandler.HackingResultSuccess(The.Player, ContextObject, this);
				return;
			}
			if (!this.Abort)
			{
				SifrahGame.GameOutcome gameOutcome = base.DetermineOutcome();
				base.PlayOutcomeSound(gameOutcome);
				bool flag = false;
				if (gameOutcome == SifrahGame.GameOutcome.CriticalFailure)
				{
					hackingSifrahHandler.HackingResultCriticalFailure(The.Player, ContextObject, this);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Failure)
				{
					hackingSifrahHandler.HackingResultFailure(The.Player, ContextObject, this);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.PartialSuccess)
				{
					hackingSifrahHandler.HackingResultPartialSuccess(The.Player, ContextObject, this);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Success)
				{
					if (base.PercentSolved >= 100)
					{
						flag = true;
					}
					hackingSifrahHandler.HackingResultSuccess(The.Player, ContextObject, this);
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
					hackingSifrahHandler.HackingResultExceptionalSuccess(The.Player, ContextObject, this);
					TinkeringSifrah.AwardInsight();
					int num2 = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty) * 2;
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
					hackingSifrahHandler.HackingResultCriticalFailure(The.Player, ContextObject, this);
					this.RequestInterfaceExit();
					AutoAct.Interrupt(null, null, null, false);
				}
			}
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x0019FE90 File Offset: 0x0019E090
		public override void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure)
		{
			double num = (double)(this.HackingRating + base.PercentSolved) * 0.009;
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
				num4 += 0.05 * (double)(this.MaxTurns - this.Turn);
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
			double num7 = num6 * 0.3;
			double num8 = num6 * 0.2;
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

		// Token: 0x060056A0 RID: 22176 RVA: 0x001A0015 File Offset: 0x0019E215
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002BAD RID: 11181
		public int Complexity;

		// Token: 0x04002BAE RID: 11182
		public int Difficulty;

		// Token: 0x04002BAF RID: 11183
		public int HackingRating;

		// Token: 0x04002BB0 RID: 11184
		public bool Abort;

		// Token: 0x04002BB1 RID: 11185
		public bool AsMaster;

		// Token: 0x04002BB2 RID: 11186
		public bool Bypass;

		// Token: 0x04002BB3 RID: 11187
		public bool InterfaceExitRequested;

		// Token: 0x04002BB4 RID: 11188
		public string HandlerID;

		// Token: 0x04002BB5 RID: 11189
		public string HandlerPartName;

		// Token: 0x04002BB6 RID: 11190
		private static readonly List<SifrahSlotConfiguration> SlotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("gaining physical access", "Items/sw_gears2.bmp", "é", "&c", 'c', 0),
			new SifrahSlotConfiguration("splicing spark vines", "Items/sw_copper_wire.bmp", "í", "&W", 'G', 0),
			new SifrahSlotConfiguration("redirecting humour flows", "Items/sw_sparktick_plasma.bmp", "÷", "&Y", 'y', 0),
			new SifrahSlotConfiguration("corrupting glow modules", "Items/sw_lens.bmp", "\u001f", "&B", 'b', 0),
			new SifrahSlotConfiguration("deceiving spirits", "Items/sw_computer.bmp", "Ñ", "&c", 'G', 0),
			new SifrahSlotConfiguration("calling on the beyond", "Items/sw_wind_turbine_3.bmp", "ì", "&m", 'K', 0)
		};
	}
}
