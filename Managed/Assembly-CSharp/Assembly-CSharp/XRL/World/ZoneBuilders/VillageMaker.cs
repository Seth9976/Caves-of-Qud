using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AB9 RID: 2745
	public class VillageMaker : ZoneBuilderSandbox
	{
		// Token: 0x06006F71 RID: 28529 RVA: 0x002704B8 File Offset: 0x0026E6B8
		public bool BuildZone(Zone Z, bool bRoads, string WallObject, bool RoundBuildings, string Huts, string Features, string HutTable, string ZoneTable, string Widgets, bool ClearCombatObjectsFirst)
		{
			if (ClearCombatObjectsFirst)
			{
				for (int i = 0; i < Z.Height; i++)
				{
					for (int j = 0; j < Z.Width; j++)
					{
						for (;;)
						{
							using (List<GameObject>.Enumerator enumerator = Z.GetCell(j, i).GetObjectsWithPart("Combat").GetEnumerator())
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
			int num = Huts.RollCached();
			List<Point> list = new List<Point>(num);
			List<Point> list2 = new List<Point>(num);
			bool flag = false;
			foreach (ZoneConnection zoneConnection in Z.EnumerateConnections())
			{
				Point point = new Point(zoneConnection.X, zoneConnection.Y);
				list.Add(point);
				if (zoneConnection.Type.Contains("Road"))
				{
					if (!flag)
					{
						list2.Clear();
						flag = true;
					}
					if (list2.Count == 0)
					{
						list2.Add(point);
					}
					else if (this.Distance(list2[0], 40, 12) > zoneConnection.Loc2D.ManhattanDistance(40, 12))
					{
						list2[0] = point;
					}
				}
				if (!flag)
				{
					list2.Add(point);
				}
			}
			int num2 = 0;
			int num3 = 0;
			while (num3 < num && num2 < 100)
			{
				Point point2 = null;
				bool flag2 = false;
				while (!flag2 && num2 < 100)
				{
					num2++;
					flag2 = true;
					point2 = new Point(Stat.Random(5, 70), Stat.Random(5, 20));
					for (int k = 0; k < list.Count; k++)
					{
						if (XRL.Rules.Geometry.Distance(point2, list[k]) < 10)
						{
							flag2 = false;
							break;
						}
					}
				}
				if (flag2)
				{
					int num4 = Stat.Random(4, 6);
					int num5 = Stat.Random(4, 6);
					num2 = 0;
					Rect2D rect2D = new Rect2D(point2.X - num4 / 2, point2.Y - num5 / 2, point2.X + num4 / 2, point2.Y + num5 / 2);
					do
					{
						rect2D.GetRandomDoorCell("?", RoundBuildings ? 2 : 1);
					}
					while (!Z.GetCell(rect2D.Door.x, rect2D.Door.y).IsEmpty() && num2++ < 8);
					list2.Add(new Point(rect2D.Door.x, rect2D.Door.y));
					base.PlaceHut(Z, rect2D, "DirtPath", WallObject, HutTable, RoundBuildings);
				}
				num3++;
			}
			Point point3 = null;
			bool flag3 = false;
			if (Features != null && Features.Contains("Cistern"))
			{
				int num6 = num3;
				string[] array = Features.Split('-', StringSplitOptions.None);
				if (num6 >= int.Parse((array != null) ? array[0] : null))
				{
					bool flag4 = false;
					while (!flag4 && num2 < 100)
					{
						num2++;
						flag4 = true;
						point3 = new Point(Stat.Random(5, 70), Stat.Random(5, 20));
						for (int l = 0; l < list.Count; l++)
						{
							if (XRL.Rules.Geometry.Distance(point3, list[l]) < 8)
							{
								flag4 = false;
								break;
							}
						}
					}
					if (flag4)
					{
						flag3 = true;
					}
				}
			}
			RoadBuilder roadBuilder = new RoadBuilder();
			if (flag3)
			{
				list2.Add(point3);
			}
			list2.Sort((Point a, Point b) => this.Distance(a, 40, 12).CompareTo(this.Distance(b, 40, 12)));
			roadBuilder.BeforePlacement = delegate(Cell C)
			{
				C.ClearObjectsWithTag("Plant");
			};
			roadBuilder.ClearAdjacent = false;
			roadBuilder.BuildRoads(Z, list2, true, false);
			if (flag3)
			{
				string text = "LowSandstoneWall";
				if (Features.Contains("Qlippoth"))
				{
					text = "HolographicLowSandstoneWall";
				}
				for (int m = point3.X - 1; m <= point3.X + 2; m++)
				{
					Z.GetCell(m, point3.Y - 1).ClearAndAddObject(text);
					Z.GetCell(m, point3.Y + 1).ClearAndAddObject(text);
				}
				int x = point3.X;
				int y = point3.Y;
				Z.GetCell(x - 1, y).ClearAndAddObject(text);
				Z.GetCell(x + 2, y).ClearAndAddObject(text);
				if (Features.Contains("BloodCistern"))
				{
					Z.GetCell(x, y).ClearAndAddObject("BloodPool");
					Z.GetCell(x + 1, y).ClearAndAddObject("BloodPool");
				}
				else
				{
					Z.GetCell(x, y).ClearAndAddObject("FreshWaterPool");
					Z.GetCell(x + 1, y).ClearAndAddObject("FreshWaterPool");
				}
			}
			if (ZoneTable != null)
			{
				string[] array2 = ZoneTable.Split(',', StringSplitOptions.None);
				for (int n = 0; n < array2.Length; n++)
				{
					foreach (PopulationResult populationResult in PopulationManager.Generate(array2[n], "zonetier", Z.NewTier.ToString(), null, null, null))
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
			for (int num7 = 0; num7 < Z.Height; num7++)
			{
				for (int num8 = 0; num8 < Z.Width; num8++)
				{
					if (Z.Map[num8][num7].HasObject("DirtRoad"))
					{
						foreach (Cell cell in Z.Map[num8][num7].GetLocalAdjacentCells())
						{
							cell.ClearObjectsWithTag("Plant");
						}
					}
				}
			}
			Z.GetCell(0, 0).RequireObject("DaylightWidget", null, null, null, null);
			return true;
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x00270B34 File Offset: 0x0026ED34
		public int Distance(Point A, int X, int Y)
		{
			return Math.Max(Math.Abs(A.X - X), Math.Abs(A.Y - Y));
		}
	}
}
