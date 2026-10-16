using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using Wintellect.PowerCollections;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace Genkit
{
	// Token: 0x0200037A RID: 890
	[Serializable]
	public class InfluenceMap
	{
		// Token: 0x0600213D RID: 8509 RVA: 0x000A466C File Offset: 0x000A286C
		public InfluenceMap(int _Width, int _Height)
		{
			this.Width = _Width;
			this.Height = _Height;
			this.Walls = new int[this.Width, this.Height];
			this.CostMap = new int[this.Width, this.Height];
			this.Seeds = new List<Location2D>();
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					this.CostMap[i, j] = int.MinValue;
				}
			}
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000A4734 File Offset: 0x000A2934
		public InfluenceMap copy()
		{
			InfluenceMap influenceMap = new InfluenceMap(this.Width, this.Height);
			if (this.Walls != null)
			{
				Array.Copy(this.Walls, influenceMap.Walls, this.Width * this.Height);
			}
			if (this.CostMap != null)
			{
				Array.Copy(this.CostMap, influenceMap.CostMap, this.Width * this.Height);
			}
			if (this.Weights != null)
			{
				influenceMap.UsingWeights();
				Array.Copy(this.Weights, influenceMap.Weights, this.Width * this.Height);
				Array.Copy(this.WeightedCosts, influenceMap.WeightedCosts, this.Width * this.Height);
			}
			influenceMap.Seeds = new List<Location2D>(this.Seeds);
			if (this.SeedGrowthProbability != null)
			{
				influenceMap.SeedGrowthProbability = new List<int>(this.SeedGrowthProbability);
			}
			influenceMap.bDraw = this.bDraw;
			return influenceMap;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000A4820 File Offset: 0x000A2A20
		public void UsingWeights()
		{
			if (this.Weights == null)
			{
				this.Weights = new int[this.Width, this.Height];
			}
			if (this.WeightedCosts == null)
			{
				this.WeightedCosts = new int[this.Width, this.Height];
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000A4860 File Offset: 0x000A2A60
		public int AddGlobalRegion()
		{
			InfluenceMapRegion influenceMapRegion = new InfluenceMapRegion(this.Regions.Count, this);
			for (int i = 0; i < this.Regions.Count; i++)
			{
				influenceMapRegion.Cells.AddRange(this.Regions[i].Cells);
				influenceMapRegion.BorderCells.AddRange(this.Regions[i].BorderCells);
				influenceMapRegion.NonBorderCells.AddRange(this.Regions[i].NonBorderCells);
				influenceMapRegion.AdjacentRegions = new List<InfluenceMapRegion>();
				if (this.Regions[i].BoundingBox.x1 < influenceMapRegion.BoundingBox.x1)
				{
					influenceMapRegion.BoundingBox.x1 = this.Regions[i].BoundingBox.x1;
				}
				if (this.Regions[i].BoundingBox.x2 > influenceMapRegion.BoundingBox.x2)
				{
					influenceMapRegion.BoundingBox.x2 = this.Regions[i].BoundingBox.x2;
				}
				if (this.Regions[i].BoundingBox.y1 < influenceMapRegion.BoundingBox.y1)
				{
					influenceMapRegion.BoundingBox.y1 = this.Regions[i].BoundingBox.y1;
				}
				if (this.Regions[i].BoundingBox.y2 > influenceMapRegion.BoundingBox.y2)
				{
					influenceMapRegion.BoundingBox.y2 = this.Regions[i].BoundingBox.y2;
				}
				influenceMapRegion.Seed = this.Regions.Count;
			}
			this.Regions.Add(influenceMapRegion);
			return this.Regions.Count - 1;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000A4A34 File Offset: 0x000A2C34
		public int FindClosestSeedToInList(int seed, List<int> targetSeeds)
		{
			int num = int.MaxValue;
			int num2 = -1;
			for (int i = 0; i < targetSeeds.Count; i++)
			{
				if (num > this.Seeds[seed].SquareDistance(this.Seeds[targetSeeds[i]]))
				{
					num = this.Seeds[seed].SquareDistance(this.Seeds[targetSeeds[i]]);
					num2 = targetSeeds[i];
				}
			}
			return num2;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000A4AB0 File Offset: 0x000A2CB0
		public int FindClosestSeedTo(Location2D where, Func<InfluenceMapRegion, bool> regionFilter = null)
		{
			int num = int.MaxValue;
			int num2 = 0;
			for (int i = 0; i < this.Seeds.Count; i++)
			{
				if (this.Seeds[i].Distance(where) < num && (regionFilter == null || regionFilter(this.Regions[i])))
				{
					num2 = i;
					num = this.Seeds[i].Distance(where);
				}
			}
			return num2;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000A4B20 File Offset: 0x000A2D20
		public int FindClosestSeedToInList(Location2D location, List<int> targetSeeds)
		{
			int num = int.MaxValue;
			int num2 = -1;
			for (int i = 0; i < targetSeeds.Count; i++)
			{
				if (num > location.SquareDistance(this.Seeds[targetSeeds[i]]))
				{
					num = location.SquareDistance(this.Seeds[targetSeeds[i]]);
					num2 = targetSeeds[i];
				}
			}
			return num2;
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000A4B84 File Offset: 0x000A2D84
		public void ClearSeeds()
		{
			this.Seeds.Clear();
			this.Regions.Clear();
			this.SeedToRegionMap.Clear();
			this.NeighborCount = 0;
			if (this.Weights != null)
			{
				for (int i = 0; i < this.Width; i++)
				{
					for (int j = 0; j < this.Height; j++)
					{
						this.Weights[i, j] = 0;
					}
				}
			}
			if (this.WeightedCosts != null)
			{
				for (int k = 0; k < this.Width; k++)
				{
					for (int l = 0; l < this.Height; l++)
					{
						this.WeightedCosts[k, l] = 0;
					}
				}
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000A4C2C File Offset: 0x000A2E2C
		private void LoadNeighbors(int X, int Y)
		{
			this.NeighborCount = 0;
			if (X > 0)
			{
				if (this.Walls[X - 1, Y] == 0)
				{
					Location2D[] neighbors = this.Neighbors;
					int num = this.NeighborCount;
					this.NeighborCount = num + 1;
					neighbors[num] = Location2D.Grid[X - 1, Y];
				}
				if (Y > 0 && this.Walls[X - 1, Y - 1] == 0)
				{
					Location2D[] neighbors2 = this.Neighbors;
					int num = this.NeighborCount;
					this.NeighborCount = num + 1;
					neighbors2[num] = Location2D.Grid[X - 1, Y - 1];
				}
				if (Y < this.Height - 1 && this.Walls[X - 1, Y + 1] == 0)
				{
					Location2D[] neighbors3 = this.Neighbors;
					int num = this.NeighborCount;
					this.NeighborCount = num + 1;
					neighbors3[num] = Location2D.Grid[X - 1, Y + 1];
				}
			}
			if (X < this.Width - 1)
			{
				if (this.Walls[X + 1, Y] == 0)
				{
					Location2D[] neighbors4 = this.Neighbors;
					int num = this.NeighborCount;
					this.NeighborCount = num + 1;
					neighbors4[num] = Location2D.Grid[X + 1, Y];
				}
				if (Y > 0 && this.Walls[X + 1, Y - 1] == 0)
				{
					Location2D[] neighbors5 = this.Neighbors;
					int num = this.NeighborCount;
					this.NeighborCount = num + 1;
					neighbors5[num] = Location2D.Grid[X + 1, Y - 1];
				}
				if (Y < this.Height - 1 && this.Walls[X + 1, Y + 1] == 0)
				{
					Location2D[] neighbors6 = this.Neighbors;
					int num = this.NeighborCount;
					this.NeighborCount = num + 1;
					neighbors6[num] = Location2D.Grid[X + 1, Y + 1];
				}
			}
			if (Y > 0 && this.Walls[X, Y - 1] == 0)
			{
				Location2D[] neighbors7 = this.Neighbors;
				int num = this.NeighborCount;
				this.NeighborCount = num + 1;
				neighbors7[num] = Location2D.Grid[X, Y - 1];
			}
			if (Y < this.Height - 1 && this.Walls[X, Y + 1] == 0)
			{
				Location2D[] neighbors8 = this.Neighbors;
				int num = this.NeighborCount;
				this.NeighborCount = num + 1;
				neighbors8[num] = Location2D.Grid[X, Y + 1];
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000A4E4A File Offset: 0x000A304A
		public int GetSeedAt(Location2D P)
		{
			if (this.SeedMap.ContainsKey(P))
			{
				return this.SeedMap[P];
			}
			return -1;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000A4E68 File Offset: 0x000A3068
		public int GetSeedAt(Point2D P)
		{
			return this.GetSeedAt(P.location);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000A4E78 File Offset: 0x000A3078
		public int LargestSeed()
		{
			int num = -1;
			int num2 = 0;
			foreach (int num3 in this.SizeMap.Keys)
			{
				if (this.SizeMap[num3] > num)
				{
					num2 = num3;
					num = this.SizeMap[num3];
				}
			}
			return num2;
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000A4EEC File Offset: 0x000A30EC
		public int LargestSize()
		{
			return this.SizeMap[this.LargestSeed()];
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000A4F00 File Offset: 0x000A3100
		public bool HasUnreached()
		{
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (this.Walls[i, j] == 0 && this.CostMap[i, j] == -2147483648)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000A4F58 File Offset: 0x000A3158
		public void AddSeedAtMaxima()
		{
			int num = 0;
			Location2D location2D = Location2D.Get(0, 0);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (this.CostMap[i, j] != -2147483648 && this.CostMap[i, j] > num)
					{
						num = this.CostMap[i, j];
						location2D = Location2D.Get(i, j);
					}
				}
			}
			this.AddSeed(location2D.X, location2D.Y, true);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000A4FE0 File Offset: 0x000A31E0
		public void MoveSeedsToCenters(bool bDraw = false)
		{
			int[] array = new int[this.Seeds.Count];
			int[] array2 = new int[this.Seeds.Count];
			int[] array3 = new int[this.Seeds.Count];
			for (int i = 0; i < this.Seeds.Count; i++)
			{
				array[i] = 0;
				array2[i] = 0;
				array3[i] = 0;
			}
			for (int j = 0; j < this.Height; j++)
			{
				for (int k = 0; k < this.Width; k++)
				{
					if (this.SeedMap.ContainsKey(Location2D.Get(k, j)))
					{
						int num = this.SeedMap[Location2D.Get(k, j)];
						array[num] += k;
						array2[num] += j;
						array3[num]++;
					}
				}
			}
			for (int l = 0; l < this.Seeds.Count; l++)
			{
				if (array3[l] > 0)
				{
					array[l] /= array3[l];
					array2[l] /= array3[l];
					array3[l] = int.MaxValue;
				}
			}
			for (int m = 0; m < this.Height; m++)
			{
				for (int n = 0; n < this.Width; n++)
				{
					if (this.SeedMap.ContainsKey(Location2D.Get(n, m)))
					{
						int num2 = this.SeedMap[Location2D.Get(n, m)];
						int num3 = (n - array[num2]) * (n - array[num2]) + (m - array2[num2]) * (m - array2[num2]);
						if (num3 < array3[num2])
						{
							this.Seeds[num2] = Location2D.Get(n, m);
							array3[num2] = num3;
						}
					}
				}
			}
			this.Recalculate(false);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000A51B8 File Offset: 0x000A33B8
		public void SeedAllUnseeded(bool bDraw = false, bool bRecalculate = true)
		{
			this.RecalculateCostOnly(bDraw);
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					if (this.Walls[i, j] == 0 && this.CostMap[i, j] == -2147483648)
					{
						this.AddSeed(Location2D.Get(i, j), false);
						this.RecalculateCostOnly(bDraw);
					}
				}
			}
			if (this.Seeds.Count > 0 && bRecalculate)
			{
				this.Recalculate(false);
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000A5240 File Offset: 0x000A3440
		public void AddSeedAtTrueRandom(int MinimumDistance = 4)
		{
			List<Location2D> list = new List<Location2D>();
			list.Clear();
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					if (this.Walls[j, i] == 0 && !this.SeedMap.ContainsKey(Location2D.Get(j, i)))
					{
						list.Add(Location2D.Get(j, i));
					}
				}
			}
			if (list.Count != 0)
			{
				this.AddSeed(Calc.Random<Location2D>(list), true);
				return;
			}
			List<Location2D> list2 = new List<Location2D>();
			for (int k = 0; k < this.Width; k++)
			{
				for (int l = 0; l < this.Height; l++)
				{
					if (this.CostMap[k, l] != -2147483648)
					{
						list2.Add(Location2D.Get(k, l));
					}
				}
			}
			Algorithms.RandomShuffleInPlace<Location2D>(list2);
			foreach (Location2D location2D in list2)
			{
				if (this.CostMap[location2D.X, location2D.Y] > MinimumDistance)
				{
					this.AddSeed(location2D.X, location2D.Y, true);
					return;
				}
			}
			if (this.Seeds.Count > 0)
			{
				this.AddSeedAtMaximaInLargestSeed(true);
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000A53A0 File Offset: 0x000A35A0
		public void AddSeedAtRandom(int MinimumDistance = 4)
		{
			List<Location2D> list = new List<Location2D>();
			for (;;)
			{
				list.Clear();
				for (int i = 0; i < this.Height; i++)
				{
					for (int j = 0; j < this.Width; j++)
					{
						if (this.Walls[j, i] == 0 && !this.SeedMap.ContainsKey(Location2D.Get(j, i)))
						{
							list.Add(Location2D.Get(j, i));
						}
					}
				}
				if (list.Count == 0)
				{
					break;
				}
				this.AddSeed(Calc.Random<Location2D>(list), true);
			}
			List<Location2D> list2 = new List<Location2D>();
			for (int k = 0; k < this.Height; k++)
			{
				for (int l = 0; l < this.Width; l++)
				{
					if (this.CostMap[l, k] != -2147483648)
					{
						list2.Add(Location2D.Get(l, k));
					}
				}
			}
			Algorithms.RandomShuffleInPlace<Location2D>(list2);
			foreach (Location2D location2D in list2)
			{
				if (this.CostMap[location2D.X, location2D.Y] > MinimumDistance)
				{
					this.AddSeed(location2D.X, location2D.Y, true);
					return;
				}
			}
			if (this.Seeds.Count > 0)
			{
				this.AddSeedAtMaximaInLargestSeed(true);
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000A5504 File Offset: 0x000A3704
		public void AddSeedAtMaximaInLargestSeed(bool Break = false)
		{
			int num = 0;
			Location2D location2D = Location2D.Get(0, 0);
			int num2 = this.LargestSeed();
			if (this.Seeds.Count == 0)
			{
				if (!Break)
				{
					this.AddSeedAtRandom(4);
				}
				return;
			}
			for (int i = 0; i < this.Width; i++)
			{
				for (int j = 0; j < this.Height; j++)
				{
					Location2D location2D2 = Location2D.Get(i, j);
					if (this.CostMap[location2D2.X, location2D2.Y] != -2147483648 && this.GetSeedAt(location2D2) == num2 && this.CostMap[location2D2.X, location2D2.Y] > num)
					{
						num = this.CostMap[location2D2.X, location2D2.Y];
						location2D = location2D2;
					}
				}
			}
			this.AddSeed(location2D.X, location2D.Y, true);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000A55E8 File Offset: 0x000A37E8
		public void Recalculate(bool bDraw = false)
		{
			if (this.Seeds.Count == 0)
			{
				throw new Exception("No seeds were defined");
			}
			if (this.Seeds.Count != this.Seeds.Distinct<Location2D>().Count<Location2D>())
			{
				Debug.LogWarning("Seeds list contained dupes");
				this.Seeds = this.Seeds.Distinct<Location2D>().ToList<Location2D>();
			}
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.CostMap[j, i] = int.MinValue;
				}
			}
			if (this.WeightedCosts != null)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						this.WeightedCosts[l, k] = int.MaxValue;
					}
				}
			}
			this.SeedMap.Clear();
			this.SizeMap.Clear();
			InfluenceMap.Frontier.Clear();
			InfluenceMap.SkipRegion.Clear();
			this.SeedToRegionMap.Clear();
			this.Regions.Clear();
			int num = 0;
			using (List<Location2D>.Enumerator enumerator = this.Seeds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Location2D location2D = enumerator.Current;
					InfluenceMap.Frontier.Enqueue(location2D);
					this.CostMap[location2D.X, location2D.Y] = 0;
					if (this.WeightedCosts != null)
					{
						this.WeightedCosts[location2D.X, location2D.Y] = 0;
					}
					this.SeedMap.Add(location2D, num);
					this.SizeMap.Add(num, 0);
					this.Regions.Add(new InfluenceMapRegion(num, this));
					this.Regions[num].AddCell(location2D);
					this.SeedToRegionMap.Add(num, this.Regions[num]);
					num++;
				}
				goto IL_04D1;
			}
			Location2D location2D2;
			int x;
			int y;
			int num2;
			for (;;)
			{
				IL_01F9:
				location2D2 = InfluenceMap.Frontier.Dequeue();
				x = location2D2.X;
				y = location2D2.Y;
				num2 = this.SeedMap[location2D2];
				this.SeedToRegionMap[num2].Size++;
				this.LoadNeighbors(x, y);
				if (this.SeedGrowthProbability == null || this.SeedGrowthProbability.Count <= num2 || Stat.Random(1, 1000) <= this.SeedGrowthProbability[num2])
				{
					break;
				}
				InfluenceMap.Frontier.Enqueue(location2D2);
			}
			for (int m = 0; m < this.NeighborCount; m++)
			{
				Location2D location2D3 = this.Neighbors[m];
				int x2 = location2D3.X;
				int y2 = location2D3.Y;
				bool flag = false;
				if (this.CostMap[x2, y2] == -2147483648)
				{
					this.CostMap[x2, y2] = this.CostMap[x, y] + 1;
					if (this.WeightedCosts != null)
					{
						this.WeightedCosts[x2, y2] = this.WeightedCosts[x, y] + Math.Max(this.Weights[x2, y2], 1);
					}
					this.SeedMap.Add(location2D3, num2);
					this.Regions[num2].AddCell(location2D3);
					this.Regions[num2].NonBorderCells.Add(location2D3);
					Dictionary<int, int> sizeMap = this.SizeMap;
					int num3 = num2;
					sizeMap[num3]++;
					InfluenceMap.Frontier.Enqueue(location2D3);
					flag = true;
				}
				else if (!InfluenceMap.SkipRegion.Contains(location2D3) && this.SeedToRegionMap[this.SeedMap[location2D3]] != this.SeedToRegionMap[num2])
				{
					InfluenceMapRegion influenceMapRegion = this.SeedToRegionMap[this.SeedMap[location2D3]];
					if (!this.SeedToRegionMap[num2].AdjacentRegions.Contains(influenceMapRegion))
					{
						this.SeedToRegionMap[num2].AdjacentRegions.Add(influenceMapRegion);
					}
					if (!this.SeedToRegionMap[num2].BorderCells.Contains(location2D2))
					{
						this.SeedToRegionMap[num2].BorderCells.Add(location2D2);
					}
				}
				if (this.WeightedCosts != null)
				{
					int num4 = this.WeightedCosts[x, y] + Math.Max(this.Weights[x2, y2], 1);
					if (num4 < this.WeightedCosts[x2, y2])
					{
						this.WeightedCosts[x2, y2] = num4;
						if (!flag)
						{
							InfluenceMap.Frontier.Enqueue(location2D3);
							InfluenceMap.SkipRegion.Add(location2D3);
						}
					}
				}
			}
			if (bDraw)
			{
				this.Draw(false, false);
			}
			IL_04D1:
			if (InfluenceMap.Frontier.Count <= 0)
			{
				if (bDraw)
				{
					this.Draw(false, true);
				}
				InfluenceMap.SkipRegion.Clear();
				return;
			}
			goto IL_01F9;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000A5AFC File Offset: 0x000A3CFC
		public int GetCostAtPoint(Point2D P)
		{
			return this.GetCostAtPoint(P.location);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000A5B0C File Offset: 0x000A3D0C
		public int GetCostAtPoint(Location2D P)
		{
			int maxValue = int.MaxValue;
			if (P == null)
			{
				return maxValue;
			}
			if (P.X < 0 || P.X >= this.Width || P.Y < 0 || P.Y >= this.Height)
			{
				return int.MaxValue;
			}
			if (this.CostMap[P.X, P.Y] == -2147483648)
			{
				return int.MaxValue;
			}
			return this.CostMap[P.X, P.Y];
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000A5B9C File Offset: 0x000A3D9C
		public int GetCostAtPoint(int x, int y)
		{
			if (x < 0 || x >= this.Width || y < 0 || y >= this.Height)
			{
				return int.MaxValue;
			}
			if (this.CostMap[x, y] == -2147483648)
			{
				return int.MaxValue;
			}
			return this.CostMap[x, y];
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000A5BF0 File Offset: 0x000A3DF0
		public int GetCostFromPointDirection(Location2D P, string Dir)
		{
			return this.GetCostAtPoint(P.FromDirection(Dir));
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000A5BFF File Offset: 0x000A3DFF
		public int GetWeightedCostAtPoint(Point2D P)
		{
			return this.GetWeightedCostAtPoint(P.location);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000A5C10 File Offset: 0x000A3E10
		public int GetWeightedCostAtPoint(Location2D P)
		{
			if (P == null || this.WeightedCosts == null)
			{
				return int.MaxValue;
			}
			if (P.X < 0 || P.X >= this.Width || P.Y < 0 || P.Y >= this.Height)
			{
				return int.MaxValue;
			}
			return this.WeightedCosts[P.X, P.Y];
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000A5C7F File Offset: 0x000A3E7F
		public int GetWeightedCostFromPointDirection(Location2D P, string Dir)
		{
			return this.GetWeightedCostAtPoint(P.FromDirection(Dir));
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000A5C8E File Offset: 0x000A3E8E
		public string GetLowestCostDirectionFrom(Point2D P)
		{
			return this.GetLowestCostDirectionFrom(P.location);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000A5CA0 File Offset: 0x000A3EA0
		public string GetLowestCostDirectionFrom(Location2D P)
		{
			if (P == null)
			{
				return ".";
			}
			string text = ".";
			int num = int.MaxValue;
			for (int i = 0; i < Location2D.Directions.Length; i++)
			{
				int costFromPointDirection = this.GetCostFromPointDirection(P, Location2D.Directions[i]);
				if (costFromPointDirection < num)
				{
					text = Location2D.Directions[i];
					num = costFromPointDirection;
				}
			}
			return text;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000A5CF8 File Offset: 0x000A3EF8
		public string GetLowestWeightedCostDirectionFrom(Point2D P)
		{
			return this.GetLowestWeightedCostDirectionFrom(P.location);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000A5D08 File Offset: 0x000A3F08
		public string GetLowestWeightedCostDirectionFrom(Location2D P)
		{
			if (P == null)
			{
				return ".";
			}
			string text = ".";
			int num = int.MaxValue;
			for (int i = 0; i < Location2D.Directions.Length; i++)
			{
				int weightedCostFromPointDirection = this.GetWeightedCostFromPointDirection(P, Location2D.Directions[i]);
				if (weightedCostFromPointDirection < num)
				{
					text = Location2D.Directions[i];
					num = weightedCostFromPointDirection;
				}
			}
			return text;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000A5D60 File Offset: 0x000A3F60
		public void RecalculateCostOnly(bool Draw = false)
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.CostMap[j, i] = int.MinValue;
				}
			}
			if (this.WeightedCosts != null)
			{
				for (int k = 0; k < this.Height; k++)
				{
					for (int l = 0; l < this.Width; l++)
					{
						this.WeightedCosts[l, k] = int.MaxValue;
					}
				}
			}
			if (this.Seeds.Count == 0)
			{
				return;
			}
			InfluenceMap.Frontier.Clear();
			using (List<Location2D>.Enumerator enumerator = this.Seeds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Location2D location2D = enumerator.Current;
					InfluenceMap.Frontier.Enqueue(location2D);
					this.CostMap[location2D.X, location2D.Y] = 0;
					if (this.WeightedCosts != null)
					{
						this.WeightedCosts[location2D.X, location2D.Y] = 0;
					}
				}
				goto IL_020A;
			}
			IL_0118:
			Location2D location2D2 = InfluenceMap.Frontier.Dequeue();
			int x = location2D2.X;
			int y = location2D2.Y;
			this.LoadNeighbors(x, y);
			for (int m = 0; m < this.NeighborCount; m++)
			{
				Location2D location2D3 = this.Neighbors[m];
				int x2 = location2D3.X;
				int y2 = location2D3.Y;
				bool flag = false;
				if (this.CostMap[x2, y2] == -2147483648)
				{
					this.CostMap[x2, y2] = this.CostMap[x, y] + 1;
					InfluenceMap.Frontier.Enqueue(location2D3);
					flag = true;
				}
				if (this.WeightedCosts != null)
				{
					int num = this.WeightedCosts[x, y] + Math.Max(this.Weights[x2, y2], 1);
					if (num < this.WeightedCosts[x2, y2])
					{
						this.WeightedCosts[x2, y2] = num;
						if (!flag)
						{
							InfluenceMap.Frontier.Enqueue(location2D3);
						}
					}
				}
			}
			IL_020A:
			if (InfluenceMap.Frontier.Count <= 0)
			{
				if (Draw || Options.DrawInfluenceMaps)
				{
					this.DrawCostsOnly(false);
				}
				return;
			}
			goto IL_0118;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000A5FA8 File Offset: 0x000A41A8
		public bool DrawLine(int x0, int y0, int x1, int y1, ScreenBuffer SB)
		{
			bool flag = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
			if (flag)
			{
				int num = x0;
				x0 = y0;
				y0 = num;
				int num2 = x1;
				x1 = y1;
				y1 = num2;
			}
			int num3 = x1 - x0;
			int num4 = Math.Abs(y1 - y0);
			int num5 = Math.Abs(num3) / 2;
			int num6 = y0;
			int num7;
			if (y0 < y1)
			{
				num7 = 1;
			}
			else
			{
				num7 = -1;
			}
			if (x0 > x1)
			{
				for (int i = x0; i >= x1; i--)
				{
					if (flag)
					{
						SB.Goto(num6, i);
						SB.Write("#", true, false, false, null, -1);
					}
					else
					{
						SB.Goto(i, num6);
						SB.Write("#", true, false, false, null, -1);
					}
					num5 -= num4;
					if (num5 < 0)
					{
						num6 += num7;
						num5 += Math.Abs(num3);
					}
				}
			}
			else
			{
				for (int j = x0; j <= x1; j++)
				{
					if (flag)
					{
						SB.Goto(num6, j);
						SB.Write("#", true, false, false, null, -1);
					}
					else
					{
						SB.Goto(j, num6);
						SB.Write("#", true, false, false, null, -1);
					}
					num5 -= num4;
					if (num5 < 0)
					{
						num6 += num7;
						num5 += Math.Abs(num3);
					}
				}
			}
			return true;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000A60DC File Offset: 0x000A42DC
		public void DrawProperty(List<string> properties, Dictionary<InfluenceMapRegion, string> propertyMap, bool bDrawConnectionMap = true)
		{
			ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
			List<string> list = new List<string>();
			list.Add("&G");
			list.Add("&R");
			list.Add("&B");
			list.Add("&M");
			list.Add("&W");
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					scrapBuffer.Goto(j, i);
					if (this.CostMap[j, i] != -2147483648)
					{
						int num = this.CostMap[j, i];
						int num2 = this.SeedMap[Location2D.Get(j, i)];
						string text;
						if (propertyMap.ContainsKey(this.SeedToRegionMap[num2]))
						{
							text = list[properties.IndexOf(propertyMap[this.SeedToRegionMap[num2]])];
						}
						else
						{
							text = "&K";
						}
						string text2 = num.ToString("X");
						scrapBuffer.Write(text + text2, true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("&K.", true, false, false, null, -1);
					}
				}
			}
			if (bDrawConnectionMap)
			{
				for (int k = 0; k < this.Seeds.Count; k++)
				{
					foreach (InfluenceMapRegion influenceMapRegion in this.Regions[k].AdjacentRegions)
					{
						this.DrawLine(this.Seeds[k].X, this.Seeds[k].Y, this.Seeds[influenceMapRegion.Seed].X, this.Seeds[influenceMapRegion.Seed].Y, scrapBuffer);
					}
				}
			}
			XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
			Keyboard.getch();
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000A62F4 File Offset: 0x000A44F4
		public void Draw(bool bDrawConnectionMap = true, bool bWait = true)
		{
			ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					scrapBuffer.Goto(j, i);
					if (this.CostMap[j, i] != -2147483648)
					{
						int num = this.CostMap[j, i];
						int num2;
						if (!this.SeedMap.TryGetValue(Location2D.Get(j, i), out num2))
						{
							num2 = 0;
						}
						string text = "&y";
						int num3 = num2 % 15;
						if (num3 == 0)
						{
							text = "&y";
						}
						if (num3 == 1)
						{
							text = "&R";
						}
						if (num3 == 2)
						{
							text = "&G";
						}
						if (num3 == 3)
						{
							text = "&B";
						}
						if (num3 == 4)
						{
							text = "&M";
						}
						if (num3 == 5)
						{
							text = "&C";
						}
						if (num3 == 6)
						{
							text = "&W";
						}
						if (num3 == 7)
						{
							text = "&r";
						}
						if (num3 == 8)
						{
							text = "&g";
						}
						if (num3 == 9)
						{
							text = "&b";
						}
						if (num3 == 10)
						{
							text = "&m";
						}
						if (num3 == 11)
						{
							text = "&c";
						}
						if (num3 == 12)
						{
							text = "&w";
						}
						if (num3 == 13)
						{
							text = "&Y";
						}
						if (num3 == 14)
						{
							text = "&K";
						}
						string text2;
						if (num <= 9)
						{
							text2 = num.ToString();
						}
						else
						{
							text2 = (65 + (num - 10)).ToString();
						}
						scrapBuffer.Write(text + text2, true, false, false, null, -1);
					}
					else if (this.Walls[j, i] > 0)
					{
						scrapBuffer.Write("&r.", true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("&K.", true, false, false, null, -1);
					}
				}
			}
			if (bDrawConnectionMap)
			{
				int num4 = 0;
				while (num4 < this.Seeds.Count && num4 < this.Regions.Count)
				{
					foreach (InfluenceMapRegion influenceMapRegion in this.Regions[num4].AdjacentRegions)
					{
						this.DrawLine(this.Seeds[num4].X, this.Seeds[num4].Y, this.Seeds[influenceMapRegion.Seed].X, this.Seeds[influenceMapRegion.Seed].Y, scrapBuffer);
					}
					num4++;
				}
			}
			XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
			if (bWait)
			{
				Keyboard.getch();
			}
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000A6590 File Offset: 0x000A4790
		public void DrawCostsOnly(bool wait = false)
		{
			ScreenBuffer scrapBuffer = Popup.ScrapBuffer;
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					scrapBuffer.Goto(j, i);
					if (this.CostMap[j, i] != -2147483648)
					{
						int num = this.CostMap[j, i];
						string text = "&B";
						string text2 = num.ToString("X");
						scrapBuffer.Write(text + text2, true, false, false, null, -1);
					}
					else
					{
						scrapBuffer.Write("&K.", true, false, false, null, -1);
					}
				}
			}
			XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
			if (wait)
			{
				Keyboard.getch();
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000A6648 File Offset: 0x000A4848
		public void ClearWalls()
		{
			for (int i = 0; i < this.Height; i++)
			{
				for (int j = 0; j < this.Width; j++)
				{
					this.Walls[j, i] = 0;
				}
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000A6685 File Offset: 0x000A4885
		public void AddSeed(Location2D P, bool bRecalculate = true)
		{
			this.AddSeed(P.X, P.Y, bRecalculate);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000A669C File Offset: 0x000A489C
		public void AddSeed(int x, int y, bool bRecalculate = true)
		{
			for (int i = 0; i < this.Seeds.Count; i++)
			{
				if (this.Seeds[i].X == x && this.Seeds[i].Y == y)
				{
					if (bRecalculate)
					{
						this.Recalculate(false);
					}
					return;
				}
			}
			this.Seeds.Add(Location2D.Get(x, y));
			if (bRecalculate)
			{
				this.Recalculate(false);
			}
		}

		// Token: 0x040010C1 RID: 4289
		private int Width;

		// Token: 0x040010C2 RID: 4290
		private int Height;

		// Token: 0x040010C3 RID: 4291
		public int[,] Walls;

		// Token: 0x040010C4 RID: 4292
		public int[,] Weights;

		// Token: 0x040010C5 RID: 4293
		public int[,] WeightedCosts;

		// Token: 0x040010C6 RID: 4294
		public List<Location2D> Seeds;

		// Token: 0x040010C7 RID: 4295
		public List<int> SeedGrowthProbability;

		// Token: 0x040010C8 RID: 4296
		public bool bDraw;

		// Token: 0x040010C9 RID: 4297
		public List<InfluenceMapRegion> Regions = new List<InfluenceMapRegion>();

		// Token: 0x040010CA RID: 4298
		public Dictionary<int, InfluenceMapRegion> SeedToRegionMap = new Dictionary<int, InfluenceMapRegion>();

		// Token: 0x040010CB RID: 4299
		private Location2D[] Neighbors = new Location2D[8];

		// Token: 0x040010CC RID: 4300
		private int NeighborCount;

		// Token: 0x040010CD RID: 4301
		public int[,] CostMap;

		// Token: 0x040010CE RID: 4302
		public Dictionary<Location2D, int> SeedMap = new Dictionary<Location2D, int>();

		// Token: 0x040010CF RID: 4303
		public Dictionary<int, int> SizeMap = new Dictionary<int, int>();

		// Token: 0x040010D0 RID: 4304
		[NonSerialized]
		private static Queue<Location2D> Frontier = new Queue<Location2D>();

		// Token: 0x040010D1 RID: 4305
		[NonSerialized]
		private static List<Location2D> SkipRegion = new List<Location2D>();
	}
}
