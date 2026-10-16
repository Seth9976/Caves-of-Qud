using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000657 RID: 1623
	internal class NativeList<T> : IDisposable where T : struct
	{
		// Token: 0x06002F79 RID: 12153 RVA: 0x000B0D14 File Offset: 0x000AEF14
		public NativeList(int initialCapacity)
		{
			Debug.Assert(initialCapacity > 0);
			this.m_NativeArray = new NativeArray<T>(initialCapacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000B0D38 File Offset: 0x000AEF38
		private void Expand(int newLength)
		{
			NativeArray<T> nativeArray = new NativeArray<T>(newLength, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			nativeArray.Slice<T>(0, this.m_Count).CopyFrom(this.m_NativeArray);
			this.m_NativeArray.Dispose();
			this.m_NativeArray = nativeArray;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000B0D84 File Offset: 0x000AEF84
		public void Add(ref T data)
		{
			bool flag = this.m_Count == this.m_NativeArray.Length;
			if (flag)
			{
				this.Expand(this.m_NativeArray.Length << 1);
			}
			int count = this.m_Count;
			this.m_Count = count + 1;
			this.m_NativeArray[count] = data;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000B0DE0 File Offset: 0x000AEFE0
		public void Add(NativeSlice<T> src)
		{
			int num = this.m_Count + src.Length;
			bool flag = this.m_NativeArray.Length < num;
			if (flag)
			{
				this.Expand(num << 1);
			}
			this.m_NativeArray.Slice<T>(this.m_Count, src.Length).CopyFrom(src);
			this.m_Count += src.Length;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000B0E4F File Offset: 0x000AF04F
		public void Clear()
		{
			this.m_Count = 0;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000B0E5C File Offset: 0x000AF05C
		public NativeSlice<T> GetSlice(int start, int length)
		{
			return this.m_NativeArray.Slice<T>(start, length);
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000B0E7B File Offset: 0x000AF07B
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000B0E83 File Offset: 0x000AF083
		// (set) Token: 0x06002F81 RID: 12161 RVA: 0x000B0E8B File Offset: 0x000AF08B
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F82 RID: 12162 RVA: 0x000B0E94 File Offset: 0x000AF094
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000B0EA8 File Offset: 0x000AF0A8
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_NativeArray.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x040016BE RID: 5822
		private NativeArray<T> m_NativeArray;

		// Token: 0x040016BF RID: 5823
		private int m_Count;
	}
}
