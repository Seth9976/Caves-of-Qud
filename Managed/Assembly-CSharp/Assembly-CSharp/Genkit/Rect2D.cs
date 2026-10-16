using System;
using System.Collections.Generic;
using XRL;
using XRL.Rules;
using XRL.World;

namespace Genkit
{
	// Token: 0x02000390 RID: 912
	[Serializable]
	public struct Rect2D : IComposite
	{
		// Token: 0x0600224B RID: 8779 RVA: 0x000AC153 File Offset: 0x000AA353
		public static implicit operator Box(Rect2D R)
		{
			return new Box(R.x1, R.y1, R.x2, R.y2);
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x000AC172 File Offset: 0x000AA372
		public Point2D randomPoint()
		{
			return new Point2D(Stat.Random(this.x1, this.x2), Stat.Random(this.y1, this.y2));
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x000AC19C File Offset: 0x000AA39C
		public List<Point2D> getPoints()
		{
			List<Point2D> list = new List<Point2D>();
			for (int i = this.x1; i <= this.x2; i++)
			{
				for (int j = this.y1; j <= this.y2; j++)
				{
					list.Add(new Point2D(i, j));
				}
			}
			return list;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x000AC1E9 File Offset: 0x000AA3E9
		public IEnumerable<Location2D> allPoints()
		{
			int num;
			for (int x = this.x1; x <= this.x2; x = num + 1)
			{
				for (int y = this.y1; y <= this.y2; y = num + 1)
				{
					yield return Location2D.Get(x, y);
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000AC1FE File Offset: 0x000AA3FE
		public bool overlaps(Rect2D rect)
		{
			return this.x1 <= rect.x2 && rect.x1 <= this.x2 && this.y1 <= rect.y2 && rect.y1 <= this.y2;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000AC23D File Offset: 0x000AA43D
		public Rect2D enforceVailidity(Rect2D source)
		{
			return new Rect2D(source.x1, source.y1, Math.Max(source.x1, source.x2), Math.Max(source.y1, source.y2));
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x000AC272 File Offset: 0x000AA472
		public Point2D Center
		{
			get
			{
				return new Point2D((this.x1 + this.x2) / 2, (this.y1 + this.y2) / 2);
			}
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000AC298 File Offset: 0x000AA498
		public Rect2D(List<Location2D> pointsToWrap)
		{
			this.x1 = int.MaxValue;
			this.y1 = int.MaxValue;
			this.x2 = int.MinValue;
			this.y2 = int.MinValue;
			this.Door = Point2D.invalid;
			this.Pinch = -1;
			this.DoorDirection = null;
			foreach (Location2D location2D in pointsToWrap)
			{
				if (this.x1 > location2D.X)
				{
					this.x1 = location2D.X;
				}
				if (this.y1 > location2D.Y)
				{
					this.y1 = location2D.Y;
				}
				if (this.x2 < location2D.X)
				{
					this.x2 = location2D.X;
				}
				if (this.y2 < location2D.Y)
				{
					this.y2 = location2D.Y;
				}
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x000AC38C File Offset: 0x000AA58C
		public Rect2D(int x1, int y1, int x2, int y2)
		{
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
			this.Door = Point2D.invalid;
			this.Pinch = -1;
			this.DoorDirection = null;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000AC3C4 File Offset: 0x000AA5C4
		public Rect2D(int x1, int y1, int x2, int y2, Point2D Door)
		{
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
			this.Door = Door;
			this.Pinch = -1;
			this.DoorDirection = null;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x000AC3F9 File Offset: 0x000AA5F9
		public int Area
		{
			get
			{
				return Math.Max(0, this.Width * this.Height);
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000AC410 File Offset: 0x000AA610
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.x1.ToString(),
				",",
				this.y1.ToString(),
				"->",
				this.x2.ToString(),
				",",
				this.y2.ToString(),
				"]"
			});
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000AC48C File Offset: 0x000AA68C
		public bool IsPointAdjacentToAvoid(Point2D P, List<Point2D> Avoid)
		{
			for (int i = 0; i < Avoid.Count; i++)
			{
				if (this.IsPointAdjacentToPoint(P, Avoid[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000AC4C0 File Offset: 0x000AA6C0
		public bool IsPointAdjacentToAvoid(Location2D P, List<Location2D> Avoid)
		{
			if (P == null)
			{
				return false;
			}
			for (int i = 0; i < Avoid.Count; i++)
			{
				if (this.IsPointAdjacentToPoint(P, Avoid[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000AC4FC File Offset: 0x000AA6FC
		public bool IsPointAdjacentToPoint(Point2D P, Point2D P2)
		{
			return (P.x == P2.x && P.y >= P2.y - 1 && P.y <= P2.y + 1) || (P.y == P2.y && P.x >= P2.x - 1 && P.x <= P2.x + 1);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000AC56C File Offset: 0x000AA76C
		public bool IsPointAdjacentToPoint(Location2D P, Location2D P2)
		{
			return !(P == null) && !(P2 == null) && ((P.X == P2.X && P.Y >= P2.Y - 1 && P.Y <= P2.Y + 1) || (P.Y == P2.Y && P.X >= P2.X - 1 && P.X <= P2.X + 1));
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000AC5F0 File Offset: 0x000AA7F0
		public bool IsPointAdjacentToDoor(Point2D P)
		{
			return !(this.Door == Point2D.invalid) && ((P.x == this.Door.x && P.y >= this.Door.y - 2 && P.y <= this.Door.y + 2) || (P.y == this.Door.y && P.x >= this.Door.x - 2 && P.x <= this.Door.x + 2));
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000AC690 File Offset: 0x000AA890
		public bool IsPointAdjacentToDoor(Location2D P)
		{
			return !(this.Door == Point2D.invalid) && !(P == null) && ((P.X == this.Door.x && P.Y >= this.Door.y - 2 && P.Y <= this.Door.y + 2) || (P.Y == this.Door.y && P.X >= this.Door.x - 2 && P.X <= this.Door.x + 2));
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000AC73B File Offset: 0x000AA93B
		public bool Contains(Rect2D rect)
		{
			return rect.x1 >= this.x1 && rect.y1 >= this.y1 && rect.x2 <= this.x2 && rect.y2 <= this.y2;
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000AC77A File Offset: 0x000AA97A
		public bool Contains(Point2D point)
		{
			return this.Contains(point.x, point.y);
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000AC78E File Offset: 0x000AA98E
		public bool Contains(int x, int y)
		{
			return x >= this.x1 && x <= this.x2 && y >= this.y1 && y <= this.y2;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x000AC7BC File Offset: 0x000AA9BC
		public string GetCellSide(Point2D C)
		{
			if (C.x == this.x1)
			{
				return "W";
			}
			if (C.x == this.x2)
			{
				return "E";
			}
			if (C.y == this.y1)
			{
				return "N";
			}
			if (C.y == this.y2)
			{
				return "S";
			}
			return ".";
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000AC81E File Offset: 0x000AAA1E
		public Point2D UpperLeft
		{
			get
			{
				return new Point2D(this.x1, this.y1);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000AC831 File Offset: 0x000AAA31
		public int Width
		{
			get
			{
				return this.x2 - this.x1 + 1;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x000AC842 File Offset: 0x000AAA42
		public int Height
		{
			get
			{
				return this.y2 - this.y1 + 1;
			}
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x000AC853 File Offset: 0x000AAA53
		public bool isInvalid()
		{
			return this == Rect2D.invalid;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x000AC865 File Offset: 0x000AAA65
		private static int Rnd(int Low, int High)
		{
			if (Low >= High)
			{
				return Low;
			}
			return Calc.Random(Low, High);
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x000AC874 File Offset: 0x000AAA74
		public Point2D GetRandomDoorCell(string Wall = "?", int MinDistance = 2)
		{
			int num = 0;
			if (Wall == "?")
			{
				num = Calc.Random(0, 3);
			}
			if (Wall == "N")
			{
				num = 0;
			}
			if (Wall == "S")
			{
				num = 1;
			}
			if (Wall == "E")
			{
				num = 2;
			}
			if (Wall == "W")
			{
				num = 3;
			}
			this.Door = Point2D.invalid;
			if (num == 0)
			{
				this.Door = new Point2D(Rect2D.Rnd(this.x1 + MinDistance, this.x2 - MinDistance), this.y1);
			}
			if (num == 1)
			{
				this.Door = new Point2D(Rect2D.Rnd(this.x1 + MinDistance, this.x2 - MinDistance), this.y2);
			}
			if (num == 2)
			{
				this.Door = new Point2D(this.x2, Rect2D.Rnd(this.y1 + MinDistance, this.y2 - MinDistance));
			}
			if (num == 3)
			{
				this.Door = new Point2D(this.x1, Rect2D.Rnd(this.y1 + MinDistance, this.y2 - MinDistance));
			}
			this.DoorDirection = this.GetCellSide(this.Door);
			return this.Door;
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x000AC9A0 File Offset: 0x000AABA0
		public bool PointIn(Location2D P)
		{
			return !(P == null) && (P.X >= this.x1 && P.X <= this.x2 && P.Y >= this.y1) && P.Y <= this.y2;
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000AC9F5 File Offset: 0x000AABF5
		public bool PointIn(Point2D P)
		{
			return P.x >= this.x1 && P.x <= this.x2 && P.y >= this.y1 && P.y <= this.y2;
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000ACA34 File Offset: 0x000AAC34
		public Rect2D ReduceBy(int x, int y)
		{
			return new Rect2D(this.x1 + x, this.y1 + y, this.x2 - x, this.y2 - y, this.Door);
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x000ACA64 File Offset: 0x000AAC64
		public Rect2D Translate(Point2D P)
		{
			return new Rect2D(this.x1 + P.x, this.y1 + P.y, this.x2 + P.x, this.y2 + P.y, this.Door);
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000ACAB0 File Offset: 0x000AACB0
		public Rect2D Clamp(int minx, int miny, int maxx, int maxy)
		{
			return new Rect2D(Math.Max(this.x1, minx), Math.Max(this.y1, miny), Math.Min(this.x2, maxx), Math.Min(this.y2, maxy));
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000ACAE8 File Offset: 0x000AACE8
		public IEnumerable<Location2D> locations
		{
			get
			{
				int num;
				for (int x = this.x1; x <= this.x2; x = num + 1)
				{
					for (int y = this.y1; y <= this.y2; y = num + 1)
					{
						yield return Location2D.Get(x, y);
						num = y;
					}
					num = x;
				}
				yield break;
			}
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x000ACB00 File Offset: 0x000AAD00
		public void ForEachLocation(Action<Location2D> action)
		{
			for (int i = this.x1; i <= this.x2; i++)
			{
				for (int j = this.y1; j <= this.y2; j++)
				{
					action(Location2D.Get(i, j));
				}
			}
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000ACB48 File Offset: 0x000AAD48
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			Rect2D rect2D = (Rect2D)obj;
			return rect2D.x1 == this.x1 && rect2D.y1 == this.y1 && rect2D.x2 == this.x2 && rect2D.y2 == this.y2;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000ACBB8 File Offset: 0x000AADB8
		public static bool operator !=(Rect2D x, Rect2D y)
		{
			return !x.Equals(y);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x000ACBD0 File Offset: 0x000AADD0
		public static bool operator ==(Rect2D x, Rect2D y)
		{
			return x.Equals(y);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000ACBE8 File Offset: 0x000AADE8
		public override int GetHashCode()
		{
			return ((this.DoorDirection == null) ? 0 : this.DoorDirection.GetHashCode()) ^ this.Door.GetHashCode() ^ this.x1 ^ this.x2 ^ this.y1 ^ this.y2 ^ this.Pinch;
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x000ACC40 File Offset: 0x000AAE40
		public Rect2D ExpandBy(int n, bool clampToScreenSize = false)
		{
			if (clampToScreenSize)
			{
				return new Rect2D(Math.Max(0, this.x1 - n), Math.Max(0, this.y1 - n), Math.Min(79, this.x2 + n), Math.Min(24, this.y2 + n), this.Door);
			}
			return new Rect2D(this.x1 - n, this.y1 - n, this.x2 + n, this.y2 + n, this.Door);
		}

		// Token: 0x04001133 RID: 4403
		public static Rect2D zero = new Rect2D(0, 0, 0, 0);

		// Token: 0x04001134 RID: 4404
		public static Rect2D invalid = new Rect2D(-1, 1, 1, -1);

		// Token: 0x04001135 RID: 4405
		public string DoorDirection;

		// Token: 0x04001136 RID: 4406
		public Point2D Door;

		// Token: 0x04001137 RID: 4407
		public int x1;

		// Token: 0x04001138 RID: 4408
		public int y1;

		// Token: 0x04001139 RID: 4409
		public int x2;

		// Token: 0x0400113A RID: 4410
		public int y2;

		// Token: 0x0400113B RID: 4411
		public int Pinch;
	}
}
