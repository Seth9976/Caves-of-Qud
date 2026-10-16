using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB7 RID: 2743
	public class RedrockStockadeMaker : ZoneBuilderSandbox
	{
		// Token: 0x06006F6D RID: 28525 RVA: 0x0026F378 File Offset: 0x0026D578
		public bool BuildZone(Zone Z, bool ClearCombatObjectsFirst, string WallObject, string ZoneTable, string Widgets, string CustomBigRoomPopulation = null, string CustomBigAlternateRoomPopulation = null, string CustomSmallRoomPopulation = null, string CustomSmallAlternateRoomPopulation = null, string BoxWidth = "30-50", string BoxHeight = "16-24", bool SpecialRedrockBuilder = false, string InsideStocakdeTable = null)
		{
			Box box = null;
			Box box2 = null;
			int i = 30;
			int num = Convert.ToInt32(Stat.Roll(BoxWidth, null));
			int num2 = Convert.ToInt32(Stat.Roll(BoxHeight, null));
			while (i > 0)
			{
				List<Box> list = Tools.GenerateBoxes(BoxGenerateOverlap.Irrelevant, new Range(1, 1), new Range(num), new Range(num2), null, null, null);
				if (list == null || list.Count <= 0)
				{
					return false;
				}
				box = list[0].Grow(-1);
				box2 = box;
				if (!list[0].rect.allPoints().Any<Location2D>((Location2D p) => Z.GetCell(p).HasObject("StairsUp")))
				{
					for (int j = list[0].y1; j <= list[0].y2; j++)
					{
						for (int k = list[0].x1; k <= list[0].x2; k++)
						{
							if (Z.IsReachable(k, j))
							{
								goto IL_0117;
							}
						}
					}
					i--;
					continue;
					IL_0117:
					Z.ClearBox(box, "CanyonMarker", null, null);
					if (ClearCombatObjectsFirst)
					{
						for (int l2 = box.y1; l2 <= box.y2; l2++)
						{
							for (int m = box.x1; m <= box.x2; m++)
							{
								for (;;)
								{
									using (List<GameObject>.Enumerator enumerator = Z.GetCell(m, l2).GetObjectsWithPart("Combat").GetEnumerator())
									{
										if (enumerator.MoveNext())
										{
											GameObject gameObject = enumerator.Current;
											Z.GetCell(m, l2).RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
											continue;
										}
									}
									break;
								}
							}
						}
					}
					Z.ClearBox(box, "CanyonMarker", null, null);
					for (int n = box.y1; n <= box.y2; n++)
					{
						for (int num3 = box.x1; num3 <= box.x2; num3++)
						{
							Dirty.PaintCell(Z.GetCell(num3, n));
						}
					}
					Z.FillRoundHollowBox(box, WallObject);
					Z.FillRoundHollowBox(box.Grow(-1), WallObject);
					List<Location2D> list2 = box.Grow(-2).rect.locations.ToList<Location2D>();
					List<Box> list3 = new List<Box>();
					if (box2.Valid)
					{
						using (List<Box>.Enumerator enumerator2 = Tools.GenerateBoxes(new List<Box>(), BoxGenerateOverlap.NeverOverlap, new Range(1, 8), new Range(9, 40), new Range(8, 14), new Range(6, 999), new Range(box2.x1, box2.x2), new Range(box2.y1, box2.y2)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Box B = enumerator2.Current;
								if (B.Valid && B.Grow(-1).Valid)
								{
									if (B.x1 == box.x1 || B.x2 == box.x2 || B.y1 == box.y1 || B.y2 == box.y2)
									{
										Z.FillRoundHollowBox(B, WallObject);
										list3.Add(B);
										list2.RemoveAll((Location2D l) => B.rect.locations.Contains(l));
									}
									else
									{
										Z.FillRoundHollowBox(B.Grow(-1), WallObject);
										list3.Add(B.Grow(-1));
										list2.RemoveAll((Location2D l) => B.Grow(-1).rect.locations.Contains(l));
									}
								}
							}
						}
					}
					bool flag = false;
					if ((double)num >= BoxWidth.GetCachedDieRoll().Average() && (double)num2 >= BoxHeight.GetCachedDieRoll().Average())
					{
						flag = true;
					}
					int num4 = 0;
					foreach (Box box3 in list3)
					{
						if (CustomBigRoomPopulation != null)
						{
							bool flag2 = 5.in100();
							string text = ((!flag2) ? CustomSmallRoomPopulation : CustomSmallAlternateRoomPopulation);
							if (box3.Volume > 25 && box3.Height > 6 && box3.Width > 4)
							{
								if (flag)
								{
									if (flag2)
									{
										ZoneBuilderSandbox.PlacePopulationInRect(Z, box3.rect, "LegendaryRedrockSnapjawRemains");
									}
									else
									{
										ZoneBuilderSandbox.PlacePopulationInRect(Z, box3.rect, "LegendaryRedrockSnapjaw");
									}
									flag = false;
								}
								text = ((!flag2) ? CustomBigRoomPopulation : CustomBigAlternateRoomPopulation);
								ZoneBuilderSandbox.PlacePopulationInRect(Z, box3.rect, text);
							}
							else
							{
								ZoneBuilderSandbox.PlacePopulationInRect(Z, box3.rect, text);
							}
						}
						else if (box3.Volume > 25 && box3.Height > 6 && box3.Width > 4)
						{
							BuildingTemplate buildingTemplate = new BuildingTemplate(box3.Width - 2, box3.Height - 2, 1, true);
							for (int num5 = 1; num5 < buildingTemplate.Height - 1; num5++)
							{
								for (int num6 = 1; num6 < buildingTemplate.Width - 1; num6++)
								{
									if (buildingTemplate.Map[num6, num5] == BuildingTemplateTile.Wall)
									{
										Z.GetCell(num6 + box3.x1, num5 + box3.y1).AddObject(WallObject, null, null, null, null);
									}
									if (buildingTemplate.Map[num6, num5] == BuildingTemplateTile.Door)
									{
										Z.GetCell(num6 + box3.x1, num5 + box3.y1).ClearWalls();
									}
								}
							}
						}
						i = 0;
						while (i < 1000)
						{
							i++;
							int num7;
							int num8;
							if (Stat.Random(0, 1) == 0)
							{
								num7 = Stat.Random(box3.x1 + 2, box3.x2 - 2);
								if (Stat.Random(0, 1) == 0)
								{
									num8 = box3.y1;
								}
								else
								{
									num8 = box3.y2;
								}
							}
							else
							{
								num8 = Stat.Random(box3.y1 + 2, box3.y2 - 2);
								if (Stat.Random(0, 1) == 0)
								{
									num7 = box3.x1;
								}
								else
								{
									num7 = box3.x2;
								}
							}
							if (num7 != box.x1 && num7 != box.x2 && num8 != box.y1 && num8 != box.y2 && ((!Z.GetCell(num7 - 1, num8).HasWall() && !Z.GetCell(num7 + 1, num8).HasWall()) || (!Z.GetCell(num7, num8 - 1).HasWall() && !Z.GetCell(num7, num8 + 1).HasWall())))
							{
								Z.GetCell(num7, num8).ClearWalls();
								break;
							}
						}
					}
					if (num4 == 0)
					{
						for (i = 1000; i > 0; i--)
						{
							Point randomPoint = box.Grow(-1).RandomPoint;
							if (Z.GetCell(randomPoint.X, randomPoint.Y).IsEmpty())
							{
								break;
							}
						}
					}
					if (ZoneTable != null)
					{
						string[] array = ZoneTable.Split(',', StringSplitOptions.None);
						for (int num9 = 0; num9 < array.Length; num9++)
						{
							foreach (PopulationResult populationResult in PopulationManager.Generate(array[num9], "zonetier", Z.NewTier.ToString(), null, null, null))
							{
								ZoneBuilderSandbox.PlaceObjectInArea(Z, Z.area, GameObjectFactory.Factory.CreateObject(populationResult.Blueprint), 0, 0, populationResult.Hint, null, false);
							}
						}
					}
					if (Widgets != null)
					{
						foreach (string text2 in Widgets.Split(',', StringSplitOptions.None))
						{
							Z.GetCell(0, 0).AddObject(text2, null, null, null, null);
						}
					}
					for (int num10 = 0; num10 < 2; num10++)
					{
						int num11 = Stat.Random(1, 15);
						if ((num11 & 1) != 0)
						{
							int num12 = Stat.Random(box.x1 + 3, box.x2 - 3);
							if (!Z.GetCell(num12, box.y1 + 2).HasWall() && Z.GetCell(num12, box.y1).HasWall())
							{
								Z.GetCell(num12, box.y1).ClearWalls();
								Z.GetCell(num12, box.y1).AddObject("BrinestalkArrowslit", null, null, null, null);
								Z.GetCell(num12, box.y1 + 1).ClearWalls();
								Z.GetCell(num12, box.y1 + 1).AddObject("BrinestalkArrowslit", null, null, null, null);
							}
						}
						if ((num11 & 2) != 0)
						{
							int num12 = Stat.Random(box.x1 + 3, box.x2 - 3);
							if (!Z.GetCell(num12, box.y2 - 2).HasWall() && Z.GetCell(num12, box.y2).HasWall())
							{
								Z.GetCell(num12, box.y2).ClearWalls();
								Z.GetCell(num12, box.y2 - 1).ClearWalls();
								Z.GetCell(num12, box.y2).AddObject("BrinestalkArrowslit", null, null, null, null);
								Z.GetCell(num12, box.y2 - 1).AddObject("BrinestalkArrowslit", null, null, null, null);
							}
						}
						if ((num11 & 4) != 0)
						{
							int num13 = Stat.Random(box.y1 + 3, box.y2 - 3);
							if (!Z.GetCell(box.x1 + 2, num13).HasWall() && Z.GetCell(box.x1, num13).HasWall())
							{
								Z.GetCell(box.x1, num13).ClearWalls();
								Z.GetCell(box.x1 + 1, num13).ClearWalls();
								Z.GetCell(box.x1, num13).AddObject("BrinestalkArrowslit", null, null, null, null);
								Z.GetCell(box.x1 + 1, num13).AddObject("BrinestalkArrowslit", null, null, null, null);
							}
						}
						if ((num11 & 8) != 0)
						{
							int num14 = Stat.Random(box.y1 + 3, box.y2 - 3);
							if (!Z.GetCell(box.x2 - 2, num14).HasWall() && Z.GetCell(box.x2, num14).HasWall())
							{
								Z.GetCell(box.x2, num14).ClearWalls();
								Z.GetCell(box.x2 - 1, num14).ClearWalls();
								Z.GetCell(box.x2, num14).AddObject("BrinestalkArrowslit", null, null, null, null);
								Z.GetCell(box.x2 - 1, num14).AddObject("BrinestalkArrowslit", null, null, null, null);
							}
						}
					}
					int num15 = 0;
					bool flag3;
					do
					{
						num15++;
						flag3 = false;
						int num16 = Stat.Random(1, 15);
						int num17 = -1;
						int num18 = -1;
						int num19 = -1;
						int num20 = -1;
						for (int num21 = box.x1 + 3; num21 <= box.x2 - 3; num21++)
						{
							if (Z.GetCell(num21, box.y1).HasObjectWithBlueprint("CanyonMarker"))
							{
								num17 = num21;
							}
							if (Z.GetCell(num21, box.y2).HasObjectWithBlueprint("CanyonMarker"))
							{
								num18 = num21;
							}
						}
						for (int num22 = box.y1 + 3; num22 <= box.y2 - 3; num22++)
						{
							if (Z.GetCell(box.x1, num22).HasObjectWithBlueprint("CanyonMarker"))
							{
								num20 = num22;
							}
							if (Z.GetCell(box.x2, num22).HasObjectWithBlueprint("CanyonMarker"))
							{
								num19 = num22;
							}
						}
						if ((num16 & 1) != 0 || num17 != -1)
						{
							int num12 = Stat.Random(box.x1 + 3, box.x2 - 3);
							if (num17 != -1 && num15 < 10)
							{
								num12 = num17;
							}
							if (!Z.GetCell(num12, box.y1 + 2).HasWall())
							{
								Z.GetCell(num12, box.y1).ClearWalls();
								Z.GetCell(num12, box.y1 + 1).ClearWalls();
								flag3 = true;
							}
							if (!Z.GetCell(num12 + 1, box.y1 + 2).HasWall())
							{
								Z.GetCell(num12 + 1, box.y1).ClearWalls();
								Z.GetCell(num12 + 1, box.y1 + 1).ClearWalls();
								flag3 = true;
							}
						}
						if ((num16 & 2) != 0 || num18 != -1)
						{
							int num12 = Stat.Random(box.x1 + 3, box.x2 - 3);
							if (num18 != -1 && num15 < 10)
							{
								num12 = num18;
							}
							if (!Z.GetCell(num12, box.y2 - 2).HasWall())
							{
								Z.GetCell(num12, box.y2).ClearWalls();
								Z.GetCell(num12, box.y2 - 1).ClearWalls();
								flag3 = true;
							}
							if (!Z.GetCell(num12 + 1, box.y2 - 2).HasWall())
							{
								Z.GetCell(num12 + 1, box.y2).ClearWalls();
								Z.GetCell(num12 + 1, box.y2 - 1).ClearWalls();
								flag3 = true;
							}
						}
						if ((num16 & 4) != 0 || num19 != -1)
						{
							int num23 = Stat.Random(box.y1 + 3, box.y2 - 3);
							if (num19 != -1 && num15 < 10)
							{
								num23 = num19;
							}
							if (!Z.GetCell(box.x1 + 2, num23).HasWall())
							{
								Z.GetCell(box.x1, num23).ClearWalls();
								Z.GetCell(box.x1 + 1, num23).ClearWalls();
								flag3 = true;
							}
							if (!Z.GetCell(box.x1 + 2, num23 + 1).HasWall())
							{
								Z.GetCell(box.x1, num23 + 1).ClearWalls();
								Z.GetCell(box.x1 + 1, num23 + 1).ClearWalls();
								flag3 = true;
							}
						}
						if ((num16 & 8) != 0 || num20 != -1)
						{
							int num24 = Stat.Random(box.y1 + 3, box.y2 - 3);
							if (num20 != -1 && num15 < 10)
							{
								num24 = num20;
							}
							if (!Z.GetCell(box.x2 - 2, num24).HasWall())
							{
								Z.GetCell(box.x2, num24).ClearWalls();
								Z.GetCell(box.x2 - 1, num24).ClearWalls();
								flag3 = true;
							}
							if (!Z.GetCell(box.x2 - 2, num24 + 1).HasWall())
							{
								Z.GetCell(box.x2, num24 + 1).ClearWalls();
								Z.GetCell(box.x2 - 1, num24 + 1).ClearWalls();
								flag3 = true;
							}
						}
					}
					while (!flag3);
					if (InsideStocakdeTable != null)
					{
						ZoneBuilderSandbox.PlacePopulationInRegion(Z, list2, InsideStocakdeTable, null, null);
					}
					ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, false, 0.2f, null);
					return true;
				}
				i--;
			}
			if (i < 0)
			{
				return true;
			}
			goto IL_0117;
		}
	}
}
