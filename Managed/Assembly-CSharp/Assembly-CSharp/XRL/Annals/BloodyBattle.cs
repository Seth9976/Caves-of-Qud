using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;

namespace XRL.Annals
{
	// Token: 0x020005FB RID: 1531
	[Serializable]
	public class BloodyBattle : HistoricEvent
	{
		// Token: 0x06004171 RID: 16753 RVA: 0x0014AC29 File Offset: 0x00148E29
		public BloodyBattle()
		{
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x0014AC31 File Offset: 0x00148E31
		public BloodyBattle(string _regionToReveal)
		{
			this.regionToReveal = _regionToReveal;
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x0014AC40 File Offset: 0x00148E40
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string randomElement = snapshotAtYear.GetList("elements").GetRandomElement<string>(null);
			string property;
			if (string.IsNullOrEmpty(this.regionToReveal))
			{
				property = snapshotAtYear.GetProperty("region", "unknown");
			}
			else
			{
				property = this.regionToReveal;
				base.SetEntityProperty("region", this.regionToReveal);
			}
			string text = snapshotAtYear.GetProperty("location", "unknown");
			text = QudHistoryHelpers.GetNewLocationInRegion(this.history, property, text);
			string text2;
			if (base.Random(0, 1) == 0)
			{
				text2 = Grammar.MakeTitleCaseWithArticle("the " + base.ExpandString("<spice.elements." + randomElement + ".adjectives.!random> ") + base.ExpandString("<spice.commonPhrases.ruins.!random>"));
			}
			else
			{
				text2 = Grammar.MakeTitleCaseWithArticle(base.ExpandString("<spice.elements." + randomElement + ".adjectives.!random>") + base.ExpandString("<spice.commonPhrases.ruins.!random>"));
			}
			base.SetEntityProperty("location", text2);
			bool flag = false;
			if (snapshotAtYear.GetList("items").Count != 0)
			{
				flag = true;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (flag)
			{
				string randomElement2 = snapshotAtYear.GetList("items").GetRandomElement<string>(null);
				base.RemoveEntityListItem("items", randomElement2);
				dictionary.Add("items", randomElement2);
				string text3 = string.Concat(new string[]
				{
					"In %",
					this.year.ToString(),
					"%, <entity.name> won a decisive victory against the combined forces of ",
					property,
					" at the bloody Battle of ",
					text,
					", though <entity.subjectPronoun> lost <entity.possessivePronoun> prized ",
					randomElement2,
					" during the course of the conflict. As a result of the battle, ",
					text,
					" was so <spice.elements.",
					randomElement,
					".ruinReason> that it was renamed ",
					text2,
					"."
				});
				base.SetEventProperty("gospel", text3);
				History history = this.history;
				string text4;
				if (history == null)
				{
					text4 = null;
				}
				else
				{
					HistoricEntityList entitiesWherePropertyEquals = history.GetEntitiesWherePropertyEquals("name", property);
					if (entitiesWherePropertyEquals == null)
					{
						text4 = null;
					}
					else
					{
						HistoricEntity randomElement3 = entitiesWherePropertyEquals.GetRandomElement(null);
						if (randomElement3 == null)
						{
							text4 = null;
						}
						else
						{
							HistoricEntitySnapshot currentSnapshot = randomElement3.GetCurrentSnapshot();
							text4 = ((currentSnapshot != null) ? currentSnapshot.GetProperty("newName", "unknown") : null);
						}
					}
				}
				string text5 = text4;
				if (string.IsNullOrEmpty(text5))
				{
					text5 = property;
				}
				base.SetEventProperty("revealsItem", randomElement2);
				base.SetEventProperty("revealsItemLocation", text2);
				base.SetEventProperty("revealsItemRegion", text5);
			}
			else
			{
				string text3 = string.Concat(new string[]
				{
					"In %",
					this.year.ToString(),
					"%, <entity.name> won a decisive victory against the combined forces of ",
					property,
					" at the bloody Battle of ",
					text,
					". As a result of the battle, ",
					text,
					" was so <spice.elements.",
					randomElement,
					".ruinReason> that it was renamed ",
					text2,
					"."
				});
				base.SetEventProperty("gospel", text3);
			}
			string text6 = string.Format("In the {0}, {1} {2} the {3}, who {4}. Let the {5} of {6} serve as testament to the {7} of {1} and a {8} to those who would {9} {10}.", new object[]
			{
				QudHistoryHelpers.GenerateSultanateYearName(),
				"<entity.name>",
				base.ExpandString("<spice.commonPhrases.vanquished.!random>"),
				base.ExpandString("<spice.history.gospels.EnemyHostName." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
				base.ExpandString("<spice.history.gospels.CommittedWrongAgainstSultan." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
				"<spice.elements." + randomElement + ".ruinDescription>",
				text2,
				base.ExpandString("<spice.commonPhrases.might.!random>"),
				base.ExpandString("<spice.commonPhrases.warning.!random>"),
				base.ExpandString("<spice.commonPhrases.challenge.!random>"),
				"<entity.objectPronoun>"
			});
			base.SetEventProperty("tombInscription", text6);
			base.SetEventProperty("tombInscriptionCategory", "DoesSomethingDestructive");
			base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			QudHistoryHelpers.RenameLocation(text, text2, this.history);
			dictionary.Add("battles", randomElement);
			HistoricEntity randomElement4 = this.history.GetEntitiesWherePropertyEquals("name", text2).GetRandomElement(null);
			if (randomElement4 != null)
			{
				randomElement4.ApplyEvent(new SetEntityProperties(null, dictionary), long.MinValue);
			}
			HistoricEntity randomElement5 = this.history.GetEntitiesWherePropertyEquals("name", property).GetRandomElement(null);
			if (randomElement5 == null)
			{
				return;
			}
			randomElement5.ApplyEvent(new SetEntityProperties(null, dictionary), long.MinValue);
		}

		// Token: 0x0400201B RID: 8219
		public string regionToReveal;
	}
}
