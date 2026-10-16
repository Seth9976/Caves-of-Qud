using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002FD RID: 765
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00055DF0 File Offset: 0x00053FF0
		// (set) Token: 0x060015A9 RID: 5545 RVA: 0x00055E08 File Offset: 0x00054008
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = Math.Max(0, value);
				while (this.Size() > this.m_MaxSize)
				{
					this.Get();
				}
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00055E40 File Offset: 0x00054040
		public ObjectPool(Func<T> CreateFunc, int maxSize = 100)
		{
			this.maxSize = maxSize;
			bool flag = CreateFunc == null;
			if (flag)
			{
				this.CreateFunc = () => new T();
			}
			else
			{
				this.CreateFunc = CreateFunc;
			}
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00055EA4 File Offset: 0x000540A4
		public int Size()
		{
			return this.m_Stack.Count;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00055EC1 File Offset: 0x000540C1
		public void Clear()
		{
			this.m_Stack.Clear();
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00055ED0 File Offset: 0x000540D0
		public T Get()
		{
			return (this.m_Stack.Count == 0) ? this.CreateFunc() : this.m_Stack.Pop();
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00055F0C File Offset: 0x0005410C
		public void Release(T element)
		{
			bool flag = this.m_Stack.Count > 0 && this.m_Stack.Peek() == element;
			if (flag)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			bool flag2 = this.m_Stack.Count < this.maxSize;
			if (flag2)
			{
				this.m_Stack.Push(element);
			}
		}

		// Token: 0x04000BE3 RID: 3043
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x04000BE4 RID: 3044
		private int m_MaxSize;

		// Token: 0x04000BE5 RID: 3045
		internal Func<T> CreateFunc;
	}
}
