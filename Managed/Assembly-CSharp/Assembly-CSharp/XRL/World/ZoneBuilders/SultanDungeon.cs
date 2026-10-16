using System;
using System.Collections.Generic;
using Genkit;
using HistoryKit;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF2 RID: 2802
	public class SultanDungeon : ZoneBuilderSandbox
	{
		// Token: 0x0600704F RID: 28751 RVA: 0x0027C5C1 File Offset: 0x0027A7C1
		public int clamp(int n, int low, int high)
		{
			if (n < low)
			{
				return low;
			}
			if (n > high)
			{
				return high;
			}
			return n;
		}

		// Token: 0x06007050 RID: 28752 RVA: 0x0027C5D0 File Offset: 0x0027A7D0
		public void partition(Rect2D rect, ref int nSegments, List<ISultanDungeonSegment> segments)
		{
			if (nSegments <= 0)
			{
				segments.Add(new SultanRectDungeonSegment(rect));
				return;
			}
			if (Stat.Random(0, 1) == 0)
			{
				int num = rect.y2 - rect.y1;
				int num2 = (rect.y2 - rect.y1) / 2;
				int num3 = (rect.y2 - rect.y1) / 8;
				int num4 = this.clamp((int)Stat.GaussianRandom(0f, (float)num3) + num2, rect.y1 + 4, rect.y2 - 4);
				if (num <= 8)
				{
					segments.Add(new SultanRectDungeonSegment(rect));
					return;
				}
				nSegments--;
				Rect2D rect2D = new Rect2D(rect.x1, rect.y1, rect.x2, num4);
				this.partition(rect2D, ref nSegments, segments);
				nSegments--;
				Rect2D rect2D2 = new Rect2D(rect.x1, num4 + 1, rect.x2, rect.y2);
				this.partition(rect2D2, ref nSegments, segments);
				return;
			}
			else
			{
				int num5 = rect.x2 - rect.x1;
				int num6 = (rect.x2 - rect.x1) / 2;
				int num7 = (rect.x2 - rect.x1) / 8;
				int num8 = this.clamp((int)Stat.GaussianRandom(0f, (float)num7) + num6, rect.x1 + 4, rect.x2 - 4);
				if (num5 <= 16)
				{
					segments.Add(new SultanRectDungeonSegment(rect));
					return;
				}
				nSegments--;
				Rect2D rect2D3 = new Rect2D(rect.x1, rect.y1, num8, rect.y2);
				this.partition(rect2D3, ref nSegments, segments);
				nSegments--;
				Rect2D rect2D4 = new Rect2D(num8 + 1, rect.y1, rect.x2, rect.y2);
				this.partition(rect2D4, ref nSegments, segments);
				return;
			}
		}

		// Token: 0x06007051 RID: 28753 RVA: 0x0027C778 File Offset: 0x0027A978
		public string ResolveOneObjectFromArgs(List<string> arg, string def = null, int tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			string randomElement = arg.GetRandomElement<string>(null);
			if (randomElement.Length == 0)
			{
				return def;
			}
			if (randomElement[0] == '$')
			{
				return PopulationManager.RollOneFrom(randomElement.Substring(1), new Dictionary<string, string> { 
				{
					"zonetier",
					tier.ToString()
				} }, "SmallBoulder").Blueprint;
			}
			return randomElement;
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x0027C7DE File Offset: 0x0027A9DE
		public List<PopulationResult> ResolveOnePopulationFromArgs(List<string> arg, string def = null, int Tier = 1)
		{
			if (arg == null || arg.Count == 0)
			{
				return PopulationManager.Generate(def, null, null, null, null, null);
			}
			return PopulationManager.Generate(arg.GetRandomElement<string>(null), "zonetier", Tier.ToString(), null, null, null);
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x0027C812 File Offset: 0x0027AA12
		public string ResolveOneTableFromArgs(List<string> arg, string def = null)
		{
			if (arg == null || arg.Count == 0)
			{
				return def;
			}
			return arg.GetRandomElement<string>(null);
		}

		// Token: 0x06007054 RID: 28756 RVA: 0x0027C828 File Offset: 0x0027AA28
		public bool BuildRandomZoneWithArgs(Zone Z, int relicChance, bool bBuildSurface, string[] args, string stairs, string monstertable = null, string tablePrefix = "SultanDungeons_")
		{
			SultanDungeonArgs sultanDungeonArgs = new SultanDungeonArgs();
			this.monstertable = monstertable;
			this.stairs = stairs;
			HistoricEntitySnapshot historicEntitySnapshot = new HistoricEntitySnapshot(null);
			historicEntitySnapshot.properties.Clear();
			for (int i = 0; i < args.Length; i++)
			{
				historicEntitySnapshot.properties.Add("additionalArg" + Guid.NewGuid().ToString(), args[i]);
			}
			sultanDungeonArgs.UpdateFromEntity(historicEntitySnapshot, tablePrefix);
			bool flag = this.BuildZoneFromArgs(Z, sultanDungeonArgs, false);
			if (flag && Stat.Random(1, 100) <= relicChance)
			{
				new PlaceRelicBuilder().BuildZoneWithRelic(Z, RelicGenerator.GenerateRelic(historicEntitySnapshot, Z.NewTier, null, true));
			}
			return flag;
		}

		// Token: 0x06007055 RID: 28757 RVA: 0x0027C8D4 File Offset: 0x0027AAD4
		public bool BuildRandomZone(Zone Z, int relicChance = 10, bool bBuildSurface = false, string tablePrefix = "SultanDungeons_")
		{
			HistoricEntityList sultanHistory = The.Game.sultanHistory;
			SultanDungeonArgs sultanDungeonArgs = new SultanDungeonArgs();
			HistoricEntitySnapshot currentSnapshot = sultanHistory.entities.GetRandomElement<HistoricEntity>(null).GetCurrentSnapshot();
			sultanDungeonArgs.UpdateFromEntity(currentSnapshot, tablePrefix);
			GameObject terrainObject = Z.GetTerrainObject();
			if (terrainObject.HasTag("RuinWalls") && Z.Z <= 15)
			{
				string blueprint;
				if (!The.ZoneManager.TryGetWorldCellProperty<string>(Z.ZoneID, "RuinWallType", out blueprint))
				{
					blueprint = PopulationManager.RollOneFrom(terrainObject.GetTag("RuinWalls", null), null, null).Blueprint;
					The.ZoneManager.SetWorldCellProperty(Z.ZoneID, "RuinWallType", blueprint);
				}
				sultanDungeonArgs.wallTypes.Clear();
				sultanDungeonArgs.wallTypes.Add(blueprint);
			}
			bool flag = this.BuildZoneFromArgs(Z, sultanDungeonArgs, false);
			if (flag && Stat.Random(1, 100) <= relicChance)
			{
				new PlaceRelicBuilder().BuildZoneWithRelic(Z, RelicGenerator.GenerateRelic(currentSnapshot, Z.NewTier, null, true));
			}
			return flag;
		}

		// Token: 0x06007056 RID: 28758 RVA: 0x0027C9B9 File Offset: 0x0027ABB9
		public bool BuildZone(Zone Z)
		{
			return this.BuildZoneFromArgs(Z, (SultanDungeonArgs)The.Game.GetObjectGameState("sultanDungeonArgs_" + this.regionName), true);
		}

		// Token: 0x06007057 RID: 28759 RVA: 0x0027C9E2 File Offset: 0x0027ABE2
		public void LogMem(string step)
		{
		}

		// Token: 0x06007058 RID: 28760 RVA: 0x0027C9E4 File Offset: 0x0027ABE4
		public bool PointInSegments(List<ISultanDungeonSegment> segments, int x, int y)
		{
			for (int i = 0; i < segments.Count; i++)
			{
				if (segments[i].contains(Location2D.Get(x, y)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007059 RID: 28761 RVA: 0x0027CA1C File Offset: 0x0027AC1C
		public GameObject sultanDungeonSemanticCreate(string blueprint)
		{
			if (string.IsNullOrEmpty(blueprint) || blueprint[0] != '*')
			{
				return GameObject.Create(blueprint);
			}
			int num = 0;
			string text = "DynamicSemanticTable:" + blueprint.Substring(1) + "::" + this.targetTier.ToString();
			if (!SultanDungeon.staticPerDungeon.TryGetValue(blueprint, out num) || Stat.Random(1, 100) <= num)
			{
				return GameObject.Create(PopulationManager.RollOneFrom(text, null, null).Blueprint);
			}
			if (!base.HasZoneColumnValue(this.targetZone, "sultandungeon_semantic_result_" + blueprint))
			{
				string blueprint2 = PopulationManager.RollOneFrom(text, null, null).Blueprint;
				base.SetZoneColumnValue(this.targetZone, "sultandungeon_semantic_result_" + blueprint, blueprint2);
				return GameObject.Create(blueprint2);
			}
			return GameObject.Create(base.GetZoneColumnValue(this.targetZone, "sultandungeon_semantic_result_" + blueprint, null));
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x0027CB00 File Offset: 0x0027AD00
		public bool BuildZoneFromArgs(Zone Z, SultanDungeonArgs regionArgs, bool AddSurface)
		{
			this.targetZone = Z;
			if (this.targetZone != null)
			{
				this.targetTier = this.targetZone.NewTier;
			}
			if (Z.Z > 10)
			{
				Z.AmbientBed = "Sounds/Ambiences/amb_bed_dungeon";
			}
			this.LogMem("step1");
			string text = "*Default";
			string tablePrefix = regionArgs.tablePrefix;
			string text2 = tablePrefix + "Floor_" + text;
			PopulationResult populationResult = PopulationManager.RollOneFrom(text2, null, "Rocky");
			if (populationResult == null)
			{
				throw new Exception("Got no floor blueprint result from " + text2);
			}
			string blueprint = populationResult.Blueprint;
			HistoricEntityList sultanHistory = The.Game.sultanHistory;
			HistoricEntitySnapshot historicEntitySnapshot = null;
			HistoricEntity randomElement = sultanHistory.GetEntitiesWherePropertyEquals("name", this.locationName).entities.GetRandomElement<HistoricEntity>(null);
			if (randomElement != null)
			{
				historicEntitySnapshot = randomElement.GetCurrentSnapshot();
			}
			SultanDungeonArgs sultanDungeonArgs = regionArgs.clone();
			sultanDungeonArgs.Mutate();
			if (historicEntitySnapshot != null)
			{
				sultanDungeonArgs.UpdateFromEntity(historicEntitySnapshot, "SultanDungeons_");
			}
			string text3 = tablePrefix + "Segmentation_*Default";
			PopulationResult populationResult2 = PopulationManager.RollOneFrom(text3, null, "Full");
			if (populationResult2 == null)
			{
				throw new Exception("Got no segmentation blueprint result from " + text3);
			}
			string blueprint2 = populationResult2.Blueprint;
			string text4 = this.ResolveOneTableFromArgs(sultanDungeonArgs.greenObjects, null);
			string text5 = this.ResolveOneTableFromArgs(sultanDungeonArgs.yellowObjects, null);
			string text6 = this.ResolveOneTableFromArgs(sultanDungeonArgs.blueObjects, null);
			string text7;
			if (text6 == null)
			{
				text7 = "SaltyWaterPuddle";
			}
			else
			{
				PopulationResult populationResult3 = PopulationManager.RollOneFrom(text6, null, "SaltyWaterPuddle");
				if (populationResult3 == null)
				{
					throw new Exception("Got no blue object result from " + text6);
				}
				text7 = populationResult3.Blueprint;
			}
			List<ISultanDungeonSegment> list = new List<ISultanDungeonSegment>();
			int num = 7;
			int num2 = 72;
			int num3 = 7;
			int num4 = 17;
			foreach (string text8 in blueprint2.Split(';', StringSplitOptions.None))
			{
				if (text8 == "Zone")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(0, 0, 80, 25)));
				}
				else if (text8 == "Full")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
				}
				else if (text8.StartsWith("BSP:"))
				{
					int num5 = Convert.ToInt32(text8.Split(':', StringSplitOptions.None)[1]);
					this.partition(new Rect2D(2, 2, 78, 24), ref num5, list);
				}
				else if (text8.StartsWith("Ring:"))
				{
					int num6 = Convert.ToInt32(text8.Split(':', StringSplitOptions.None)[1]);
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
					if (num6 == 2)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(20, 8, 60, 16)));
					}
					if (num6 == 3)
					{
						list.Add(new SultanRectDungeonSegment(new Rect2D(15, 8, 65, 16)));
						list.Add(new SultanRectDungeonSegment(new Rect2D(25, 10, 55, 14)));
					}
				}
				else if (text8.StartsWith("Blocks"))
				{
					string[] array2 = text8.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					int num7 = array2[0].RollCached();
					for (int j = 0; j < num7; j++)
					{
						int num8 = array2[1].RollCached();
						int num9 = array2[2].RollCached();
						int num10 = Stat.Random(2, 78 - num8);
						int num11 = Stat.Random(2, 23 - num9);
						int num12 = num10 + num8;
						int num13 = num11 + num9;
						if (num < num10)
						{
							num = num10;
						}
						if (num2 > num12)
						{
							num2 = num12;
						}
						if (num3 < num11)
						{
							num3 = num11;
						}
						if (num4 > num13)
						{
							num4 = num13;
						}
						list.Add(new SultanRectDungeonSegment(new Rect2D(num10, num11, num10 + num8, num11 + num9)));
					}
				}
				else if (text8.StartsWith("Circle"))
				{
					string[] array3 = text8.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanCircleDungeonSegment(Location2D.Get(array3[0].RollCached(), array3[1].RollCached()), array3[2].RollCached()));
				}
				else if (text8.StartsWith("Tower"))
				{
					string[] array4 = text8.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTowerDungeonSegment(Location2D.Get(array4[0].RollCached(), array4[1].RollCached()), array4[2].RollCached(), array4[3].RollCached()));
				}
			}
			ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
			for (int k = 0; k < list.Count; k++)
			{
				string text9 = sultanDungeonArgs.primaryTemplate.GetRandomElement<string>(null);
				int primaryTemplateN = sultanDungeonArgs.primaryTemplateN;
				string text10 = "";
				string randomElement2 = sultanDungeonArgs.secondaryTemplate.GetRandomElement<string>(null);
				int secondaryTemplateN = sultanDungeonArgs.secondaryTemplateN;
				if (text9.Contains(","))
				{
					string[] array5 = text9.Split(',', StringSplitOptions.None);
					text9 = array5[0];
					text10 = array5[1];
				}
				Cell cell = Z.GetCell(0, 0);
				if (cell == null)
				{
					throw new Exception("No cell at 0, 0");
				}
				if (Z.GetZoneZ() <= 10)
				{
					cell.AddObject("DaylightWidget", null, null, null, null);
				}
				if ((!AddSurface || Z.Z > 10 || string.IsNullOrEmpty(cell.PaintTile)) && !string.IsNullOrEmpty(blueprint))
				{
					cell.AddObject(blueprint, null, null, null, null);
				}
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(text9, primaryTemplateN, list[k].width(), list[k].height(), true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				if (!string.IsNullOrEmpty(text10))
				{
					waveCollapseFastModel.ClearColors(text10, "border1");
				}
				waveCollapseFastModel.UpdateSample(randomElement2, secondaryTemplateN, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap2.ReplaceBorders(new Color32(byte.MaxValue, 0, 0, byte.MaxValue), new Color32(0, 0, 0, byte.MaxValue));
				colorOutputMap.Paste(colorOutputMap2, list[k].x1, list[k].y1);
				MemoryHelper.GCCollect();
			}
			this.LogMem("step2");
			string defaultWall = base.GetDefaultWall(Z);
			if (Z.Z > 10)
			{
				for (int l = 0; l < Z.Height; l++)
				{
					for (int m = 0; m < Z.Width; m++)
					{
						bool flag = false;
						for (int n = 0; n < list.Count; n++)
						{
							if (list[n].contains(m, l))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							Cell cell2 = Z.GetCell(m, l);
							if (cell2 == null)
							{
								throw new Exception("No cell at " + m.ToString() + ", " + l.ToString());
							}
							if (!cell2.HasWall())
							{
								cell2.AddObject(defaultWall, null, null, null, null);
							}
						}
					}
				}
			}
			if (AddSurface && Z.Z == 10)
			{
				string terrain = base.GetTerrain(Z);
				if (!string.IsNullOrEmpty(terrain))
				{
					if (terrain.Contains("Canyon") || terrain.Contains("Hills") || terrain.Contains("Mountains"))
					{
						new Hills().BuildZone(Z);
						Z.GetCell(0, 0).AddObject("Rocky", null, null, null, null);
					}
					else if (terrain.Contains("Wastes") || terrain.Contains("Desert") || terrain.Contains("dunes"))
					{
						new SaltDunes().BuildZone(Z);
					}
					else if (terrain.Contains("marsh") || terrain.Contains("Watervine"))
					{
						new Watervine().BuildZone(Z);
					}
					else if (terrain.Contains("River") || terrain.Contains("Water") || terrain.Contains("Opal") || terrain.Contains("Stream"))
					{
						new OverlandWater().BuildZone(Z);
					}
					else if (terrain.Contains("Ruins"))
					{
						new Ruins().BuildZone(Z);
					}
					else if (terrain.Contains("Flowerf"))
					{
						new Flowerfields().BuildZone(Z);
					}
					else if (terrain.Contains("Banana"))
					{
						new BananaGrove().BuildZone(Z);
					}
					else
					{
						Z.GetCell(0, 0).AddObject(blueprint, null, null, null, null);
					}
				}
			}
			int num14 = 0;
			int num15 = 0;
			for (int num16 = 0; num16 < list.Count; num16++)
			{
				string text11 = this.ResolveOneObjectFromArgs(sultanDungeonArgs.wallTypes, "Fulcrete", Z.NewTier);
				string text12 = ((!string.IsNullOrEmpty(regionArgs.cultPeriod)) ? regionArgs.cultPeriod : SultanDungeon.GetSultanPeriodFromTier(Z.NewTier).ToString());
				if (text11.Contains("^"))
				{
					string text13;
					if (!string.IsNullOrEmpty(regionArgs.cultPeriod))
					{
						if (!The.Game.HasStringGameState(text11 + this.regionName))
						{
							The.Game.SetStringGameState(text11 + this.regionName, SultanDungeon.GetDefaultWallByPeriod(text12));
						}
						text13 = The.Game.GetStringGameState(text11 + this.regionName, "");
					}
					else
					{
						text13 = SultanDungeon.GetDefaultWallByPeriod(text12);
					}
					text11 = (25.in100() ? text11.Replace("^", "") : text13);
				}
				if (text11 == "*auto")
				{
					text11 = base.GetDefaultWall(Z);
				}
				if (text11.Contains("*SultanWall*"))
				{
					if (string.IsNullOrEmpty(regionArgs.cultPeriod))
					{
						Stat.Random(1, 5).ToString();
					}
					else
					{
						string cultPeriod = regionArgs.cultPeriod;
					}
					text11 = text11.Replace("*SultanWall*", "InnerSultanWall_Period" + regionArgs.cultPeriod);
				}
				for (int num17 = list[num16].y1; num17 < list[num16].y2; num17++)
				{
					for (int num18 = list[num16].x1; num18 < list[num16].x2; num18++)
					{
						if (list[num16].contains(num18, num17))
						{
							Cell cell3 = Z.GetCell(num18 + num14, num17 + num15);
							if (cell3 == null)
							{
								throw new Exception("No cell at " + (num18 + num14).ToString() + ", " + (num17 + num15).ToString());
							}
							for (int num19 = num16 + 1; num19 < list.Count; num19++)
							{
								if (list[num19].contains(num18, num17))
								{
									goto IL_0DA4;
								}
							}
							Color32 color = colorOutputMap.getPixel(num18, num17);
							if (list[num16].HasCustomColor(num18, num17))
							{
								color = list[num16].GetCustomColor(num18, num17);
							}
							if (WaveCollapseTools.equals(color, ColorOutputMap.BLACK))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								cell3.ClearWalls();
								if (!cell3.HasWall() && !cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(text11, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.BLUE))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								if (!cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(text7, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.YELLOW))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								PopulationResult populationResult4 = PopulationManager.RollOneFrom(text5, null, "Door");
								string text14 = ((populationResult4 != null) ? populationResult4.Blueprint : null);
								if (GameObjectFactory.Factory.GetBlueprintIfExists(text11).HasTag("UsesGate", false))
								{
									text14 = GameObjectFactory.Factory.GetBlueprintIfExists(text11).GetTag("UsesGate", "");
								}
								if (populationResult4 == null)
								{
									throw new Exception("Got no yellow result from " + text5);
								}
								if (!cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(text14, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.GREEN))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								PopulationResult populationResult5 = PopulationManager.RollOneFrom(text4, null, "Shimscale Mangrove Tree");
								if (populationResult5 == null)
								{
									throw new Exception("Got no green result from " + text4);
								}
								if (!cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									cell3.AddObject(populationResult5.Blueprint, null, null, null, null);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.RED) || WaveCollapseTools.equals(color, ColorOutputMap.MAGENTA))
							{
								if (Z.Z == 10)
								{
									cell3.Clear(null, false, false, null);
								}
								cell3.ClearWalls();
								if (!cell3.HasObjectWithTag("NoRuinsRemove"))
								{
									Rocky.Paint(cell3);
								}
							}
							else if (WaveCollapseTools.equals(color, ColorOutputMap.WHITE))
							{
								if (blueprint == "Rocky")
								{
									Rocky.Paint(cell3);
								}
								if (blueprint == "Grassy")
								{
									Grassy.PaintCell(cell3);
								}
								if (blueprint == "Flowery")
								{
									Flowery.Paint(cell3);
								}
								if (blueprint == "Mushroomy")
								{
									Mushroomy.Paint(cell3);
								}
								if (blueprint == "Trashy")
								{
									Trashy.PaintCell(cell3);
								}
								if (blueprint == "Dirty")
								{
									Dirty.PaintCell(cell3);
								}
								if (blueprint == "UndergroundGrassy")
								{
									UndergroundGrassy.PaintCell(cell3);
								}
								if (blueprint == "GrassPaint")
								{
									GrassPaint.PaintCell(cell3);
								}
								if (blueprint == "UndergroundGrassPaint")
								{
									UndergroundGrassPaint.PaintCell(cell3);
								}
							}
						}
						IL_0DA4:;
					}
				}
			}
			this.LogMem("step3a");
			foreach (PopulationResult populationResult6 in this.ResolveOnePopulationFromArgs(sultanDungeonArgs.preconnectencounters, tablePrefix + "PreconnectEncounters_*Default", Z.NewTier))
			{
				for (int num20 = 0; num20 < populationResult6.Number; num20++)
				{
					ZoneBuilderSandbox.PlaceObjectInRect(Z, new Rect2D(5, 5, Z.Width - 5, Z.Height - 5), populationResult6.Blueprint, populationResult6.Hint, num20, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
				}
			}
			this.LogMem("step3b");
			if (Z.Z > 10)
			{
				int num21 = Stat.Random(num3, num3);
				int num22 = Stat.Random(num4, num4);
				for (int num23 = 0; num23 < Z.Width; num23++)
				{
					int num24 = 0;
					int num25 = num21 + Stat.Random(0, 2);
					while (num24 < num25)
					{
						Cell cell4 = Z.GetCell(num23, num24);
						if (cell4.HasWall())
						{
							if (this.PointInSegments(list, num23, num24))
							{
								break;
							}
						}
						else
						{
							cell4.AddObject(defaultWall, null, null, null, null);
						}
						num24++;
					}
					int num26 = Z.Height - 1;
					int num27 = num22 - Stat.Random(0, 2);
					while (num26 > num27)
					{
						Cell cell5 = Z.GetCell(num23, num26);
						if (cell5.HasWall())
						{
							if (this.PointInSegments(list, num23, num26))
							{
								break;
							}
						}
						else
						{
							cell5.AddObject(defaultWall, null, null, null, null);
						}
						num26--;
					}
				}
				int num28 = Stat.Random(num, num);
				int num29 = Stat.Random(num2, num2);
				for (int num30 = 1; num30 < Z.Height - 1; num30++)
				{
					int num31 = 0;
					int num32 = num28 + Stat.Random(0, 2);
					while (num31 < num32)
					{
						Cell cell6 = Z.GetCell(num31, num30);
						if (cell6.HasWall())
						{
							if (this.PointInSegments(list, num31, num30))
							{
								break;
							}
						}
						else
						{
							cell6.AddObject(defaultWall, null, null, null, null);
						}
						num31++;
					}
					int num33 = Z.Width - 1;
					int num34 = num29 - Stat.Random(0, 2);
					while (num33 > num34)
					{
						Cell cell7 = Z.GetCell(num33, num30);
						if (cell7.HasWall())
						{
							if (this.PointInSegments(list, num33, num30))
							{
								break;
							}
						}
						else
						{
							cell7.AddObject(defaultWall, null, null, null, null);
						}
						num33--;
					}
				}
			}
			this.LogMem("step4");
			InfluenceMap influenceMap = new InfluenceMap(colorOutputMap.width, colorOutputMap.height);
			for (int num35 = 0; num35 < colorOutputMap.height; num35++)
			{
				for (int num36 = 0; num36 < colorOutputMap.width; num36++)
				{
					influenceMap.Walls[num36, num35] = (Z.GetCell(num36, num35).HasWall() ? 1 : 0);
				}
			}
			influenceMap.SeedAllUnseeded(false, true);
			if (influenceMap.Seeds.Count == 0)
			{
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.Walls[Stat.Random(10, 70), Stat.Random(5, 15)] = 0;
				influenceMap.SeedAllUnseeded(false, true);
			}
			influenceMap.MoveSeedsToCenters(false);
			int num37 = 0;
			using (Pathfinder pathfinder = Z.getPathfinder(null))
			{
				for (int num38 = 0; num38 < colorOutputMap.height; num38++)
				{
					for (int num39 = 0; num39 < colorOutputMap.width; num39++)
					{
						if (influenceMap.Walls[num39, num38] > 0)
						{
							pathfinder.CurrentNavigationMap[num39, num38] = 999;
						}
						else
						{
							pathfinder.CurrentNavigationMap[num39, num38] = 0;
						}
					}
				}
				this.LogMem("step5");
				List<int> list2 = new List<int>();
				list2.Add(num37);
				for (int num40 = 0; num40 < influenceMap.Seeds.Count; num40++)
				{
					if (!list2.Contains(num40))
					{
						int num41 = influenceMap.FindClosestSeedToInList(num40, list2);
						if (num41 == -1)
						{
							num41 = num37;
						}
						if (pathfinder.FindPath(influenceMap.Seeds[num41], influenceMap.Seeds[num40], false, false, 9999, false))
						{
							int num42 = 0;
							foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
							{
								Cell cell8 = Z.GetCell(pathfinderNode.X, pathfinderNode.Y);
								if (cell8.HasWall())
								{
									if (num42 == 0)
									{
										if (cell8.HasObjectWithTag("UsesGate"))
										{
											string tag = cell8.GetObjectWithTag("UsesGate").GetTag("UsesGate", "Iron Gate");
											cell8.AddObject(tag, null, null, null, null);
										}
										else
										{
											cell8.AddObject("Door", null, null, null, null);
										}
										num42++;
									}
									cell8.ClearWalls();
									pathfinder.CurrentNavigationMap[pathfinderNode.X, pathfinderNode.Y] = 0;
									pathfinderNode.weight = 0;
								}
								else if (Z.Z == 10)
								{
									cell8.Clear(null, false, false, null);
								}
							}
						}
						list2.Add(num40);
					}
				}
				foreach (ZoneConnection zoneConnection in The.ZoneManager.GetZoneConnections(Z.ZoneID))
				{
					Location2D location2D = Location2D.Get(zoneConnection.X, zoneConnection.Y);
					int num43 = influenceMap.FindClosestSeedToInList(location2D, list2);
					if (num43 == -1)
					{
						num43 = num37;
					}
					if (pathfinder.FindPath(influenceMap.Seeds[num43], location2D, false, false, 9999, false))
					{
						int num44 = 0;
						foreach (PathfinderNode pathfinderNode2 in pathfinder.Steps)
						{
							Cell cell9 = Z.GetCell(pathfinderNode2.X, pathfinderNode2.Y);
							if (cell9.HasWall())
							{
								if (num44 == 0)
								{
									if (cell9.HasObjectWithTag("UsesGate"))
									{
										string tag2 = cell9.GetObjectWithTag("UsesGate").GetTag("UsesGate", "Iron Gate");
										cell9.AddObject(tag2, null, null, null, null);
									}
									else
									{
										cell9.AddObject("Door", null, null, null, null);
									}
									num44++;
								}
								cell9.ClearWalls();
								pathfinder.CurrentNavigationMap[pathfinderNode2.X, pathfinderNode2.Y] = 0;
								pathfinderNode2.weight = 0;
							}
							else if (Z.Z == 10)
							{
								cell9.Clear(null, false, false, null);
							}
						}
					}
				}
				this.LogMem("step6");
				for (int num45 = 0; num45 < list2.Count; num45++)
				{
					if (!Z.IsReachable(influenceMap.Seeds[num45].X, influenceMap.Seeds[num45].Y))
					{
						Z.BuildReachableMap(influenceMap.Seeds[num45].X, influenceMap.Seeds[num45].Y);
					}
				}
			}
			if (regionArgs.properties.Contains("Waste") || sultanDungeonArgs.properties.Contains("Waste"))
			{
				int oracleIntColumn = ZoneBuilderSandbox.GetOracleIntColumn(Z, 1, 4);
				bool flag2 = Stat.Random(1, 100) <= 30;
				new GenericChutes().BuildZone(Z, oracleIntColumn, flag2);
			}
			if (this.stairs.Contains("U"))
			{
				new StairsUp().BuildZone(Z);
			}
			if (this.stairs.Contains("D"))
			{
				new StairsDown().BuildZone(Z);
			}
			List<Point2D> list3 = new List<Point2D>();
			if (!this.stairs.Contains("D") && influenceMap != null && influenceMap.Seeds.Count > 0)
			{
				list3.Add(influenceMap.Seeds[0].Point);
			}
			new ForceConnections()._BuildZone(Z, list3);
			this.LogMem("step7");
			for (int num46 = 0; num46 < 500; num46++)
			{
				bool flag3 = false;
				for (int num47 = 1; num47 < Z.Width - 1; num47++)
				{
					for (int num48 = 1; num48 < Z.Height - 1; num48++)
					{
						Cell cell10 = Z.GetCell(num47, num48);
						GameObject firstObjectWithPart = cell10.GetFirstObjectWithPart("Door");
						if (firstObjectWithPart != null && firstObjectWithPart.CanClear(false, false))
						{
							int num49 = 0;
							int num50 = 0;
							if (Z.GetCell(num47 - 1, num48).HasWall())
							{
								num50++;
							}
							if (Z.GetCell(num47 + 1, num48).HasWall())
							{
								num50++;
							}
							if (Z.GetCell(num47, num48 - 1).HasWall())
							{
								num49++;
							}
							if (Z.GetCell(num47, num48 + 1).HasWall())
							{
								num49++;
							}
							if (num49 + num50 != 2 || num49 == 1 || num50 == 1)
							{
								flag3 |= cell10.RemoveObject(firstObjectWithPart, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
							}
						}
					}
				}
				if (!flag3)
				{
					break;
				}
			}
			this.LogMem("step8");
			GameObject gameObject = Z.FindObject("StairsUp");
			GameObject gameObject2 = Z.FindObject("StairsDown");
			List<Location2D> list4 = new List<Location2D>();
			if (gameObject != null)
			{
				list4.Add(gameObject.GetCurrentCell().Location);
			}
			if (gameObject2 != null)
			{
				list4.Add(gameObject2.GetCurrentCell().Location);
			}
			this.LogMem("step9");
			int num51 = 0;
			while (influenceMap.LargestSize() > 150)
			{
				influenceMap.AddSeedAtMaximaInLargestSeed(false);
				num51++;
				this.LogMem("adseed " + num51.ToString());
			}
			this.LogMem("step10");
			InfluenceMap influenceMap2 = new InfluenceMap(80, 25);
			influenceMap2.Seeds = new List<Location2D>(influenceMap.Seeds);
			Z.SetInfluenceMapWalls(influenceMap2.Walls);
			influenceMap2.Recalculate(false);
			this.LogMem("step11");
			int num52 = 0;
			if (80.in100())
			{
				num52++;
			}
			Dictionary<InfluenceMapRegion, Rect2D> dictionary = new Dictionary<InfluenceMapRegion, Rect2D>();
			Dictionary<InfluenceMapRegion, string> dictionary2 = new Dictionary<InfluenceMapRegion, string>();
			Dictionary<InfluenceMapRegion, string> dictionary3 = new Dictionary<InfluenceMapRegion, string>();
			this.LogMem("step12");
			List<InfluenceMapRegion> list5 = new List<InfluenceMapRegion>();
			if (Z.GetZoneProperty("Relicstyle", "") == "Vault")
			{
				List<InfluenceMapRegion> list6 = new List<InfluenceMapRegion>();
				foreach (InfluenceMapRegion influenceMapRegion in influenceMap.Regions.Shuffle<InfluenceMapRegion>(null))
				{
					Rect2D rect2D = GridTools.MaxRectByArea(influenceMapRegion.GetGrid(), false, 0, 0).Translate(influenceMapRegion.BoundingBox.UpperLeft);
					dictionary.Add(influenceMapRegion, rect2D);
					if (rect2D.Width >= 4 && rect2D.Height >= 4 && !influenceMapRegion.AnyPointsIn(list4) && influenceMapRegion.AdjacentRegions.Count == 1)
					{
						list6.Add(influenceMapRegion);
						ZoneBuilderSandbox.PlaceObjectInRegionRect(Z, influenceMapRegion, rect2D, "N", "RelicChest", null, null, null, null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
						dictionary2.SetValue(influenceMapRegion, "vault");
						goto IL_1AED;
					}
				}
				foreach (InfluenceMapRegion influenceMapRegion2 in influenceMap.Regions.Shuffle<InfluenceMapRegion>(null))
				{
					Rect2D rect2D2 = dictionary[influenceMapRegion2];
					if (rect2D2.Width >= 4 && rect2D2.Height >= 4 && !influenceMapRegion2.AnyPointsIn(list4))
					{
						list6.Add(influenceMapRegion2);
						ZoneBuilderSandbox.PlaceObjectInRegionRect(Z, influenceMapRegion2, rect2D2, "N", "RelicChest", null, null, null, null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
						dictionary2.SetValue(influenceMapRegion2, "vault");
						goto IL_1AED;
					}
				}
				if (list6.Count == 0)
				{
					foreach (InfluenceMapRegion influenceMapRegion3 in influenceMap.Regions)
					{
						if (influenceMapRegion3.AdjacentRegions.Count == 1 && !influenceMapRegion3.AnyPointsIn(list4))
						{
							list6.Add(influenceMapRegion3);
						}
					}
				}
				if (list6.Count == 0)
				{
					list6.Add(influenceMap.Regions.GetRandomElement<InfluenceMapRegion>(null));
				}
				InfluenceMapRegion randomElement3 = list6.GetRandomElement<InfluenceMapRegion>(null);
				list5.Add(randomElement3);
				ZoneBuilderSandbox.PlaceObjectInRegion(Z, randomElement3, "RelicChest", 0, 0, null, null, false, null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
				dictionary2.SetValue(randomElement3, "vault");
			}
			IL_1AED:
			foreach (InfluenceMapRegion influenceMapRegion4 in influenceMap.Regions)
			{
				if (!list5.Contains(influenceMapRegion4))
				{
					Rect2D rect2D3;
					if (!dictionary.ContainsKey(influenceMapRegion4))
					{
						rect2D3 = GridTools.MaxRectByArea(influenceMapRegion4.GetGrid(), false, 0, 0).Translate(influenceMapRegion4.BoundingBox.UpperLeft);
						dictionary.Add(influenceMapRegion4, rect2D3);
					}
					else
					{
						rect2D3 = dictionary[influenceMapRegion4];
					}
					if (influenceMapRegion4.BoundingBox.x1 > 0 && influenceMapRegion4.BoundingBox.x2 < 79 && influenceMapRegion4.BoundingBox.y1 > 0 && influenceMapRegion4.BoundingBox.y2 < 24 && !influenceMapRegion4.AnyPointsIn(list4))
					{
						if (rect2D3.Area >= 4)
						{
							if (regionArgs.furnishings.Count > 0)
							{
								ZoneBuilderSandbox.PlacePopulationInRegion(Z, influenceMapRegion4, regionArgs.furnishings.GetRandomElement<string>(null), null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
							}
							dictionary3.Add(influenceMapRegion4, "room");
						}
						else if (influenceMap2.SeedToRegionMap[influenceMapRegion4.Seed].AdjacentRegions.Count == 1)
						{
							if (num52 > 0)
							{
								num52--;
								ZoneBuilderSandbox.PlacePopulationInRegion(Z, influenceMapRegion4, "RandomChest", null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
							}
							else if (regionArgs.cubbies.Count > 0)
							{
								ZoneBuilderSandbox.PlacePopulationInRegion(Z, influenceMapRegion4, regionArgs.cubbies.GetRandomElement<string>(null), null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
							}
							dictionary3.Add(influenceMapRegion4, "cubby");
						}
						else
						{
							if (regionArgs.halls.Count > 0)
							{
								ZoneBuilderSandbox.PlacePopulationInRegion(Z, influenceMapRegion4, regionArgs.halls.GetRandomElement<string>(null), null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
							}
							dictionary3.Add(influenceMapRegion4, "hall");
						}
					}
					else
					{
						dictionary3.Add(influenceMapRegion4, "connection");
					}
				}
			}
			this.LogMem("step13");
			Queue<InfluenceMapRegion> queue = new Queue<InfluenceMapRegion>(Algorithms.RandomShuffle<InfluenceMapRegion>(influenceMap2.Regions));
			IL_1E75:
			while (queue.Count > 0)
			{
				InfluenceMapRegion influenceMapRegion5 = queue.Dequeue();
				foreach (InfluenceMapRegion influenceMapRegion6 in influenceMapRegion5.AdjacentRegions)
				{
					if (dictionary2.ContainsKey(influenceMap.SeedToRegionMap[influenceMapRegion6.Seed]) && dictionary2[influenceMap.SeedToRegionMap[influenceMapRegion6.Seed]] != "vault")
					{
						if (!dictionary2.ContainsKey(influenceMap.SeedToRegionMap[influenceMapRegion5.Seed]))
						{
							dictionary2.Add(influenceMap.SeedToRegionMap[influenceMapRegion5.Seed], dictionary2[influenceMap.SeedToRegionMap[influenceMapRegion6.Seed]]);
						}
						goto IL_1E75;
					}
				}
				if (influenceMapRegion5.AdjacentRegions.Count <= 1)
				{
					if (!dictionary2.ContainsKey(influenceMap.SeedToRegionMap[influenceMapRegion5.Seed]))
					{
						dictionary2.Add(influenceMap.SeedToRegionMap[influenceMapRegion5.Seed], "abandoned");
					}
				}
				else if (!dictionary2.ContainsKey(influenceMap.SeedToRegionMap[influenceMapRegion5.Seed]))
				{
					dictionary2.Add(influenceMap.SeedToRegionMap[influenceMapRegion5.Seed], "cult");
				}
			}
			this.LogMem("step14");
			int num53 = Stat.Random(7, 10);
			int num54 = 0;
			this.LogMem("step15");
			foreach (InfluenceMapRegion influenceMapRegion7 in influenceMap.Regions.Shuffle<InfluenceMapRegion>(null))
			{
				if (!list5.Contains(influenceMapRegion7))
				{
					if (dictionary2[influenceMapRegion7] == "vault")
					{
						string text15 = this.ResolveCultRole("Hero", sultanDungeonArgs, Z.NewTier);
						if (!string.IsNullOrEmpty(text15))
						{
							GameObject gameObject3 = GameObject.Create(text15);
							while (gameObject3.Render == null)
							{
								Debug.LogError("generated cult hero with no render part from " + text15);
								text15 = this.ResolveCultRole("Hero", sultanDungeonArgs, Z.NewTier);
								gameObject3 = GameObject.Create(text15);
							}
							this.EnrollInCult(gameObject3, sultanDungeonArgs);
							if (!gameObject3.HasPart<GivesRep>())
							{
								gameObject3 = HeroMaker.MakeHero(gameObject3, null, null, -1, "Hero");
							}
							gameObject3.RequirePart<SocialRoles>(false).RemoveRole("member of the " + sultanDungeonArgs.cultName);
							gameObject3.RequirePart<SocialRoles>(false).RequireRole("{{M|leader of the " + sultanDungeonArgs.cultName + "}}");
							ZoneBuilderSandbox.PlaceObjectInArea(Z, influenceMapRegion7, gameObject3, 0, 0, null, null, false);
						}
					}
					else
					{
						if (dictionary2[influenceMapRegion7] == "cult")
						{
							if (num54 > 0)
							{
								num54--;
								continue;
							}
							if (num53 <= 0)
							{
								continue;
							}
							num53--;
							List<PopulationResult> list7 = this.ResolveOnePopulationFromArgs(sultanDungeonArgs.encounters, tablePrefix + "Encounters_*Default", Z.NewTier);
							int num55 = 0;
							using (List<PopulationResult>.Enumerator enumerator = list7.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									PopulationResult populationResult7 = enumerator.Current;
									num55++;
									bool flag4 = false;
									string text16;
									if (populationResult7.Blueprint.StartsWith("="))
									{
										text16 = this.ResolveCultRole(populationResult7.Blueprint.Substring(1), sultanDungeonArgs, Z.NewTier);
										flag4 = true;
									}
									else
									{
										text16 = populationResult7.Blueprint;
									}
									if (!string.IsNullOrEmpty(text16))
									{
										for (int num56 = 0; num56 < populationResult7.Number; num56++)
										{
											GameObject gameObject4 = GameObject.Create(text16);
											if (flag4)
											{
												this.EnrollInCult(gameObject4, sultanDungeonArgs);
											}
											ZoneBuilderSandbox.PlaceObjectInArea(Z, influenceMapRegion7, gameObject4, num55, num56, null, null, false);
										}
									}
								}
								continue;
							}
						}
						if (dictionary2[influenceMapRegion7] == "abandoned")
						{
							ZoneBuilderSandbox.PlacePopulationInRegionRect(Z, influenceMapRegion7, dictionary[influenceMapRegion7], "", "CaveDecoration", null, null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
							ZoneBuilderSandbox.PlacePopulationInRegionRect(Z, influenceMapRegion7, dictionary[influenceMapRegion7], "", "Tier" + Z.NewTier.ToString() + "CavePopulation", null, null, new Func<string, GameObject>(this.sultanDungeonSemanticCreate));
						}
					}
				}
			}
			this.LogMem("step16");
			MemoryHelper.GCCollect();
			return true;
		}

		// Token: 0x0600705B RID: 28763 RVA: 0x0027EDCC File Offset: 0x0027CFCC
		public void EnrollInCult(GameObject GO, SultanDungeonArgs args)
		{
			if (!string.IsNullOrEmpty(args.cultFaction))
			{
				GO.RequirePart<SocialRoles>(false).RequireRole("member of the " + args.cultName);
				if (GO.Brain != null)
				{
					GO.Brain.FactionFeelings.Clear();
					GO.Brain.Allegiance.Clear();
					GO.Brain.Allegiance.Add(args.cultFaction, 100);
				}
			}
		}

		// Token: 0x0600705C RID: 28764 RVA: 0x0027EE44 File Offset: 0x0027D044
		private void ProcessBlueprintForCultRole(GameObjectBlueprint B)
		{
			string text = "Minion";
			string text2;
			if (B.Tags.TryGetValue("Role", out text2) && !text2.IsNullOrEmpty())
			{
				text = text2;
			}
			if (B.Props.TryGetValue("Role", out text2) && !text2.IsNullOrEmpty())
			{
				text = text2;
			}
			List<string> list;
			if (!this.roleMembers.TryGetValue(text, out list))
			{
				list = new List<string>();
				this.roleMembers.Add(text, list);
			}
			if (!this.memberTier.ContainsKey(B.Name))
			{
				list.Add(B.Name);
				this.allMembers.Add(B.Name);
				this.memberTier.Add(B.Name, B.Tier);
				this.memberHasBody.Add(B.Name, B.HasPart("Body"));
			}
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x0027EF1C File Offset: 0x0027D11C
		public string ResolveCultRole(string Role, SultanDungeonArgs args, int targetTier)
		{
			if (this.roleMembers == null)
			{
				this.roleMembers = new Dictionary<string, List<string>>(16);
				this.allMembers = new List<string>(16);
				this.memberTier = new Dictionary<string, int>(16);
				this.memberHasBody = new Dictionary<string, bool>(16);
				if (args.cultFactions.Count > 0)
				{
					foreach (string text in args.cultFactions)
					{
						foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.GetFactionMembers(text, false, true))
						{
							this.ProcessBlueprintForCultRole(gameObjectBlueprint);
						}
					}
				}
				if (!string.IsNullOrEmpty(this.monstertable))
				{
					foreach (string text2 in PopulationManager.GetEach(this.monstertable, new Dictionary<string, string> { 
					{
						"ZoneTier",
						targetTier.ToString()
					} }, null))
					{
						GameObjectBlueprint gameObjectBlueprint2 = GameObjectFactory.Factory.Blueprints[text2];
						this.ProcessBlueprintForCultRole(gameObjectBlueprint2);
					}
				}
			}
			bool flag = false;
			if (Role == "Hero")
			{
				flag = true;
				if (!this.roleMembers.ContainsKey("Hero"))
				{
					Role = "Leader";
				}
			}
			List<string> list = this.allMembers;
			List<string> list2;
			if (this.roleMembers.TryGetValue(Role, out list2) && list2.Count > 0)
			{
				list = list2;
			}
			int num = 12;
			while (num.in100())
			{
				if (15.in100())
				{
					targetTier--;
				}
				else
				{
					targetTier++;
				}
				num = Math.Max(num - 6, 1);
			}
			Tier.Constrain(ref targetTier);
			List<string> list3 = new List<string>();
			for (int i = 0; i < 8; i++)
			{
				if (flag)
				{
					list3.Clear();
					int j = 0;
					int count = list.Count;
					while (j < count)
					{
						if ((this.memberTier[list[j]] - i == targetTier || this.memberTier[list[j]] + i == targetTier) && this.memberHasBody[list[j]])
						{
							list3.Add(list[j]);
						}
						j++;
					}
					if (list3.Count > 0)
					{
						return list3.GetRandomElement<string>(null);
					}
					list3.Clear();
					int k = 0;
					int count2 = this.allMembers.Count;
					while (k < count2)
					{
						if ((this.memberTier[this.allMembers[k]] - i == targetTier || this.memberTier[this.allMembers[k]] + i == targetTier) && this.memberHasBody[this.allMembers[k]])
						{
							list3.Add(this.allMembers[k]);
						}
						k++;
					}
					if (list3.Count > 0)
					{
						return list3.GetRandomElement<string>(null);
					}
				}
				list3.Clear();
				int l = 0;
				int count3 = list.Count;
				while (l < count3)
				{
					if (this.memberTier[list[l]] - i == targetTier || this.memberTier[list[l]] + i == targetTier)
					{
						list3.Add(list[l]);
					}
					l++;
				}
				if (list3.Count > 0)
				{
					return list3.GetRandomElement<string>(null);
				}
				list3.Clear();
				int m = 0;
				int count4 = this.allMembers.Count;
				while (m < count4)
				{
					if (this.memberTier[this.allMembers[m]] - i == targetTier || this.memberTier[this.allMembers[m]] + i == targetTier)
					{
						list3.Add(this.allMembers[m]);
					}
					m++;
				}
				if (list3.Count > 0)
				{
					return list3.GetRandomElement<string>(null);
				}
			}
			if (list2 != null && list2.Count > 0)
			{
				if (flag)
				{
					list3.Clear();
					int n = 0;
					int count5 = list2.Count;
					while (n < count5)
					{
						if (this.memberHasBody[list2[n]])
						{
							list3.Add(list2[n]);
						}
						n++;
					}
					if (list3.Count > 0)
					{
						return list3.GetRandomElement<string>(null);
					}
				}
				return list2.GetRandomElement<string>(null);
			}
			return null;
		}

		// Token: 0x0600705E RID: 28766 RVA: 0x0027F3C0 File Offset: 0x0027D5C0
		public static int GetSultanPeriodFromTier(int tier)
		{
			if (tier == 0 || tier == 1 || tier == 2)
			{
				return 5;
			}
			if (tier == 3 || tier == 4)
			{
				return 4;
			}
			if (tier == 5 || tier == 6)
			{
				return 3;
			}
			if (tier == 7)
			{
				return 2;
			}
			if (tier == 8)
			{
				return 1;
			}
			return 5;
		}

		// Token: 0x0600705F RID: 28767 RVA: 0x0027F3F0 File Offset: 0x0027D5F0
		public static string GetDefaultWallByPeriod(string period)
		{
			PopulationResult populationResult = PopulationManager.RollOneFrom("SultanDungeons_Wall_Default_Period" + period, null, null);
			if (populationResult == null)
			{
				return null;
			}
			return populationResult.Blueprint;
		}

		// Token: 0x040033B9 RID: 13241
		public string locationName;

		// Token: 0x040033BA RID: 13242
		public string regionName;

		// Token: 0x040033BB RID: 13243
		public string stairs = "";

		// Token: 0x040033BC RID: 13244
		public string monstertable = "";

		// Token: 0x040033BD RID: 13245
		public const int CHANCE_FOR_SPECIAL_HISTORIC_WALL = 25;

		// Token: 0x040033BE RID: 13246
		public long lastmem;

		// Token: 0x040033BF RID: 13247
		private static Dictionary<string, int> staticPerDungeon = new Dictionary<string, int> { { "*LightSource", 20 } };

		// Token: 0x040033C0 RID: 13248
		private int targetTier;

		// Token: 0x040033C1 RID: 13249
		private Zone targetZone;

		// Token: 0x040033C2 RID: 13250
		public Dictionary<string, int> memberTier;

		// Token: 0x040033C3 RID: 13251
		public Dictionary<string, List<string>> roleMembers;

		// Token: 0x040033C4 RID: 13252
		public List<string> allMembers;

		// Token: 0x040033C5 RID: 13253
		public Dictionary<string, bool> memberHasBody;
	}
}
