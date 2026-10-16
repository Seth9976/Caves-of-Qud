using System;
using System.Collections;
using System.Collections.Generic;
using Trivial.Mono.Cecil;

namespace Trivial.Mono.Collections.Generic
{
	// Token: 0x02000009 RID: 9
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002577 File Offset: 0x00000777
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000002 RID: 2
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000025C9 File Offset: 0x000007C9
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000025D3 File Offset: 0x000007D3
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000025EF File Offset: 0x000007EF
		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000025EF File Offset: 0x000007EF
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000025EF File Offset: 0x000007EF
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025F2 File Offset: 0x000007F2
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002600 File Offset: 0x00000800
		object IList.Item
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000264C File Offset: 0x0000084C
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025EF File Offset: 0x000007EF
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002654 File Offset: 0x00000854
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002657 File Offset: 0x00000857
		public Collection()
		{
			this.items = Empty<T>.Array;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000266A File Offset: 0x0000086A
		public Collection(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			this.items = new T[capacity];
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002688 File Offset: 0x00000888
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

		// Token: 0x06000025 RID: 37 RVA: 0x000026D8 File Offset: 0x000008D8
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

		// Token: 0x06000026 RID: 38 RVA: 0x00002734 File Offset: 0x00000934
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002743 File Offset: 0x00000943
		public int IndexOf(T item)
		{
			return Array.IndexOf<T>(this.items, item, 0, this.size);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002758 File Offset: 0x00000958
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

		// Token: 0x06000029 RID: 41 RVA: 0x000027B0 File Offset: 0x000009B0
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

		// Token: 0x0600002A RID: 42 RVA: 0x000027FC File Offset: 0x000009FC
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

		// Token: 0x0600002B RID: 43 RVA: 0x00002836 File Offset: 0x00000A36
		public void Clear()
		{
			this.OnClear();
			Array.Clear(this.items, 0, this.size);
			this.size = 0;
			this.version++;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002865 File Offset: 0x00000A65
		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(this.items, 0, array, arrayIndex, this.size);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000287C File Offset: 0x00000A7C
		public T[] ToArray()
		{
			T[] array = new T[this.size];
			Array.Copy(this.items, 0, array, 0, this.size);
			return array;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028AA File Offset: 0x00000AAA
		private void CheckIndex(int index)
		{
			if (index < 0 || index > this.size)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028C0 File Offset: 0x00000AC0
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

		// Token: 0x06000030 RID: 48 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual void OnAdd(T item, int index)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual void OnInsert(T item, int index)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual void OnSet(T item, int index)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual void OnRemove(T item, int index)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual void OnClear()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002924 File Offset: 0x00000B24
		internal virtual void Grow(int desired)
		{
			int num = this.size + desired;
			if (num > this.items.Length)
			{
				num = Math.Max(Math.Max(this.items.Length * 2, 4), num);
				this.Resize(num);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002963 File Offset: 0x00000B63
		protected void Resize(int new_size)
		{
			if (new_size != this.size)
			{
				if (new_size < this.size)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.items = this.items.Resize<T>(new_size);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002990 File Offset: 0x00000B90
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

		// Token: 0x06000038 RID: 56 RVA: 0x000029E0 File Offset: 0x00000BE0
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029E8 File Offset: 0x00000BE8
		bool IList.Contains(object value)
		{
			return this.IndexOf(value) > -1;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029F4 File Offset: 0x00000BF4
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

		// Token: 0x0600003B RID: 59 RVA: 0x00002A38 File Offset: 0x00000C38
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

		// Token: 0x0600003C RID: 60 RVA: 0x00002A84 File Offset: 0x00000C84
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

		// Token: 0x0600003D RID: 61 RVA: 0x00002AC4 File Offset: 0x00000CC4
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002865 File Offset: 0x00000A65
		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(this.items, 0, array, index, this.size);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002ACD File Offset: 0x00000CCD
		public Collection<T>.Enumerator GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AD5 File Offset: 0x00000CD5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AD5 File Offset: 0x00000CD5
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Collection<T>.Enumerator(this);
		}

		// Token: 0x04000006 RID: 6
		internal T[] items;

		// Token: 0x04000007 RID: 7
		internal int size;

		// Token: 0x04000008 RID: 8
		private int version;

		// Token: 0x02000138 RID: 312
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x1700031F RID: 799
			// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00025DE0 File Offset: 0x00023FE0
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000320 RID: 800
			// (get) Token: 0x06000B7D RID: 2941 RVA: 0x00025DE8 File Offset: 0x00023FE8
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

			// Token: 0x06000B7E RID: 2942 RVA: 0x00025E0A File Offset: 0x0002400A
			internal Enumerator(Collection<T> collection)
			{
				this = default(Collection<T>.Enumerator);
				this.collection = collection;
				this.version = collection.version;
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x00025E28 File Offset: 0x00024028
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

			// Token: 0x06000B80 RID: 2944 RVA: 0x00025E8A File Offset: 0x0002408A
			public void Reset()
			{
				this.CheckState();
				this.next = 0;
			}

			// Token: 0x06000B81 RID: 2945 RVA: 0x00025E99 File Offset: 0x00024099
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

			// Token: 0x06000B82 RID: 2946 RVA: 0x00025ED7 File Offset: 0x000240D7
			public void Dispose()
			{
				this.collection = null;
			}

			// Token: 0x040006E7 RID: 1767
			private Collection<T> collection;

			// Token: 0x040006E8 RID: 1768
			private T current;

			// Token: 0x040006E9 RID: 1769
			private int next;

			// Token: 0x040006EA RID: 1770
			private readonly int version;
		}
	}
}
