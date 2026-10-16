using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using Unity.Profiling;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.UI;

namespace XRL.World.AI.Pathfinding
{
	// Token: 0x02000B74 RID: 2932
	public class FindPath
	{
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06007573 RID: 30067 RVA: 0x002C0307 File Offset: 0x002BE507
		public bool Usable
		{
			get
			{
				return this.Found && this.Steps != null && this.Steps.Count > 1;
			}
		}

		// Token: 0x06007574 RID: 30068 RVA: 0x002C032C File Offset: 0x002BE52C
		public static void Initalize()
		{
			for (int i = 0; i < 4000; i++)
			{
				FindPath.CellNavs[i] = new CellNavigationValue(0.0, 0.0, null, null, null);
			}
			for (int j = 0; j < 25; j++)
			{
				for (int k = 0; k < 80; k++)
				{
					FindPath.Grid[k, j] = new GridNode(k, j);
				}
			}
		}

		// Token: 0x06007575 RID: 30069 RVA: 0x002C0397 File Offset: 0x002BE597
		public CellNavigationValue NewCellNav(double Cost = 0.0, double Estimate = 0.0, Cell C = null, Cell Parent = null, string Direction = null)
		{
			if (FindPath.CellNavCount >= 3999)
			{
				return null;
			}
			FindPath.CellNavCount++;
			return FindPath.CellNavs[FindPath.CellNavCount].Set(Cost, Estimate, C, Parent, Direction);
		}

		// Token: 0x06007576 RID: 30070 RVA: 0x002C03CA File Offset: 0x002BE5CA
		public FindPath()
		{
		}

		// Token: 0x06007577 RID: 30071 RVA: 0x002C03F4 File Offset: 0x002BE5F4
		public FindPath(string StartZoneID, int X1, int Y1, string EndZoneID, int X2, int Y2, bool PathGlobal = false, bool PathUnlimited = false, GameObject Looker = null, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false, int MaxWeight = 100)
		{
			if (StartZoneID == null)
			{
				return;
			}
			if (EndZoneID == null)
			{
				return;
			}
			Zone zone = The.ZoneManager.GetZone(StartZoneID);
			Zone zone2 = The.ZoneManager.GetZone(EndZoneID);
			this.PerformPathfind(zone, X1, Y1, zone2, X2, Y2, PathGlobal, Looker, PathUnlimited, false, false, null, MaxWeight, false, false, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, true);
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x002C0480 File Offset: 0x002BE680
		public FindPath(Zone StartZone, int X1, int Y1, Zone EndZone, int X2, int Y2, bool PathGlobal, bool PathUnlimited, GameObject Looker, bool AddNoise)
		{
			if (StartZone == null)
			{
				return;
			}
			if (EndZone == null)
			{
				return;
			}
			this.PerformPathfind(StartZone, X1, Y1, EndZone, X2, Y2, PathGlobal, Looker, PathUnlimited, AddNoise, false, null, 100, false, false, false, false, false, false, true);
		}

		// Token: 0x06007579 RID: 30073 RVA: 0x002C04E0 File Offset: 0x002BE6E0
		public FindPath(Zone StartZone, int X1, int Y1, Zone EndZone, int X2, int Y2, bool PathGlobal = false, bool PathUnlimited = false, GameObject Looker = null, bool AddNoise = false, bool CardinalOnly = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false)
		{
			if (StartZone == null)
			{
				return;
			}
			if (EndZone == null)
			{
				return;
			}
			this.PerformPathfind(StartZone, X1, Y1, EndZone, X2, Y2, PathGlobal, Looker, PathUnlimited, AddNoise, CardinalOnly, null, 100, false, false, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, true);
		}

		// Token: 0x0600757A RID: 30074 RVA: 0x002C0548 File Offset: 0x002BE748
		public FindPath(Zone StartZone, int X1, int Y1, Zone EndZone, int X2, int Y2, bool PathGlobal = false, bool PathUnlimited = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false, GameObject Looker = null, int MaxWeight = 100)
		{
			if (StartZone == null)
			{
				return;
			}
			if (EndZone == null)
			{
				return;
			}
			this.PerformPathfind(StartZone, X1, Y1, EndZone, X2, Y2, PathGlobal, Looker, PathUnlimited, false, false, null, MaxWeight, false, false, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, true);
		}

		// Token: 0x0600757B RID: 30075 RVA: 0x002C05B8 File Offset: 0x002BE7B8
		public FindPath(Cell StartCell, Cell EndCell, bool PathGlobal = false, bool PathUnlimited = true, GameObject Looker = null, int MaxWeight = 100, bool ExploredOnly = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false, CleanQueue<XRLCore.SortPoint> Avoid = null)
		{
			if (StartCell == null || EndCell == null)
			{
				return;
			}
			Zone parentZone = StartCell.ParentZone;
			Zone parentZone2 = EndCell.ParentZone;
			if (parentZone == null || parentZone2 == null)
			{
				return;
			}
			this.PerformPathfind(parentZone, StartCell.X, StartCell.Y, parentZone2, EndCell.X, EndCell.Y, PathGlobal, Looker, true, false, false, Avoid, MaxWeight, ExploredOnly, false, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, true);
		}

		// Token: 0x0600757C RID: 30076 RVA: 0x002C0640 File Offset: 0x002BE840
		public FindPath(Zone StartZone, int X1, int Y1, Zone EndZone, int X2, int Y2, bool PathGlobal, bool PathUnlimited, GameObject Looker, CleanQueue<XRLCore.SortPoint> Avoid, bool ExploredOnly = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false)
		{
			if (StartZone == null)
			{
				return;
			}
			if (EndZone == null)
			{
				return;
			}
			this.PerformPathfind(StartZone, X1, Y1, EndZone, X2, Y2, PathGlobal, Looker, PathUnlimited, false, false, Avoid, 100, ExploredOnly, false, false, IgnoreCreatures, IgnoreGases, FlexPhase, true);
		}

		// Token: 0x0600757D RID: 30077 RVA: 0x002C06A4 File Offset: 0x002BE8A4
		private void DrawNodes()
		{
			int num = -1;
			int num2 = -1;
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			GridNode[,] grid = FindPath.Grid;
			int version = FindPath.Version;
			scrapBuffer.Clear();
			for (int i = 0; i <= grid.GetUpperBound(0); i++)
			{
				for (int j = 0; j <= grid.GetUpperBound(1); j++)
				{
					GridNode gridNode = grid[i, j];
					if (gridNode.Version != version)
					{
						if (i == this.Destination.X && j == this.Destination.Y)
						{
							scrapBuffer.WriteAt(i, j, "&MD", true);
						}
					}
					else
					{
						char c = 'S';
						char c2 = 'g';
						if (gridNode.Parent != null)
						{
							int num3 = gridNode.Parent.X - gridNode.X;
							int num4 = gridNode.Parent.Y - gridNode.Y;
							if (num3 == -1 && num4 == -1)
							{
								c = 'Ú';
							}
							else if (num3 == 0 && num4 == -1)
							{
								c = '\u0018';
							}
							else if (num3 == 1 && num4 == -1)
							{
								c = '¿';
							}
							else if (num3 == -1 && num4 == 0)
							{
								c = '\u001b';
							}
							else if (num3 == 1 && num4 == 0)
							{
								c = '\u001a';
							}
							else if (num3 == -1 && num4 == 1)
							{
								c = 'À';
							}
							else if (num3 == 0 && num4 == 1)
							{
								c = '\u0019';
							}
							else if (num3 == 1 && num4 == 1)
							{
								c = 'Ù';
							}
						}
						if (gridNode.Distance == 0)
						{
							c2 = 'O';
						}
						else if (gridNode.Closed)
						{
							c2 = 'b';
						}
						ConsoleChar consoleChar = scrapBuffer.Buffer[i, j];
						consoleChar.Tile = null;
						consoleChar.Char = c;
						consoleChar.Foreground = ColorUtility.ColorMap[c2];
					}
				}
			}
			for (GridNode gridNode2 = grid[this.Destination.X, this.Destination.Y]; gridNode2 != null; gridNode2 = gridNode2.Parent)
			{
				scrapBuffer.Buffer[gridNode2.X, gridNode2.Y].Foreground = ColorUtility.ColorMap['O'];
			}
			if (num >= 0 && num2 >= 0 && grid[num, num2] != null)
			{
				scrapBuffer.WriteAt(0, 0, "F:" + (grid[num, num2].Total + grid[num, num2].Distance).ToString(), true);
				scrapBuffer.WriteAt(0, 1, "G:" + grid[num, num2].Total.ToString(), true);
				scrapBuffer.WriteAt(0, 2, "D:" + grid[num, num2].Distance.ToString(), true);
				scrapBuffer.WriteAt(0, 3, "L:" + grid[num, num2].Length.ToString(), true);
				scrapBuffer.WriteAt(0, 4, "C:" + grid[num, num2].Cost.ToString(), true);
			}
			scrapBuffer.Draw();
			if (Options.DrawPathfinderHalt)
			{
				Keyboard.getch();
				return;
			}
			Thread.Sleep(50);
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x002C09C8 File Offset: 0x002BEBC8
		public void PerformPathfind(Zone Zone, int X1, int Y1, int X2, int Y2, int Range = 0, GameObject Looker = null, bool CardinalDirectionsOnly = false, int MaxWeight = 100, bool Autoexploring = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false, bool AllowDigging = true)
		{
			this.Clear();
			Cell cell = Zone.GetCell(X1, Y1);
			this.Destination = Zone.GetCell(X2, Y2);
			this.Looker = Looker;
			if (cell == null || this.Destination == null)
			{
				return;
			}
			NavigationQueue queue = FindPath.Queue;
			int num = ++FindPath.Version;
			Cell[][] map = Zone.Map;
			GridNode[,] grid = FindPath.Grid;
			GridNode gridNode = grid[X1, Y1];
			int num2 = 0;
			int[][] array = (CardinalDirectionsOnly ? GridNode.OffsetsCardinal : GridNode.Offsets);
			int num3 = array.Length;
			this.Nav = Zone.CalculateNav(Looker, Autoexploring, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, AllowDigging);
			gridNode.Set(cell, this, null);
			gridNode.Version = num;
			queue.Enqueue(gridNode);
			while (queue.Count != 0)
			{
				GridNode gridNode2 = queue.Dequeue();
				gridNode2.Closed = true;
				if (gridNode2.Distance <= Range)
				{
					this.Finalize(gridNode2, MaxWeight, map);
					break;
				}
				if (num2++ >= 2000)
				{
					break;
				}
				for (int i = 0; i < num3; i++)
				{
					int num4 = gridNode2.X + array[i][0];
					int num5 = gridNode2.Y + array[i][1];
					if (num4 >= 0 && num5 >= 0 && num4 < 80 && num5 < 25)
					{
						gridNode = grid[num4, num5];
						if (gridNode.Version == num)
						{
							int num6 = gridNode.Cost + gridNode2.Total;
							if (num6 <= gridNode.Total && (num6 != gridNode.Total || (gridNode.Diagonal && !gridNode.DiagonalTo(gridNode2))))
							{
								gridNode.SetParent(gridNode2);
								if (!gridNode.Closed)
								{
									queue.Requeue(gridNode);
								}
							}
						}
						else
						{
							gridNode.Set(map[num4][num5], this, gridNode2);
							gridNode.Version = num;
							if (gridNode.Weight > MaxWeight && gridNode.Distance > 0)
							{
								gridNode.Closed = true;
							}
							else
							{
								queue.Enqueue(gridNode);
							}
						}
					}
				}
			}
			if (Options.DrawPathfinder)
			{
				this.DrawNodes();
			}
		}

		// Token: 0x0600757F RID: 30079 RVA: 0x002C0BEC File Offset: 0x002BEDEC
		private void Finalize(GridNode End, int MaxWeight, Cell[][] Map)
		{
			this.Steps.EnsureCapacity<Cell>(End.Length);
			this.Steps.Add(Map[End.X][End.Y]);
			this.Weights.Add(End.Weight);
			GridNode gridNode = End;
			for (GridNode gridNode2 = End.Parent; gridNode2 != null; gridNode2 = gridNode.Parent)
			{
				if (gridNode2.Weight > MaxWeight && gridNode2.Parent != null)
				{
					this.Steps.Clear();
					this.Directions.Clear();
					this.Weights.Clear();
					this.Found = false;
					return;
				}
				this.Directions.Add(gridNode.Direction);
				this.Steps.Add(Map[gridNode2.X][gridNode2.Y]);
				this.Weights.Add(gridNode2.Weight);
				gridNode = gridNode2;
			}
			this.Steps.Reverse();
			this.Directions.Reverse();
			this.Weights.Reverse();
			this.Found = true;
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x002C0CF0 File Offset: 0x002BEEF0
		public void PerformPathfind(Zone StartZone, int X1, int Y1, Zone EndZone, int X2, int Y2, bool PathGlobal = false, GameObject Looker = null, bool Unlimited = false, bool AddNoise = false, bool CardinalDirectionsOnly = false, CleanQueue<XRLCore.SortPoint> Avoid = null, int MaxWeight = 100, bool ExploredOnly = false, bool Autoexploring = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false, bool AllowDigging = true)
		{
			using (FindPath.s_PerformPathfind.Auto())
			{
				if (StartZone == EndZone && !ExploredOnly && !AddNoise && Avoid == null)
				{
					this.PerformPathfind(StartZone, X1, Y1, X2, Y2, 0, Looker, CardinalDirectionsOnly, MaxWeight, Autoexploring, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, AllowDigging);
				}
				else
				{
					this.Clear();
					if (StartZone != null)
					{
						if (EndZone != null)
						{
							if (StartZone != EndZone || X1 != X2 || Y1 != Y2)
							{
								string directionFromZone = EndZone.GetDirectionFromZone(StartZone);
								FindPath.CellNavCount = 0;
								bool drawPathfinder = Options.DrawPathfinder;
								Cell cell = StartZone.GetCell(X1, Y1);
								Cell cell2 = EndZone.GetCell(X2, Y2);
								StartZone.CalculateNavigationMap(Looker, AddNoise, ExploredOnly, Autoexploring, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase);
								if (StartZone != EndZone)
								{
									EndZone.CalculateNavigationMap(Looker, AddNoise, ExploredOnly, Autoexploring, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase);
								}
								List<Zone> list = null;
								FindPath.OpenList.Clear();
								FindPath.CloseList.Clear();
								FindPath.OrderedNavigationValues.Clear();
								FindPath.OpenList.Add(StartZone.GetCell(X1, Y1), this.NewCellNav(0.0, 0.0, null, null, "."));
								try
								{
									ScreenBuffer screenBuffer = Popup._ScreenBuffer;
									FindPath.ZoneFilter.Clear();
									FindPath.ZoneFilter.Add(StartZone.ZoneID);
									FindPath.ZoneFilter.Add(EndZone.ZoneID);
									int num = (Unlimited ? 9999 : Math.Max(80, cell.ManhattanDistanceTo(cell2) * 5 / 4));
									if (num > 1 && !cell.IsAdjacentTo(cell2, true))
									{
										if (StartZone.NavigationMap[X1, Y1].Weight <= MaxWeight && !cell.AnyLocalAdjacentCell((Cell C) => C.ParentZone.NavigationMap[C.X, C.Y].Weight <= MaxWeight))
										{
											this.Found = false;
											return;
										}
										if (!cell2.AnyLocalAdjacentCell((Cell C) => C.ParentZone.NavigationMap[C.X, C.Y].Weight <= MaxWeight))
										{
											this.Found = false;
											return;
										}
									}
									int num2 = 0;
									while (FindPath.OpenList.Count > 0)
									{
										Cell cell3 = null;
										double num3 = double.MaxValue;
										foreach (KeyValuePair<Cell, CellNavigationValue> keyValuePair in FindPath.OpenList)
										{
											if (keyValuePair.Value.Total < num3)
											{
												num3 = keyValuePair.Value.Total;
												cell3 = keyValuePair.Key;
											}
										}
										if (drawPathfinder)
										{
											screenBuffer.WriteAt(cell3, "&Y!", true).Draw();
										}
										CellNavigationValue cellNavigationValue = FindPath.OpenList[cell3];
										if (drawPathfinder)
										{
											screenBuffer.WriteAt(cell, "&GS", true).WriteAt(cell2, "&RE", true).Draw();
										}
										string[] array = FindPath.DirectionListSorted;
										if (CardinalDirectionsOnly)
										{
											array = FindPath.DirectionListCardinalOnly;
										}
										else if (PathGlobal && StartZone != EndZone)
										{
											if (cell3.HasObjectWithPart("StairsUp"))
											{
												array = FindPath.DirectionListUSorted;
											}
											else if (cell3.HasObjectWithPart("StairsDown"))
											{
												array = FindPath.DirectionListDSorted;
											}
										}
										int i = 0;
										int num4 = array.Length;
										while (i < num4)
										{
											string text = array[i];
											num2++;
											if (num2 > 2000 && !Unlimited)
											{
												this.Found = false;
												return;
											}
											Cell cellFromDirectionWithOneValidExternalDirection = cell3.GetCellFromDirectionWithOneValidExternalDirection(text, directionFromZone, EndZone.ZoneID);
											NavigationWeight[,] array2 = StartZone.NavigationMap;
											if (cellFromDirectionWithOneValidExternalDirection != null && cellFromDirectionWithOneValidExternalDirection.ParentZone != StartZone)
											{
												if (cellFromDirectionWithOneValidExternalDirection.ParentZone == cell2.ParentZone)
												{
													array2 = EndZone.NavigationMap;
												}
												else
												{
													if (list == null)
													{
														list = new List<Zone>();
													}
													if (!list.Contains(cellFromDirectionWithOneValidExternalDirection.ParentZone))
													{
														cellFromDirectionWithOneValidExternalDirection.ParentZone.CalculateNavigationMap(Looker, AddNoise, ExploredOnly, false, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase);
													}
													array2 = cellFromDirectionWithOneValidExternalDirection.ParentZone.NavigationMap;
												}
											}
											if (cellFromDirectionWithOneValidExternalDirection == cell2 || (cellFromDirectionWithOneValidExternalDirection != null && !FindPath.CloseList.ContainsKey(cellFromDirectionWithOneValidExternalDirection) && array2[cellFromDirectionWithOneValidExternalDirection.X, cellFromDirectionWithOneValidExternalDirection.Y].Weight <= MaxWeight))
											{
												if (drawPathfinder)
												{
													screenBuffer.WriteAt(cellFromDirectionWithOneValidExternalDirection, "&K?", true).Draw();
												}
												double num5 = cell2.RealDistanceTo(cellFromDirectionWithOneValidExternalDirection, false);
												if (FindPath.CellNavCount >= 4000)
												{
													this.Found = false;
													return;
												}
												CellNavigationValue cellNavigationValue2;
												if (FindPath.OpenList.TryGetValue(cellFromDirectionWithOneValidExternalDirection, out cellNavigationValue2))
												{
													double num6 = cellNavigationValue.Cost + (double)array2[cellFromDirectionWithOneValidExternalDirection.X, cellFromDirectionWithOneValidExternalDirection.Y].Weight;
													if (Avoid != null)
													{
														for (int j = 0; j < Avoid.Items.Count; j++)
														{
															XRLCore.SortPoint sortPoint = Avoid.Items[j];
															if (sortPoint.X == cellFromDirectionWithOneValidExternalDirection.X && sortPoint.Y == cellFromDirectionWithOneValidExternalDirection.Y)
															{
																num6 = 99999.0;
															}
														}
													}
													if (text.Length > 1)
													{
														num6 += 0.001;
													}
													double num7 = num5;
													if (cellFromDirectionWithOneValidExternalDirection.ParentZone.Z != cell2.ParentZone.Z)
													{
														num7 *= 10.0;
													}
													if (text != cellNavigationValue.Direction)
													{
														num7 += 60.0;
													}
													if (num6 + num7 < cellNavigationValue2.Total)
													{
														cellNavigationValue2.Set(num6, num7, null, cell3, text);
													}
												}
												else if (FindPath.CloseList.TryGetValue(cellFromDirectionWithOneValidExternalDirection, out cellNavigationValue2))
												{
													double num8 = cellNavigationValue.Cost + (double)array2[cellFromDirectionWithOneValidExternalDirection.X, cellFromDirectionWithOneValidExternalDirection.Y].Weight;
													if (Avoid != null)
													{
														for (int k = 0; k < Avoid.Items.Count; k++)
														{
															XRLCore.SortPoint sortPoint2 = Avoid.Items[k];
															if (sortPoint2.X == cellFromDirectionWithOneValidExternalDirection.X && sortPoint2.Y == cellFromDirectionWithOneValidExternalDirection.Y)
															{
																num8 = 99999.0;
															}
														}
													}
													if (text.Length > 1)
													{
														num8 += 0.001;
													}
													double num9 = num5;
													if (cellFromDirectionWithOneValidExternalDirection.ParentZone.Z != cell2.ParentZone.Z)
													{
														num9 *= 10.0;
													}
													if (num8 + num9 < cellNavigationValue2.Total)
													{
														cellNavigationValue2.Set(num8, num9, null, cell3, text);
														FindPath.OpenList.Add(cellFromDirectionWithOneValidExternalDirection, cellNavigationValue2);
														FindPath.CloseList.Remove(cellFromDirectionWithOneValidExternalDirection);
													}
												}
												else if (num5 <= (double)num)
												{
													double num10 = cellNavigationValue.Cost + (double)array2[cellFromDirectionWithOneValidExternalDirection.X, cellFromDirectionWithOneValidExternalDirection.Y].Weight;
													if (Avoid != null)
													{
														for (int l = 0; l < Avoid.Items.Count; l++)
														{
															XRLCore.SortPoint sortPoint3 = Avoid.Items[l];
															if (sortPoint3.X == cellFromDirectionWithOneValidExternalDirection.X && sortPoint3.Y == cellFromDirectionWithOneValidExternalDirection.Y)
															{
																num10 = 99999.0;
															}
														}
													}
													if (text.Length > 1)
													{
														num10 += 0.001;
													}
													double num11 = num5;
													if (cellFromDirectionWithOneValidExternalDirection.ParentZone.Z != cell2.ParentZone.Z)
													{
														num11 *= 10.0;
													}
													FindPath.OpenList.Add(cellFromDirectionWithOneValidExternalDirection, this.NewCellNav(num10, num11, null, cell3, text));
												}
												if (cellFromDirectionWithOneValidExternalDirection == cell2)
												{
													this.Found = true;
													if (drawPathfinder)
													{
														screenBuffer.WriteAt(cell, "S", true).WriteAt(cell2, "E", true).Draw();
													}
													if (text != ".")
													{
														this.Steps.Add(cellFromDirectionWithOneValidExternalDirection);
														this.Directions.Add(text);
														this.Weights.Add(array2[cellFromDirectionWithOneValidExternalDirection.X, cellFromDirectionWithOneValidExternalDirection.Y].Weight);
													}
													Cell cell4 = cell3;
													while (cell4 != null)
													{
														this.Steps.Add(cell4);
														this.Weights.Add(array2[cell4.X, cell4.Y].Weight);
														if (drawPathfinder)
														{
															screenBuffer.WriteAt(cell4, ".", true).Draw();
														}
														if (FindPath.OpenList.ContainsKey(cell4))
														{
															if (FindPath.OpenList[cell4].Direction != ".")
															{
																this.Directions.Add(FindPath.OpenList[cell4].Direction);
															}
															cell4 = FindPath.OpenList[cell4].Parent;
														}
														else
														{
															if (FindPath.CloseList[cell4].Direction != ".")
															{
																this.Directions.Add(FindPath.CloseList[cell4].Direction);
															}
															cell4 = FindPath.CloseList[cell4].Parent;
														}
													}
													this.Directions.Reverse();
													this.Steps.Reverse();
													this.Weights.Reverse();
													if (drawPathfinder && Options.DrawPathfinderHalt)
													{
														screenBuffer.Draw();
														Keyboard.getch();
													}
													return;
												}
											}
											i++;
										}
										if (!FindPath.CloseList.ContainsKey(cell3))
										{
											FindPath.CloseList.Add(cell3, cellNavigationValue);
											FindPath.OpenList.Remove(cell3);
										}
									}
									this.Found = false;
									if (drawPathfinder && Options.DrawPathfinderHalt)
									{
										screenBuffer.Draw();
										Keyboard.getch();
									}
								}
								finally
								{
									FindPath.OpenList.Clear();
									FindPath.CloseList.Clear();
									FindPath.OrderedNavigationValues.Clear();
									for (int m = 0; m <= FindPath.CellNavCount; m++)
									{
										FindPath.CellNavs[m].Parent = null;
										FindPath.CellNavs[m].C = null;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06007581 RID: 30081 RVA: 0x002C1724 File Offset: 0x002BF924
		public void Clear()
		{
			this.Looker = null;
			this.Destination = null;
			this.Nav = 0;
			this.Found = false;
			FindPath.Queue.Clear();
			this.Directions.Clear();
			this.Steps.Clear();
			this.Weights.Clear();
		}

		// Token: 0x040035B2 RID: 13746
		public const int MAX_CELL_NAV = 4000;

		// Token: 0x040035B3 RID: 13747
		public static CellNavigationValue[] CellNavs = new CellNavigationValue[4000];

		// Token: 0x040035B4 RID: 13748
		public static int CellNavCount = -1;

		// Token: 0x040035B5 RID: 13749
		[NonSerialized]
		private static List<string> ZoneFilter = new List<string>();

		// Token: 0x040035B6 RID: 13750
		[NonSerialized]
		private static NavigationQueue Queue = new NavigationQueue();

		// Token: 0x040035B7 RID: 13751
		[NonSerialized]
		private static GridNode[,] Grid = new GridNode[80, 25];

		// Token: 0x040035B8 RID: 13752
		[NonSerialized]
		private static int Version;

		// Token: 0x040035B9 RID: 13753
		[NonSerialized]
		private static Dictionary<Cell, CellNavigationValue> OpenList = new Dictionary<Cell, CellNavigationValue>();

		// Token: 0x040035BA RID: 13754
		[NonSerialized]
		private static Dictionary<Cell, CellNavigationValue> CloseList = new Dictionary<Cell, CellNavigationValue>();

		// Token: 0x040035BB RID: 13755
		[NonSerialized]
		private static OrderedBag<CellNavigationValue> OrderedNavigationValues = new OrderedBag<CellNavigationValue>();

		// Token: 0x040035BC RID: 13756
		public static readonly string[] DirectionList = new string[] { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x040035BD RID: 13757
		public static readonly string[] DirectionListSorted = new string[] { "N", "E", "S", "W", "NW", "NE", "SE", "SW" };

		// Token: 0x040035BE RID: 13758
		public static readonly string[] DirectionListU = new string[] { "U", "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x040035BF RID: 13759
		public static readonly string[] DirectionListUSorted = new string[] { "U", "N", "E", "S", "W", "NW", "NE", "SE", "SW" };

		// Token: 0x040035C0 RID: 13760
		public static readonly string[] DirectionListD = new string[] { "D", "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x040035C1 RID: 13761
		public static readonly string[] DirectionListDSorted = new string[] { "D", "N", "E", "S", "W", "NW", "NE", "SE", "SW" };

		// Token: 0x040035C2 RID: 13762
		public static readonly string[] DirectionListCardinalOnly = new string[] { "E", "S", "N", "W" };

		// Token: 0x040035C3 RID: 13763
		public bool Found;

		// Token: 0x040035C4 RID: 13764
		public List<string> Directions = new List<string>();

		// Token: 0x040035C5 RID: 13765
		public List<Cell> Steps = new List<Cell>();

		// Token: 0x040035C6 RID: 13766
		public List<int> Weights = new List<int>();

		// Token: 0x040035C7 RID: 13767
		internal GameObject Looker;

		// Token: 0x040035C8 RID: 13768
		internal Cell Destination;

		// Token: 0x040035C9 RID: 13769
		internal int Nav;

		// Token: 0x040035CA RID: 13770
		private static readonly ProfilerMarker s_PerformPathfind = new ProfilerMarker("s_PerformPathfind");
	}
}
