using System;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000636 RID: 1590
	internal class EntryPool
	{
		// Token: 0x06002EB9 RID: 11961 RVA: 0x000A96C0 File Offset: 0x000A78C0
		public EntryPool(int maxCapacity = 1024)
		{
			this.m_ThreadEntries = new Stack<Entry>[JobsUtility.ThreadIndexCount];
			int i = 0;
			int threadIndexCount = JobsUtility.ThreadIndexCount;
			while (i < threadIndexCount)
			{
				this.m_ThreadEntries[i] = new Stack<Entry>(128);
				i++;
			}
			this.m_SharedPool = new ImplicitPool<Entry>(EntryPool.k_CreateAction, EntryPool.k_ResetAction, 128, maxCapacity);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000A9728 File Offset: 0x000A7928
		public Entry Get()
		{
			Stack<Entry> stack = this.m_ThreadEntries[UIRUtility.GetThreadIndex()];
			bool flag = stack.Count == 0;
			if (flag)
			{
				ImplicitPool<Entry> sharedPool = this.m_SharedPool;
				lock (sharedPool)
				{
					for (int i = 0; i < 128; i++)
					{
						stack.Push(this.m_SharedPool.Get());
					}
				}
			}
			return stack.Pop();
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000A97BC File Offset: 0x000A79BC
		public void ReturnAll()
		{
			int i = 0;
			int num = this.m_ThreadEntries.Length;
			while (i < num)
			{
				this.m_ThreadEntries[i].Clear();
				i++;
			}
			this.m_SharedPool.ReturnAll();
		}

		// Token: 0x040015B5 RID: 5557
		private const int k_StackSize = 128;

		// Token: 0x040015B6 RID: 5558
		private Stack<Entry>[] m_ThreadEntries;

		// Token: 0x040015B7 RID: 5559
		private ImplicitPool<Entry> m_SharedPool;

		// Token: 0x040015B8 RID: 5560
		private static readonly Func<Entry> k_CreateAction = () => new Entry();

		// Token: 0x040015B9 RID: 5561
		private static readonly Action<Entry> k_ResetAction = delegate(Entry e)
		{
			e.Reset();
		};
	}
}
