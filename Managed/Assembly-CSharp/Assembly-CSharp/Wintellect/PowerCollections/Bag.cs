using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public class Bag<T> : CollectionBase<T>, ICloneable
	{
		// Token: 0x06001A33 RID: 6707 RVA: 0x0008492D File Offset: 0x00082B2D
		private static KeyValuePair<T, int> NewPair(T item, int count)
		{
			return new KeyValuePair<T, int>(item, count);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00084936 File Offset: 0x00082B36
		private static KeyValuePair<T, int> NewPair(T item)
		{
			return new KeyValuePair<T, int>(item, 0);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0008493F File Offset: 0x00082B3F
		public Bag()
			: this(EqualityComparer<T>.Default)
		{
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0008494C File Offset: 0x00082B4C
		public Bag(IEqualityComparer<T> equalityComparer)
		{
			if (equalityComparer == null)
			{
				throw new ArgumentNullException("equalityComparer");
			}
			this.keyEqualityComparer = equalityComparer;
			this.equalityComparer = Comparers.EqualityComparerKeyValueFromComparerKey<T, int>(equalityComparer);
			this.hash = new Hash<KeyValuePair<T, int>>(this.equalityComparer);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00084986 File Offset: 0x00082B86
		public Bag(IEnumerable<T> collection)
			: this(collection, EqualityComparer<T>.Default)
		{
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00084994 File Offset: 0x00082B94
		public Bag(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer)
			: this(equalityComparer)
		{
			this.AddMany(collection);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x000849A4 File Offset: 0x00082BA4
		private Bag(IEqualityComparer<KeyValuePair<T, int>> equalityComparer, IEqualityComparer<T> keyEqualityComparer, Hash<KeyValuePair<T, int>> hash, int count)
		{
			this.equalityComparer = equalityComparer;
			this.keyEqualityComparer = keyEqualityComparer;
			this.hash = hash;
			this.count = count;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000849C9 File Offset: 0x00082BC9
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000849D1 File Offset: 0x00082BD1
		public Bag<T> Clone()
		{
			return new Bag<T>(this.equalityComparer, this.keyEqualityComparer, this.hash.Clone(null), this.count);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000849F8 File Offset: 0x00082BF8
		public Bag<T> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(T), out flag))
			{
				throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, typeof(T).FullName));
			}
			Hash<KeyValuePair<T, int>> hash = new Hash<KeyValuePair<T, int>>(this.equalityComparer);
			foreach (KeyValuePair<T, int> keyValuePair in this.hash)
			{
				T t;
				if (!flag && keyValuePair.Key != null)
				{
					t = (T)((object)((ICloneable)((object)keyValuePair.Key)).Clone());
				}
				else
				{
					t = keyValuePair.Key;
				}
				KeyValuePair<T, int> keyValuePair2 = Bag<T>.NewPair(t, keyValuePair.Value);
				KeyValuePair<T, int> keyValuePair3;
				hash.Insert(keyValuePair2, true, out keyValuePair3);
			}
			return new Bag<T>(this.equalityComparer, this.keyEqualityComparer, hash, this.count);
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x00084AEC File Offset: 0x00082CEC
		public IEqualityComparer<T> Comparer
		{
			get
			{
				return this.keyEqualityComparer;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x00084AF4 File Offset: 0x00082CF4
		public sealed override int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x00084AFC File Offset: 0x00082CFC
		public int NumberOfCopies(T item)
		{
			KeyValuePair<T, int> keyValuePair;
			if (this.hash.Find(Bag<T>.NewPair(item), false, out keyValuePair))
			{
				return keyValuePair.Value;
			}
			return 0;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00084B28 File Offset: 0x00082D28
		public int GetRepresentativeItem(T item, out T representative)
		{
			KeyValuePair<T, int> keyValuePair;
			if (this.hash.Find(Bag<T>.NewPair(item), false, out keyValuePair))
			{
				representative = keyValuePair.Key;
				return keyValuePair.Value;
			}
			representative = item;
			return 0;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00084B68 File Offset: 0x00082D68
		public sealed override IEnumerator<T> GetEnumerator()
		{
			foreach (KeyValuePair<T, int> pair in this.hash)
			{
				int num;
				for (int i = 0; i < pair.Value; i = num)
				{
					yield return pair.Key;
					num = i + 1;
				}
				pair = default(KeyValuePair<T, int>);
			}
			IEnumerator<KeyValuePair<T, int>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00084B78 File Offset: 0x00082D78
		public sealed override bool Contains(T item)
		{
			KeyValuePair<T, int> keyValuePair;
			return this.hash.Find(Bag<T>.NewPair(item), false, out keyValuePair);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00084B99 File Offset: 0x00082D99
		public IEnumerable<T> DistinctItems()
		{
			foreach (KeyValuePair<T, int> keyValuePair in this.hash)
			{
				yield return keyValuePair.Key;
			}
			IEnumerator<KeyValuePair<T, int>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00084BAC File Offset: 0x00082DAC
		public sealed override void Add(T item)
		{
			KeyValuePair<T, int> keyValuePair = Bag<T>.NewPair(item, 1);
			KeyValuePair<T, int> keyValuePair2;
			if (!this.hash.Insert(keyValuePair, false, out keyValuePair2))
			{
				KeyValuePair<T, int> keyValuePair3 = Bag<T>.NewPair(keyValuePair2.Key, keyValuePair2.Value + 1);
				this.hash.Insert(keyValuePair3, true, out keyValuePair);
			}
			this.count++;
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00084C08 File Offset: 0x00082E08
		public void AddRepresentative(T item)
		{
			KeyValuePair<T, int> keyValuePair = Bag<T>.NewPair(item, 1);
			KeyValuePair<T, int> keyValuePair2;
			if (!this.hash.Insert(keyValuePair, false, out keyValuePair2))
			{
				KeyValuePair<T, int> keyValuePair3 = Bag<T>.NewPair(keyValuePair.Key, keyValuePair2.Value + 1);
				this.hash.Insert(keyValuePair3, true, out keyValuePair);
			}
			this.count++;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00084C64 File Offset: 0x00082E64
		public void ChangeNumberOfCopies(T item, int numCopies)
		{
			if (numCopies == 0)
			{
				this.RemoveAllCopies(item);
				return;
			}
			KeyValuePair<T, int> keyValuePair;
			KeyValuePair<T, int> keyValuePair2;
			if (this.hash.Find(Bag<T>.NewPair(item), false, out keyValuePair))
			{
				this.count += numCopies - keyValuePair.Value;
				keyValuePair2 = Bag<T>.NewPair(keyValuePair.Key, numCopies);
			}
			else
			{
				this.count += numCopies;
				keyValuePair2 = Bag<T>.NewPair(item, numCopies);
			}
			KeyValuePair<T, int> keyValuePair3;
			this.hash.Insert(keyValuePair2, true, out keyValuePair3);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00084CE0 File Offset: 0x00082EE0
		public void AddMany(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (this == collection)
			{
				collection = this.ToArray();
			}
			foreach (T t in collection)
			{
				this.Add(t);
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00084D44 File Offset: 0x00082F44
		public sealed override bool Remove(T item)
		{
			KeyValuePair<T, int> keyValuePair;
			if (this.hash.Delete(Bag<T>.NewPair(item), out keyValuePair))
			{
				if (keyValuePair.Value > 1)
				{
					KeyValuePair<T, int> keyValuePair2 = Bag<T>.NewPair(keyValuePair.Key, keyValuePair.Value - 1);
					KeyValuePair<T, int> keyValuePair3;
					this.hash.Insert(keyValuePair2, true, out keyValuePair3);
				}
				this.count--;
				return true;
			}
			return false;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00084DA8 File Offset: 0x00082FA8
		public int RemoveAllCopies(T item)
		{
			KeyValuePair<T, int> keyValuePair;
			if (this.hash.Delete(Bag<T>.NewPair(item), out keyValuePair))
			{
				this.count -= keyValuePair.Value;
				return keyValuePair.Value;
			}
			return 0;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x00084DE8 File Offset: 0x00082FE8
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

		// Token: 0x06001A4B RID: 6731 RVA: 0x00084E5C File Offset: 0x0008305C
		public sealed override void Clear()
		{
			this.hash.StopEnumerations();
			this.hash = new Hash<KeyValuePair<T, int>>(this.equalityComparer);
			this.count = 0;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00084E81 File Offset: 0x00083081
		private void CheckConsistentComparison(Bag<T> otherBag)
		{
			if (otherBag == null)
			{
				throw new ArgumentNullException("otherBag");
			}
			if (!object.Equals(this.equalityComparer, otherBag.equalityComparer))
			{
				throw new InvalidOperationException(Strings.InconsistentComparisons);
			}
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00084EB0 File Offset: 0x000830B0
		public bool IsEqualTo(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (otherBag.Count != this.Count)
			{
				return false;
			}
			foreach (T t in otherBag.DistinctItems())
			{
				if (this.NumberOfCopies(t) != otherBag.NumberOfCopies(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00084F24 File Offset: 0x00083124
		public bool IsSupersetOf(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (otherBag.Count > this.Count)
			{
				return false;
			}
			foreach (T t in otherBag.DistinctItems())
			{
				if (this.NumberOfCopies(t) < otherBag.NumberOfCopies(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00084F98 File Offset: 0x00083198
		public bool IsProperSupersetOf(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			return otherBag.Count < this.Count && this.IsSupersetOf(otherBag);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00084FB8 File Offset: 0x000831B8
		public bool IsSubsetOf(Bag<T> otherBag)
		{
			return otherBag.IsSupersetOf(this);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00084FC1 File Offset: 0x000831C1
		public bool IsProperSubsetOf(Bag<T> otherBag)
		{
			return otherBag.IsProperSupersetOf(this);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00084FCC File Offset: 0x000831CC
		public bool IsDisjointFrom(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			Bag<T> bag;
			Bag<T> bag2;
			if (otherBag.Count > this.Count)
			{
				bag = this;
				bag2 = otherBag;
			}
			else
			{
				bag = otherBag;
				bag2 = this;
			}
			foreach (T t in bag.DistinctItems())
			{
				if (bag2.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00085044 File Offset: 0x00083244
		public void UnionWith(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (otherBag == this)
			{
				return;
			}
			foreach (T t in otherBag.DistinctItems())
			{
				int num = this.NumberOfCopies(t);
				int num2 = otherBag.NumberOfCopies(t);
				if (num2 > num)
				{
					this.ChangeNumberOfCopies(t, num2);
				}
			}
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000850B4 File Offset: 0x000832B4
		public Bag<T> Union(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			Bag<T> bag;
			Bag<T> bag2;
			if (otherBag.Count > this.Count)
			{
				bag = this;
				bag2 = otherBag;
			}
			else
			{
				bag = otherBag;
				bag2 = this;
			}
			Bag<T> bag3 = bag2.Clone();
			bag3.UnionWith(bag);
			return bag3;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000850F0 File Offset: 0x000832F0
		public void SumWith(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (this == otherBag)
			{
				this.AddMany(otherBag);
				return;
			}
			foreach (T t in otherBag.DistinctItems())
			{
				int num = this.NumberOfCopies(t);
				int num2 = otherBag.NumberOfCopies(t);
				this.ChangeNumberOfCopies(t, num + num2);
			}
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00085164 File Offset: 0x00083364
		public Bag<T> Sum(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			Bag<T> bag;
			Bag<T> bag2;
			if (otherBag.Count > this.Count)
			{
				bag = this;
				bag2 = otherBag;
			}
			else
			{
				bag = otherBag;
				bag2 = this;
			}
			Bag<T> bag3 = bag2.Clone();
			bag3.SumWith(bag);
			return bag3;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000851A0 File Offset: 0x000833A0
		public void IntersectionWith(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			this.hash.StopEnumerations();
			Bag<T> bag;
			Bag<T> bag2;
			if (otherBag.Count > this.Count)
			{
				bag = this;
				bag2 = otherBag;
			}
			else
			{
				bag = otherBag;
				bag2 = this;
			}
			Hash<KeyValuePair<T, int>> hash = new Hash<KeyValuePair<T, int>>(this.equalityComparer);
			int num = 0;
			foreach (T t in bag.DistinctItems())
			{
				int num2 = bag2.NumberOfCopies(t);
				int num3 = bag.NumberOfCopies(t);
				int num4 = Math.Min(num2, num3);
				if (num4 > 0)
				{
					KeyValuePair<T, int> keyValuePair;
					hash.Insert(Bag<T>.NewPair(t, num4), true, out keyValuePair);
					num += num4;
				}
			}
			this.hash = hash;
			this.count = num;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00085270 File Offset: 0x00083470
		public Bag<T> Intersection(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			Bag<T> bag;
			Bag<T> bag2;
			if (otherBag.Count > this.Count)
			{
				bag = this;
				bag2 = otherBag;
			}
			else
			{
				bag = otherBag;
				bag2 = this;
			}
			Bag<T> bag3 = new Bag<T>(this.keyEqualityComparer);
			foreach (T t in bag.DistinctItems())
			{
				int num = bag2.NumberOfCopies(t);
				int num2 = bag.NumberOfCopies(t);
				int num3 = Math.Min(num, num2);
				if (num3 > 0)
				{
					bag3.ChangeNumberOfCopies(t, num3);
				}
			}
			return bag3;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00085310 File Offset: 0x00083510
		public void DifferenceWith(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (this == otherBag)
			{
				this.Clear();
				return;
			}
			foreach (T t in otherBag.DistinctItems())
			{
				int num = this.NumberOfCopies(t);
				int num2 = otherBag.NumberOfCopies(t);
				int num3 = num - num2;
				if (num3 < 0)
				{
					num3 = 0;
				}
				this.ChangeNumberOfCopies(t, num3);
			}
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00085388 File Offset: 0x00083588
		public Bag<T> Difference(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			Bag<T> bag = this.Clone();
			bag.DifferenceWith(otherBag);
			return bag;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000853A0 File Offset: 0x000835A0
		public void SymmetricDifferenceWith(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (this == otherBag)
			{
				this.Clear();
				return;
			}
			foreach (T t in otherBag.DistinctItems())
			{
				int num = this.NumberOfCopies(t);
				int num2 = otherBag.NumberOfCopies(t);
				int num3 = Math.Abs(num - num2);
				if (num3 != num)
				{
					this.ChangeNumberOfCopies(t, num3);
				}
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00085424 File Offset: 0x00083624
		public Bag<T> SymmetricDifference(Bag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			Bag<T> bag;
			Bag<T> bag2;
			if (otherBag.Count > this.Count)
			{
				bag = this;
				bag2 = otherBag;
			}
			else
			{
				bag = otherBag;
				bag2 = this;
			}
			Bag<T> bag3 = bag2.Clone();
			bag3.SymmetricDifferenceWith(bag);
			return bag3;
		}

		// Token: 0x04000EEF RID: 3823
		private IEqualityComparer<KeyValuePair<T, int>> equalityComparer;

		// Token: 0x04000EF0 RID: 3824
		private IEqualityComparer<T> keyEqualityComparer;

		// Token: 0x04000EF1 RID: 3825
		private Hash<KeyValuePair<T, int>> hash;

		// Token: 0x04000EF2 RID: 3826
		private int count;
	}
}
