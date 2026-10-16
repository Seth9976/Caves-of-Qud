using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005DF RID: 1503
	internal class ChunkAllocatingArray<T>
	{
		// Token: 0x06002C55 RID: 11349 RVA: 0x0009FF60 File Offset: 0x0009E160
		public ChunkAllocatingArray()
		{
			this.m_Chunks = new List<T[]> { new T[2048] };
		}

		// Token: 0x17000AF7 RID: 2807
		public T this[int index]
		{
			get
			{
				int num = index / 2048;
				int num2 = index % 2048;
				bool flag = num >= this.m_Chunks.Count;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				return this.m_Chunks[num][num2];
			}
			set
			{
				int i = index / 2048;
				int num = index % 2048;
				while (i >= this.m_Chunks.Count)
				{
					this.m_Chunks.Add(new T[2048]);
				}
				this.m_Chunks[i][num] = value;
			}
		}

		// Token: 0x04001441 RID: 5185
		private const int k_ChunkSize = 2048;

		// Token: 0x04001442 RID: 5186
		private readonly List<T[]> m_Chunks;
	}
}
