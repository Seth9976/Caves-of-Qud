using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using Qud.API;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Annals;
using XRL.Core;
using XRL.EditorFormats.Map;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.World.Biomes;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.ZoneBuilders;

namespace XRL.World.WorldBuilders
{
	// Token: 0x0200099C RID: 2460
	public class JoppaWorldBuilder : WorldBuilder
	{
		// Token: 0x06006A2C RID: 27180 RVA: 0x00218B00 File Offset: 0x00216D00
		public void BuildMazes()
		{
			Maze maze = RecursiveBacktrackerMaze.Generate(240, 75, false, The.Game.GetWorldSeed("CanyonMaze"));
			maze.SetBorder(true);
			The.Game.WorldMazes.Add("QudCanyonMaze", maze);
			Maze maze2 = RecursiveBacktrackerMaze.Generate(80, 25, false, The.Game.GetWorldSeed("WaterwayMaze"));
			maze2.Cell[25, 3].N = true;
			maze2.Cell[25, 3].S = true;
			maze2.Cell[25, 3].E = false;
			maze2.Cell[25, 3].W = false;
			maze2.Cell[24, 3].E = false;
			maze2.Cell[26, 3].W = false;
			maze2.Cell[25, 2].S = true;
			maze2.Cell[25, 4].N = true;
			The.Game.WorldMazes.Add("QudWaterwayMaze", maze2);
		}

		// Token: 0x06006A2D RID: 27181 RVA: 0x00218C18 File Offset: 0x00216E18
		public bool WorldCellHasTerrain(string World, int x, int y, string[] T)
		{
			this.WorldZone = The.ZoneManager.GetZone(World);
			GameObject firstObjectWithPart = this.WorldZone.GetCell(x, y).GetFirstObjectWithPart("TerrainTravel");
			if (firstObjectWithPart != null)
			{
				GameObjectBlueprint blueprint = firstObjectWithPart.GetBlueprint(true);
				for (int i = 0; i < T.Length; i++)
				{
					if (blueprint.DescendsFrom(T[i]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006A2E RID: 27182 RVA: 0x00218C78 File Offset: 0x00216E78
		public void BuildMutableEncounters()
		{
			try
			{
				History sultanHistory = The.Game.sultanHistory;
				MetricsManager.rngCheckpoint("init " + this.World);
				this.mutableMap = new MutabilityMap();
				this.mutableMap.Init(240, 75);
				this.terrainComponents = new Dictionary<Location2D, TerrainTravel>();
				this.terrainTypes = new Dictionary<Location2D, string>();
				this.WorldZone = The.ZoneManager.GetZone(this.World);
				this.extensions.ForEach(delegate(IJoppaWorldBuilderExtension e)
				{
					e.OnBeforeMutableInit(this);
				});
				for (int i = 0; i < 25; i++)
				{
					for (int j = 0; j < 80; j++)
					{
						Event.ResetPool(true);
						string text = string.Concat(new string[]
						{
							this.World,
							".",
							j.ToString(),
							".",
							i.ToString(),
							"."
						});
						List<CellBlueprint> cellBlueprints = The.ZoneManager.GetCellBlueprints(text);
						int num = 1;
						string text2 = "None";
						int num2 = 1;
						TerrainTravel terrainTravel = null;
						GameObject firstObjectWithPart = this.WorldZone.GetCell(j, i).GetFirstObjectWithPart("TerrainTravel");
						if (firstObjectWithPart != null)
						{
							terrainTravel = firstObjectWithPart.GetPart<TerrainTravel>();
							text2 = firstObjectWithPart.GetTag("Terrain", firstObjectWithPart.Blueprint);
							num2 = int.Parse(firstObjectWithPart.GetTag("RegionTier", "1"));
							if (terrainTravel == null)
							{
								MetricsManager.LogError(string.Format("Terrain object {0} is missing the TerrainTravel part in world map cell [{1},{2}]", firstObjectWithPart.Blueprint, j, i));
							}
						}
						else
						{
							MetricsManager.LogError(string.Format("Missing Terrain object in world map cell [{0},{1}]", j, i));
							num = 0;
						}
						using (List<CellBlueprint>.Enumerator enumerator = cellBlueprints.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (!enumerator.Current.Mutable)
								{
									num = 0;
								}
								else if (firstObjectWithPart != null && firstObjectWithPart.GetBlueprint(true).DescendsFrom("TerrainWater"))
								{
									num = 1;
								}
							}
						}
						int num3 = num2;
						this.terrainTypes.Add(Location2D.Get(j, i), text2);
						this.terrainComponents.Add(Location2D.Get(j, i), terrainTravel);
						if (!this.worldInfo.terrainLocations.ContainsKey(text2))
						{
							this.worldInfo.terrainLocations.Add(text2, new List<Location2D>());
						}
						this.worldInfo.terrainLocations[text2].Add(Location2D.Get(j, i));
						if (!this.worldInfo.tierLocations.ContainsKey(num3))
						{
							this.worldInfo.tierLocations.Add(num3, new List<Location2D>());
						}
						this.worldInfo.tierLocations[num3].Add(Location2D.Get(j, i));
						for (int k = 0; k < 3; k++)
						{
							for (int l = 0; l < 3; l++)
							{
								int num4 = j * 3 + k;
								int num5 = i * 3 + l;
								this.mutableMap.AddMutableLocation(Location2D.Get(num4, num5), text2, num);
								this.mutableMap.SetMutable(Location2D.Get(num4, num5), num);
							}
						}
					}
				}
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.4.1.0.10");
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.4.1.0.11");
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.4.0.0.11");
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.4.0.0.10");
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.3.1.2.10");
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.3.2.2.10");
				this.mutableMap.RemoveMutableLocation("JoppaWorld.53.3.2.0.10");
				this.extensions.ForEach(delegate(IJoppaWorldBuilderExtension e)
				{
					e.OnAfterMutableInit(this);
				});
				this.RiverSystem = new uint[240, 75];
				MetricsManager.rngCheckpoint("mamon");
				if (this.World == "JoppaWorld")
				{
					this.AddMamonVillage();
					this.AddYonderPath(this.World);
				}
				MetricsManager.rngCheckpoint("biomes");
				for (int m = 0; m < 80; m++)
				{
					if (m % 2 == 0)
					{
						WorldCreationProgress.StepProgress("Generating lairs...", false);
					}
					for (int n = 0; n < 25; n++)
					{
						string text3 = string.Concat(new string[]
						{
							this.World,
							".",
							m.ToString(),
							".",
							n.ToString(),
							"."
						});
						string text4 = this.terrainTypes[Location2D.Get(m, n)];
						TerrainTravel terrainTravel2 = this.terrainComponents[Location2D.Get(m, n)];
						for (int num6 = 0; num6 < 3; num6++)
						{
							for (int num7 = 0; num7 < 3; num7++)
							{
								string text5 = string.Concat(new string[]
								{
									text3,
									num6.ToString(),
									".",
									num7.ToString(),
									".10"
								});
								The.ZoneManager.AdjustZoneGenerationTierTo(text5);
								if (Options.ShowOverlandRegions)
								{
									if (BiomeManager.Biomes["Slimy"].GetBiomeValue(text5) > 0)
									{
										base.MarkCell("JoppaWorld", m, n, BiomeManager.Biomes["Slimy"].GetBiomeValue(text5).ToString());
									}
									if (BiomeManager.Biomes["Tarry"].GetBiomeValue(text5) > 0)
									{
										base.MarkCell("JoppaWorld", m, n, BiomeManager.Biomes["Tarry"].GetBiomeValue(text5).ToString());
									}
									if (BiomeManager.Biomes["Rusty"].GetBiomeValue(text5) > 0)
									{
										base.MarkCell("JoppaWorld", m, n, BiomeManager.Biomes["Rusty"].GetBiomeValue(text5).ToString());
									}
									if (BiomeManager.Biomes["Fungal"].GetBiomeValue(text5) > 0)
									{
										base.MarkCell("JoppaWorld", m, n, BiomeManager.Biomes["Fungal"].GetBiomeValue(text5).ToString());
									}
								}
								if (this.World == "JoppaWorld" && this.mutableMap.GetMutable(m * 3 + num6, n * 3 + num7) == 1)
								{
									if (3.in100())
									{
										if (5.in100())
										{
											The.ZoneManager.SetZoneProperty(text5, "ambient_bed_3", "Sounds/Ambiences/amb_bed_ruins");
											The.ZoneManager.AddZoneMidBuilder(text5, "OverlandRuins");
											The.ZoneManager.AddZoneMidBuilder(text5, "InsertPresetFromPopulation", "Population", "CyberStations", "Important", true);
											bool flag;
											string text7;
											string text6 = QudHistoryFactory.NameRuinsSite(sultanHistory, out flag, out text7);
											The.ZoneManager.SetZoneName(text5, text6, null, null, null, null, flag, true);
											string text8 = this.AddSecret(text5, text6, new string[] { "ruins", "tech", "cybernetics" }, "Ruins with Becoming Nooks", null, false, false);
											base.game.SetStringGameState(text8 + "_NameRoot", text7);
											The.ZoneManager.SetZoneProperty(text5, "TeleportGateCandidateNameRoot", text7);
											terrainTravel2.AddEncounter(new EncounterEntry("You notice some ruins nearby. Would you like to investigate?", text5, "", text8, true));
											this.AddLocationFinder(text5, text8, 0, 5000);
											if (Options.ShowOverlandEncounters)
											{
												The.ZoneManager.GetZone("JoppaWorld").GetCell(m, n).GetObjectInCell(0)
													.Render.RenderString = "#";
											}
											GeneratedLocationInfo generatedLocationInfo = new GeneratedLocationInfo();
											generatedLocationInfo.name = text6;
											generatedLocationInfo.targetZone = text5;
											generatedLocationInfo.zoneLocation = Location2D.Get(m * 3 + num6, n * 3 + num7);
											generatedLocationInfo.secretID = text8;
											this.worldInfo.ruins.Add(generatedLocationInfo);
											this.mutableMap.SetMutable(generatedLocationInfo.zoneLocation, 0);
										}
										else
										{
											The.ZoneManager.SetZoneProperty(text5, "ambient_bed_3", "Sounds/Ambiences/amb_bed_ruins");
											The.ZoneManager.AddZoneMidBuilder(text5, "OverlandRuins");
											bool flag2;
											string text10;
											string text9 = QudHistoryFactory.NameRuinsSite(sultanHistory, out flag2, out text10);
											The.ZoneManager.SetZoneName(text5, text9, null, null, null, null, flag2, true);
											string text11 = this.AddSecret(text5, text9, new string[] { "ruins", "tech" }, "Ruins", null, false, false);
											base.game.SetStringGameState(text11 + "_NameRoot", text10);
											The.ZoneManager.SetZoneProperty(text5, "TeleportGateCandidateNameRoot", text10);
											terrainTravel2.AddEncounter(new EncounterEntry("You notice some ruins nearby. Would you like to investigate?", text5, "", text11, true));
											this.AddLocationFinder(text5, text11, 0, 5000);
											if (Options.ShowOverlandEncounters)
											{
												The.ZoneManager.GetZone("JoppaWorld").GetCell(m, n).GetObjectInCell(0)
													.Render.RenderString = "#";
											}
											GeneratedLocationInfo generatedLocationInfo2 = new GeneratedLocationInfo();
											generatedLocationInfo2.name = text9;
											generatedLocationInfo2.targetZone = text5;
											generatedLocationInfo2.zoneLocation = Location2D.Get(m * 3 + num6, n * 3 + num7);
											generatedLocationInfo2.secretID = text11;
											this.worldInfo.ruins.Add(generatedLocationInfo2);
											this.mutableMap.SetMutable(generatedLocationInfo2.zoneLocation, 0);
										}
									}
									else
									{
										if (string.Equals(text4, "Jungle") && 3.in100())
										{
											The.ZoneManager.AddZoneBuilder(text5, 6000, "GoatfolkYurts");
											string text12 = SettlementNames.GenerateGoatfolkVillageName(sultanHistory) + ", goatfolk village";
											The.ZoneManager.SetZoneName(text5, text12, null, null, null, null, true, true);
											string text13 = this.AddSecret(text5, text12, new string[] { "settlement", "goatfolk", "humanoid" }, "Settlements", null, false, false);
											terrainTravel2.AddEncounter(new EncounterEntry("You smell roasted boar nearby. Would you like to investigate?", text5, "", text13, true));
											this.AddLocationFinder(text5, text13, 0, 5000);
											if (Options.ShowOverlandEncounters)
											{
												The.ZoneManager.GetZone("JoppaWorld").GetCell(m, n).GetObjectInCell(0)
													.Render.RenderString = "Y";
											}
											GeneratedLocationInfo generatedLocationInfo3 = new GeneratedLocationInfo();
											generatedLocationInfo3.name = text12;
											generatedLocationInfo3.targetZone = text5;
											generatedLocationInfo3.zoneLocation = Zone.zoneIDTo240x72Location(text5);
											generatedLocationInfo3.secretID = text13;
											this.worldInfo.enemySettlements.Add(generatedLocationInfo3);
											this.mutableMap.SetMutable(generatedLocationInfo3.zoneLocation, 0);
										}
										if (string.Equals(text4, "DeepJungle") && 15.in1000())
										{
											The.ZoneManager.AddZoneBuilder(text5, 6000, "GoatfolkQlippothYurts");
											string text14 = SettlementNames.GenerateGoatfolkQlippothVillageName(sultanHistory) + ", goatfolk haunt";
											The.ZoneManager.SetZoneName(text5, text14, null, null, null, null, true, true);
											string text15 = this.AddSecret(text5, text14, new string[] { "settlement", "goatfolk", "humanoid" }, "Settlements", null, false, false);
											terrainTravel2.AddEncounter(new EncounterEntry("You experience a sense memory of roasted boar smell. Would you like to investigate?", text5, "", text15, true));
											this.AddLocationFinder(text5, text15, 0, 5000);
											if (Options.ShowOverlandEncounters)
											{
												The.ZoneManager.GetZone("JoppaWorld").GetCell(m, n).GetObjectInCell(0)
													.Render.RenderString = "Y";
											}
											GeneratedLocationInfo generatedLocationInfo4 = new GeneratedLocationInfo();
											generatedLocationInfo4.name = text14;
											generatedLocationInfo4.targetZone = text5;
											generatedLocationInfo4.zoneLocation = Zone.zoneIDTo240x72Location(text5);
											generatedLocationInfo4.secretID = text15;
											this.worldInfo.enemySettlements.Add(generatedLocationInfo4);
											this.mutableMap.SetMutable(generatedLocationInfo4.zoneLocation, 0);
										}
									}
								}
							}
						}
					}
				}
				MetricsManager.rngCheckpoint("paths");
				this.BuildStep("Placing canyons", new Action<string>(this.BuildCanyonSystems));
				this.BuildStep("Placing rivers", new Action<string>(this.BuildRiverSystems));
				this.BuildStep("Placing roads", new Action<string>(this.BuildRoadSystems));
				MetricsManager.rngCheckpoint("forts");
				this.BuildStep("Placing forts", new Action<string>(this.BuildForts));
				MetricsManager.rngCheckpoint("farms");
				this.BuildStep("Placing farms", new Action<string>(this.BuildFarms));
				MetricsManager.rngCheckpoint("statics");
				this.BuildStep("Placing static encounters", new Action<string>(this.AddStaticEncounters));
				this.BuildStep("Placing Bey Lah", new Action<string>(this.AddHindrenVillage));
				this.BuildStep("Placing Hydropon", new Action<string>(this.AddHydropon));
				MetricsManager.rngCheckpoint("oboroqoru");
				if (this.World == "JoppaWorld")
				{
					this.BuildStep("Placing Oboroqoru's lair", new Action(this.AddOboroqorusLair));
				}
				MetricsManager.rngCheckpoint("waterway");
				this.BuildStep("Placing waterways", new Action(this.AddWaterway));
				MetricsManager.rngCheckpoint("sultans");
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Organizing cults...", false);
				}
				this.BuildStep("Creating sultan entries", new Action(JournalAPI.InitializeSultanEntries));
				this.BuildStep("Recording sultan aliases", new Action(this.RecordSultanAliases));
				this.BuildStep("Placing historic sites", new Action(this.AddSultanHistoryLocations));
				this.BuildStep("Renaming sultan tombs", new Action(this.RenameSultanTombs));
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Mythologizing creatures...", false);
				}
				MetricsManager.rngCheckpoint("lairs");
				this.BuildStep("Placing lairs", new Action<string>(this.BuildLairs));
				this.BuildStep("Placing lairs", new Action(this.AddNephilimLairs));
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Founding villages...", false);
				}
				MetricsManager.rngCheckpoint("villages");
				this.BuildStep("Placing villages", new Action(this.AddVillages));
				MetricsManager.rngCheckpoint("secrets");
				this.BuildStep("Placing secrets", new Action<string>(this.BuildSecrets));
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Seeding personalities...", false);
				}
				MetricsManager.rngCheckpoint("quests");
				this.BuildStep("Generating dynamic quests", new Action<string>(this.BuildDynamicQuests));
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Shucking clams...", false);
				}
				MetricsManager.rngCheckpoint("clams");
				this.BuildStep("Placing clams", new Action(this.PlaceClams));
				MetricsManager.rngCheckpoint("heirlooms");
				this.BuildStep("Requiring faction heirlooms", new Action(Factions.RequireCachedHeirlooms));
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Spreading gossip...", false);
				}
				MetricsManager.rngCheckpoint("gossip");
				this.BuildStep("Creating gossip", new Action(JournalAPI.InitializeGossip));
				this.BuildStep("Creating observations", new Action(JournalAPI.InitializeObservations));
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("BuildMutableEncounters", ex, "game_exception");
			}
		}

		// Token: 0x06006A2F RID: 27183 RVA: 0x00219BEC File Offset: 0x00217DEC
		public void BuildStep(string Context, Action<string> Action)
		{
			this.BuildStep(Context, delegate
			{
				Action(this.World);
			});
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x00219C20 File Offset: 0x00217E20
		public void BuildStep(string Context, Action Action)
		{
			MetricsManager.LogInfo(Context);
			try
			{
				Action();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException(Context, ex, "game_exception");
			}
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x00219C5C File Offset: 0x00217E5C
		public Location2D getLocationWithinNFromTerrainType(int min, int max, string terrainType)
		{
			List<Location2D> list = new List<Location2D>();
			foreach (Location2D location2D in this.worldInfo.terrainLocations[terrainType])
			{
				for (int i = location2D.X - max; i < location2D.X + max; i++)
				{
					for (int j = location2D.Y - max; j < location2D.Y + max; j++)
					{
						if (i > 0 && i < 80 && j > 0 && j < 25)
						{
							Location2D location2D2 = Location2D.Get(i, j);
							int num = location2D2.Distance(location2D);
							if (num >= min && num <= max && !list.Contains(location2D2) && this.mutableMap.GetMutable(i * 3 + 1, j * 3 + 1) > 0)
							{
								list.Add(Location2D.Get(i, j));
							}
						}
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			Location2D randomElement = list.GetRandomElement<Location2D>(null);
			this.mutableMap.SetMutable(randomElement, 0);
			return randomElement;
		}

		// Token: 0x06006A32 RID: 27186 RVA: 0x00219D8C File Offset: 0x00217F8C
		public Location2D getLocationWithinNFromTerrainTypeTier(int min, int max, string terrainType, int tier)
		{
			List<Location2D> list = new List<Location2D>();
			using (List<Location2D>.Enumerator enumerator = this.worldInfo.terrainLocations[terrainType].Shuffle<Location2D>(null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Location2D location2D = enumerator.Current;
					list.Clear();
					for (int i = location2D.X - max; i < location2D.X + max; i++)
					{
						for (int j = location2D.Y - max; j < location2D.Y + max; j++)
						{
							if (i > 0 && i < 80 && j > 0 && j < 25)
							{
								Location2D location2D2 = Location2D.Get(i, j);
								int num = location2D2.Distance(location2D);
								if (num >= min && num <= max && !list.Contains(location2D2) && this.mutableMap.GetMutable(i * 3 + 1, j * 3 + 1) > 0 && this.worldInfo.tierLocations[tier].Contains(Location2D.Get(i, j)))
								{
									list.Add(Location2D.Get(i, j));
								}
							}
						}
					}
					if (list.Count == 0)
					{
						return null;
					}
					Location2D randomElement = list.GetRandomElement<Location2D>(null);
					this.mutableMap.SetMutable(Location2D.Get(randomElement.X * 3 + 1, randomElement.Y * 3 + 1), 0);
					return randomElement;
				}
			}
			return null;
		}

		// Token: 0x06006A33 RID: 27187 RVA: 0x00219F20 File Offset: 0x00218120
		public Location2D getLocationWithinNFromTerrainBlueprintTier(int min, int max, string terrainBlueprint, int tier)
		{
			List<Location2D> list = new List<Location2D>();
			Location2D location = base.game.ZoneManager.GetZone("JoppaWorld").GetFirstObject((GameObject o) => o.Blueprint == terrainBlueprint).GetCurrentCell()
				.Location;
			list.Clear();
			for (int i = location.X - max; i < location.X + max; i++)
			{
				for (int j = location.Y - max; j < location.Y + max; j++)
				{
					if (i > 0 && i < 80 && j > 0 && j < 25)
					{
						Location2D location2D = Location2D.Get(i, j);
						int num = location2D.Distance(location);
						if (num >= min && num <= max && !list.Contains(location2D) && this.mutableMap.GetMutable(i * 3 + 1, j * 3 + 1) > 0 && this.worldInfo.tierLocations[tier].Contains(Location2D.Get(i, j)))
						{
							list.Add(Location2D.Get(i, j));
						}
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			Location2D randomElement = list.GetRandomElement<Location2D>(null);
			this.mutableMap.SetMutable(Location2D.Get(randomElement.X * 3 + 1, randomElement.Y * 3 + 1), 0);
			return randomElement;
		}

		// Token: 0x06006A34 RID: 27188 RVA: 0x0021A084 File Offset: 0x00218284
		public string GetZoneIdOfTerrain(string Terrain, string z = "10")
		{
			Location2D randomElement = this.worldInfo.terrainLocations[Terrain].GetRandomElement<Location2D>(null);
			return string.Concat(new string[]
			{
				"JoppaWorld.",
				randomElement.X.ToString(),
				".",
				randomElement.Y.ToString(),
				".",
				Stat.Random(0, 2).ToString(),
				".",
				Stat.Random(0, 2).ToString(),
				".",
				z
			});
		}

		// Token: 0x06006A35 RID: 27189 RVA: 0x0021A128 File Offset: 0x00218328
		public Location2D popMutableBlockOfTerrain(string Terrain)
		{
			List<Location2D> list = new List<Location2D>();
			using (List<Location2D>.Enumerator enumerator = this.worldInfo.terrainLocations[Terrain].Shuffle<Location2D>(null).GetEnumerator())
			{
				IL_00E6:
				while (enumerator.MoveNext())
				{
					Location2D location2D = enumerator.Current;
					list.Clear();
					bool flag = true;
					for (int i = 0; i <= 2; i++)
					{
						for (int j = 0; j <= 2; j++)
						{
							if (this.mutableMap.GetMutable(location2D.X * 3 + i, location2D.Y * 3 + j) <= 0)
							{
								goto IL_00E6;
							}
						}
					}
					if (flag)
					{
						for (int k = 0; k <= 2; k++)
						{
							for (int l = 0; l <= 2; l++)
							{
								this.mutableMap.RemoveMutableLocation(Location2D.Get(location2D.X * 3 + k, location2D.Y * 3 + l));
							}
						}
						return Location2D.Get(location2D.X * 3 + 1, location2D.Y * 3 + 1);
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Finds a radius x radius square of terrain and returns the center zone
		/// </summary>
		/// <param name="Terrain" />
		/// <param name="where" />
		/// <param name="radius" />
		/// <returns />
		// Token: 0x06006A36 RID: 27190 RVA: 0x0021A24C File Offset: 0x0021844C
		public Location2D popMutableLocationBlockOfTerrain(string Terrain, Predicate<Location2D> where = null, int radius = 1)
		{
			List<Location2D> list = new List<Location2D>();
			if (!this.worldInfo.terrainLocations.ContainsKey(Terrain))
			{
				Debug.LogError("Couldn't find terrain: " + Terrain);
			}
			using (List<Location2D>.Enumerator enumerator = this.worldInfo.terrainLocations[Terrain].Shuffle<Location2D>(null).GetEnumerator())
			{
				IL_00F6:
				while (enumerator.MoveNext())
				{
					Location2D location2D = enumerator.Current;
					list.Clear();
					for (int i = -radius; i <= radius; i++)
					{
						for (int j = -radius; j <= radius; j++)
						{
							if (this.mutableMap.GetMutable(location2D.X * 3 + i, location2D.Y * 3 + j) <= 0)
							{
								goto IL_00F6;
							}
							Location2D location2D2 = Location2D.Get(location2D.X * 3 + i, location2D.Y * 3 + j);
							if (where != null && !where(location2D2))
							{
								goto IL_00F6;
							}
							list.Add(location2D2);
						}
					}
					if (list.Count == 9)
					{
						Location2D location2D3 = list[4];
						list.ForEach(delegate(Location2D r)
						{
							this.mutableMap.RemoveMutableLocation(r);
						});
						return location2D3;
					}
				}
			}
			return null;
		}

		// Token: 0x06006A37 RID: 27191 RVA: 0x0021A380 File Offset: 0x00218580
		public IEnumerable<Location2D> YieldBlocksWithin(string Terrain, Box Box = null, bool Mutable = true)
		{
			List<Location2D> list;
			if (this.worldInfo.terrainLocations.TryGetValue(Terrain, out list))
			{
				list = list.Shuffle<Location2D>(null);
				foreach (Location2D location2D in list)
				{
					if ((Box == null || Box.contains(location2D)) && (!Mutable || this.mutableMap.GetWorldBlockMutable(location2D)))
					{
						yield return location2D;
					}
				}
				List<Location2D>.Enumerator enumerator = default(List<Location2D>.Enumerator);
			}
			else
			{
				Debug.LogError("Couldn't find terrain: " + Terrain);
			}
			yield break;
			yield break;
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x0021A3A8 File Offset: 0x002185A8
		public Location2D peekMutableLocation(Predicate<Location2D> where = null, bool centerOnly = true)
		{
			List<Location2D> list = new List<Location2D>();
			foreach (KeyValuePair<string, List<Location2D>> keyValuePair in this.worldInfo.terrainLocations.Where<KeyValuePair<string, List<Location2D>>>((KeyValuePair<string, List<Location2D>> l) => l.Value.Count > 0).ToList<KeyValuePair<string, List<Location2D>>>().Shuffle<KeyValuePair<string, List<Location2D>>>(null))
			{
				foreach (Location2D location2D in keyValuePair.Value.Shuffle<Location2D>(null))
				{
					list.Clear();
					for (int i = 0; i <= 2; i++)
					{
						for (int j = 0; j <= 2; j++)
						{
							if ((!centerOnly || (i == 1 && j == 1)) && this.mutableMap.GetMutable(location2D.X * 3 + i, location2D.Y * 3 + j) > 0)
							{
								Location2D location2D2 = Location2D.Get(location2D.X * 3 + i, location2D.Y * 3 + j);
								if (where == null || where(location2D2))
								{
									list.Add(location2D2);
								}
							}
						}
					}
					if (list.Count > 0)
					{
						return list.GetRandomElement<Location2D>(null);
					}
				}
			}
			return null;
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x0021A528 File Offset: 0x00218728
		public Location2D popMutableLocation(Predicate<Location2D> where = null, bool centerOnly = true)
		{
			List<Location2D> list = new List<Location2D>();
			foreach (KeyValuePair<string, List<Location2D>> keyValuePair in this.worldInfo.terrainLocations.Where<KeyValuePair<string, List<Location2D>>>((KeyValuePair<string, List<Location2D>> l) => l.Value.Count > 0).ToList<KeyValuePair<string, List<Location2D>>>().Shuffle<KeyValuePair<string, List<Location2D>>>(null))
			{
				foreach (Location2D location2D in keyValuePair.Value.Shuffle<Location2D>(null))
				{
					list.Clear();
					for (int i = 0; i <= 2; i++)
					{
						for (int j = 0; j <= 2; j++)
						{
							if ((!centerOnly || (i == 1 && j == 1)) && this.mutableMap.GetMutable(location2D.X * 3 + i, location2D.Y * 3 + j) > 0)
							{
								Location2D location2D2 = Location2D.Get(location2D.X * 3 + i, location2D.Y * 3 + j);
								if (where == null || where(location2D2))
								{
									list.Add(location2D2);
								}
							}
						}
					}
					if (list.Count > 0)
					{
						Location2D randomElement = list.GetRandomElement<Location2D>(null);
						this.mutableMap.RemoveMutableLocation(randomElement);
						return randomElement;
					}
				}
			}
			return null;
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x0021A6D0 File Offset: 0x002188D0
		public Location2D popMutableLocationOfTerrain(string Terrain, Predicate<Location2D> where = null, bool centerOnly = true)
		{
			List<Location2D> list = new List<Location2D>();
			if (!this.worldInfo.terrainLocations.ContainsKey(Terrain))
			{
				Debug.LogError("Couldn't find terrain: " + Terrain);
			}
			foreach (Location2D location2D in this.worldInfo.terrainLocations[Terrain].Shuffle<Location2D>(null))
			{
				list.Clear();
				for (int i = 0; i <= 2; i++)
				{
					for (int j = 0; j <= 2; j++)
					{
						if ((!centerOnly || (i == 1 && j == 1)) && this.mutableMap.GetMutable(location2D.X * 3 + i, location2D.Y * 3 + j) > 0)
						{
							Location2D location2D2 = Location2D.Get(location2D.X * 3 + i, location2D.Y * 3 + j);
							if (where == null || where(location2D2))
							{
								list.Add(location2D2);
							}
						}
					}
				}
				if (list.Count > 0)
				{
					Location2D randomElement = list.GetRandomElement<Location2D>(null);
					this.mutableMap.RemoveMutableLocation(randomElement);
					return randomElement;
				}
			}
			return null;
		}

		// Token: 0x06006A3B RID: 27195 RVA: 0x0021A808 File Offset: 0x00218A08
		public Location2D getLocationOfTier(int tier)
		{
			int num = 0;
			List<Location2D> list;
			while (!this.worldInfo.tierLocations.TryGetValue(tier, out list))
			{
				Debug.LogWarning("Couldn't find location of tier " + tier.ToString());
				tier--;
				if (tier < 1)
				{
					tier = 8;
				}
				num++;
				if (num > 9)
				{
					return null;
				}
			}
			foreach (Location2D location2D in list.Shuffle<Location2D>(null))
			{
				if (this.mutableMap.GetMutable(location2D.X * 3 + 1, location2D.Y * 3 + 1) > 0)
				{
					this.mutableMap.SetMutable(Location2D.Get(location2D.X * 3 + 1, location2D.Y * 3 + 1), 0);
					return location2D;
				}
			}
			return null;
		}

		// Token: 0x06006A3C RID: 27196 RVA: 0x0021A8EC File Offset: 0x00218AEC
		public Location2D getLocationOfTier(int minTier, int maxTier)
		{
			List<int> list = new List<int>();
			for (int i = minTier; i <= maxTier; i++)
			{
				list.Add(i);
			}
			list.ShuffleInPlace<int>(null);
			foreach (int num in list)
			{
				if (this.worldInfo.tierLocations.ContainsKey(num))
				{
					foreach (Location2D location2D in this.worldInfo.tierLocations[num].Shuffle<Location2D>(null))
					{
						if (this.mutableMap.GetMutable(location2D.X * 3 + 1, location2D.Y * 3 + 1) > 0)
						{
							this.mutableMap.SetMutable(Location2D.Get(location2D.X * 3 + 1, location2D.Y * 3 + 1), 0);
							return location2D;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06006A3D RID: 27197 RVA: 0x0021AA14 File Offset: 0x00218C14
		public void BuildDynamicQuests(string WorldID)
		{
			foreach (HistoricEntity historicEntity in The.Game.sultanHistory.GetEntitiesWherePropertyEquals("type", "village"))
			{
				if (historicEntity.GetCurrentSnapshot().hasProperty("zoneID"))
				{
					int num = 1;
					if (historicEntity.HasEntityProperty("isVillageZero", -1L))
					{
						num = 2;
					}
					for (int i = 0; i < num; i++)
					{
						VillageDynamicQuestContext villageDynamicQuestContext = new VillageDynamicQuestContext(historicEntity);
						villageDynamicQuestContext.questNumber = i;
						string text = "Dynamic Village Quests";
						try
						{
							DynamicQuestFactory.fabricateQuestTemplate(PopulationManager.RollOneFrom(text, null, null).Blueprint, villageDynamicQuestContext);
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("DynamicVillageQuestFab", ex, "game_exception");
						}
					}
				}
			}
		}

		// Token: 0x06006A3E RID: 27198 RVA: 0x0021AAF4 File Offset: 0x00218CF4
		public void AddVillages()
		{
			History sultanHistory = The.Game.sultanHistory;
			HistoricEntityList entitiesWherePropertyEquals = sultanHistory.GetEntitiesWherePropertyEquals("type", "village");
			bool flag = false;
			int num = 0;
			foreach (HistoricEntity historicEntity in entitiesWherePropertyEquals)
			{
				historicEntity.events.Find((HistoricEvent x) => x is InitializeVillage);
				HistoricEntitySnapshot currentSnapshot = historicEntity.GetCurrentSnapshot();
				string property = currentSnapshot.GetProperty("name", "unknown");
				string property2 = currentSnapshot.GetProperty("region", "unknown");
				bool flag2 = false;
				if (currentSnapshot.GetProperty("villageZero", "false") == "true")
				{
					if (flag)
					{
						sultanHistory.entities.Remove(historicEntity);
						continue;
					}
					if (The.Game.GetStringGameState("VillageZeroRegion", "&YJoppa") == property2)
					{
						flag2 = true;
						flag = true;
					}
					if (!flag2)
					{
						sultanHistory.entities.Remove(historicEntity);
						continue;
					}
				}
				int num2;
				int num3;
				if (flag2)
				{
					num2 = 1;
					num3 = 1;
				}
				else
				{
					try
					{
						num2 = currentSnapshot.Tier;
					}
					catch
					{
						num2 = Tier.Constrain(num);
					}
					try
					{
						num3 = currentSnapshot.TechTier;
					}
					catch
					{
						num3 = Tier.Constrain(num);
					}
				}
				Location2D location2D = null;
				if (flag2)
				{
					Box box = new Box(0, 15, 26, 25);
					location2D = this.YieldBlocksWithin(property2, box, true).FirstOrDefault<Location2D>();
				}
				if (location2D == null)
				{
					location2D = this.YieldBlocksWithin(property2, null, true).FirstOrDefault<Location2D>();
				}
				if (location2D == null)
				{
					MetricsManager.LogError(string.Format("Unable to find map cell for village in {0} (V0: {1})", property2, flag2));
					sultanHistory.entities.Remove(historicEntity);
				}
				else
				{
					this.mutableMap.SetWorldBlockMutable(location2D, 0);
					Location2D location2D2 = Location2D.Get(location2D.X * 3 + 1, location2D.Y * 3 + 1);
					GameObject firstObjectWithPart = this.WorldZone.GetCell(location2D.X, location2D.Y).GetFirstObjectWithPart("TerrainTravel");
					VillageTerrain villageTerrain = new VillageTerrain(historicEntity);
					firstObjectWithPart.AddPart<VillageTerrain>(villageTerrain, true, false);
					string text = string.Concat(new string[]
					{
						"JoppaWorld.",
						location2D.X.ToString(),
						".",
						location2D.Y.ToString(),
						".1.1.10"
					});
					The.ZoneManager.AdjustZoneGenerationTierTo(text);
					if (flag2)
					{
						base.game.SetStringGameState("villageZeroStartingLocation", text + "@37,22");
						historicEntity.SetEntityPropertyAtCurrentYear("isVillageZero", "true");
					}
					historicEntity.SetEntityPropertyAtCurrentYear("zoneID", text);
					currentSnapshot.setProperty("zoneID", text);
					if (currentSnapshot.hasProperty("worships_creature"))
					{
						Location2D villageLocation2 = Zone.zoneIDTo240x72Location(text);
						GeneratedLocationInfo generatedLocationInfo = this.worldInfo.lairs.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => l != null && villageLocation2.ManhattanDistance(l.zoneLocation) >= 0 && villageLocation2.ManhattanDistance(l.zoneLocation) <= 18).GetRandomElement<GeneratedLocationInfo>(null);
						if (generatedLocationInfo == null)
						{
							generatedLocationInfo = this.worldInfo.lairs.GetRandomElement<GeneratedLocationInfo>(null);
						}
						GameObject cachedObjects = The.ZoneManager.GetCachedObjects(generatedLocationInfo.ownerID);
						Worships.PostProcessEvent(historicEntity, cachedObjects.GetReferenceDisplayName(int.MaxValue, null, "Worship", false, true, false, false, false, false, false, false, null, false), cachedObjects.ID);
					}
					if (currentSnapshot.hasProperty("despises_creature"))
					{
						Location2D villageLocation = Zone.zoneIDTo240x72Location(text);
						GeneratedLocationInfo generatedLocationInfo2 = this.worldInfo.lairs.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => l != null && villageLocation.ManhattanDistance(l.zoneLocation) >= 0 && villageLocation.ManhattanDistance(l.zoneLocation) <= 18).GetRandomElement<GeneratedLocationInfo>(null);
						if (generatedLocationInfo2 == null)
						{
							generatedLocationInfo2 = this.worldInfo.lairs.GetRandomElement<GeneratedLocationInfo>(null);
						}
						GameObject cachedObjects2 = The.ZoneManager.GetCachedObjects(generatedLocationInfo2.ownerID);
						Despises.PostProcessEvent(historicEntity, cachedObjects2.GetReferenceDisplayName(int.MaxValue, null, "Despise", false, true, false, false, false, false, false, false, null, false), cachedObjects2.ID);
					}
					TerrainTravel part = this.WorldZone.GetCell(location2D.X, location2D.Y).GetFirstObjectWithPart("TerrainTravel").GetPart<TerrainTravel>();
					Faction faction = VillageBase.CreateVillageFaction(currentSnapshot);
					string text2 = this.AddSecret(text, property, new string[] { "settlement", "villages", "humanoid" }, "Settlements", historicEntity.id, flag2, flag2);
					JournalAPI.GetMapNote(text2).Tradable = !flag2;
					JournalAPI.GetMapNote(text2).Attributes.Add("nobuy:" + faction.Name);
					JournalAPI.GetMapNote(text2).Attributes.Add("nosell:" + faction.Name);
					JournalMapNote mapNote = JournalAPI.GetMapNote(text2);
					mapNote.History = mapNote.History + " {{K|-learned from " + Faction.GetFormattedName(faction.Name) + "}}";
					JournalAPI.GetMapNote(text2).LearnedFrom = Faction.GetFormattedName(faction.Name);
					if (currentSnapshot.GetProperty("abandoned", "unknown") == "true")
					{
						part.AddEncounter(new EncounterEntry("You discover an abandoned village. Would you like to investigate?", text, "", text2, true));
					}
					else
					{
						part.AddEncounter(new EncounterEntry("You discover a village. Would you like to investigate?", text, "", text2, true));
					}
					villageTerrain.secretId = text2;
					The.ZoneManager.AddZoneBuilder(text, 6000, "Village", "VillageEntityID", historicEntity.id, "villageTier", num2, "villageTechTier", num3, "villageFaction", faction.Name);
					The.ZoneManager.SetZoneName(text, property, null, null, null, null, true, true);
					The.ZoneManager.SetZoneIncludeStratumInZoneDisplay(text, new bool?(false));
					foreach (string text3 in Directions.DirectionList)
					{
						Location2D location2D3 = location2D2.FromDirection(text3);
						string text4 = Zone.XYToID("JoppaWorld", location2D3.X, location2D3.Y, 10);
						The.ZoneManager.AdjustZoneGenerationTierTo(text4);
						The.ZoneManager.AddZoneBuilder(text4, 6000, "VillageOutskirts", "VillageEntityID", historicEntity.id, "villageTier", num2, "villageTechTier", num3, "villageFaction", faction.Name);
						The.ZoneManager.SetZoneProperty(text4, "NoBiomes", "Yes");
						The.ZoneManager.SetZoneName(text4, "outskirts", property, "some", null, null, false, true);
						The.ZoneManager.SetZoneIncludeStratumInZoneDisplay(text4, new bool?(false));
						string text5 = Zone.XYToID("JoppaWorld", location2D3.X, location2D3.Y, 9);
						The.ZoneManager.AdjustZoneGenerationTierTo(text5);
						The.ZoneManager.AddZoneBuilder(text5, 6000, "VillageOver", "VillageEntityID", historicEntity.id, "villageTier", num2, "villageTechTier", num3, "villageFaction", faction.Name);
						The.ZoneManager.SetZoneName(text5, "sky", property, null, null, "the", false, true);
						string text6 = Zone.XYToID("JoppaWorld", location2D3.X, location2D3.Y, 11);
						The.ZoneManager.AddZoneBuilder(text6, 6000, "VillageUnder", "VillageEntityID", historicEntity.id, "villageTier", num2, "villageTechTier", num3, "villageFaction", faction.Name);
					}
					for (int j = 9; j >= 0; j--)
					{
						string text7 = string.Concat(new string[]
						{
							"JoppaWorld.",
							location2D.X.ToString(),
							".",
							location2D.Y.ToString(),
							".1.1.",
							j.ToString()
						});
						The.ZoneManager.AdjustZoneGenerationTierTo(text7);
						The.ZoneManager.AddZoneBuilder(text7, 6000, "VillageOver", "VillageEntityID", historicEntity.id, "villageTier", num2, "villageTechTier", num3, "villageFaction", faction.Name);
						The.ZoneManager.SetZoneName(text7, "sky", property, null, null, "the", false, true);
					}
					string text8 = string.Concat(new string[]
					{
						"JoppaWorld.",
						location2D.X.ToString(),
						".",
						location2D.Y.ToString(),
						".1.1.11"
					});
					The.ZoneManager.AddZoneBuilder(text8, 6000, "VillageUnder", "VillageEntityID", historicEntity.id, "villageTier", num2, "villageTechTier", num3, "villageFaction", faction.Name);
					The.ZoneManager.SetZoneName(text8, "undervillage", property, null, null, "the", false, true);
					GeneratedLocationInfo generatedLocationInfo3 = new GeneratedLocationInfo();
					generatedLocationInfo3.name = property;
					generatedLocationInfo3.targetZone = text;
					generatedLocationInfo3.zoneLocation = location2D2;
					generatedLocationInfo3.secretID = text2;
					this.worldInfo.villages.Add(generatedLocationInfo3);
					this.mutableMap.SetMutable(generatedLocationInfo3.zoneLocation, 0);
					num++;
				}
			}
			JournalAPI.InitializeVillageEntries();
		}

		// Token: 0x06006A3F RID: 27199 RVA: 0x0021B4D8 File Offset: 0x002196D8
		public void AddSultanHistoryLocations()
		{
			History sultanHistory = The.Game.sultanHistory;
			HistoricEntityList entitiesWherePropertyEquals = sultanHistory.GetEntitiesWherePropertyEquals("type", "region");
			HistoricEntityList entitiesWherePropertyEquals2 = sultanHistory.GetEntitiesWherePropertyEquals("type", "sultan");
			for (int i = 0; i < 8; i++)
			{
				int num = i + 1;
				int num2 = num;
				bool flag = false;
				Location2D location2D;
				if (i == 0)
				{
					location2D = this.getLocationWithinNFromTerrainBlueprintTier(3, 8, "TerrainJoppa", num2);
				}
				else if (i < 2)
				{
					location2D = this.getLocationWithinNFromTerrainBlueprintTier(1, 29, "TerrainJoppa", num2);
				}
				else
				{
					location2D = this.getLocationOfTier(num2);
				}
				int num3 = 0;
				if (i == 0)
				{
					num3 = 5;
				}
				if (i == 1)
				{
					num3 = 5;
				}
				if (i == 2)
				{
					num3 = 4;
				}
				if (i == 3)
				{
					num3 = 4;
				}
				if (i == 4)
				{
					num3 = 3;
				}
				if (i == 5)
				{
					num3 = 3;
				}
				if (i == 6)
				{
					num3 = 2;
				}
				if (i == 7)
				{
					num3 = 1;
				}
				HistoricEntity historicEntity = null;
				HistoricEntitySnapshot historicEntitySnapshot = null;
				for (int j = 0; j < entitiesWherePropertyEquals.entities.Count; j++)
				{
					HistoricEntitySnapshot currentSnapshot = entitiesWherePropertyEquals.entities[j].GetCurrentSnapshot();
					if (Convert.ToInt32(currentSnapshot.GetProperty("period", "-1")) == num3 && currentSnapshot.hasListProperty("items"))
					{
						historicEntity = entitiesWherePropertyEquals.entities[j];
						historicEntitySnapshot = currentSnapshot;
						entitiesWherePropertyEquals.entities.RemoveAt(j);
						break;
					}
				}
				if (historicEntity == null)
				{
					for (int k = 0; k < entitiesWherePropertyEquals.entities.Count; k++)
					{
						HistoricEntitySnapshot regionSnap = entitiesWherePropertyEquals.entities[k].GetCurrentSnapshot();
						if (Convert.ToInt32(regionSnap.GetProperty("period", "-1")) == num3)
						{
							bool flag2 = false;
							HistoricEntity.eventMatchingDelegate <>9__0;
							for (int l = 0; l < entitiesWherePropertyEquals2.entities.Count; l++)
							{
								HistoricEntity historicEntity2 = entitiesWherePropertyEquals2.entities[l];
								HistoricEntity.eventMatchingDelegate eventMatchingDelegate;
								if ((eventMatchingDelegate = <>9__0) == null)
								{
									eventMatchingDelegate = (<>9__0 = (HistoricEvent ev) => ev.HasEventProperty("revealsRegion") && ev.GetEventProperty("revealsRegion", null) != null && ev.GetEventProperty("revealsRegion", null) == regionSnap.GetProperty("newName", "unknown"));
								}
								if (historicEntity2.GetRandomEventWhereDelegate(eventMatchingDelegate, Stat.Rnd) != null)
								{
									flag2 = true;
									break;
								}
							}
							if (flag2)
							{
								historicEntity = entitiesWherePropertyEquals.entities[k];
								historicEntitySnapshot = regionSnap;
								entitiesWherePropertyEquals.entities.RemoveAt(k);
								break;
							}
						}
					}
				}
				if (historicEntity == null)
				{
					int num4 = Stat.Random(0, entitiesWherePropertyEquals.entities.Count - 1);
					historicEntity = entitiesWherePropertyEquals.entities[num4];
					historicEntitySnapshot = historicEntity.GetCurrentSnapshot();
					entitiesWherePropertyEquals.entities.RemoveAt(num4);
				}
				int num5 = 1;
				if (num == 0)
				{
					num5 += 4;
				}
				else if (num == 1)
				{
					num5 += 4;
				}
				else if (num == 2)
				{
					num5 += Stat.Random(4, 5);
				}
				else if (num == 3)
				{
					num5 += Stat.Random(4, 5);
				}
				else if (num == 4)
				{
					num5 += Stat.Random(4, 5);
				}
				else if (num == 5)
				{
					num5 += Stat.Random(4, 5);
				}
				else if (num == 6)
				{
					num5 += Stat.Random(4, 6);
				}
				else if (num == 7)
				{
					num5 += Stat.Random(5, 6);
				}
				else if (num == 8)
				{
					num5 += Stat.Random(5, 7);
				}
				string property = historicEntitySnapshot.GetProperty("newName", "unknown");
				The.Game.SetStringGameState("SultanDungeonPlacementOrder_" + i.ToString(), property);
				The.Game.SetStringGameState("SultanDungeonPlaced_" + property, i.ToString());
				The.Game.SetObjectGameState("sultanRegionPosition_" + property, location2D.Vector2i);
				The.Game.SetObjectGameState("sultanRegionPosition_" + historicEntitySnapshot.GetProperty("name", "unknown"), location2D.Vector2i);
				SultanRegion sultanRegion = this.WorldZone.GetCell(location2D.X, location2D.Y).GetFirstObjectWithPart("TerrainTravel").AddPart<SultanRegion>(new SultanRegion(historicEntity), true, false);
				GameObject gameObject = GameObjectFactory.Factory.CreateObject("SultanRegionSurface");
				SultanRegionSurface part = gameObject.GetPart<SultanRegionSurface>();
				part.RegionName = property;
				part.RevealKey = "sultanRegionReveal_" + property;
				part.RevealLocation = new Vector2i(location2D.X, location2D.Y);
				part.RevealString = "You discover " + property + ".";
				string text = string.Concat(new string[]
				{
					"JoppaWorld.",
					location2D.X.ToString(),
					".",
					location2D.Y.ToString(),
					".1.1.10"
				});
				The.ZoneManager.AdjustZoneGenerationTierTo(text);
				TerrainTravel part2 = this.WorldZone.GetCell(location2D.X, location2D.Y).GetFirstObjectWithPart("TerrainTravel").GetPart<TerrainTravel>();
				string text2 = this.AddSecret(text, property, new string[] { "historic", "tech", "ruins" }, "Historic Sites", null, false, false);
				string property2 = historicEntitySnapshot.GetProperty("nameRoot", "unknown");
				base.game.SetStringGameState(text2 + "_NameRoot", property2);
				The.ZoneManager.SetZoneProperty(text, "TeleportGateCandidateNameRoot", property2);
				if (i == 0)
				{
					JournalAPI.GetMapNote(text2).Attributes.Add("nobuy:Joppa");
					JournalMapNote mapNote = JournalAPI.GetMapNote(text2);
					mapNote.History = mapNote.History + " {{K|-known by " + Faction.GetFormattedName("Joppa") + "}}";
				}
				part2.AddEncounter(new EncounterEntry("You discover some historic ruins. Would you like to investigate?", text, "", text2, true));
				part.RevealSecret = text2;
				sultanRegion.secretId = text2;
				HistoricEntitySnapshot currentSnapshot2 = sultanHistory.GetEntitiesWherePropertyEquals("newName", property).entities[0].GetCurrentSnapshot();
				List<string> locationsInRegion = QudHistoryHelpers.GetLocationsInRegion(sultanHistory, currentSnapshot2.GetProperty("name", "unknown"));
				if (num5 < locationsInRegion.Count)
				{
					num5 = locationsInRegion.Count;
				}
				SultanDungeonArgs sultanDungeonArgs = new SultanDungeonArgs();
				if (num3 > 0)
				{
					HistoricEntity historicEntity3 = entitiesWherePropertyEquals2.GetEntitiesWherePropertyEquals("period", num3.ToString()).GetRandomElement(null);
					if (historicEntity3 == null)
					{
						historicEntity3 = entitiesWherePropertyEquals2.GetRandomElement(null);
					}
					if (historicEntity3 != null)
					{
						sultanDungeonArgs.UpdateFromEntity(historicEntity3.GetCurrentSnapshot(), "SultanDungeons_");
					}
				}
				sultanDungeonArgs.UpdateWalls(num3, "SultanDungeons_");
				sultanDungeonArgs.UpdateFromEntity(currentSnapshot2, "SultanDungeons_");
				if (50.in100())
				{
					sultanDungeonArgs.wallTypes.Add("*SultanWall*");
				}
				Faction faction = Factions.Get("SultanCult" + sultanDungeonArgs.cultPeriod);
				The.Game.SetObjectGameState("sultanDungeonArgs_" + property, sultanDungeonArgs);
				List<string> list = new List<string>();
				for (int m = 0; m < locationsInRegion.Count; m++)
				{
					list.Add(locationsInRegion[m]);
				}
				for (int n = 0; n < num5 - locationsInRegion.Count; n++)
				{
					list.Add(null);
				}
				Algorithms.RandomShuffle<string>(list);
				if (list[num5 - 1] == null)
				{
					for (int num6 = 0; num6 < num5; num6++)
					{
						if (list[num6] != null)
						{
							list[num5 - 1] = list[num6];
							list[num6] = null;
							break;
						}
					}
				}
				for (int num7 = 0; num7 < num5; num7++)
				{
					string text3 = list[num7];
					string text4 = string.Concat(new string[]
					{
						"JoppaWorld.",
						location2D.X.ToString(),
						".",
						location2D.Y.ToString(),
						".1.1.",
						(num7 + 10).ToString()
					});
					The.ZoneManager.AdjustZoneGenerationTierTo(text4);
					if (num7 > 0)
					{
						faction.HolyPlaces.Add(text4);
					}
					bool flag3;
					if (text3 == null)
					{
						flag3 = false;
						string text5 = Grammar.MakeTitleCase(property);
						if (num7 == 0)
						{
							The.ZoneManager.SetZoneName(text4, text5, null, null, null, null, true, true);
						}
						else
						{
							The.ZoneManager.SetZoneName(text4, "liminal floor", text5, null, null, null, false, true);
						}
						if (locationsInRegion.Count > 0)
						{
							text3 = locationsInRegion.GetRandomElement<string>(null);
						}
						else
						{
							text3 = sultanHistory.GetEntitiesWherePropertyEquals("type", "location").entities.GetRandomElement<HistoricEntity>(null).GetCurrentSnapshot().GetProperty("name", "unknown");
						}
					}
					else
					{
						flag3 = true;
						string text6 = Grammar.MakeTitleCase(property);
						The.ZoneManager.SetZoneName(text4, Grammar.MakeTitleCase(text3), text6, null, null, null, true, true);
					}
					The.ZoneManager.SetZoneProperty(text4, "HistoricSite", property);
					The.ZoneManager.SetZoneProperty(text4, "TeleportGateCandidateNameRoot", property2);
					HistoricEntitySnapshot currentSnapshot3 = sultanHistory.GetEntitiesWherePropertyEquals("name", text3).entities[0].GetCurrentSnapshot();
					string text7 = "";
					if (num7 < num5 - 1)
					{
						text7 += "D";
					}
					if (num7 > 0)
					{
						text7 += "U";
					}
					if (num7 != 0)
					{
						The.ZoneManager.ClearZoneBuilders(text4);
					}
					if (num7 != 0)
					{
						The.ZoneManager.SetZoneProperty(text4, "SkipTerrainBuilders", true);
					}
					The.ZoneManager.AddZoneBuilder(text4, 6000, "SultanDungeon", "locationName", text3, "regionName", property, "stairs", text7);
					The.ZoneManager.AddZoneBuilder(text4, 6000, "AddWidgetBuilder", "Object", The.ZoneManager.CacheObject(gameObject, true, false));
					The.ZoneManager.AddZoneBuilder(text4, 6000, "Music", "Track", "Music/of Chrome and How");
					The.ZoneManager.SetZoneProperty(text4, "ZoneTierOverride", num.ToString());
					if (flag3)
					{
						if (currentSnapshot3.listProperties.ContainsKey("items"))
						{
							using (List<string>.Enumerator enumerator = currentSnapshot3.listProperties["items"].GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									string text8 = enumerator.Current;
									HistoricEntityList entitiesWherePropertyEquals3 = sultanHistory.GetEntitiesWherePropertyEquals("name", text8);
									if (entitiesWherePropertyEquals3.Count > 0)
									{
										GameObject gameObject2 = RelicGenerator.GenerateRelic(entitiesWherePropertyEquals3.entities[0].GetCurrentSnapshot(), num2, null, false);
										gameObject2.AddPart<TakenAchievement>(new TakenAchievement
										{
											AchievementID = "ACH_RECOVER_RELIC"
										}, true, false);
										The.ZoneManager.SetZoneProperty(text4, "Relicstyle", "Vault");
										if (num7 == num5 - 1)
										{
											The.ZoneManager.AddZoneBuilder(text4, 6000, "PlaceRelicBuilder", "Relic", The.ZoneManager.CacheObject(gameObject2, false, false));
											flag = true;
										}
										else
										{
											The.ZoneManager.AddZoneBuilder(text4, 6000, "PlaceRelicBuilder", "Relic", The.ZoneManager.CacheObject(gameObject2, false, false), "AddCreditWedges", false);
										}
									}
									else
									{
										Debug.LogError("Unknown relic: " + text8);
									}
								}
								goto IL_0B56;
							}
						}
						if (3.in1000() && num7 != num5 - 1)
						{
							GameObject gameObject3 = RelicGenerator.GenerateRelic(currentSnapshot3, num2, null, false);
							gameObject3.AddPart<TakenAchievement>(new TakenAchievement
							{
								AchievementID = "ACH_RECOVER_RELIC"
							}, true, false);
							The.ZoneManager.AddZoneBuilder(text4, 6000, "PlaceRelicBuilder", "Relic", The.ZoneManager.CacheObject(gameObject3, false, false), "AddCreditWedges", false);
						}
					}
					IL_0B56:;
				}
				if (!flag)
				{
					string text9 = string.Concat(new string[]
					{
						"JoppaWorld.",
						location2D.X.ToString(),
						".",
						location2D.Y.ToString(),
						".1.1.",
						(10 + num5 - 1).ToString()
					});
					The.ZoneManager.AdjustZoneGenerationTierTo(text9);
					GameObject gameObject4 = RelicGenerator.GenerateRelic(currentSnapshot2, num2, null, true);
					gameObject4.AddPart<TakenAchievement>(new TakenAchievement
					{
						AchievementID = "ACH_RECOVER_RELIC"
					}, true, false);
					The.ZoneManager.SetZoneProperty(text9, "Relicstyle", "Vault");
					The.ZoneManager.AddZoneBuilder(text9, 6000, "PlaceRelicBuilder", "Relic", The.ZoneManager.CacheObject(gameObject4, false, false));
				}
				this.mutableMap.SetMutable(Location2D.Get(location2D.X * 3 + 1, location2D.Y * 3 + 1), 0);
				for (int num8 = 0; num8 < entitiesWherePropertyEquals.entities.Count; num8++)
				{
					HistoricEntitySnapshot currentSnapshot4 = entitiesWherePropertyEquals.entities[num8].GetCurrentSnapshot();
					if (Convert.ToInt32(currentSnapshot4.GetProperty("period", "-1")) == num3 && i != 0 && currentSnapshot4.hasListProperty("items"))
					{
						i--;
						break;
					}
				}
			}
			for (int num9 = 1; num9 <= 6; num9++)
			{
				HistoricEntity periodSultan = QudHistoryHelpers.GetPeriodSultan(sultanHistory, num9);
				string sultanFactionName = Faction.GetSultanFactionName(num9);
				Faction faction2 = Factions.Get(sultanFactionName);
				faction2.Emblem = new FactionEmblem();
				faction2.Emblem = new FactionEmblem();
				faction2.Emblem.ColorString = "&y";
				string property3 = periodSultan.GetCurrentSnapshot().GetProperty("name", "<unknown>");
				faction2.Emblem.Tile = SultanShrine.GetStatueForSultan(property3, num9);
				faction2.Emblem.RenderString = "8";
				faction2.Emblem.TileColor = "&y";
				faction2.Emblem.DetailColor = 'g';
				foreach (string text10 in HistoryAPI.GetSultanLikedFactions(periodSultan))
				{
					faction2.ApplyWorshipAttitude(text10, 50);
					Factions.Get(text10).ApplyWorshipAttitude(sultanFactionName, 50);
				}
				foreach (string text11 in HistoryAPI.GetSultanHatedFactions(periodSultan))
				{
					faction2.ApplyWorshipAttitude(text11, -100);
					Factions.Get(text11).ApplyWorshipAttitude(sultanFactionName, -100);
				}
			}
		}

		// Token: 0x06006A40 RID: 27200 RVA: 0x0021C350 File Offset: 0x0021A550
		public void RecordSultanAliases()
		{
			foreach (HistoricEntity historicEntity in The.Game.sultanHistory.GetEntitiesWherePropertyEquals("type", "sultan"))
			{
				string property = historicEntity.GetCurrentSnapshot().GetProperty("period", "unknown");
				base.game.SetStringGameState("*Sultan" + property + "Name*", historicEntity.GetCurrentSnapshot().GetProperty("name", "unknown"));
			}
		}

		// Token: 0x06006A41 RID: 27201 RVA: 0x0021C3F0 File Offset: 0x0021A5F0
		public void RenameSultanTombs()
		{
			int i = 5;
			int num = 1;
			while (i >= 1)
			{
				for (int j = 0; j <= 2; j++)
				{
					for (int k = 0; k <= 2; k++)
					{
						string text = ZoneID.Assemble("JoppaWorld", 53, 3, j, k, i);
						The.ZoneManager.AdjustZoneGenerationTierTo(text);
						The.ZoneManager.SetZoneBaseDisplayName(text, The.ZoneManager.GetZoneBaseDisplayName(text, true).Replace("*Sultan" + num.ToString() + "Name*", base.game.GetStringGameState("*Sultan" + num.ToString() + "Name*", "")), true);
					}
				}
				i--;
				num++;
			}
		}

		// Token: 0x06006A42 RID: 27202 RVA: 0x0021C4B0 File Offset: 0x0021A6B0
		public void AddMutableEncounterToTerrainRect(string Terrain, int n, Action<string, Location2D, TerrainTravel> encounter, bool unflagAsMutable = true)
		{
			for (int i = 0; i < n; i++)
			{
				Location2D mutableLocationWithTerrain = this.mutableMap.GetMutableLocationWithTerrain(Terrain);
				if (mutableLocationWithTerrain != null)
				{
					int x = mutableLocationWithTerrain.X;
					int y = mutableLocationWithTerrain.Y;
					int num = mutableLocationWithTerrain.X / 3;
					int num2 = mutableLocationWithTerrain.Y / 3;
					string text = Zone.XYToID("JoppaWorld", x, y, 10);
					ZoneManager zoneManager = The.ZoneManager;
					if (zoneManager != null)
					{
						zoneManager.AdjustZoneGenerationTierTo(text);
					}
					encounter(text, mutableLocationWithTerrain, this.terrainComponents[Location2D.Get(num, num2)]);
					if (unflagAsMutable)
					{
						this.mutableMap.RemoveMutableLocation(mutableLocationWithTerrain);
					}
				}
			}
		}

		// Token: 0x06006A43 RID: 27203 RVA: 0x0021C558 File Offset: 0x0021A758
		public void AddMutableEncounterToTerrain(string Terrain, int n, Action<string, Location2D, TerrainTravel> encounter, bool unflagAsMutable = true)
		{
			for (int i = 0; i < n; i++)
			{
				Location2D mutableLocationWithTerrain = this.mutableMap.GetMutableLocationWithTerrain(Terrain);
				if (mutableLocationWithTerrain != null)
				{
					int x = mutableLocationWithTerrain.X;
					int y = mutableLocationWithTerrain.Y;
					int num = mutableLocationWithTerrain.X / 3;
					int num2 = mutableLocationWithTerrain.Y / 3;
					string text = Zone.XYToID("JoppaWorld", x, y, 10);
					ZoneManager zoneManager = The.ZoneManager;
					if (zoneManager != null)
					{
						zoneManager.AdjustZoneGenerationTierTo(text);
					}
					encounter(text, mutableLocationWithTerrain, this.terrainComponents[Location2D.Get(num, num2)]);
					if (unflagAsMutable)
					{
						this.mutableMap.RemoveMutableLocation(mutableLocationWithTerrain);
					}
				}
			}
		}

		// Token: 0x06006A44 RID: 27204 RVA: 0x0021C600 File Offset: 0x0021A800
		public void BuildForts(string WorldID)
		{
			if (WorldID == "JoppaWorld")
			{
				WorldCreationProgress.StepProgress("Building forts...", false);
				this.AddMutableEncounterToTerrain("DesertCanyon", 1, delegate(string zoneID, Location2D location, TerrainTravel pTravel)
				{
					if (Options.ShowOverlandEncounters && pTravel != null)
					{
						pTravel.ParentObject.Render.RenderString = "]";
						pTravel.ParentObject.Render.SetForegroundColor('m');
					}
					GameObject gameObject = GameObject.Create("Snapjaw Hero Stopsvaalinn");
					gameObject.SetIntProperty("RequireVillagePlacement", 1, false);
					The.ZoneManager.AddZonePostBuilder(zoneID, "AddObjectBuilder", "Object", The.ZoneManager.CacheObject(gameObject, false, false));
					string text = this.AddSecret(zoneID, "the snapjaw who wields {{R-r-K-y-Y sequence|Stopsvalinn}}", new string[] { "artifact", "tech", "stopsvalinn", "old" }, "Artifacts", "$stopsvalinn", false, false);
					this.AddLocationFinder(zoneID, text, 0, 5000);
				}, true);
				this.AddMutableEncounterToTerrain("DesertCanyon", 25, delegate(string zoneID, Location2D location, TerrainTravel pTravel)
				{
					if (Options.ShowOverlandEncounters && pTravel != null)
					{
						pTravel.ParentObject.Render.RenderString = "F";
						pTravel.ParentObject.Render.SetForegroundColor('w');
					}
					The.ZoneManager.AddZoneBuilder(zoneID, 6000, "SnapjawStockadeMaker");
					string text2 = this.AddSecret(zoneID, "a snapjaw fort", new string[] { "snapjaw", "settlement", "humanoid" }, "Settlements", null, false, false);
					this.AddLocationFinder(zoneID, text2, 0, 5000);
					GeneratedLocationInfo generatedLocationInfo = new GeneratedLocationInfo();
					generatedLocationInfo.name = "a snapjaw fort";
					generatedLocationInfo.targetZone = zoneID;
					generatedLocationInfo.zoneLocation = location;
					generatedLocationInfo.secretID = text2;
					this.worldInfo.enemySettlements.Add(generatedLocationInfo);
					this.mutableMap.SetMutable(generatedLocationInfo.zoneLocation, 0);
				}, true);
				if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
				{
					WorldCreationProgress.StepProgress("Planting orchards...", false);
				}
				this.AddMutableEncounterToTerrain("DesertCanyon", 25, delegate(string zoneID, Location2D location, TerrainTravel pTravel)
				{
					if (Options.ShowOverlandEncounters && pTravel != null)
					{
						pTravel.ParentObject.Render.RenderString = "&RF";
					}
					The.ZoneManager.AddZoneBuilder(zoneID, 6000, "StarappleFarmMaker");
					string text3 = SettlementNames.GenerateStarappleFarmName(The.Game.sultanHistory);
					The.ZoneManager.SetZoneName(zoneID, text3, null, null, null, null, true, true);
					string text4 = this.AddSecret(zoneID, text3, new string[] { "apple", "settlement", "humanoid" }, "Settlements", null, false, false);
					this.AddLocationFinder(zoneID, text4, 0, 5000);
					The.ZoneManager.AddZonePostBuilder(zoneID, "IsCheckpoint", "Key", zoneID);
					GeneratedLocationInfo generatedLocationInfo2 = new GeneratedLocationInfo();
					generatedLocationInfo2.name = text3;
					generatedLocationInfo2.targetZone = zoneID;
					generatedLocationInfo2.zoneLocation = location;
					generatedLocationInfo2.secretID = text4;
					this.worldInfo.friendlySettlements.Add(generatedLocationInfo2);
					this.mutableMap.SetMutable(generatedLocationInfo2.zoneLocation, 0);
				}, true);
				WorldCreationProgress.StepProgress("Settling farms...", false);
				this.AddMutableEncounterToTerrain("DesertCanyon", 25, delegate(string zoneID, Location2D location, TerrainTravel pTravel)
				{
					if (Options.ShowOverlandEncounters && pTravel != null)
					{
						pTravel.ParentObject.Render.RenderString = "&RF";
					}
					The.ZoneManager.AddZoneBuilder(zoneID, 6000, "PigFarmMaker");
					string text5 = SettlementNames.GeneratePigFarmName(The.Game.sultanHistory);
					The.ZoneManager.SetZoneName(zoneID, text5, null, null, null, null, true, true);
					string text6 = this.AddSecret(zoneID, text5, new string[] { "pig", "settlement", "humanoid" }, "Settlements", null, false, false);
					this.AddLocationFinder(zoneID, text6, 0, 5000);
					The.ZoneManager.AddZonePostBuilder(zoneID, "IsCheckpoint", "Key", zoneID);
					The.ZoneManager.SetZoneProperty(zoneID, "ambient_bed_2", "Sounds/Ambiences/amb_creature_snapjaw");
					GeneratedLocationInfo generatedLocationInfo3 = new GeneratedLocationInfo();
					generatedLocationInfo3.name = text5;
					generatedLocationInfo3.targetZone = zoneID;
					generatedLocationInfo3.zoneLocation = location;
					generatedLocationInfo3.secretID = text6;
					this.worldInfo.friendlySettlements.Add(generatedLocationInfo3);
					this.mutableMap.SetMutable(generatedLocationInfo3.zoneLocation, 0);
				}, true);
			}
		}

		// Token: 0x06006A45 RID: 27205 RVA: 0x0021C6B2 File Offset: 0x0021A8B2
		public void BuildFarms(string WorldID)
		{
			if (CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsSwitch)))
			{
				WorldCreationProgress.StepProgress("Fencing pigs...", false);
			}
		}

		// Token: 0x06006A46 RID: 27206 RVA: 0x0021C6CC File Offset: 0x0021A8CC
		public void AddWaterway()
		{
			for (int i = 60; i < 240; i++)
			{
				for (int j = 0; j < 75; j++)
				{
					if (this.mutableMap.GetMutable(i, j) == 1)
					{
						string text = Zone.XYToID("JoppaWorld", i, j, 11);
						The.ZoneManager.AddZonePostBuilder(text, "Waterway");
					}
				}
			}
		}

		// Token: 0x06006A47 RID: 27207 RVA: 0x0021C728 File Offset: 0x0021A928
		public void AddOboroqorusLair()
		{
			Location2D location2D = this.mutableMap.popMutableLocationInArea(81, 54, 94, 68);
			if (location2D == null)
			{
				XRLCore.LogError("worldgen", "no position for oboroqoru's lair");
				return;
			}
			int x = location2D.X;
			int y = location2D.Y;
			Faction faction = Factions.Get("Apes");
			string text = this.ZoneIDFromXY("JoppaWorld", x, y);
			ZoneManager zoneManager = The.ZoneManager;
			if (zoneManager != null)
			{
				zoneManager.AdjustZoneGenerationTierTo(text);
			}
			string text2 = this.AddSecret(text, "the lair of {{M|Oboroqoru, Ape God}}", new string[] { "lair", "oboroqoru", "ape" }, "Lairs", "$oboroqorulair", false, false);
			GameObject firstObjectWithPart = The.ZoneManager.GetZone("JoppaWorld").GetCell(x / 3, y / 3).GetFirstObjectWithPart("TerrainTravel");
			((firstObjectWithPart != null) ? firstObjectWithPart.GetPart<TerrainTravel>() : null).AddEncounter(new EncounterEntry("You discover a lair. Would you like to investigate?", text, "", text2, true));
			if (Options.ShowOverlandEncounters)
			{
				Render render = The.ZoneManager.GetZone("JoppaWorld").GetCell(x / 3, y / 3).GetObjectInCell(0)
					.Render;
				if (render != null)
				{
					render.RenderString = "A";
					render.ColorString = (render.TileColor = "&M");
				}
			}
			for (int i = -1; i < 1; i++)
			{
				for (int j = -1; j < 1; j++)
				{
					string text3 = Zone.XYToID("JoppaWorld", x + i, y + j, 10);
					if ((i == 0 && j == 0) || 75.in100())
					{
						The.ZoneManager.AddZoneBuilder(text3, 6000, "Torchposts");
					}
				}
			}
			string text4 = "the lair of Oboroqoru, Ape God";
			for (int k = 0; k < 10; k++)
			{
				string text5 = Zone.XYToID("JoppaWorld", x, y, 10 + k);
				The.ZoneManager.SetZoneNameContext(text5, text4, true);
				faction.HolyPlaces.Add(text5);
				if (k == 0)
				{
					The.ZoneManager.AddZonePostBuilder(text5, "WallOutcrop", "Blueprint", "Shale");
					this.AddLocationFinder(text5, text2, 3000, 5000);
				}
				else
				{
					if (k == 9)
					{
						The.ZoneManager.AddZoneBuilder(text5, -1000, "BasicRoomHall");
						The.ZoneManager.AddZoneBuilder(text5, -1000, "StairsUp");
						GameObject gameObject = GameObject.Create("Oboroqoru");
						gameObject.SetStringProperty("nosecret", text2, false);
						The.ZoneManager.AddZoneBuilder(text5, -1000, "AddObjectBuilder", "Object", The.ZoneManager.CacheObject(gameObject, false, false));
						The.ZoneManager.AddZoneBuilder(text5, -1000, "AddBlueprintBuilder", "Object", "Chest6");
					}
					else
					{
						The.ZoneManager.AddZoneBuilder(text5, -1000, "BasicRoomHall");
						The.ZoneManager.AddZoneBuilder(text5, -1000, "StairsUp");
						The.ZoneManager.AddZoneBuilder(text5, -1000, "StairsDown");
					}
					if (45.in100())
					{
						The.ZoneManager.AddZoneBuilder(text5, -1000, "AddBlueprintBuilder", "Object", "Chest7");
					}
					The.ZoneManager.AddZoneBuilder(text5, -1000, "ApegodCave");
					if (k < 3)
					{
						The.ZoneManager.AddZoneBuilder(text5, -1000, "Population", "Table", "ApeGodLair1");
					}
					else if (k < 6)
					{
						The.ZoneManager.AddZoneBuilder(text5, -1000, "Population", "Table", "ApeGodLair2");
					}
					else
					{
						The.ZoneManager.AddZoneBuilder(text5, -1000, "Population", "Table", "ApeGodLair3");
					}
					this.AddLocationFinder(text5, text2, 3000, -1000);
				}
			}
		}

		// Token: 0x06006A48 RID: 27208 RVA: 0x0021CAF4 File Offset: 0x0021ACF4
		public void AddNephilimLairs()
		{
			this.AddRermadonLair();
			this.AddQasQonLair();
			this.AddShugruithLair();
			this.AddAgolgotLairSecret();
			this.AddBethsaidaLairSecret();
		}

		// Token: 0x06006A49 RID: 27209 RVA: 0x0021CB14 File Offset: 0x0021AD14
		public void AddShugruithLair()
		{
			ZoneManager zoneManager = The.ZoneManager;
			Location2D location2D = this.peekMutableLocation(null, false);
			int i = 10;
			string text = this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i);
			ZoneManager zoneManager2 = The.ZoneManager;
			if (zoneManager2 != null)
			{
				zoneManager2.AdjustZoneGenerationTierTo(text);
			}
			string text2 = this.AddSecret(text, "the mouth of {{M|Shug'ruith the Burrower}}", new string[] { "lair", "shugruith", "nephilim" }, "Lairs", "$shugruithmouth", false, false);
			this.AddLocationFinder(text, text2, 2000, 5000);
			TerrainTravel terrainTravel;
			if (zoneManager.GetZone("JoppaWorld").GetCell(location2D.X / 3, location2D.Y / 3).GetFirstObjectWithPart("TerrainTravel")
				.TryGetPart<TerrainTravel>(out terrainTravel))
			{
				terrainTravel.AddEncounter(new EncounterEntry("You come upon a yawning pit. Would you like to investigate?", text, "", text2, true));
			}
			string[] cardinalDirectionList = Directions.CardinalDirectionList;
			string text3 = "D";
			int num = Stat.Random(40, 47);
			int num2 = Stat.Random(5, 75);
			int num3 = Stat.Random(5, 19);
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowDescendingMouth", "X", num2, "Y", num3);
			The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowBuilder");
			i = 11;
			IL_0495:
			while (i < num)
			{
				Stat.Random(0, 3);
				if (text3 == "D")
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowAscendingMouth", "X", num2, "Y", num3);
				}
				if (text3 == "S")
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowNorthMouth");
				}
				if (text3 == "N")
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowSouthMouth");
				}
				if (text3 == "W")
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowEastMouth");
				}
				if (text3 == "E")
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowWestMouth");
				}
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowBuilder");
				if (Stat.Random(1, 100) <= 50 || i == num - 1)
				{
					text3 = "D";
				}
				else
				{
					text3 = cardinalDirectionList.GetRandomElement<string>(null);
				}
				while (!(text3 == "D"))
				{
					Location2D location2D2 = location2D.FromDirection(text3);
					if (!(location2D2 == null) && location2D2.X >= 0 && location2D2.Y >= 0 && location2D2.X < 240 && location2D2.Y < 75 && this.mutableMap.GetMutable(location2D2.X, location2D2.Y) == 1)
					{
						if (text3 == "N")
						{
							The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowNorthMouth");
						}
						if (text3 == "S")
						{
							The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowSouthMouth");
						}
						if (text3 == "E")
						{
							The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowEastMouth");
						}
						if (text3 == "W")
						{
							The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowWestMouth");
						}
						location2D = location2D2;
						goto IL_0495;
					}
					text3 = "D";
				}
				num2 = Stat.Random(5, 75);
				num3 = Stat.Random(5, 19);
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowDescendingMouth", "X", num2, "Y", num3);
				i++;
			}
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowAscendingMouth");
			The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i), "ShugBurrowBuilder");
			string text4 = this.ZoneIDFromXYz("JoppaWorld", location2D.X, location2D.Y, i);
			ZoneManager zoneManager3 = The.ZoneManager;
			if (zoneManager3 != null)
			{
				zoneManager3.AdjustZoneGenerationTierTo(text4);
			}
			this.AddSecret(text4, "the cradle of {{M|Shug'ruith the Burrower}}", new string[] { "lair", "shugruith", "nephilim" }, "Lairs", "$shugruithlair", false, false);
			for (int j = 0; j <= 3; j++)
			{
				string text5 = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, i + j);
				zoneManager.AddZonePostBuilder(text5, "ShugLair", "l", j);
				zoneManager.SetZoneProperty(text5, "ChasmColor", "&m");
			}
		}

		// Token: 0x06006A4A RID: 27210 RVA: 0x0021D0C8 File Offset: 0x0021B2C8
		public void AddQasQonLair()
		{
			ZoneManager zoneManager = The.ZoneManager;
			Location2D location2D = this.popMutableLocationOfTerrain("MoonStair", null, false);
			int x = location2D.X;
			int y = location2D.Y;
			string text = this.ZoneIDFromXY("JoppaWorld", x, y);
			ZoneManager zoneManager2 = The.ZoneManager;
			if (zoneManager2 != null)
			{
				zoneManager2.AdjustZoneGenerationTierTo(text);
			}
			string text2 = this.AddSecret(text, "the chuppah of {{M|Girsh Qas and Girsh Qon}}", new string[] { "lair", "qasqon", "nephilim" }, "Lairs", "$qasqonlair", false, false);
			TerrainTravel terrainTravel;
			if (zoneManager.GetZone("JoppaWorld").GetCell(x / 3, y / 3).GetFirstObjectWithPart("TerrainTravel")
				.TryGetPart<TerrainTravel>(out terrainTravel))
			{
				terrainTravel.AddEncounter(new EncounterEntry("You discover a lair. Would you like to investigate?", text, "", text2, true));
			}
			for (int i = 0; i <= 3; i++)
			{
				string text3 = Zone.XYToID("JoppaWorld", x, y, 10 + i);
				if (i == 3)
				{
					zoneManager.AddZoneBuilder(text3, 6000, "QasQonLair", "Nephal", true);
				}
				else
				{
					zoneManager.AddZoneBuilder(text3, 6000, "QasQonLair");
				}
				zoneManager.SetZoneProperty(text3, "ChasmColor", "&m");
			}
		}

		// Token: 0x06006A4B RID: 27211 RVA: 0x0021D1FC File Offset: 0x0021B3FC
		public void AddRermadonLair()
		{
			ZoneManager zoneManager = The.ZoneManager;
			Location2D location2D = this.popMutableLocationOfTerrain("PalladiumReef", null, false);
			int x = location2D.X;
			int y = location2D.Y;
			string text = this.ZoneIDFromXY("JoppaWorld", x, y);
			ZoneManager zoneManager2 = The.ZoneManager;
			if (zoneManager2 != null)
			{
				zoneManager2.AdjustZoneGenerationTierTo(text);
			}
			string text2 = this.AddSecret(text, "the cradle of {{M|Girsh Rermadon}}", new string[] { "lair", "rermadon", "nephilim" }, "Lairs", "$rermadonlair", false, false);
			TerrainTravel terrainTravel;
			if (zoneManager.GetZone("JoppaWorld").GetCell(x / 3, y / 3).GetFirstObjectWithPart("TerrainTravel")
				.TryGetPart<TerrainTravel>(out terrainTravel))
			{
				terrainTravel.AddEncounter(new EncounterEntry("You discover a lair. Would you like to investigate?", text, "", text2, true));
			}
			for (int i = 0; i <= 3; i++)
			{
				string text3 = Zone.XYToID("JoppaWorld", x, y, 10 + i);
				if (i == 3)
				{
					zoneManager.AddZoneBuilder(text3, 6000, "RermadonLair", "Nephal", true);
				}
				else
				{
					zoneManager.AddZoneBuilder(text3, 6000, "RermadonLair");
				}
				zoneManager.SetZoneProperty(text3, "ChasmColor", "&m");
			}
		}

		// Token: 0x06006A4C RID: 27212 RVA: 0x0021D330 File Offset: 0x0021B530
		public void AddAgolgotLairSecret()
		{
			GameObject gameObject = this.WorldZone.FindObject("TerrainGolgotha");
			Cell cell = ((gameObject != null) ? gameObject.CurrentCell : null);
			if (cell == null)
			{
				MetricsManager.LogError("Unable to find world map terrain of Golgotha.");
				return;
			}
			int oracleIntColumn = ZoneBuilderSandbox.GetOracleIntColumn(cell.X, cell.Y, 1, 1, 40, 45);
			string text = ZoneID.Assemble(this.World, cell.X, cell.Y, 1, 1, oracleIntColumn - 3 + 1);
			ZoneManager zoneManager = The.ZoneManager;
			if (zoneManager != null)
			{
				zoneManager.AdjustZoneGenerationTierTo(text);
			}
			this.AddSecret(text, "the cradle of {{M|Girsh Agolgot}}", new string[] { "lair", "agolgot", "nephilim" }, "Lairs", "$agolgotlair", false, false);
		}

		// Token: 0x06006A4D RID: 27213 RVA: 0x0021D3E8 File Offset: 0x0021B5E8
		public void AddBethsaidaLairSecret()
		{
			GameObject gameObject = this.WorldZone.FindObject("TerrainBethesdaSusa");
			Cell cell = ((gameObject != null) ? gameObject.CurrentCell : null);
			if (cell == null)
			{
				MetricsManager.LogError("Unable to find world map terrain of Bethesda Susa.");
				return;
			}
			int oracleIntColumn = ZoneBuilderSandbox.GetOracleIntColumn(cell.X, cell.Y, 1, 1, 40, 45);
			string text = ZoneID.Assemble(this.World, cell.X, cell.Y, 1, 1, oracleIntColumn - 3 + 1);
			ZoneManager zoneManager = The.ZoneManager;
			if (zoneManager != null)
			{
				zoneManager.AdjustZoneGenerationTierTo(text);
			}
			this.AddSecret(text, "the cradle of {{M|Girsh Bethsaida}}", new string[] { "lair", "bethsaida", "nephilim" }, "Lairs", "$bethsaidalair", false, false);
		}

		// Token: 0x06006A4E RID: 27214 RVA: 0x0021D4A0 File Offset: 0x0021B6A0
		public void AddMamonVillage()
		{
			List<Location2D> list = this.BuildMamonRiver("JoppaWorld", 81, 62, 1, 8, 20, 3);
			while (list.Count <= 18)
			{
				list = this.BuildMamonRiver("JoppaWorld", 81, 62, 1, 8, 20, 3);
			}
			int num = Stat.Random(3, 5);
			int num2 = list[num].X;
			int num3 = list[num].Y;
			string text = this.ZoneIDFromXY("JoppaWorld", num2, num3);
			The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "IdolFight");
			num += Stat.Random(3, 5);
			num2 = list[num].X;
			num3 = list[num].Y;
			text = this.ZoneIDFromXY("JoppaWorld", num2, num3);
			The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "MinorRazedGoatfolkVillage");
			num += Stat.Random(3, 5);
			num2 = list[num].X;
			num3 = list[num].Y;
			text = this.ZoneIDFromXY("JoppaWorld", num2, num3);
			The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "WildWatervineMerchant");
			num++;
			if (list[num - 1].X < list[num].X)
			{
				The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "SmokingAreaE");
			}
			if (list[num - 1].Y < list[num].Y)
			{
				The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "SmokingAreaS");
			}
			if (list[num - 1].Y > list[num].Y)
			{
				The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "SmokingAreaN");
			}
			num2 = list[num].X;
			num3 = list[num].Y;
			text = this.ZoneIDFromXY("JoppaWorld", num2, num3);
			The.ZoneManager.AddZonePostBuilderAfterTerrain(text, "RazedGoatfolkVillage");
			if (Options.ShowOverlandEncounters)
			{
				The.ZoneManager.GetZone("JoppaWorld").GetCell(num2 / 3, num3 / 3).GetObjectInCell(0)
					.GetPart<Render>()
					.RenderString = "&Mg";
			}
			this.AddSecret(Zone.XYToID("JoppaWorld", num2, num3, 10), "the village lair of {{M|Mamon Souldrinker}}", new string[] { "lair", "mammon", "goatfolk", "humanoid" }, "Lairs", "$mamonvillage", false, false);
			this.AddLocationFinder(text, "$mamonvillage", 0, 5000);
		}

		// Token: 0x06006A4F RID: 27215 RVA: 0x0021D708 File Offset: 0x0021B908
		public string ZoneIDFromXYz(string World, int xp, int yp, int zp)
		{
			int num = (int)Math.Floor((double)((float)xp / 3f));
			int num2 = (int)Math.Floor((double)((float)yp / 3f));
			return ZoneID.Assemble(World, num, num2, xp % 3, yp % 3, zp);
		}

		// Token: 0x06006A50 RID: 27216 RVA: 0x0021D748 File Offset: 0x0021B948
		public string ZoneIDFromXY(string World, int xp, int yp)
		{
			int num = (int)Math.Floor((double)((float)xp / 3f));
			int num2 = (int)Math.Floor((double)((float)yp / 3f));
			return ZoneID.Assemble(World, num, num2, xp % 3, yp % 3, 10);
		}

		// Token: 0x06006A51 RID: 27217 RVA: 0x0021D788 File Offset: 0x0021B988
		public string AddSecret(string secretZone, string name, string[] adj, string category, string secretid = null, bool revealed = false, bool silent = false)
		{
			if (secretid != null && JournalAPI.GetMapNote(secretid) != null)
			{
				Debug.LogWarning("dupe secret: " + secretid);
				return secretid;
			}
			string objectTypeForZone = ZoneManager.GetObjectTypeForZone(secretZone);
			if (objectTypeForZone != "")
			{
				List<string> list = new List<string>(adj);
				string tag = GameObjectFactory.Factory.Blueprints[objectTypeForZone].GetTag("SecretAttributes", "");
				if (tag != "")
				{
					list.AddRange(tag.Split(',', StringSplitOptions.None));
				}
				else
				{
					list.Add(objectTypeForZone.Replace("Terrain", "").Replace("Watervine", "Saltmarsh").Replace("1", "")
						.Replace("2", "")
						.Replace("3", "")
						.Replace("4", "")
						.Replace("5", "")
						.Replace("6", "")
						.Replace("7", "")
						.Replace("8", "")
						.Replace("9", "")
						.Replace("0", "")
						.ToLower());
				}
				adj = list.ToArray();
			}
			if (secretid == null)
			{
				secretid = Guid.NewGuid().ToString();
			}
			JournalAPI.AddMapNote(secretZone, name, category, adj, secretid, revealed, false, 0L, silent);
			return secretid;
		}

		// Token: 0x06006A52 RID: 27218 RVA: 0x0021D90C File Offset: 0x0021BB0C
		public void AddHindrenVillage(string WorldID)
		{
			Location2D location2D = this.popMutableBlockOfTerrain("Flowerfields");
			string text = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10);
			Cell cell = The.ZoneManager.GetZone(WorldID).GetCell(location2D.X / 3, location2D.Y / 3);
			The.ZoneManager.SetZoneProperty(text, "NoBiomes", "Yes");
			cell.GetFirstObjectWithPart("TerrainTravel").AddPart<BeyLahTerrain>(new BeyLahTerrain(), true, false);
			The.ZoneManager.ClearZoneBuilders(text);
			The.ZoneManager.AddZoneBuilder(text, 4900, "ClearAll");
			The.ZoneManager.AddZonePostBuilder(text, "MapBuilder", "FileName", "BeyLah.rpm");
			The.ZoneManager.AddZonePostBuilder(text, "HindrenClues");
			The.ZoneManager.AddZonePostBuilder(text, "Music", "Track", "Music/Bey Lah Heritage II");
			The.ZoneManager.AddZonePostBuilder(text, "AddWidgetBuilder", "Blueprint", "BeyLahSurface");
			The.ZoneManager.AddZonePostBuilder(text, "IsCheckpoint", "Key", text);
			The.ZoneManager.SetZoneName(text, "Bey Lah", null, null, null, null, true, true);
			The.ZoneManager.SetZoneIncludeStratumInZoneDisplay(text, new bool?(false));
			The.ZoneManager.SetZoneProperty(text, "SkipTerrainBuilders", true);
			foreach (string text2 in Directions.DirectionList)
			{
				Location2D location2D2 = location2D.FromDirection(text2);
				string text3 = Zone.XYToID("JoppaWorld", location2D2.X, location2D2.Y, 10);
				The.ZoneManager.AdjustZoneGenerationTierTo(text3);
				The.ZoneManager.AddZoneBuilder(text3, 4900, "ClearAll");
				The.ZoneManager.AddZoneBuilder(text3, 5000, "BeyLahOutskirts");
				The.ZoneManager.AddZoneBuilder(text3, 5000, "Music", "Track", "Music/Bey Lah Heritage II");
				The.ZoneManager.SetZoneName(text3, "outskirts", "Bey Lah", "some", null, null, false, true);
				The.ZoneManager.SetZoneIncludeStratumInZoneDisplay(text3, new bool?(false));
			}
			List<Location2D> list = new List<Location2D>();
			List<int> list2 = new List<int> { 0, 1, 2, 3 };
			list2.ShuffleInPlace<int>(null);
			int num = 4;
			bool flag = false;
			for (;;)
			{
				foreach (int num2 in list2)
				{
					list = this.BuildEskhindRoad("JoppaWorld", location2D.X, location2D.Y, num2, 8, 4, -1, false);
					Location2D location2D3 = list.Last<Location2D>();
					if (location2D3.Distance(location2D) >= num && this.mutableMap.GetMutable(Location2D.Get(location2D3.X, location2D3.Y)) > 0)
					{
						flag = true;
						list = this.BuildEskhindRoad("JoppaWorld", location2D.X, location2D.Y, num2, 8, 4, -1, true);
						break;
					}
				}
				if (flag)
				{
					break;
				}
				num--;
			}
			string text4 = Zone.XYToID("JoppaWorld", list.Last<Location2D>().X, list.Last<Location2D>().Y, 10);
			The.ZoneManager.AdjustZoneGenerationTierTo(text4);
			The.ZoneManager.ClearZoneBuilders(text4);
			The.ZoneManager.AddZoneBuilder(text4, 6000, "ClearAll");
			The.ZoneManager.AddZoneBuilder(text4, 6000, "MapBuilder", "FileName", "HollowTree.rpm");
			The.ZoneManager.AddZoneBuilder(text4, 6000, "Music", "Track", "Music/Overworld");
			base.game.SetStringGameState("HollowTreeZoneId", text4);
			The.ZoneManager.SetZoneProperty(text4, "NoBiomes", "Yes");
			this.mutableMap.SetMutable(Location2D.Get(list.Last<Location2D>().X, list.Last<Location2D>().Y), 0);
			GeneratedLocationInfo generatedLocationInfo = new GeneratedLocationInfo();
			generatedLocationInfo.name = "Bey Lah";
			generatedLocationInfo.targetZone = text;
			generatedLocationInfo.zoneLocation = Location2D.Get(location2D.X, location2D.Y);
			generatedLocationInfo.secretID = null;
			this.worldInfo.villages.Add(generatedLocationInfo);
			this.mutableMap.SetMutable(generatedLocationInfo.zoneLocation, 0);
			base.game.SetStringGameState("BeyLahZoneID", Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10));
			this.AddSecret(Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10), "Bey Lah", new string[] { "settlement", "hindren" }, "Settlements", "$beylah", false, false);
			JournalAPI.GetMapNote("$beylah").Attributes.Add("nobuy:Hindren");
		}

		// Token: 0x06006A53 RID: 27219 RVA: 0x0021DDF4 File Offset: 0x0021BFF4
		public void AddHydropon(string WorldID)
		{
			Location2D location2D = this.popMutableLocationOfTerrain("PalladiumReef", null, false);
			string text = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10);
			Cell cell = The.ZoneManager.GetZone(WorldID).GetCell(location2D.X / 3, location2D.Y / 3);
			The.ZoneManager.SetZoneProperty(text, "NoBiomes", "Yes");
			The.ZoneManager.SetZoneProperty(text, "NoSvardymStorm", "Yes");
			cell.GetFirstObjectWithPart("TerrainTravel").AddPart<HydroponTerrain>(new HydroponTerrain(), true, false);
			The.ZoneManager.ClearZoneBuilders(text);
			The.ZoneManager.AddZonePostBuilder(text, "ClearAll");
			The.ZoneManager.AddZonePostBuilder(text, "Reef");
			The.ZoneManager.AddZonePostBuilder(text, "MapBuilder", "FileName", "Hydropon.rpm", "ClearBeforePlace", true);
			The.ZoneManager.SetZoneName(text, "Hydropon", null, null, null, "the", true, true);
			The.ZoneManager.SetZoneIncludeStratumInZoneDisplay(text, new bool?(false));
			The.ZoneManager.SetZoneProperty(text, "SkipTerrainBuilders", true);
			GeneratedLocationInfo generatedLocationInfo = new GeneratedLocationInfo();
			generatedLocationInfo.name = "Hydropon";
			generatedLocationInfo.targetZone = text;
			generatedLocationInfo.zoneLocation = location2D;
			generatedLocationInfo.secretID = null;
			this.worldInfo.villages.Add(generatedLocationInfo);
			this.mutableMap.SetMutable(generatedLocationInfo.zoneLocation, 0);
			base.game.SetStringGameState("HydroponZoneID", Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10));
			string text2 = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10);
			The.ZoneManager.AdjustZoneGenerationTierTo(text2);
			this.AddSecret(text2, "the Hydropon", new string[] { "settlement" }, "Settlements", "$hydropon", false, false);
			this.AddLocationFinder(text2, "$hydropon", 0, 5000);
		}

		// Token: 0x06006A54 RID: 27220 RVA: 0x0021DFE8 File Offset: 0x0021C1E8
		public void AddHamilcrabSecret()
		{
			GameObject firstObject = this.WorldZone.GetFirstObject("TerrainAsphaltMines");
			Cell cell = ((firstObject != null) ? firstObject.CurrentCell : null);
			if (cell != null)
			{
				string text = Zone.XYToID(this.WorldZone.ZoneWorld, cell.X * 3 + 1, cell.Y * 3 + 1, 40);
				this.AddSecret(text, "Hamilcrab's shop", new string[] { "special", "oddity", "merchant" }, "Merchants", "$hamilcrab", false, false);
			}
		}

		// Token: 0x06006A55 RID: 27221 RVA: 0x0021E074 File Offset: 0x0021C274
		public void AddGreatMachineArtifact()
		{
			Location2D locationOfTier = this.getLocationOfTier(1, 8);
			string text = Zone.XYToID("JoppaWorld", locationOfTier.X * 3 + 1, locationOfTier.Y * 3 + 1, Stat.Random(40, 50));
			The.ZoneManager.AdjustZoneGenerationTierTo(text);
			string blueprint = PopulationManager.RollOneFrom("StaticObjectsTable:GreatMachine_Artifact", null, null).Blueprint;
			GameObjectBlueprint blueprint2 = GameObjectFactory.Factory.GetBlueprint(blueprint);
			The.ZoneManager.AddZonePostBuilder(text, "PlaceRelicBuilder", "Blueprint", blueprint);
			this.AddSecret(text, blueprint2.CachedDisplayNameStripped, new string[] { "artifact" }, "Artifacts", "$greatmachineartifact", false, false);
		}

		// Token: 0x06006A56 RID: 27222 RVA: 0x0021E11C File Offset: 0x0021C31C
		public List<Location2D> BuildEskhindRoad(string WorldID, int StartX, int StartY, int Direction, int Bias, int MinimumLength, int ExcludeDirection, bool layRoad)
		{
			if (layRoad)
			{
				string text = this.ZoneIDFromXY(WorldID, StartX, StartY);
				if (Direction == 0)
				{
					base.game.SetStringGameState("EskhindRoadDirection", "north");
					The.ZoneManager.AddZoneConnection(text, "-", 43, 12, "Road", null);
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_NORTH;
				}
				else if (Direction == 1)
				{
					base.game.SetStringGameState("EskhindRoadDirection", "east");
					The.ZoneManager.AddZoneConnection(text, "-", 72, 16, "Road", null);
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_EAST;
				}
				else if (Direction == 2)
				{
					base.game.SetStringGameState("EskhindRoadDirection", "south");
					The.ZoneManager.AddZoneConnection(text, "-", 59, 19, "Road", null);
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_SOUTH;
				}
				else if (Direction == 3)
				{
					base.game.SetStringGameState("EskhindRoadDirection", "west");
					The.ZoneManager.AddZoneConnection(text, "-", 20, 9, "Road", null);
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_WEST;
				}
				if ((this.RoadSystem[StartX, StartY] & JoppaWorldBuilder.ROAD_NORTH) != 0U)
				{
					The.ZoneManager.AddZoneConnection(text, "-", 36, 0, "RoadNorthMouth", null);
					The.ZoneManager.AddZoneConnection(text, "n", 36, 24, "RoadSouthMouth", null);
				}
				if ((this.RoadSystem[StartX, StartY] & JoppaWorldBuilder.ROAD_EAST) != 0U)
				{
					The.ZoneManager.AddZoneConnection(text, "-", 79, 13, "RoadEastMouth", null);
					The.ZoneManager.AddZoneConnection(text, "e", 0, 13, "RoadWestMouth", null);
				}
				if ((this.RoadSystem[StartX, StartY] & JoppaWorldBuilder.ROAD_SOUTH) != 0U)
				{
					The.ZoneManager.AddZoneConnection(text, "-", 57, 24, "RoadSouthMouth", null);
					The.ZoneManager.AddZoneConnection(text, "s", 57, 0, "RoadNorthMouth", null);
				}
				if ((this.RoadSystem[StartX, StartY] & JoppaWorldBuilder.ROAD_WEST) != 0U)
				{
					The.ZoneManager.AddZoneConnection(text, "-", 0, 8, "RoadWestMouth", null);
					The.ZoneManager.AddZoneConnection(text, "w", 79, 8, "RoadEastMouth", null);
				}
				The.ZoneManager.AddZonePostBuilder(text, "RoadBuilder", "ClearSolids", false, "Noise", false);
				this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_START;
			}
			List<Location2D> list = new List<Location2D>();
			this.ContinueEskhindRoad(StartX, StartY, Direction, 0, Bias, MinimumLength, ExcludeDirection, list, layRoad);
			return list;
		}

		// Token: 0x06006A57 RID: 27223 RVA: 0x0021E3E4 File Offset: 0x0021C5E4
		public void ContinueEskhindRoad(int StartX, int StartY, int Direction, int Depth, int Bias, int MinimumLength, int ExcludeDirection, List<Location2D> Points, bool layRoad)
		{
			int num = StartX;
			int num2 = StartY;
			if (Direction == 0)
			{
				num2--;
			}
			if (Direction == 2)
			{
				num2++;
			}
			if (Direction == 1)
			{
				num++;
			}
			if (Direction == 3)
			{
				num--;
			}
			if (num < 0 || num2 < 0 || num >= 240 || num2 >= 75)
			{
				return;
			}
			if (layRoad)
			{
				this.mutableMap.RemoveMutableLocation(Location2D.Get(num, num2));
			}
			Points.Add(Location2D.Get(num, num2));
			if (Depth > MinimumLength && Stat.Random(0, 100) < 50 + (Depth - MinimumLength) * 25)
			{
				if (layRoad)
				{
					The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadStartMouth");
					this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_START;
					The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadBuilder");
				}
				return;
			}
			int num3 = Direction;
			if (Bias <= 0 && 50.in100())
			{
				int num4 = Stat.Random(0, 1);
				if (num4 == 0)
				{
					num3--;
				}
				if (num4 == 1)
				{
					num3++;
				}
				if (num3 < 0)
				{
					num3 = 3;
				}
				if (num3 > 3)
				{
					num3 = 0;
				}
				if (num3 == ExcludeDirection)
				{
					num3 = Direction;
				}
			}
			if (Direction == 0 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadSouthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_SOUTH;
			}
			if (Direction == 1 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadWestMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_WEST;
			}
			if (Direction == 2 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadNorthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_NORTH;
			}
			if (Direction == 3 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadEastMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_EAST;
			}
			int num5 = num;
			int num6 = num2;
			if (num3 == 0)
			{
				num6--;
			}
			if (num3 == 2)
			{
				num6++;
			}
			if (num3 == 1)
			{
				num5++;
			}
			if (num3 == 3)
			{
				num5--;
			}
			if (num6 < 0 || num5 < 0 || num6 == 75 || num5 == 240 || (Depth > 0 && this.RoadSystem[num5, num6] != 0U))
			{
				if (layRoad)
				{
					this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_START;
					The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadStartMouth");
				}
				return;
			}
			if (num3 == 0 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadNorthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_NORTH;
			}
			if (num3 == 1 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadEastMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_EAST;
			}
			if (num3 == 2 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadSouthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_SOUTH;
			}
			if (num3 == 3 && layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadWestMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_WEST;
			}
			bool flag = false;
			if (layRoad)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadBuilder");
			}
			if (layRoad && Options.ShowOverlandEncounters)
			{
				Render part = The.ZoneManager.GetZone("JoppaWorld").GetCell(num / 3, num2 / 3).GetObjectInCell(0)
					.GetPart<Render>();
				part.RenderString = "=";
				if (flag)
				{
					part.RenderString = "t";
				}
			}
			this.ContinueEskhindRoad(num, num2, num3, Depth + 1, Bias - 1, MinimumLength, ExcludeDirection, Points, layRoad);
		}

		// Token: 0x06006A58 RID: 27224 RVA: 0x0021E7E4 File Offset: 0x0021C9E4
		public void AddYonderPath(string WorldID)
		{
			Location2D location = The.ZoneManager.GetZone(WorldID).GetFirstObject("TerrainFungalCenter").CurrentCell.Location;
			Location2D location2D = Location2D.Get(location.X * 3 + 1, location.Y * 3 + 1);
			string text = this.ZoneIDFromXY(WorldID, location2D.X, location2D.Y);
			The.ZoneManager.AddZoneBuilder(text, 6000, "FungalTrailExileCorpse");
			The.ZoneManager.AddZoneConnection(text, "-", Stat.Random(20, 50), Stat.Random(10, 15), "FungalTrailStart", null);
			this.mutableMap.SetMutable(location2D, 0);
			List<Location2D> list = new List<Location2D>(4) { location2D };
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			while (num < 3 && num4 < 100)
			{
				string randomCardinalDirection = Directions.GetRandomCardinalDirection();
				num2 = list[num].X;
				num3 = list[num].Y;
				Directions.ApplyDirection(randomCardinalDirection, ref num2, ref num3, 1);
				Location2D location2D2 = Location2D.Get(num2, num3);
				if (!list.Contains(location2D2) && this.mutableMap.GetMutable(location2D2) > 0)
				{
					int num5 = Stat.Random(10, 70);
					int num6 = Stat.Random(5, 20);
					if (randomCardinalDirection == "N")
					{
						The.ZoneManager.AddZoneConnection(text, "-", num5, 0, "FungalTrailNorthMouth", null);
						The.ZoneManager.AddZoneConnection(text, randomCardinalDirection, num5, 24, "FungalTrailSouthMouth", null);
					}
					else if (randomCardinalDirection == "S")
					{
						The.ZoneManager.AddZoneConnection(text, "-", num5, 24, "FungalTrailSouthMouth", null);
						The.ZoneManager.AddZoneConnection(text, randomCardinalDirection, num5, 0, "FungalTrailNorthMouth", null);
					}
					else if (randomCardinalDirection == "E")
					{
						The.ZoneManager.AddZoneConnection(text, "-", 79, num6, "FungalTrailEastMouth", null);
						The.ZoneManager.AddZoneConnection(text, randomCardinalDirection, 0, num6, "FungalTrailWestMouth", null);
					}
					else if (randomCardinalDirection == "W")
					{
						The.ZoneManager.AddZoneConnection(text, "-", 0, num6, "FungalTrailWestMouth", null);
						The.ZoneManager.AddZoneConnection(text, randomCardinalDirection, 79, num6, "FungalTrailEastMouth", null);
					}
					list.Add(location2D2);
					The.ZoneManager.AddZoneBuilder(text, 6000, "FungalTrailBuilder");
					this.mutableMap.SetMutable(location2D2, 0);
					text = this.ZoneIDFromXY(WorldID, num2, num3);
					num++;
				}
				num4++;
			}
			The.ZoneManager.AddZoneBuilder(text, 6000, "FungalTrailKlanqHut");
			The.ZoneManager.AddZoneBuilder(text, 6000, "FungalTrailBuilder");
			The.ZoneManager.AddZoneConnection(text, "-", Stat.Random(20, 50), Stat.Random(10, 15), "FungalTrailStart", null);
			base.game.SetStringGameState("FungalTrailEnd", text);
		}

		// Token: 0x06006A59 RID: 27225 RVA: 0x0021EAD0 File Offset: 0x0021CCD0
		public void AddStaticEncounters(string WorldID)
		{
			The.ZoneManager.GetZone("JoppaWorld");
			WorldCreationProgress.StepProgress("Generating village history...", false);
			if (WorldID == "JoppaWorld")
			{
				Location2D location2D = this.popMutableLocationOfTerrain("Saltmarsh", null, true);
				string text = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10);
				GameObject gameObject = The.ZoneManager.GetZone(WorldID).GetCell(location2D.X / 3, location2D.Y / 3).Objects[0];
				The.ZoneManager.AddZonePostBuilder(text, "AddBlueprintBuilder", "Object", "SkrefCorpse");
				if (Options.ShowOverlandEncounters)
				{
					gameObject.Render.RenderString = "&r%";
				}
				this.AddSecret(text, "some flattened remains", new string[] { "encounter", "special", "oddity" }, "Oddities", "$skrefcorpse", false, false);
				Location2D location2D2 = this.popMutableLocationOfTerrain("DesertCanyon", null, true);
				string text2 = Zone.XYToID("JoppaWorld", location2D2.X, location2D2.Y, 10);
				gameObject = The.ZoneManager.GetZone(WorldID).GetCell(location2D2.X / 3, location2D2.Y / 3).Objects[0];
				The.Game.SetStringGameState("$TrembleEntranceEncounter", text2);
				The.ZoneManager.AddZonePostBuilder(text2, "TrembleEntrance");
				if (Options.ShowOverlandEncounters)
				{
					gameObject.Render.RenderString = "&Wx";
				}
				Location2D location2D3 = this.popMutableLocationOfTerrain("Saltmarsh", null, true);
				string text3 = Zone.XYToID("JoppaWorld", location2D3.X, location2D3.Y, 10);
				gameObject = The.ZoneManager.GetZone(WorldID).GetCell(location2D3.X / 3, location2D3.Y / 3).Objects[0];
				for (int i = -1; i < 2; i++)
				{
					for (int j = -1; j < 2; j++)
					{
						string text4 = Zone.XYToID("JoppaWorld", location2D3.X + i, location2D3.Y + j, 10);
						The.ZoneManager.AddZonePostBuilder(text4, "DenseBrinestalk");
					}
				}
				The.ZoneManager.AddZonePostBuilder(text3, "AddBlueprintBuilder", "Object", "OasisGlowpad");
				if (Options.ShowOverlandEncounters)
				{
					gameObject.Render.RenderString = "&W$";
				}
				this.AddSecret(text3, "a secluded merchant from the Consortium of Phyta", new string[] { "encounter", "special", "oddity", "consortium" }, "Oddities", "$glowpadmerchant", false, false);
				Location2D location2D4 = this.mutableMap.popMutableLocationInArea(0, 0, 119, 74);
				int num = location2D4.X / 3;
				int num2 = location2D4.Y / 3;
				string text5 = Zone.XYToID("JoppaWorld", location2D4.X, location2D4.Y, 10);
				The.ZoneManager.AdjustZoneGenerationTierTo(text5);
				The.Game.SetIntGameState("RuinofHouseIsner_xCoordinate", num);
				The.Game.SetIntGameState("RuinofHouseIsner_yCoordinate", num2);
				Event.ResetPool(true);
				The.ZoneManager.AddZonePostBuilder(text5, "AddBlueprintBuilder", "Object", "ChestIsner");
				if (Options.ShowOverlandEncounters)
				{
					The.ZoneManager.GetZone(WorldID).GetCell(num, num2).Objects[0].Render.RenderString = "&M*";
				}
				this.AddSecret(text5, "the Ruin of House Isner", new string[] { "artifact", "special", "tech", "saltmarsh", "saltdunes", "desertcanyons", "flowerfields" }, "Artifacts", "$ruinofhouseisner", false, false);
				Location2D location2D5 = this.popMutableLocationOfTerrain("LakeHinnom", null, true);
				string text6 = Zone.XYToID("JoppaWorld", location2D5.X, location2D5.Y, 10);
				The.ZoneManager.AdjustZoneGenerationTierTo(text6);
				The.Game.SetStringGameState("Recorporealization_ZoneID", text6);
				The.ZoneManager.AddZonePostBuilder(text6, "AddWidgetBuilder", "Blueprint", "RecorporealizationBoothSpawner");
				The.ZoneManager.SetZoneName(text6, "Gyl", null, null, null, null, true, true);
				this.AddSecret(text6, "Recoming nook at Gyl", new string[] { "oddity", "ruins" }, "Oddities", "$recomingnook", false, false);
				this.AddLocationFinder(text6, "$recomingnook", 0, 5000);
				this.WorldZone.GetCell(location2D5.X / 3, location2D5.Y / 3).GetFirstObjectWithPart("TerrainTravel").GetPart<TerrainTravel>()
					.AddEncounter(new EncounterEntry("You notice some ruins nearby. Would you like to investigate?", text6, "", "$recomingnook", true));
				this.AddHamilcrabSecret();
			}
		}

		// Token: 0x06006A5A RID: 27226 RVA: 0x0021EF9C File Offset: 0x0021D19C
		public void BuildSecrets(string WorldID)
		{
			if (WorldID == "JoppaWorld")
			{
				Location2D location2D;
				string text;
				for (int i = 0; i < 32; i++)
				{
					location2D = this.mutableMap.popMutableLocationInArea(0, 0, 119, 74);
					text = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, 10);
					The.ZoneManager.AdjustZoneGenerationTierTo(text);
					this.AddSecret(text, "a {{w|dromad}} caravan", new string[] { "dromad", "merchant" }, "Merchants", "DromadMerchant_" + text, false, false);
					The.ZoneManager.AddZonePostBuilder(text, "AddBlueprintBuilder", "Object", "DromadTrader1");
				}
				location2D = this.getLocationOfTier(4, 6);
				location2D = Location2D.Get(location2D.X * 3 + 1, location2D.Y * 3 + 1);
				text = Zone.XYToID("JoppaWorld", location2D.X, location2D.Y, Stat.Random(10, 19));
				The.ZoneManager.AdjustZoneGenerationTierTo(text);
				The.ZoneManager.AddZonePostBuilder(text, "PlaceRelicBuilder", "Relic", The.ZoneManager.CacheObject(GameObject.Create("Kindrish"), false, false));
				this.AddSecret(text, "Kindrish, the ancestral bracelet of the hindren", new string[] { "artifact", "kindrish" }, "Artifacts", "~kindrish", false, false);
				this.AddGreatMachineArtifact();
				foreach (KeyValuePair<string, IBiome> keyValuePair in BiomeManager.Biomes)
				{
					List<Point2D> list = new List<Point2D>();
					for (int j = 0; j < 240; j++)
					{
						for (int k = 0; k < 75; k++)
						{
							text = Zone.XYToID("JoppaWorld", j, k, 10);
							if (The.ZoneManager.CheckBiomesAllowed(text) && keyValuePair.Value.IsNotable(text))
							{
								list.Add(new Point2D(j, k));
							}
						}
					}
					foreach (Point2D point2D in list)
					{
						text = Zone.XYToID("JoppaWorld", point2D.x, point2D.y, 10);
						string text2;
						if (string.Equals(keyValuePair.Key, "slimy", StringComparison.CurrentCultureIgnoreCase))
						{
							text2 = "a {{g|slime}} bog";
						}
						else if (string.Equals(keyValuePair.Key, "tarry", StringComparison.CurrentCultureIgnoreCase))
						{
							text2 = "some {{fiery|flaming}} {{K|tar}} pits";
						}
						else if (string.Equals(keyValuePair.Key, "rusty", StringComparison.CurrentCultureIgnoreCase))
						{
							text2 = "a {{rusty|rust}} bog";
						}
						else if (string.Equals(keyValuePair.Key, "fungal", StringComparison.CurrentCultureIgnoreCase))
						{
							text2 = "a {{m|fungus}} forest";
						}
						else
						{
							text2 = Grammar.A(keyValuePair.Key, false) + " region";
						}
						string text3 = this.AddSecret(text, text2, new string[]
						{
							"biome",
							keyValuePair.Key.ToLower()
						}, "Natural Features", "Biome_" + text, false, false);
						this.AddLocationFinder(text, text3, 0, 5000);
					}
				}
				List<Point3D> list2 = new List<Point3D>(800);
				for (int l = 0; l < 240; l++)
				{
					for (int m = 0; m < 75; m++)
					{
						for (int n = 10; n <= 29; n++)
						{
							int num;
							if (FungalBiome.BiomeLevels == null)
							{
								num = BiomeManager.BiomeValue("Fungal", Zone.XYToID("JoppaWorld", l, m, n));
							}
							else
							{
								num = (int)FungalBiome.BiomeLevels[l, m, n % 10];
							}
							if (num >= 1)
							{
								if (n == 10)
								{
									list2.Add(new Point3D(l, m, n));
								}
								if (n > 10 && 5.in100())
								{
									list2.Add(new Point3D(l, m, n));
								}
							}
						}
					}
				}
				string[] array = new string[]
				{
					"waterLichen Minor", "honeyLichen Minor", "lavaLichen Minor", "acidLichen Minor", "wineLichen Minor", "slimeLichen Minor", "ciderLichen Minor", "gelLichen Minor", "asphaltLichen Minor", "saltLichen Minor",
					"oilLichen Minor", "sapLichen Minor", "waxLichen Minor", "inkLichen Minor"
				};
				list2.ShuffleInPlace<Point3D>(null);
				int num2 = 0;
				foreach (string text4 in array)
				{
					int num4 = Stat.Random(3, 4);
					int num5 = 0;
					while (num5 < num4 && num2 < list2.Count)
					{
						text = Zone.XYToID("JoppaWorld", list2[num2].x, list2[num2].y, list2[num2].z);
						The.ZoneManager.AdjustZoneGenerationTierTo(text);
						GameObject gameObject = GameObjectFactory.Factory.CreateObject(text4);
						string text5 = "surface";
						if (list2[num2].z > 10)
						{
							text5 = "underground";
						}
						The.ZoneManager.AddZonePostBuilder(text, "AddObjectBuilder", "Object", The.ZoneManager.CacheObject(gameObject, false, false));
						this.AddSecret(text, gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, false, false, false, true, false, null, false, null, false), new string[]
						{
							"weep",
							text5,
							gameObject.GetPart<LiquidFont>().Liquid
						}, "Natural Features", gameObject.GetPart<SecretObject>().id, false, false);
						num2++;
						num5++;
					}
				}
			}
		}

		// Token: 0x06006A5B RID: 27227 RVA: 0x0021F584 File Offset: 0x0021D784
		public void BuildLairs(string WorldID)
		{
			if (WorldID != "JoppaWorld")
			{
				return;
			}
			MetricsManager.rngCheckpoint("%LAIRS 1");
			this.Lairs = new int[240, 75];
			List<Location2D> list = new List<Location2D>();
			for (int i = 0; i < 240; i++)
			{
				for (int j = 0; j < 75; j++)
				{
					if (this.mutableMap.GetMutable(i, j) > 0 && ((this.RoadSystem[i, j] & JoppaWorldBuilder.ROAD_START) > 0U || (this.RiverSystem[i, j] & this.RIVER_START) > 0U))
					{
						list.Add(Location2D.Get(i, j));
					}
				}
			}
			int num = 125;
			int num2 = 300;
			int num3 = num - list.Count + num2;
			MetricsManager.rngCheckpoint("%LAIRS 2");
			for (int k = 0; k < num3; k++)
			{
				list.Add(this.mutableMap.popMutableLocation());
			}
			MetricsManager.rngCheckpoint("%LAIRS 3");
			Coach.StartSection("Generate Lairs", false);
			list.ShuffleInPlace<Location2D>(null);
			MetricsManager.rngCheckpoint("%LAIRS 4");
			Event.PinCurrentPool();
			for (int l = 0; l < num; l++)
			{
				Event.ResetPool(true);
				try
				{
					if (this.AddLairAt(list[l], l))
					{
						this.mutableMap.SetMutable(list[l], 0);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException(string.Format("AddLairAt::{0}", list[l]), ex, "game_exception");
				}
				Event.ResetToPin();
			}
			Coach.EndSection();
			MetricsManager.rngCheckpoint("%LAIRS 5");
		}

		// Token: 0x06006A5C RID: 27228 RVA: 0x0021F728 File Offset: 0x0021D928
		public void PlaceClams()
		{
			MapFile mapFile = MapFile.Resolve("Tzimtzlum", true);
			BallBag<string> ballBag = new BallBag<string>(0);
			ballBag.Add("reef_surface", 15);
			ballBag.Add("reef_cave", 5);
			ballBag.Add("lake_surface", 7);
			ballBag.Add("lake_cave", 3);
			ClamSystem clamSystem = The.Game.RequireSystem<ClamSystem>(() => new ClamSystem());
			foreach (MapFileCellReference mapFileCellReference in mapFile.Cells.AllCells(null))
			{
				if (mapFileCellReference.cell.Objects.Any<MapFileObjectBlueprint>((MapFileObjectBlueprint o) => o.Name == "Giant Clam"))
				{
					string text = ballBag.PeekOne();
					string text2 = null;
					if (text == "reef_surface")
					{
						text2 = this.GetZoneIdOfTerrain("PalladiumReef", "10");
					}
					else if (text == "reef_cave")
					{
						text2 = this.GetZoneIdOfTerrain("PalladiumReef", Stat.Random(11, 15).ToString());
					}
					else if (text == "lake_surface")
					{
						text2 = this.GetZoneIdOfTerrain("LakeHinnom", "10");
					}
					else if (text == "lake_cave")
					{
						text2 = this.GetZoneIdOfTerrain("LakeHinnom", Stat.Random(11, 15).ToString());
					}
					The.Game.ZoneManager.AddZonePostBuilder(text2, "PlaceAClam", "clamNumber", clamSystem.clamJoppaZone.Count);
					clamSystem.clamJoppaZone.Add(text2);
				}
			}
			clamSystem.clamJoppaZone.Add("JoppaWorld.67.17.1.1.10");
		}

		// Token: 0x06006A5D RID: 27229 RVA: 0x0021F914 File Offset: 0x0021DB14
		public void AddLocationFinder(string ZoneID, string SecretID, int Value = 0, int Priority = 5000)
		{
			if (Value > 0)
			{
				The.ZoneManager.AddZoneBuilder(ZoneID, Priority, "AddLocationFinder", "SecretID", SecretID, "Value", Value);
				return;
			}
			The.ZoneManager.AddZoneBuilder(ZoneID, Priority, "AddLocationFinder", "SecretID", SecretID);
		}

		// Token: 0x06006A5E RID: 27230 RVA: 0x0021F964 File Offset: 0x0021DB64
		public static GameObject GenerateLairOwner(string TerrainBlueprint, int ZoneTier, int GenericChance = 10)
		{
			string tag = GameObjectFactory.Factory.Blueprints[TerrainBlueprint].GetTag("LairOwnerTable", "GenericLairOwner");
			Dictionary<string, string> dictionary = new Dictionary<string, string> { 
			{
				"zonetier",
				ZoneTier.ToString()
			} };
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(PopulationManager.RollOneFrom(GenericChance.in100() ? "GenericLairOwner" : tag, dictionary, null).Blueprint);
			gameObject.SetIntProperty("LairOwner", 1, false);
			gameObject.SetStringProperty("Role", "Hero", false);
			gameObject.RemovePart<AIPilgrim>();
			if (!gameObject.HasPart<GivesRep>())
			{
				gameObject = HeroMaker.MakeHero(gameObject, Array.Empty<string>(), Array.Empty<string>(), ZoneTier, "Lair");
				if (gameObject == null)
				{
					return null;
				}
			}
			if (!gameObject.HasTag("Merchant"))
			{
				if (gameObject.HasStat("Strength"))
				{
					gameObject.GetStat("Strength").BoostStat(1);
				}
				if (gameObject.HasStat("Intelligence"))
				{
					gameObject.GetStat("Intelligence").BoostStat(1);
				}
				if (gameObject.HasStat("Toughness"))
				{
					gameObject.GetStat("Toughness").BoostStat(1);
				}
				if (gameObject.HasStat("Willpower"))
				{
					gameObject.GetStat("Willpower").BoostStat(1);
				}
				if (gameObject.HasStat("Ego"))
				{
					gameObject.GetStat("Ego").BoostStat(1);
				}
				if (gameObject.HasStat("Agility"))
				{
					gameObject.GetStat("Agility").BoostStat(1);
				}
				if (gameObject.HasStat("Hitpoints"))
				{
					gameObject.GetStat("Hitpoints").BaseValue *= 2;
				}
			}
			else
			{
				if (gameObject.HasStat("Willpower"))
				{
					gameObject.GetStat("Willpower").BoostStat(1);
				}
				if (gameObject.HasStat("Hitpoints"))
				{
					gameObject.GetStat("Hitpoints").BaseValue = Math.Max(50 * ZoneTier, gameObject.GetStat("Hitpoints").BaseValue);
				}
				if (gameObject.HasStat("Level"))
				{
					gameObject.GetStat("Level").BaseValue = Math.Max(ZoneTier * 5, gameObject.GetStat("Level").BaseValue);
				}
				if (gameObject.HasStat("XP") && gameObject.HasStat("Level"))
				{
					gameObject.GetStat("XP").BaseValue = Leveler.GetXPForLevel(gameObject.GetStat("Level").Value);
				}
				if (gameObject.HasStat("XPValue") && gameObject.HasStat("Level"))
				{
					float num = (float)gameObject.GetStat("Level").Value;
					num /= 2f;
					gameObject.GetStat("XPValue").BaseValue = (int)(num * 200f);
				}
			}
			if (gameObject.HasPart<Inventory>() && gameObject.HasTag("LairInventory"))
			{
				if (gameObject.HasTag("LairAddMakersMark"))
				{
					HasMakersMark hasMakersMark = gameObject.RequirePart<HasMakersMark>(false);
					if (hasMakersMark.Mark == null)
					{
						hasMakersMark.Mark = MakersMark.Generate(true);
					}
					gameObject.EquipFromPopulationTable(gameObject.GetTag("LairInventory", null), gameObject.GetTier(), GenericInventoryRestocker.GetCraftmarkApplication(gameObject), null, false, true);
				}
				else
				{
					gameObject.EquipFromPopulationTable(gameObject.GetTag("LairInventory", null), ZoneTier, null, null, false, true);
				}
			}
			return gameObject;
		}

		// Token: 0x06006A5F RID: 27231 RVA: 0x0021FC9C File Offset: 0x0021DE9C
		public bool AddLairAt(Location2D pos, int nLair)
		{
			MetricsManager.rngCheckpoint("LAIR " + nLair.ToString() + " start");
			string text = Zone.XYToID("JoppaWorld", pos.X, pos.Y, 10);
			int num = 0;
			if (25.in100())
			{
				num += Stat.Random(1, 25);
			}
			string text2 = Zone.XYToID("JoppaWorld", pos.X, pos.Y, 10 + num);
			string text3 = null;
			int zoneTier = The.ZoneManager.GetZoneTier(text);
			string objectTypeForZone = ZoneManager.GetObjectTypeForZone(pos.X / 3, pos.Y / 3, "JoppaWorld");
			GeneratedLocationInfo generatedLocationInfo = new GeneratedLocationInfo();
			GameObject gameObject = JoppaWorldBuilder.GenerateLairOwner(objectTypeForZone, zoneTier, 10);
			if (gameObject == null)
			{
				MetricsManager.LogError(string.Concat(new string[] { "AddLairAt: Couldn't get a lair monster for ", text2, " in a ", objectTypeForZone, "." }));
				return false;
			}
			if (gameObject.HasTag("LairMinionsInherit"))
			{
				text3 = "DynamicInheritsTable:" + gameObject.GetTag("LairMinionsInherit", null) + ":Tier" + The.ZoneManager.GetZoneTier(text).ToString();
			}
			else if (gameObject.HasTag("LairMinions"))
			{
				text3 = gameObject.GetTag("LairMinions", null);
			}
			else
			{
				string text4 = gameObject.GetBlueprint(true).Inherits;
				string text5 = text4;
				while (text5 != null && ((!text5.StartsWith("Base") && !GameObjectFactory.Factory.Blueprints[text5].Tags.ContainsKey("BaseObject")) || GameObjectFactory.Factory.Blueprints[text5].Tags.ContainsKey("SkipAsLairBaseCreatureType")))
				{
					text5 = GameObjectFactory.Factory.Blueprints[text5].Inherits;
				}
				if (text5 != null)
				{
					text4 = text5;
				}
				if (text3 == null)
				{
					text3 = "DynamicInheritsTable:" + text4 + ":Tier" + The.ZoneManager.GetZoneTier(text).ToString();
				}
			}
			int num2 = gameObject.Stat("Level", 0) * 75;
			int num3 = gameObject.Stat("Level", 0) / 5 + 1;
			int num4 = num3 - 2;
			if (num3 < 2)
			{
				num3 = 2;
			}
			if (num3 > 8)
			{
				num3 = 8;
			}
			if (num4 < 1)
			{
				num4 = 1;
			}
			if (num4 > 8)
			{
			}
			string text6 = Guid.NewGuid().ToString();
			gameObject.SetStringProperty("nosecret", text6, false);
			string text7 = gameObject.GetPropertyOrTag("LairAdjectives", "");
			if (text7.Length > 0)
			{
				text7 += ",";
			}
			text7 += GameObjectFactory.Factory.Blueprints[objectTypeForZone].GetTag("LairAdjectives", "lair");
			GameObjectBlueprint blueprint = gameObject.GetBlueprint(true);
			string referenceDisplayName = gameObject.GetReferenceDisplayName(int.MaxValue, null, "LairName", false, false, false, false, false, false, false, false, null, false);
			string text8 = Grammar.MakePossessive(referenceDisplayName);
			string text9 = referenceDisplayName.Strip();
			string text10 = text8.Strip();
			string tag = gameObject.GetTag("LairDepth", "2-4");
			string text11 = "the %l of %o".Replace("%l", blueprint.GetTag("LairName", "lair")).Replace("%o", referenceDisplayName).Replace("%p", text8);
			string propertyOrTag = gameObject.GetPropertyOrTag("LairAmbientBed", null);
			int num5 = tag.RollCached();
			MetricsManager.rngCheckpoint("LAIR " + nLair.ToString() + " lairstats");
			for (int i = 0; i < num5; i++)
			{
				string text12 = i.ToString();
				string text13 = Zone.XYToID("JoppaWorld", pos.X, pos.Y, 10 + i);
				if (!propertyOrTag.IsNullOrEmpty())
				{
					The.ZoneManager.SetZoneProperty(text13, "ambient_bed_2", propertyOrTag);
				}
				string text14 = blueprint.GetTag("LairNameContext", "the lair of %o");
				bool flag = false;
				string text15;
				if (i == num5 - 1)
				{
					text15 = blueprint.GetTag("LairLevelNameFinal", "");
					if (!text15.IsNullOrEmpty())
					{
						flag = true;
					}
				}
				else if (i == 0)
				{
					text15 = blueprint.GetTag("LairLevelNameSurface", "");
				}
				else
				{
					text15 = blueprint.GetTag("LairLevelNameSubsurface", "");
				}
				if (text15 != null)
				{
					text15 = text15.Replace("%o", text9).Replace("%n", text12).Replace("%p", text10);
				}
				text14 = text14.Replace("%o", text9).Replace("%n", text12).Replace("%p", text10);
				The.ZoneManager.SetZoneName(text13, text15, text14, null, null, null, flag, true);
				if (i == 0)
				{
					The.ZoneManager.AddZonePostBuilder(text13, "BasicLair", "Table", text3, "Adjectives", text7, "Stairs", "D");
				}
				else if (i == num5 - 1)
				{
					The.ZoneManager.ClearZoneBuilders(text13);
					The.ZoneManager.SetZoneProperty(text13, "SkipTerrainBuilders", true);
					The.ZoneManager.AddZonePostBuilder(text13, "BasicLair", "Table", text3, "Adjectives", text7, "Stairs", "U");
					The.ZoneManager.AddZonePostBuilder(text13, "AddObjectBuilder", "Object", The.ZoneManager.CacheObject(gameObject, false, false));
					The.ZoneManager.AddZonePostBuilder(text13, "AddBlueprintBuilder", "Object", "Chest" + num3.ToString());
				}
				else
				{
					The.ZoneManager.ClearZoneBuilders(text13);
					The.ZoneManager.SetZoneProperty(text13, "SkipTerrainBuilders", true);
					The.ZoneManager.AddZonePostBuilder(text13, "BasicLair", "Table", text3, "Adjectives", text7, "Stairs", "UD");
				}
				this.AddLocationFinder(text13, text6, num2, 5000);
			}
			MetricsManager.rngCheckpoint("LAIR " + nLair.ToString() + " levelgen");
			string text16 = Zone.XYToID("JoppaWorld", pos.X, pos.Y, 10);
			List<string> list = new List<string>();
			string tag2 = gameObject.GetTag("LairCategory", "Lairs");
			string tag3 = gameObject.GetTag("LairName", "lair");
			if (tag2 == "Lairs")
			{
				list.Add("lair");
			}
			if (gameObject.HasTag("LairAdjectives"))
			{
				list.AddRange(gameObject.GetTag("LairAdjectives", null).Split(',', StringSplitOptions.None));
			}
			if (gameObject.HasTag("SecretAdjectives"))
			{
				list.AddRange(gameObject.GetTag("SecretAdjectives", null).Split(',', StringSplitOptions.None));
			}
			list.Add(gameObject.GetPropertyOrTag("Species", null) ?? gameObject.GetBlueprint(true).GetBaseTypeName().ToLower());
			string propertyOrTag2 = gameObject.GetPropertyOrTag("Class", null);
			if (!propertyOrTag2.IsNullOrEmpty())
			{
				list.Add(propertyOrTag2);
			}
			this.AddSecret(text16, "the " + tag3 + " of " + gameObject.GetReferenceDisplayName(int.MaxValue, null, "LairName", false, false, false, false, false, false, false, false, null, false), list.ToArray(), tag2, text6, false, false);
			MetricsManager.rngCheckpoint("LAIR " + nLair.ToString() + " secretgen");
			Coach.EndSection();
			Coach.StartSection("Add pulldowns", false);
			Zone zone = The.ZoneManager.GetZone("JoppaWorld");
			TerrainTravel terrainTravel = null;
			Render render = null;
			GameObject firstObjectWithPart = zone.GetCell(pos.X / 3, pos.Y / 3).GetFirstObjectWithPart("TerrainTravel");
			if (firstObjectWithPart != null)
			{
				terrainTravel = firstObjectWithPart.GetPart<TerrainTravel>();
				render = firstObjectWithPart.GetPart<Render>();
			}
			if (Options.ShowOverlandEncounters && render != null)
			{
				render.RenderString = "&W*";
				render.ParentObject.SetStringProperty("OverlayColor", "&M", false);
			}
			if (terrainTravel != null)
			{
				terrainTravel.AddEncounter(new EncounterEntry(blueprint.GetTag("LairPulldownMessage", "You discover a lair. Would you like to investigate?"), text, "", text6, true));
			}
			generatedLocationInfo.targetZone = Zone.XYToID("JoppaWorld", pos.X, pos.Y, 10 + num5 - 1);
			generatedLocationInfo.zoneLocation = pos;
			generatedLocationInfo.name = text11;
			generatedLocationInfo.ownerID = gameObject.ID;
			generatedLocationInfo.secretID = text6;
			this.worldInfo.lairs.Add(generatedLocationInfo);
			MetricsManager.rngCheckpoint("LAIR " + nLair.ToString() + " travelgen");
			Coach.EndSection();
			return true;
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06006A60 RID: 27232 RVA: 0x0022052D File Offset: 0x0021E72D
		// (set) Token: 0x06006A61 RID: 27233 RVA: 0x0022053A File Offset: 0x0021E73A
		public uint[,] RoadSystem
		{
			get
			{
				return this.worldInfo.RoadSystem;
			}
			set
			{
				this.worldInfo.RoadSystem = value;
			}
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x00220548 File Offset: 0x0021E748
		public Location2D GetRoadHead()
		{
			int num = Stat.Random(0, 239);
			int num2 = Stat.Random(0, 74);
			while (this.RoadSystem[num, num2] != 0U)
			{
				num = Stat.Random(0, 239);
				num2 = Stat.Random(0, 74);
			}
			return Location2D.Get(num, num2);
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x00220598 File Offset: 0x0021E798
		public void BuildCanyonSystems(string WorldID)
		{
			if (WorldID == "JoppaWorld")
			{
				Zone zone = The.ZoneManager.GetZone("JoppaWorld");
				Maze maze = The.Game.WorldMazes["QudCanyonMaze"];
				for (int i = 0; i < 240; i++)
				{
					for (int j = 0; j < 75; j++)
					{
						string text = Zone.XYToID("JoppaWorld", i, j, 10);
						int num = i / 3;
						int num2 = j / 3;
						GameObject firstObjectWithPart = zone.GetCell(num, num2).GetFirstObjectWithPart("TerrainTravel");
						if (firstObjectWithPart != null && (firstObjectWithPart.Blueprint.Contains("TerrainJoppaRedrockChannel") || firstObjectWithPart.Blueprint.Contains("Canyon") || firstObjectWithPart.Blueprint.Contains("Hills") || firstObjectWithPart.Blueprint.Contains("Asphalt") || firstObjectWithPart.Blueprint.Contains("TerrainRustedArchway") || firstObjectWithPart.Blueprint.Contains("TerrainBethesdaSusa")))
						{
							MazeCell mazeCell = maze.Cell[i, j];
							The.ZoneManager.AddZoneMidBuilder(text, "CanyonStartMouth");
							if (mazeCell.N)
							{
								The.ZoneManager.AddZoneMidBuilder(text, "CanyonNorthMouth");
							}
							if (mazeCell.S)
							{
								The.ZoneManager.AddZoneMidBuilder(text, "CanyonSouthMouth");
							}
							if (mazeCell.E)
							{
								The.ZoneManager.AddZoneMidBuilder(text, "CanyonEastMouth");
							}
							if (mazeCell.W)
							{
								The.ZoneManager.AddZoneMidBuilder(text, "CanyonWestMouth");
							}
							The.ZoneManager.AddZoneMidBuilder(text, "CanyonBuilder");
							The.ZoneManager.AddZonePostBuilder(text, "CanyonReacher");
						}
					}
				}
			}
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x00220754 File Offset: 0x0021E954
		public void BuildRoadSystems(string WorldID)
		{
			this.RoadSystem = new uint[240, 75];
			for (int i = 0; i < 240; i++)
			{
				for (int j = 0; j < 75; j++)
				{
					if (this.mutableMap.GetMutable(i, j) == 1)
					{
						this.RoadSystem[i, j] = 0U;
					}
					else
					{
						this.RoadSystem[i, j] = JoppaWorldBuilder.ROAD_NONE;
					}
				}
			}
			if (WorldID == "JoppaWorld")
			{
				for (int k = 0; k < 160; k++)
				{
					Location2D roadHead = this.GetRoadHead();
					this.BuildRoad(WorldID, roadHead.X, roadHead.Y);
				}
			}
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x002207FC File Offset: 0x0021E9FC
		public void BuildRoad(string WorldID, int StartX, int StartY)
		{
			int num = Stat.Random(0, 3);
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RoadStartMouth");
			this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_START;
			if (num == 0)
			{
				this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_NORTH;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RoadNorthMouth");
			}
			if (num == 1)
			{
				this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_EAST;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RoadEastMouth");
			}
			if (num == 2)
			{
				this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_SOUTH;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RoadSouthMouth");
			}
			if (num == 3)
			{
				this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_WEST;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RoadWestMouth");
			}
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RoadBuilder");
			this.ContinueRoad(StartX, StartY, num, 0);
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x00220928 File Offset: 0x0021EB28
		public void ContinueRoad(int StartX, int StartY, int Direction, int Depth)
		{
			int num = StartX;
			int num2 = StartY;
			if (Direction == 0)
			{
				num2--;
			}
			if (Direction == 2)
			{
				num2++;
			}
			if (Direction == 1)
			{
				num++;
			}
			if (Direction == 3)
			{
				num--;
			}
			if (num < 0 || num2 < 0 || num >= 240 || num2 >= 75)
			{
				return;
			}
			if (Stat.Random(0, 100) < 2 + Depth * 5)
			{
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_START;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadStartMouth");
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadBuilder");
				return;
			}
			int num3 = Direction;
			if (50.in100())
			{
				int num4 = Stat.Random(0, 1);
				if (num4 == 0)
				{
					num3--;
				}
				if (num4 == 1)
				{
					num3++;
				}
				if (num3 < 0)
				{
					num3 = 3;
				}
				if (num3 > 3)
				{
					num3 = 0;
				}
			}
			if (Direction == 0)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadSouthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_SOUTH;
			}
			if (Direction == 1)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadWestMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_WEST;
			}
			if (Direction == 2)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadNorthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_NORTH;
			}
			if (Direction == 3)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadEastMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_EAST;
			}
			int num5 = num;
			int num6 = num2;
			if (num3 == 0)
			{
				num6--;
			}
			if (num3 == 2)
			{
				num6++;
			}
			if (num3 == 1)
			{
				num5++;
			}
			if (num3 == 3)
			{
				num5--;
			}
			if (num6 < 0 || num5 < 0 || num6 == 75 || num5 == 240 || this.RoadSystem[num5, num6] != 0U)
			{
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_START;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadStartMouth");
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadBuilder");
				return;
			}
			if (num3 == 0)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadNorthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_NORTH;
			}
			if (num3 == 1)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadEastMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_EAST;
			}
			if (num3 == 2)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadSouthMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_SOUTH;
			}
			if (num3 == 3)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadWestMouth");
				this.RoadSystem[num, num2] |= JoppaWorldBuilder.ROAD_WEST;
			}
			if (Stat.Random(0, 100) < 5)
			{
				int num7 = Direction;
				if (num7 == num3)
				{
					int num8 = Stat.Random(0, 1);
					if (num8 == 0)
					{
						num7--;
					}
					if (num8 == 1)
					{
						num7++;
					}
					if (num7 < 0)
					{
						num7 = 3;
					}
					if (num7 > 3)
					{
						num7 = 0;
					}
				}
				if (num7 == 0)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RoadNorthMouth");
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_NORTH;
				}
				if (num7 == 1)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RoadEastMouth");
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_EAST;
				}
				if (num7 == 2)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RoadSouthMouth");
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_SOUTH;
				}
				if (num7 == 3)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RoadWestMouth");
					this.RoadSystem[StartX, StartY] |= JoppaWorldBuilder.ROAD_WEST;
				}
				this.ContinueRoad(num, num2, num7, Depth + 1);
			}
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RoadBuilder");
			if (Options.ShowOverlandEncounters)
			{
				The.ZoneManager.GetZone("JoppaWorld").GetCell(num / 3, num2 / 3).GetObjectInCell(0)
					.Render.RenderString = ".";
			}
			this.ContinueRoad(num, num2, num3, Depth);
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06006A67 RID: 27239 RVA: 0x00220DCA File Offset: 0x0021EFCA
		// (set) Token: 0x06006A68 RID: 27240 RVA: 0x00220DD7 File Offset: 0x0021EFD7
		public uint[,] RiverSystem
		{
			get
			{
				return this.worldInfo.RiverSystem;
			}
			set
			{
				this.worldInfo.RiverSystem = value;
			}
		}

		// Token: 0x06006A69 RID: 27241 RVA: 0x00220DE8 File Offset: 0x0021EFE8
		public Location2D GetRiverHead()
		{
			int num = Stat.Random(0, 239);
			int num2 = Stat.Random(0, 74);
			string[] array = new string[] { "TerrainWater", "TerrainSaltmarsh", "TerrainSaltmarsh2" };
			while (this.RiverSystem[num, num2] != 0U || !this.WorldCellHasTerrain("JoppaWorld", num / 3, num2 / 3, array))
			{
				num = Stat.Random(0, 239);
				num2 = Stat.Random(0, 74);
			}
			return Location2D.Get(num, num2);
		}

		// Token: 0x06006A6A RID: 27242 RVA: 0x00220E6C File Offset: 0x0021F06C
		public void BuildRiverSystems(string WorldID)
		{
			for (int i = 0; i < 240; i++)
			{
				for (int j = 0; j < 75; j++)
				{
					if (this.mutableMap.GetMutable(i, j) == 1)
					{
						this.RiverSystem[i, j] = 0U;
					}
					else
					{
						this.RiverSystem[i, j] = this.RIVER_NONE;
					}
				}
			}
			if (WorldID == "JoppaWorld")
			{
				for (int k = 0; k < 160; k++)
				{
					Location2D riverHead = this.GetRiverHead();
					this.BuildRiver(WorldID, riverHead.X, riverHead.Y);
				}
			}
		}

		// Token: 0x06006A6B RID: 27243 RVA: 0x00220F00 File Offset: 0x0021F100
		public void BuildRiver(string WorldID, int StartX, int StartY)
		{
			int num = Stat.Random(0, 3);
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverStartMouth");
			this.RiverSystem[StartX, StartY] |= this.RIVER_START;
			if (num == 0)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverNorthMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_NORTH;
			}
			if (num == 1)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverEastMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_EAST;
			}
			if (num == 2)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverSouthMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_SOUTH;
			}
			if (num == 3)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverWestMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_WEST;
			}
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverBuilder");
			this.ContinueRiver(StartX, StartY, num, 0);
		}

		// Token: 0x06006A6C RID: 27244 RVA: 0x00221034 File Offset: 0x0021F234
		public List<Location2D> BuildMamonRiver(string WorldID, int StartX, int StartY, int Direction, int Bias, int MinimumLength, int ExcludeDirection)
		{
			The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverStartMouth");
			this.RiverSystem[StartX, StartY] |= this.RIVER_START;
			if (Direction == 0)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverNorthMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_NORTH;
			}
			if (Direction == 1)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverEastMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_EAST;
			}
			if (Direction == 2)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverSouthMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_SOUTH;
			}
			if (Direction == 3)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverWestMouth");
				this.RiverSystem[StartX, StartY] |= this.RIVER_WEST;
			}
			The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY(WorldID, StartX, StartY), "RiverBuilder");
			List<Location2D> list = new List<Location2D>();
			this.ContinueMamonRiver(StartX, StartY, Direction, 0, Bias, MinimumLength, ExcludeDirection, list);
			return list;
		}

		// Token: 0x06006A6D RID: 27245 RVA: 0x00221170 File Offset: 0x0021F370
		public void ContinueRiver(int StartX, int StartY, int Direction, int Depth)
		{
			int num = StartX;
			int num2 = StartY;
			if (Direction == 0)
			{
				num2--;
			}
			if (Direction == 2)
			{
				num2++;
			}
			if (Direction == 1)
			{
				num++;
			}
			if (Direction == 3)
			{
				num--;
			}
			if (num < 0 || num2 < 0 || num >= 240 || num2 >= 75)
			{
				return;
			}
			if (Stat.Random(0, 100) < Depth - 8)
			{
				this.RiverSystem[num, num2] |= this.RIVER_START;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverStartMouth");
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverBuilder");
				return;
			}
			int num3 = Direction;
			if (Stat.Random(1, 100) < 20)
			{
				int num4 = Stat.Random(0, 1);
				if (num4 == 0)
				{
					num3--;
				}
				if (num4 == 1)
				{
					num3++;
				}
				if (num3 < 0)
				{
					num3 = 3;
				}
				if (num3 > 3)
				{
					num3 = 0;
				}
			}
			if (Direction == 0)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverSouthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_SOUTH;
			}
			if (Direction == 1)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverWestMouth");
				this.RiverSystem[num, num2] |= this.RIVER_WEST;
			}
			if (Direction == 2)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverNorthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_NORTH;
			}
			if (Direction == 3)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverEastMouth");
				this.RiverSystem[num, num2] |= this.RIVER_EAST;
			}
			int num5 = num;
			int num6 = num2;
			if (num3 == 0)
			{
				num6--;
			}
			if (num3 == 2)
			{
				num6++;
			}
			if (num3 == 1)
			{
				num5++;
			}
			if (num3 == 3)
			{
				num5--;
			}
			if (num6 < 0 || num5 < 0 || num6 >= 75 || num5 >= 240 || this.RiverSystem[num5, num6] != 0U)
			{
				this.RiverSystem[num, num2] |= this.RIVER_START;
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverStartMouth");
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverBuilder");
				return;
			}
			if (num3 == 0)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverNorthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_NORTH;
			}
			if (num3 == 1)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverEastMouth");
				this.RiverSystem[num, num2] |= this.RIVER_EAST;
			}
			if (num3 == 2)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverSouthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_SOUTH;
			}
			if (num3 == 3)
			{
				The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverWestMouth");
				this.RiverSystem[num, num2] |= this.RIVER_WEST;
			}
			bool flag = false;
			if (5.in100())
			{
				flag = true;
				int num7 = Direction;
				if (num7 == num3)
				{
					int num8 = Stat.Random(0, 1);
					if (num8 == 0)
					{
						num7--;
					}
					if (num8 == 1)
					{
						num7++;
					}
					if (num7 < 0)
					{
						num7 = 3;
					}
					if (num7 > 3)
					{
						num7 = 0;
					}
				}
				if (num7 == 0)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RiverNorthMouth");
				}
				if (num7 == 1)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RiverEastMouth");
				}
				if (num7 == 2)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RiverSouthMouth");
				}
				if (num7 == 3)
				{
					The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", StartX, StartY), "RiverWestMouth");
				}
				this.ContinueRiver(num, num2, num7, Depth + 1);
			}
			The.ZoneManager.AddZoneMidBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverBuilder");
			if (Options.ShowOverlandEncounters)
			{
				Render render = The.ZoneManager.GetZone("JoppaWorld").GetCell(num / 3, num2 / 3).GetObjectInCell(0)
					.Render;
				render.RenderString = (flag ? "T" : "~");
				render.Tile = null;
			}
			this.ContinueRiver(num, num2, num3, Depth + 1);
		}

		// Token: 0x06006A6E RID: 27246 RVA: 0x002215DC File Offset: 0x0021F7DC
		public void ContinueMamonRiver(int StartX, int StartY, int Direction, int Depth, int Bias, int MinimumLength, int ExcludeDirection, List<Location2D> Points)
		{
			int num = StartX;
			int num2 = StartY;
			if (Direction == 0)
			{
				num2--;
			}
			if (Direction == 2)
			{
				num2++;
			}
			if (Direction == 1)
			{
				num++;
			}
			if (Direction == 3)
			{
				num--;
			}
			if (num < 0 || num2 < 0 || num >= 240 || num2 >= 75)
			{
				return;
			}
			this.mutableMap.RemoveMutableLocation(Location2D.Get(num, num2));
			Points.Add(Location2D.Get(num, num2));
			if (Depth > MinimumLength && Stat.Random(0, 100) < 2 + Depth * 5)
			{
				this.RiverSystem[num, num2] |= this.RIVER_START;
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverStartMouth");
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverBuilder");
				return;
			}
			int num3 = Direction;
			if (Bias <= 0 && 50.in100())
			{
				int num4 = Stat.Random(0, 1);
				if (num4 == 0)
				{
					num3--;
				}
				if (num4 == 1)
				{
					num3++;
				}
				if (num3 < 0)
				{
					num3 = 3;
				}
				if (num3 > 3)
				{
					num3 = 0;
				}
				if (num3 == ExcludeDirection)
				{
					num3 = Direction;
				}
			}
			if (Direction == 0)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverSouthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_SOUTH;
			}
			if (Direction == 1)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverWestMouth");
				this.RiverSystem[num, num2] |= this.RIVER_WEST;
			}
			if (Direction == 2)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverNorthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_NORTH;
			}
			if (Direction == 3)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverEastMouth");
				this.RiverSystem[num, num2] |= this.RIVER_EAST;
			}
			int num5 = num;
			int num6 = num2;
			if (num3 == 0)
			{
				num6--;
			}
			if (num3 == 2)
			{
				num6++;
			}
			if (num3 == 1)
			{
				num5++;
			}
			if (num3 == 3)
			{
				num5--;
			}
			if (num6 < 0 || num5 < 0 || num6 == 75 || num5 == 240 || this.RiverSystem[num5, num6] != 0U)
			{
				this.RiverSystem[num, num2] |= this.RIVER_START;
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverStartMouth");
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverBuilder");
				return;
			}
			if (num3 == 0)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverNorthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_NORTH;
			}
			if (num3 == 1)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverEastMouth");
				this.RiverSystem[num, num2] |= this.RIVER_EAST;
			}
			if (num3 == 2)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverSouthMouth");
				this.RiverSystem[num, num2] |= this.RIVER_SOUTH;
			}
			if (num3 == 3)
			{
				The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverWestMouth");
				this.RiverSystem[num, num2] |= this.RIVER_WEST;
			}
			bool flag = false;
			The.ZoneManager.AddZonePostBuilder(this.ZoneIDFromXY("JoppaWorld", num, num2), "RiverBuilder");
			if (Options.ShowOverlandEncounters)
			{
				Render part = The.ZoneManager.GetZone("JoppaWorld").GetCell(num / 3, num2 / 3).GetObjectInCell(0)
					.GetPart<Render>();
				part.RenderString = "=";
				if (flag)
				{
					part.RenderString = "t";
				}
			}
			this.ContinueMamonRiver(num, num2, num3, Depth, Bias - 1, MinimumLength, ExcludeDirection, Points);
		}

		// Token: 0x06006A6F RID: 27247 RVA: 0x002219B4 File Offset: 0x0021FBB4
		public override bool BuildWorld(string worldName)
		{
			this.extensions = ModManager.GetInstancesWithAttribute<IJoppaWorldBuilderExtension>(typeof(JoppaWorldBuilderExtension));
			this.World = worldName;
			this.worldInfo = new WorldInfo();
			The.Game.ObjectGameState.Add("JoppaWorldInfo", this.worldInfo);
			this.WorldZone = The.ZoneManager.GetZone(this.World);
			this.extensions.ForEach(delegate(IJoppaWorldBuilderExtension e)
			{
				e.OnBeforeBuild(this);
			});
			MetricsManager.rngCheckpoint("JWB start");
			JournalAPI.SuspendSorting();
			MetricsManager.rngCheckpoint("mazes");
			WorldCreationProgress.NextStep("Generating deep history...", 1);
			this.BuildMazes();
			MetricsManager.rngCheckpoint("mutable");
			WorldCreationProgress.NextStep("Generating historical sites...", WorldFactory.Factory.countWorlds() * 80);
			this.BuildMutableEncounters();
			if (The.Game.AlternateStart && TutorialManager.currentStep == null)
			{
				Factions.Get("Joppa").Visible = false;
			}
			base.game.SetStringGameState("BlackOrbLocation", "OrbWorld.40.15.1.1.12@19,7");
			JournalAPI.ResumeSorting();
			MetricsManager.rngCheckpoint("embark");
			if (base.game.GetStringGameState("RuinedJoppa", "No") == "Yes")
			{
				Factions.Get("Joppa").Visible = false;
				Cell currentCell = base.game.ZoneManager.GetZone("JoppaWorld").GetFirstObject((GameObject o) => o.Blueprint == "TerrainJoppa").GetCurrentCell();
				currentCell.Clear(null, false, false, null);
				currentCell.AddObject("TerrainJoppaRuins", null, null, null, null);
				base.game.ZoneManager.SetZoneName("JoppaWorld.11.22.1.1.10", "abandoned village", null, null, null, null, false, true);
				for (int i = 0; i <= 2; i++)
				{
					for (int j = 0; j <= 2; j++)
					{
						if (i != 1 || j != 1)
						{
							base.game.ZoneManager.SetZoneName(string.Concat(new string[]
							{
								"JoppaWorld.11.22.",
								i.ToString(),
								".",
								j.ToString(),
								".10"
							}), "salt marsh", "", null, null, null, false, true);
						}
					}
				}
			}
			this.extensions.ForEach(delegate(IJoppaWorldBuilderExtension e)
			{
				e.OnAfterBuild(this);
			});
			return true;
		}

		// Token: 0x0400308E RID: 12430
		public const string ID_JOPPA = "JoppaWorld.11.22.1.1.10";

		// Token: 0x0400308F RID: 12431
		public const string ID_GRIT_GATE = "JoppaWorld.22.14.1.0.13";

		// Token: 0x04003090 RID: 12432
		public const string ID_EZRA = "JoppaWorld.53.4.0.0.10";

		// Token: 0x04003091 RID: 12433
		public const string ID_SPINDLE = "JoppaWorld.53.3.1.1.10";

		// Token: 0x04003092 RID: 12434
		public const string ID_GOLGOTHA = "JoppaWorld.23.9.1.1.10";

		// Token: 0x04003093 RID: 12435
		public const string ID_BETHESDA = "JoppaWorld.25.3.1.1.10";

		// Token: 0x04003094 RID: 12436
		public const string ID_TEMPLE_ROCK = "JoppaWorld.25.3.1.1.26";

		// Token: 0x04003095 RID: 12437
		public const string ID_COURT = "JoppaWorld.53.4.1.1.10";

		// Token: 0x04003096 RID: 12438
		public MutabilityMap mutableMap = new MutabilityMap();

		// Token: 0x04003097 RID: 12439
		public Dictionary<Location2D, string> terrainTypes = new Dictionary<Location2D, string>();

		// Token: 0x04003098 RID: 12440
		public Dictionary<Location2D, TerrainTravel> terrainComponents = new Dictionary<Location2D, TerrainTravel>();

		// Token: 0x04003099 RID: 12441
		public WorldInfo worldInfo;

		// Token: 0x0400309A RID: 12442
		public Zone WorldZone;

		// Token: 0x0400309B RID: 12443
		public List<IJoppaWorldBuilderExtension> extensions = new List<IJoppaWorldBuilderExtension>();

		// Token: 0x0400309C RID: 12444
		private string World = "JoppaWorld";

		// Token: 0x0400309D RID: 12445
		public int[,] Lairs;

		// Token: 0x0400309E RID: 12446
		public static uint ROAD_NORTH = 1U;

		// Token: 0x0400309F RID: 12447
		public static uint ROAD_SOUTH = 2U;

		// Token: 0x040030A0 RID: 12448
		public static uint ROAD_EAST = 4U;

		// Token: 0x040030A1 RID: 12449
		public static uint ROAD_WEST = 8U;

		// Token: 0x040030A2 RID: 12450
		public static uint ROAD_NONE = 16U;

		// Token: 0x040030A3 RID: 12451
		public static uint ROAD_START = 32U;

		// Token: 0x040030A4 RID: 12452
		public uint RIVER_NORTH = 1U;

		// Token: 0x040030A5 RID: 12453
		public uint RIVER_SOUTH = 2U;

		// Token: 0x040030A6 RID: 12454
		public uint RIVER_EAST = 4U;

		// Token: 0x040030A7 RID: 12455
		public uint RIVER_WEST = 8U;

		// Token: 0x040030A8 RID: 12456
		public uint RIVER_NONE = 16U;

		// Token: 0x040030A9 RID: 12457
		public uint RIVER_START = 32U;
	}
}
