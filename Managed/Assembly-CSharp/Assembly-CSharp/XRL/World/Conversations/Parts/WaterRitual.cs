using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.API;
using XRL.Liquids;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C1F RID: 3103
	public class WaterRitual : IConversationPart
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06007C27 RID: 31783 RVA: 0x002EA0D3 File Offset: 0x002E82D3
		public static BaseLiquid Liquid
		{
			get
			{
				BaseLiquid baseLiquid;
				if ((baseLiquid = WaterRitual._Liquid) == null)
				{
					baseLiquid = (WaterRitual._Liquid = LiquidVolume.GetLiquid(The.Speaker.GetWaterRitualLiquid(The.Player)));
				}
				return baseLiquid;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06007C28 RID: 31784 RVA: 0x002EA0FD File Offset: 0x002E82FD
		public static string LiquidName
		{
			get
			{
				return WaterRitual.Liquid.GetName();
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06007C29 RID: 31785 RVA: 0x002EA109 File Offset: 0x002E8309
		public static string LiquidNameStripped
		{
			get
			{
				return ColorUtility.StripFormatting(WaterRitual.Liquid.GetName());
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06007C2A RID: 31786 RVA: 0x002EA11A File Offset: 0x002E831A
		public static WaterRitualRecord Record
		{
			get
			{
				WaterRitualRecord waterRitualRecord;
				if ((waterRitualRecord = WaterRitual._Record) == null)
				{
					waterRitualRecord = (WaterRitual._Record = The.Speaker.RequirePart<WaterRitualRecord>(false));
				}
				return waterRitualRecord;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06007C2B RID: 31787 RVA: 0x002EA136 File Offset: 0x002E8336
		public static Faction RecordFaction
		{
			get
			{
				Faction faction;
				if ((faction = WaterRitual._RecordFaction) == null)
				{
					faction = (WaterRitual._RecordFaction = Factions.Get(WaterRitual.Record.faction));
				}
				return faction;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06007C2C RID: 31788 RVA: 0x002EA158 File Offset: 0x002E8358
		public static bool Alternative
		{
			get
			{
				bool flag = WaterRitual._Alternative.GetValueOrDefault();
				if (WaterRitual._Alternative == null)
				{
					flag = WaterRitual.RecordFaction.UseAltBehavior(The.Speaker);
					WaterRitual._Alternative = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06007C2D RID: 31789 RVA: 0x002EA19A File Offset: 0x002E839A
		public static int Performance
		{
			get
			{
				return The.Speaker.GetIntProperty("WaterRitualPerformance", 100);
			}
		}

		// Token: 0x06007C2E RID: 31790 RVA: 0x002EA1AD File Offset: 0x002E83AD
		public static void Reset()
		{
			WaterRitual._Liquid = null;
			WaterRitual._Record = null;
			WaterRitual._RecordFaction = null;
			WaterRitual._Alternative = null;
		}

		// Token: 0x06007C2F RID: 31791 RVA: 0x002EA1CC File Offset: 0x002E83CC
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == LeftElementEvent.ID || ID == EnteredElementEvent.ID || ID == DisplayTextEvent.ID;
		}

		// Token: 0x06007C30 RID: 31792 RVA: 0x002EA1F4 File Offset: 0x002E83F4
		public override bool HandleEvent(DisplayTextEvent E)
		{
			E.Text.Append("\n\n{{C|-----}}\n{{y|Your reputation with ");
			if (WaterRitual.RecordFaction.Visible)
			{
				E.Text.Append("{{C|").Append(WaterRitual.RecordFaction.GetFormattedName()).Append("}}");
			}
			else
			{
				E.Text.Append(The.Speaker.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, null, false));
			}
			E.Text.Append(" is {{C|").Append(WaterRitual.RecordFaction.CurrentReputation).Append("}}.\n")
				.Append(WaterRitual.RecordFaction.Visible ? The.Speaker.T(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false) : The.Speaker.It)
				.Append(" can award an additional {{C|")
				.Append(WaterRitual.Record.totalFactionAvailable)
				.Append("}} reputation.}}");
			return base.HandleEvent(E);
		}

		// Token: 0x06007C31 RID: 31793 RVA: 0x002EA30B File Offset: 0x002E850B
		public override bool HandleEvent(LeftElementEvent E)
		{
			WaterRitual.Reset();
			return base.HandleEvent(E);
		}

		// Token: 0x06007C32 RID: 31794 RVA: 0x002EA31C File Offset: 0x002E851C
		public override bool HandleEvent(EnteredElementEvent E)
		{
			bool flag = !The.Speaker.HasIntProperty("WaterRitualed");
			if (flag)
			{
				this.PerformRitual();
			}
			WaterRitualStartEvent.Send(The.Player, The.Speaker, WaterRitual.Record, flag);
			return base.HandleEvent(E);
		}

		// Token: 0x06007C33 RID: 31795 RVA: 0x002EA364 File Offset: 0x002E8564
		public void PerformRitual()
		{
			The.Speaker.SetIntProperty("WaterRitualed", 1, false);
			if (!The.Speaker.HasIntProperty("SifrahWaterRitual"))
			{
				Popup.Show(string.Concat(new string[]
				{
					"You share your ",
					WaterRitual.LiquidName,
					" with ",
					The.Speaker.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, null, false),
					" and begin the water ritual."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.HandleAchievements();
			this.AddAccomplishment();
			this.ModifyReputation();
			this.NameItems();
		}

		// Token: 0x06007C34 RID: 31796 RVA: 0x002EA408 File Offset: 0x002E8608
		public void HandleAchievements()
		{
			if (The.Speaker.Blueprint == "Oboroqoru")
			{
				Achievement.WATER_RITUAL_OBOROQORU.Unlock();
			}
			if (The.Speaker.Blueprint == "Mamon")
			{
				Achievement.WATER_RITUAL_MAMON.Unlock();
			}
			Achievement.WATER_RITUAL_100_TIMES.Progress.Increment(1);
		}

		// Token: 0x06007C35 RID: 31797 RVA: 0x002EA468 File Offset: 0x002E8668
		public void AddAccomplishment()
		{
			string referenceDisplayName = The.Speaker.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, null, false);
			string referenceDisplayName2 = The.Speaker.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false);
			IPronounProvider pronounProvider = The.Player.GetPronounProvider(false);
			string text = string.Concat(new string[]
			{
				"In sacred ritual you shared your ",
				WaterRitual.LiquidName,
				" with ",
				referenceDisplayName,
				"."
			});
			string text2 = string.Concat(new string[]
			{
				"In sacred ritual =name= shared ",
				pronounProvider.PossessiveAdjective,
				" holy ",
				WaterRitual.LiquidNameStripped,
				" with noted luminary ",
				referenceDisplayName2,
				"."
			});
			MuralWeight muralWeight = (WanderSystem.WanderEnabled() ? MuralWeight.Low : MuralWeight.Medium);
			string[] array = new string[9];
			array[0] = "<spice.elements.";
			array[1] = The.Player.GetMythicDomain(5, null);
			array[2] = ".weddingConditions.!random.capitalize>, =name= cemented ";
			array[3] = The.Player.GetPronounProvider(false).PossessiveAdjective;
			array[4] = " friendship with ";
			int num = 5;
			GameObject speaker = The.Speaker;
			Faction ifExists = Factions.GetIfExists((speaker != null) ? speaker.GetPrimaryFaction(false) : null);
			array[num] = ((ifExists != null) ? ifExists.GetFormattedName() : null);
			array[6] = " by marrying ";
			array[7] = referenceDisplayName;
			array[8] = ".";
			JournalAPI.AddAccomplishment(text, text2, string.Concat(array), null, "general", MuralCategory.Treats, muralWeight, null, -1L, true);
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x002EA5C8 File Offset: 0x002E87C8
		public void ModifyReputation()
		{
			string referenceDisplayName = The.Speaker.GetReferenceDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, null, false);
			int performance = WaterRitual.Performance;
			WaterRitual.Record.totalFactionAvailable = WaterRitual.Record.totalFactionAvailable * performance / 100;
			GivesRep part = The.Speaker.GetPart<GivesRep>();
			int num = ((part != null) ? part.repValue : WaterRitual.REL_LOVE);
			int num2 = GetWaterRitualReputationAmountEvent.GetFor(The.Player, The.Speaker, WaterRitual.Record, WaterRitual.Record.faction, num);
			num2 = num2 * performance / 100;
			if (num2 != 0)
			{
				The.Game.PlayerReputation.Modify(WaterRitual.RecordFaction, num2, null, null, "WaterRitualPrimaryAward", false, false, false, false);
			}
			if (part == null)
			{
				return;
			}
			part.wasParleyed = true;
			foreach (KeyValuePair<string, int> keyValuePair in The.Speaker.Brain.GetBaseAllegiance())
			{
				if (!(keyValuePair.Key == WaterRitual.Record.faction))
				{
					Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
					if (ifExists != null)
					{
						int num3 = GetWaterRitualReputationAmountEvent.GetFor(The.Player, The.Speaker, WaterRitual.Record, ifExists.Name, num) * performance / 100;
						The.Game.PlayerReputation.Modify(ifExists, num3, "because they love " + referenceDisplayName, null, "WaterRitualFactionAward", false, false, false, true);
					}
				}
			}
			foreach (FriendorFoe friendorFoe in part.relatedFactions)
			{
				Faction ifExists2 = Factions.GetIfExists(friendorFoe.faction);
				if (ifExists2 != null)
				{
					int num4 = 0;
					string text = "regard";
					string text2 = "WaterRitualAttitudeAward";
					string text3 = friendorFoe.status;
					if (!(text3 == "friend"))
					{
						if (!(text3 == "dislike"))
						{
							if (text3 == "hate")
							{
								num4 = -num;
								text = "despise";
								text2 = "WaterRitualHateAward";
							}
						}
						else
						{
							num4 = -num / 2;
							text = "dislike";
							text2 = "WaterRitualDislikeAward";
						}
					}
					else
					{
						num4 = num;
						text = "admire";
						text2 = "WaterRitualFriendAward";
					}
					num4 = GetWaterRitualReputationAmountEvent.GetFor(The.Player, The.Speaker, WaterRitual.Record, ifExists2.Name, num4) * (100 + (performance - 100) / 10) / 100;
					if (num4 != 0)
					{
						Reputation playerReputation = The.Game.PlayerReputation;
						Faction faction = ifExists2;
						int num5 = num4;
						text3 = text2;
						playerReputation.Modify(faction, num5, "because they " + text + " " + referenceDisplayName, null, text3, false, false, false, true);
					}
				}
			}
			The.Game.PlayerReputation.FinishModify();
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x002EA8AC File Offset: 0x002E8AAC
		public void NameItems()
		{
			int performance = WaterRitual.Performance;
			ItemNaming.Opportunity(The.Speaker, null, The.Player, null, "WaterRitual", 7 - performance / 100, 0, 0, performance / 100, false);
			ItemNaming.Opportunity(The.Player, null, The.Speaker, null, "WaterRitual", 7 - performance / 100, 0, 0, performance / 100, false);
		}

		// Token: 0x04003877 RID: 14455
		public static readonly int REL_LOVE = RuleSettings.REPUTATION_BASE_UNIT * 2;

		// Token: 0x04003878 RID: 14456
		public static readonly int REL_LIKE = RuleSettings.REPUTATION_BASE_UNIT;

		// Token: 0x04003879 RID: 14457
		public static readonly int REL_DISLIKE = -RuleSettings.REPUTATION_BASE_UNIT;

		// Token: 0x0400387A RID: 14458
		public static readonly int REL_HATE = -RuleSettings.REPUTATION_BASE_UNIT * 2;

		// Token: 0x0400387B RID: 14459
		private static BaseLiquid _Liquid;

		// Token: 0x0400387C RID: 14460
		private static WaterRitualRecord _Record;

		// Token: 0x0400387D RID: 14461
		private static Faction _RecordFaction;

		// Token: 0x0400387E RID: 14462
		private static bool? _Alternative;
	}
}
