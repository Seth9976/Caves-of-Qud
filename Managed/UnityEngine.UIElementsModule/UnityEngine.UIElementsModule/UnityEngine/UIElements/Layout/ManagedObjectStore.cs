using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000690 RID: 1680
	internal class ManagedObjectStore<T> where T : class
	{
		// Token: 0x060030CB RID: 12491 RVA: 0x000B957C File Offset: 0x000B777C
		public ManagedObjectStore()
		{
			this.m_Chunks = new List<T[]> { new T[2048] };
			this.m_Length = 1;
			this.m_Free = new Queue<int>();
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000B95C4 File Offset: 0x000B77C4
		public T GetValue(int index)
		{
			bool flag = index == 0;
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				int num = index / 2048;
				int num2 = index % 2048;
				t = this.m_Chunks[num][num2];
			}
			return t;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000B9610 File Offset: 0x000B7810
		public void UpdateValue(ref int index, T value)
		{
			bool flag = index != 0;
			if (flag)
			{
				bool flag2 = value != null;
				if (flag2)
				{
					int num = index / 2048;
					int num2 = index % 2048;
					this.m_Chunks[num][num2] = value;
				}
				else
				{
					this.m_Free.Enqueue(index);
					int num3 = index / 2048;
					int num4 = index % 2048;
					this.m_Chunks[num3][num4] = default(T);
					index = 0;
				}
			}
			else
			{
				bool flag3 = value != null;
				if (flag3)
				{
					bool flag4 = this.m_Free.Count > 0;
					if (flag4)
					{
						index = this.m_Free.Dequeue();
						int num5 = index / 2048;
						int num6 = index % 2048;
						this.m_Chunks[num5][num6] = value;
					}
					else
					{
						int length = this.m_Length;
						this.m_Length = length + 1;
						index = length;
						bool flag5 = index >= this.m_Chunks.Count * 2048;
						if (flag5)
						{
							this.m_Chunks.Add(new T[2048]);
						}
						int num7 = index / 2048;
						int num8 = index % 2048;
						this.m_Chunks[num7][num8] = value;
					}
				}
			}
		}

		// Token: 0x04001819 RID: 6169
		private const int k_ChunkSize = 2048;

		// Token: 0x0400181A RID: 6170
		private int m_Length;

		// Token: 0x0400181B RID: 6171
		private readonly List<T[]> m_Chunks;

		// Token: 0x0400181C RID: 6172
		private readonly Queue<int> m_Free;
	}
}
