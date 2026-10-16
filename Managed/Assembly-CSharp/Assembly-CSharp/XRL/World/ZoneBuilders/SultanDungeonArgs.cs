using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF3 RID: 2803
	[Serializable]
	public class SultanDungeonArgs : IComposite
	{
		// Token: 0x06007062 RID: 28770 RVA: 0x0027F446 File Offset: 0x0027D646
		public static string translateArg(string input)
		{
			if (SultanDungeonArgs.sultanArgumentClusters.ContainsKey(input))
			{
				return SultanDungeonArgs.sultanArgumentClusters[input];
			}
			return input;
		}

		// Token: 0x06007063 RID: 28771 RVA: 0x0027F462 File Offset: 0x0027D662
		public void UpdateWalls(int Period, string tablePrefix = "SultanDungeons_")
		{
			this.wallTypes.Add(PopulationManager.RollOneFrom(tablePrefix + "Wall_Default_Period" + Period.ToString(), null, null).Blueprint);
		}

		// Token: 0x06007064 RID: 28772 RVA: 0x0027F490 File Offset: 0x0027D690
		public void UpdateFromEntity(HistoricEntitySnapshot snap, string tablePrefix = "SultanDungeons_")
		{
			this.tablePrefix = tablePrefix;
			this.properties = new List<string>();
			if (snap.properties.ContainsKey("cultName"))
			{
				this.cultName = snap.properties["cultName"];
				this.cultPeriod = snap.properties["period"];
				this.cultFaction = Faction.GetSultanFactionName(this.cultPeriod);
				if (snap.listProperties.ContainsKey("likedFactions"))
				{
					this.cultFactions.AddRange(snap.listProperties["likedFactions"]);
				}
				if (snap.listProperties.ContainsKey("hatedFactions"))
				{
					this.enemyFactions.AddRange(snap.listProperties["hatedFactions"]);
				}
			}
			else
			{
				foreach (string text in snap.properties.Values)
				{
					string text2 = SultanDungeonArgs.translateArg(text.ToLower());
					if (!this.properties.Contains(text2))
					{
						this.properties.Add(text2);
					}
				}
				foreach (List<string> list in snap.listProperties.Values)
				{
					foreach (string text3 in list)
					{
						string text4 = SultanDungeonArgs.translateArg(text3.ToLower());
						if (!this.properties.Contains(text4))
						{
							this.properties.Add(text4);
						}
					}
				}
				foreach (string text5 in this.properties)
				{
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Wall_" + text5))
					{
						this.wallTypes.Add(PopulationManager.RollOneFrom(tablePrefix + "Wall_" + text5, null, "Fulcrete").Blueprint);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "PrimaryTemplate_" + text5))
					{
						this.primaryTemplate.Add(PopulationManager.RollOneFrom(tablePrefix + "PrimaryTemplate_" + text5, null, "smalltemple,W").Blueprint);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "SecondaryTemplate_" + text5))
					{
						this.secondaryTemplate.Add(PopulationManager.RollOneFrom(tablePrefix + "SecondaryTemplate_" + text5, null, "gardens").Blueprint);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "DetailTemplate_" + text5))
					{
						this.detailTemplate.Add(PopulationManager.RollOneFrom(tablePrefix + "DetailTemplate_" + text5, null, "gardens").Blueprint);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Green_" + text5))
					{
						this.greenObjects.Add(tablePrefix + "Green_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Blue_" + text5))
					{
						this.blueObjects.Add(tablePrefix + "Blue_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Yellow_" + text5))
					{
						this.yellowObjects.Add(tablePrefix + "Yellow_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Segmentations_" + text5))
					{
						this.segmentations.Add(tablePrefix + "Segmentations_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Furnishings_" + text5))
					{
						this.furnishings.Add(tablePrefix + "Furnishings_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Halls_" + text5))
					{
						this.halls.Add(tablePrefix + "Halls_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Cubbies_" + text5))
					{
						this.cubbies.Add(tablePrefix + "Cubbies_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "Encounters_" + text5))
					{
						this.encounters.Add(tablePrefix + "Encounters_" + text5);
					}
					if (PopulationManager.Populations.ContainsKey(tablePrefix + "PreconnectEncounters_" + text5))
					{
						this.preconnectencounters.Add(tablePrefix + "PreconnectEncounters_" + text5);
					}
				}
				snap.listProperties.ContainsKey("battles");
			}
			if (this.wallTypes.Count == 0)
			{
				this.wallTypes.Add(PopulationManager.RollOneFrom(tablePrefix + "Wall_*Default", null, "Fulcrete").Blueprint);
			}
			if (this.primaryTemplate.Count == 0)
			{
				int num = Stat.Random(1, 2);
				for (int i = 0; i < num; i++)
				{
					this.primaryTemplate.Add(PopulationManager.RollOneFrom(tablePrefix + "PrimaryTemplate_*Default", null, "smalltemple,W").Blueprint);
				}
			}
			if (this.detailTemplate.Count == 0)
			{
				int num2 = Stat.Random(1, 2);
				for (int j = 0; j < num2; j++)
				{
					this.detailTemplate.Add(PopulationManager.RollOneFrom(tablePrefix + "SecondaryTemplate_*Default", null, "gardens").Blueprint);
				}
			}
			if (this.secondaryTemplate.Count == 0)
			{
				int num3 = Stat.Random(1, 2);
				for (int k = 0; k < num3; k++)
				{
					this.secondaryTemplate.Add(PopulationManager.RollOneFrom(tablePrefix + "SecondaryTemplate_*Default", null, "gardens").Blueprint);
				}
			}
			if (this.greenObjects.Count == 0)
			{
				this.greenObjects.Add(tablePrefix + "Green_*Default");
			}
			if (this.yellowObjects.Count == 0)
			{
				this.yellowObjects.Add(tablePrefix + "Yellow_*Default");
			}
			if (this.blueObjects.Count == 0)
			{
				this.blueObjects.Add(tablePrefix + "Blue_*Default");
			}
			if (this.preconnectencounters.Count == 0)
			{
				this.preconnectencounters.Add(tablePrefix + "PreconnectEncounters_*Default");
			}
			if (this.encounters.Count == 0)
			{
				this.encounters.Add(tablePrefix + "Encounters_*Default");
			}
			if (this.furnishings.Count == 0)
			{
				this.furnishings.Add(tablePrefix + "Furnishings_*Default");
			}
			if (this.cubbies.Count == 0)
			{
				this.cubbies.Add(tablePrefix + "Cubbies_*Default");
			}
			if (this.halls.Count == 0)
			{
				this.halls.Add(tablePrefix + "Halls_*Default");
			}
		}

		// Token: 0x06007065 RID: 28773 RVA: 0x0027FBD8 File Offset: 0x0027DDD8
		public void Mutate()
		{
			if (Stat.Random(1, 100) <= 25)
			{
				this.primaryTemplate.Clear();
				this.primaryTemplate.Add(PopulationManager.RollOneFrom(this.tablePrefix + "PrimaryTemplate_*Default", null, "smalltemple,W").Blueprint);
			}
			if (Stat.Random(1, 100) <= 25)
			{
				this.secondaryTemplate.Clear();
				this.secondaryTemplate.Add(PopulationManager.RollOneFrom(this.tablePrefix + "SecondaryTemplate_*Default", null, "gardens").Blueprint);
			}
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x0027FC6C File Offset: 0x0027DE6C
		public SultanDungeonArgs clone()
		{
			SultanDungeonArgs sultanDungeonArgs = new SultanDungeonArgs();
			sultanDungeonArgs.wallTypes.AddRange(this.wallTypes);
			sultanDungeonArgs.primaryTemplate.AddRange(this.primaryTemplate);
			sultanDungeonArgs.primaryTemplateN = this.primaryTemplateN;
			sultanDungeonArgs.secondaryTemplate.AddRange(this.secondaryTemplate);
			sultanDungeonArgs.secondaryTemplateN = this.secondaryTemplateN;
			sultanDungeonArgs.detailTemplate.AddRange(this.detailTemplate);
			sultanDungeonArgs.detailTemplateN = this.detailTemplateN;
			sultanDungeonArgs.greenObjects.AddRange(this.greenObjects);
			sultanDungeonArgs.yellowObjects.AddRange(this.yellowObjects);
			sultanDungeonArgs.blueObjects.AddRange(this.blueObjects);
			sultanDungeonArgs.segmentations.AddRange(this.segmentations);
			sultanDungeonArgs.furnishings.AddRange(this.furnishings);
			sultanDungeonArgs.encounters.AddRange(this.encounters);
			sultanDungeonArgs.preconnectencounters.AddRange(this.preconnectencounters);
			sultanDungeonArgs.halls.AddRange(this.halls);
			sultanDungeonArgs.cubbies.AddRange(this.cubbies);
			sultanDungeonArgs.cultPeriod = this.cultPeriod;
			sultanDungeonArgs.cultName = this.cultName;
			sultanDungeonArgs.cultFaction = this.cultFaction;
			sultanDungeonArgs.cultFactions.AddRange(this.cultFactions);
			sultanDungeonArgs.enemyFactions.AddRange(this.enemyFactions);
			foreach (string text in this.additionalArgs.Keys)
			{
				sultanDungeonArgs.additionalArgs.Add(text, new List<string>());
				sultanDungeonArgs.additionalArgs[text].AddRange(this.additionalArgs[text]);
			}
			return sultanDungeonArgs;
		}

		// Token: 0x040033C6 RID: 13254
		public static Dictionary<string, string> sultanArgumentClusters = new Dictionary<string, string>
		{
			{ "scholarship", "Scholarship" },
			{ "scholar", "Scholarship" },
			{ "academy", "Scholarship" },
			{ "scribe", "Scholarship" },
			{ "scriptorium", "Scholarship" },
			{ "philosopher", "Scholarship" },
			{ "college", "Scholarship" },
			{ "historian", "Scholarship" },
			{ "scientist", "Scholarship" },
			{ "laboratory", "Scholarship" },
			{ "tinker", "Circuitry" },
			{ "electrician", "Circuitry" },
			{ "circuitry", "Circuitry" },
			{ "travel", "Travel" },
			{ "explorer", "Travel" },
			{ "nomad", "Travel" },
			{ "might", "Warrior" },
			{ "soldier", "Warrior" },
			{ "barracks", "Warrior" },
			{ "arena", "Warrior" },
			{ "gladiator", "Warrior" },
			{ "stars", "Stars" },
			{ "astrologist", "Stars" },
			{ "observatory", "Stars" },
			{ "astronomer", "Stars" },
			{ "stargazer", "Stars" },
			{ "light", "Stars" },
			{ "glass", "Glass" },
			{ "window maker", "Glass" },
			{ "glassblower", "Glass" },
			{ "jewels", "Jewels" },
			{ "jeweler", "Jewels" },
			{ "geologist", "Jewels" },
			{ "dig site", "Jewels" },
			{ "time", "Time" },
			{ "salt", "Salt" },
			{ "cook", "Salt" },
			{ "tavern", "Salt" },
			{ "ice", "Ice" },
			{ "winter eremite", "Ice" },
			{ "hermitage", "Ice" },
			{ "chance", "Chance" },
			{ "gambler", "Chance" },
			{ "gambling hall", "Chance" },
			{ "gardens", "Plants" },
			{ "pools", "Liquids" },
			{ "museums", "Art" },
			{ "theaters", "Art" },
			{ "waste", "Waste" },
			{ "consulate", "Consulate" },
			{ "market", "Market" },
			{ "forums", "Forums" },
			{ "middle-class", "Residential" },
			{ "residential", "Residential" },
			{ "food storage", "Storage" },
			{ "pipe hub", "Pipes" },
			{ "prison", "Prison" },
			{ "temple", "Temple" },
			{ "city-state", "City-States" },
			{ "precinct", "Precincts" },
			{ "province", "Provinces" },
			{ "district", "Districts" },
			{ "districts", "Districts" },
			{ "quarter", "Quarters" },
			{ "quarters", "Quarters" },
			{ "poor", "Poor" },
			{ "rich", "Rich" },
			{ "godless", "Godless" },
			{ "sultans", "Sultans" }
		};

		// Token: 0x040033C7 RID: 13255
		public List<string> wallTypes = new List<string>();

		// Token: 0x040033C8 RID: 13256
		public List<string> primaryTemplate = new List<string>();

		// Token: 0x040033C9 RID: 13257
		public int primaryTemplateN = 3;

		// Token: 0x040033CA RID: 13258
		public List<string> secondaryTemplate = new List<string>();

		// Token: 0x040033CB RID: 13259
		public int secondaryTemplateN = 3;

		// Token: 0x040033CC RID: 13260
		public List<string> detailTemplate = new List<string>();

		// Token: 0x040033CD RID: 13261
		public int detailTemplateN = 3;

		// Token: 0x040033CE RID: 13262
		public List<string> greenObjects = new List<string>();

		// Token: 0x040033CF RID: 13263
		public List<string> yellowObjects = new List<string>();

		// Token: 0x040033D0 RID: 13264
		public List<string> blueObjects = new List<string>();

		// Token: 0x040033D1 RID: 13265
		public List<string> segmentations = new List<string>();

		// Token: 0x040033D2 RID: 13266
		public List<string> preconnectencounters = new List<string>();

		// Token: 0x040033D3 RID: 13267
		public List<string> furnishings = new List<string>();

		// Token: 0x040033D4 RID: 13268
		public List<string> encounters = new List<string>();

		// Token: 0x040033D5 RID: 13269
		public List<string> halls = new List<string>();

		// Token: 0x040033D6 RID: 13270
		public List<string> cubbies = new List<string>();

		// Token: 0x040033D7 RID: 13271
		public Dictionary<string, List<string>> additionalArgs = new Dictionary<string, List<string>>();

		// Token: 0x040033D8 RID: 13272
		public List<string> cultFactions = new List<string>();

		// Token: 0x040033D9 RID: 13273
		public List<string> enemyFactions = new List<string>();

		// Token: 0x040033DA RID: 13274
		public List<string> properties = new List<string>();

		// Token: 0x040033DB RID: 13275
		public string cultName;

		// Token: 0x040033DC RID: 13276
		public string cultPeriod;

		// Token: 0x040033DD RID: 13277
		public string cultFaction;

		// Token: 0x040033DE RID: 13278
		public string tablePrefix = "SultanDungeons_";
	}
}
