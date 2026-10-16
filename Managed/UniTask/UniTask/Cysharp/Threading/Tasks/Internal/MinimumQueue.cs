using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200010D RID: 269
	internal class MinimumQueue<T>
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0000E0BC File Offset: 0x0000C2BC
		public MinimumQueue(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			this.array = new T[capacity];
			this.head = (this.tail = (this.size = 0));
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000E103 File Offset: 0x0000C303
		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.size;
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000E10B File Offset: 0x0000C30B
		public T Peek()
		{
			if (this.size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			return this.array[this.head];
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000E12C File Offset: 0x0000C32C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Enqueue(T item)
		{
			if (this.size == this.array.Length)
			{
				this.Grow();
			}
			this.array[this.tail] = item;
			this.MoveNext(ref this.tail);
			this.size++;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000E17C File Offset: 0x0000C37C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Dequeue()
		{
			if (this.size == 0)
			{
				this.ThrowForEmptyQueue();
			}
			int num = this.head;
			T[] array = this.array;
			T t = array[num];
			array[num] = default(T);
			this.MoveNext(ref this.head);
			this.size--;
			return t;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		private void Grow()
		{
			int num = (int)((long)this.array.Length * 200L / 100L);
			if (num < this.array.Length + 4)
			{
				num = this.array.Length + 4;
			}
			this.SetCapacity(num);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000E21C File Offset: 0x0000C41C
		private void SetCapacity(int capacity)
		{
			T[] array = new T[capacity];
			if (this.size > 0)
			{
				if (this.head < this.tail)
				{
					Array.Copy(this.array, this.head, array, 0, this.size);
				}
				else
				{
					Array.Copy(this.array, this.head, array, 0, this.array.Length - this.head);
					Array.Copy(this.array, 0, array, this.array.Length - this.head, this.tail);
				}
			}
			this.array = array;
			this.head = 0;
			this.tail = ((this.size == capacity) ? 0 : this.size);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000E2CC File Offset: 0x0000C4CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void MoveNext(ref int index)
		{
			int num = index + 1;
			if (num == this.array.Length)
			{
				num = 0;
			}
			index = num;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000E2EE File Offset: 0x0000C4EE
		private void ThrowForEmptyQueue()
		{
			throw new InvalidOperationException("EmptyQueue");
		}

		// Token: 0x0400010D RID: 269
		private const int MinimumGrow = 4;

		// Token: 0x0400010E RID: 270
		private const int GrowFactor = 200;

		// Token: 0x0400010F RID: 271
		private T[] array;

		// Token: 0x04000110 RID: 272
		private int head;

		// Token: 0x04000111 RID: 273
		private int tail;

		// Token: 0x04000112 RID: 274
		private int size;
	}
}
