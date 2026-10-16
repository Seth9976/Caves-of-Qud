using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using UnityEngine;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.ZoneBuilders.Utility;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B0A RID: 2826
	public class VillageOutskirts : VillageBase
	{
		// Token: 0x06007195 RID: 29077 RVA: 0x002A36C0 File Offset: 0x002A18C0
		public List<PopulationResult> ResolveBuildingContents(List<PopulationResult> templateResults)
		{
			int num = 0;
			List<PopulationResult> list = new List<PopulationResult>(templateResults.Count);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (PopulationResult populationResult in templateResults)
			{
				for (int i = 0; i < populationResult.Number; i++)
				{
					if (!populationResult.Blueprint.StartsWith("*"))
					{
						list.Add(new PopulationResult(populationResult.Blueprint, 1, null, null));
					}
					else if (this.staticVillageResults.ContainsKey(populationResult.Blueprint) && !Stat.Chance(num))
					{
						list.Add(new PopulationResult(this.staticVillageResults[populationResult.Blueprint], 1, null, null));
					}
					else if (dictionary.ContainsKey(populationResult.Blueprint) && !Stat.Chance(num))
					{
						list.Add(new PopulationResult(dictionary[populationResult.Blueprint], 1, null, null));
					}
					else
					{
						PopulationResult populationResult2 = new PopulationResult(null, 1, null, null);
						string text = "DynamicSemanticTable:" + populationResult.Blueprint.Replace("*", "") + "::" + this.villageTechTier.ToString();
						populationResult2.Blueprint = PopulationManager.RollOneFrom(text, null, null).Blueprint;
						populationResult2.Hint = populationResult.Hint;
						if (string.IsNullOrEmpty(populationResult2.Blueprint))
						{
							Debug.LogError("Couldn't resolve object for " + populationResult.Blueprint);
						}
						else
						{
							list.Add(populationResult2);
							if (this.staticPerBuilding.Contains(populationResult.Blueprint) && !dictionary.ContainsKey(populationResult.Blueprint))
							{
								dictionary.Add(populationResult.Blueprint, populationResult2.Blueprint);
							}
							if (this.staticPerVillage.Contains(populationResult.Blueprint) && !this.staticVillageResults.ContainsKey(populationResult.Blueprint))
							{
								this.staticVillageResults.Add(populationResult.Blueprint, populationResult2.Blueprint);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06007196 RID: 29078 RVA: 0x002A38FC File Offset: 0x002A1AFC
		public override void addInitialStructures()
		{
			List<ISultanDungeonSegment> list = new List<ISultanDungeonSegment>();
			int num = 7;
			int num2 = 72;
			int num3 = 7;
			int num4 = 17;
			string blueprint = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_InitialStructureSegmentation"), null, "Full").Blueprint;
			if (blueprint == "None")
			{
				return;
			}
			foreach (string text in blueprint.Split(';', StringSplitOptions.None))
			{
				if (text == "FullHMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HMirror"
					});
				}
				else if (text == "FullVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "VMirror"
					});
				}
				else if (text == "FullHVMirror")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22))
					{
						mutator = "HVMirror"
					});
				}
				else if (text == "Full")
				{
					list.Add(new SultanRectDungeonSegment(new Rect2D(2, 2, 78, 22)));
				}
				else if (text.StartsWith("BSP:"))
				{
					int num5 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
					base.partition(new Rect2D(2, 2, 78, 24), ref num5, list);
				}
				else if (text.StartsWith("Ring:"))
				{
					int num6 = Convert.ToInt32(text.Split(':', StringSplitOptions.None)[1]);
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
				else if (text.StartsWith("Blocks"))
				{
					string[] array2 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
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
						SultanRectDungeonSegment sultanRectDungeonSegment = new SultanRectDungeonSegment(new Rect2D(num10, num11, num10 + num8, num11 + num9));
						if (text.Contains("[HMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HMirror";
						}
						if (text.Contains("[VMirror]"))
						{
							sultanRectDungeonSegment.mutator = "VMirror";
						}
						if (text.Contains("[HVMirror]"))
						{
							sultanRectDungeonSegment.mutator = "HVMirror";
						}
						list.Add(sultanRectDungeonSegment);
					}
				}
				else if (text.StartsWith("Circle"))
				{
					string[] array3 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanCircleDungeonSegment(Location2D.Get(array3[0].RollCached(), array3[1].RollCached()), array3[2].RollCached()));
				}
				else if (text.StartsWith("Tower"))
				{
					string[] array4 = text.Split(':', StringSplitOptions.None)[1].Split(',', StringSplitOptions.None);
					list.Add(new SultanTowerDungeonSegment(Location2D.Get(array4[0].RollCached(), array4[1].RollCached()), array4[2].RollCached(), array4[3].RollCached()));
				}
			}
			ColorOutputMap colorOutputMap = new ColorOutputMap(80, 25);
			for (int k = 0; k < list.Count; k++)
			{
				string text2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num14 = 3;
				string text3 = "";
				string blueprint2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureTemplate"), null, null).Blueprint;
				int num15 = 3;
				if (text2.Contains(","))
				{
					string[] array5 = text2.Split(',', StringSplitOptions.None);
					text2 = array5[0];
					text3 = array5[1];
				}
				WaveCollapseFastModel waveCollapseFastModel = new WaveCollapseFastModel(text2, num14, list[k].width(), list[k].height(), true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				if (!string.IsNullOrEmpty(text3))
				{
					waveCollapseFastModel.ClearColors(text3, "border1");
				}
				waveCollapseFastModel.UpdateSample(blueprint2.Split(',', StringSplitOptions.None)[0], num15, true, false, 8, 0);
				waveCollapseFastModel.Run(Stat.Random(int.MinValue, 2147483646), 0);
				ColorOutputMap colorOutputMap2 = new ColorOutputMap(waveCollapseFastModel);
				colorOutputMap2.ReplaceBorders(new Color32(byte.MaxValue, 0, 0, byte.MaxValue), new Color32(0, 0, 0, byte.MaxValue));
				if (list[k].mutator == "HMirror")
				{
					colorOutputMap2.HMirror();
				}
				if (list[k].mutator == "VMirror")
				{
					colorOutputMap2.VMirror();
				}
				if (list[k].mutator == "HVMirror")
				{
					colorOutputMap2.HMirror();
					colorOutputMap2.VMirror();
				}
				colorOutputMap.Paste(colorOutputMap2, list[k].x1, list[k].y1);
				MemoryHelper.GCCollect();
			}
			string text4 = base.RollOneFrom("Village_InitialStructureSegmentationFullscreenMutation");
			int num16 = 0;
			int num17 = 0;
			for (int l = 0; l < list.Count; l++)
			{
				string text5 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Village_StructureWall"), null, null).Blueprint;
				if (text5 == "*auto")
				{
					text5 = base.GetDefaultWall(this.zone);
				}
				for (int m = list[l].y1; m < list[l].y2; m++)
				{
					for (int n = list[l].x1; n < list[l].x2; n++)
					{
						if (list[l].contains(n, m))
						{
							for (int num18 = l + 1; num18 < list.Count; num18++)
							{
								if (list[num18].contains(n, m))
								{
									goto IL_0847;
								}
							}
							Color32 color = colorOutputMap.getPixel(n, m);
							if (list[l].HasCustomColor(n, m))
							{
								color = list[l].GetCustomColor(n, m);
							}
							if (WaveCollapseTools.equals(color, ColorOutputMap.BLACK))
							{
								this.zone.GetCell(n + num16, m + num17).ClearWalls();
								this.zone.GetCell(n + num16, m + num17).AddObject(text5, null, null, null, null);
								if (text4 == "VMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(n + num16, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HMirror" || text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, m + num17).AddObject(text5, null, null, null, null);
								}
								if (text4 == "HVMirror")
								{
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).ClearWalls();
									this.zone.GetCell(this.zone.Width - (n + num16) - 1, this.zone.Height - (m + num17) - 1).AddObject(text5, null, null, null, null);
								}
							}
						}
						IL_0847:;
					}
				}
			}
		}

		// Token: 0x06007197 RID: 29079 RVA: 0x002A4198 File Offset: 0x002A2398
		public bool BuildZone(Zone Z)
		{
			this.zone = Z;
			this.zone.SetZoneProperty("relaxedbiomes", "true");
			this.villageSnapshot = base.villageEntity.GetCurrentSnapshot();
			this.region = this.villageSnapshot.GetProperty("region", "unknown");
			this.villagerBaseFaction = this.villageSnapshot.GetProperty("baseFaction", "unknown");
			this.villageName = this.villageSnapshot.GetProperty("name", "unknown");
			Z.SetZoneProperty("villageEntityId", base.villageEntity.id);
			this.isVillageZero = this.villageSnapshot.GetProperty("isVillageZero", "false") == "true";
			Tier.Constrain(ref this.villageTier);
			base.generateVillageTheme();
			base.generateSignatureItems();
			base.generateSignatureDish("Focal");
			base.generateSignatureLiquid();
			base.generateSignatureSkill();
			this.generateStoryType();
			base.getVillageDoorStyle();
			base.makeSureThereIsEnoughSpace();
			foreach (Cell cell in Z.GetCells())
			{
				for (int i = cell.Objects.Count - 1; i >= 0; i--)
				{
					GameObject gameObject = cell.Objects[i];
					if (!gameObject.IsPlayer() && !gameObject.HasTagOrProperty("NoVillageStrip"))
					{
						if (gameObject.IsCombat)
						{
							gameObject.Physics.CurrentCell = null;
							this.originalCreatures.Add(gameObject);
						}
						else if (gameObject.IsWall() && gameObject.HasTag("Category_Settlement"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalWalls.Add(gameObject);
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Plant") || gameObject.GetBlueprint(true).InheritsFrom("BasePlant") || gameObject.HasTagOrProperty("PlantLike"))
						{
							gameObject.Physics.CurrentCell = null;
							if (gameObject != null)
							{
								this.originalPlants.Add(gameObject);
							}
						}
						else if (gameObject.HasPart<LiquidVolume>())
						{
							gameObject.Physics.CurrentCell = null;
							if (gameObject.IsOpenLiquidVolume())
							{
								this.originalLiquids.Add(gameObject);
							}
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Furniture"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalFurniture.Add(gameObject);
						}
						else if (gameObject.GetBlueprint(true).InheritsFrom("Item"))
						{
							gameObject.Physics.CurrentCell = null;
							this.originalItems.Add(gameObject);
						}
					}
				}
			}
			if (1.in100())
			{
				this.addInitialStructures();
			}
			InfluenceMap regionMap = new InfluenceMap(Z.Width, Z.Height);
			for (int j = 0; j < Z.Width; j++)
			{
				for (int k = 0; k < Z.Height; k++)
				{
					regionMap.Walls[j, k] = (Z.GetCell(j, k).HasObjectWithTagOrProperty("Wall") ? 1 : 0);
				}
			}
			regionMap.SeedAllUnseeded(false, true);
			while (regionMap.LargestSize() > 150)
			{
				regionMap.AddSeedAtMaximaInLargestSeed(false);
			}
			regionMap.SeedGrowthProbability = new List<int>();
			for (int l = 0; l < regionMap.Seeds.Count; l++)
			{
				regionMap.SeedGrowthProbability.Add(Stat.Random(10, 1000));
			}
			regionMap.Recalculate(false);
			int num = Stat.Random(4, 9);
			int num2 = 0;
			int num3 = regionMap.FindClosestSeedTo(Location2D.Get(40, 13), (InfluenceMapRegion r) => r.maxRect.ReduceBy(1, 1).Width >= 6 && r.maxRect.ReduceBy(1, 1).Height >= 6 && r.AdjacentRegions.Count > 1);
			Location2D location2D = regionMap.Seeds[num3];
			this.townSquare = regionMap.Regions[num3];
			this.townSquareLayout = null;
			int num4 = Stat.Random(0, 3);
			foreach (InfluenceMapRegion influenceMapRegion in regionMap.Regions)
			{
				if (num4 <= 0)
				{
					break;
				}
				num4--;
				Rect2D Rect = GridTools.MaxRectByArea(influenceMapRegion.GetGrid(), false, 0, 0).Translate(influenceMapRegion.BoundingBox.UpperLeft).ReduceBy(1, 1);
				PopulationLayout populationLayout = new PopulationLayout(Z, influenceMapRegion, Rect);
				if (influenceMapRegion.AdjacentRegions.Count <= 1 && influenceMapRegion.Size >= 9 && !influenceMapRegion.IsEdgeRegion() && influenceMapRegion != this.townSquare)
				{
					this.buildings.Add(populationLayout);
				}
				else if ((Rect.Width >= 6 && Rect.Height >= 6 && num > 0) || influenceMapRegion == this.townSquare)
				{
					List<GameObject> originalLiquids = this.originalLiquids;
					string text;
					if (originalLiquids == null)
					{
						text = null;
					}
					else
					{
						GameObject randomElement = originalLiquids.GetRandomElement<GameObject>(null);
						text = ((randomElement != null) ? randomElement.Blueprint : null);
					}
					string text2 = text ?? base.getZoneDefaultLiquid(this.zone);
					if (influenceMapRegion == this.townSquare)
					{
						this.townSquareLayout = populationLayout;
						if (base.fabricateStoryBuilding())
						{
							this.buildings.Add(populationLayout);
						}
					}
					else
					{
						string text3 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingStyle"), null, null).Blueprint;
						if (text3.StartsWith("wfc,") && !base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]).Any<ColorOutputMap>((ColorOutputMap t) => t.extrawidth <= Rect.Width && t.extraheight <= Rect.Height))
						{
							text3 = "squarehut";
						}
						this.buildings.Add(populationLayout);
						if (text3 == "burrow")
						{
							base.FabricateBurrow(populationLayout);
						}
						if (text3 == "aerie")
						{
							base.FabricateAerie(populationLayout);
						}
						if (text3 == "pond")
						{
							base.FabricatePond(populationLayout, text2);
						}
						if (text3 == "islandpond")
						{
							base.FabricateIslandPond(populationLayout, text2);
						}
						if (text3 == "walledpond")
						{
							base.FabricateWalledPond(populationLayout, text2);
						}
						if (text3 == "walledislandpond")
						{
							base.FabricateWalledIslandPond(populationLayout, text2);
						}
						if (text3 == "tent")
						{
							base.FabricateTent(populationLayout);
						}
						if (text3 == "roundhut")
						{
							base.FabricateHut(populationLayout, true);
						}
						if (text3 == "squarehut")
						{
							base.FabricateHut(populationLayout, false);
						}
						if (text3.StartsWith("wfc,"))
						{
							base.getWfcBuildingTemplate(text3.Split(',', StringSplitOptions.None)[1]).ShuffleInPlace<ColorOutputMap>(null);
							bool flag = false;
							foreach (ColorOutputMap colorOutputMap in base.getWfcBuildingTemplate("huts"))
							{
								int num5 = colorOutputMap.width / 2;
								int num6 = colorOutputMap.height / 2;
								if (colorOutputMap.extrawidth <= populationLayout.innerRect.Width && colorOutputMap.extraheight <= populationLayout.innerRect.Height)
								{
									for (int m = 0; m < colorOutputMap.width; m++)
									{
										for (int n = 0; n < colorOutputMap.height; n++)
										{
											Cell cell2 = Z.GetCell(populationLayout.position.X - num5 + m, populationLayout.position.Y - num6 + n);
											if (cell2 != null && colorOutputMap.getPixel(m, n).Equals(ColorOutputMap.BLACK))
											{
												cell2.AddObject(base.getAVillageWall(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
											}
										}
									}
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								base.FabricateHut(populationLayout, false);
							}
						}
						num--;
						num2++;
					}
				}
				else if (influenceMapRegion.AdjacentRegions.Count == 1 && !influenceMapRegion.IsEdgeRegion() && this.townSquare != influenceMapRegion)
				{
					VillageBase.MakeCaveBuilding(Z, regionMap, influenceMapRegion, "Shale");
					this.buildings.Add(populationLayout);
				}
			}
			regionMap.SeedAllUnseeded(false, true);
			base.CarvePathwaysFromLocations(Z, true, regionMap, location2D);
			this.zone.ClearReachableMap(false);
			this.zone.BuildReachableMap(location2D.X, location2D.Y);
			base.SnakeToConnections(Location2D.Get(location2D.X, location2D.Y));
			base.clearDegenerateDoors();
			base.applyDoorFilters();
			for (int num7 = 0; num7 < Z.Width; num7++)
			{
				for (int num8 = 0; num8 < Z.Height; num8++)
				{
					regionMap.Walls[num7, num8] = (Z.GetCell(num7, num8).HasObjectWithTag("Wall") ? 1 : 0);
				}
			}
			List<Location2D> list = new List<Location2D>();
			foreach (PopulationLayout populationLayout2 in this.buildings)
			{
				regionMap.Seeds[populationLayout2.region.Seed] = populationLayout2.position;
				list.Add(populationLayout2.position);
			}
			regionMap.Recalculate(false);
			InfluenceMap influenceMap = regionMap.copy();
			using (Pathfinder pathfinder = this.zone.getPathfinder(null))
			{
				NoiseMap noiseMap = new NoiseMap(80, 25, 10, 3, 3, 4, 80, 80, 6, 3, -3, 1, new List<NoiseMapNode>());
				for (int num9 = 0; num9 < this.zone.Height; num9++)
				{
					for (int num10 = 0; num10 < this.zone.Width; num10++)
					{
						if (this.zone.GetCell(num10, num9).HasWall())
						{
							pathfinder.CurrentNavigationMap[num10, num9] = 999;
						}
						else
						{
							pathfinder.CurrentNavigationMap[num10, num9] = noiseMap.Noise[num10, num9];
						}
					}
				}
				foreach (PopulationLayout populationLayout3 in this.buildings)
				{
					foreach (Location2D location2D2 in populationLayout3.region.Cells)
					{
						int x = location2D2.X;
						int y = location2D2.Y;
						if (x != 0 && x != 79 && y != 0 && y != 24 && Z.GetCell(x, y).IsEmpty())
						{
							int num11 = 0;
							int num12 = 0;
							if (Z.GetCell(x - 1, y).HasWall() || Z.GetCell(x - 1, y).HasObjectWithTag("Door"))
							{
								num12++;
							}
							if (Z.GetCell(x + 1, y).HasWall() || Z.GetCell(x + 1, y).HasObjectWithTag("Door"))
							{
								num12++;
							}
							if (Z.GetCell(x, y - 1).HasWall() || Z.GetCell(x, y - 1).HasObjectWithTag("Door"))
							{
								num11++;
							}
							if (Z.GetCell(x, y + 1).HasWall() || Z.GetCell(x, y + 1).HasObjectWithTag("Door"))
							{
								num11++;
							}
							if ((num11 == 2 && num12 == 0) || (num11 == 0 && num12 == 2))
							{
								influenceMap.Walls[x, y] = 1;
							}
							if (this.burrowedDoors.Contains(Location2D.Get(x, y)))
							{
								influenceMap.Walls[x, y] = 1;
							}
						}
					}
				}
				influenceMap.Recalculate(false);
				foreach (PopulationLayout populationLayout4 in this.buildings)
				{
					string blueprint = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingFloor"), null, null).Blueprint;
					string text4 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_BuildingPath"), null, null).Blueprint;
					if (text4 == "Pond")
					{
						text4 = base.getZoneDefaultLiquid(this.zone);
					}
					if (pathfinder.FindPath(populationLayout4.position, location2D, false, true, 9999, false))
					{
						foreach (PathfinderNode pathfinderNode in pathfinder.Steps)
						{
							if (!string.IsNullOrEmpty(text4))
							{
								this.zone.GetCell(pathfinderNode.pos).AddObject(text4, null, null, null, null);
							}
							if (!this.buildingPaths.Contains(pathfinderNode.pos))
							{
								this.buildingPaths.Add(pathfinderNode.pos);
							}
						}
					}
					foreach (Location2D location2D3 in populationLayout4.region.Cells)
					{
						if (!Z.GetCell(location2D3).HasWall() && !this.buildingPaths.Contains(location2D3))
						{
							if (influenceMap.Regions.Count<InfluenceMapRegion>() <= populationLayout4.region.Seed)
							{
								populationLayout4.outside.Add(location2D3);
								int num13 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
								if (num13 > 0)
								{
									populationLayout4.outsideWall.Add(location2D3);
								}
								if (num13 >= 2)
								{
									populationLayout4.outsideCorner.Add(location2D3);
								}
							}
							else if (!influenceMap.Regions[populationLayout4.region.Seed].Cells.Contains(location2D3))
							{
								populationLayout4.outside.Add(location2D3);
								int num14 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
								if (num14 > 0)
								{
									populationLayout4.outsideWall.Add(location2D3);
								}
								if (num14 >= 2)
								{
									populationLayout4.outsideCorner.Add(location2D3);
								}
							}
							else
							{
								populationLayout4.inside.Add(location2D3);
								if (!string.IsNullOrEmpty(blueprint))
								{
									Z.GetCell(location2D3).AddObject(blueprint, null, null, null, null);
								}
								int num15 = Z.GetCell(location2D3).CountObjectWithTagCardinalAdjacent("Wall");
								if (num15 > 0)
								{
									populationLayout4.insideWall.Add(location2D3);
								}
								if (num15 >= 2)
								{
									populationLayout4.insideCorner.Add(location2D3);
								}
							}
						}
					}
				}
			}
			Dictionary<InfluenceMapRegion, Rect2D> dictionary = new Dictionary<InfluenceMapRegion, Rect2D>();
			Dictionary<InfluenceMapRegion, string> dictionary2 = new Dictionary<InfluenceMapRegion, string>();
			InfluenceMap influenceMap2 = new InfluenceMap(Z.Width, Z.Height);
			influenceMap2.Seeds = new List<Location2D>(regionMap.Seeds);
			Z.SetInfluenceMapWalls(influenceMap2.Walls);
			influenceMap2.Recalculate(false);
			int num16 = 0;
			for (int num17 = 0; num17 < influenceMap2.Regions.Count; num17++)
			{
				InfluenceMapRegion R = influenceMap2.Regions[num17];
				Rect2D rect2D;
				if (!dictionary.ContainsKey(R))
				{
					rect2D = GridTools.MaxRectByArea(R.GetGrid(), false, 0, 0).Translate(R.BoundingBox.UpperLeft);
					dictionary.Add(R, rect2D);
				}
				else
				{
					rect2D = dictionary[R];
				}
				if (num17 != num3)
				{
					if (list.Contains(regionMap.Seeds[R.Seed]))
					{
						dictionary2.Add(R, "building");
						PopulationLayout populationLayout5 = this.buildings.First<PopulationLayout>((PopulationLayout b) => b.position == regionMap.Seeds[R.Seed]);
						string text5 = base.RollOneFrom("Villages_BuildingTheme_" + this.villageTheme);
						using (List<PopulationResult>.Enumerator enumerator7 = this.ResolveBuildingContents(PopulationManager.Generate(base.ResolvePopulationTableName("Villages_BuildingContents_Dwelling_" + text5), null, null, null, null, null)).GetEnumerator())
						{
							while (enumerator7.MoveNext())
							{
								PopulationResult populationResult = enumerator7.Current;
								base.PlaceObjectInBuilding(populationResult, populationLayout5, null);
							}
							goto IL_191B;
						}
					}
					if (rect2D.Area >= 4)
					{
						dictionary2.Add(R, "greenspace");
						if (num16 == 0 && this.signatureHistoricObjectInstance != null)
						{
							string text6 = "IronFence";
							string text7 = "Iron Gate";
							Z.GetCell(rect2D.Center).AddObject(this.signatureHistoricObjectInstance, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							ZoneBuilderSandbox.encloseRectWithWall(this.zone, new Rect2D(rect2D.Center.x - 1, rect2D.Center.y - 1, rect2D.Center.x + 1, rect2D.Center.y + 1), text6);
							Z.GetCell(rect2D.Center).GetCellFromDirection(Directions.GetRandomCardinalDirection(), true).Clear(null, false, false, null)
								.AddObject(text7, null, null, null, null);
						}
						else
						{
							string blueprint2 = PopulationManager.RollOneFrom(base.ResolvePopulationTableName("Villages_GreenspaceContents"), null, null).Blueprint;
							int num18 = 20;
							if (blueprint2 == "farm" && rect2D.Area >= num18 && rect2D.Width >= 6 && rect2D.Height <= 6)
							{
								rect2D = rect2D.ReduceBy(1, 1).Clamp(1, 1, 78, 23);
								if (rect2D.Width <= 5 || rect2D.Height <= 5)
								{
									goto IL_191B;
								}
								Location2D location = rect2D.GetRandomDoorCell("?", 2).location;
								ZoneBuilderSandbox.PlaceObjectOnRect(Z, "BrinestalkFence", rect2D, false);
								base.GetCell(Z, location).Clear(null, false, false, null);
								base.GetCell(Z, location).AddObject("Brinestalk Gate", null, null, null, null);
								string cellSide = rect2D.GetCellSide(location.Point);
								Rect2D rect2D2 = rect2D.ReduceBy(0, 0);
								int num19 = 0;
								if (cellSide == "N")
								{
									num19 = ((Stat.Random(0, 1) == 0) ? 2 : 3);
								}
								if (cellSide == "S")
								{
									num19 = ((Stat.Random(0, 1) == 0) ? 0 : 1);
								}
								if (cellSide == "E")
								{
									num19 = ((Stat.Random(0, 1) == 0) ? 0 : 3);
								}
								if (cellSide == "W")
								{
									num19 = ((Stat.Random(0, 1) == 0) ? 1 : 2);
								}
								if (num19 == 0 || num19 == 1)
								{
									rect2D2.y2 = rect2D2.y1 + 3;
								}
								else
								{
									rect2D2.y1 = rect2D2.y2 - 3;
								}
								if (num19 == 0 || num19 == 3)
								{
									rect2D2.x2 = rect2D2.x1 + 3;
								}
								else
								{
									rect2D2.x1 = rect2D2.x2 - 3;
								}
								base.ClearRect(Z, rect2D2);
								ZoneBuilderSandbox.PlaceObjectOnRect(Z, base.getAVillageWall(), rect2D2, false);
								Location2D location2 = rect2D2.GetRandomDoorCell(cellSide, 1).location;
								Z.GetCell(location2).Clear(null, false, false, null);
								Z.GetCell(location2).AddObject(base.getAVillageDoor(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
								this.burrowedDoors.Add(Location2D.Get(location2.X, location2.Y));
								ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D.ReduceBy(1, 1), base.ResolvePopulationTableName("Villages_FarmAnimals"));
								ZoneBuilderSandbox.PlacePopulationInRect(Z, rect2D2.ReduceBy(1, 1), base.ResolvePopulationTableName("Villages_FarmHutContents"));
							}
							else if (blueprint2 == "garden" || blueprint2 == "farm")
							{
								int num20 = Stat.Random(1, 4);
								GameObject afarmablePlant = base.getAFarmablePlant();
								if (num20 == 1)
								{
									List<GameObject> originalLiquids2 = this.originalLiquids;
									string text8;
									if (originalLiquids2 == null)
									{
										text8 = null;
									}
									else
									{
										GameObject randomElement2 = originalLiquids2.GetRandomElement<GameObject>(null);
										text8 = ((randomElement2 != null) ? randomElement2.Blueprint : null);
									}
									string text9 = text8 ?? "SaltyWaterPuddle";
									bool flag2 = Stat.Random(1, 100) <= 33;
									for (int num21 = R.BoundingBox.x1; num21 <= R.BoundingBox.x2; num21++)
									{
										for (int num22 = R.BoundingBox.x1; num22 <= R.BoundingBox.x2; num22++)
										{
											if (num21 % 2 == 0)
											{
												if (R.Cells.Contains(Location2D.Get(num21, num22)) && !this.buildingPaths.Contains(Location2D.Get(num21, num22)))
												{
													Cell cell3 = Z.GetCell(num21, num22);
													if (cell3 != null)
													{
														cell3.AddObject(afarmablePlant.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
													}
												}
											}
											else if (flag2 && R.Cells.Contains(Location2D.Get(num21, num22)) && Z.GetCell(num21, num22) != null)
											{
												Z.GetCell(num21, num22).AddObject(text9, null, null, null, null);
											}
										}
									}
								}
								if (num20 == 2)
								{
									List<GameObject> originalLiquids3 = this.originalLiquids;
									string text10;
									if (originalLiquids3 == null)
									{
										text10 = null;
									}
									else
									{
										GameObject randomElement3 = originalLiquids3.GetRandomElement<GameObject>(null);
										text10 = ((randomElement3 != null) ? randomElement3.Blueprint : null);
									}
									string text11 = text10 ?? "SaltyWaterPuddle";
									bool flag3 = Stat.Random(1, 100) <= 33;
									for (int num23 = R.BoundingBox.x1; num23 <= R.BoundingBox.x2; num23++)
									{
										for (int num24 = R.BoundingBox.x1; num24 <= R.BoundingBox.x2; num24++)
										{
											if (num24 % 2 == 0)
											{
												if (R.Cells.Contains(Location2D.Get(num23, num24)) && !this.buildingPaths.Contains(Location2D.Get(num23, num24)))
												{
													Cell cell4 = Z.GetCell(num23, num24);
													if (cell4 != null)
													{
														cell4.AddObject(afarmablePlant.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
													}
												}
											}
											else if (flag3 && R.Cells.Contains(Location2D.Get(num23, num24)) && Z.GetCell(num23, num24) != null)
											{
												Z.GetCell(num23, num24).AddObject(text11, null, null, null, null);
											}
										}
									}
								}
								if (num20 == 3)
								{
									int num25 = Stat.Random(20, 98);
									for (int num26 = R.BoundingBox.x1; num26 <= R.BoundingBox.x2; num26++)
									{
										for (int num27 = R.BoundingBox.x1; num27 <= R.BoundingBox.x2; num27++)
										{
											if (R.Cells.Contains(Location2D.Get(num26, num27)) && !this.buildingPaths.Contains(Location2D.Get(num26, num27)) && Stat.Random(1, 100) <= num25)
											{
												Cell cell5 = Z.GetCell(num26, num27);
												if (cell5 != null)
												{
													cell5.AddObject(afarmablePlant.Blueprint, new Action<GameObject>(base.setVillageDomesticatedProperties), null);
												}
											}
										}
									}
								}
								if (num20 == 4)
								{
									int num28 = Stat.Random(20, 98);
									for (int num29 = R.BoundingBox.x1; num29 <= R.BoundingBox.x2; num29++)
									{
										for (int num30 = R.BoundingBox.x1; num30 <= R.BoundingBox.x2; num30++)
										{
											if (R.Cells.Contains(Location2D.Get(num29, num30)) && !this.buildingPaths.Contains(Location2D.Get(num29, num30)) && Stat.Random(1, 100) <= num28)
											{
												Cell cell6 = Z.GetCell(num29, num30);
												if (cell6 != null)
												{
													cell6.AddObject(base.getAFarmablePlant(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
												}
											}
										}
									}
								}
							}
						}
						num16++;
					}
					else if (influenceMap2.SeedToRegionMap[R.Seed].AdjacentRegions.Count == 1)
					{
						dictionary2.Add(R, "cubby");
					}
					else
					{
						dictionary2.Add(R, "hall");
					}
				}
				IL_191B:;
			}
			this.buildings.RemoveAll((PopulationLayout b) => b.inside.Count == 0 && b.outside.Count == 0);
			if (this.villageSnapshot.GetProperty("abandoned", "unknown") == "true")
			{
				int num31 = 1;
				try
				{
					num31 = Convert.ToInt32(this.villageSnapshot.GetProperty("ruinScale", "unknown"));
					if (num31 < 1)
					{
						num31 = 1;
					}
					if (num31 > 4)
					{
						num31 = 4;
					}
				}
				catch (Exception ex)
				{
					global::Logger.Exception(ex);
				}
				if (num31 > 1)
				{
					int num32 = 10;
					if (num31 == 3)
					{
						num32 = 50;
					}
					if (num31 == 4)
					{
						num32 = 100;
					}
					new Ruiner().RuinZone(Z, num32, false, 100000, 500000);
					foreach (GameObject gameObject2 in this.originalPlants)
					{
						ZoneBuilderSandbox.PlaceObject(gameObject2, Z, null);
					}
				}
				if (If.Chance(70, null, null))
				{
					foreach (GameObject gameObject3 in this.originalCreatures)
					{
						ZoneBuilderSandbox.PlaceObject(gameObject3, Z, null);
					}
				}
				Z.ReplaceAll("Torchpost", "Unlit Torchpost");
				Z.ReplaceAll("Sconce", "Unlit Torchpost");
			}
			else
			{
				int num33 = Stat.Random(0, 4);
				for (int num34 = 0; num34 < num33; num34++)
				{
					ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), base.generateVillager(false), null, null, false);
				}
				Z.ForeachObjectWithPart("SecretObject", delegate(GameObject obj)
				{
					obj.RemovePart<SecretObject>();
				});
				Z.ForeachObjectWithPart("AIPilgrim", delegate(GameObject obj)
				{
					obj.RemovePart<AIPilgrim>();
				});
				Z.ForeachObjectWithPart("ConvertSpawner", delegate(GameObject obj)
				{
					obj.RemovePart<ConvertSpawner>();
				});
				foreach (GameObject gameObject4 in this.originalFurniture)
				{
					ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject4, null, null, false);
				}
				foreach (GameObject gameObject5 in this.originalItems)
				{
					ZoneBuilderSandbox.PlaceObject(Z, regionMap.Regions.GetRandomElement<InfluenceMapRegion>(null), gameObject5, null, null, false);
				}
				Z.ForeachObject(delegate(GameObject o)
				{
					if (o.GetBlueprint(true).InheritsFrom("Furniture") && o.Physics != null)
					{
						o.Physics.Owner = this.villageFaction;
					}
					if (this.villageSnapshot.listProperties.ContainsKey("signatureLiquids") && o.GetBlueprint(true).HasTag("Vessel", false) && If.Chance(80, null, null))
					{
						LiquidVolume liquidVolume = o.LiquidVolume;
						if (liquidVolume != null)
						{
							liquidVolume.InitialLiquid = this.villageSnapshot.GetList("signatureLiquids").GetRandomElement<string>(null);
						}
					}
				});
			}
			if (Z.HasBuilder("RiverBuilder"))
			{
				bool flag4 = false;
				List<GameObject> originalLiquids4 = this.originalLiquids;
				string text12;
				if (originalLiquids4 == null)
				{
					text12 = null;
				}
				else
				{
					GameObject randomElement4 = originalLiquids4.GetRandomElement<GameObject>(null);
					text12 = ((randomElement4 != null) ? randomElement4.Blueprint : null);
				}
				new RiverBuilder(flag4, text12 ?? "SaltyWaterDeepPool", false).BuildZone(Z);
			}
			if (Z.HasBuilder("RoadBuilder"))
			{
				new RoadBuilder(false).BuildZone(Z);
			}
			string text13 = ((this.villageSnapshot.GetProperty("abandoned", "unknown") == "true") ? Stat.Random(5, 25).ToString() : (10 - this.villageTechTier).ToString());
			PowerGrid powerGrid = new PowerGrid();
			powerGrid.DamageChance = text13;
			if ((5 + this.villageTechTier * 2).in100())
			{
				powerGrid.MissingConsumers = "1d6";
				powerGrid.MissingProducers = "1d3";
			}
			powerGrid.BuildZone(Z);
			Hydraulics hydraulics = new Hydraulics();
			hydraulics.DamageChance = text13;
			if ((5 + this.villageTechTier * 2).in100())
			{
				hydraulics.MissingConsumers = "1d6";
				hydraulics.MissingProducers = "1d3";
			}
			hydraulics.BuildZone(Z);
			MechanicalPower mechanicalPower = new MechanicalPower();
			mechanicalPower.DamageChance = text13;
			if ((15 - this.villageTechTier).in100())
			{
				mechanicalPower.MissingConsumers = "1d6";
				mechanicalPower.MissingProducers = "1d3";
			}
			mechanicalPower.BuildZone(Z);
			Z.SetMusic("Music/Mehmets Book on Strings");
			Z.FireEvent("VillageInit");
			this.cleanup();
			return true;
		}

		// Token: 0x040034B7 RID: 13495
		private string[] staticPerVillage = new string[] { "*WhateverTagsYouWant" };

		// Token: 0x040034B8 RID: 13496
		private string[] staticPerBuilding = new string[] { "*LightSource" };

		// Token: 0x040034B9 RID: 13497
		private Dictionary<string, string> staticVillageResults = new Dictionary<string, string>();
	}
}
