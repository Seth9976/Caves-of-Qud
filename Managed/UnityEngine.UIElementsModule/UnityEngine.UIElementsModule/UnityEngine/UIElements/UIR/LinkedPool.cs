using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000649 RID: 1609
	internal class LinkedPool<T> where T : LinkedPoolItem<T>
	{
		// Token: 0x06002F25 RID: 12069 RVA: 0x000AC34B File Offset: 0x000AA54B
		public LinkedPool(Func<T> createFunc, Action<T> resetAction, int limit = 10000)
		{
			Debug.Assert(createFunc != null);
			this.m_CreateFunc = createFunc;
			this.m_ResetAction = resetAction;
			Debug.Assert(limit > 0);
			this.m_Limit = limit;
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000AC37E File Offset: 0x000AA57E
		// (set) Token: 0x06002F27 RID: 12071 RVA: 0x000AC386 File Offset: 0x000AA586
		public int Count { get; private set; }

		// Token: 0x06002F28 RID: 12072 RVA: 0x000AC38F File Offset: 0x000AA58F
		public void Clear()
		{
			this.m_PoolFirst = default(T);
			this.Count = 0;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000AC3A8 File Offset: 0x000AA5A8
		public T Get()
		{
			T t = this.m_PoolFirst;
			bool flag = this.m_PoolFirst != null;
			if (flag)
			{
				int num = this.Count - 1;
				this.Count = num;
				this.m_PoolFirst = t.poolNext;
				Action<T> resetAction = this.m_ResetAction;
				if (resetAction != null)
				{
					resetAction(t);
				}
			}
			else
			{
				t = this.m_CreateFunc();
			}
			return t;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000AC41C File Offset: 0x000AA61C
		public void Return(T item)
		{
			bool flag = this.Count < this.m_Limit;
			if (flag)
			{
				item.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = item;
				int num = this.Count + 1;
				this.Count = num;
			}
		}

		// Token: 0x0400165A RID: 5722
		private readonly Func<T> m_CreateFunc;

		// Token: 0x0400165B RID: 5723
		private readonly Action<T> m_ResetAction;

		// Token: 0x0400165C RID: 5724
		private readonly int m_Limit;

		// Token: 0x0400165D RID: 5725
		private T m_PoolFirst;
	}
}
