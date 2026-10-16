using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.Names;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x02000608 RID: 1544
	[Serializable]
	public class Marry : HistoricEvent
	{
		// Token: 0x06004190 RID: 16784 RVA: 0x0014DEC8 File Offset: 0x0014C0C8
		public override void Generate()
		{
			this.duration = 0L;
			HistoricEntitySnapshot currentSnapshot = this.entity.GetCurrentSnapshot();
			string randomElement = currentSnapshot.GetList("elements").GetRandomElement<string>(null);
			string text = base.ExpandString("<spice.elements." + randomElement + ".weddingConditions.!random>");
			string text2 = null;
			bool flag = false;
			string text3;
			string text4;
			string text7;
			if (50.in100())
			{
				string newFaction = QudHistoryHelpers.GetNewFaction(this.entity);
				base.AddEntityListItem("likedFactions", newFaction, false);
				List<GameObjectBlueprint> factionMembers = GameObjectFactory.Factory.GetFactionMembers(newFaction, false, true);
				if (factionMembers.Count > 0)
				{
					text3 = NameMaker.MakeName(GameObject.Create(factionMembers.GetRandomElement<GameObjectBlueprint>(null).Name), null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
				}
				else
				{
					text3 = NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
				}
				text4 = Faction.GetFormattedName(newFaction);
				if (90.in100())
				{
					string text5 = base.ExpandString("<spice.items.types.!random>");
					text2 = QudHistoryHelpers.NameItemNounRoot(base.ExpandString("<spice.commonPhrases.marriage.!random>"), this.history, null) + ", " + Grammar.GetRandomMeaningfulWord(text4) + base.ExpandString("<spice.commonPhrases.gift.!random>");
					text2 = Grammar.MakeTitleCase(text2);
					HistoricEntity historicEntity = this.history.CreateEntity(this.year);
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					dictionary.Add("itemType", text5);
					string text6;
					QudHistoryHelpers.ExtractArticle(ref text2, out text6);
					dictionary.Add("baseName", text2);
					dictionary.Add("article", text6);
					text2 = QudHistoryHelpers.Ansify(text2, null);
					dictionary.Add("name", text2);
					dictionary.Add("period", currentSnapshot.GetProperty("period", "unknown"));
					historicEntity.ApplyEvent(new SetEntityProperties(dictionary, new Dictionary<string, string>
					{
						{ "lovedFactions", newFaction },
						{ "elements", "none" }
					}), long.MinValue);
					base.AddEntityListItem("items", text2, false);
					flag = true;
				}
				text7 = Grammar.Pluralize(base.ExpandString("<spice.placeNouns.!random>"));
			}
			else
			{
				string text8 = base.ExpandString("<spice.professions.!random>");
				text4 = base.ExpandString("<spice.professions." + text8 + ".plural>");
				base.SetEntityProperty("profession", text8);
				base.SetEntityProperty("professionRank", base.ExpandString("<spice.professions." + text8 + ".training>"));
				text3 = NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
				text7 = Grammar.Pluralize(base.ExpandString("<spice.professions." + text8 + ".guildhall>"));
			}
			string text9;
			if (base.Random(0, 100) < 70)
			{
				text9 = ((currentSnapshot.GetProperty("subjectPronoun", "unknown") == "he") ? "Husband" : "Wife");
			}
			else
			{
				text9 = Grammar.MakeTitleCase(base.ExpandString("<spice.commonPhrases.spouse.!random>"));
			}
			string text10;
			if (30.in100())
			{
				text10 = Grammar.MakePossessive(text3) + " " + text9;
			}
			else if (50.in100())
			{
				text10 = text9 + " to " + text3;
			}
			else
			{
				text10 = text9 + " of " + text3;
			}
			base.AddEntityListItem("cognomen", text10, false);
			string text11 = string.Format("{0}, {1} cemented {2} friendship with {3} by marrying {4}.", new object[]
			{
				text,
				currentSnapshot.GetProperty("name", "unknown"),
				currentSnapshot.GetProperty("possessivePronoun", "unknown"),
				text4,
				text3
			});
			if (flag)
			{
				text11 += string.Format(" {0}, {1} bestowed upon {2} a wedding gift they called {3}.", new object[]
				{
					Grammar.InitCap(base.ExpandString("<spice.commonPhrases.inHonorOf.!random>")),
					"the " + text4,
					currentSnapshot.GetProperty("name", "unknown"),
					text2
				});
			}
			base.SetEventProperty("gospel", Grammar.InitCap(text11));
			string text12 = string.Format("{0}, {1} and {2}, whose {3} alliance by marriage {4}.", new object[]
			{
				"<spice.instancesOf.marriageBlessing.!random.capitalize>",
				"<entity.name>",
				text3,
				"<spice.commonPhrases.historic.!random>",
				base.ExpandString("<spice.history.gospels.MarriageAllianceResult." + QudHistoryHelpers.GetSultanateEra(currentSnapshot) + ".!random>").Replace("*group*", text4).Replace("*hall*", text7)
			});
			base.SetEventProperty("tombInscription", text12);
			base.SetEventProperty("tombInscriptionCategory", "Trysts");
		}
	}
}
