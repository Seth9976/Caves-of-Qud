using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x02000602 RID: 1538
	[Serializable]
	public class FoundGuild : HistoricEvent
	{
		// Token: 0x06004183 RID: 16771 RVA: 0x0014CCA8 File Offset: 0x0014AEA8
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string property = snapshotAtYear.GetProperty("region", "unknown");
			string newRegion = QudHistoryHelpers.GetNewRegion(this.history, property);
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			base.SetEntityProperty("region", newRegion);
			string text;
			if (!snapshotAtYear.GetProperty("profession", "unknown").Equals("unknown"))
			{
				text = snapshotAtYear.GetProperty("profession", "unknown");
			}
			else
			{
				text = base.ExpandString("<spice.professions.!random>");
			}
			string text2 = base.ExpandString("<spice.elements." + randomElement + ".nounsPlural.!random>");
			string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
			base.AddEntityListItem("likedFactions", newFaction, false);
			HistoricEntity historicEntity = this.history.CreateEntity(this.history.currentYear);
			historicEntity.ApplyEvent(new InitializeLocation(newRegion, int.Parse(snapshotAtYear.GetProperty("period", "unknown"))), long.MinValue);
			string text3;
			do
			{
				if (base.Random(0, 1) == 0)
				{
					text3 = "the " + Grammar.MakeTitleCase(base.ExpandString(string.Concat(new string[] { "<spice.elements.", randomElement, ".adjectives.!random> <spice.professions.", text, ".guildhall>" })));
				}
				else
				{
					text3 = "the " + Grammar.MakeTitleCase(base.ExpandString(string.Concat(new string[] { "<spice.professions.", text, ".guildhall> of the <spice.elements.", randomElement, ".adjectives.!random>" })));
				}
			}
			while (this.history.GetEntitiesWherePropertyEquals("name", text3).Count > 0);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("name", text3);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2.Add("parameters", base.ExpandString("<spice.professions." + text + ".guildhall>"));
			historicEntity.ApplyEvent(new SetEntityProperties(dictionary, dictionary2), long.MinValue);
			new Dictionary<string, string>().Add("parameters", randomElement);
			historicEntity.ApplyEvent(new SetEntityProperties(null, dictionary2), long.MinValue);
			this.history.GetEntitiesWherePropertyEquals("name", newRegion).GetRandomElement(null).ApplyEvent(new AddLocationToRegion(text3), long.MinValue);
			string text4 = string.Concat(new string[]
			{
				"After <spice.commonPhrases.treating.!random> with ",
				Faction.GetFormattedName(newFaction),
				", <entity.name> convinced them to help <entity.objectPronoun> found <spice.professions.",
				text,
				".guildhall.article> in ",
				newRegion,
				" for the purpose of <spice.professions.",
				text,
				".studying> ",
				text2,
				". They named it ",
				text3,
				"."
			});
			base.SetEventProperty("gospel", text4);
			string text5 = base.ExpandString("<spice.history.gospels.CivilizationActivity." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>");
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>
			{
				{ "*var*", text2 },
				{ "*activity*", text5 }
			};
			string text6 = base.ExpandString("<spice.professions." + text + ".foundingVerbTombInscription.!random>", dictionary3);
			string text7 = string.Format("In {0} we {1} the {2} of {3}, where {4} {5}.", new object[]
			{
				QudHistoryHelpers.GenerateSultanateYearName(),
				base.ExpandString("<spice.instancesOf.rejoicedAt.!random>"),
				base.ExpandString("<spice.commonPhrases.inauguration.!random>"),
				text3,
				Faction.GetFormattedName(newFaction),
				text6
			});
			base.SetEventProperty("tombInscription", text7);
			base.SetEventProperty("tombInscriptionCategory", "CreatesSomething");
		}
	}
}
