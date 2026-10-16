using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A83 RID: 2691
	public class ChavvahRoots : ZoneBuilderSandbox
	{
		// Token: 0x06006E8C RID: 28300 RVA: 0x0025D75C File Offset: 0x0025B95C
		public bool BuildZone(Zone Z)
		{
			if (Z.Z > 50)
			{
				return ZoneTemplateManager.Templates["MoonStairCaves"].Execute(Z, null);
			}
			new MapBuilder
			{
				ClearBeforePlace = true,
				ID = ((Z.Z == 50) ? "ChavvahRootBaseBottom.rpm" : "ChavvahRootBase.rpm")
			}.BuildZone(Z);
			GameObject gameObject = Z.FindObject("Dirty");
			GameObject gameObject2 = Z.FindObject("Grassy");
			if (gameObject != null || gameObject2 != null)
			{
				if (gameObject != null)
				{
					gameObject.Obliterate(null, false, null);
				}
				if (gameObject2 != null)
				{
					gameObject2.Obliterate(null, false, null);
				}
				Z.GetCell(0, 0).AddObject("CrystalDirty", null, null, null, null);
			}
			Point2D point = Location2D.Get(40, 12).Point;
			List<Cell> list = new List<Cell>();
			List<Cell> list2 = new List<Cell>();
			List<Cell> list3 = new List<Cell>();
			List<Cell> list4 = new List<Cell>();
			foreach (Cell cell in Z.GetCells())
			{
				if (cell.CosmeticDistanceTo(point) < 14)
				{
					cell.RemoveObjects((GameObject o) => o.Blueprint != "Chavvah Taproot Floor" && o.Blueprint != "TauSoft").ForEach(delegate(GameObject o)
					{
						o.Obliterate(null, false, null);
					});
					int num = cell.GetAdjacentObjectCount("Chavvah Taproot Floor", 1);
					if (cell.HasObject("Chavvah Taproot Floor"))
					{
						num++;
					}
					if (num == 0)
					{
						cell.Clear(null, false, false, null);
						if (Z.Z % 5 != 0)
						{
							cell.AddObject("Pit", null, null, null, null);
						}
					}
					else if (num >= 9)
					{
						if (cell.X <= 40)
						{
							list.Add(cell);
						}
						else
						{
							list2.Add(cell);
						}
						cell.AddObject("Chavvah Taproot", null, null, null, null);
						list4.Add(cell);
					}
					else
					{
						list3.Add(cell);
					}
				}
			}
			List<Cell> list5 = list4.Where<Cell>((Cell c) => c.GetCardinalAdjacentCellsWhere((Cell c) => c.HasObject("Chavvah Taproot"), false, true, false).Count < 4).ToList<Cell>();
			List<Point2D> list6 = new List<Point2D>();
			Queue<Cell> mandatoryStartCells = new Queue<Cell>();
			Action<Cell> <>9__5;
			Z.GetCellsWithObject("TauSoft").ForEach(delegate(Cell t)
			{
				if (t != null)
				{
					Action<Cell> action2;
					if ((action2 = <>9__5) == null)
					{
						action2 = (<>9__5 = delegate(Cell a)
						{
							if (a != null)
							{
								mandatoryStartCells.Enqueue(a);
							}
						});
					}
					t.ForeachCardinalAdjacentCell(action2);
				}
			});
			Stack<Cell> stack = new Stack<Cell>();
			int num2 = Stat.Random(12, 16) + mandatoryStartCells.Count;
			Func<int, int, int, int> <>9__9;
			for (int i = 0; i < num2; i++)
			{
				Cell cell2;
				if (mandatoryStartCells.Count > 0)
				{
					cell2 = mandatoryStartCells.Dequeue();
				}
				else
				{
					cell2 = list5.GetRandomElement<Cell>(null);
					if (stack.Count > 0)
					{
						cell2 = stack.Pop();
					}
				}
				if (cell2 != null)
				{
					Cell rootStartCell = cell2;
					int j = Stat.Random(3, 8);
					int num3 = 5;
					Comparison<Point2D> <>9__7;
					while (j > 0)
					{
						int num4 = Stat.Random(3, 8);
						int num5 = 16;
						IEnumerable<Point2D> enumerable = from p in cell2.Pos2D.GetRadialPoints(num4, list6)
							where p.x >= 0 && p.x < 80 && p.y >= 0 && p.y < 25
							select p;
						List<Point2D> list7;
						if (enumerable == null)
						{
							list7 = null;
						}
						else
						{
							List<Point2D> list8 = enumerable.ToList<Point2D>();
							if (list8 == null)
							{
								list7 = null;
							}
							else
							{
								List<Point2D> list9 = list8.ShuffleInPlace<Point2D>(null);
								if (list9 == null)
								{
									list7 = null;
								}
								else
								{
									IEnumerable<Point2D> enumerable2 = list9.Take<Point2D>(num5);
									list7 = ((enumerable2 != null) ? enumerable2.ToList<Point2D>() : null);
								}
							}
						}
						List<Point2D> list10 = list7;
						if (list10 == null)
						{
							break;
						}
						List<Point2D> list11 = list10;
						Comparison<Point2D> comparison;
						if ((comparison = <>9__7) == null)
						{
							comparison = (<>9__7 = (Point2D a, Point2D b) => b.ManhattanDistance(rootStartCell.Pos2D) - a.ManhattanDistance(rootStartCell.Pos2D));
						}
						list11.Sort(comparison);
						Point2D point2D = list10.FirstOrDefault<Point2D>();
						Zone z = Z;
						Location2D location = cell2.Location;
						Location2D location2 = point2D.location;
						bool flag = true;
						float num6 = 0.2f;
						int num7 = 0;
						Action<Cell> action = delegate(Cell c)
						{
							c.RemoveObjects((GameObject o) => o.Blueprint == "Pit");
							if (!c.HasObject("Crystalline Root"))
							{
								c.AddObject("Crystalline Root", null, null, null, null);
							}
						};
						Func<int, int, int, int> func;
						if ((func = <>9__9) == null)
						{
							func = (<>9__9 = delegate(int x, int y, int c)
							{
								if (Z.GetCell(x, y).HasObject("Chavvah Taproot"))
								{
									return int.MaxValue;
								}
								return 0;
							});
						}
						ZoneBuilderSandbox.TunnelTo(z, location, location2, flag, num6, num7, action, func);
						cell2 = Z.GetCell(point2D);
						if (Stat.Random(1, 100) <= num3)
						{
							stack.Push(cell2);
						}
						j--;
					}
				}
			}
			string rightStair = "Chavvah Spiral StairsUp";
			if (Z.Z % 5 == 0)
			{
				rightStair = "Chavvah Spiral StairsDown";
			}
			Cell cell3 = null;
			if (Z.Z % 5 <= 1)
			{
				ZoneConnection zoneConnection = (from con in Z.EnumerateConnections()
					where con.Object == rightStair
					select con).FirstOrDefault<ZoneConnection>();
				Cell cell4;
				if (zoneConnection != null)
				{
					cell4 = Z.GetCell(zoneConnection.Loc2D);
				}
				else
				{
					cell4 = list2.GetRandomElement<Cell>(null);
				}
				if (cell3 != null && cell4 != null)
				{
					ZoneBuilderSandbox.TunnelTo(Z, cell3.Location, cell4.Location, true, 0.2f, 200, null, delegate(int x, int y, int c)
					{
						if (Z.GetCell(x, y).HasObject("Chavvah Taproot"))
						{
							return Stat.Random(0, 50);
						}
						return 200;
					});
				}
				if (Z.Z != 50 && cell4 != null)
				{
					cell4.AddObject(rightStair, null, null, null, null);
				}
			}
			Z.FireEvent("FirmPitEdges");
			ZoneBuilderSandbox.EnsureAllVoidsConnected(Z, true, 0.2f, null);
			return true;
		}
	}
}
