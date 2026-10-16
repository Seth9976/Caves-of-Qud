using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002FC RID: 764
	[Serializable]
	public class OrderedBag<T> : CollectionBase<T>, ICloneable
	{
		// Token: 0x06001B64 RID: 7012 RVA: 0x000893DB File Offset: 0x000875DB
		public OrderedBag()
			: this(Comparers.DefaultComparer<T>())
		{
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000893E8 File Offset: 0x000875E8
		public OrderedBag(Comparison<T> comparison)
			: this(Comparers.ComparerFromComparison<T>(comparison))
		{
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000893F6 File Offset: 0x000875F6
		public OrderedBag(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.comparer = comparer;
			this.tree = new RedBlackTree<T>(comparer);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0008941F File Offset: 0x0008761F
		public OrderedBag(IEnumerable<T> collection)
			: this(collection, Comparers.DefaultComparer<T>())
		{
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x0008942D File Offset: 0x0008762D
		public OrderedBag(IEnumerable<T> collection, Comparison<T> comparison)
			: this(collection, Comparers.ComparerFromComparison<T>(comparison))
		{
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0008943C File Offset: 0x0008763C
		public OrderedBag(IEnumerable<T> collection, IComparer<T> comparer)
			: this(comparer)
		{
			this.AddMany(collection);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0008944C File Offset: 0x0008764C
		private OrderedBag(IComparer<T> comparer, RedBlackTree<T> tree)
		{
			this.comparer = comparer;
			this.tree = tree;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00089462 File Offset: 0x00087662
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0008946A File Offset: 0x0008766A
		public OrderedBag<T> Clone()
		{
			return new OrderedBag<T>(this.comparer, this.tree.Clone());
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00089484 File Offset: 0x00087684
		public OrderedBag<T> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(T), out flag))
			{
				throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, typeof(T).FullName));
			}
			OrderedBag<T> orderedBag = new OrderedBag<T>(this.comparer);
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
				orderedBag.Add(t2);
			}
			return orderedBag;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x00089540 File Offset: 0x00087740
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00089548 File Offset: 0x00087748
		public sealed override int Count
		{
			get
			{
				return this.tree.ElementCount;
			}
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00089555 File Offset: 0x00087755
		public int NumberOfCopies(T item)
		{
			return this.tree.CountRange(this.tree.EqualRangeTester(item));
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0008956E File Offset: 0x0008776E
		public sealed override IEnumerator<T> GetEnumerator()
		{
			return this.tree.GetEnumerator();
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0008957C File Offset: 0x0008777C
		public sealed override bool Contains(T item)
		{
			T t;
			return this.tree.Find(item, false, false, out t);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x00089599 File Offset: 0x00087799
		public IEnumerable<T> GetEqualItems(T item)
		{
			return this.tree.EnumerateRange(this.tree.EqualRangeTester(item));
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000895B2 File Offset: 0x000877B2
		public IEnumerable<T> DistinctItems()
		{
			T t = default(T);
			bool flag = true;
			foreach (T item in this)
			{
				if (flag || this.comparer.Compare(item, t) != 0)
				{
					yield return item;
				}
				t = item;
				flag = false;
				item = default(T);
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x170003AD RID: 941
		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.tree.GetItemByIndex(index);
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000895E8 File Offset: 0x000877E8
		public int LastIndexOf(T item)
		{
			return this.tree.FindIndex(item, false);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000895F7 File Offset: 0x000877F7
		public int IndexOf(T item)
		{
			return this.tree.FindIndex(item, true);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00089608 File Offset: 0x00087808
		public sealed override void Add(T item)
		{
			T t;
			this.tree.Insert(item, DuplicatePolicy.InsertLast, out t);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00089628 File Offset: 0x00087828
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

		// Token: 0x06001B7A RID: 7034 RVA: 0x0008968C File Offset: 0x0008788C
		public sealed override bool Remove(T item)
		{
			T t;
			return this.tree.Delete(item, false, out t);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000896A8 File Offset: 0x000878A8
		public int RemoveAllCopies(T item)
		{
			return this.tree.DeleteRange(this.tree.EqualRangeTester(item));
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000896C4 File Offset: 0x000878C4
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

		// Token: 0x06001B7D RID: 7037 RVA: 0x00089738 File Offset: 0x00087938
		public sealed override void Clear()
		{
			this.tree.Clear();
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00089745 File Offset: 0x00087945
		private void CheckEmpty()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0008975C File Offset: 0x0008795C
		public T GetFirst()
		{
			this.CheckEmpty();
			T t;
			this.tree.FirstItemInRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), out t);
			return t;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x00089790 File Offset: 0x00087990
		public T GetLast()
		{
			this.CheckEmpty();
			T t;
			this.tree.LastItemInRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), out t);
			return t;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x000897C4 File Offset: 0x000879C4
		public T RemoveFirst()
		{
			this.CheckEmpty();
			T t;
			this.tree.DeleteItemFromRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), true, out t);
			return t;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000897F8 File Offset: 0x000879F8
		public T RemoveLast()
		{
			this.CheckEmpty();
			T t;
			this.tree.DeleteItemFromRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), false, out t);
			return t;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0008982C File Offset: 0x00087A2C
		private void CheckConsistentComparison(OrderedBag<T> otherBag)
		{
			if (otherBag == null)
			{
				throw new ArgumentNullException("otherBag");
			}
			if (!object.Equals(this.comparer, otherBag.comparer))
			{
				throw new InvalidOperationException(Strings.InconsistentComparisons);
			}
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0008985C File Offset: 0x00087A5C
		public bool IsSupersetOf(OrderedBag<T> otherBag)
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

		// Token: 0x06001B85 RID: 7045 RVA: 0x000898D0 File Offset: 0x00087AD0
		public bool IsProperSupersetOf(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			return otherBag.Count < this.Count && this.IsSupersetOf(otherBag);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x000898F0 File Offset: 0x00087AF0
		public bool IsSubsetOf(OrderedBag<T> otherBag)
		{
			return otherBag.IsSupersetOf(this);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000898F9 File Offset: 0x00087AF9
		public bool IsProperSubsetOf(OrderedBag<T> otherBag)
		{
			return otherBag.IsProperSupersetOf(this);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00089904 File Offset: 0x00087B04
		public bool IsDisjointFrom(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			OrderedBag<T> orderedBag;
			OrderedBag<T> orderedBag2;
			if (otherBag.Count > this.Count)
			{
				orderedBag = this;
				orderedBag2 = otherBag;
			}
			else
			{
				orderedBag = otherBag;
				orderedBag2 = this;
			}
			foreach (T t in orderedBag)
			{
				if (orderedBag2.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00089978 File Offset: 0x00087B78
		public bool IsEqualTo(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			if (otherBag.Count != this.Count)
			{
				return false;
			}
			bool flag3;
			using (IEnumerator<T> enumerator = this.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = otherBag.GetEnumerator())
				{
					bool flag;
					bool flag2;
					do
					{
						flag = enumerator.MoveNext();
						flag2 = enumerator2.MoveNext();
						if (!flag || !flag2)
						{
							goto IL_0057;
						}
					}
					while (this.comparer.Compare(enumerator.Current, enumerator2.Current) == 0);
					return false;
					IL_0057:
					flag3 = flag == flag2;
				}
			}
			return flag3;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00089A18 File Offset: 0x00087C18
		public void UnionWith(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			T t = default(T);
			bool flag = true;
			int num = 0;
			int num2 = 0;
			foreach (T t2 in otherBag)
			{
				if (flag || this.comparer.Compare(t2, t) != 0)
				{
					num = this.NumberOfCopies(t2);
					num2 = 1;
				}
				else
				{
					num2++;
				}
				if (num2 > num)
				{
					this.Add(t2);
				}
				t = t2;
				flag = false;
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x00089AAC File Offset: 0x00087CAC
		public OrderedBag<T> Union(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			OrderedBag<T> orderedBag;
			OrderedBag<T> orderedBag2;
			if (otherBag.Count > this.Count)
			{
				orderedBag = this;
				orderedBag2 = otherBag;
			}
			else
			{
				orderedBag = otherBag;
				orderedBag2 = this;
			}
			OrderedBag<T> orderedBag3 = orderedBag2.Clone();
			orderedBag3.UnionWith(orderedBag);
			return orderedBag3;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00089AE5 File Offset: 0x00087CE5
		public void SumWith(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			this.AddMany(otherBag);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00089AF8 File Offset: 0x00087CF8
		public OrderedBag<T> Sum(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			OrderedBag<T> orderedBag;
			OrderedBag<T> orderedBag2;
			if (otherBag.Count > this.Count)
			{
				orderedBag = this;
				orderedBag2 = otherBag;
			}
			else
			{
				orderedBag = otherBag;
				orderedBag2 = this;
			}
			OrderedBag<T> orderedBag3 = orderedBag2.Clone();
			orderedBag3.AddMany(orderedBag);
			return orderedBag3;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00089B34 File Offset: 0x00087D34
		public void IntersectionWith(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			this.tree.StopEnumerations();
			OrderedBag<T> orderedBag;
			OrderedBag<T> orderedBag2;
			if (otherBag.Count > this.Count)
			{
				orderedBag = this;
				orderedBag2 = otherBag;
			}
			else
			{
				orderedBag = otherBag;
				orderedBag2 = this;
			}
			RedBlackTree<T> redBlackTree = new RedBlackTree<T>(this.comparer);
			T t = default(T);
			bool flag = true;
			int num = 0;
			int num2 = 0;
			foreach (T t2 in orderedBag)
			{
				if (flag || this.comparer.Compare(t2, t) != 0)
				{
					num2 = orderedBag2.NumberOfCopies(t2);
					num = 1;
				}
				else
				{
					num++;
				}
				if (num <= num2)
				{
					T t3;
					redBlackTree.Insert(t2, DuplicatePolicy.InsertLast, out t3);
				}
				t = t2;
				flag = false;
			}
			this.tree = redBlackTree;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00089C0C File Offset: 0x00087E0C
		public OrderedBag<T> Intersection(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			OrderedBag<T> orderedBag;
			OrderedBag<T> orderedBag2;
			if (otherBag.Count > this.Count)
			{
				orderedBag = this;
				orderedBag2 = otherBag;
			}
			else
			{
				orderedBag = otherBag;
				orderedBag2 = this;
			}
			T t = default(T);
			bool flag = true;
			int num = 0;
			int num2 = 0;
			OrderedBag<T> orderedBag3 = new OrderedBag<T>(this.comparer);
			foreach (T t2 in orderedBag)
			{
				if (flag || this.comparer.Compare(t2, t) != 0)
				{
					num2 = orderedBag2.NumberOfCopies(t2);
					num = 1;
				}
				else
				{
					num++;
				}
				if (num <= num2)
				{
					orderedBag3.Add(t2);
				}
				t = t2;
				flag = false;
			}
			return orderedBag3;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00089CD0 File Offset: 0x00087ED0
		public void DifferenceWith(OrderedBag<T> otherBag)
		{
			if (this == otherBag)
			{
				this.Clear();
			}
			this.CheckConsistentComparison(otherBag);
			T t = default(T);
			bool flag = true;
			int num = 0;
			int num2 = 0;
			foreach (T t2 in otherBag)
			{
				if (flag || this.comparer.Compare(t2, t) != 0)
				{
					num = this.NumberOfCopies(t2);
					num2 = 1;
				}
				else
				{
					num2++;
				}
				if (num2 <= num)
				{
					this.Remove(t2);
				}
				t = t2;
				flag = false;
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00089D6C File Offset: 0x00087F6C
		public OrderedBag<T> Difference(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			OrderedBag<T> orderedBag = this.Clone();
			orderedBag.DifferenceWith(otherBag);
			return orderedBag;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00089D82 File Offset: 0x00087F82
		public void SymmetricDifferenceWith(OrderedBag<T> otherBag)
		{
			this.tree = this.SymmetricDifference(otherBag).tree;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00089D98 File Offset: 0x00087F98
		public OrderedBag<T> SymmetricDifference(OrderedBag<T> otherBag)
		{
			this.CheckConsistentComparison(otherBag);
			OrderedBag<T> orderedBag = new OrderedBag<T>(this.comparer);
			IEnumerator<T> enumerator = this.GetEnumerator();
			IEnumerator<T> enumerator2 = otherBag.GetEnumerator();
			bool flag = enumerator.MoveNext();
			bool flag2 = enumerator2.MoveNext();
			for (;;)
			{
				int num;
				if (!flag)
				{
					if (!flag2)
					{
						break;
					}
					num = 1;
				}
				else if (!flag2)
				{
					num = -1;
				}
				else
				{
					num = this.comparer.Compare(enumerator.Current, enumerator2.Current);
				}
				if (num == 0)
				{
					flag = enumerator.MoveNext();
					flag2 = enumerator2.MoveNext();
				}
				else if (num < 0)
				{
					orderedBag.Add(enumerator.Current);
					flag = enumerator.MoveNext();
				}
				else
				{
					orderedBag.Add(enumerator2.Current);
					flag2 = enumerator2.MoveNext();
				}
			}
			return orderedBag;
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00089E49 File Offset: 0x00088049
		public IList<T> AsList()
		{
			return new OrderedBag<T>.ListView(this, new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), true, false);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00089E64 File Offset: 0x00088064
		public OrderedBag<T>.View Reversed()
		{
			return new OrderedBag<T>.View(this, new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), true, true);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00089E7F File Offset: 0x0008807F
		public OrderedBag<T>.View Range(T from, bool fromInclusive, T to, bool toInclusive)
		{
			return new OrderedBag<T>.View(this, this.tree.DoubleBoundedRangeTester(from, fromInclusive, to, toInclusive), false, false);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00089E99 File Offset: 0x00088099
		public OrderedBag<T>.View RangeFrom(T from, bool fromInclusive)
		{
			return new OrderedBag<T>.View(this, this.tree.LowerBoundedRangeTester(from, fromInclusive), false, false);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00089EB0 File Offset: 0x000880B0
		public OrderedBag<T>.View RangeTo(T to, bool toInclusive)
		{
			return new OrderedBag<T>.View(this, this.tree.UpperBoundedRangeTester(to, toInclusive), false, false);
		}

		// Token: 0x04000F12 RID: 3858
		private IComparer<T> comparer;

		// Token: 0x04000F13 RID: 3859
		private RedBlackTree<T> tree;

		// Token: 0x020016EB RID: 5867
		[Serializable]
		private class ListView : ReadOnlyListBase<T>
		{
			// Token: 0x0600DAD6 RID: 56022 RVA: 0x00504614 File Offset: 0x00502814
			public ListView(OrderedBag<T> myBag, RedBlackTree<T>.RangeTester rangeTester, bool entireTree, bool reversed)
			{
				this.myBag = myBag;
				this.rangeTester = rangeTester;
				this.entireTree = entireTree;
				this.reversed = reversed;
			}

			// Token: 0x17000DC0 RID: 3520
			// (get) Token: 0x0600DAD7 RID: 56023 RVA: 0x00504639 File Offset: 0x00502839
			public sealed override int Count
			{
				get
				{
					if (this.entireTree)
					{
						return this.myBag.Count;
					}
					return this.myBag.tree.CountRange(this.rangeTester);
				}
			}

			// Token: 0x17000DC1 RID: 3521
			public sealed override T this[int index]
			{
				get
				{
					if (this.entireTree)
					{
						if (this.reversed)
						{
							return this.myBag[this.myBag.Count - 1 - index];
						}
						return this.myBag[index];
					}
					else
					{
						T t;
						int num = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
						int num2 = this.myBag.tree.LastItemInRange(this.rangeTester, out t);
						if (num < 0 || num2 < 0 || index < 0 || index >= num2 - num + 1)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.reversed)
						{
							return this.myBag[num2 - index];
						}
						return this.myBag[num + index];
					}
				}
				set
				{
					base[index] = value;
				}
			}

			// Token: 0x0600DAD9 RID: 56025 RVA: 0x00504724 File Offset: 0x00502924
			public sealed override int IndexOf(T item)
			{
				if (this.entireTree)
				{
					if (this.reversed)
					{
						return this.myBag.Count - 1 - this.myBag.LastIndexOf(item);
					}
					return this.myBag.IndexOf(item);
				}
				else
				{
					if (this.rangeTester(item) != 0)
					{
						return -1;
					}
					if (this.reversed)
					{
						int num = this.myBag.tree.FindIndex(item, false);
						if (num < 0)
						{
							return -1;
						}
						T t;
						return this.myBag.tree.LastItemInRange(this.rangeTester, out t) - num;
					}
					else
					{
						int num2 = this.myBag.tree.FindIndex(item, true);
						if (num2 < 0)
						{
							return -1;
						}
						T t;
						int num3 = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
						return num2 - num3;
					}
				}
			}

			// Token: 0x04005C31 RID: 23601
			private OrderedBag<T> myBag;

			// Token: 0x04005C32 RID: 23602
			private RedBlackTree<T>.RangeTester rangeTester;

			// Token: 0x04005C33 RID: 23603
			private bool entireTree;

			// Token: 0x04005C34 RID: 23604
			private bool reversed;
		}

		// Token: 0x020016EC RID: 5868
		[Serializable]
		public class View : CollectionBase<T>
		{
			// Token: 0x0600DADB RID: 56027 RVA: 0x005047F4 File Offset: 0x005029F4
			internal View(OrderedBag<T> myBag, RedBlackTree<T>.RangeTester rangeTester, bool entireTree, bool reversed)
			{
				this.myBag = myBag;
				this.rangeTester = rangeTester;
				this.entireTree = entireTree;
				this.reversed = reversed;
			}

			// Token: 0x0600DADC RID: 56028 RVA: 0x00504819 File Offset: 0x00502A19
			private bool ItemInView(T item)
			{
				return this.rangeTester(item) == 0;
			}

			// Token: 0x0600DADD RID: 56029 RVA: 0x0050482C File Offset: 0x00502A2C
			public sealed override IEnumerator<T> GetEnumerator()
			{
				if (this.reversed)
				{
					return this.myBag.tree.EnumerateRangeReversed(this.rangeTester).GetEnumerator();
				}
				return this.myBag.tree.EnumerateRange(this.rangeTester).GetEnumerator();
			}

			// Token: 0x17000DC2 RID: 3522
			// (get) Token: 0x0600DADE RID: 56030 RVA: 0x00504878 File Offset: 0x00502A78
			public sealed override int Count
			{
				get
				{
					if (this.entireTree)
					{
						return this.myBag.Count;
					}
					return this.myBag.tree.CountRange(this.rangeTester);
				}
			}

			// Token: 0x0600DADF RID: 56031 RVA: 0x005048A4 File Offset: 0x00502AA4
			public sealed override void Clear()
			{
				if (this.entireTree)
				{
					this.myBag.Clear();
					return;
				}
				this.myBag.tree.DeleteRange(this.rangeTester);
			}

			// Token: 0x0600DAE0 RID: 56032 RVA: 0x005048D1 File Offset: 0x00502AD1
			public sealed override void Add(T item)
			{
				if (!this.ItemInView(item))
				{
					throw new ArgumentException(Strings.OutOfViewRange, "item");
				}
				this.myBag.Add(item);
			}

			// Token: 0x0600DAE1 RID: 56033 RVA: 0x005048F8 File Offset: 0x00502AF8
			public sealed override bool Remove(T item)
			{
				return this.ItemInView(item) && this.myBag.Remove(item);
			}

			// Token: 0x0600DAE2 RID: 56034 RVA: 0x00504911 File Offset: 0x00502B11
			public sealed override bool Contains(T item)
			{
				return this.ItemInView(item) && this.myBag.Contains(item);
			}

			// Token: 0x0600DAE3 RID: 56035 RVA: 0x0050492C File Offset: 0x00502B2C
			public int IndexOf(T item)
			{
				if (this.entireTree)
				{
					if (!this.reversed)
					{
						return this.myBag.tree.FindIndex(item, true);
					}
					int num = this.myBag.tree.FindIndex(item, false);
					if (num < 0)
					{
						return -1;
					}
					return this.myBag.Count - 1 - num;
				}
				else
				{
					if (!this.ItemInView(item))
					{
						return -1;
					}
					if (this.reversed)
					{
						int num2 = this.myBag.tree.FindIndex(item, false);
						if (num2 < 0)
						{
							return -1;
						}
						T t;
						return this.myBag.tree.LastItemInRange(this.rangeTester, out t) - num2;
					}
					else
					{
						int num3 = this.myBag.tree.FindIndex(item, true);
						if (num3 < 0)
						{
							return -1;
						}
						T t;
						int num4 = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
						return num3 - num4;
					}
				}
			}

			// Token: 0x0600DAE4 RID: 56036 RVA: 0x00504A04 File Offset: 0x00502C04
			public int LastIndexOf(T item)
			{
				if (this.entireTree)
				{
					if (!this.reversed)
					{
						return this.myBag.tree.FindIndex(item, false);
					}
					int num = this.myBag.tree.FindIndex(item, true);
					if (num < 0)
					{
						return -1;
					}
					return this.myBag.Count - 1 - num;
				}
				else
				{
					if (!this.ItemInView(item))
					{
						return -1;
					}
					if (this.reversed)
					{
						int num2 = this.myBag.tree.FindIndex(item, true);
						if (num2 < 0)
						{
							return -1;
						}
						T t;
						return this.myBag.tree.LastItemInRange(this.rangeTester, out t) - num2;
					}
					else
					{
						int num3 = this.myBag.tree.FindIndex(item, false);
						if (num3 < 0)
						{
							return -1;
						}
						T t;
						int num4 = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
						return num3 - num4;
					}
				}
			}

			// Token: 0x17000DC3 RID: 3523
			public T this[int index]
			{
				get
				{
					if (this.entireTree)
					{
						if (this.reversed)
						{
							return this.myBag[this.myBag.Count - 1 - index];
						}
						return this.myBag[index];
					}
					else
					{
						T t;
						int num = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
						int num2 = this.myBag.tree.LastItemInRange(this.rangeTester, out t);
						if (num < 0 || num2 < 0 || index < 0 || index >= num2 - num + 1)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.reversed)
						{
							return this.myBag[num2 - index];
						}
						return this.myBag[num + index];
					}
				}
			}

			// Token: 0x0600DAE6 RID: 56038 RVA: 0x00504B97 File Offset: 0x00502D97
			public IList<T> AsList()
			{
				return new OrderedBag<T>.ListView(this.myBag, this.rangeTester, this.entireTree, this.reversed);
			}

			// Token: 0x0600DAE7 RID: 56039 RVA: 0x00504BB6 File Offset: 0x00502DB6
			public OrderedBag<T>.View Reversed()
			{
				return new OrderedBag<T>.View(this.myBag, this.rangeTester, this.entireTree, !this.reversed);
			}

			// Token: 0x0600DAE8 RID: 56040 RVA: 0x00504BD8 File Offset: 0x00502DD8
			public T GetFirst()
			{
				T t;
				int num;
				if (this.reversed)
				{
					num = this.myBag.tree.LastItemInRange(this.rangeTester, out t);
				}
				else
				{
					num = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
				}
				if (num < 0)
				{
					throw new InvalidOperationException(Strings.CollectionIsEmpty);
				}
				return t;
			}

			// Token: 0x0600DAE9 RID: 56041 RVA: 0x00504C34 File Offset: 0x00502E34
			public T GetLast()
			{
				T t;
				int num;
				if (this.reversed)
				{
					num = this.myBag.tree.FirstItemInRange(this.rangeTester, out t);
				}
				else
				{
					num = this.myBag.tree.LastItemInRange(this.rangeTester, out t);
				}
				if (num < 0)
				{
					throw new InvalidOperationException(Strings.CollectionIsEmpty);
				}
				return t;
			}

			// Token: 0x04005C35 RID: 23605
			private OrderedBag<T> myBag;

			// Token: 0x04005C36 RID: 23606
			private RedBlackTree<T>.RangeTester rangeTester;

			// Token: 0x04005C37 RID: 23607
			private bool entireTree;

			// Token: 0x04005C38 RID: 23608
			private bool reversed;
		}
	}
}
