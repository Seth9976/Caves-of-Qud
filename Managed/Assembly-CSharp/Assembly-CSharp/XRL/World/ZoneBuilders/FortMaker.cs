using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AAE RID: 2734
	public class FortMaker : ZoneBuilderSandbox
	{
		// Token: 0x06006F57 RID: 28503 RVA: 0x0026CDC0 File Offset: 0x0026AFC0
		public bool BuildZone(Zone Z, bool ClearCombatObjectsFirst, string WallObject, string ZoneTable, string Widgets)
		{
			List<Box> list = Tools.GenerateBoxes(BoxGenerateOverlap.Irrelevant, new Range(1, 1), new Range(30, 50), new Range(16, 24), null, new Range(1, 78), new Range(1, 23));
			if (list == null || list.Count == 0)
			{
				return false;
			}
			Box box = list[0].Grow(-1);
			Box box2 = box;
			Z.ClearBox(box, null);
			if (ClearCombatObjectsFirst)
			{
				for (int i = box.y1; i <= box.y2; i++)
				{
					for (int j = box.x1; j <= box.x2; j++)
					{
						for (;;)
						{
							using (List<GameObject>.Enumerator enumerator = Z.GetCell(j, i).GetObjectsWithPartReadonly("Combat").GetEnumerator())
							{
								if (enumerator.MoveNext())
								{
									GameObject gameObject = enumerator.Current;
									Z.GetCell(j, i).RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
									continue;
								}
							}
							break;
						}
					}
				}
			}
			Z.ClearBox(box, null);
			Z.FillBox(box, "DirtFloor", false);
			Z.FillHollowBox(box, WallObject);
			for (int k = box.x1; k <= box.x2; k++)
			{
				for (int l = box.y1; l <= box.y2; l++)
				{
					Cell cell = Z.GetCell(k, l);
					if (cell.HasObjectWithBlueprint("CanyonMarker"))
					{
						cell.ClearWalls();
					}
				}
			}
			List<Box> list2 = new List<Box>();
			if (box2.Valid)
			{
				foreach (Box box3 in Tools.GenerateBoxes(new List<Box>(), BoxGenerateOverlap.NeverOverlap, new Range(1, 8), new Range(9, 40), new Range(8, 14), new Range(6, 999), new Range(box2.x1, box2.x2), new Range(box2.y1, box2.y2)))
				{
					if (box3.Valid && box3.Grow(-1).Valid)
					{
						if (box3.x1 == box.x1 || box3.x2 == box.x2 || box3.y1 == box.y1 || box3.y2 == box.y2)
						{
							Z.FillHollowBox(box3, WallObject);
							list2.Add(box3);
						}
						else
						{
							Z.FillHollowBox(box3.Grow(-1), WallObject);
							list2.Add(box3.Grow(-1));
						}
					}
				}
			}
			foreach (Box box4 in list2)
			{
				if (box4.Volume > 25 && box4.Height > 4 && box4.Width > 4)
				{
					BuildingTemplate buildingTemplate = new BuildingTemplate(box4.Width, box4.Height, 1, true);
					for (int m = 1; m < buildingTemplate.Width - 1; m++)
					{
						for (int n = 1; n < buildingTemplate.Height - 1; n++)
						{
							if (buildingTemplate.Map[m, n] == BuildingTemplateTile.Wall)
							{
								Z.GetCell(m + box4.x1, n + box4.y1).AddObject(WallObject, null, null, null, null);
							}
							if (buildingTemplate.Map[m, n] == BuildingTemplateTile.Door)
							{
								int num = m + box4.x1;
								int num2 = n + box4.y1;
								if (num != box.x1 && num != box.x2 && num2 != box.y1 && num2 != box.y2 && num != box.x1 + 1 && num != box.x2 - 1 && num2 != box.y1 + 1 && num2 != box.y2 - 1)
								{
									Z.GetCell(m + box4.x1, n + box4.y1).AddObject("Door", null, null, null, null);
								}
							}
						}
					}
				}
				int num3 = 0;
				while (num3 < 1000)
				{
					num3++;
					int num4;
					int num5;
					if (Stat.Random(0, 1) == 0)
					{
						num4 = Stat.Random(box4.x1 + 1, box4.x2 - 1);
						if (Stat.Random(0, 1) == 0)
						{
							num5 = box4.y1;
						}
						else
						{
							num5 = box4.y2;
						}
					}
					else
					{
						num5 = Stat.Random(box4.y1 + 1, box4.y2 - 1);
						if (Stat.Random(0, 1) == 0)
						{
							num4 = box4.x1;
						}
						else
						{
							num4 = box4.x2;
						}
					}
					if (num4 != box.x1 && num4 != box.x2 && num5 != box.y1 && num5 != box.y2 && ((!Z.GetCell(num4 - 1, num5).HasWall() && !Z.GetCell(num4 + 1, num5).HasWall()) || (!Z.GetCell(num4, num5 - 1).HasWall() && !Z.GetCell(num4, num5 + 1).HasWall())))
					{
						Z.GetCell(num4, num5).Clear(null, false, false, null);
						Z.GetCell(num4, num5).AddObject("Door", null, null, null, null);
						break;
					}
				}
			}
			if (ZoneTable != null)
			{
				string[] array = ZoneTable.Split(',', StringSplitOptions.None);
				for (int num6 = 0; num6 < array.Length; num6++)
				{
					foreach (PopulationResult populationResult in PopulationManager.Generate(array[num6], "zonetier", Z.NewTier.ToString(), null, null, null))
					{
						ZoneBuilderSandbox.PlaceObjectInArea(Z, Z.area, GameObjectFactory.Factory.CreateObject(populationResult.Blueprint), 0, 0, populationResult.Hint, null, false);
					}
				}
			}
			if (Widgets != null)
			{
				foreach (string text in Widgets.Split(',', StringSplitOptions.None))
				{
					Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject(text), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			int num7 = Stat.Random(1, 15);
			if ((num7 & 1) != 0)
			{
				int num8 = Stat.Random(box.x1 + 1, box.x2 - 2);
				if (!Z.GetCell(num8, box.y1 + 1).HasWall())
				{
					Z.GetCell(num8, box.y1).Clear(null, false, false, null);
					Z.GetCell(num8, box.y1).AddObject("Door", null, null, null, null);
				}
				if (!Z.GetCell(num8 + 1, box.y1 + 1).HasWall())
				{
					Z.GetCell(num8 + 1, box.y1).Clear(null, false, false, null);
					Z.GetCell(num8 + 1, box.y1).AddObject("Door", null, null, null, null);
				}
			}
			if ((num7 & 2) != 0)
			{
				int num9 = Stat.Random(box.x1 + 1, box.x2 - 2);
				if (!Z.GetCell(num9, box.y2 - 1).HasWall())
				{
					Z.GetCell(num9, box.y2).Clear(null, false, false, null);
					Z.GetCell(num9, box.y2).AddObject("Door", null, null, null, null);
				}
				if (!Z.GetCell(num9 + 1, box.y2 - 1).HasWall())
				{
					Z.GetCell(num9 + 1, box.y2).Clear(null, false, false, null);
					Z.GetCell(num9 + 1, box.y2).AddObject("Door", null, null, null, null);
				}
			}
			if ((num7 & 4) != 0)
			{
				int num10 = Stat.Random(box.y1 + 1, box.y2 - 2);
				if (!Z.GetCell(box.x1 + 1, num10).HasWall())
				{
					Z.GetCell(box.x1, num10).Clear(null, false, false, null);
					Z.GetCell(box.x1, num10).AddObject("Door", null, null, null, null);
				}
				if (!Z.GetCell(box.x1 + 1, num10 + 1).HasWall())
				{
					Z.GetCell(box.x1, num10 + 1).Clear(null, false, false, null);
					Z.GetCell(box.x1, num10 + 1).AddObject("Door", null, null, null, null);
				}
			}
			if ((num7 & 8) != 0)
			{
				int num11 = Stat.Random(box.y1 + 1, box.y2 - 2);
				if (!Z.GetCell(box.x2 - 1, num11).HasWall())
				{
					Z.GetCell(box.x2, num11).Clear(null, false, false, null);
					Z.GetCell(box.x2, num11).AddObject("Door", null, null, null, null);
				}
				if (!Z.GetCell(box.x2 - 1, num11 + 1).HasWall())
				{
					Z.GetCell(box.x2, num11 + 1).Clear(null, false, false, null);
					Z.GetCell(box.x2, num11 + 1).AddObject("Door", null, null, null, null);
				}
			}
			return true;
		}
	}
}
