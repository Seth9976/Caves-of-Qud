using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySharp.Utils
{
	// Token: 0x020001A9 RID: 425
	public abstract class Heap<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00064C26 File Offset: 0x00062E26
		public int Count
		{
			get
			{
				return this._tail;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00064C2E File Offset: 0x00062E2E
		public int Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00064C36 File Offset: 0x00062E36
		protected Comparer<T> Comparer { get; }

		// Token: 0x06001021 RID: 4129
		protected abstract bool Dominates(T x, T y);

		// Token: 0x06001022 RID: 4130 RVA: 0x00064C3E File Offset: 0x00062E3E
		protected Heap()
			: this(Comparer<T>.Default)
		{
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00064C4B File Offset: 0x00062E4B
		protected Heap(Comparer<T> comparer)
			: this(Enumerable.Empty<T>(), comparer)
		{
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00064C59 File Offset: 0x00062E59
		protected Heap(IEnumerable<T> collection)
			: this(collection, Comparer<T>.Default)
		{
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00064C68 File Offset: 0x00062E68
		protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.Comparer = comparer;
			foreach (T t in collection)
			{
				if (this.Count == this.Capacity)
				{
					this.Grow();
				}
				T[] heap = this._heap;
				int tail = this._tail;
				this._tail = tail + 1;
				heap[tail] = t;
			}
			for (int i = Heap<T>.Parent(this._tail - 1); i >= 0; i--)
			{
				this.BubbleDown(i);
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00064D30 File Offset: 0x00062F30
		public void Add(T item)
		{
			if (this.Count == this.Capacity)
			{
				this.Grow();
			}
			T[] heap = this._heap;
			int tail = this._tail;
			this._tail = tail + 1;
			heap[tail] = item;
			this.BubbleUp(this._tail - 1);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00064D7C File Offset: 0x00062F7C
		private void BubbleUp(int i)
		{
			while (i != 0 && !this.Dominates(this._heap[Heap<T>.Parent(i)], this._heap[i]))
			{
				this.Swap(i, Heap<T>.Parent(i));
				i = Heap<T>.Parent(i);
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00064DC8 File Offset: 0x00062FC8
		public T GetMin()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Heap is empty");
			}
			return this._heap[0];
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00064DEC File Offset: 0x00062FEC
		public T ExtractDominating()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException("Heap is empty");
			}
			T t = this._heap[0];
			this._tail--;
			this.Swap(this._tail, 0);
			this.BubbleDown(0);
			return t;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x00064E3C File Offset: 0x0006303C
		private void BubbleDown(int i)
		{
			for (;;)
			{
				int num = this.Dominating(i);
				if (num == i)
				{
					break;
				}
				this.Swap(i, num);
				i = num;
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00064E64 File Offset: 0x00063064
		private int Dominating(int i)
		{
			int dominating = this.GetDominating(Heap<T>.YoungChild(i), i);
			return this.GetDominating(Heap<T>.OldChild(i), dominating);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00064E90 File Offset: 0x00063090
		private int GetDominating(int newNode, int dominatingNode)
		{
			if (newNode < this._tail && !this.Dominates(this._heap[dominatingNode], this._heap[newNode]))
			{
				return newNode;
			}
			return dominatingNode;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00064EC0 File Offset: 0x000630C0
		private void Swap(int i, int j)
		{
			T t = this._heap[i];
			this._heap[i] = this._heap[j];
			this._heap[j] = t;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00064EFF File Offset: 0x000630FF
		private static int Parent(int i)
		{
			return (i + 1) / 2 - 1;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00064F08 File Offset: 0x00063108
		private static int YoungChild(int i)
		{
			return (i + 1) * 2 - 1;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00064F11 File Offset: 0x00063111
		private static int OldChild(int i)
		{
			return Heap<T>.YoungChild(i) + 1;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00064F1C File Offset: 0x0006311C
		private void Grow()
		{
			int num = this._capacity * 2 + 1;
			T[] array = new T[num];
			Array.Copy(this._heap, array, this._capacity);
			this._heap = array;
			this._capacity = num;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00064F5B File Offset: 0x0006315B
		public IEnumerator<T> GetEnumerator()
		{
			return this._heap.Take<T>(this.Count).GetEnumerator();
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00064F73 File Offset: 0x00063173
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000AFC RID: 2812
		private const int InitialCapacity = 0;

		// Token: 0x04000AFD RID: 2813
		private const int GrowFactor = 2;

		// Token: 0x04000AFE RID: 2814
		private const int MinGrow = 1;

		// Token: 0x04000AFF RID: 2815
		private int _capacity;

		// Token: 0x04000B00 RID: 2816
		private T[] _heap = new T[0];

		// Token: 0x04000B01 RID: 2817
		private int _tail;
	}
}
