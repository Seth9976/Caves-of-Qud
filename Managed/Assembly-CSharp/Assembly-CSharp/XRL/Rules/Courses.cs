using System;

namespace XRL.Rules
{
	// Token: 0x020005D9 RID: 1497
	public static class Courses
	{
		// Token: 0x06004032 RID: 16434 RVA: 0x0013B114 File Offset: 0x00139314
		static Courses()
		{
			string[] directionList = Directions.DirectionList;
			int[] toIndex = Courses.ToIndex;
			string[] toDirection = Courses.ToDirection;
			Pivot[] toPivot = Courses.ToPivot;
			Pivot[][] toExpandPivot = Courses.ToExpandPivot;
			int[] opposite = Courses.Opposite;
			int[][] expand = Courses.Expand;
			int[] all = Courses.All;
			int i = 0;
			int num = all.Length;
			while (i < num)
			{
				int num2 = all[i];
				int num3 = 0;
				int num4 = 0;
				if ((num2 & 1) != 0)
				{
					num4 = -1;
				}
				else if ((num2 & 4) != 0)
				{
					num4 = 1;
				}
				if ((num2 & 2) != 0)
				{
					num3 = 1;
				}
				else if ((num2 & 8) != 0)
				{
					num3 = -1;
				}
				toIndex[num2] = i;
				toDirection[num2] = directionList[i];
				toPivot[num2] = new Pivot(num3, num4);
				opposite[num2] = ((num2 << 2) | (num2 >> 2)) & 15;
				i++;
			}
			int j = 0;
			int num5 = all.Length;
			while (j < num5)
			{
				int num6 = all[j];
				int num7 = 0;
				expand[num6] = new int[8];
				toExpandPivot[num6] = new Pivot[8];
				foreach (int num8 in Courses.IterateAdjacent(num6, true, false))
				{
					expand[num6][num7] = num8;
					toExpandPivot[num6][num7++] = toPivot[num8];
				}
				j++;
			}
			toDirection[0] = ".";
			toDirection[15] = "?";
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x0013B2F8 File Offset: 0x001394F8
		public static RingSpanEnumerator<int> IterateAdjacent(int Course, bool IncludeSelf = false, bool Invert = false)
		{
			return Courses.All.IterateRingAt<int>(Courses.ToIndex[Course], IncludeSelf, Invert);
		}

		// Token: 0x04001F6A RID: 8042
		public const int NONE = 0;

		// Token: 0x04001F6B RID: 8043
		public const int NORTH = 1;

		// Token: 0x04001F6C RID: 8044
		public const int EAST = 2;

		// Token: 0x04001F6D RID: 8045
		public const int SOUTH = 4;

		// Token: 0x04001F6E RID: 8046
		public const int WEST = 8;

		// Token: 0x04001F6F RID: 8047
		public const int NORTHEAST = 3;

		// Token: 0x04001F70 RID: 8048
		public const int SOUTHEAST = 6;

		// Token: 0x04001F71 RID: 8049
		public const int SOUTHWEST = 12;

		// Token: 0x04001F72 RID: 8050
		public const int NORTHWEST = 9;

		// Token: 0x04001F73 RID: 8051
		public const int OMNI = 15;

		// Token: 0x04001F74 RID: 8052
		public const int COUNT = 8;

		// Token: 0x04001F75 RID: 8053
		public const int LENGTH = 16;

		// Token: 0x04001F76 RID: 8054
		public static readonly Courses.Utilities.FromDirectionIndexer FromDirection = new Courses.Utilities.FromDirectionIndexer();

		// Token: 0x04001F77 RID: 8055
		public static readonly string[] ToDirection = new string[16];

		// Token: 0x04001F78 RID: 8056
		public static readonly int[] ToIndex = new int[16];

		// Token: 0x04001F79 RID: 8057
		public static readonly Pivot[] ToPivot = new Pivot[16];

		// Token: 0x04001F7A RID: 8058
		public static readonly Pivot[][] ToExpandPivot = new Pivot[16][];

		// Token: 0x04001F7B RID: 8059
		public static readonly int[] Opposite = new int[16];

		// Token: 0x04001F7C RID: 8060
		public static readonly int[][] Expand = new int[16][];

		// Token: 0x04001F7D RID: 8061
		public static readonly int[] All = new int[] { 9, 1, 3, 2, 6, 4, 12, 8 };

		// Token: 0x04001F7E RID: 8062
		public static readonly int[] Cardinal = new int[] { 1, 2, 4, 8 };

		// Token: 0x04001F7F RID: 8063
		public static readonly int[] Ordinal = new int[] { 9, 3, 6, 12 };

		// Token: 0x020019E8 RID: 6632
		public static class Utilities
		{
			// Token: 0x02001E8E RID: 7822
			public sealed class FromDirectionIndexer
			{
				// Token: 0x0600F7C9 RID: 63433 RVA: 0x0055D15C File Offset: 0x0055B35C
				static FromDirectionIndexer()
				{
					Courses.Utilities.FromDirectionIndexer.Array[9] = 1;
					Courses.Utilities.FromDirectionIndexer.Array[0] = 2;
					Courses.Utilities.FromDirectionIndexer.Array[14] = 4;
					Courses.Utilities.FromDirectionIndexer.Array[18] = 8;
					Courses.Utilities.FromDirectionIndexer.Array[41] = 1;
					Courses.Utilities.FromDirectionIndexer.Array[32] = 2;
					Courses.Utilities.FromDirectionIndexer.Array[46] = 4;
					Courses.Utilities.FromDirectionIndexer.Array[50] = 8;
				}

				// Token: 0x17001051 RID: 4177
				public int this[char Direction]
				{
					get
					{
						return Courses.Utilities.FromDirectionIndexer.Array[(int)(Direction - 'E')];
					}
				}

				// Token: 0x17001052 RID: 4178
				public int this[string Direction]
				{
					get
					{
						if (Direction.Length == 2)
						{
							return Courses.Utilities.FromDirectionIndexer.Array[(int)(Direction[0] - 'E')] | Courses.Utilities.FromDirectionIndexer.Array[(int)(Direction[1] - 'E')];
						}
						return Courses.Utilities.FromDirectionIndexer.Array[(int)(Direction[0] - 'E')];
					}
				}

				// Token: 0x040076B3 RID: 30387
				private const char FIRST = 'E';

				// Token: 0x040076B4 RID: 30388
				private const char LAST = 'w';

				// Token: 0x040076B5 RID: 30389
				private const int LENGTH = 51;

				// Token: 0x040076B6 RID: 30390
				private static readonly int[] Array = new int[51];
			}
		}
	}
}
