using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleLib.Console;
using UnityEngine;
using XRL;
using XRL.Core;
using XRL.UI;
using XRL.Wish;
using XRL.World;
using XRL.World.AI.Pathfinding;

namespace Genkit
{
	// Token: 0x0200038B RID: 907
	[HasWishCommand]
	public class Pathfinder : IDisposable
	{
		// Token: 0x06002215 RID: 8725 RVA: 0x000AA514 File Offset: 0x000A8714
		[WishCommand("pathbench", null)]
		public static void pathbench()
		{
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				global::UnityEngine.Debug.Log("Beginning Pathfider Benchmark...");
				Stopwatch stopwatch = Stopwatch.StartNew();
				int num = 0;
				foreach (global::XRL.World.GameObject gameObject in The.PlayerCell.ParentZone.GetObjectsWithPart("Combat"))
				{
					Zone currentZone = gameObject.CurrentZone;
					new FindPath().PerformPathfind(gameObject.CurrentZone, gameObject.CurrentCell.X, gameObject.CurrentCell.Y, 80 - gameObject.CurrentCell.X, 25 - gameObject.CurrentCell.Y, 0, gameObject, false, 95, false, false, false, false, false, true);
					num++;
				}
				stopwatch.Stop();
				global::UnityEngine.Debug.Log(string.Format("Benchmark: {0}ms {1}ticks [{2} paths]", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, num));
			}, 0);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000AA548 File Offset: 0x000A8748
		public Pathfinder(int width, int height)
		{
			this.width = width;
			this.height = height;
			this.CurrentNavigationMap = new int[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					Location2D location2D = Location2D.Get(i, j);
					PathfinderNode pathfinderNode = PathfinderNode.fromPool();
					pathfinderNode.pos = location2D;
					this.nodesByPosition.Add(location2D, pathfinderNode);
					this.addNodes.Add(pathfinderNode);
				}
			}
			for (int k = 0; k < width; k++)
			{
				for (int l = 0; l < height; l++)
				{
					if (k > 0)
					{
						this.nodesByPosition[Location2D.Get(k, l)].adjacentNodes.Add(this.nodesByPosition[Location2D.Get(k - 1, l)]);
						this.nodesByPosition[Location2D.Get(k, l)].nodesByDirection.Add("W", this.nodesByPosition[Location2D.Get(k - 1, l)]);
					}
					if (k < width - 1)
					{
						this.nodesByPosition[Location2D.Get(k, l)].adjacentNodes.Add(this.nodesByPosition[Location2D.Get(k + 1, l)]);
						this.nodesByPosition[Location2D.Get(k, l)].nodesByDirection.Add("E", this.nodesByPosition[Location2D.Get(k + 1, l)]);
					}
					if (l > 0)
					{
						this.nodesByPosition[Location2D.Get(k, l)].adjacentNodes.Add(this.nodesByPosition[Location2D.Get(k, l - 1)]);
						this.nodesByPosition[Location2D.Get(k, l)].nodesByDirection.Add("N", this.nodesByPosition[Location2D.Get(k, l - 1)]);
					}
					if (l < height - 1)
					{
						this.nodesByPosition[Location2D.Get(k, l)].adjacentNodes.Add(this.nodesByPosition[Location2D.Get(k, l + 1)]);
						this.nodesByPosition[Location2D.Get(k, l)].nodesByDirection.Add("S", this.nodesByPosition[Location2D.Get(k, l + 1)]);
					}
				}
			}
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000AA804 File Offset: 0x000A8A04
		public static void Initalize()
		{
			if (Pathfinder.CellNavs == null)
			{
				Pathfinder.CellNavs = new CellNavigationValue[16800];
				for (int i = 0; i < 16800; i++)
				{
					Pathfinder.CellNavs[i] = new CellNavigationValue(0, 0, null, null, null);
				}
				return;
			}
			for (int j = 0; j < 16800; j++)
			{
				Pathfinder.CellNavs[j].Set(0, 0, null, null, null);
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000AA86B File Offset: 0x000A8A6B
		public CellNavigationValue NewCellNav(int g, int h, PathfinderNode cCell, PathfinderNode pParent, string pDirection)
		{
			if (Pathfinder.nCellNav >= 16799)
			{
				return null;
			}
			Pathfinder.nCellNav++;
			return Pathfinder.CellNavs[Pathfinder.nCellNav].Set(g, h, cCell, pParent, pDirection);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000AA8A0 File Offset: 0x000A8AA0
		public void setWeightsFromGrid<T>(Grid<T> grid, Func<int, int, T, int> weightFunc)
		{
			grid.forEach(delegate(int x, int y, T c)
			{
				this.CurrentNavigationMap[x, y] = weightFunc(x, y, c);
			});
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000AA8D4 File Offset: 0x000A8AD4
		private int CellDistance(PathfinderNode c1, PathfinderNode c2)
		{
			int num = Math.Abs(c1.X - c2.X);
			int num2 = Math.Abs(c1.Y - c2.Y);
			return Math.Max(num, num2);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000AA90C File Offset: 0x000A8B0C
		public bool FindPath(Location2D Start, Location2D End, bool Display = false, bool CardinalDirectionsOnly = false, int MaxDistance = 9999, bool ShuffleDirections = false)
		{
			return this.FindPath(this.nodesByPosition[Start], this.nodesByPosition[End], Display, CardinalDirectionsOnly, MaxDistance, ShuffleDirections);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000AA934 File Offset: 0x000A8B34
		public bool FindPath(PathfinderNode Start, PathfinderNode Finish, bool Display = true, bool CardinalDirectionsOnly = false, int MaxDistance = 9999, bool ShuffleDirections = false)
		{
			if (Pathfinder.CellNavs == null)
			{
				Pathfinder.Initalize();
			}
			Pathfinder.nCellNav = 0;
			ScreenBuffer screenBuffer = Popup._ScreenBuffer;
			this.Steps.Clear();
			this.Directions.Clear();
			this.OpenList.Clear();
			this.CloseList.Clear();
			this.OpenList.Add(Start, new CellNavigationValue(0, 0, null, null, "."));
			List<string> list;
			if (CardinalDirectionsOnly)
			{
				list = new List<string>(Pathfinder.DirectionListCardinalOnly);
			}
			else
			{
				list = new List<string>(Pathfinder.DirectionListSorted);
			}
			while (this.OpenList.Count > 0)
			{
				PathfinderNode pathfinderNode = null;
				int num = 999999999;
				foreach (PathfinderNode pathfinderNode2 in this.OpenList.Keys)
				{
					if (this.OpenList[pathfinderNode2].EstimatedTotalCost < num)
					{
						num = this.OpenList[pathfinderNode2].EstimatedTotalCost;
						pathfinderNode = pathfinderNode2;
					}
				}
				if (Display)
				{
					screenBuffer.Goto(pathfinderNode.X, pathfinderNode.Y);
					screenBuffer.Write("&Y!", true, false, false, null, -1);
					XRLCore._Console.DrawBuffer(screenBuffer, null, false);
				}
				CellNavigationValue cellNavigationValue = this.OpenList[pathfinderNode];
				if (Display)
				{
					screenBuffer.Goto(Start.X, Start.Y);
					screenBuffer.Write("&GS", true, false, false, null, -1);
					screenBuffer.Goto(Finish.X, Finish.Y);
					screenBuffer.Write("&RE", true, false, false, null, -1);
					XRLCore._Console.DrawBuffer(screenBuffer, null, false);
				}
				if (ShuffleDirections)
				{
					list.ShuffleInPlace<string>(null);
				}
				foreach (string text in list)
				{
					PathfinderNode nodeFromDirection = pathfinderNode.GetNodeFromDirection(text);
					if (nodeFromDirection == Finish || (nodeFromDirection != null && !this.CloseList.ContainsKey(nodeFromDirection) && this.CurrentNavigationMap[nodeFromDirection.X, nodeFromDirection.Y] < 9999))
					{
						if (Display)
						{
							screenBuffer.Goto(nodeFromDirection.X, nodeFromDirection.Y);
							screenBuffer.Write("&K?", true, false, false, null, -1);
							XRLCore._Console.DrawBuffer(screenBuffer, null, false);
						}
						int num2 = this.CellDistance(Finish, nodeFromDirection);
						if (Pathfinder.nCellNav >= 4000)
						{
							this.Found = false;
							for (int i = 0; i < 4000; i++)
							{
								Pathfinder.CellNavs[i].cCell = null;
								Pathfinder.CellNavs[i].Parent = null;
							}
							this.OpenList.Clear();
							this.CloseList.Clear();
							return this.Found;
						}
						if (this.OpenList.ContainsKey(nodeFromDirection))
						{
							int num3 = cellNavigationValue.EstimatedTotalCost + this.CurrentNavigationMap[nodeFromDirection.X, nodeFromDirection.Y] + num2;
							int num4 = num2;
							if (this.OpenList[nodeFromDirection].EstimatedTotalCost < num3)
							{
								this.OpenList[nodeFromDirection].Set(cellNavigationValue.EstimatedNodeToGoal + this.CurrentNavigationMap[nodeFromDirection.X, nodeFromDirection.Y], num4 + this.CurrentNavigationMap[nodeFromDirection.X, nodeFromDirection.Y], null, pathfinderNode, text);
							}
						}
						else if (num2 <= MaxDistance)
						{
							int num5 = num2;
							this.OpenList.Add(nodeFromDirection, this.NewCellNav(cellNavigationValue.EstimatedNodeToGoal + this.CurrentNavigationMap[nodeFromDirection.X, nodeFromDirection.Y], num5 + this.CurrentNavigationMap[nodeFromDirection.X, nodeFromDirection.Y], null, pathfinderNode, text));
						}
						if (nodeFromDirection == Finish)
						{
							this.Found = true;
							if (Display)
							{
								screenBuffer.Goto(Start.X, Start.Y);
								screenBuffer.Write("S", true, false, false, null, -1);
								screenBuffer.Goto(Finish.X, Finish.Y);
								screenBuffer.Write("E", true, false, false, null, -1);
							}
							if (text != ".")
							{
								this.Steps.Add(nodeFromDirection);
								this.Directions.Add(text);
							}
							PathfinderNode pathfinderNode3 = pathfinderNode;
							while (pathfinderNode3 != null && !this.Steps.Contains(pathfinderNode3))
							{
								this.Steps.Add(pathfinderNode3);
								if (Display)
								{
									screenBuffer.Goto(pathfinderNode3.X, pathfinderNode3.Y);
									screenBuffer.Write(".", true, false, false, null, -1);
									XRLCore._Console.DrawBuffer(screenBuffer, null, false);
								}
								if (this.OpenList.ContainsKey(pathfinderNode3))
								{
									if (this.OpenList[pathfinderNode3].Direction != ".")
									{
										this.Directions.Add(this.OpenList[pathfinderNode3].Direction);
									}
									pathfinderNode3 = this.OpenList[pathfinderNode3].Parent;
								}
								else
								{
									if (this.CloseList[pathfinderNode3].Direction != ".")
									{
										this.Directions.Add(this.CloseList[pathfinderNode3].Direction);
									}
									pathfinderNode3 = this.CloseList[pathfinderNode3].Parent;
								}
							}
							this.Directions.Reverse();
							this.Steps.Reverse();
							for (int j = 0; j <= Pathfinder.nCellNav; j++)
							{
								Pathfinder.CellNavs[j].Parent = null;
								Pathfinder.CellNavs[j].cCell = null;
							}
							if (Display)
							{
								XRLCore._Console.DrawBuffer(screenBuffer, null, false);
								Keyboard.getch();
							}
							this.OpenList.Clear();
							this.CloseList.Clear();
							return this.Found;
						}
					}
				}
				if (!this.CloseList.ContainsKey(pathfinderNode))
				{
					this.CloseList.Add(pathfinderNode, cellNavigationValue);
					this.OpenList.Remove(pathfinderNode);
					continue;
				}
				continue;
			}
			this.Found = false;
			for (int k = 0; k <= Pathfinder.nCellNav; k++)
			{
				Pathfinder.CellNavs[k].Parent = null;
				Pathfinder.CellNavs[k].cCell = null;
			}
			if (Display)
			{
				XRLCore._Console.DrawBuffer(screenBuffer, null, false);
				Keyboard.getch();
			}
			this.OpenList.Clear();
			this.CloseList.Clear();
			return this.Found;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000AAFF0 File Offset: 0x000A91F0
		public static Pathfinder next(int width, int height)
		{
			if (width != 80 || height != 25)
			{
				return new Pathfinder(width, height);
			}
			if (Pathfinder.pathfinderPool.Count > 0)
			{
				return Pathfinder.pathfinderPool.Dequeue();
			}
			return new Pathfinder(width, height);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000AB024 File Offset: 0x000A9224
		public void Dispose()
		{
			try
			{
				if (this.width != 80 || this.height != 25)
				{
					using (List<PathfinderNode>.Enumerator enumerator = this.addNodes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							PathfinderNode pathfinderNode = enumerator.Current;
							if (pathfinderNode != null)
							{
								pathfinderNode.Dispose();
							}
						}
						goto IL_0075;
					}
				}
				this.Steps.Clear();
				this.Directions.Clear();
				this.Found = false;
				Pathfinder.pathfinderPool.Enqueue(this);
				IL_0075:;
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Pathfinder::Dispose", ex, "game_exception");
			}
		}

		// Token: 0x0400110F RID: 4367
		public List<PathfinderNode> addNodes = new List<PathfinderNode>(2000);

		// Token: 0x04001110 RID: 4368
		public Dictionary<Location2D, PathfinderNode> nodesByPosition = new Dictionary<Location2D, PathfinderNode>(64);

		// Token: 0x04001111 RID: 4369
		private int width;

		// Token: 0x04001112 RID: 4370
		private int height;

		// Token: 0x04001113 RID: 4371
		public static CellNavigationValue[] CellNavs = null;

		// Token: 0x04001114 RID: 4372
		public static int nCellNav = -1;

		// Token: 0x04001115 RID: 4373
		public static string[] DirectionList = new string[] { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x04001116 RID: 4374
		public static string[] DirectionListSorted = new string[] { "N", "E", "S", "W", "NW", "NE", "SE", "SW" };

		// Token: 0x04001117 RID: 4375
		public static string[] DirectionListU = new string[] { "U", "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x04001118 RID: 4376
		public static string[] DirectionListD = new string[] { "D", "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x04001119 RID: 4377
		public static string[] DirectionListCardinalOnly = new string[] { "E", "S", "N", "W" };

		// Token: 0x0400111A RID: 4378
		public int[,] CurrentNavigationMap;

		// Token: 0x0400111B RID: 4379
		public const int MAXWEIGHT = 9999;

		// Token: 0x0400111C RID: 4380
		public bool Found;

		// Token: 0x0400111D RID: 4381
		public List<string> Directions = new List<string>();

		// Token: 0x0400111E RID: 4382
		public List<PathfinderNode> Steps = new List<PathfinderNode>(2000);

		// Token: 0x0400111F RID: 4383
		private Dictionary<PathfinderNode, CellNavigationValue> OpenList = new Dictionary<PathfinderNode, CellNavigationValue>();

		// Token: 0x04001120 RID: 4384
		private Dictionary<PathfinderNode, CellNavigationValue> CloseList = new Dictionary<PathfinderNode, CellNavigationValue>();

		// Token: 0x04001121 RID: 4385
		private static Queue<Pathfinder> pathfinderPool = new Queue<Pathfinder>();
	}
}
