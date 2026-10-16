using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x020005F6 RID: 1526
	[Serializable]
	public class BattleItem : HistoricEvent
	{
		// Token: 0x06004167 RID: 16743 RVA: 0x00149C48 File Offset: 0x00147E48
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("location", "unknown");
			string property2 = snapshotAtYear.GetProperty("region", "unknown");
			string newLocationInRegion = QudHistoryHelpers.GetNewLocationInRegion(this.history, property2, property);
			base.SetEntityProperty("location", newLocationInRegion);
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string text = base.ExpandString("<spice.items.weapons.!random>");
			string text2 = base.ExpandString("<spice.elements." + randomElement + ".adjectives.!random>");
			int num = 0;
			int num2 = base.Random(0, 1);
			string text3;
			string text4;
			if (num2 == 0)
			{
				text3 = "liberate";
				if (snapshotAtYear.GetList("likedFactions").Count != 0)
				{
					text4 = snapshotAtYear.GetList("likedFactions").GetRandomElement<string>(null);
				}
				else
				{
					text4 = QudHistoryHelpers.GetNewFaction(this.entity);
					base.AddEntityListItem("likedFactions", text4, false);
					num = 1;
				}
			}
			else
			{
				text3 = "subjugate";
				if (snapshotAtYear.GetList("hatedFactions").Count != 0)
				{
					text4 = snapshotAtYear.GetList("hatedFactions").GetRandomElement<string>(null);
				}
				else
				{
					text4 = QudHistoryHelpers.GetNewFaction(this.entity);
					base.AddEntityListItem("hatedFactions", text4, false);
					num = 1;
				}
			}
			string text5 = (text3.Equals("liberate") ? base.ExpandString("<spice.commonPhrases.boon.!random>") : base.ExpandString("<spice.commonPhrases.bane.!random>"));
			string text6;
			if (base.Random(0, 1) == 0)
			{
				text6 = Grammar.MakeTitleCase(QudHistoryHelpers.NameItemAdjRoot(text2, this.history, this.entity) + " " + Grammar.GetRandomMeaningfulWord(Faction.GetFormattedName(text4)) + text5);
			}
			else
			{
				text6 = Grammar.MakeTitleCase(string.Concat(new string[]
				{
					QudHistoryHelpers.NameItemAdjRoot(text2, this.history, this.entity),
					", the ",
					text5,
					" of ",
					Faction.GetFormattedName(text4)
				}));
			}
			if (num == 0)
			{
				string text7 = string.Concat(new string[]
				{
					"At the Battle of ",
					newLocationInRegion,
					", <entity.name> fought to ",
					text3,
					" ",
					Faction.GetFormattedName(text4),
					". <entity.subjectPronoun.capitalize> wielded ",
					Grammar.A(text2, false),
					" ",
					text,
					" with such <spice.commonPhrases.finesse.!random> that it became forever known as ",
					text6,
					"."
				});
				base.SetEventProperty("gospel", text7);
			}
			else
			{
				string text8 = (snapshotAtYear.GetProperty("isSultan", "unknown").Equals("true") ? "sultan " : "");
				string text7 = string.Concat(new string[]
				{
					"At the Battle of ",
					newLocationInRegion,
					", <entity.name> fought as a mercenary ",
					text8,
					"to ",
					text3,
					" ",
					Faction.GetFormattedName(text4),
					". <entity.subjectPronoun.capitalize> wielded ",
					Grammar.A(text2, false),
					" ",
					text,
					" with such <spice.commonPhrases.finesse.!random> that it became forever known as ",
					text6,
					"."
				});
				base.SetEventProperty("gospel", text7);
			}
			string text9;
			if (num2 == 0)
			{
				text9 = string.Format("{0} the Battle of {1}, where -- {2} -- {3} wielded {5} {6} and struck down the {4} in the name of {7}. {0}, afterward, how {8} and {9} {10} in {11} for days and days.", new object[]
				{
					base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
					newLocationInRegion,
					base.ExpandString("<spice.elements." + randomElement + ".mythicalBattleVista.!random>").Replace("*var*", text2),
					"<entity.name>",
					base.ExpandString("<spice.history.gospels.EnemyHostName." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
					"<entity.possessivePronoun>",
					text,
					base.ExpandString("<spice.instancesOf.justice.!random>"),
					Faction.GetFormattedName(text4),
					base.ExpandString("<spice.instancesOf.dearOnes.!random>"),
					base.ExpandString("<spice.instancesOf.criedOut.!random>"),
					base.ExpandString("<spice.commonPhrases.celebration.!random>")
				});
			}
			else
			{
				text9 = string.Format("{0} the Battle of {1}, where -- {2} -- {3} wielded {5} {6} and struck down the {4} in the name of {7}. {0}, afterward, how {8} and {9} {10} in {11} for days and days.", new object[]
				{
					base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
					newLocationInRegion,
					base.ExpandString("<spice.elements." + randomElement + ".mythicalBattleVista.!random>").Replace("*var*", text2),
					"<entity.name>",
					base.ExpandString("<spice.history.gospels.EnemyHostName." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
					"<entity.possessivePronoun>",
					text,
					base.ExpandString("<spice.instancesOf.justice.!random>"),
					Faction.GetFormattedName(text4),
					base.ExpandString("<spice.instancesOf.dearOnes.!random>"),
					base.ExpandString("<spice.instancesOf.criedOut.!random>"),
					base.ExpandString("<spice.commonPhrases.woe.!random>")
				});
			}
			base.SetEventProperty("tombInscription", text9);
			base.SetEventProperty("tombInscriptionCategory", "WieldsItemInBattle");
			HistoricEntity historicEntity = this.history.CreateEntity(this.year);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("itemType", text);
			string text10;
			QudHistoryHelpers.ExtractArticle(ref text6, out text10);
			dictionary.Add("baseName", text6);
			dictionary.Add("article", text10);
			text6 = QudHistoryHelpers.Ansify(text6, text10);
			dictionary.Add("name", text6);
			dictionary.Add("descriptionAdj", text2);
			dictionary.Add("period", snapshotAtYear.GetProperty("period", "unknown"));
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2.Add("elements", randomElement);
			if (text3.Equals("liberate"))
			{
				dictionary2.Add("likedFactions", text4);
			}
			else
			{
				dictionary2.Add("hatedFactions", text4);
			}
			historicEntity.ApplyEvent(new SetEntityProperties(dictionary, dictionary2), long.MinValue);
			base.AddEntityListItem("items", text6, false);
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			dictionary3.Add("battles", "generic");
			this.history.GetEntitiesWherePropertyEquals("name", newLocationInRegion).GetRandomElement(null).ApplyEvent(new SetEntityProperties(null, dictionary3), long.MinValue);
		}
	}
}
