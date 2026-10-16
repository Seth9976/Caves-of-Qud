using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x02000601 RID: 1537
	[Serializable]
	public class ForgeItem : HistoricEvent
	{
		// Token: 0x06004181 RID: 16769 RVA: 0x0014C7B4 File Offset: 0x0014A9B4
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("region", "unknown");
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string text = base.ExpandString("<spice.elements." + randomElement + ".adjectives.!random>");
			string text2 = base.ExpandString("<spice.elements." + randomElement + ".nouns.!random>");
			int num = base.Random(0, 0);
			if (snapshotAtYear.GetProperty("profession", "unknown").Equals("unknown"))
			{
				num = 1;
			}
			string text4;
			string text6;
			string text7;
			if (num == 0)
			{
				string text3 = snapshotAtYear.GetProperty("profession", "unknown");
				bool flag = base.Random(0, 1) != 0;
				text4 = base.ExpandString("<spice.professions." + text3 + ".items.!random>");
				string text5;
				if (!flag)
				{
					text5 = "While visiting an obscure <spice.professions." + text3 + ".guildhall>";
				}
				else
				{
					text5 = "At a remote <spice.professions." + text3 + ".guildhall>";
				}
				if (base.Random(0, 1) == 0)
				{
					text5 = text5 + " in " + property;
					base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
				}
				text6 = string.Concat(new string[]
				{
					text5,
					", <entity.name> <spice.professions.",
					text3,
					".forged> ",
					Grammar.A(text4, false),
					" that evoked the presence of ",
					Grammar.A(text, false),
					" ",
					text2,
					"."
				});
				text7 = string.Format("{0} by a {1} {2} {3} {4}, {5} visited {6} {7} and {8} {9}.", new object[]
				{
					"<spice.commonPhrases.inspired.!random.capitalize>",
					"<spice.commonPhrases.prized.!random>",
					"",
					text2,
					base.ExpandString("<spice.history.gospels.ObjectFoundBy." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
					"<entity.name>",
					"<entity.possessivePronoun>",
					"<spice.professions." + text3 + ".guildhall>",
					"<spice.professions." + text3 + ".forged>",
					Grammar.A(text4, false)
				});
			}
			else
			{
				string text3 = base.ExpandString("<spice.professions.!random>");
				bool flag2 = base.Random(0, 1) != 0;
				text4 = base.ExpandString("<spice.professions." + text3 + ".items.!random>");
				string text8;
				if (!flag2)
				{
					text8 = "While visiting an obscure <spice.professions." + text3 + ".guildhall>";
				}
				else
				{
					text8 = "At a remote <spice.professions." + text3 + ".guildhall>";
				}
				if (base.Random(0, 1) == 0)
				{
					text8 = text8 + " in " + property;
					base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
				}
				text6 = string.Concat(new string[]
				{
					text8,
					", <entity.name> met with a group of <spice.professions.",
					text3,
					".plural> and commissioned ",
					Grammar.A(text4, false),
					" that evoked the presence of ",
					Grammar.A(text, false),
					" ",
					text2,
					"."
				});
				text7 = string.Format("{0} by a {1} {2} {3}, {4} visited a {5} {6} and commissioned a group of {7} to {8} {9}.", new object[]
				{
					"<spice.commonPhrases.inspired.!random.capitalize>",
					"<spice.commonPhrases.prized.!random>",
					text2,
					base.ExpandString("<spice.history.gospels.ObjectFoundBy." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
					"<entity.name>",
					"<spice.commonPhrases.eminent.!random>",
					"<spice.professions." + text3 + ".guildhall>",
					"<spice.professions." + text3 + ".plural>",
					"<spice.professions." + text3 + ".forge>",
					Grammar.A(text4, false)
				});
			}
			string text9 = Grammar.MakeTitleCase(QudHistoryHelpers.NameItemNounRoot(text2, this.history, this.entity));
			text6 = text6 + " <entity.subjectPronoun.capitalize> named it " + text9 + ".";
			text7 = text7 + " <entity.subjectPronoun.capitalize> named it " + text9 + ".";
			base.SetEventProperty("gospel", text6);
			base.SetEventProperty("tombInscription", text7);
			base.SetEventProperty("tombInscriptionCategory", "CreatesSomething");
			HistoricEntity historicEntity = this.history.CreateEntity(this.history.currentYear);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("itemType", text4);
			string text10;
			QudHistoryHelpers.ExtractArticle(ref text9, out text10);
			dictionary.Add("baseName", text9);
			dictionary.Add("article", text10);
			text9 = QudHistoryHelpers.Ansify(text9, text10);
			dictionary.Add("name", text9);
			dictionary.Add("descriptionAdj", text);
			dictionary.Add("descriptionNoun", text2);
			dictionary.Add("period", snapshotAtYear.GetProperty("period", "unknown"));
			historicEntity.ApplyEvent(new SetEntityProperties(dictionary, new Dictionary<string, string> { { "elements", randomElement } }), long.MinValue);
			base.AddEntityListItem("items", text9, false);
		}
	}
}
