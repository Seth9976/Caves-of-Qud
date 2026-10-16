using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using Qud.API;
using XRL.EditorFormats.Map;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B02 RID: 2818
	public class CryptOfWarriors : ZoneBuilderSandbox
	{
		// Token: 0x06007095 RID: 28821 RVA: 0x00285AEC File Offset: 0x00283CEC
		public GameObject generateSarcophagusContents()
		{
			return GameObject.Create("Ancient Corpse");
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x00285AF8 File Offset: 0x00283CF8
		public GameObject generateRelic(string table)
		{
			PopulationResult populationResult = PopulationManager.RollOneFrom(table, null, null);
			string text = populationResult.Blueprint;
			if (text == "PriestCryptLoot")
			{
				text = EncountersAPI.GetAnItemBlueprint(delegate(GameObjectBlueprint B)
				{
					double partParameter = B.GetPartParameter<double>("Commerce", "Value", 0.0);
					return (B.Tier >= 5 || (B.Tier == 4 && partParameter >= 800.0)) && B.Tier <= 7 && !B.DescendsFrom("MeleeWeapon") && !B.Name.Contains("Carbide") && !B.Name.Contains("Fullerite") && !B.DescendsFrom("Corpse") && !B.DescendsFrom("Energy Cell") && !B.DescendsFrom("Liquid Fueled Energy Cell") && !B.DescendsFrom("Random Figurine") && !B.DescendsFrom("Gemstone") && partParameter >= 200.0;
				});
			}
			if (text == "RoyalCryptLoot")
			{
				text = EncountersAPI.GetAnItemBlueprint(delegate(GameObjectBlueprint B)
				{
					double partParameter2 = B.GetPartParameter<double>("Commerce", "Value", 0.0);
					return (B.Tier >= 5 || (B.Tier == 4 && partParameter2 >= 800.0)) && B.Tier <= 7 && !B.DescendsFrom("MeleeWeapon") && !B.Name.Contains("Carbide") && !B.Name.Contains("Fullerite") && !B.DescendsFrom("Corpse") && !B.DescendsFrom("Energy Cell") && !B.DescendsFrom("Liquid Fueled Energy Cell") && !B.DescendsFrom("Random Figurine") && !B.DescendsFrom("Gemstone") && partParameter2 >= 250.0;
				});
			}
			int num = 0;
			int num2 = 0;
			if (populationResult.Hint != null)
			{
				foreach (string text2 in populationResult.Hint.Split(',', StringSplitOptions.None))
				{
					if (text2 != null && text2.Contains("SetBonusModChance:"))
					{
						num = text2.Split(':', StringSplitOptions.None)[1].RollCached();
					}
					if (text2 != null && text2.Contains("SetMinModNumber:"))
					{
						num2 = text2.Split(':', StringSplitOptions.None)[1].RollCached();
					}
				}
			}
			return GameObjectFactory.Factory.CreateObject(text, num, num2, null, null, null, null, null);
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x00285C0C File Offset: 0x00283E0C
		public bool BuildZone(Zone zone)
		{
			List<string> list = new List<string>
			{
				"CryptPeriod1_N", "CryptPeriod1_S", "CryptPeriod1_E", "CryptPeriod1_W", "CryptPeriod2_N", "CryptPeriod2_S", "CryptPeriod2_E", "CryptPeriod2_W", "CryptPeriod3_N", "CryptPeriod3_S",
				"CryptPeriod3_E", "CryptPeriod3_W", "CryptPeriod4_N", "CryptPeriod4_S", "CryptPeriod4_E", "CryptPeriod4_W", "CryptPeriod5_N", "CryptPeriod5_S", "CryptPeriod5_E", "CryptPeriod5_W"
			};
			if (zone.Z == 7)
			{
				list.Add("CryptPeriod1_Large");
				list.Add("CryptPeriod2_Large");
				list.Add("CryptPeriod3_Large");
				list.Add("CryptPeriod4_Large");
				list.Add("CryptPeriod5_Large");
				The.Game.RequireSystem<CryptOfPriestsAnchorSystem>(() => new CryptOfPriestsAnchorSystem());
			}
			else
			{
				The.Game.RequireSystem<CryptOfWarriorsAnchorSystem>();
			}
			zone.FillHollowBox(new Box(0, 0, zone.Width - 1, zone.Height - 1), "EbonFulcrete");
			new SpindleFootprint().BuildZone(zone, SpindleFootprint.FootprintMode.Spindle);
			if ((zone.Y == 0 || zone.Y == 2) && (zone.X == 0 || zone.X == 1))
			{
				zone.ClearBox(new Box(zone.Width - 1, 1, zone.Width - 1, zone.Height - 2), null);
			}
			if ((zone.Y == 0 || zone.Y == 2) && (zone.X == 1 || zone.X == 2))
			{
				zone.ClearBox(new Box(0, 1, 0, zone.Height - 2), null);
			}
			if ((zone.X == 0 || zone.X == 2) && (zone.Y == 0 || zone.Y == 1))
			{
				zone.ClearBox(new Box(1, zone.Height - 1, zone.Width - 2, zone.Height - 1), null);
			}
			if ((zone.X == 0 || zone.X == 2) && (zone.Y == 1 || zone.Y == 2))
			{
				zone.ClearBox(new Box(1, 0, zone.Width - 2, 0), null);
			}
			Dictionary<string, MapFile> dictionary = new Dictionary<string, MapFile>();
			Dictionary<string, Tuple<int, int>> dictionary2 = new Dictionary<string, Tuple<int, int>>();
			foreach (string text in list)
			{
				if (!dictionary.ContainsKey(text))
				{
					dictionary.Add(text, MapFile.Resolve("preset_tile_chunks/" + text + ".rpm", true));
				}
			}
			foreach (KeyValuePair<string, MapFile> keyValuePair in dictionary)
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < 80; i++)
				{
					for (int j = 0; j < 25; j++)
					{
						if (keyValuePair.Value.Cells[i, j].Objects.Count > 0)
						{
							if (num < i + 1)
							{
								num = i + 1;
							}
							if (num2 < j + 1)
							{
								num2 = j + 1;
							}
						}
					}
				}
				dictionary2.Add(keyValuePair.Key, new Tuple<int, int>(num, num2));
			}
			List<Rect2D> maskedAreas = new List<Rect2D>();
			List<Rect2D> cryptFootprintsOnly = new List<Rect2D>();
			if (zone.X == 0 && zone.Y == 1)
			{
				zone.GetCell(70, 12).AddObject("StairsDown", null, null, null, null);
				zone.GetCell(70, 13).AddObject("StairsDown", null, null, null, null);
				maskedAreas.Add(new Rect2D(69, 11, 71, 14));
				ZoneBuilderSandbox.EnsureAllVoidsConnected(zone, false, 0.2f, null);
			}
			List<Cell> list2 = (from c in zone.GetCells()
				where c.X > 5 && c.X < 74 && c.Y > 4 && c.Y < 19 && c.Y % 4 == 0 && c.X % 8 == 0
				select c).ToList<Cell>();
			if (zone.X == 0 && zone.Y == 0)
			{
				maskedAreas.Add(new Rect2D(76, 22, 79, 24));
			}
			if (zone.X == 2 && zone.Y == 0)
			{
				maskedAreas.Add(new Rect2D(0, 22, 3, 24));
			}
			if (zone.X == 0 && zone.Y == 2)
			{
				maskedAreas.Add(new Rect2D(76, 0, 79, 2));
			}
			if (zone.X == 2 && zone.Y == 2)
			{
				maskedAreas.Add(new Rect2D(0, 0, 3, 2));
			}
			if (zone.X == 1 && zone.Y == 0)
			{
				maskedAreas.Add(new Rect2D(0, 19, 79, zone.Height - 1));
				maskedAreas.Add(new Rect2D(25, 20, 54, zone.Height - 1));
			}
			if (zone.X == 1 && zone.Y == 2)
			{
				maskedAreas.Add(new Rect2D(0, 0, 79, 4));
				maskedAreas.Add(new Rect2D(25, 4, 54, 5));
			}
			if (zone.X == 0 && zone.Y == 1)
			{
				maskedAreas.Add(new Rect2D(75, 0, 79, 24));
				maskedAreas.Add(new Rect2D(74, 6, 75, 19));
			}
			if (zone.X == 2 && zone.Y == 1)
			{
				maskedAreas.Add(new Rect2D(0, 0, 4, 24));
				maskedAreas.Add(new Rect2D(4, 6, 5, 19));
			}
			maskedAreas.Add(new Rect2D(35, 8, 45, 16));
			zone.GetCell(36, 9).AddObject("TombPillarPlacement", null, null, null, null);
			bool flag;
			do
			{
				string text2 = list.GetRandomElement<string>(null);
				Tuple<int, int> tuple = dictionary2[text2];
				Tuple<int, int> tuple2 = new Tuple<int, int>(tuple.Item1 + 2, tuple.Item2 + 2);
				string[] array = new string[] { "AB", "A", "B", "" };
				flag = false;
				using (IEnumerator<Cell> enumerator3 = list2.InRandomOrderNoAlloc<Cell>(null).GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Cell cell = enumerator3.Current;
						Cell cell2 = cell;
						Rect2D newRect = new Rect2D(cell2.X, cell2.Y, cell2.X + tuple2.Item1, cell2.Y + tuple2.Item2);
						if (cell2.X > 1 && cell2.Y > 1 && cell2.X < zone.Width - tuple2.Item1 && cell2.Y < zone.Height - tuple2.Item2 && !maskedAreas.Any<Rect2D>((Rect2D r) => r.overlaps(newRect)))
						{
							if (text2.EndsWith("_N") && cell2.Y < 12)
							{
								text2 = text2.Substring(0, text2.Length - 2) + "_S";
							}
							if (text2.EndsWith("_S") && cell2.Y > 12)
							{
								text2 = text2.Substring(0, text2.Length - 2) + "_N";
							}
							if (text2.EndsWith("_E") && cell2.X > 50)
							{
								text2 = text2.Substring(0, text2.Length - 2) + "_W";
							}
							if (text2.EndsWith("_W") && cell2.X < 30)
							{
								text2 = text2.Substring(0, text2.Length - 2) + "_E";
							}
							MapFile mapFile = dictionary[text2];
							string text3 = array.GetRandomElement<string>(null);
							string blueprint = PopulationManager.RollOneFrom("CryptFurniture", null, null).Blueprint;
							bool flag2 = false;
							for (;;)
							{
								flag = true;
								maskedAreas.Add(newRect);
								cryptFootprintsOnly.Add(new Rect2D(cell2.X + 1, cell2.Y + 1, cell2.X + tuple2.Item1 - 2, cell2.Y + tuple2.Item2 - 2));
								int num3 = 0;
								int num4 = 20;
								if (text3 != "AB" && Stat.Random(1, 100) <= num4)
								{
									text3 = "M";
								}
								EaterCryptPlaque eaterCryptPlaque = new EaterCryptPlaque();
								if (text3 == "")
								{
									eaterCryptPlaque.IsEmpty = true;
								}
								string text4;
								if (zone.Z == 8)
								{
									if (Stat.Random(0, 1) == 0)
									{
										text4 = "Warrior";
										eaterCryptPlaque.Caste = "warrior";
									}
									else
									{
										text4 = "Tutor";
										eaterCryptPlaque.Caste = "tutor";
									}
								}
								else if (Stat.Random(0, 1) == 0)
								{
									text4 = "Priest";
									eaterCryptPlaque.Caste = "priest";
								}
								else
								{
									text4 = "RoyalFamily";
									eaterCryptPlaque.Caste = "royal";
								}
								eaterCryptPlaque.GeneratePlaque();
								string text5 = "CryptReliquary_" + text4;
								Location2D location2D = Location2D.Get(0, 0);
								for (int k = 0; k < tuple.Item2; k++)
								{
									for (int l = 0; l < tuple.Item1; l++)
									{
										foreach (MapFileObjectBlueprint mapFileObjectBlueprint in mapFile.Cells[l, k].Objects)
										{
											Cell cell3 = zone.GetCell(l + cell2.X + 1, k + cell2.Y + 1);
											if (cell3 != null)
											{
												if (mapFileObjectBlueprint.Name.Contains("AnchorRoomTile"))
												{
													cell3.PaintTile = (((cell3.X + cell3.Y) % 2 == 0) ? "Tiles2/sw_floor_diamond_1.bmp" : "Tiles2/sw_floor_diamond_2.bmp");
													cell3.PaintTileColor = "&K";
													cell3.PaintColorString = "&K";
													cell3.PaintRenderString = '\u0004'.ToString();
												}
												if (mapFileObjectBlueprint.Name.Contains("CryptWall"))
												{
													GameObject gameObject = cell3.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
													Description part = gameObject.GetPart<Description>();
													if (text4 == "Warrior")
													{
														Render render = gameObject.Render;
														render.ColorString += "^r";
														part._Short += " In tracery around the margins, warriors assemble in hex phalanxes.";
													}
													if (text4 == "Tutor")
													{
														Render render2 = gameObject.Render;
														render2.ColorString += "^C";
														part._Short += " In tracery around the margins, tutors instruct pupils in domed amphitheatres.";
													}
													if (text4 == "Priest")
													{
														Render render3 = gameObject.Render;
														render3.ColorString += "^W";
														part._Short += " In tracery around the margins, priests burn incense inside of thuribles.";
													}
													if (text4 == "RoyalFamily")
													{
														Render render4 = gameObject.Render;
														render4.ColorString += "^m";
														part._Short += " In tracery around the margins, royals are extolled at sky-kited festivals.";
													}
												}
												else if (GameObjectFactory.Factory.GetBlueprint(mapFileObjectBlueprint.Name).DescendsFrom("Crypt Door") || mapFileObjectBlueprint.Name == "Crypt Door" || mapFileObjectBlueprint.Name.StartsWith("Crypt Double Door"))
												{
													GameObject gameObject2 = cell3.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
													gameObject2.RemovePart<EaterCryptPlaque>();
													gameObject2.AddPart(eaterCryptPlaque.DeepCopy(gameObject2, null), true, false);
													if (text4 == "Warrior")
													{
														gameObject2.Render.DetailColor = "r";
													}
													if (text4 == "Tutor")
													{
														gameObject2.Render.DetailColor = "C";
													}
													if (text4 == "Priest")
													{
														gameObject2.Render.DetailColor = "W";
													}
													if (text4 == "RoyalFamily")
													{
														gameObject2.Render.DetailColor = "m";
													}
												}
												else if (mapFileObjectBlueprint.Name == "Crypt Sitter")
												{
													if (text3 != "M" && text3 != "")
													{
														cell3.AddObject("Crypt Sitter Special", null, null, null, null);
													}
												}
												else if (mapFileObjectBlueprint.Name == "Crypt Sitter Special")
												{
													if (text3 != "M" && text3 != "")
													{
														cell3.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
													}
												}
												else if (mapFileObjectBlueprint.Name == "CryptFurnitureSpawner")
												{
													if (text3 != "M")
													{
														cell3.AddObject(blueprint, null, null, null, null);
													}
												}
												else if (mapFileObjectBlueprint.Name == "CryptFurnitureSpawner Flipped")
												{
													if (text3 != "M")
													{
														if (blueprint == "Eater Hologram")
														{
															cell3.AddObject("Eater Hologram Flipped", null, null, null, null);
														}
														else
														{
															cell3.AddObject(blueprint, null, null, null, null);
														}
													}
												}
												else if (mapFileObjectBlueprint.Name == "Sarcophagus")
												{
													if (text3 != "M")
													{
														GameObject gameObject3 = cell3.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
														if ((text3.Contains("A") && num3 == 0) || (text3.Contains("B") && num3 == 1))
														{
															GameObject gameObject4 = cell3.AddObject(this.generateSarcophagusContents(), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
															gameObject3.GetPart<Enclosing>().EnterEnclosure(gameObject4, null);
														}
														else
														{
															cell3.AddObject("AnchorRoomTile", null, null, null, null);
															cell3.PaintTile = (((cell3.X + cell3.Y) % 2 == 0) ? "Tiles2/sw_floor_diamond_1.bmp" : "Tiles2/sw_floor_diamond_2.bmp");
															cell3.PaintTileColor = "&K";
															cell3.PaintColorString = "&K";
															cell3.PaintRenderString = '\u0004'.ToString();
														}
													}
													else
													{
														cell3.AddObject("AnchorRoomTile", null, null, null, null);
														cell3.PaintTile = (((cell3.X + cell3.Y) % 2 == 0) ? "Tiles2/sw_floor_diamond_1.bmp" : "Tiles2/sw_floor_diamond_2.bmp");
														cell3.PaintTileColor = "&K";
														cell3.PaintColorString = "&K";
														cell3.PaintRenderString = '\u0004'.ToString();
													}
													num3++;
												}
												else if (mapFileObjectBlueprint.Name == "Reliquary")
												{
													location2D = cell3.Location;
													if (text3 != "M")
													{
														GameObject gameObject5 = cell3.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
														if (text3.Contains("A"))
														{
															GameObject gameObject6 = this.generateRelic(text5);
															gameObject5.ReceiveObject(gameObject6, false, null);
															int num5 = 0;
															if (text4 == "Priest")
															{
																num5 = 25;
															}
															if (text4 == "RoyalFamily")
															{
																num5 = 75;
															}
															if (If.Chance(num5, null, null) && gameObject6.Blueprint != "Grave Goods")
															{
																PopulationResult populationResult = PopulationManager.RollOneFrom("Cybernetics7", null, null);
																gameObject5.ReceiveObject(populationResult.Blueprint, false, 0, 0, null, null, null, null);
															}
														}
														if (text3.Contains("B"))
														{
															GameObject gameObject7 = this.generateRelic(text5);
															gameObject5.ReceiveObject(gameObject7, false, null);
															int num6 = 0;
															if (text4 == "Priest")
															{
																num6 = 25;
															}
															if (text4 == "RoyalFamily")
															{
																num6 = 75;
															}
															if (If.Chance(num6, null, null) && gameObject7.Blueprint != "Grave Goods")
															{
																PopulationResult populationResult2 = PopulationManager.RollOneFrom("Cybernetics7", null, null);
																gameObject5.ReceiveObject(populationResult2.Blueprint, false, 0, 0, null, null, null, null);
															}
														}
													}
												}
												else
												{
													cell3.AddObject(mapFileObjectBlueprint.Name, null, null, null, null);
												}
											}
										}
									}
								}
								if (text3 == "M")
								{
									InfluenceMap influenceMap = new InfluenceMap(80, 25);
									zone.SetInfluenceMapWallsDoors(influenceMap.Walls);
									influenceMap.AddSeed(location2D, true);
									ZoneBuilderSandbox.PlacePopulationInRegion(zone, influenceMap.Regions[0], "MopangoHideout", null, null);
								}
								if (flag2)
								{
									goto IL_147A;
								}
								flag2 = true;
								Vector2i vector2i = new Vector2i(0, 0);
								if (text2.EndsWith("_N"))
								{
									vector2i = new Vector2i(0, -newRect.Height - 1);
									text2 = text2.Substring(0, text2.Length - 2) + "_S";
								}
								else if (text2.EndsWith("_S"))
								{
									vector2i = new Vector2i(0, newRect.Height + 1);
									text2 = text2.Substring(0, text2.Length - 2) + "_N";
								}
								else if (text2.EndsWith("_E"))
								{
									vector2i = new Vector2i(newRect.Width + 1, 0);
									text2 = text2.Substring(0, text2.Length - 2) + "_W";
								}
								else
								{
									if (!text2.EndsWith("_W"))
									{
										goto IL_147A;
									}
									vector2i = new Vector2i(-newRect.Width - 1, 0);
									text2 = text2.Substring(0, text2.Length - 2) + "_E";
								}
								newRect = new Rect2D(newRect.x1 + vector2i.x, newRect.y1 + vector2i.y, newRect.x2 + vector2i.x, newRect.y2 + vector2i.y);
								if (!(newRect != Rect2D.invalid) || maskedAreas.Any<Rect2D>((Rect2D r) => r.overlaps(newRect)) || !new Rect2D(0, 0, 79, 24).Contains(newRect))
								{
									goto IL_147A;
								}
								mapFile = dictionary[text2];
								tuple = dictionary2[text2];
								tuple2 = new Tuple<int, int>(tuple.Item1 + 2, tuple.Item2 + 2);
								cell2 = zone.GetCell(newRect.x1, newRect.y1);
							}
						}
					}
					IL_147A:;
				}
			}
			while (flag);
			Stat.ReseedFrom(zone.ZoneID, false);
			zone.ForeachCell(delegate(Cell c)
			{
				c.SetReachable(true);
			});
			zone.GetCell(0, 0).AddObject("ConcreteFloor", null, null, null, null);
			List<Location2D> cryptExclusionAreas = new List<Location2D>();
			Action<Location2D> <>9__16;
			foreach (Rect2D rect2D in cryptFootprintsOnly)
			{
				Action<Location2D> action;
				if ((action = <>9__16) == null)
				{
					action = (<>9__16 = delegate(Location2D a)
					{
						cryptExclusionAreas.Add(a);
					});
				}
				rect2D.ForEachLocation(action);
			}
			List<Cell> list3 = (from c in zone.GetCells()
				where !cryptExclusionAreas.Contains(c.Location)
				select c).ToList<Cell>();
			List<Cell> list4 = new List<Cell>();
			foreach (Cell cell4 in zone.GetCellsWithTaggedObject("Door"))
			{
				Cell closestCellFromList = cell4.getClosestCellFromList(list3);
				list4.Add(closestCellFromList);
			}
			InfluenceMap influenceMap2 = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.LargestRegion, 100, cryptExclusionAreas, Options.GetOption("OptionDrawInfluenceMaps", "No") == "Yes", null);
			influenceMap2.Regions.ForEach(delegate(InfluenceMapRegion r)
			{
				r.Tags.Add("connected");
			});
			List<InfluenceMapRegion> list5 = influenceMap2.Regions.Where<InfluenceMapRegion>((InfluenceMapRegion r) => !r.IsEdgeRegion() && r.maxRect.Width >= 6 && r.maxRect.Height >= 6).ToList<InfluenceMapRegion>();
			list5.Sort((InfluenceMapRegion a, InfluenceMapRegion b) => b.maxRect.Area.CompareTo(a.maxRect.Area));
			List<Location2D> spots = list4.Select<Cell, Location2D>((Cell d) => d.Location).ToList<Location2D>();
			spots.AddRange(list5.Select<InfluenceMapRegion, Location2D>((InfluenceMapRegion r) => r.Center));
			if (zone.X == 0 && zone.Y == 0)
			{
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "e", 3));
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "s", 3));
			}
			if (zone.X == 2 && zone.Y == 0)
			{
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "w", 3));
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "s", 3));
			}
			if (zone.X == 0 && zone.Y == 2)
			{
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "n", 3));
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "e", 3));
			}
			if (zone.X == 2 && zone.Y == 2)
			{
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "w", 3));
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "n", 3));
			}
			if (zone.X == 1 && (zone.Y == 0 || zone.Y == 2))
			{
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "e", 3));
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "w", 3));
			}
			if (zone.Y == 1 && (zone.X == 0 || zone.X == 2))
			{
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "n", 3));
				spots.Add(base.getMatchedEdgeConnectionLocation(zone, "tombedge", "s", 3));
			}
			foreach (Location2D location2D2 in spots)
			{
				zone.GetCell(location2D2).ClearWalls();
				if (location2D2.X == 0 || location2D2.X == 79 || location2D2.Y == 0 || location2D2.Y == 24)
				{
					zone.GetCell(location2D2).GetLocalAdjacentCells().ForEach(delegate(Cell c)
					{
						c.ClearWalls();
						c.AddObject("CryptTrail", null, null, null, null);
					});
				}
			}
			if (spots.Count > 1)
			{
				Location2D source = spots[0];
				spots.Sort((Location2D a, Location2D b) => a.ManhattanDistance(source).CompareTo(b.ManhattanDistance(source)));
				base.BuildPathWithObject(zone, spots, "CryptTrail", 0, false, delegate(int x, int y, int c)
				{
					if (!spots.Contains(Location2D.Get(x2, y2)) && (x2 == 0 || y2 == 0 || x2 == 79 || y2 == 24))
					{
						return 80;
					}
					if (!zone.GetCell(x2, y2).IsPassable(null, true))
					{
						return 80;
					}
					if (maskedAreas.Any<Rect2D>((Rect2D r) => r.Contains(x2, y2)))
					{
						return int.MaxValue;
					}
					if (cryptExclusionAreas.Contains(Location2D.Get(x2, y2)))
					{
						return int.MaxValue;
					}
					if (cryptFootprintsOnly.Any<Rect2D>((Rect2D r) => r.Contains(x2, y2)))
					{
						return int.MaxValue;
					}
					return 0;
				});
			}
			if (zone.Z == 7)
			{
				foreach (InfluenceMapRegion influenceMapRegion in list5)
				{
					Grid<Color4> grid = new Grid<Color4>(influenceMapRegion.maxRect.Width, influenceMapRegion.maxRect.Height);
					grid.fromWFCTemplate("cryptgarden" + Stat.Random(1, 3).ToString(), 3, int.MaxValue);
					if (Stat.Random(1, 100) <= 80)
					{
						grid.mirrorHorizontal();
					}
					if (Stat.Random(1, 100) <= 80)
					{
						grid.mirrorVertical();
					}
					string blueprint2 = PopulationManager.RollOneFrom("CryptGardenPlants", null, null).Blueprint;
					for (int m = 0; m < influenceMapRegion.maxRect.Width; m++)
					{
						for (int n = 0; n < influenceMapRegion.maxRect.Height; n++)
						{
							Cell cell5 = zone.GetCell(m + influenceMapRegion.maxRect.x1, n + influenceMapRegion.maxRect.y1);
							if (!cell5.HasObject("CryptTrail"))
							{
								cell5.AddObject("CryptTrail", null, null, null, null);
								if (grid.get(m, n) == Color4.black)
								{
									cell5.AddObject(blueprint2, null, null, null, null);
								}
								else if (Stat.Random(1, 100) <= 5)
								{
									cell5.AddObject("Ornate Bench", null, null, null, null);
								}
							}
						}
					}
				}
				List<Cell> list6 = zone.GetCells().Where<Cell>(delegate(Cell c)
				{
					if (!c.HasObject("CryptTrail"))
					{
						return (from d in c.GetLocalAdjacentCells(1, false)
							where d.HasObject("CryptTrail")
							select d).Count<Cell>() > 0;
					}
					return false;
				}).ToList<Cell>();
				list6.ShuffleInPlace<Cell>(null);
				int num7 = 0;
				while (num7 < 16 && num7 < list6.Count)
				{
					list6[num7].AddObject("Tomb Techlight1", null, null, null, null);
					num7++;
				}
			}
			string seedid = "rocks_" + zone.ZoneID;
			zone.ForeachCell(delegate(Cell c)
			{
				int x2 = c.X;
				int y2 = c.Y;
				if (c.DistanceToEdge() > 5)
				{
					return;
				}
				if ((double)(5 - c.DistanceToEdge()) - this.sampleSimplexNoise(seedid, x2, y2, zone.Z, 0f, 5f, 0.1f, 5, 1.1f, 1f) > 0.0 && !cryptFootprintsOnly.Any<Rect2D>((Rect2D r) => r.Contains(x2, y2)) && !maskedAreas.Any<Rect2D>((Rect2D r) => r.Contains(x2, y2)) && !c.HasWall() && !c.HasObject("CryptTrail") && !c.HasObject("Burnished Azzurum"))
				{
					c.Clear(null, false, false, null);
					c.AddObject("Polished Black Marble", null, null, null, null);
				}
			});
			Grid<bool> grid2 = new Grid<bool>(80, 25);
			if (zone.X == 0 || zone.Y == 1)
			{
				grid2.setRect(0, 0, 8, 24, true);
			}
			if (zone.X == 2 || zone.Y == 1)
			{
				grid2.setRect(72, 0, 79, 24, true);
			}
			if (zone.Y == 0 || zone.X == 1)
			{
				grid2.setRect(0, 0, 79, 5, true);
			}
			if (zone.Y == 2 || zone.X == 1)
			{
				grid2.setRect(0, 19, 79, 24, true);
			}
			seedid = "forest_" + zone.ZoneID;
			grid2.forEach(delegate(int x, int y, bool b)
			{
				if (b && this.sampleSimplexNoise(seedid, x, y, zone.Z, -1f, 1f, 0.1f, 5, 1.1f, 1f) > 0.0 && !cryptFootprintsOnly.Any<Rect2D>((Rect2D r) => r.Contains(x, y)) && !maskedAreas.Any<Rect2D>((Rect2D r) => r.Contains(x, y)))
				{
					Cell cell8 = zone.GetCell(x, y);
					if (!cell8.HasWall() && !cell8.HasObject("CryptTrail"))
					{
						cell8.AddPopulation("CryptHolographicForest");
					}
				}
			});
			influenceMap2 = ZoneBuilderSandbox.GenerateInfluenceMap(zone, new List<Point>(), InfluenceMapSeedStrategy.LargestRegion, 100, cryptExclusionAreas, Options.GetOption("OptionDrawInfluenceMaps", "No") == "Yes", null);
			influenceMap2.Regions.ForEach(delegate(InfluenceMapRegion r)
			{
				r.Tags.Add("connected");
			});
			if (zone.Z == 7)
			{
				if (ZoneTemplateManager.HasTemplates("CryptOfPriests"))
				{
					ZoneTemplateManager.Templates["CryptOfPriests"].Execute(zone, influenceMap2);
				}
			}
			else if (ZoneTemplateManager.HasTemplates("CryptOfWarriors"))
			{
				ZoneTemplateManager.Templates["CryptOfWarriors"].Execute(zone, influenceMap2);
			}
			if (zone.Z == 8)
			{
				if (The.Game.RequireGameState<List<string>>("CryptOfWarriorsStairsZones", delegate
				{
					List<string> stairZones = new List<string>();
					Func<string, bool> <>9__28;
					for (int num8 = 0; num8 < 4; num8++)
					{
						List<string> stairZones2 = stairZones;
						IEnumerable<string> warriorsAllowedStairsZones = CryptOfWarriorsAnchorSystem.warriorsAllowedStairsZones;
						Func<string, bool> func;
						if ((func = <>9__28) == null)
						{
							func = (<>9__28 = (string a) => !stairZones.Contains(a));
						}
						stairZones2.Add(warriorsAllowedStairsZones.Where<string>(func).GetRandomElement<string>(null));
					}
					return stairZones;
				}).Contains(zone.ZoneID))
				{
					zone.GetEmptyCells(null, null).GetRandomElement<Cell>(null).AddObject("StairsUp", null, null, null, null);
					ZoneBuilderSandbox.EnsureAllVoidsConnected(zone, false, 0.2f, null);
				}
			}
			else if (zone.Z == 7)
			{
				if (The.Game.RequireGameState<List<string>>("CryptOfPriestsStairsZones", delegate
				{
					List<string> stairZones = new List<string>();
					Func<string, bool> <>9__29;
					for (int num9 = 0; num9 < 3; num9++)
					{
						List<string> stairZones3 = stairZones;
						IEnumerable<string> priestsAllowedStairsZones = CryptOfPriestsAnchorSystem.priestsAllowedStairsZones;
						Func<string, bool> func2;
						if ((func2 = <>9__29) == null)
						{
							func2 = (<>9__29 = (string a) => !stairZones.Contains(a));
						}
						stairZones3.Add(priestsAllowedStairsZones.Where<string>(func2).GetRandomElement<string>(null));
					}
					stairZones.Add("JoppaWorld.53.3.0.2.7");
					return stairZones;
				}).Contains(zone.ZoneID))
				{
					if (zone.ZoneID == "JoppaWorld.53.3.0.2.7")
					{
						zone.GetCell(2, 22).Clear(null, false, false, null);
						zone.GetCell(2, 22).AddObject("StairsUp", null, null, null, null);
						ZoneBuilderSandbox.EnsureAllVoidsConnected(zone, false, 0.2f, null);
					}
					else
					{
						zone.GetEmptyCells(null, null).GetRandomElement<Cell>(null).AddObject("Crypt Exit Teleporter", null, null, null, null);
					}
				}
			}
			zone.GetCell(0, 0).AddObject("Finish_TombOfTheEaters_EnterTheTombOfTheEaters", null, null, null, null);
			foreach (Cell cell6 in zone.GetCellsWithObject("StairsUp"))
			{
				base.EnsureCellReachable(zone, cell6);
			}
			foreach (Cell cell7 in zone.GetCellsWithObject("StairsDown"))
			{
				base.EnsureCellReachable(zone, cell7);
			}
			new ChildrenOfTheTomb().BuildZone(zone);
			return true;
		}

		// Token: 0x04003419 RID: 13337
		public const string wallType = "EbonFulcrete";
	}
}
