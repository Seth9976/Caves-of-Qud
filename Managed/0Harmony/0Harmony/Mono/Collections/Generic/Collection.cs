using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;

namespace Mono.Collections.Generic
{
	// Token: 0x020001AA RID: 426
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000182A0 File Offset: 0x000164A0
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x170001EA RID: 490
		public T this[int index]
		{
			get
			{
				if (index >= this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.items[index];
			}
			set
			{
				this.CheckIndex(index);
				if (index == this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.OnSet(value, index);
				this.items[index] = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x000182F2 File Offset: 0x000164F2
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x000182FC File Offset: 0x000164FC
		public int Capacity
		{
			get
			{
				return this.items.Length;
			}
			set
			{
				if (value < 0 || value < this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Resize(value);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00018318 File Offset: 0x00016518
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00018318 File Offset: 0x00016518
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00018318 File Offset: 0x00016518
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001EF RID: 495
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.CheckIndex(index);
				try
				{
					this[index] = (T)((object)value);
					return;
				}
				catch (InvalidCastException)
				{
				}
				catch (NullReferenceException)
				{
				}
				throw new ArgumentException();
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00018378 File Offset: 0x00016578
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00018318 File Offset: 0x00016518
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00018380 File Offset: 0x00016580
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00018383 File Offset: 0x00016583
		public Collection()
		{
			this.items = Empty<T>.Array;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00018396 File Offset: 0x00016596
		public Collection(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.items = ((capacity == 0) ? Empty<T>.Array : new T[capacity]);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000183C0 File Offset: 0x000165C0
		public Collection(ICollection<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.items = new T[items.Count];
			items.CopyTo(this.items, 0);
			this.size = this.items.Length;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00018410 File Offset: 0x00016610
		public void Add(T item)
		{
			if (this.size == this.items.Length)
			{
				this.Grow(1);
			}
			this.OnAdd(item, this.size);
			T[] array = this.items;
			int num = this.size;
			this.size = num + 1;
			array[num] = item;
			this.version++;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001846C File Offset: 0x0001666C
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001847B File Offset: 0x0001667B
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this.items, item, 0, this.size);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00018490 File Offset: 0x00016690
		public void Insert(int index, T item)
		{
			this.CheckIndex(index);
			if (this.size == this.items.Length)
			{
				this.Grow(1);
			}
			this.OnInsert(item, index);
			this.Shift(index, 1);
			this.items[index] = item;
			this.version++;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000184E8 File Offset: 0x000166E8
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
			T t = this.items[index];
			this.OnRemove(t, index);
			this.Shift(index, -1);
			this.version++;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00018534 File Offset: 0x00016734
		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num == -1)
			{
				return false;
			}
			this.OnRemove(item, num);
			this.Shift(num, -1);
			this.version++;
			return true;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001856E File Offset: 0x0001676E
		public void Clear()
		{
			this.OnClear();
			Array.Clear(this.items, 0, this.size);
			this.size = 0;
			this.version++;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001859D File Offset: 0x0001679D
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this.items, 0, array, arrayIndex, this.size);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000185B4 File Offset: 0x000167B4
		public T[] ToArray()
		{
			T[] array = new T[this.size];
			Array.Copy(this.items, 0, array, 0, this.size);
			return array;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000185E2 File Offset: 0x000167E2
		private void CheckIndex(int index)
		{
			if (index < 0 || index > this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000185F8 File Offset: 0x000167F8
		private void Shift(int start, int delta)
		{
			if (delta < 0)
			{
				start -= delta;
			}
			if (start < this.size)
			{
				Array.Copy(this.items, start, this.items, start + delta, this.size - start);
			}
			this.size += delta;
			if (delta < 0)
			{
				Array.Clear(this.items, this.size, -delta);
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00018659 File Offset: 0x00016859
		protected virtual void OnAdd(T item, int index)
		{
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00018659 File Offset: 0x00016859
		protected virtual void OnInsert(T item, int index)
		{
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00018659 File Offset: 0x00016859
		protected virtual void OnSet(T item, int index)
		{
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00018659 File Offset: 0x00016859
		protected virtual void OnRemove(T item, int index)
		{
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00018659 File Offset: 0x00016859
		protected virtual void OnClear()
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001865C File Offset: 0x0001685C
		internal virtual void Grow(int desired)
		{
			int num = this.size + desired;
			if (num <= this.items.Length)
			{
				return;
			}
			num = Math.Max(Math.Max(this.items.Length * 2, 4), num);
			this.Resize(num);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001869C File Offset: 0x0001689C
		protected void Resize(int new_size)
		{
			if (new_size == this.size)
			{
				return;
			}
			if (new_size < this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.items = this.items.Resize<T>(new_size);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000186CC File Offset: 0x000168CC
		int IList.Add(object value)
		{
			try
			{
				this.Add((T)((object)value));
				return this.size - 1;
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			throw new ArgumentException();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001871C File Offset: 0x0001691C
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00018724 File Offset: 0x00016924
		bool IList.Contains(object value)
		{
			return ((IList)this).IndexOf(value) > -1;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00018730 File Offset: 0x00016930
		int IList.IndexOf(object value)
		{
			try
			{
				return this.IndexOf((T)((object)value));
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			return -1;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00018774 File Offset: 0x00016974
		void IList.Insert(int index, object value)
		{
			this.CheckIndex(index);
			try
			{
				this.Insert(index, (T)((object)value));
				return;
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			throw new ArgumentException();
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000187C0 File Offset: 0x000169C0
		void IList.Remove(object value)
		{
			try
			{
				this.Remove((T)((object)value));
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00018800 File Offset: 0x00016A00
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001859D File Offset: 0x0001679D
		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(this.items, 0, array, index, this.size);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00018809 File Offset: 0x00016A09
		public Collection<T>.Enumerator GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00018811 File Offset: 0x00016A11
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00018811 File Offset: 0x00016A11
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x0400023A RID: 570
		internal T[] items;

		// Token: 0x0400023B RID: 571
		internal int size;

		// Token: 0x0400023C RID: 572
		private int version;

		// Token: 0x020001AB RID: 427
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001881E File Offset: 0x00016A1E
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06000727 RID: 1831 RVA: 0x00018826 File Offset: 0x00016A26
			object IEnumerator.Current
			{
				get
				{
					this.CheckState();
					if (this.next <= 0)
					{
						throw new InvalidOperationException();
					}
					return this.current;
				}
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x00018848 File Offset: 0x00016A48
			internal Enumerator(Collection<T> collection)
			{
				this = default(Collection<T>.Enumerator);
				this.collection = collection;
				this.version = collection.version;
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x00018864 File Offset: 0x00016A64
			public bool MoveNext()
			{
				this.CheckState();
				if (this.next < 0)
				{
					return false;
				}
				if (this.next < this.collection.size)
				{
					T[] items = this.collection.items;
					int num = this.next;
					this.next = num + 1;
					this.current = items[num];
					return true;
				}
				this.next = -1;
				return false;
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x000188C6 File Offset: 0x00016AC6
			public void Reset()
			{
				this.CheckState();
				this.next = 0;
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x000188D5 File Offset: 0x00016AD5
			private void CheckState()
			{
				if (this.collection == null)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				if (this.version != this.collection.version)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x00018913 File Offset: 0x00016B13
			public void Dispose()
			{
				this.collection = null;
			}

			// Token: 0x0400023D RID: 573
			private Collection<T> collection;

			// Token: 0x0400023E RID: 574
			private T current;

			// Token: 0x0400023F RID: 575
			private int next;

			// Token: 0x04000240 RID: 576
			private readonly int version;
		}
	}
}
