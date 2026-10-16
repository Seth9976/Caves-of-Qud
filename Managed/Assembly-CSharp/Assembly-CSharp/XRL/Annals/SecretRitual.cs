using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x0200061E RID: 1566
	[Serializable]
	public class SecretRitual : HistoricEvent
	{
		// Token: 0x060041CB RID: 16843 RVA: 0x001507F0 File Offset: 0x0014E9F0
		public SecretRitual()
		{
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x001507F8 File Offset: 0x0014E9F8
		public SecretRitual(string _regionToReveal)
		{
			this.regionToReveal = _regionToReveal;
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x00150808 File Offset: 0x0014EA08
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
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
			string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
			int num = base.Random(0, 2);
			int num2 = base.Random(0, 1);
			string text;
			if (num == 0)
			{
				text = "deep in " + property + ", ";
			}
			else if (num == 1)
			{
				text = "while wandering around " + property + ", ";
			}
			else
			{
				text = "deep in the wilds of " + property + ", ";
			}
			base.SetEventProperty("revealsRegion", QudHistoryHelpers.GetRegionNewName(this.history, property));
			string text4;
			if (num2 == 0)
			{
				base.AddEntityListItem("likedFactions", newFaction, false);
				string text2 = base.ExpandString("<spice.elements.entity$elements[random].quality.!random>");
				string text3 = Grammar.InitCap(base.ExpandString(string.Concat(new string[]
				{
					text,
					"<entity.name> stumbled upon a clan of ",
					Faction.GetFormattedName(newFaction),
					" performing a secret ritual. Because of <entity.possessivePronoun> ",
					text2,
					", they accepted <entity.objectPronoun> into their fold and taught <entity.objectPronoun> their secrets."
				})));
				base.SetEventProperty("gospel", text3);
				text4 = string.Format("{0} when, {1}{2} taught a clan of {3} the ritual of the {4}, which can only be performed by one with {8} {5}. The {9} were {6}, and in return they accepted {7} into their fold and taught {7} their secrets.", new object[]
				{
					base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
					text,
					"<entity.name>",
					Faction.GetFormattedName(newFaction),
					Grammar.MakeTitleCase(base.ExpandString("<spice.history.gospels.RitualName." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>")),
					text2,
					base.ExpandString("<spice.commonPhrases.thankful.!random>"),
					"<entity.objectPronoun>",
					"<entity.possessivePronoun>",
					Factions.GetIfExists(newFaction).DisplayName
				});
			}
			else
			{
				base.AddEntityListItem("hatedFactions", newFaction, false);
				string text2 = base.ExpandString("<spice.elements.entity$elements[random].quality.!random>");
				string text3 = Grammar.InitCap(base.ExpandString(string.Concat(new string[]
				{
					text,
					"<entity.name> stumbled upon a clan of ",
					Faction.GetFormattedName(newFaction),
					" performing a secret ritual. Because of <entity.possessivePronoun> ",
					text2,
					", they furiously rebuked <entity.objectPronoun> and declared <entity.objectPronoun> a villain to their kind."
				})));
				base.SetEventProperty("gospel", text3);
				text4 = string.Format("{0} when, {1}{2} taught a clan of {3} the ritual of the {4}, which can only be performed by one with {8} {5}. The treacherous {3} {6} {7} and stole the secrets of the ritual.", new object[]
				{
					base.ExpandString("<spice.commonPhrases.remember.!random.capitalize>"),
					text,
					"<entity.name>",
					Faction.GetFormattedName(newFaction).Replace("the ", ""),
					Grammar.MakeTitleCase(base.ExpandString("<spice.history.gospels.RitualName." + QudHistoryHelpers.GetSultanateEra(snapshotAtYear) + ".!random>")),
					text2,
					base.ExpandString("<spice.instancesOf.brokeFaithWith.!random>"),
					"<entity.objectPronoun>",
					"<entity.possessivePronoun>"
				});
			}
			base.SetEventProperty("tombInscription", text4);
			base.SetEventProperty("tombInscriptionCategory", "LearnsSecret");
		}

		// Token: 0x0400204A RID: 8266
		public string regionToReveal;
	}
}
