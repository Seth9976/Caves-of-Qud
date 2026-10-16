using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000613 RID: 1555
	internal class Allocator2D
	{
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000A2E9C File Offset: 0x000A109C
		internal List<Allocator2D.Area> areas
		{
			get
			{
				return this.m_Areas;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000A2EA4 File Offset: 0x000A10A4
		public Vector2Int minSize
		{
			get
			{
				return this.m_MinSize;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000A2EAC File Offset: 0x000A10AC
		public Vector2Int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000A2EB4 File Offset: 0x000A10B4
		public Vector2Int maxAllocSize
		{
			get
			{
				return this.m_MaxAllocSize;
			}
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000A2EBC File Offset: 0x000A10BC
		public Allocator2D(int minSize, int maxSize, int rowHeightBias)
			: this(new Vector2Int(minSize, minSize), new Vector2Int(maxSize, maxSize), rowHeightBias)
		{
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000A2ED8 File Offset: 0x000A10D8
		public Allocator2D(Vector2Int minSize, Vector2Int maxSize, int rowHeightBias)
		{
			Debug.Assert(minSize.x > 0 && minSize.x <= maxSize.x && minSize.y > 0 && minSize.y <= maxSize.y);
			Debug.Assert(minSize.x == UIRUtility.GetNextPow2(minSize.x) && minSize.y == UIRUtility.GetNextPow2(minSize.y) && maxSize.x == UIRUtility.GetNextPow2(maxSize.x) && maxSize.y == UIRUtility.GetNextPow2(maxSize.y));
			Debug.Assert(rowHeightBias >= 0);
			this.m_MinSize = minSize;
			this.m_MaxSize = maxSize;
			this.m_RowHeightBias = rowHeightBias;
			Allocator2D.BuildAreas(this.m_Areas, minSize, maxSize);
			this.m_MaxAllocSize = Allocator2D.ComputeMaxAllocSize(this.m_Areas, rowHeightBias);
			this.m_Rows = Allocator2D.BuildRowArray(this.m_MaxAllocSize.y, rowHeightBias);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000A2FF0 File Offset: 0x000A11F0
		public bool TryAllocate(int width, int height, out Allocator2D.Alloc2D alloc2D)
		{
			bool flag = width < 1 || width > this.m_MaxAllocSize.x || height < 1 || height > this.m_MaxAllocSize.y;
			bool flag2;
			if (flag)
			{
				alloc2D = default(Allocator2D.Alloc2D);
				flag2 = false;
			}
			else
			{
				int nextPow2Exp = UIRUtility.GetNextPow2Exp(Mathf.Max(height - this.m_RowHeightBias, 1));
				for (Allocator2D.Row row = this.m_Rows[nextPow2Exp]; row != null; row = row.next)
				{
					bool flag3 = row.rect.width >= width;
					if (flag3)
					{
						Alloc alloc = row.allocator.Allocate((uint)width);
						bool flag4 = alloc.size > 0U;
						if (flag4)
						{
							alloc2D = new Allocator2D.Alloc2D(row, alloc, width, height);
							return true;
						}
					}
				}
				int num = (1 << nextPow2Exp) + this.m_RowHeightBias;
				Debug.Assert(num >= height);
				for (int i = 0; i < this.m_Areas.Count; i++)
				{
					Allocator2D.Area area = this.m_Areas[i];
					bool flag5 = area.rect.height >= num && area.rect.width >= width;
					if (flag5)
					{
						Alloc alloc2 = area.allocator.Allocate((uint)num);
						bool flag6 = alloc2.size > 0U;
						if (flag6)
						{
							Allocator2D.Row row = Allocator2D.Row.pool.Get();
							row.alloc = alloc2;
							row.allocator = new BestFitAllocator((uint)area.rect.width);
							row.area = area;
							row.next = this.m_Rows[nextPow2Exp];
							row.rect = new RectInt(area.rect.xMin, area.rect.yMin + (int)alloc2.start, area.rect.width, num);
							this.m_Rows[nextPow2Exp] = row;
							Alloc alloc3 = row.allocator.Allocate((uint)width);
							Debug.Assert(alloc3.size > 0U);
							alloc2D = new Allocator2D.Alloc2D(row, alloc3, width, height);
							return true;
						}
					}
				}
				alloc2D = default(Allocator2D.Alloc2D);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000A322C File Offset: 0x000A142C
		public void Free(Allocator2D.Alloc2D alloc2D)
		{
			bool flag = alloc2D.alloc.size == 0U;
			if (!flag)
			{
				Allocator2D.Row row = alloc2D.row;
				row.allocator.Free(alloc2D.alloc);
				bool flag2 = row.allocator.highWatermark == 0U;
				if (flag2)
				{
					row.area.allocator.Free(row.alloc);
					int nextPow2Exp = UIRUtility.GetNextPow2Exp(row.rect.height - this.m_RowHeightBias);
					Allocator2D.Row row2 = this.m_Rows[nextPow2Exp];
					bool flag3 = row2 == row;
					if (flag3)
					{
						this.m_Rows[nextPow2Exp] = row.next;
					}
					else
					{
						Allocator2D.Row row3 = row2;
						while (row3.next != row)
						{
							row3 = row3.next;
						}
						row3.next = row.next;
					}
					Allocator2D.Row.pool.Return(row);
				}
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000A3314 File Offset: 0x000A1514
		private static void BuildAreas(List<Allocator2D.Area> areas, Vector2Int minSize, Vector2Int maxSize)
		{
			int num = Mathf.Min(minSize.x, minSize.y);
			int num2 = num;
			areas.Add(new Allocator2D.Area(new RectInt(0, 0, num, num2)));
			while (num < maxSize.x || num2 < maxSize.y)
			{
				bool flag = num < maxSize.x;
				if (flag)
				{
					areas.Add(new Allocator2D.Area(new RectInt(num, 0, num, num2)));
					num *= 2;
				}
				bool flag2 = num2 < maxSize.y;
				if (flag2)
				{
					areas.Add(new Allocator2D.Area(new RectInt(0, num2, num, num2)));
					num2 *= 2;
				}
			}
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000A33C0 File Offset: 0x000A15C0
		private static Vector2Int ComputeMaxAllocSize(List<Allocator2D.Area> areas, int rowHeightBias)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < areas.Count; i++)
			{
				Allocator2D.Area area = areas[i];
				num = Mathf.Max(area.rect.width, num);
				num2 = Mathf.Max(area.rect.height, num2);
			}
			return new Vector2Int(num, UIRUtility.GetPrevPow2(num2 - rowHeightBias) + rowHeightBias);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000A3430 File Offset: 0x000A1630
		private static Allocator2D.Row[] BuildRowArray(int maxRowHeight, int rowHeightBias)
		{
			int num = UIRUtility.GetNextPow2Exp(maxRowHeight - rowHeightBias) + 1;
			return new Allocator2D.Row[num];
		}

		// Token: 0x040014D5 RID: 5333
		private readonly Vector2Int m_MinSize;

		// Token: 0x040014D6 RID: 5334
		private readonly Vector2Int m_MaxSize;

		// Token: 0x040014D7 RID: 5335
		private readonly Vector2Int m_MaxAllocSize;

		// Token: 0x040014D8 RID: 5336
		private readonly int m_RowHeightBias;

		// Token: 0x040014D9 RID: 5337
		private readonly Allocator2D.Row[] m_Rows;

		// Token: 0x040014DA RID: 5338
		private readonly List<Allocator2D.Area> m_Areas = new List<Allocator2D.Area>();

		// Token: 0x02000614 RID: 1556
		public class Area
		{
			// Token: 0x06002E1F RID: 11807 RVA: 0x000A3453 File Offset: 0x000A1653
			public Area(RectInt rect)
			{
				this.rect = rect;
				this.allocator = new BestFitAllocator((uint)rect.height);
			}

			// Token: 0x040014DB RID: 5339
			public RectInt rect;

			// Token: 0x040014DC RID: 5340
			public BestFitAllocator allocator;
		}

		// Token: 0x02000615 RID: 1557
		public class Row : LinkedPoolItem<Allocator2D.Row>
		{
			// Token: 0x06002E20 RID: 11808 RVA: 0x000A3476 File Offset: 0x000A1676
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static Allocator2D.Row Create()
			{
				return new Allocator2D.Row();
			}

			// Token: 0x06002E21 RID: 11809 RVA: 0x000A347D File Offset: 0x000A167D
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Reset(Allocator2D.Row row)
			{
				row.rect = default(RectInt);
				row.area = null;
				row.allocator = null;
				row.alloc = default(Alloc);
				row.next = null;
			}

			// Token: 0x040014DD RID: 5341
			public RectInt rect;

			// Token: 0x040014DE RID: 5342
			public Allocator2D.Area area;

			// Token: 0x040014DF RID: 5343
			public BestFitAllocator allocator;

			// Token: 0x040014E0 RID: 5344
			public Alloc alloc;

			// Token: 0x040014E1 RID: 5345
			public Allocator2D.Row next;

			// Token: 0x040014E2 RID: 5346
			public static readonly LinkedPool<Allocator2D.Row> pool = new LinkedPool<Allocator2D.Row>(new Func<Allocator2D.Row>(Allocator2D.Row.Create), new Action<Allocator2D.Row>(Allocator2D.Row.Reset), 256);
		}

		// Token: 0x02000616 RID: 1558
		public struct Alloc2D
		{
			// Token: 0x06002E24 RID: 11812 RVA: 0x000A34DF File Offset: 0x000A16DF
			public Alloc2D(Allocator2D.Row row, Alloc alloc, int width, int height)
			{
				this.alloc = alloc;
				this.row = row;
				this.rect = new RectInt(row.rect.xMin + (int)alloc.start, row.rect.yMin, width, height);
			}

			// Token: 0x040014E3 RID: 5347
			public RectInt rect;

			// Token: 0x040014E4 RID: 5348
			public Allocator2D.Row row;

			// Token: 0x040014E5 RID: 5349
			public Alloc alloc;
		}
	}
}
