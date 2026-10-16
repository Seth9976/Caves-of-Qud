using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB6 RID: 2742
	public class StockadeMaker : ZoneBuilderSandbox
	{
		// Token: 0x06006F6B RID: 28523 RVA: 0x0026E2B8 File Offset: 0x0026C4B8
		public bool BuildZone(Zone Z, bool ClearCombatObjectsFirst, string WallObject, string ZoneTable, string Widgets, string CustomRoomPopulation = null, string BoxWidth = "30-50", string BoxHeight = "16-24", bool SpecialRedrockBuilder = false)
		{
			Box box = null;
			Box box2 = null;
			int i;
			for (i = 10; i > 0; i--)
			{
				List<Box> list = Tools.GenerateBoxes(BoxGenerateOverlap.Irrelevant, new Range(1, 1), new Range(Convert.ToInt32(BoxWidth.Split('-', StringSplitOptions.None)[0]), Convert.ToInt32(BoxWidth.Split('-', StringSplitOptions.None)[1])), new Range(Convert.ToInt32(BoxHeight.Split('-', StringSplitOptions.None)[0]), Convert.ToInt32(BoxHeight.Split('-', StringSplitOptions.None)[1])), null, null, null);
				if (list == null || list.Count == 0)
				{
					return false;
				}
				box = list[0].Grow(-1);
				box2 = box;
				for (int j = list[0].x1; j <= list[0].x2; j++)
				{
					int num = list[0].y1;
					while (num <= list[0].y2 && !Z.IsReachable(j, num))
					{
						num++;
					}
				}
			}
			if (i < 0)
			{
				return true;
			}
			Z.ClearBox(box, "CanyonMarker", null, null);
			if (ClearCombatObjectsFirst)
			{
				for (int k = box.y1; k <= box.y2; k++)
				{
					for (int l = box.x1; l <= box.x2; l++)
					{
						for (;;)
						{
							using (List<GameObject>.Enumerator enumerator = Z.GetCell(l, k).GetObjectsWithPart("Combat").GetEnumerator())
							{
								if (enumerator.MoveNext())
								{
									GameObject gameObject = enumerator.Current;
									Z.GetCell(l, k).RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
									continue;
								}
							}
							break;
						}
					}
				}
			}
			Z.ClearBox(box, "CanyonMarker", null, null);
			for (int m = box.x1; m <= box.x2; m++)
			{
				for (int n = box.y1; n <= box.y2; n++)
				{
					Dirty.PaintCell(Z.GetCell(m, n));
				}
			}
			Z.FillRoundHollowBox(box, WallObject);
			Z.FillRoundHollowBox(box.Grow(-1), WallObject);
			List<Box> list2 = new List<Box>();
			if (box2.Valid)
			{
				foreach (Box box3 in Tools.GenerateBoxes(new List<Box>(), BoxGenerateOverlap.NeverOverlap, new Range(1, 8), new Range(9, 40), new Range(8, 14), new Range(6, 999), new Range(box2.x1, box2.x2), new Range(box2.y1, box2.y2)))
				{
					if (box3.Valid && box3.Grow(-1).Valid)
					{
						if (box3.x1 == box.x1 || box3.x2 == box.x2 || box3.y1 == box.y1 || box3.y2 == box.y2)
						{
							Z.FillRoundHollowBox(box3, WallObject);
							list2.Add(box3);
						}
						else
						{
							Z.FillRoundHollowBox(box3.Grow(-1), WallObject);
							list2.Add(box3.Grow(-1));
						}
					}
				}
			}
			bool flag = true;
			int num2 = 0;
			foreach (Box box4 in list2)
			{
				if (CustomRoomPopulation != null)
				{
					if (box4.Volume > 25 && box4.Height > 6 && box4.Width > 4)
					{
						if (flag && SpecialRedrockBuilder)
						{
							ZoneBuilderSandbox.PlacePopulationInRect(Z, box4.rect, "LegendarySnapjawParty");
							flag = false;
						}
						else
						{
							ZoneBuilderSandbox.PlacePopulationInRect(Z, box4.rect, CustomRoomPopulation);
						}
					}
				}
				else if (box4.Volume > 25 && box4.Height > 6 && box4.Width > 4)
				{
					BuildingTemplate buildingTemplate = new BuildingTemplate(box4.Width - 2, box4.Height - 2, 1, true);
					for (int num3 = 1; num3 < buildingTemplate.Width - 1; num3++)
					{
						for (int num4 = 1; num4 < buildingTemplate.Height - 1; num4++)
						{
							if (buildingTemplate.Map[num3, num4] == BuildingTemplateTile.Wall)
							{
								Z.GetCell(num3 + box4.x1, num4 + box4.y1).AddObject(WallObject, null, null, null, null);
							}
							if (buildingTemplate.Map[num3, num4] == BuildingTemplateTile.Door)
							{
								Z.GetCell(num3 + box4.x1, num4 + box4.y1).ClearWalls();
							}
						}
					}
					foreach (RoomData roomData in buildingTemplate.RoomList)
					{
						int num5 = box4.x1 + roomData.Left + 1;
						int num6 = box4.y1 + roomData.Top + 1;
						if (num2 == 0)
						{
							List<Cell> localEmptyAdjacentCells = Z.GetCell(num5, num6).GetLocalEmptyAdjacentCells();
							if (localEmptyAdjacentCells.Count > 0)
							{
								int num7 = Stat.Random(1, 100);
								if (num7 <= 70)
								{
									localEmptyAdjacentCells[0].AddObject("Fort Snapjaw Warlord", null, null, null, null);
								}
								else if (num7 <= 90)
								{
									localEmptyAdjacentCells[0].AddObject("Snapjaw Hero 0", null, null, null, null);
								}
								else
								{
									localEmptyAdjacentCells[0].AddObject("Snapjaw Hero 1", null, null, null, null);
								}
							}
							if (localEmptyAdjacentCells.Count > 1)
							{
								Cell cell = localEmptyAdjacentCells[1];
								string text = "Chest";
								int num8 = Stat.Random(1, 2);
								cell.AddObject(text + num8.ToString(), null, null, null, null);
							}
							if (localEmptyAdjacentCells.Count > 2 && Stat.Random(1, 100) <= 50)
							{
								localEmptyAdjacentCells[2].AddObject("Table", null, null, null, null);
							}
						}
						else if (num2 == 1)
						{
							List<Cell> localEmptyAdjacentCells2 = Z.GetCell(num5, num6).GetLocalEmptyAdjacentCells();
							if (localEmptyAdjacentCells2.Count > 0)
							{
								localEmptyAdjacentCells2[0].AddObject("Table", null, null, null, null);
							}
							if (localEmptyAdjacentCells2.Count > 1)
							{
								localEmptyAdjacentCells2[1].AddObject("Chest2", null, null, null, null);
							}
						}
						else if (num2 == 2)
						{
							List<Cell> localEmptyAdjacentCells3 = Z.GetCell(num5, num6).GetLocalEmptyAdjacentCells();
							if (localEmptyAdjacentCells3.Count > 0)
							{
								localEmptyAdjacentCells3[0].AddObject("Forge", null, null, null, null);
							}
							if (localEmptyAdjacentCells3.Count > 1)
							{
								localEmptyAdjacentCells3[1].AddObject("Anvil", null, null, null, null);
							}
							if (localEmptyAdjacentCells3.Count > 2 && Stat.Random(1, 100) <= 50)
							{
								localEmptyAdjacentCells3[2].AddObject("ChestBronzeIngots", null, null, null, null);
							}
						}
						else
						{
							List<Cell> emptyAdjacentCells = Z.GetCell(num5, num6).GetEmptyAdjacentCells();
							if (emptyAdjacentCells.Count > 0 && Stat.Random(1, 100) <= 25)
							{
								emptyAdjacentCells[0].AddObject("Table", null, null, null, null);
							}
						}
						num2++;
					}
				}
				i = 0;
				while (i < 1000)
				{
					i++;
					int num9;
					int num10;
					if (Stat.Random(0, 1) == 0)
					{
						num9 = Stat.Random(box4.x1 + 2, box4.x2 - 2);
						if (Stat.Random(0, 1) == 0)
						{
							num10 = box4.y1;
						}
						else
						{
							num10 = box4.y2;
						}
					}
					else
					{
						num10 = Stat.Random(box4.y1 + 2, box4.y2 - 2);
						if (Stat.Random(0, 1) == 0)
						{
							num9 = box4.x1;
						}
						else
						{
							num9 = box4.x2;
						}
					}
					if (num9 != box.x1 && num9 != box.x2 && num10 != box.y1 && num10 != box.y2 && ((!Z.GetCell(num9 - 1, num10).HasWall() && !Z.GetCell(num9 + 1, num10).HasWall()) || (!Z.GetCell(num9, num10 - 1).HasWall() && !Z.GetCell(num9, num10 + 1).HasWall())))
					{
						Z.GetCell(num9, num10).ClearWalls();
						break;
					}
				}
			}
			if (num2 == 0)
			{
				for (i = 1000; i > 0; i--)
				{
					Point randomPoint = box.Grow(-1).RandomPoint;
					if (Z.GetCell(randomPoint.X, randomPoint.Y).IsEmpty())
					{
						Z.GetCell(randomPoint.X, randomPoint.Y).AddObject("Snapjaw Hero 0", null, null, null, null);
						break;
					}
				}
			}
			if (ZoneTable != null)
			{
				string[] array = ZoneTable.Split(',', StringSplitOptions.None);
				for (int num8 = 0; num8 < array.Length; num8++)
				{
					foreach (PopulationResult populationResult in PopulationManager.Generate(array[num8], "zonetier", Z.NewTier.ToString(), null, null, null))
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
			for (int num11 = 0; num11 < 2; num11++)
			{
				int num12 = Stat.Random(1, 15);
				if ((num12 & 1) != 0)
				{
					int num13 = Stat.Random(box.x1 + 3, box.x2 - 3);
					if (!Z.GetCell(num13, box.y1 + 2).HasWall() && Z.GetCell(num13, box.y1).HasWall())
					{
						Z.GetCell(num13, box.y1).ClearWalls();
						Z.GetCell(num13, box.y1).AddObject("BrinestalkArrowslit", null, null, null, null);
						Z.GetCell(num13, box.y1 + 1).ClearWalls();
						Z.GetCell(num13, box.y1 + 1).AddObject("BrinestalkArrowslit", null, null, null, null);
					}
				}
				if ((num12 & 2) != 0)
				{
					int num13 = Stat.Random(box.x1 + 3, box.x2 - 3);
					if (!Z.GetCell(num13, box.y2 - 2).HasWall() && Z.GetCell(num13, box.y2).HasWall())
					{
						Z.GetCell(num13, box.y2).ClearWalls();
						Z.GetCell(num13, box.y2 - 1).ClearWalls();
						Z.GetCell(num13, box.y2).AddObject("BrinestalkArrowslit", null, null, null, null);
						Z.GetCell(num13, box.y2 - 1).AddObject("BrinestalkArrowslit", null, null, null, null);
					}
				}
				if ((num12 & 4) != 0)
				{
					int num14 = Stat.Random(box.y1 + 3, box.y2 - 3);
					if (!Z.GetCell(box.x1 + 2, num14).HasWall() && Z.GetCell(box.x1, num14).HasWall())
					{
						Z.GetCell(box.x1, num14).ClearWalls();
						Z.GetCell(box.x1 + 1, num14).ClearWalls();
						Z.GetCell(box.x1, num14).AddObject("BrinestalkArrowslit", null, null, null, null);
						Z.GetCell(box.x1 + 1, num14).AddObject("BrinestalkArrowslit", null, null, null, null);
					}
				}
				if ((num12 & 8) != 0)
				{
					int num15 = Stat.Random(box.y1 + 3, box.y2 - 3);
					if (!Z.GetCell(box.x2 - 2, num15).HasWall() && Z.GetCell(box.x2, num15).HasWall())
					{
						Z.GetCell(box.x2, num15).ClearWalls();
						Z.GetCell(box.x2 - 1, num15).ClearWalls();
						Z.GetCell(box.x2, num15).AddObject("BrinestalkArrowslit", null, null, null, null);
						Z.GetCell(box.x2 - 1, num15).AddObject("BrinestalkArrowslit", null, null, null, null);
					}
				}
			}
			int num16 = 0;
			bool flag2;
			do
			{
				num16++;
				flag2 = false;
				int num17 = Stat.Random(1, 15);
				int num18 = -1;
				int num19 = -1;
				int num20 = -1;
				int num21 = -1;
				for (int num22 = box.x1 + 3; num22 <= box.x2 - 3; num22++)
				{
					if (Z.GetCell(num22, box.y1).HasObjectWithBlueprint("CanyonMarker"))
					{
						num18 = num22;
					}
					if (Z.GetCell(num22, box.y2).HasObjectWithBlueprint("CanyonMarker"))
					{
						num19 = num22;
					}
				}
				for (int num23 = box.y1 + 3; num23 <= box.y2 - 3; num23++)
				{
					if (Z.GetCell(box.x1, num23).HasObjectWithBlueprint("CanyonMarker"))
					{
						num21 = num23;
					}
					if (Z.GetCell(box.x2, num23).HasObjectWithBlueprint("CanyonMarker"))
					{
						num20 = num23;
					}
				}
				if ((num17 & 1) != 0 || num18 != -1)
				{
					int num13 = Stat.Random(box.x1 + 3, box.x2 - 3);
					if (num18 != -1 && num16 < 10)
					{
						num13 = num18;
					}
					if (!Z.GetCell(num13, box.y1 + 2).HasWall())
					{
						Z.GetCell(num13, box.y1).ClearWalls();
						Z.GetCell(num13, box.y1 + 1).ClearWalls();
						flag2 = true;
					}
					if (!Z.GetCell(num13 + 1, box.y1 + 2).HasWall())
					{
						Z.GetCell(num13 + 1, box.y1).ClearWalls();
						Z.GetCell(num13 + 1, box.y1 + 1).ClearWalls();
						flag2 = true;
					}
				}
				if ((num17 & 2) != 0 || num19 != -1)
				{
					int num13 = Stat.Random(box.x1 + 3, box.x2 - 3);
					if (num19 != -1 && num16 < 10)
					{
						num13 = num19;
					}
					if (!Z.GetCell(num13, box.y2 - 2).HasWall())
					{
						Z.GetCell(num13, box.y2).ClearWalls();
						Z.GetCell(num13, box.y2 - 1).ClearWalls();
						flag2 = true;
					}
					if (!Z.GetCell(num13 + 1, box.y2 - 2).HasWall())
					{
						Z.GetCell(num13 + 1, box.y2).ClearWalls();
						Z.GetCell(num13 + 1, box.y2 - 1).ClearWalls();
						flag2 = true;
					}
				}
				if ((num17 & 4) != 0 || num20 != -1)
				{
					int num24 = Stat.Random(box.y1 + 3, box.y2 - 3);
					if (num20 != -1 && num16 < 10)
					{
						num24 = num20;
					}
					if (!Z.GetCell(box.x1 + 2, num24).HasWall())
					{
						Z.GetCell(box.x1, num24).ClearWalls();
						Z.GetCell(box.x1 + 1, num24).ClearWalls();
						flag2 = true;
					}
					if (!Z.GetCell(box.x1 + 2, num24 + 1).HasWall())
					{
						Z.GetCell(box.x1, num24 + 1).ClearWalls();
						Z.GetCell(box.x1 + 1, num24 + 1).ClearWalls();
						flag2 = true;
					}
				}
				if ((num17 & 8) != 0 || num21 != -1)
				{
					int num25 = Stat.Random(box.y1 + 3, box.y2 - 3);
					if (num21 != -1 && num16 < 10)
					{
						num25 = num21;
					}
					if (!Z.GetCell(box.x2 - 2, num25).HasWall())
					{
						Z.GetCell(box.x2, num25).ClearWalls();
						Z.GetCell(box.x2 - 1, num25).ClearWalls();
						flag2 = true;
					}
					if (!Z.GetCell(box.x2 - 2, num25 + 1).HasWall())
					{
						Z.GetCell(box.x2, num25 + 1).ClearWalls();
						Z.GetCell(box.x2 - 1, num25 + 1).ClearWalls();
						flag2 = true;
					}
				}
			}
			while (!flag2);
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, false, 0.2f, null);
			return true;
		}
	}
}
