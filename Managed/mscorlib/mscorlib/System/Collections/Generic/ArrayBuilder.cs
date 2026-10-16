using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AA6 RID: 2726
	internal struct ArrayBuilder<T>
	{
		// Token: 0x0600619D RID: 24989 RVA: 0x00146516 File Offset: 0x00144716
		public ArrayBuilder(int capacity)
		{
			this = default(ArrayBuilder<T>);
			if (capacity > 0)
			{
				this._array = new T[capacity];
			}
		}

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x0600619E RID: 24990 RVA: 0x0014652F File Offset: 0x0014472F
		public int Capacity
		{
			get
			{
				T[] array = this._array;
				if (array == null)
				{
					return 0;
				}
				return array.Length;
			}
		}

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x0600619F RID: 24991 RVA: 0x0014653F File Offset: 0x0014473F
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700115D RID: 4445
		public T this[int index]
		{
			get
			{
				return this._array[index];
			}
			set
			{
				this._array[index] = value;
			}
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x00146564 File Offset: 0x00144764
		public void Add(T item)
		{
			if (this._count == this.Capacity)
			{
				this.EnsureCapacity(this._count + 1);
			}
			this.UncheckedAdd(item);
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x00146589 File Offset: 0x00144789
		public T First()
		{
			return this._array[0];
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x00146597 File Offset: 0x00144797
		public T Last()
		{
			return this._array[this._count - 1];
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x001465AC File Offset: 0x001447AC
		public T[] ToArray()
		{
			if (this._count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = this._array;
			if (this._count < array.Length)
			{
				array = new T[this._count];
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			return array;
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x001465FC File Offset: 0x001447FC
		public void UncheckedAdd(T item)
		{
			T[] array = this._array;
			int count = this._count;
			this._count = count + 1;
			array[count] = item;
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x00146628 File Offset: 0x00144828
		private void EnsureCapacity(int minimum)
		{
			int capacity = this.Capacity;
			int num = ((capacity == 0) ? 4 : (2 * capacity));
			if (num > 2146435071)
			{
				num = Math.Max(capacity + 1, 2146435071);
			}
			num = Math.Max(num, minimum);
			T[] array = new T[num];
			if (this._count > 0)
			{
				Array.Copy(this._array, 0, array, 0, this._count);
			}
			this._array = array;
		}

		// Token: 0x040039F5 RID: 14837
		private const int DefaultCapacity = 4;

		// Token: 0x040039F6 RID: 14838
		private const int MaxCoreClrArrayLength = 2146435071;

		// Token: 0x040039F7 RID: 14839
		private T[] _array;

		// Token: 0x040039F8 RID: 14840
		private int _count;
	}
}
