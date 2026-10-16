using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F6 RID: 758
	[Serializable]
	public class Deque<T> : ListBase<T>, ICloneable
	{
		// Token: 0x06001AAC RID: 6828 RVA: 0x0008673F File Offset: 0x0008493F
		private void StopEnumerations()
		{
			this.changeStamp++;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0008674F File Offset: 0x0008494F
		private void CheckEnumerationStamp(int startStamp)
		{
			if (startStamp != this.changeStamp)
			{
				throw new InvalidOperationException(Strings.ChangeDuringEnumeration);
			}
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00086765 File Offset: 0x00084965
		public Deque()
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0008676D File Offset: 0x0008496D
		public Deque(IEnumerable<T> collection)
		{
			this.AddManyToBack(collection);
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0008677C File Offset: 0x0008497C
		public sealed override int Count
		{
			get
			{
				if (this.end >= this.start)
				{
					return this.end - this.start;
				}
				return this.end + this.buffer.Length - this.start;
			}
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000867B0 File Offset: 0x000849B0
		public sealed override void CopyTo(T[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = ((this.buffer == null) ? 0 : this.buffer.Length);
			if (this.start > this.end)
			{
				Array.Copy(this.buffer, this.start, array, arrayIndex, num - this.start);
				Array.Copy(this.buffer, 0, array, arrayIndex + num - this.start, this.end);
				return;
			}
			if (this.end > this.start)
			{
				Array.Copy(this.buffer, this.start, array, arrayIndex, this.end - this.start);
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x00086854 File Offset: 0x00084A54
		// (set) Token: 0x06001AB3 RID: 6835 RVA: 0x0008686C File Offset: 0x00084A6C
		public int Capacity
		{
			get
			{
				if (this.buffer == null)
				{
					return 0;
				}
				return this.buffer.Length - 1;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", Strings.CapacityLessThanCount);
				}
				if (value > 2147483646)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == this.Capacity)
				{
					return;
				}
				T[] array = new T[value + 1];
				this.CopyTo(array, 0);
				this.end = this.Count;
				this.start = 0;
				this.buffer = array;
			}
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000868DA File Offset: 0x00084ADA
		public void TrimToSize()
		{
			this.Capacity = this.Count;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000868E8 File Offset: 0x00084AE8
		public sealed override void Clear()
		{
			this.StopEnumerations();
			this.buffer = null;
			this.start = (this.end = 0);
		}

		// Token: 0x17000390 RID: 912
		public sealed override T this[int index]
		{
			get
			{
				int num = index + this.start;
				if (num < this.start)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.end < this.start)
				{
					int num2 = this.buffer.Length;
					if (num >= num2)
					{
						num -= num2;
						if (num >= this.end)
						{
							throw new ArgumentOutOfRangeException("index");
						}
					}
					return this.buffer[num];
				}
				if (num >= this.end)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.buffer[num];
			}
			set
			{
				this.StopEnumerations();
				int num = index + this.start;
				if (num < this.start)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.end < this.start)
				{
					int num2 = this.buffer.Length;
					if (num >= num2)
					{
						num -= num2;
						if (num >= this.end)
						{
							throw new ArgumentOutOfRangeException("index");
						}
					}
					this.buffer[num] = value;
					return;
				}
				if (num >= this.end)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.buffer[num] = value;
			}
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00086A32 File Offset: 0x00084C32
		public sealed override IEnumerator<T> GetEnumerator()
		{
			int startStamp = this.changeStamp;
			int count = this.Count;
			int num;
			for (int i = 0; i < count; i = num)
			{
				yield return this[i];
				this.CheckEnumerationStamp(startStamp);
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00086A41 File Offset: 0x00084C41
		private void CreateInitialBuffer(T firstItem)
		{
			this.buffer = new T[8];
			this.start = 0;
			this.end = 1;
			this.buffer[0] = firstItem;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00086A6C File Offset: 0x00084C6C
		public sealed override void Insert(int index, T item)
		{
			this.StopEnumerations();
			int count = this.Count;
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this.buffer == null)
			{
				this.CreateInitialBuffer(item);
				return;
			}
			int num = this.buffer.Length;
			int num2;
			if (index < count / 2)
			{
				this.start--;
				if (this.start < 0)
				{
					this.start += num;
				}
				num2 = index + this.start;
				if (num2 >= num)
				{
					num2 -= num;
					if (num - 1 > this.start)
					{
						Array.Copy(this.buffer, this.start + 1, this.buffer, this.start, num - 1 - this.start);
					}
					this.buffer[num - 1] = this.buffer[0];
					if (num2 > 0)
					{
						Array.Copy(this.buffer, 1, this.buffer, 0, num2);
					}
				}
				else if (num2 > this.start)
				{
					Array.Copy(this.buffer, this.start + 1, this.buffer, this.start, num2 - this.start);
				}
			}
			else
			{
				num2 = index + this.start;
				if (num2 >= num)
				{
					num2 -= num;
				}
				if (num2 <= this.end)
				{
					if (this.end > num2)
					{
						Array.Copy(this.buffer, num2, this.buffer, num2 + 1, this.end - num2);
					}
					this.end++;
					if (this.end >= num)
					{
						this.end -= num;
					}
				}
				else
				{
					if (this.end > 0)
					{
						Array.Copy(this.buffer, 0, this.buffer, 1, this.end);
					}
					this.buffer[0] = this.buffer[num - 1];
					if (num - 1 > num2)
					{
						Array.Copy(this.buffer, num2, this.buffer, num2 + 1, num - 1 - num2);
					}
					this.end++;
				}
			}
			this.buffer[num2] = item;
			if (this.start == this.end)
			{
				this.IncreaseBuffer();
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00086C90 File Offset: 0x00084E90
		public void InsertRange(int index, IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.StopEnumerations();
			int count = this.Count;
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			ICollection<T> collection2;
			if (collection is ICollection<T>)
			{
				collection2 = (ICollection<T>)collection;
			}
			else
			{
				collection2 = new List<T>(collection);
			}
			if (collection2.Count == 0)
			{
				return;
			}
			if (this.Capacity < this.Count + collection2.Count)
			{
				this.Capacity = this.Count + collection2.Count;
			}
			int num = this.buffer.Length;
			int num3;
			if (index < count / 2)
			{
				int num2 = this.start;
				num3 = num2 - collection2.Count;
				if (num3 < 0)
				{
					num3 += num;
				}
				this.start = num3;
				int i = index;
				while (i > 0)
				{
					int num4 = i;
					if (num - num3 < num4)
					{
						num4 = num - num3;
					}
					if (num - num2 < num4)
					{
						num4 = num - num2;
					}
					Array.Copy(this.buffer, num2, this.buffer, num3, num4);
					i -= num4;
					if ((num3 += num4) >= num)
					{
						num3 -= num;
					}
					if ((num2 += num4) >= num)
					{
						num2 -= num;
					}
				}
			}
			else
			{
				int num2 = this.end;
				num3 = num2 + collection2.Count;
				if (num3 >= num)
				{
					num3 -= num;
				}
				this.end = num3;
				int num5;
				for (int j = count - index; j > 0; j -= num5)
				{
					num5 = j;
					if (num3 > 0 && num3 < num5)
					{
						num5 = num3;
					}
					if (num2 > 0 && num2 < num5)
					{
						num5 = num2;
					}
					if ((num3 -= num5) < 0)
					{
						num3 += num;
					}
					if ((num2 -= num5) < 0)
					{
						num2 += num;
					}
					Array.Copy(this.buffer, num2, this.buffer, num3, num5);
				}
				num3 -= collection2.Count;
				if (num3 < 0)
				{
					num3 += num;
				}
			}
			foreach (T t in collection2)
			{
				this.buffer[num3] = t;
				if (++num3 >= num)
				{
					num3 -= num;
				}
			}
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00086EB0 File Offset: 0x000850B0
		public sealed override void RemoveAt(int index)
		{
			this.StopEnumerations();
			int count = this.Count;
			if (index < 0 || index >= count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			int num = this.buffer.Length;
			if (index < count / 2)
			{
				int num2 = index + this.start;
				if (num2 >= num)
				{
					num2 -= num;
					if (num2 > 0)
					{
						Array.Copy(this.buffer, 0, this.buffer, 1, num2);
					}
					this.buffer[0] = this.buffer[num - 1];
					if (num - 1 > this.start)
					{
						Array.Copy(this.buffer, this.start, this.buffer, this.start + 1, num - 1 - this.start);
					}
				}
				else if (num2 > this.start)
				{
					Array.Copy(this.buffer, this.start, this.buffer, this.start + 1, num2 - this.start);
				}
				this.buffer[this.start] = default(T);
				this.start++;
				if (this.start >= num)
				{
					this.start -= num;
					return;
				}
			}
			else
			{
				int num2 = index + this.start;
				if (num2 >= num)
				{
					num2 -= num;
				}
				this.end--;
				if (this.end < 0)
				{
					this.end = num - 1;
				}
				if (num2 <= this.end)
				{
					if (this.end > num2)
					{
						Array.Copy(this.buffer, num2 + 1, this.buffer, num2, this.end - num2);
					}
				}
				else
				{
					if (num - 1 > num2)
					{
						Array.Copy(this.buffer, num2 + 1, this.buffer, num2, num - 1 - num2);
					}
					this.buffer[num - 1] = this.buffer[0];
					if (this.end > 0)
					{
						Array.Copy(this.buffer, 1, this.buffer, 0, this.end);
					}
				}
				this.buffer[this.end] = default(T);
			}
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000870B4 File Offset: 0x000852B4
		public void RemoveRange(int index, int count)
		{
			this.StopEnumerations();
			int count2 = this.Count;
			if (index < 0 || index >= count2)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0 || count > count2 - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count == 0)
			{
				return;
			}
			int num = this.buffer.Length;
			int num2;
			int num3;
			if (index < count2 / 2)
			{
				num2 = this.start + index;
				if (num2 >= num)
				{
					num2 -= num;
				}
				num3 = num2 + count;
				if (num3 >= num)
				{
					num3 -= num;
				}
				int num4;
				for (int i = index; i > 0; i -= num4)
				{
					num4 = i;
					if (num3 > 0 && num3 < num4)
					{
						num4 = num3;
					}
					if (num2 > 0 && num2 < num4)
					{
						num4 = num2;
					}
					if ((num3 -= num4) < 0)
					{
						num3 += num;
					}
					if ((num2 -= num4) < 0)
					{
						num2 += num;
					}
					Array.Copy(this.buffer, num2, this.buffer, num3, num4);
				}
				for (int i = 0; i < count; i++)
				{
					this.buffer[num2] = default(T);
					if (++num2 >= num)
					{
						num2 -= num;
					}
				}
				this.start = num2;
				return;
			}
			int num5 = count2 - index - count;
			num2 = this.end - num5;
			if (num2 < 0)
			{
				num2 += num;
			}
			num3 = num2 - count;
			if (num3 < 0)
			{
				num3 += num;
			}
			int j = num5;
			while (j > 0)
			{
				int num6 = j;
				if (num - num3 < num6)
				{
					num6 = num - num3;
				}
				if (num - num2 < num6)
				{
					num6 = num - num2;
				}
				Array.Copy(this.buffer, num2, this.buffer, num3, num6);
				j -= num6;
				if ((num3 += num6) >= num)
				{
					num3 -= num;
				}
				if ((num2 += num6) >= num)
				{
					num2 -= num;
				}
			}
			for (j = 0; j < count; j++)
			{
				if (--num2 < 0)
				{
					num2 += num;
				}
				this.buffer[num2] = default(T);
			}
			this.end = num2;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0008727C File Offset: 0x0008547C
		private void IncreaseBuffer()
		{
			int count = this.Count;
			int num = this.buffer.Length;
			T[] array = new T[num * 2];
			if (this.start >= this.end)
			{
				Array.Copy(this.buffer, this.start, array, 0, num - this.start);
				Array.Copy(this.buffer, 0, array, num - this.start, this.end);
				this.end = this.end + num - this.start;
				this.start = 0;
			}
			else
			{
				Array.Copy(this.buffer, this.start, array, 0, this.end - this.start);
				this.end -= this.start;
				this.start = 0;
			}
			this.buffer = array;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00087348 File Offset: 0x00085548
		public void AddToFront(T item)
		{
			this.StopEnumerations();
			if (this.buffer == null)
			{
				this.CreateInitialBuffer(item);
				return;
			}
			int num = this.start - 1;
			this.start = num;
			if (num < 0)
			{
				this.start += this.buffer.Length;
			}
			this.buffer[this.start] = item;
			if (this.start == this.end)
			{
				this.IncreaseBuffer();
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000873BA File Offset: 0x000855BA
		public void AddManyToFront(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.InsertRange(0, collection);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000873D4 File Offset: 0x000855D4
		public void AddToBack(T item)
		{
			this.StopEnumerations();
			if (this.buffer == null)
			{
				this.CreateInitialBuffer(item);
				return;
			}
			this.buffer[this.end] = item;
			int num = this.end + 1;
			this.end = num;
			if (num >= this.buffer.Length)
			{
				this.end -= this.buffer.Length;
			}
			if (this.start == this.end)
			{
				this.IncreaseBuffer();
			}
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0008744D File Offset: 0x0008564D
		public sealed override void Add(T item)
		{
			this.AddToBack(item);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00087458 File Offset: 0x00085658
		public void AddManyToBack(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (T t in collection)
			{
				this.AddToBack(t);
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000874B0 File Offset: 0x000856B0
		public T RemoveFromFront()
		{
			if (this.start == this.end)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
			this.StopEnumerations();
			T t = this.buffer[this.start];
			this.buffer[this.start] = default(T);
			int num = this.start + 1;
			this.start = num;
			if (num >= this.buffer.Length)
			{
				this.start -= this.buffer.Length;
			}
			return t;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00087538 File Offset: 0x00085738
		public T RemoveFromBack()
		{
			if (this.start == this.end)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
			this.StopEnumerations();
			int num = this.end - 1;
			this.end = num;
			if (num < 0)
			{
				this.end += this.buffer.Length;
			}
			T t = this.buffer[this.end];
			this.buffer[this.end] = default(T);
			return t;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000875B8 File Offset: 0x000857B8
		public T GetAtFront()
		{
			if (this.start == this.end)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
			return this.buffer[this.start];
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000875E4 File Offset: 0x000857E4
		public T GetAtBack()
		{
			if (this.start == this.end)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
			if (this.end == 0)
			{
				return this.buffer[this.buffer.Length - 1];
			}
			return this.buffer[this.end - 1];
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0008763B File Offset: 0x0008583B
		public Deque<T> Clone()
		{
			return new Deque<T>(this);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00087643 File Offset: 0x00085843
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0008764C File Offset: 0x0008584C
		public Deque<T> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(T), out flag))
			{
				throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, typeof(T).FullName));
			}
			Deque<T> deque = new Deque<T>();
			foreach (T t in this)
			{
				T t2;
				if (flag)
				{
					t2 = t;
				}
				else if (t == null)
				{
					t2 = default(T);
				}
				else
				{
					t2 = (T)((object)((ICloneable)((object)t)).Clone());
				}
				deque.AddToBack(t2);
			}
			return deque;
		}

		// Token: 0x04000EFA RID: 3834
		private const int INITIAL_SIZE = 8;

		// Token: 0x04000EFB RID: 3835
		private T[] buffer;

		// Token: 0x04000EFC RID: 3836
		private int start;

		// Token: 0x04000EFD RID: 3837
		private int end;

		// Token: 0x04000EFE RID: 3838
		private int changeStamp;
	}
}
