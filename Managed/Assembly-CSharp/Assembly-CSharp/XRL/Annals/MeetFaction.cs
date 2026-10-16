using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x02000609 RID: 1545
	[Serializable]
	public class MeetFaction : HistoricEvent
	{
		// Token: 0x06004191 RID: 16785 RVA: 0x0014E392 File Offset: 0x0014C592
		public MeetFaction()
		{
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x0014E39A File Offset: 0x0014C59A
		public MeetFaction(string _regionToReveal)
		{
			this.regionToReveal = _regionToReveal;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x0014E3AC File Offset: 0x0014C5AC
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string text = QudHistoryHelpers.GenerateSultanateYearName();
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
			HistoricEntity historicEntity = this.history.CreateEntity(this.history.currentYear);
			historicEntity.ApplyEvent(new InitializeLocation(property, int.Parse(snapshotAtYear.GetProperty("period", "unknown"))), long.MinValue);
			string property2 = historicEntity.GetSnapshotAtYear(this.year).GetProperty("name", "unknown");
			this.history.GetEntitiesWherePropertyEquals("name", property).GetRandomElement(null).ApplyEvent(new AddLocationToRegion(property2), long.MinValue);
			base.SetEntityProperty("location", property2);
			string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
			base.AddEntityListItem("likedFactions", newFaction, false);
			string text2 = base.ExpandString("<spice.elements.!random>");
			while (snapshotAtYear.GetList("elements").Contains(text2))
			{
				text2 = base.ExpandString("<spice.elements.!random>");
			}
			int num = base.Random(0, 2);
			int num2 = base.Random(0, 1);
			string text3;
			if (num == 0)
			{
				text3 = "deep in " + property + ", ";
			}
			else if (num == 1)
			{
				text3 = "while wandering around " + property + ", ";
			}
			else
			{
				text3 = "deep in the wilds of " + property + ", ";
			}
			base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			if (snapshotAtYear.GetProperty("profession", "unknown").Equals("unknown"))
			{
				num2 = 0;
			}
			string text5;
			string text6;
			if (num2 == 0)
			{
				string text4 = base.ExpandString("<spice.elements.entity$elements[random].professions.!random>");
				text5 = base.ExpandString("<spice.professions." + text4 + ".actions.!random>");
				text6 = Grammar.InitialCap(base.ExpandString(string.Concat(new string[]
				{
					text3,
					"<entity.name> discovered ",
					historicEntity.GetSnapshotAtYear(this.history.currentYear).GetProperty("name", "unknown"),
					". There <entity.subjectPronoun> befriended ",
					Faction.GetFormattedName(newFaction),
					" and ",
					text5,
					"."
				})));
				base.SetEventProperty("gospel", text6);
			}
			else
			{
				text5 = base.ExpandString("<spice.professions.entity$profession.actions.!random>");
				text6 = Grammar.InitialCap(base.ExpandString(string.Concat(new string[]
				{
					text3,
					"<entity.name> discovered ",
					historicEntity.GetSnapshotAtYear(this.history.currentYear).GetProperty("name", "unknown"),
					". There <entity.subjectPronoun> befriended ",
					Faction.GetFormattedName(newFaction),
					" and ",
					text5,
					"."
				})));
				base.SetEventProperty("gospel", text6);
			}
			string text7 = string.Format("Let us {0} the {1}, when, {2}{3} {4} {5} and {6} the {7} {8} who lived there. To {9} {10} {11} to them, {12} {13}.", new object[]
			{
				base.ExpandString("<spice.commonPhrases.remember.!random>"),
				text,
				text3,
				snapshotAtYear.GetProperty("name", "unknown"),
				base.ExpandString("<spice.commonPhrases.conquered.!random>"),
				property2,
				base.ExpandString("<spice.commonPhrases.tamed.!random>"),
				base.ExpandString("<spice.commonPhrases.wild.!random>"),
				Faction.GetFormattedName(newFaction),
				base.ExpandString("<spice.commonPhrases.demonstrate.!random>"),
				"<entity.possessivePronoun>",
				base.ExpandString("<spice.commonPhrases.wisdom.!random>"),
				"<entity.subjectPronoun>",
				text5
			});
			if (If.Chance(80, null, null))
			{
				base.SetEventProperty("tombInscription", base.ExpandString(text7));
			}
			else
			{
				base.SetEventProperty("tombInscription", text6);
			}
			base.SetEventProperty("tombInscriptionCategory", "Treats");
		}

		// Token: 0x0400201F RID: 8223
		public string regionToReveal;
	}
}
