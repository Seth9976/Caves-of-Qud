using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008BA RID: 2234
	[Serializable]
	public class ReverseEngineeringSifrah : TinkeringSifrah
	{
		// Token: 0x0600573F RID: 22335 RVA: 0x001A79A0 File Offset: 0x001A5BA0
		public ReverseEngineeringSifrah(GameObject ContextObject, int Complexity, int Difficulty, int ReverseEngineerRating, TinkerData LearnData)
		{
			this.Description = "Reverse engineering " + ContextObject.an(int.MaxValue, null, null, false, true, false, false, false, false, false, false, false, true, false, null, false, null, false);
			this.Complexity = Complexity;
			this.Difficulty = Difficulty;
			this.ReverseEngineerRating = ReverseEngineerRating;
			this.LearnData = LearnData;
			int num = 4 + Complexity + Difficulty;
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
			if (num2 > ReverseEngineeringSifrah.slotConfigs.Count)
			{
				num2 = ReverseEngineeringSifrah.slotConfigs.Count;
			}
			this.MaxTurns = 3;
			bool flag = false;
			bool flag2 = false;
			int num3 = 0;
			int @for = GetTinkeringBonusEvent.GetFor(The.Player, ContextObject, "ReverseEngineerTurns", this.MaxTurns, 0, ref num3, ref flag, ref flag2, false, true);
			this.MaxTurns += @for;
			if (num3 != 0)
			{
				ReverseEngineerRating += num3;
				this.ReverseEngineerRating = ReverseEngineerRating;
			}
			List<SifrahToken> list = new List<SifrahToken>(num);
			if (list.Count < num && (Complexity < 3 || ReverseEngineerRating > 20 + Complexity * 3) && !The.Player.HasPart<Myopia>())
			{
				list.Add(new TinkeringSifrahTokenVisualInspection());
			}
			if (list.Count < num && (Complexity < 4 || ReverseEngineerRating > 10 + Complexity * 2))
			{
				list.Add(new TinkeringSifrahTokenPhysicalManipulation());
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
				}
			}
			if (list.Count < num && flag2)
			{
				list.Add(new TinkeringSifrahTokenPsychometry("read psychic impressions of manufacturing process"));
			}
			if (list.Count < num && The.Player.HasSkill("TenfoldPath_Bin"))
			{
				list.Add(new TinkeringSifrahTokenTenfoldPathBin());
			}
			if (list.Count < num && The.Player.HasPart<Telekinesis>() && (Complexity < 4 || ReverseEngineerRating > Difficulty + Complexity * 2))
			{
				list.Add(new TinkeringSifrahTokenTelekinesis());
			}
			int num4 = num - list.Count;
			if (num4 > 0)
			{
				List<SifrahPrioritizableToken> list2 = new List<SifrahPrioritizableToken>();
				if (Complexity < 5 || ReverseEngineerRating > 5 + Complexity * 3)
				{
					list2.Add(new TinkeringSifrahTokenToolkit());
				}
				list2.Add(new TinkeringSifrahTokenComputePower(Complexity + Difficulty));
				list2.Add(new TinkeringSifrahTokenCharge(Math.Min(1000 * (Complexity + Difficulty), 10000)));
				foreach (BitType bitType in BitType.BitTypes)
				{
					if (bitType.Level > Complexity)
					{
						break;
					}
					list2.Add(new TinkeringSifrahTokenBit(bitType));
				}
				list2.Add(new TinkeringSifrahTokenLiquid("brainbrine"));
				list2.Add(new TinkeringSifrahTokenLiquid("ink"));
				list2.Add(new TinkeringSifrahTokenLiquid("lava"));
				list2.Add(new TinkeringSifrahTokenLiquid("oil"));
				list2.Add(new TinkeringSifrahTokenLiquid("wax"));
				list2.Add(new TinkeringSifrahTokenCopperWire());
				base.AssignPossibleTokens(list2, list, num4, num);
			}
			if (num > list.Count)
			{
				num = list.Count;
			}
			List<SifrahSlot> list3 = SifrahSlot.GenerateListFromConfigurations(ReverseEngineeringSifrah.slotConfigs, num2, num);
			this.Slots = list3;
			this.Tokens = list;
			if (!base.AnyUsableTokens(ContextObject))
			{
				Popup.ShowFail("You have no usable options to employ for reverse engineering " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", giving you no chance of success. You can remedy this situation by improving your Intelligence and tinkering skills, or by obtaining items useful for tinkering.", true, true, true);
				this.Finished = true;
				this.Abort = true;
			}
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x001A7D40 File Offset: 0x001A5F40
		public override bool CheckEarlyExit(GameObject ContextObject)
		{
			return Popup.ShowYesNo("Exiting will still disassemble " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ", and will result in an attempt at reverse engineering as matters stand. Do you still want to exit?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes;
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x001A7D91 File Offset: 0x001A5F91
		public override bool CheckOutOfOptions(GameObject ContextObject)
		{
			Popup.ShowFail("You have no more usable options.", true, true, true);
			return true;
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x001A7DA4 File Offset: 0x001A5FA4
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
					this.Succeeded = false;
					this.Critical = true;
					Popup.Show("You think you've made a terrible mistake...", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					if (The.Player.HasPart<Dystechnia>())
					{
						Dystechnia.CauseExplosion(ContextObject, The.Player, null);
					}
					else
					{
						The.Player.Sparksplatter();
						The.Player.TakeDamage(this.Complexity + this.Difficulty + Stat.Random(1, (this.Complexity + this.Difficulty) * 4), "from an electrical discharge!", "Electric", null, null, The.Player, ContextObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
						The.Player.TakeDamage(this.Complexity + this.Difficulty / 2 + Stat.Random(1, (this.Complexity + this.Difficulty) * 2), "from a sharp edge!", "Cutting", null, null, The.Player, ContextObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
						The.Player.ApplyEffect(new Stun(Stat.Random(5, 10), 20, false), null);
					}
					this.RequestInterfaceExit();
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Failure)
				{
					this.Succeeded = false;
					this.Critical = false;
					Popup.Show("You fail to reverse engineer " + ContextObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (gameOutcome == SifrahGame.GameOutcome.PartialSuccess)
				{
					this.Succeeded = true;
					this.Critical = false;
					this.XP = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty);
					if (base.PercentSolved < 100)
					{
						this.XP = this.XP * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
				}
				else if (gameOutcome == SifrahGame.GameOutcome.Success)
				{
					this.Succeeded = true;
					this.Critical = false;
					this.Mods = 1;
					this.XP = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty) * 3 / 2;
					if (base.PercentSolved < 100)
					{
						this.XP = this.XP * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
				}
				else if (gameOutcome == SifrahGame.GameOutcome.CriticalSuccess)
				{
					this.Succeeded = true;
					this.Critical = true;
					this.Mods = 3;
					this.XP = (this.Tokens.Count * this.Tokens.Count - this.Slots.Count) * (this.Complexity + this.Difficulty) * 5;
					if (base.PercentSolved < 100)
					{
						this.XP = this.XP * (100 - (100 - base.PercentSolved) * 3) / 100;
					}
				}
				SifrahGame.RecordOutcome(this, gameOutcome, this.Slots.Count, this.Tokens.Count, false, false);
			}
		}

		// Token: 0x06005743 RID: 22339 RVA: 0x001A80E8 File Offset: 0x001A62E8
		public override void CalculateOutcomeChances(out int Success, out int Failure, out int PartialSuccess, out int CriticalSuccess, out int CriticalFailure)
		{
			double num = (double)(this.ReverseEngineerRating / 2 + base.PercentSolved) * 0.009;
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
			double num4 = 0.034 + (double)this.Powerup * 0.04;
			if (this.Turn > 1)
			{
				num4 += 0.037 * (double)(this.MaxTurns - this.Turn);
			}
			double num5 = num2 * num4;
			if (num2 > 1.0)
			{
				num2 = 1.0;
			}
			double num6 = 1.0 - num2;
			double num7 = num2 * 0.3;
			double num8 = num6 * 0.1;
			num2 -= num5;
			num2 -= num7;
			num6 -= num8;
			Success = (int)(num2 * 100.0);
			PartialSuccess = (int)(num7 * 100.0);
			Failure = (int)(num6 * 100.0);
			CriticalSuccess = (int)(num5 * 100.0);
			CriticalFailure = (int)(num8 * 100.0);
			while (Success + Failure + PartialSuccess + CriticalSuccess + CriticalFailure < 100)
			{
				PartialSuccess++;
			}
		}

		// Token: 0x06005744 RID: 22340 RVA: 0x001A8258 File Offset: 0x001A6458
		public void RequestInterfaceExit()
		{
			this.InterfaceExitRequested = true;
		}

		// Token: 0x04002BF4 RID: 11252
		public int Complexity;

		// Token: 0x04002BF5 RID: 11253
		public int Difficulty;

		// Token: 0x04002BF6 RID: 11254
		public int ReverseEngineerRating;

		// Token: 0x04002BF7 RID: 11255
		public TinkerData LearnData;

		// Token: 0x04002BF8 RID: 11256
		public bool Abort;

		// Token: 0x04002BF9 RID: 11257
		public bool InterfaceExitRequested;

		// Token: 0x04002BFA RID: 11258
		public bool CriticallyFailed;

		// Token: 0x04002BFB RID: 11259
		public bool Succeeded;

		// Token: 0x04002BFC RID: 11260
		public bool Critical;

		// Token: 0x04002BFD RID: 11261
		public int XP;

		// Token: 0x04002BFE RID: 11262
		public int Mods;

		// Token: 0x04002BFF RID: 11263
		private static readonly List<SifrahSlotConfiguration> slotConfigs = new List<SifrahSlotConfiguration>
		{
			new SifrahSlotConfiguration("sketching gearworks", "Items/sw_gears2.bmp", "é", "&c", 'c', 0),
			new SifrahSlotConfiguration("diagramming spark vines", "Items/sw_copper_wire.bmp", "í", "&W", 'G', 0),
			new SifrahSlotConfiguration("measuring humour flows", "Items/sw_sparktick_plasma.bmp", "÷", "&Y", 'y', 0),
			new SifrahSlotConfiguration("mapping glow modules", "Items/sw_lens.bmp", "\u001f", "&B", 'b', 0),
			new SifrahSlotConfiguration("summoning spirits", "Items/sw_computer.bmp", "Ñ", "&c", 'G', 0),
			new SifrahSlotConfiguration("communing with the beyond", "Items/sw_wind_turbine_3.bmp", "ì", "&m", 'K', 0)
		};

		// Token: 0x04002C00 RID: 11264
		private static string[] status = new string[5];
	}
}
