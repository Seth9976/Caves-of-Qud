using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000658 RID: 1624
	internal class NativePagedList<T> : IDisposable where T : struct
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
		public NativePagedList(int poolCapacity, Allocator firstPageAllocator = Allocator.Persistent, Allocator otherPagesAllocator = Allocator.Persistent)
		{
			Debug.Assert(poolCapacity > 0);
			this.k_PoolCapacity = Mathf.NextPowerOfTwo(poolCapacity);
			this.m_FirstPageAllocator = firstPageAllocator;
			this.m_OtherPagesAllocator = otherPagesAllocator;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000B0F34 File Offset: 0x000AF134
		public void Add(ref T data)
		{
			bool flag = this.m_CountInLastPage < this.m_LastPage.Length;
			if (flag)
			{
				int countInLastPage = this.m_CountInLastPage;
				this.m_CountInLastPage = countInLastPage + 1;
				this.m_LastPage[countInLastPage] = data;
			}
			else
			{
				int num = ((this.m_Pages.Count > 0) ? (this.m_LastPage.Length << 1) : this.k_PoolCapacity);
				Allocator allocator = ((this.m_Pages.Count == 0) ? this.m_FirstPageAllocator : this.m_OtherPagesAllocator);
				this.m_LastPage = new NativeArray<T>(num, allocator, NativeArrayOptions.UninitializedMemory);
				this.m_Pages.Add(this.m_LastPage);
				this.m_LastPage[0] = data;
				this.m_CountInLastPage = 1;
			}
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000B0FF9 File Offset: 0x000AF1F9
		public void Add(T data)
		{
			this.Add(ref data);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000B1008 File Offset: 0x000AF208
		public List<NativeSlice<T>> GetPages()
		{
			this.m_Enumerator.Clear();
			bool flag = this.m_Pages.Count > 0;
			if (flag)
			{
				int num = this.m_Pages.Count - 1;
				for (int i = 0; i < num; i++)
				{
					this.m_Enumerator.Add(this.m_Pages[i]);
				}
				bool flag2 = this.m_CountInLastPage > 0;
				if (flag2)
				{
					this.m_Enumerator.Add(this.m_LastPage.Slice<T>(0, this.m_CountInLastPage));
				}
			}
			return this.m_Enumerator;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000B10AC File Offset: 0x000AF2AC
		public int GetCount()
		{
			int num = this.m_CountInLastPage;
			for (int i = 0; i < this.m_Pages.Count - 1; i++)
			{
				num += this.m_Pages[i].Length;
			}
			return num;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000B10FC File Offset: 0x000AF2FC
		public void Reset()
		{
			bool flag = this.m_Pages.Count > 1;
			if (flag)
			{
				this.m_LastPage = this.m_Pages[0];
				for (int i = 1; i < this.m_Pages.Count; i++)
				{
					this.m_Pages[i].Dispose();
				}
				this.m_Pages.Clear();
				this.m_Pages.Add(this.m_LastPage);
			}
			this.m_CountInLastPage = 0;
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x000B1184 File Offset: 0x000AF384
		// (set) Token: 0x06002F8B RID: 12171 RVA: 0x000B118C File Offset: 0x000AF38C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F8C RID: 12172 RVA: 0x000B1195 File Offset: 0x000AF395
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000B11A8 File Offset: 0x000AF3A8
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_Pages.Count; i++)
					{
						this.m_Pages[i].Dispose();
					}
					this.m_Pages.Clear();
					this.m_CountInLastPage = 0;
				}
				this.disposed = true;
			}
		}

		// Token: 0x040016C1 RID: 5825
		private readonly int k_PoolCapacity;

		// Token: 0x040016C2 RID: 5826
		private List<NativeArray<T>> m_Pages = new List<NativeArray<T>>(8);

		// Token: 0x040016C3 RID: 5827
		private NativeArray<T> m_LastPage;

		// Token: 0x040016C4 RID: 5828
		private int m_CountInLastPage;

		// Token: 0x040016C5 RID: 5829
		private Allocator m_FirstPageAllocator;

		// Token: 0x040016C6 RID: 5830
		private Allocator m_OtherPagesAllocator;

		// Token: 0x040016C7 RID: 5831
		private List<NativeSlice<T>> m_Enumerator = new List<NativeSlice<T>>(8);

		// Token: 0x02000659 RID: 1625
		public struct Enumerator
		{
			// Token: 0x06002F8E RID: 12174 RVA: 0x000B1218 File Offset: 0x000AF418
			public Enumerator(NativePagedList<T> nativePagedList, int offset)
			{
				this.m_IndexInCurrentPage = 0;
				this.m_IndexOfCurrentPage = 0;
				this.m_CountInCurrentPage = 0;
				this.m_NativePagedList = nativePagedList;
				for (int i = 0; i < this.m_NativePagedList.m_Pages.Count - 1; i++)
				{
					this.m_CountInCurrentPage = this.m_NativePagedList.m_Pages[i].Length;
					bool flag = offset >= this.m_CountInCurrentPage;
					if (!flag)
					{
						this.m_IndexInCurrentPage = offset;
						this.m_IndexOfCurrentPage = i;
						this.m_CurrentPage = this.m_NativePagedList.m_Pages[this.m_IndexOfCurrentPage];
						return;
					}
					offset -= this.m_CountInCurrentPage;
				}
				this.m_IndexOfCurrentPage = this.m_NativePagedList.m_Pages.Count - 1;
				this.m_CountInCurrentPage = this.m_NativePagedList.m_CountInLastPage;
				this.m_IndexInCurrentPage = offset;
				this.m_CurrentPage = this.m_NativePagedList.m_LastPage;
			}

			// Token: 0x06002F8F RID: 12175 RVA: 0x000B1314 File Offset: 0x000AF514
			public bool HasNext()
			{
				return this.m_IndexInCurrentPage < this.m_CountInCurrentPage;
			}

			// Token: 0x06002F90 RID: 12176 RVA: 0x000B1334 File Offset: 0x000AF534
			public T GetNext()
			{
				bool flag = !this.HasNext();
				if (flag)
				{
					throw new InvalidOperationException("No more elements");
				}
				T t = this.m_CurrentPage[this.m_IndexInCurrentPage];
				this.m_IndexInCurrentPage++;
				bool flag2 = this.m_IndexInCurrentPage == this.m_CountInCurrentPage;
				if (flag2)
				{
					this.m_IndexInCurrentPage = 0;
					this.m_IndexOfCurrentPage++;
					int count = this.m_NativePagedList.m_Pages.Count;
					bool flag3 = this.m_IndexOfCurrentPage < count;
					if (flag3)
					{
						bool flag4 = this.m_IndexOfCurrentPage < count - 1;
						if (flag4)
						{
							this.m_CountInCurrentPage = this.m_NativePagedList.m_Pages[this.m_IndexOfCurrentPage].Length;
						}
						else
						{
							this.m_CountInCurrentPage = this.m_NativePagedList.m_CountInLastPage;
						}
					}
					else
					{
						this.m_IndexOfCurrentPage = count - 1;
						this.m_CountInCurrentPage = this.m_NativePagedList.m_CountInLastPage;
						this.m_IndexInCurrentPage = this.m_CountInCurrentPage;
					}
					this.m_CurrentPage = this.m_NativePagedList.m_Pages[this.m_IndexOfCurrentPage];
				}
				return t;
			}

			// Token: 0x040016C9 RID: 5833
			private NativePagedList<T> m_NativePagedList;

			// Token: 0x040016CA RID: 5834
			private NativeArray<T> m_CurrentPage;

			// Token: 0x040016CB RID: 5835
			private int m_IndexInCurrentPage;

			// Token: 0x040016CC RID: 5836
			private int m_IndexOfCurrentPage;

			// Token: 0x040016CD RID: 5837
			private int m_CountInCurrentPage;
		}
	}
}
