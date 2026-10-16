using System;
using System.Collections.Generic;
using XRL.World;

namespace Genkit
{
	// Token: 0x0200038E RID: 910
	[Serializable]
	public struct Point2D : IComposite
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000ABB50 File Offset: 0x000A9D50
		public Location2D location
		{
			get
			{
				return Location2D.Get(this.x, this.y);
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000ABB63 File Offset: 0x000A9D63
		public override string ToString()
		{
			return this.x.ToString() + "," + this.y.ToString();
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000ABB88 File Offset: 0x000A9D88
		public string RegionDirection(int RegionWidth, int RegionHeight)
		{
			int num = Calc.Clamp((int)((float)this.x / (float)RegionWidth * 3f), 0, 2);
			int num2 = Calc.Clamp((int)((float)this.y / (float)RegionHeight * 3f), 0, 2);
			return Point2D.RegionDirections[num2, num];
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000ABBD3 File Offset: 0x000A9DD3
		public Point2D(int _x, int _y)
		{
			this.x = _x;
			this.y = _y;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000ABBE3 File Offset: 0x000A9DE3
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000ABBE6 File Offset: 0x000A9DE6
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.x);
			Writer.WriteOptimized(this.y);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000ABC00 File Offset: 0x000A9E00
		public void Read(SerializationReader Reader)
		{
			this.x = Reader.ReadOptimizedInt32();
			this.y = Reader.ReadOptimizedInt32();
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x000ABC1C File Offset: 0x000A9E1C
		public readonly Point2D FromDirection(string D)
		{
			if (D == "N")
			{
				return new Point2D(this.x, this.y - 1);
			}
			if (D == "S")
			{
				return new Point2D(this.x, this.y + 1);
			}
			if (D == "E")
			{
				return new Point2D(this.x + 1, this.y);
			}
			if (D == "W")
			{
				return new Point2D(this.x - 1, this.y);
			}
			if (D == "NW")
			{
				return new Point2D(this.x - 1, this.y - 1);
			}
			if (D == "NE")
			{
				return new Point2D(this.x + 1, this.y - 1);
			}
			if (D == "SW")
			{
				return new Point2D(this.x - 1, this.y + 1);
			}
			if (D == "SE")
			{
				return new Point2D(this.x + 1, this.y + 1);
			}
			return this;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000ABD3F File Offset: 0x000A9F3F
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000ABD4E File Offset: 0x000A9F4E
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (Point2D)obj == this;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000ABD83 File Offset: 0x000A9F83
		public static bool operator ==(Point2D c1, Point2D c2)
		{
			return c1.x == c2.x && c1.y == c2.y;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000ABDA3 File Offset: 0x000A9FA3
		public static bool operator !=(Point2D c1, Point2D c2)
		{
			return c1.x != c2.x || c1.y != c2.y;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000ABDC6 File Offset: 0x000A9FC6
		public static Point2D operator -(Point2D a, Point2D b)
		{
			return new Point2D(a.x - b.x, a.y - b.y);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000ABDE7 File Offset: 0x000A9FE7
		public static Point2D operator +(Point2D a, Point2D b)
		{
			return new Point2D(a.x + b.x, a.y + b.y);
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000ABE08 File Offset: 0x000AA008
		public int Distance(Point2D c2)
		{
			return (int)Math.Sqrt((double)this.SquareDistance(c2));
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000ABE18 File Offset: 0x000AA018
		public int SquareDistance(Point2D c2)
		{
			return (c2.x - this.x) * (c2.x - this.x) + (c2.y - this.y) * (c2.y - this.y);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000ABE51 File Offset: 0x000AA051
		public int ManhattanDistance(Point2D c2)
		{
			return Math.Max(Math.Abs(this.x - c2.x), Math.Abs(this.y - c2.y));
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000ABE7C File Offset: 0x000AA07C
		public int ManhattanDistance(int X, int Y)
		{
			return Math.Max(Math.Abs(this.x - X), Math.Abs(this.y - Y));
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000ABEA0 File Offset: 0x000AA0A0
		public List<Point2D> GetRadialPoints(int Radius, List<Point2D> listToUse = null)
		{
			List<Point2D> list;
			if (listToUse != null)
			{
				list = listToUse;
				list.Clear();
			}
			else
			{
				list = new List<Point2D>();
			}
			for (int i = this.x - Radius; i <= this.x + Radius; i++)
			{
				for (int j = this.y - Radius; j <= this.y + Radius; j++)
				{
					if ((int)Math.Sqrt((double)this.SquareDistance(new Point2D(i, j))) == Radius)
					{
						list.Add(new Point2D(i, j));
					}
				}
			}
			return list;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000ABF1C File Offset: 0x000AA11C
		// Note: this type is marked as 'beforefieldinit'.
		static Point2D()
		{
			string[,] array = new string[3, 3];
			array[0, 0] = "NW";
			array[0, 1] = "N";
			array[0, 2] = "NE";
			array[1, 0] = "W";
			array[1, 1] = ".";
			array[1, 2] = "E";
			array[2, 0] = "SW";
			array[2, 1] = "S";
			array[2, 2] = "SE";
			Point2D.RegionDirections = array;
		}

		// Token: 0x04001128 RID: 4392
		public static readonly Point2D zero = new Point2D(0, 0);

		// Token: 0x04001129 RID: 4393
		public static readonly Point2D invalid = new Point2D(int.MinValue, int.MaxValue);

		// Token: 0x0400112A RID: 4394
		public static string[] Directions = new string[] { "N", "S", "E", "W", "NW", "NE", "SW", "SE" };

		// Token: 0x0400112B RID: 4395
		public static string[,] RegionDirections;

		// Token: 0x0400112C RID: 4396
		public int x;

		// Token: 0x0400112D RID: 4397
		public int y;
	}
}
