using System;

namespace XRL.World.AI.Pathfinding
{
	// Token: 0x02000B71 RID: 2929
	internal class GridNode : IComparable<GridNode>, IEquatable<GridNode>
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600755D RID: 30045 RVA: 0x002BFDB8 File Offset: 0x002BDFB8
		public bool Diagonal
		{
			get
			{
				return this.Parent != null && this.DiagonalTo(this.Parent);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600755E RID: 30046 RVA: 0x002BFDD0 File Offset: 0x002BDFD0
		public string Direction
		{
			get
			{
				if (this.Parent == null)
				{
					return ".";
				}
				int x = this.X;
				int y = this.Y;
				int x2 = this.Parent.X;
				int y2 = this.Parent.Y;
				if (x == x2)
				{
					if (y == y2)
					{
						return ".";
					}
					if (y < y2)
					{
						return "N";
					}
					return "S";
				}
				else if (x < x2)
				{
					if (y == y2)
					{
						return "W";
					}
					if (y < y2)
					{
						return "NW";
					}
					return "SW";
				}
				else
				{
					if (y == y2)
					{
						return "E";
					}
					if (y < y2)
					{
						return "NE";
					}
					return "SE";
				}
			}
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x002BFE66 File Offset: 0x002BE066
		public GridNode()
		{
		}

		// Token: 0x06007560 RID: 30048 RVA: 0x002BFE6E File Offset: 0x002BE06E
		public GridNode(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x06007561 RID: 30049 RVA: 0x002BFE84 File Offset: 0x002BE084
		public void Set(Cell Cell, FindPath Pathfinder, GridNode Parent = null)
		{
			this.Weight = Cell.NavigationWeight(Pathfinder.Looker, Pathfinder.Nav);
			this.Distance = Cell.ManhattanDistanceTo(Pathfinder.Destination);
			if (Parent == null)
			{
				this.Cost = 0;
			}
			else if (this.Distance == 0)
			{
				this.Cost = 1;
			}
			else
			{
				this.Cost = this.Weight + 1;
			}
			this.Closed = false;
			this.SetParent(Parent);
		}

		// Token: 0x06007562 RID: 30050 RVA: 0x002BFEF4 File Offset: 0x002BE0F4
		public bool DiagonalTo(GridNode Node)
		{
			return this.X != Node.X && this.Y != Node.Y;
		}

		// Token: 0x06007563 RID: 30051 RVA: 0x002BFF18 File Offset: 0x002BE118
		public void SetParent(GridNode Node)
		{
			this.Parent = Node;
			if (Node != null)
			{
				this.Total = Node.Total + this.Cost;
				this.Length = Node.Length + 1;
				return;
			}
			this.Total = this.Cost;
			this.Length = 1;
		}

		// Token: 0x06007564 RID: 30052 RVA: 0x002BFF64 File Offset: 0x002BE164
		public int ManhattanDistanceTo(GridNode L)
		{
			return Math.Abs(L.X - this.X) + Math.Abs(L.Y - this.Y);
		}

		// Token: 0x06007565 RID: 30053 RVA: 0x002BFF8B File Offset: 0x002BE18B
		public int CompareTo(GridNode Other)
		{
			return this.Total + this.Distance - (Other.Total + Other.Distance);
		}

		// Token: 0x06007566 RID: 30054 RVA: 0x002BFFA8 File Offset: 0x002BE1A8
		public bool Equals(GridNode other)
		{
			return other != null && this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x002BFFCD File Offset: 0x002BE1CD
		public override int GetHashCode()
		{
			return (this.Y << 15) ^ this.X;
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x002BFFE0 File Offset: 0x002BE1E0
		// Note: this type is marked as 'beforefieldinit'.
		static GridNode()
		{
			int[][] array = new int[8][];
			array[0] = new int[] { 0, -1 };
			array[1] = new int[] { 1, -1 };
			int num = 2;
			int[] array2 = new int[2];
			array2[0] = 1;
			array[num] = array2;
			array[3] = new int[] { 1, 1 };
			array[4] = new int[] { 0, 1 };
			array[5] = new int[] { -1, 1 };
			int num2 = 6;
			int[] array3 = new int[2];
			array3[0] = -1;
			array[num2] = array3;
			array[7] = new int[] { -1, -1 };
			GridNode.Offsets = array;
			int[][] array4 = new int[4][];
			array4[0] = new int[] { 0, -1 };
			int num3 = 1;
			int[] array5 = new int[2];
			array5[0] = 1;
			array4[num3] = array5;
			array4[2] = new int[] { 0, 1 };
			int num4 = 3;
			int[] array6 = new int[2];
			array6[0] = -1;
			array4[num4] = array6;
			GridNode.OffsetsCardinal = array4;
		}

		// Token: 0x0400359E RID: 13726
		public static int[][] Offsets;

		// Token: 0x0400359F RID: 13727
		public static int[][] OffsetsCardinal;

		// Token: 0x040035A0 RID: 13728
		public GridNode Parent;

		// Token: 0x040035A1 RID: 13729
		public int X;

		// Token: 0x040035A2 RID: 13730
		public int Y;

		// Token: 0x040035A3 RID: 13731
		public int Cost;

		// Token: 0x040035A4 RID: 13732
		public int Weight;

		// Token: 0x040035A5 RID: 13733
		public int Total;

		// Token: 0x040035A6 RID: 13734
		public int Distance;

		// Token: 0x040035A7 RID: 13735
		public int Length;

		// Token: 0x040035A8 RID: 13736
		public int Index;

		// Token: 0x040035A9 RID: 13737
		public int Version;

		// Token: 0x040035AA RID: 13738
		public bool Closed;
	}
}
