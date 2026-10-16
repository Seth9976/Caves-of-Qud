using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x02000307 RID: 775
	[Serializable]
	public class Set<T> : CollectionBase<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICloneable
	{
		// Token: 0x06001CAC RID: 7340 RVA: 0x0008D0C8 File Offset: 0x0008B2C8
		public Set()
			: this(EqualityComparer<T>.Default)
		{
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0008D0D5 File Offset: 0x0008B2D5
		public Set(IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			this.equalityComparer = equalityComparer;
			this.hash = new Hash<T>(equalityComparer);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0008D0FE File Offset: 0x0008B2FE
		public Set(IEnumerable<T> collection)
			: this(collection, EqualityComparer<T>.Default)
		{
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0008D10C File Offset: 0x0008B30C
		public Set(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer)
			: this(equalityComparer)
		{
			this.AddMany(collection);
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0008D11C File Offset: 0x0008B31C
		private Set(IEqualityComparer<T> equalityComparer, Hash<T> hash)
		{
			this.equalityComparer = equalityComparer;
			this.hash = hash;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0008D132 File Offset: 0x0008B332
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0008D13A File Offset: 0x0008B33A
		public Set<T> Clone()
		{
			return new Set<T>(this.equalityComparer, this.hash.Clone(null));
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0008D154 File Offset: 0x0008B354
		public Set<T> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(T), out flag))
			{
				throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, typeof(T).FullName));
			}
			Set<T> set = new Set<T>(this.equalityComparer);
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
				set.Add(t2);
			}
			return set;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x0008D210 File Offset: 0x0008B410
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this.equalityComparer;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x0008D218 File Offset: 0x0008B418
		public sealed override int Count
		{
			get
			{
				return this.hash.ElementCount;
			}
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0008D225 File Offset: 0x0008B425
		public sealed override IEnumerator<T> GetEnumerator()
		{
			return this.hash.GetEnumerator();
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0008D234 File Offset: 0x0008B434
		public sealed override bool Contains(T item)
		{
			T t;
			return this.hash.Find(item, false, out t);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0008D250 File Offset: 0x0008B450
		public bool TryGetItem(T item, out T foundItem)
		{
			return this.hash.Find(item, false, out foundItem);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0008D260 File Offset: 0x0008B460
		public new bool Add(T item)
		{
			T t;
			return !this.hash.Insert(item, true, out t);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0008D27F File Offset: 0x0008B47F
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0008D28C File Offset: 0x0008B48C
		public void AddMany(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (collection == this)
			{
				return;
			}
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0008D2E8 File Offset: 0x0008B4E8
		public sealed override bool Remove(T item)
		{
			T t;
			return this.hash.Delete(item, out t);
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0008D304 File Offset: 0x0008B504
		public int RemoveMany(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			int num = 0;
			if (collection == this)
			{
				num = this.Count;
				this.Clear();
			}
			else
			{
				foreach (T t in collection)
				{
					if (this.Remove(t))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0008D378 File Offset: 0x0008B578
		public sealed override void Clear()
		{
			this.hash.StopEnumerations();
			this.hash = new Hash<T>(this.equalityComparer);
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0008D396 File Offset: 0x0008B596
		private void CheckConsistentComparison(Set<T> otherSet)
		{
			if (otherSet == null)
			{
				throw new ArgumentNullException("otherSet");
			}
			if (!object.Equals(this.equalityComparer, otherSet.equalityComparer))
			{
				throw new InvalidOperationException(Strings.InconsistentComparisons);
			}
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x0008D3C4 File Offset: 0x0008B5C4
		public bool IsSupersetOf(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			if (otherSet.Count > this.Count)
			{
				return false;
			}
			foreach (T t in otherSet)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0008D42C File Offset: 0x0008B62C
		public bool IsProperSupersetOf(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			return otherSet.Count < this.Count && this.IsSupersetOf(otherSet);
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0008D44C File Offset: 0x0008B64C
		public bool IsSubsetOf(Set<T> otherSet)
		{
			return otherSet.IsSupersetOf(this);
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0008D455 File Offset: 0x0008B655
		public bool IsProperSubsetOf(Set<T> otherSet)
		{
			return otherSet.IsProperSupersetOf(this);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0008D460 File Offset: 0x0008B660
		public bool IsEqualTo(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			if (otherSet.Count != this.Count)
			{
				return false;
			}
			foreach (T t in otherSet)
			{
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0008D4C8 File Offset: 0x0008B6C8
		public bool IsDisjointFrom(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			Set<T> set;
			Set<T> set2;
			if (otherSet.Count > this.Count)
			{
				set = this;
				set2 = otherSet;
			}
			else
			{
				set = otherSet;
				set2 = this;
			}
			foreach (T t in set)
			{
				if (set2.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0008D53C File Offset: 0x0008B73C
		public void UnionWith(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			this.AddMany(otherSet);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0008D54C File Offset: 0x0008B74C
		public Set<T> Union(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			Set<T> set;
			Set<T> set2;
			if (otherSet.Count > this.Count)
			{
				set = this;
				set2 = otherSet;
			}
			else
			{
				set = otherSet;
				set2 = this;
			}
			Set<T> set3 = set2.Clone();
			set3.AddMany(set);
			return set3;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0008D588 File Offset: 0x0008B788
		public void IntersectionWith(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			this.hash.StopEnumerations();
			Set<T> set;
			Set<T> set2;
			if (otherSet.Count > this.Count)
			{
				set = this;
				set2 = otherSet;
			}
			else
			{
				set = otherSet;
				set2 = this;
			}
			Hash<T> hash = new Hash<T>(this.equalityComparer);
			foreach (T t in set)
			{
				if (set2.Contains(t))
				{
					T t2;
					hash.Insert(t, true, out t2);
				}
			}
			this.hash = hash;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0008D624 File Offset: 0x0008B824
		public Set<T> Intersection(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			Set<T> set;
			Set<T> set2;
			if (otherSet.Count > this.Count)
			{
				set = this;
				set2 = otherSet;
			}
			else
			{
				set = otherSet;
				set2 = this;
			}
			Set<T> set3 = new Set<T>(this.equalityComparer);
			foreach (T t in set)
			{
				if (set2.Contains(t))
				{
					set3.Add(t);
				}
			}
			return set3;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0008D6A8 File Offset: 0x0008B8A8
		public void DifferenceWith(Set<T> otherSet)
		{
			if (this == otherSet)
			{
				this.Clear();
			}
			this.CheckConsistentComparison(otherSet);
			if (otherSet.Count < this.Count)
			{
				using (IEnumerator<T> enumerator = otherSet.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						this.Remove(t);
					}
					return;
				}
			}
			this.RemoveAll((T item) => otherSet.Contains(item));
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0008D744 File Offset: 0x0008B944
		public Set<T> Difference(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			Set<T> set = this.Clone();
			set.DifferenceWith(otherSet);
			return set;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0008D75C File Offset: 0x0008B95C
		public void SymmetricDifferenceWith(Set<T> otherSet)
		{
			if (this == otherSet)
			{
				this.Clear();
			}
			this.CheckConsistentComparison(otherSet);
			if (otherSet.Count > this.Count)
			{
				this.hash.StopEnumerations();
				Hash<T> hash = otherSet.hash.Clone(null);
				foreach (T t in this)
				{
					T t2;
					if (hash.Find(t, false, out t2))
					{
						hash.Delete(t, out t2);
					}
					else
					{
						hash.Insert(t, true, out t2);
					}
				}
				this.hash = hash;
				return;
			}
			foreach (T t3 in otherSet)
			{
				if (this.Contains(t3))
				{
					this.Remove(t3);
				}
				else
				{
					this.Add(t3);
				}
			}
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0008D850 File Offset: 0x0008BA50
		public Set<T> SymmetricDifference(Set<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			Set<T> set;
			Set<T> set2;
			if (otherSet.Count > this.Count)
			{
				set = this;
				set2 = otherSet;
			}
			else
			{
				set = otherSet;
				set2 = this;
			}
			Set<T> set3 = set2.Clone();
			foreach (T t in set)
			{
				if (set3.Contains(t))
				{
					set3.Remove(t);
				}
				else
				{
					set3.Add(t);
				}
			}
			return set3;
		}

		// Token: 0x04000F31 RID: 3889
		private IEqualityComparer<T> equalityComparer;

		// Token: 0x04000F32 RID: 3890
		private Hash<T> hash;
	}
}
