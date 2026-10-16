using System;
using System.Diagnostics;

namespace TMPro
{
	// Token: 0x0200006A RID: 106
	[DebuggerDisplay("Item count = {m_Count}")]
	public struct TMP_TextProcessingStack<T>
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x0002B836 File Offset: 0x00029A36
		public TMP_TextProcessingStack(T[] stack)
		{
			this.itemStack = stack;
			this.m_Capacity = stack.Length;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002B869 File Offset: 0x00029A69
		public TMP_TextProcessingStack(int capacity)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = 0;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002B89F File Offset: 0x00029A9F
		public TMP_TextProcessingStack(int capacity, int rolloverSize)
		{
			this.itemStack = new T[capacity];
			this.m_Capacity = capacity;
			this.index = 0;
			this.m_RolloverSize = rolloverSize;
			this.m_DefaultItem = default(T);
			this.m_Count = 0;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0002B8D5 File Offset: 0x00029AD5
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0002B8DD File Offset: 0x00029ADD
		public T current
		{
			get
			{
				if (this.index > 0)
				{
					return this.itemStack[this.index - 1];
				}
				return this.itemStack[0];
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0002B908 File Offset: 0x00029B08
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x0002B910 File Offset: 0x00029B10
		public int rolloverSize
		{
			get
			{
				return this.m_RolloverSize;
			}
			set
			{
				this.m_RolloverSize = value;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002B91C File Offset: 0x00029B1C
		internal static void SetDefault(TMP_TextProcessingStack<T>[] stack, T item)
		{
			for (int i = 0; i < stack.Length; i++)
			{
				stack[i].SetDefault(item);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002B944 File Offset: 0x00029B44
		public void Clear()
		{
			this.index = 0;
			this.m_Count = 0;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002B954 File Offset: 0x00029B54
		public void SetDefault(T item)
		{
			if (this.itemStack == null)
			{
				this.m_Capacity = 4;
				this.itemStack = new T[this.m_Capacity];
				this.m_DefaultItem = default(T);
			}
			this.itemStack[0] = item;
			this.index = 1;
			this.m_Count = 1;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		public void Add(T item)
		{
			if (this.index < this.itemStack.Length)
			{
				this.itemStack[this.index] = item;
				this.index++;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002B9DC File Offset: 0x00029BDC
		public T Remove()
		{
			this.index--;
			this.m_Count--;
			if (this.index <= 0)
			{
				this.m_Count = 0;
				this.index = 1;
				return this.itemStack[0];
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002BA3C File Offset: 0x00029C3C
		public void Push(T item)
		{
			if (this.index == this.m_Capacity)
			{
				this.m_Capacity *= 2;
				if (this.m_Capacity == 0)
				{
					this.m_Capacity = 4;
				}
				Array.Resize<T>(ref this.itemStack, this.m_Capacity);
			}
			this.itemStack[this.index] = item;
			if (this.m_RolloverSize == 0)
			{
				this.index++;
				this.m_Count++;
				return;
			}
			this.index = (this.index + 1) % this.m_RolloverSize;
			this.m_Count = ((this.m_Count < this.m_RolloverSize) ? (this.m_Count + 1) : this.m_RolloverSize);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002BAF8 File Offset: 0x00029CF8
		public T Pop()
		{
			if (this.index == 0 && this.m_RolloverSize == 0)
			{
				return default(T);
			}
			if (this.m_RolloverSize == 0)
			{
				this.index--;
			}
			else
			{
				this.index = (this.index - 1) % this.m_RolloverSize;
				this.index = ((this.index < 0) ? (this.index + this.m_RolloverSize) : this.index);
			}
			T t = this.itemStack[this.index];
			this.itemStack[this.index] = this.m_DefaultItem;
			this.m_Count = ((this.m_Count > 0) ? (this.m_Count - 1) : 0);
			return t;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002BBB2 File Offset: 0x00029DB2
		public T Peek()
		{
			if (this.index == 0)
			{
				return this.m_DefaultItem;
			}
			return this.itemStack[this.index - 1];
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002BBD6 File Offset: 0x00029DD6
		public T CurrentItem()
		{
			if (this.index > 0)
			{
				return this.itemStack[this.index - 1];
			}
			return this.itemStack[0];
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002BC01 File Offset: 0x00029E01
		public T PreviousItem()
		{
			if (this.index > 1)
			{
				return this.itemStack[this.index - 2];
			}
			return this.itemStack[0];
		}

		// Token: 0x040004CF RID: 1231
		public T[] itemStack;

		// Token: 0x040004D0 RID: 1232
		public int index;

		// Token: 0x040004D1 RID: 1233
		private T m_DefaultItem;

		// Token: 0x040004D2 RID: 1234
		private int m_Capacity;

		// Token: 0x040004D3 RID: 1235
		private int m_RolloverSize;

		// Token: 0x040004D4 RID: 1236
		private int m_Count;

		// Token: 0x040004D5 RID: 1237
		private const int k_DefaultCapacity = 4;
	}
}
