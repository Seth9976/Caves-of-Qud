using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000674 RID: 1652
	internal class TempAllocator<T> : IDisposable where T : struct
	{
		// Token: 0x06003040 RID: 12352 RVA: 0x000B66EC File Offset: 0x000B48EC
		public TempAllocator(int poolCapacity, int excessMinCapacity, int excessMaxCapacity)
		{
			Debug.Assert(poolCapacity >= 1);
			Debug.Assert(excessMinCapacity >= 1);
			Debug.Assert(excessMinCapacity <= excessMaxCapacity);
			this.m_ExcessMinCapacity = excessMinCapacity;
			this.m_ExcessMaxCapacity = excessMaxCapacity;
			this.m_NextExcessSize = this.m_ExcessMinCapacity;
			this.m_Pool = default(TempAllocator<T>.Page);
			this.m_Pool.array = new NativeArray<T>(poolCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			this.m_Excess = new List<TempAllocator<T>.Page>(8);
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003041 RID: 12353 RVA: 0x000B676D File Offset: 0x000B496D
		// (set) Token: 0x06003042 RID: 12354 RVA: 0x000B6775 File Offset: 0x000B4975
		private protected bool disposed { protected get; private set; }

		// Token: 0x06003043 RID: 12355 RVA: 0x000B677E File Offset: 0x000B497E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000B6790 File Offset: 0x000B4990
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Reset();
					this.m_Pool.array.Dispose();
					this.m_Pool.used = 0;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000B67E0 File Offset: 0x000B49E0
		public NativeSlice<T> Alloc(int count)
		{
			bool flag = count > 0;
			NativeSlice<T> nativeSlice2;
			if (flag)
			{
				NativeSlice<T> nativeSlice = this.DoAlloc(count);
				nativeSlice2 = nativeSlice;
			}
			else
			{
				nativeSlice2 = default(NativeSlice<T>);
			}
			return nativeSlice2;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000B6810 File Offset: 0x000B4A10
		private NativeSlice<T> DoAlloc(int count)
		{
			Debug.Assert(!this.disposed);
			int num = this.m_Pool.used + count;
			bool flag = num <= this.m_Pool.array.Length;
			NativeSlice<T> nativeSlice2;
			if (flag)
			{
				NativeSlice<T> nativeSlice = this.m_Pool.array.Slice<T>(this.m_Pool.used, count);
				this.m_Pool.used = num;
				nativeSlice2 = nativeSlice;
			}
			else
			{
				bool flag2 = count > this.m_ExcessMaxCapacity;
				if (flag2)
				{
					TempAllocator<T>.Page page = new TempAllocator<T>.Page
					{
						array = new NativeArray<T>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory),
						used = count
					};
					this.m_Excess.Add(page);
					nativeSlice2 = page.array.Slice<T>(0, count);
				}
				else
				{
					for (int i = this.m_Excess.Count - 1; i >= 0; i--)
					{
						TempAllocator<T>.Page page2 = this.m_Excess[i];
						num = page2.used + count;
						bool flag3 = num <= page2.array.Length;
						if (flag3)
						{
							NativeSlice<T> nativeSlice3 = page2.array.Slice<T>(page2.used, count);
							page2.used = num;
							this.m_Excess[i] = page2;
							return nativeSlice3;
						}
					}
					while (count > this.m_NextExcessSize)
					{
						this.m_NextExcessSize <<= 1;
					}
					TempAllocator<T>.Page page3 = new TempAllocator<T>.Page
					{
						array = new NativeArray<T>(this.m_NextExcessSize, Allocator.TempJob, NativeArrayOptions.UninitializedMemory),
						used = count
					};
					this.m_Excess.Add(page3);
					this.m_NextExcessSize = Mathf.Min(this.m_NextExcessSize << 1, this.m_ExcessMaxCapacity);
					nativeSlice2 = page3.array.Slice<T>(0, count);
				}
			}
			return nativeSlice2;
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000B69EB File Offset: 0x000B4BEB
		public void Reset()
		{
			this.ReleaseExcess();
			this.m_Pool.used = 0;
			this.m_NextExcessSize = this.m_ExcessMinCapacity;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000B6A10 File Offset: 0x000B4C10
		private void ReleaseExcess()
		{
			foreach (TempAllocator<T>.Page page in this.m_Excess)
			{
				NativeArray<T> array = page.array;
				array.Dispose();
			}
			this.m_Excess.Clear();
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000B6A7C File Offset: 0x000B4C7C
		public TempAllocator<T>.Statistics GatherStatistics()
		{
			TempAllocator<T>.Statistics statistics = new TempAllocator<T>.Statistics
			{
				pool = new TempAllocator<T>.PageStatistics
				{
					size = this.m_Pool.array.Length,
					used = this.m_Pool.used
				},
				excess = new TempAllocator<T>.PageStatistics[this.m_Excess.Count]
			};
			for (int i = 0; i < this.m_Excess.Count; i++)
			{
				statistics.excess[i] = new TempAllocator<T>.PageStatistics
				{
					size = this.m_Excess[i].array.Length,
					used = this.m_Excess[i].used
				};
			}
			return statistics;
		}

		// Token: 0x04001797 RID: 6039
		private readonly int m_ExcessMinCapacity;

		// Token: 0x04001798 RID: 6040
		private readonly int m_ExcessMaxCapacity;

		// Token: 0x04001799 RID: 6041
		private TempAllocator<T>.Page m_Pool;

		// Token: 0x0400179A RID: 6042
		private List<TempAllocator<T>.Page> m_Excess;

		// Token: 0x0400179B RID: 6043
		private int m_NextExcessSize;

		// Token: 0x02000675 RID: 1653
		private struct Page
		{
			// Token: 0x0400179D RID: 6045
			public NativeArray<T> array;

			// Token: 0x0400179E RID: 6046
			public int used;
		}

		// Token: 0x02000676 RID: 1654
		public struct Statistics
		{
			// Token: 0x0400179F RID: 6047
			public TempAllocator<T>.PageStatistics pool;

			// Token: 0x040017A0 RID: 6048
			public TempAllocator<T>.PageStatistics[] excess;
		}

		// Token: 0x02000677 RID: 1655
		public struct PageStatistics
		{
			// Token: 0x040017A1 RID: 6049
			public int size;

			// Token: 0x040017A2 RID: 6050
			public int used;
		}
	}
}
