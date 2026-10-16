using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x020005FF RID: 1535
	[Serializable]
	public class CorruptAdministrator : HistoricEvent
	{
		// Token: 0x0600417B RID: 16763 RVA: 0x0014C013 File Offset: 0x0014A213
		public CorruptAdministrator()
		{
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x0014C01B File Offset: 0x0014A21B
		public CorruptAdministrator(string _regionToReveal)
		{
			this.regionToReveal = _regionToReveal;
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x0014C02C File Offset: 0x0014A22C
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string text = null;
			string text2 = QudHistoryHelpers.GenerateSultanateYearName();
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
			base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			int num = base.Random(0, 1);
			List<string> list = snapshotAtYear.GetList("likedFactions");
			if (list.Count == 0)
			{
				num = 0;
			}
			if (num == 0)
			{
				string text3 = base.ExpandString("<spice.elements.entity$elements[random].practices.!random>");
				if (!snapshotAtYear.GetProperty("isSultan", "unknown").Equals("true"))
				{
					string newRegion = QudHistoryHelpers.GetNewRegion(this.history, property);
					base.SetEntityProperty("region", newRegion);
					base.SetEntityProperty("location", QudHistoryHelpers.GetRandomLocationInRegionByPeriod(this.history, newRegion, int.Parse(snapshotAtYear.GetProperty("period", "unknown"))));
					base.SetEventProperty("gospel", string.Concat(new string[]
					{
						"In %",
						this.year.ToString(),
						"%, a corrupt administrator was appointed minister of ",
						property,
						". ",
						Grammar.InitCap(Grammar.RandomShePronoun()),
						" outlawed the practice of ",
						text3,
						", and <entity.name> was forced to flee."
					}));
					text = string.Format("{0} the {1}, when a corrupt administrator was appointed minister of {2}. {3} outlawed the practice of {4}, and {5} was forced to flee.", new object[]
					{
						base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
						text2,
						property,
						Grammar.InitCap(Grammar.RandomShePronoun()),
						text3,
						"<entity.name>"
					});
					base.SetEventProperty("tombInscriptionCategory", "EnduresHardship");
				}
				else
				{
					base.SetEventProperty("gospel", string.Concat(new string[]
					{
						"In %",
						this.year.ToString(),
						"%, <entity.name> appointed a corrupt administrator as minister of ",
						property,
						". ",
						Grammar.InitCap(Grammar.RandomShePronoun()),
						" mandated the practice of ",
						text3,
						" in <entity.name>'s name."
					}));
					text = string.Format("{0} the {1}, when the {2} {10} of {3} was {4} in {5}. {0} that {6}, to {7} {8}, appointed a minister who mandated the practice of {9}.", new object[]
					{
						base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
						text2,
						"moral",
						base.ExpandString("<spice.history.gospels.ImmoralPractice." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
						base.ExpandString("<spice.commonPhrases.rife.!random>"),
						property,
						"<entity.name>",
						base.ExpandString("<spice.commonPhrases.restore.!random>"),
						base.ExpandString("<spice.commonPhrases.morality.!random>"),
						text3,
						base.ExpandString("<spice.commonPhrases.depravity.!random>")
					});
					base.SetEventProperty("tombInscriptionCategory", "DoesBureaucracy");
				}
			}
			else if (num == 1)
			{
				string formattedName = Faction.GetFormattedName(list[base.Random(0, list.Count - 1)]);
				if (!snapshotAtYear.GetProperty("isSultan", "unknown").Equals("true"))
				{
					string newRegion2 = QudHistoryHelpers.GetNewRegion(this.history, property);
					base.SetEntityProperty("region", newRegion2);
					base.SetEntityProperty("location", QudHistoryHelpers.GetRandomLocationInRegionByPeriod(this.history, newRegion2, int.Parse(snapshotAtYear.GetProperty("period", "unknown"))));
					base.SetEventProperty("gospel", string.Concat(new string[]
					{
						"In %",
						this.year.ToString(),
						"%, a corrupt administrator was appointed minister of ",
						property,
						". ",
						Grammar.InitCap(Grammar.RandomShePronoun()),
						" outlawed association with ",
						formattedName,
						", and <entity.name> was forced to flee."
					}));
					text = string.Format("{0} the {1}, when a corrupt administrator was appointed minister of {2}. {3} outlawed association with {4}, and {5} was forced to flee.", new object[]
					{
						base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
						text2,
						property,
						Grammar.InitCap(Grammar.RandomShePronoun()),
						formattedName,
						"<entity.name>"
					});
					base.SetEventProperty("tombInscriptionCategory", "EnduresHardship");
				}
				else
				{
					base.SetEventProperty("gospel", string.Concat(new string[]
					{
						"In %",
						this.year.ToString(),
						"%, <entity.name> appointed a corrupt administrator as minister of ",
						property,
						". ",
						Grammar.InitCap(Grammar.RandomShePronoun()),
						" mandated association with ",
						formattedName,
						" in <entity.name>'s name."
					}));
					text = string.Format("{0} the {1}, when the {2} {10} of {3} was {4} in {5}. {0} that {6}, to {7} {8}, appointed a minister who mandated association with {9}.", new object[]
					{
						base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
						text2,
						"moral",
						base.ExpandString("<spice.history.gospels.ImmoralPractice." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>"),
						base.ExpandString("<spice.commonPhrases.rife.!random>"),
						property,
						"<entity.name>",
						base.ExpandString("<spice.commonPhrases.restore.!random>"),
						base.ExpandString("<spice.commonPhrases.morality.!random>"),
						formattedName,
						base.ExpandString("<spice.commonPhrases.depravity.!random>")
					});
					base.SetEventProperty("tombInscriptionCategory", "DoesBureaucracy");
				}
			}
			base.SetEventProperty("tombInscription", text);
		}

		// Token: 0x0400201D RID: 8221
		public string regionToReveal;
	}
}
