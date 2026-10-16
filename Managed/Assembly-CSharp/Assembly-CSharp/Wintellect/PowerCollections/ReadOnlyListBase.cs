using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000303 RID: 771
	[Serializable]
	public abstract class ReadOnlyListBase<T> : ReadOnlyCollectionBase<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		// Token: 0x06001C4D RID: 7245 RVA: 0x0008BD54 File Offset: 0x00089F54
		private void MethodModifiesCollection()
		{
			throw new NotSupportedException(string.Format(Strings.CannotModifyCollection, Util.SimpleClassName(base.GetType())));
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001C4E RID: 7246
		public abstract override int Count { get; }

		// Token: 0x170003C5 RID: 965
		public virtual T this[int index]
		{
			get
			{
				throw new NotImplementedException(Strings.MustOverrideIndexerGet);
			}
			set
			{
				this.MethodModifiesCollection();
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x0008BD84 File Offset: 0x00089F84
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

		// Token: 0x06001C52 RID: 7250 RVA: 0x0008BD93 File Offset: 0x00089F93
		public override bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x0008BDA2 File Offset: 0x00089FA2
		public virtual void CopyTo(T[] array)
		{
			this.CopyTo(array, 0);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x0008BDAC File Offset: 0x00089FAC
		public override void CopyTo(T[] array, int arrayIndex)
		{
			base.CopyTo(array, arrayIndex);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0008BDB6 File Offset: 0x00089FB6
		public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			this.Range(index, count).CopyTo(array, arrayIndex);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x0008BDC8 File Offset: 0x00089FC8
		public virtual T Find(Predicate<T> predicate)
		{
			return Algorithms.FindFirstWhere<T>(this, predicate);
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x0008BDD1 File Offset: 0x00089FD1
		public virtual bool TryFind(Predicate<T> predicate, out T foundItem)
		{
			return Algorithms.TryFindFirstWhere<T>(this, predicate, out foundItem);
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x0008BDDB File Offset: 0x00089FDB
		public virtual T FindLast(Predicate<T> predicate)
		{
			return Algorithms.FindLastWhere<T>(this, predicate);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0008BDE4 File Offset: 0x00089FE4
		public virtual bool TryFindLast(Predicate<T> predicate, out T foundItem)
		{
			return Algorithms.TryFindLastWhere<T>(this, predicate, out foundItem);
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0008BDEE File Offset: 0x00089FEE
		public virtual int FindIndex(Predicate<T> predicate)
		{
			return Algorithms.FindFirstIndexWhere<T>(this, predicate);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0008BDF8 File Offset: 0x00089FF8
		public virtual int FindIndex(int index, Predicate<T> predicate)
		{
			int num = Algorithms.FindFirstIndexWhere<T>(this.Range(index, this.Count - index), predicate);
			if (num < 0)
			{
				return -1;
			}
			return num + index;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0008BE24 File Offset: 0x0008A024
		public virtual int FindIndex(int index, int count, Predicate<T> predicate)
		{
			int num = Algorithms.FindFirstIndexWhere<T>(this.Range(index, count), predicate);
			if (num < 0)
			{
				return -1;
			}
			return num + index;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0008BE49 File Offset: 0x0008A049
		public virtual int FindLastIndex(Predicate<T> predicate)
		{
			return Algorithms.FindLastIndexWhere<T>(this, predicate);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0008BE52 File Offset: 0x0008A052
		public virtual int FindLastIndex(int index, Predicate<T> predicate)
		{
			return Algorithms.FindLastIndexWhere<T>(this.Range(0, index + 1), predicate);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0008BE64 File Offset: 0x0008A064
		public virtual int FindLastIndex(int index, int count, Predicate<T> predicate)
		{
			int num = Algorithms.FindLastIndexWhere<T>(this.Range(index - count + 1, count), predicate);
			if (num >= 0)
			{
				return num + index - count + 1;
			}
			return -1;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0008BE91 File Offset: 0x0008A091
		public virtual int IndexOf(T item)
		{
			return Algorithms.FirstIndexOf<T>(this, item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0008BEA0 File Offset: 0x0008A0A0
		public virtual int IndexOf(T item, int index)
		{
			int num = Algorithms.FirstIndexOf<T>(this.Range(index, this.Count - index), item, EqualityComparer<T>.Default);
			if (num >= 0)
			{
				return num + index;
			}
			return -1;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0008BED4 File Offset: 0x0008A0D4
		public virtual int IndexOf(T item, int index, int count)
		{
			int num = Algorithms.FirstIndexOf<T>(this.Range(index, count), item, EqualityComparer<T>.Default);
			if (num >= 0)
			{
				return num + index;
			}
			return -1;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x0008BEFE File Offset: 0x0008A0FE
		public virtual int LastIndexOf(T item)
		{
			return Algorithms.LastIndexOf<T>(this, item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0008BF0C File Offset: 0x0008A10C
		public virtual int LastIndexOf(T item, int index)
		{
			return Algorithms.LastIndexOf<T>(this.Range(0, index + 1), item, EqualityComparer<T>.Default);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0008BF24 File Offset: 0x0008A124
		public virtual int LastIndexOf(T item, int index, int count)
		{
			int num = Algorithms.LastIndexOf<T>(this.Range(index - count + 1, count), item, EqualityComparer<T>.Default);
			if (num >= 0)
			{
				return num + index - count + 1;
			}
			return -1;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0008BF56 File Offset: 0x0008A156
		public virtual IList<T> Range(int start, int count)
		{
			return Algorithms.Range<T>(this, start, count);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0008BF60 File Offset: 0x0008A160
		void IList<T>.Insert(int index, T item)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0008BF68 File Offset: 0x0008A168
		void IList<T>.RemoveAt(int index)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0008BF70 File Offset: 0x0008A170
		int IList.Add(object value)
		{
			this.MethodModifiesCollection();
			return -1;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0008BF79 File Offset: 0x0008A179
		void IList.Clear()
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0008BF81 File Offset: 0x0008A181
		bool IList.Contains(object value)
		{
			return (value is T || value == null) && this.Contains((T)((object)value));
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0008BF9C File Offset: 0x0008A19C
		int IList.IndexOf(object value)
		{
			if (value is T || value == null)
			{
				return this.IndexOf((T)((object)value));
			}
			return -1;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0008BFB7 File Offset: 0x0008A1B7
		void IList.Insert(int index, object value)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0008BFBF File Offset: 0x0008A1BF
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x0008BFC2 File Offset: 0x0008A1C2
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0008BFC5 File Offset: 0x0008A1C5
		void IList.Remove(object value)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0008BFCD File Offset: 0x0008A1CD
		void IList.RemoveAt(int index)
		{
			this.MethodModifiesCollection();
		}

		// Token: 0x170003C8 RID: 968
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.MethodModifiesCollection();
			}
		}
	}
}
