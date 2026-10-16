using System;
using System.Collections.Generic;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x0200000A RID: 10
	public abstract class StatisticalQueue<T>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002806 File Offset: 0x00000A06
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000280E File Offset: 0x00000A0E
		public int Count { get; private set; }

		// Token: 0x06000029 RID: 41 RVA: 0x00002817 File Offset: 0x00000A17
		public StatisticalQueue(int capacity)
		{
			this.m_capacity = capacity;
			this.Count = 0;
			this.m_list = new List<T>(this.m_capacity);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002840 File Offset: 0x00000A40
		public StatisticalQueue(int capacity, IEnumerable<T> startingValues)
		{
			this.m_capacity = capacity;
			List<T> list = new List<T>(startingValues);
			this.m_list = list.GetRange(Mathf.Max(0, list.Count - this.m_capacity), Mathf.Min(list.Count, this.m_capacity));
			this.Count = this.m_list.Count;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void AddValue(T item)
		{
			this.m_list.Add(item);
			if (this.Count < this.m_capacity)
			{
				int num = this.Count + 1;
				this.Count = num;
				return;
			}
			this.m_list.RemoveAt(0);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void Reset()
		{
			this.m_list.Clear();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028F5 File Offset: 0x00000AF5
		public bool IsEmpty()
		{
			return this.m_list.Count == 0;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002905 File Offset: 0x00000B05
		public T GetMostRecent()
		{
			return this.m_list[this.m_list.Count - 1];
		}

		// Token: 0x0600002F RID: 47
		public abstract T GetMax();

		// Token: 0x06000030 RID: 48
		public abstract T GetMin();

		// Token: 0x06000031 RID: 49
		public abstract T GetAverage();

		// Token: 0x06000032 RID: 50
		public abstract T GetInstantPartialDerivative();

		// Token: 0x06000033 RID: 51
		public abstract T GetAveragePartialDerivative();

		// Token: 0x04000014 RID: 20
		private int m_capacity;

		// Token: 0x04000016 RID: 22
		protected List<T> m_list;
	}
}
