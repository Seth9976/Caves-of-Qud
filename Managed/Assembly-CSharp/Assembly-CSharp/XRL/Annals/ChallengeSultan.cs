using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x020005FD RID: 1533
	[Serializable]
	public class ChallengeSultan : HistoricEvent
	{
		// Token: 0x06004177 RID: 16759 RVA: 0x0014B408 File Offset: 0x00149608
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("region", "unknown");
			int num = base.Random(0, 2);
			string text;
			if (num == 0)
			{
				text = "In %" + this.year.ToString() + "%, ";
			}
			else if (num == 1)
			{
				text = "While leading a small army in " + property + ", ";
				base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			}
			else if (num == 2)
			{
				text = "In early %" + this.year.ToString() + "%, ";
			}
			else
			{
				text = "In late %" + this.year.ToString() + "%, ";
			}
			int num2 = 0;
			if (snapshotAtYear.GetProperty("isSultan", "unknown").EqualsNoCase("true"))
			{
				num2 = 1;
			}
			string text6;
			string text7;
			if (num2 == 0)
			{
				base.SetEntityProperty("isSultan", "true");
				int num3 = base.Random(0, 2);
				string text2;
				if (num3 == 0)
				{
					text2 = " challenged the sultan of Qud to a duel";
				}
				else if (num3 == 1)
				{
					text2 = " challenged the legitimacy of the sultan of Qud";
				}
				else
				{
					text2 = " assassinated the sultan of Qud";
				}
				int num4 = base.Random(0, 2);
				if (snapshotAtYear.GetList("likedFactions").Count == 0 && snapshotAtYear.GetProperty("profession", "unknown").EqualsNoCase("unknown"))
				{
					num4 = 1;
				}
				else if (snapshotAtYear.GetList("likedFactions").Count == 0)
				{
					num4 = base.Random(1, 2);
				}
				else if (snapshotAtYear.GetProperty("profession", "unknown").Equals("unknown"))
				{
					num4 = base.Random(0, 1);
				}
				string text3;
				string text4;
				if (num4 == 0)
				{
					string randomElement = snapshotAtYear.GetList("likedFactions").GetRandomElement<string>(null);
					text3 = " over the rights of " + Faction.GetFormattedName(randomElement);
					text4 = "venerating " + Faction.GetFormattedName(randomElement);
				}
				else if (num4 == 1)
				{
					text4 = base.ExpandString("<spice.elements.entity$elements[random].practices.!random>");
					text3 = " over an ordinance prohibiting the practice of " + text4;
				}
				else
				{
					string text5 = base.ExpandString("<spice.professions.entity$profession.plural>");
					text3 = " over the sanctioned persecution of " + text5;
					text4 = "venerating " + text5;
				}
				int num5 = (int)(this.year - this.entity.firstYear);
				text6 = string.Concat(new string[]
				{
					text,
					"<entity.name>",
					text2,
					text3,
					". <entity.subjectPronoun.capitalize> won and <spice.commonPhrases.ascended.!random>. <entity.subjectPronoun.capitalize> was ",
					Grammar.Cardinal(num5),
					" years old."
				});
				base.SetEventProperty("gospel", text6);
				text7 = string.Format("In {0}, {1} averred {9} descendancy from the Fossilized Saads, who were known for {2}. By {3} right, {4} ousted the pretender sultan, who had prohibited {5} in favor of {6} worship, and crowned {7} sultan of Qud. {10} was {8} years old.", new object[]
				{
					property,
					"<entity.name>",
					text4,
					"<spice.commonPhrases.sacred.!random>",
					"<entity.subjectPronoun>",
					text4,
					base.ExpandString("<spice.history.gospels." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".worshipObject.!random>"),
					"<entity.reflexivePronoun>",
					Grammar.Cardinal(num5),
					"<entity.possessivePronoun>",
					"<entity.subjectPronoun.capitalize>"
				});
				base.SetEventProperty("tombInscription", text7);
				base.SetEventProperty("tombInscriptionCategory", "CrownedSultan");
				return;
			}
			int num6 = base.Random(0, 2);
			string text8;
			if (num6 == 0)
			{
				text8 = " was challenged by <spice.commonPhrases.pretender.!random.article> to a duel";
			}
			else if (num6 == 1)
			{
				text8 = " had <entity.possessivePronoun> legitimacy challenged by <spice.commonPhrases.pretender.!random.article>";
			}
			else
			{
				text8 = " was assailed by <spice.commonPhrases.pretender.!random.article>";
			}
			int num7 = base.Random(0, 2);
			if (snapshotAtYear.GetList("hatedFactions").Count == 0 && snapshotAtYear.GetList("hatedProfessions").Count == 0)
			{
				num7 = 1;
			}
			else if (snapshotAtYear.GetList("hatedFactions").Count == 0)
			{
				num7 = base.Random(1, 2);
			}
			else if (snapshotAtYear.GetList("hatedProfessions").Count == 0)
			{
				num7 = base.Random(0, 1);
			}
			string text9;
			string text10;
			if (num7 == 0)
			{
				string randomElement2 = snapshotAtYear.GetList("hatedFactions").GetRandomElement<string>(null);
				text9 = " over the rights of " + Faction.GetFormattedName(randomElement2);
				text10 = "venerating " + Faction.GetFormattedName(randomElement2);
			}
			else if (num7 == 1)
			{
				text10 = base.ExpandString("<spice.elements.entity$elements[random].practices.!random>");
				text9 = " over an ordinance mandating the practice of " + text10;
			}
			else
			{
				string text11 = base.ExpandString("<spice.professions.entity$profession.plural>");
				text9 = " over the sanctioned persecution of " + text11;
				text10 = "venerating " + text11;
			}
			int num8 = (int)(this.history.currentYear - this.entity.firstYear);
			if (90.in100())
			{
				text6 = string.Concat(new string[]
				{
					text,
					"<entity.name>",
					text8,
					text9,
					". <entity.subjectPronoun.capitalize> won and had the <spice.commonPhrases.pretender.!random> <spice.commonPhrases.killed.!random>. <entity.subjectPronoun.capitalize> was ",
					Grammar.Cardinal(num8),
					" years old."
				});
				base.SetEventProperty("gospel", text6);
				text7 = string.Format("In the {0}, {1} {2} claimed descendancy from the Fossilized Saads and challenged {3} over the tradition of {4}. {5}, {3} won and had the {6} {7}.", new object[]
				{
					QudHistoryHelpers.GenerateSultanateYearName(),
					"an",
					"aspirant",
					"<entity.name>",
					text10,
					"<spice.instancesOf.ofCourse.!random.capitalize>",
					"<spice.commonPhrases.pretender.!random>",
					"<spice.commonPhrases.killed.!random>"
				});
				base.SetEventProperty("tombInscription", text7);
				base.SetEventProperty("tombInscriptionCategory", "Slays");
				return;
			}
			text6 = string.Concat(new string[]
			{
				text,
				"<entity.name>",
				text8,
				text9,
				". <entity.subjectPronoun.capitalize> lost and was <spice.commonPhrases.killed.!random>. <entity.subjectPronoun.capitalize> was ",
				Grammar.Cardinal(num8),
				" years old."
			});
			base.SetEventProperty("gospel", text6);
			base.SetEntityProperty("isAlive", "false");
			text7 = string.Format("In the {0}, an aspirant claimed descendancy from the Fossilized Saads and challenged {1} over the tradition of {2}. The aspirant won and {1} was {3}. {4} was {5} years old.", new object[]
			{
				QudHistoryHelpers.GenerateSultanateYearName(),
				"<entity.name>",
				text10,
				"<spice.commonPhrases.killed.!random>",
				"<entity.subjectPronoun.capitalize>",
				Grammar.Cardinal(num8)
			});
			base.SetEventProperty("tombInscription", text7);
			base.SetEventProperty("tombInscriptionCategory", "Dies");
		}
	}
}
