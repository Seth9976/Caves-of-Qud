using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F9 RID: 761
	[Serializable]
	public abstract class ListBase<T> : CollectionBase<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001B01 RID: 6913
		public abstract override int Count { get; }

		// Token: 0x06001B02 RID: 6914
		public abstract override void Clear();

		// Token: 0x1700039D RID: 925
		public abstract T this[int index] { get; set; }

		// Token: 0x06001B05 RID: 6917
		public abstract void Insert(int index, T item);

		// Token: 0x06001B06 RID: 6918
		public abstract void RemoveAt(int index);

		// Token: 0x06001B07 RID: 6919 RVA: 0x0008843E File Offset: 0x0008663E
		public override IEnumerator<T> GetEnumerator()
		{
			int count = this.Count;
			int num;
			for (int i = 0; i < count; i = num)
			{
				yield return this[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0008844D File Offset: 0x0008664D
		public override bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0008845C File Offset: 0x0008665C
		public override void Add(T item)
		{
			this.Insert(this.Count, item);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0008846C File Offset: 0x0008666C
		public override bool Remove(T item)
		{
			int num = this.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0008848F File Offset: 0x0008668F
		public virtual void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00088499 File Offset: 0x00086699
		public override void CopyTo(T[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000884A3 File Offset: 0x000866A3
		public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this.Range(index, count).CopyTo(array, arrayIndex);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000884B5 File Offset: 0x000866B5
		public new virtual IList<T> AsReadOnly()
		{
			return Algorithms.ReadOnly<T>(this);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000884BD File Offset: 0x000866BD
		public virtual T Find(Predicate<T> predicate)
		{
			return Algorithms.FindFirstWhere<T>(this, predicate);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000884C6 File Offset: 0x000866C6
		public virtual bool TryFind(Predicate<T> predicate, out T foundItem)
		{
			return Algorithms.TryFindFirstWhere<T>(this, predicate, out foundItem);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000884D0 File Offset: 0x000866D0
		public virtual T FindLast(Predicate<T> predicate)
		{
			return Algorithms.FindLastWhere<T>(this, predicate);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000884D9 File Offset: 0x000866D9
		public virtual bool TryFindLast(Predicate<T> predicate, out T foundItem)
		{
			return Algorithms.TryFindLastWhere<T>(this, predicate, out foundItem);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000884E3 File Offset: 0x000866E3
		public virtual int FindIndex(Predicate<T> predicate)
		{
			return Algorithms.FindFirstIndexWhere<T>(this, predicate);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000884EC File Offset: 0x000866EC
		public virtual int FindIndex(int index, Predicate<T> predicate)
		{
			int num = Algorithms.FindFirstIndexWhere<T>(this.Range(index, this.Count - index), predicate);
			if (num < 0)
			{
				return -1;
			}
			return num + index;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00088518 File Offset: 0x00086718
		public virtual int FindIndex(int index, int count, Predicate<T> predicate)
		{
			int num = Algorithms.FindFirstIndexWhere<T>(this.Range(index, count), predicate);
			if (num < 0)
			{
				return -1;
			}
			return num + index;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0008853D File Offset: 0x0008673D
		public virtual int FindLastIndex(Predicate<T> predicate)
		{
			return Algorithms.FindLastIndexWhere<T>(this, predicate);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00088546 File Offset: 0x00086746
		public virtual int FindLastIndex(int index, Predicate<T> predicate)
		{
			return Algorithms.FindLastIndexWhere<T>(this.Range(0, index + 1), predicate);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00088558 File Offset: 0x00086758
		public virtual int FindLastIndex(int index, int count, Predicate<T> predicate)
		{
			int num = Algorithms.FindLastIndexWhere<T>(this.Range(index - count + 1, count), predicate);
			if (num >= 0)
			{
				return num + index - count + 1;
			}
			return -1;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00088585 File Offset: 0x00086785
		public virtual int IndexOf(T item)
		{
			return Algorithms.FirstIndexOf<T>(this, item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00088594 File Offset: 0x00086794
		public virtual int IndexOf(T item, int index)
		{
			int num = Algorithms.FirstIndexOf<T>(this.Range(index, this.Count - index), item, EqualityComparer<T>.Default);
			if (num >= 0)
			{
				return num + index;
			}
			return -1;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000885C8 File Offset: 0x000867C8
		public virtual int IndexOf(T item, int index, int count)
		{
			int num = Algorithms.FirstIndexOf<T>(this.Range(index, count), item, EqualityComparer<T>.Default);
			if (num >= 0)
			{
				return num + index;
			}
			return -1;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000885F2 File Offset: 0x000867F2
		public virtual int LastIndexOf(T item)
		{
			return Algorithms.LastIndexOf<T>(this, item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00088600 File Offset: 0x00086800
		public virtual int LastIndexOf(T item, int index)
		{
			return Algorithms.LastIndexOf<T>(this.Range(0, index + 1), item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00088618 File Offset: 0x00086818
		public virtual int LastIndexOf(T item, int index, int count)
		{
			int num = Algorithms.LastIndexOf<T>(this.Range(index - count + 1, count), item, EqualityComparer<T>.Default);
			if (num >= 0)
			{
				return num + index - count + 1;
			}
			return -1;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0008864A File Offset: 0x0008684A
		public virtual IList<T> Range(int start, int count)
		{
			return Algorithms.Range<T>(this, start, count);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00088654 File Offset: 0x00086854
		private T ConvertToItemType(string name, object value)
		{
			T t;
			try
			{
				t = (T)((object)value);
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(Strings.WrongType, value, typeof(T)), name);
			}
			return t;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00088698 File Offset: 0x00086898
		int IList.Add(object value)
		{
			int count = this.Count;
			this.Insert(count, this.ConvertToItemType("value", value));
			return count;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000886C0 File Offset: 0x000868C0
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000886C8 File Offset: 0x000868C8
		bool IList.Contains(object value)
		{
			return (value is T || value == null) && this.Contains((T)((object)value));
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000886E3 File Offset: 0x000868E3
		int IList.IndexOf(object value)
		{
			if (value is T || value == null)
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000886FE File Offset: 0x000868FE
		void IList.Insert(int index, object value)
		{
			this.Insert(index, this.ConvertToItemType("value", value));
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x00088713 File Offset: 0x00086913
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00088716 File Offset: 0x00086916
		bool IList.IsReadOnly
		{
			get
			{
				return ((ICollection<T>)this).IsReadOnly;
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0008871E File Offset: 0x0008691E
		void IList.Remove(object value)
		{
			if (value is T || value == null)
			{
				this.Remove((T)((object)value));
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00088738 File Offset: 0x00086938
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170003A0 RID: 928
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = this.ConvertToItemType("value", value);
			}
		}
	}
}
