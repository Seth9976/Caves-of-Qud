using System;
using System.Collections.Generic;
using Cysharp.Text;
using UnityEngine;
using XRL.Rules;
using XRL.World;

namespace Genkit
{
	// Token: 0x0200037C RID: 892
	[Serializable]
	public sealed class Location2D : ILocationArea, IEquatable<Location2D>, IEquatable<Cell>
	{
		// Token: 0x0600217D RID: 8573 RVA: 0x000A6EA8 File Offset: 0x000A50A8
		static Location2D()
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
			Location2D.RegionDirections = array;
			Location2D.LineReturnCache = new List<Location2D>(16);
			Location2D.Grid = new Location2D[Location2D.MaxX, Location2D.MaxY];
			for (int i = 0; i < Location2D.MaxX; i++)
			{
				for (int j = 0; j < Location2D.MaxY; j++)
				{
					Location2D.Grid[i, j] = new Location2D(i, j);
				}
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000A7008 File Offset: 0x000A5208
		public Location2D(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
			if (X < 80 && Y < 25)
			{
				if (X < 40)
				{
					this.Ordinal |= 8;
				}
				else
				{
					this.Ordinal |= 2;
				}
				if (Y < 13)
				{
					this.Ordinal |= 1;
					return;
				}
				this.Ordinal |= 4;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000A7078 File Offset: 0x000A5278
		public Point2D Point
		{
			get
			{
				return new Point2D(this.X, this.Y);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000A708B File Offset: 0x000A528B
		public Vector2i Vector2i
		{
			get
			{
				return new Vector2i(this.X, this.Y);
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000A709E File Offset: 0x000A529E
		public static implicit operator Vector2(Location2D L)
		{
			return new Vector2((float)L.X, (float)L.Y);
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000A70B3 File Offset: 0x000A52B3
		public IEnumerable<Location2D> cardinalNeighbors
		{
			get
			{
				Location2D location2D = Location2D.Get(this.X - 1, 0);
				Location2D n2 = Location2D.Get(this.X + 1, 0);
				Location2D n3 = Location2D.Get(this.X, this.Y - 1);
				Location2D n4 = Location2D.Get(this.X, this.Y + 1);
				if (location2D == null)
				{
					yield return location2D;
				}
				if (n2 == null)
				{
					yield return n2;
				}
				if (n3 == null)
				{
					yield return n3;
				}
				if (n4 == null)
				{
					yield return n4;
				}
				yield break;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000A70C3 File Offset: 0x000A52C3
		public Location2D Clamp(int minx = 0, int maxx = 79, int miny = 0, int maxy = 24)
		{
			return Location2D.Get(Math.Clamp(this.X, minx, maxx), Math.Clamp(this.Y, miny, maxy));
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000A70E5 File Offset: 0x000A52E5
		public Location2D Wiggle(int XDelta, int YDelta)
		{
			return Location2D.Get(this.X + Stat.Random(-XDelta, XDelta), this.Y + Stat.Random(-YDelta, YDelta));
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000A710A File Offset: 0x000A530A
		public static Location2D Get(int X, int Y)
		{
			if (X < 0 || X >= Location2D.MaxX || Y < 0 || Y >= Location2D.MaxY)
			{
				return null;
			}
			return Location2D.Grid[X, Y];
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000A7134 File Offset: 0x000A5334
		public static List<Location2D> GetWithin(int X1, int Y1, int X2, int Y2)
		{
			List<Location2D> list = new List<Location2D>((X2 - X1 + 1) * (Y2 - Y1 + 1));
			for (int i = Y1; i <= Y2; i++)
			{
				for (int j = X1; j <= X2; j++)
				{
					list.Add(Location2D.Get(j, i));
				}
			}
			return list;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000A7178 File Offset: 0x000A5378
		public override string ToString()
		{
			return this.X.ToString() + "," + this.Y.ToString();
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000A71AC File Offset: 0x000A53AC
		public string RegionDirection(int RegionWidth, int RegionHeight)
		{
			int num = Calc.Clamp((int)((float)this.X / (float)RegionWidth * 3f), 0, 2);
			int num2 = Calc.Clamp((int)((float)this.Y / (float)RegionHeight * 3f), 0, 2);
			return Location2D.RegionDirections[num2, num];
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000A71F7 File Offset: 0x000A53F7
		public bool SameAs(Location2D o)
		{
			return o.X == this.X && o.Y == this.Y;
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000A7217 File Offset: 0x000A5417
		public bool Inside(int X1, int Y1, int X2, int Y2)
		{
			return this.X >= X1 && this.X <= X2 && this.Y >= Y1 && this.Y <= Y2;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x000A7248 File Offset: 0x000A5448
		public string DirectionTo(Location2D destination)
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				if (destination.Y > this.Y)
				{
					utf16ValueStringBuilder.Append("S");
				}
				if (destination.Y < this.Y)
				{
					utf16ValueStringBuilder.Append("N");
				}
				if (destination.X < this.X)
				{
					utf16ValueStringBuilder.Append("W");
				}
				if (destination.X > this.X)
				{
					utf16ValueStringBuilder.Append("E");
				}
				if (utf16ValueStringBuilder.Length == 0)
				{
					text = ".";
				}
				else
				{
					text = utf16ValueStringBuilder.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x000A7300 File Offset: 0x000A5500
		public string DirectionToCenter()
		{
			string text = "";
			if (this.Y < 12)
			{
				text += "S";
			}
			else
			{
				text += "N";
			}
			if (this.X < 40)
			{
				text += "E";
			}
			else
			{
				text += "W";
			}
			return text;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x000A735C File Offset: 0x000A555C
		public string CardinalDirectionToCenter()
		{
			return this.DirectionToCenter()[Stat.Random(0, this.DirectionToCenter().Length - 1)].ToString();
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000A7390 File Offset: 0x000A5590
		public bool OppositeDirections(Location2D a, Location2D b)
		{
			int num = a.X - this.X;
			int num2 = b.X - this.X;
			if (num > 0 && num2 < 0)
			{
				return true;
			}
			if (num < 0 && num2 > 0)
			{
				return true;
			}
			int num3 = a.Y - this.Y;
			int num4 = b.Y - this.Y;
			return (num3 > 0 && num4 < 0) || (num3 < 0 && num4 > 0);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000A7400 File Offset: 0x000A5600
		public bool Backtracking(Location2D a, Location2D b)
		{
			int num = a.X - this.X;
			int num2 = b.X - a.X;
			if (num > 0 && num2 < 0)
			{
				return true;
			}
			if (num < 0 && num2 > 0)
			{
				return true;
			}
			int num3 = a.Y - this.Y;
			int num4 = b.Y - a.Y;
			return (num3 > 0 && num4 < 0) || (num3 < 0 && num4 > 0);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000A746E File Offset: 0x000A566E
		public override int GetHashCode()
		{
			return (this.X << 7) + this.Y;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x000A7480 File Offset: 0x000A5680
		public Location2D FromDirection(string D)
		{
			if (D == "N")
			{
				return Location2D.Get(this.X, this.Y - 1);
			}
			if (D == "S")
			{
				return Location2D.Get(this.X, this.Y + 1);
			}
			if (D == "E")
			{
				return Location2D.Get(this.X + 1, this.Y);
			}
			if (D == "W")
			{
				return Location2D.Get(this.X - 1, this.Y);
			}
			if (D == "NW")
			{
				return Location2D.Get(this.X - 1, this.Y - 1);
			}
			if (D == "NE")
			{
				return Location2D.Get(this.X + 1, this.Y - 1);
			}
			if (D == "SW")
			{
				return Location2D.Get(this.X - 1, this.Y + 1);
			}
			if (D == "SE")
			{
				return Location2D.Get(this.X + 1, this.Y + 1);
			}
			return this;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x000A759E File Offset: 0x000A579E
		public bool Equals(Location2D L)
		{
			return L != null && this.X == L.X && this.Y == L.Y;
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000A75C3 File Offset: 0x000A57C3
		public bool Equals(Cell C)
		{
			return C != null && this.X == C.X && this.Y == C.Y;
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x000A75E8 File Offset: 0x000A57E8
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && (Location2D)obj == this;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x000A760E File Offset: 0x000A580E
		public static Location2D operator -(Location2D a, Location2D b)
		{
			return Location2D.Get(a.X - b.X, a.Y - b.Y);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x000A762F File Offset: 0x000A582F
		public static Location2D operator +(Location2D a, Location2D b)
		{
			return Location2D.Get(a.X + b.X, a.Y + b.Y);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000A7650 File Offset: 0x000A5850
		public static bool operator ==(Location2D c1, Location2D c2)
		{
			if (c1 == null)
			{
				return c2 == null;
			}
			return c2 != null && c1.X == c2.X && c1.Y == c2.Y;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000A767D File Offset: 0x000A587D
		public static bool operator !=(Location2D c1, Location2D c2)
		{
			return !(c1 == c2);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x000A7689 File Offset: 0x000A5889
		public int Distance(Location2D c2)
		{
			if (c2 == null)
			{
				return int.MaxValue;
			}
			if (c2.X == this.X && c2.Y == this.Y)
			{
				return 0;
			}
			return (int)Math.Sqrt((double)this.SquareDistance(c2));
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000A76C6 File Offset: 0x000A58C6
		public int Distance(int X, int Y)
		{
			if (X == this.X && Y == this.Y)
			{
				return 0;
			}
			return (int)Math.Sqrt((double)this.SquareDistance(X, Y));
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x000A76EC File Offset: 0x000A58EC
		public int SquareDistance(Location2D c2)
		{
			if (c2 == null)
			{
				return int.MaxValue;
			}
			if (c2.X == this.X && c2.Y == this.Y)
			{
				return 0;
			}
			return (c2.X - this.X) * (c2.X - this.X) + (c2.Y - this.Y) * (c2.Y - this.Y);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x000A775D File Offset: 0x000A595D
		public int SquareDistance(int X, int Y)
		{
			return (X - this.X) * (X - this.X) + (Y - this.Y) * (Y - this.Y);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x000A7784 File Offset: 0x000A5984
		public int ManhattanDistance(Location2D c2)
		{
			if (c2 == null)
			{
				return int.MaxValue;
			}
			if (c2.X == this.X && c2.Y == this.Y)
			{
				return 0;
			}
			return Math.Max(Math.Abs(this.X - c2.X), Math.Abs(this.Y - c2.Y));
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x000A77E7 File Offset: 0x000A59E7
		public int ManhattanDistance(int X, int Y)
		{
			return Math.Max(Math.Abs(this.X - X), Math.Abs(this.Y - Y));
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x000A7808 File Offset: 0x000A5A08
		public List<Location2D> GetRadialPoints(int Radius)
		{
			List<Location2D> list = new List<Location2D>();
			for (int i = this.X - Radius; i <= this.X + Radius; i++)
			{
				for (int j = this.Y - Radius; j <= this.Y + Radius; j++)
				{
					Location2D location2D = Location2D.Get(i, j);
					if (location2D != null && (int)Math.Sqrt((double)this.SquareDistance(location2D)) == Radius)
					{
						list.Add(location2D);
					}
				}
			}
			return list;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x000A787C File Offset: 0x000A5A7C
		public static List<Location2D> GetLine(int X1, int Y1, int X2, int Y2, bool ReadOnly = true)
		{
			List<Location2D> list;
			if (ReadOnly)
			{
				list = Location2D.LineReturnCache;
				list.Clear();
			}
			else
			{
				list = new List<Location2D>();
			}
			Location2D.GetLine(X1, Y1, X2, Y2, list);
			return list;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x000A78AC File Offset: 0x000A5AAC
		public static void GetLine(int X1, int Y1, int X2, int Y2, List<Location2D> Return)
		{
			int num = Math.Abs(X1 - X2);
			int num2 = -Math.Abs(Y1 - Y2);
			int num3 = ((X1 < X2) ? 1 : (-1));
			int num4 = ((Y1 < Y2) ? 1 : (-1));
			int num5 = num + num2;
			int num6 = num5 * 2;
			Return.EnsureCapacity<Location2D>(Return.Count + Math.Max(num, -num2) + 1);
			for (;;)
			{
				Return.Add(Location2D.Grid[X1, Y1]);
				if (num6 >= num2)
				{
					if (X1 == X2)
					{
						break;
					}
					num5 += num2;
					X1 += num3;
				}
				if (num6 <= num)
				{
					if (Y1 == Y2)
					{
						break;
					}
					num5 += num;
					Y1 += num4;
				}
				num6 = num5 * 2;
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x000A7948 File Offset: 0x000A5B48
		public static List<Location2D> GetCardinalLine(int X1, int Y1, int X2, int Y2, bool ReadOnly = true)
		{
			List<Location2D> list;
			if (ReadOnly)
			{
				list = Location2D.LineReturnCache;
				list.Clear();
			}
			else
			{
				list = new List<Location2D>();
			}
			Location2D.GetCardinalLine(X1, Y1, X2, Y2, list);
			return list;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x000A7978 File Offset: 0x000A5B78
		public static void GetCardinalLine(int X1, int Y1, int X2, int Y2, List<Location2D> Return)
		{
			int num = Math.Abs(X1 - X2);
			int num2 = -Math.Abs(Y1 - Y2);
			int num3 = ((X1 < X2) ? 1 : (-1));
			int num4 = ((Y1 < Y2) ? 1 : (-1));
			int num5 = num + num2;
			Return.EnsureCapacity<Location2D>(Return.Count + num - num2 + 1);
			Return.Add(Location2D.Grid[X1, Y1]);
			while (X1 != X2 || Y1 != Y2)
			{
				if (2 * num5 - num2 > num - 2 * num5)
				{
					num5 += num2;
					X1 += num3;
				}
				else
				{
					num5 += num;
					Y1 += num4;
				}
				Return.Add(Location2D.Grid[X1, Y1]);
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x000A7A19 File Offset: 0x000A5C19
		public IEnumerable<Location2D> YieldAdjacent(int Radius)
		{
			Radius = Radius * 2 + 1;
			int x = this.X;
			int y = this.Y;
			int x2 = 1;
			int y2 = 0;
			int i = 1;
			int j = 0;
			int p = 1;
			int c = Radius * Radius - 1;
			while (j < c)
			{
				x += x2;
				y += y2;
				int num2;
				if (p >= i)
				{
					p = 0;
					int num = x2;
					x2 = -y2;
					y2 = num;
					if (y2 == 0)
					{
						num2 = i;
						i = num2 + 1;
					}
				}
				Location2D location2D = Location2D.Get(x, y);
				if (location2D != null)
				{
					yield return location2D;
				}
				num2 = j;
				j = num2 + 1;
				num2 = p;
				p = num2 + 1;
			}
			yield break;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x000A7A30 File Offset: 0x000A5C30
		public float AngleTo(Location2D Location)
		{
			return Vector2.Angle(this, Location);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x000A7A44 File Offset: 0x000A5C44
		public float AngleTo(Location2D fulcrum, Location2D to)
		{
			Vector2 vector = new Vector2((float)(this.X - fulcrum.X), (float)(this.Y - fulcrum.Y));
			Vector2 vector2 = new Vector2((float)(to.X - fulcrum.X), (float)(to.Y - fulcrum.Y));
			return Vector2.Angle(vector, vector2);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x000A7A9B File Offset: 0x000A5C9B
		public IEnumerable<Location2D> YieldPerimeter(int Size = 1)
		{
			int x = this.X - Size;
			int x2 = this.X + Size;
			int y = this.Y - Size;
			int y2 = this.Y + Size;
			while (x <= x2)
			{
				if (x >= 0 && x < Location2D.MaxX)
				{
					if (y >= 0)
					{
						yield return Location2D.Grid[x, y];
					}
					if (y2 < Location2D.MaxY)
					{
						yield return Location2D.Grid[x, y2];
					}
				}
				int num = x;
				x = num + 1;
			}
			y2 = this.X - Size;
			y = this.X + Size;
			x2 = this.Y - Size + 1;
			x = this.Y + Size - 1;
			while (x2 <= x)
			{
				if (x2 >= 0 && x2 < Location2D.MaxY)
				{
					if (y2 >= 0)
					{
						yield return Location2D.Grid[y2, x2];
					}
					if (y < Location2D.MaxX)
					{
						yield return Location2D.Grid[y, x2];
					}
				}
				int num = x2;
				x2 = num + 1;
			}
			yield break;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x000A7AB2 File Offset: 0x000A5CB2
		public IEnumerable<Location2D> EnumerateLocations()
		{
			yield return this;
			yield break;
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x000A7AC2 File Offset: 0x000A5CC2
		public IEnumerable<Location2D> EnumerateBorderLocations()
		{
			yield return this;
			yield break;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x000A7AD2 File Offset: 0x000A5CD2
		public IEnumerable<Location2D> EnumerateNonBorderLocations()
		{
			yield return this;
			yield break;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x000A7AE2 File Offset: 0x000A5CE2
		public Location2D GetCenter()
		{
			return this;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x000A7AE5 File Offset: 0x000A5CE5
		public bool PointIn(Location2D location)
		{
			return location == this;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x000A7AF0 File Offset: 0x000A5CF0
		public int GetCourseFrom(Location2D Target)
		{
			int num = 0;
			if (Target == null || Target == this)
			{
				return num;
			}
			int x = Target.X;
			int y = Target.Y;
			if (this.X < x)
			{
				num |= 2;
			}
			else if (this.X > x)
			{
				num |= 8;
			}
			if (this.Y < y)
			{
				num |= 4;
			}
			else if (this.Y > y)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x000A7B4F File Offset: 0x000A5D4F
		public int GetCourseTo(Location2D Target)
		{
			return Courses.Opposite[this.GetCourseFrom(Target)];
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000A7B5E File Offset: 0x000A5D5E
		[Obsolete("Use Location2D.Zero")]
		public static Location2D zero
		{
			get
			{
				return Location2D.Zero;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000A7B65 File Offset: 0x000A5D65
		[Obsolete("Use Location2D.Invalid")]
		public static Location2D invalid
		{
			get
			{
				return Location2D.Invalid;
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000A7B6C File Offset: 0x000A5D6C
		[Obsolete("Use Location2D.Get")]
		public static Location2D get(int x, int y)
		{
			return Location2D.Get(x, y);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000A7B75 File Offset: 0x000A5D75
		[Obsolete("Use X")]
		public int x
		{
			get
			{
				return this.X;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000A7B7D File Offset: 0x000A5D7D
		[Obsolete("Use X")]
		public int y
		{
			get
			{
				return this.Y;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000A7B85 File Offset: 0x000A5D85
		[Obsolete("Use X")]
		public int __x
		{
			get
			{
				return this.X;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x000A7B8D File Offset: 0x000A5D8D
		[Obsolete("Use X")]
		public int __y
		{
			get
			{
				return this.Y;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x000A7B95 File Offset: 0x000A5D95
		[Obsolete("Use Point")]
		public Point2D point
		{
			get
			{
				return this.Point;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000A7B9D File Offset: 0x000A5D9D
		[Obsolete("Use Vector2i")]
		public Vector2i vector2i
		{
			get
			{
				return this.Vector2i;
			}
		}

		// Token: 0x040010DC RID: 4316
		public static readonly Location2D Zero = new Location2D(0, 0);

		// Token: 0x040010DD RID: 4317
		public static readonly Location2D Invalid = new Location2D(int.MinValue, int.MaxValue);

		// Token: 0x040010DE RID: 4318
		public static readonly int MaxX = 250;

		// Token: 0x040010DF RID: 4319
		public static readonly int MaxY = 85;

		// Token: 0x040010E0 RID: 4320
		public static Location2D[,] Grid;

		// Token: 0x040010E1 RID: 4321
		public readonly int X;

		// Token: 0x040010E2 RID: 4322
		public readonly int Y;

		// Token: 0x040010E3 RID: 4323
		public readonly int Ordinal;

		// Token: 0x040010E4 RID: 4324
		public static string[] Directions = new string[] { "N", "S", "E", "W", "NW", "NE", "SW", "SE" };

		// Token: 0x040010E5 RID: 4325
		public static string[,] RegionDirections;

		// Token: 0x040010E6 RID: 4326
		private static readonly List<Location2D> LineReturnCache;
	}
}
