using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x02000605 RID: 1541
	[Serializable]
	public class LiberateCity : HistoricEvent
	{
		// Token: 0x06004189 RID: 16777 RVA: 0x0014D318 File Offset: 0x0014B518
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("region", "unknown");
			string text = snapshotAtYear.GetProperty("location", "unknown");
			text = QudHistoryHelpers.GetNewLocationInRegion(this.history, property, text);
			string text2 = QudHistoryHelpers.GenerateLocationName(snapshotAtYear.GetProperty("name", "unknown"), this.history);
			QudHistoryHelpers.RenameLocation(text, text2, this.history);
			base.SetEntityProperty("location", text2);
			string text3 = "friend";
			string text4 = "enemy";
			string randomElement;
			string formattedName;
			string formattedName2;
			for (;;)
			{
				int num = base.Random(0, 4);
				if (num == 0)
				{
					randomElement = snapshotAtYear.GetList("colors").GetRandomElement<string>(null);
					if (snapshotAtYear.GetList("colors").Count != 0)
					{
						break;
					}
				}
				else if (num == 1)
				{
					formattedName = Faction.GetFormattedName(snapshotAtYear.GetList("likedFactions").GetRandomElement<string>(null));
					if (snapshotAtYear.GetList("likedFactions").Count != 0)
					{
						goto Block_4;
					}
				}
				else if (num == 2)
				{
					formattedName2 = Faction.GetFormattedName(snapshotAtYear.GetList("hatedFactions").GetRandomElement<string>(null));
					if (snapshotAtYear.GetList("hatedFactions").Count != 0)
					{
						goto Block_6;
					}
				}
				else
				{
					if (num != 3)
					{
						goto IL_0214;
					}
					if (!snapshotAtYear.GetProperty("profession", "unknown").Equals("unknown"))
					{
						goto Block_8;
					}
				}
			}
			string text5 = "Acting against the prohibition on the color " + randomElement + ", ";
			string text6 = text3 + " in " + randomElement;
			goto IL_024D;
			Block_4:
			text5 = "Acting against the persecution of " + formattedName + ", ";
			text6 = text3 + " to " + formattedName;
			goto IL_024D;
			Block_6:
			text5 = "Acting against the enfranchisement of " + formattedName2 + ", ";
			text6 = text4 + " to " + formattedName2;
			goto IL_024D;
			Block_8:
			string text7 = base.ExpandString("<spice.professions." + snapshotAtYear.GetProperty("profession", "unknown") + ".plural>");
			text5 = "Acting against labor laws restricting the rights of " + text7 + ", ";
			text6 = text3 + " to " + text7;
			goto IL_024D;
			IL_0214:
			string randomElement2 = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			text5 = "Acting against the prohibition on the practice of <spice.elements." + randomElement2 + ".practices.!random>, ";
			text6 = "child of <spice.elements." + randomElement2 + ".materials.!random>";
			IL_024D:
			string text9;
			if (base.Random(0, 1) == 0)
			{
				if (snapshotAtYear.GetProperty("isSultan", "unknown").Equals("true"))
				{
					string text8 = string.Concat(new string[] { text5, "<entity.name> led an army to the gates of ", text, ". <entity.subjectPronoun.capitalize> <spice.commonPhrases.liberated.!random> its citizens, and in <entity.possessivePronoun> honor they thenceforth called it ", text2, "." });
					base.SetEventProperty("gospel", text8);
					base.SetEventProperty("tombInscriptionCategory", "Resists");
					text9 = string.Format("{0} {1}, {2} and {3} of {4}, who led an army to its gates and {5} its {6} {7}. The people of the city {8} in {9}, {10} {11} name, and renamed {4} to {12} in {13} honor.", new object[]
					{
						"<spice.commonPhrases.love.!random.capitalize>",
						"<entity.name>",
						text6,
						"<spice.commonPhrases.savior.!random>",
						text,
						"<spice.instancesOf.unseated.!random>",
						"<spice.commonPhrases.corrupt.!random>",
						"<spice.commonPhrases.despots.!random>",
						base.ExpandString("<spice.history.gospels.Celebration." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
						"<spice.commonPhrases.celebration.!random>",
						"<spice.instancesOf.chantedOrSang.!random>",
						Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")),
						text2,
						"<entity.possessivePronoun>"
					});
				}
				else
				{
					string text8 = string.Concat(new string[]
					{
						text5,
						"<entity.name> led an army to the gates of ",
						text,
						". <entity.subjectPronoun.capitalize> <spice.commonPhrases.liberated.!random> its citizens, and they <spice.commonPhrases.crowned.!random> <entity.objectPronoun> sultan of Qud, dowering <entity.objectPronoun> with the ",
						QudHistoryHelpers.GetMaskName(snapshotAtYear),
						". In <entity.possessivePronoun> honor they changed the name of ",
						text,
						" to ",
						text2,
						"."
					});
					base.SetEventProperty("gospel", text8);
					base.SetEntityProperty("isSultan", "true");
					text9 = string.Format("{0} {1}, {2} and {3} of {4}, who led an army to its gates and {5} its {6} {7}. The people of the city {8} in {9}, dowered {1} with the {11}, and {14} {13} sultan of Qud. In {13} honor they renamed {4} to {12}.", new object[]
					{
						"<spice.commonPhrases.love.!random.capitalize>",
						"<entity.name>",
						text6,
						"<spice.commonPhrases.savior.!random>",
						text,
						"<spice.instancesOf.unseated.!random>",
						"<spice.commonPhrases.corrupt.!random>",
						"<spice.commonPhrases.despots.!random>",
						base.ExpandString("<spice.history.gospels.Celebration." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
						"<spice.commonPhrases.celebration.!random>",
						"<entity.objectPronoun>",
						QudHistoryHelpers.GetMaskName(snapshotAtYear),
						text2,
						"<entity.possessivePronoun>",
						"<spice.commonPhrases.crowned.!random>"
					});
					base.SetEventProperty("tombInscriptionCategory", "CrownedSultan");
				}
			}
			else
			{
				string text8 = string.Concat(new string[] { text5, "<entity.name> led an army to the gates of ", text, ". <entity.subjectPronoun.capitalize> sacked ", text, " and <spice.commonPhrases.slaughtered.!random> its citizens, forcing them to change its name to ", text2, "." });
				base.SetEventProperty("gospel", text8);
				text9 = string.Format("{0} {1}, {2} and {3} of {4}, who led an army to its gates and {5} its {6} {7}. The people of the city {8} in {9}, {10} {11} name, and renamed {4} to {12} in {13} honor.", new object[]
				{
					"<spice.commonPhrases.love.!random.capitalize>",
					"<entity.name>",
					text6,
					"<spice.commonPhrases.savior.!random>",
					text,
					"<spice.instancesOf.unseated.!random>",
					"<spice.commonPhrases.corrupt.!random>",
					"<spice.commonPhrases.despots.!random>",
					base.ExpandString("<spice.history.gospels.Celebration." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
					"<spice.commonPhrases.celebration.!random>",
					"<spice.instancesOf.chantedOrSang.!random>",
					Grammar.MakePossessive(snapshotAtYear.GetProperty("name", "unknown")),
					text2,
					"<entity.possessivePronoun>"
				});
				base.SetEventProperty("tombInscriptionCategory", "Resists");
			}
			base.SetEventProperty("tombInscription", text9);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("monuments", snapshotAtYear.GetProperty("name", "unknown"));
			this.history.GetEntitiesWherePropertyEquals("name", text2).GetRandomElement(null).ApplyEvent(new SetEntityProperties(null, dictionary), long.MinValue);
		}
	}
}
