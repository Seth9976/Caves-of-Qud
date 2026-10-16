using System;
using System.Collections.Generic;
using HistoryKit;
using UnityEngine;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x02000607 RID: 1543
	[Serializable]
	public class LoseItemAtTavern : HistoricEvent
	{
		// Token: 0x0600418C RID: 16780 RVA: 0x0014D9AD File Offset: 0x0014BBAD
		public LoseItemAtTavern()
		{
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0014D9B5 File Offset: 0x0014BBB5
		public LoseItemAtTavern(string _regionToReveal)
		{
			this.regionToReveal = _regionToReveal;
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0014D9C4 File Offset: 0x0014BBC4
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string newRegion;
			if (string.IsNullOrEmpty(this.regionToReveal))
			{
				newRegion = QudHistoryHelpers.GetNewRegion(this.history, snapshotAtYear.GetProperty("region", "unknown"));
			}
			else
			{
				newRegion = this.regionToReveal;
			}
			int num = int.Parse(snapshotAtYear.GetProperty("period", "unknown"));
			string randomLocationInRegionByPeriod = QudHistoryHelpers.GetRandomLocationInRegionByPeriod(this.history, newRegion, num);
			if (string.IsNullOrEmpty(randomLocationInRegionByPeriod))
			{
				Debug.LogError("LoseItemAtTavern::Generate could not get a random historical location in region " + newRegion + " for period " + num.ToString());
				return;
			}
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string text = base.ExpandString("<spice.elements." + randomElement + ".adjectives.!random>");
			base.SetEntityProperty("region", newRegion);
			string newLocationInRegion = QudHistoryHelpers.GetNewLocationInRegion(this.history, newRegion, randomLocationInRegionByPeriod);
			base.SetEntityProperty("location", newLocationInRegion);
			string text2 = null;
			bool flag = false;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string text3;
			if (snapshotAtYear.GetList("items").Count != 0)
			{
				text3 = snapshotAtYear.GetList("items").GetRandomElement<string>(null);
				base.RemoveEntityListItem("items", text3);
			}
			else
			{
				flag = true;
				do
				{
					text3 = Grammar.MakeTitleCase(QudHistoryHelpers.NameItemAdjRoot(text, this.history, this.entity) + " " + Grammar.GetRandomMeaningfulWord(snapshotAtYear.GetProperty("name", "unknown")) + base.ExpandString("<spice.commonPhrases.boon.!random>"));
				}
				while (this.history.GetEntitiesWherePropertyEquals("name", text3).Count > 0);
				text2 = base.ExpandString("<spice.items.types.!random>");
				HistoricEntity historicEntity = this.history.CreateEntity(this.year);
				dictionary.Add("itemType", text2);
				string text4;
				QudHistoryHelpers.ExtractArticle(ref text3, out text4);
				dictionary.Add("baseName", text3);
				dictionary.Add("article", text4);
				text3 = QudHistoryHelpers.Ansify(text3, text4);
				dictionary.Add("name", text3);
				dictionary.Add("descriptionAdj", text);
				dictionary.Add("location", randomLocationInRegionByPeriod);
				dictionary.Add("period", snapshotAtYear.GetProperty("period", "unknown"));
				historicEntity.ApplyEvent(new SetEntityProperties(dictionary, null), long.MinValue);
			}
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2.Add("items", text3);
			HistoricEntityList entitiesWherePropertyEquals = this.history.GetEntitiesWherePropertyEquals("name", randomLocationInRegionByPeriod);
			if (entitiesWherePropertyEquals.Count == 0)
			{
				Debug.LogError("no matching location (1) for " + randomLocationInRegionByPeriod);
			}
			HistoricEntity randomElement2 = entitiesWherePropertyEquals.GetRandomElement(null);
			if (randomElement2 == null)
			{
				Debug.LogError("no matching location (2) " + randomLocationInRegionByPeriod);
			}
			if (randomElement2 != null)
			{
				randomElement2.ApplyEvent(new SetEntityProperties(null, dictionary2), long.MinValue);
				HistoricEntityList entitiesWherePropertyEquals2 = this.history.GetEntitiesWherePropertyEquals("name", newRegion);
				if (entitiesWherePropertyEquals2 != null)
				{
					entitiesWherePropertyEquals2.GetRandomElement(null).ApplyEvent(new SetEntityProperties(null, dictionary2), long.MinValue);
				}
			}
			if (flag)
			{
				string text5 = string.Concat(new string[]
				{
					"While traveling through ",
					newRegion,
					", <entity.name> stopped at a market in ",
					randomLocationInRegionByPeriod,
					". At an obscure shop, <entity.subjectPronoun> purchased ",
					Grammar.A(text, false),
					" ",
					text2,
					" and named it ",
					text3,
					". Then <entity.subjectPronoun> went to a nearby tavern and lost ",
					text3,
					" <spice.commonPhrases.lostInTavern.!random>. <entity.subjectPronoun.capitalize> cursed the tavern and left ",
					randomLocationInRegionByPeriod,
					"."
				});
				base.SetEventProperty("gospel", text5);
			}
			else
			{
				string text5 = string.Concat(new string[] { "While traveling through ", newRegion, ", <entity.name> stopped at a tavern in ", randomLocationInRegionByPeriod, ". There <entity.subjectPronoun> lost <entity.possessivePronoun> prized ", text3, " <spice.commonPhrases.lostInTavern.!random>. <entity.subjectPronoun.capitalize> cursed the tavern and left ", randomLocationInRegionByPeriod, "." });
				base.SetEventProperty("gospel", text5);
			}
			string text6 = string.Format("{0} {1} of {2}! It was there that {3} {4}.", new object[]
			{
				base.ExpandString("<spice.instancesOf.aCurseUpon.!random.capitalize>"),
				randomLocationInRegionByPeriod,
				newRegion,
				"<entity.name>",
				base.ExpandString("<spice.history.gospels.LostItem." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>").Replace("*item*", text3)
			});
			base.SetEventProperty("tombInscription", text6);
			base.SetEventProperty("tombInscriptionCategory", "CommitsFolly");
			string regionNewName = QudHistoryHelpers.GetRegionNewName(this.history, newRegion);
			base.SetEventProperty("revealsRegion", regionNewName);
			base.SetEventProperty("revealsItem", text3);
			base.SetEventProperty("revealsItemLocation", randomLocationInRegionByPeriod);
			base.SetEventProperty("revealsItemRegion", regionNewName);
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			dictionary3.Add("parameters", "tavern");
			HistoricEntity randomElement3 = this.history.GetEntitiesWherePropertyEquals("name", randomLocationInRegionByPeriod).GetRandomElement(null);
			if (randomElement3 != null)
			{
				randomElement3.ApplyEvent(new SetEntityProperties(null, dictionary3), long.MinValue);
				return;
			}
			Debug.LogError("LoseItemAtTavern::Generate could not find an entity with name=" + randomLocationInRegionByPeriod);
		}

		// Token: 0x0400201E RID: 8222
		public string regionToReveal;
	}
}
