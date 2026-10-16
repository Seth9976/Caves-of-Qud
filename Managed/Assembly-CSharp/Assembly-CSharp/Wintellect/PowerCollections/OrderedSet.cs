using System;
using System.Collections;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002FF RID: 767
	[Serializable]
	public class OrderedSet<T> : CollectionBase<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICloneable
	{
		// Token: 0x06001BDE RID: 7134 RVA: 0x0008A990 File Offset: 0x00088B90
		public OrderedSet()
			: this(Comparers.DefaultComparer<T>())
		{
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x0008A99D File Offset: 0x00088B9D
		public OrderedSet(Comparison<T> comparison)
			: this(Comparers.ComparerFromComparison<T>(comparison))
		{
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x0008A9AB File Offset: 0x00088BAB
		public OrderedSet(IComparer<T> comparer)
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			this.comparer = comparer;
			this.tree = new RedBlackTree<T>(comparer);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0008A9D4 File Offset: 0x00088BD4
		public OrderedSet(IEnumerable<T> collection)
			: this(collection, Comparers.DefaultComparer<T>())
		{
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x0008A9E2 File Offset: 0x00088BE2
		public OrderedSet(IEnumerable<T> collection, Comparison<T> comparison)
			: this(collection, Comparers.ComparerFromComparison<T>(comparison))
		{
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0008A9F1 File Offset: 0x00088BF1
		public OrderedSet(IEnumerable<T> collection, IComparer<T> comparer)
			: this(comparer)
		{
			this.AddMany(collection);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0008AA01 File Offset: 0x00088C01
		private OrderedSet(IComparer<T> comparer, RedBlackTree<T> tree)
		{
			this.comparer = comparer;
			this.tree = tree;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0008AA17 File Offset: 0x00088C17
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0008AA1F File Offset: 0x00088C1F
		public OrderedSet<T> Clone()
		{
			return new OrderedSet<T>(this.comparer, this.tree.Clone());
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0008AA38 File Offset: 0x00088C38
		public OrderedSet<T> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(T), out flag))
			{
				throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, typeof(T).FullName));
			}
			OrderedSet<T> orderedSet = new OrderedSet<T>(this.comparer);
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
				orderedSet.Add(t2);
			}
			return orderedSet;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0008AAF4 File Offset: 0x00088CF4
		public IComparer<T> Comparer
		{
			get
			{
				return this.comparer;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0008AAFC File Offset: 0x00088CFC
		public sealed override int Count
		{
			get
			{
				return this.tree.ElementCount;
			}
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0008AB09 File Offset: 0x00088D09
		public sealed override IEnumerator<T> GetEnumerator()
		{
			return this.tree.GetEnumerator();
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x0008AB18 File Offset: 0x00088D18
		public sealed override bool Contains(T item)
		{
			T t;
			return this.tree.Find(item, false, false, out t);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0008AB35 File Offset: 0x00088D35
		public bool TryGetItem(T item, out T foundItem)
		{
			return this.tree.Find(item, true, false, out foundItem);
		}

		// Token: 0x170003B7 RID: 951
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

		// Token: 0x06001BEE RID: 7150 RVA: 0x0008AB6C File Offset: 0x00088D6C
		public int IndexOf(T item)
		{
			return this.tree.FindIndex(item, true);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0008AB7C File Offset: 0x00088D7C
		public new bool Add(T item)
		{
			T t;
			return !this.tree.Insert(item, DuplicatePolicy.ReplaceFirst, out t);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0008AB9B File Offset: 0x00088D9B
		void ICollection<T>.Add(T item)
		{
			this.Add(item);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x0008ABA8 File Offset: 0x00088DA8
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

		// Token: 0x06001BF2 RID: 7154 RVA: 0x0008AC04 File Offset: 0x00088E04
		public sealed override bool Remove(T item)
		{
			T t;
			return this.tree.Delete(item, true, out t);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0008AC20 File Offset: 0x00088E20
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

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0008AC94 File Offset: 0x00088E94
		public sealed override void Clear()
		{
			this.tree.StopEnumerations();
			this.tree = new RedBlackTree<T>(this.comparer);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0008ACB2 File Offset: 0x00088EB2
		private void CheckEmpty()
		{
			if (this.Count == 0)
			{
				throw new InvalidOperationException(Strings.CollectionIsEmpty);
			}
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0008ACC8 File Offset: 0x00088EC8
		public T GetFirst()
		{
			this.CheckEmpty();
			T t;
			this.tree.FirstItemInRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), out t);
			return t;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0008ACFC File Offset: 0x00088EFC
		public T GetLast()
		{
			this.CheckEmpty();
			T t;
			this.tree.LastItemInRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), out t);
			return t;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0008AD30 File Offset: 0x00088F30
		public T RemoveFirst()
		{
			this.CheckEmpty();
			T t;
			this.tree.DeleteItemFromRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), true, out t);
			return t;
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0008AD64 File Offset: 0x00088F64
		public T RemoveLast()
		{
			this.CheckEmpty();
			T t;
			this.tree.DeleteItemFromRange(new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), false, out t);
			return t;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0008AD98 File Offset: 0x00088F98
		private void CheckConsistentComparison(OrderedSet<T> otherSet)
		{
			if (otherSet == null)
			{
				throw new ArgumentNullException("otherSet");
			}
			if (!object.Equals(this.comparer, otherSet.comparer))
			{
				throw new InvalidOperationException(Strings.InconsistentComparisons);
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0008ADC8 File Offset: 0x00088FC8
		public bool IsSupersetOf(OrderedSet<T> otherSet)
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

		// Token: 0x06001BFC RID: 7164 RVA: 0x0008AE30 File Offset: 0x00089030
		public bool IsProperSupersetOf(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			return otherSet.Count < this.Count && this.IsSupersetOf(otherSet);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0008AE50 File Offset: 0x00089050
		public bool IsSubsetOf(OrderedSet<T> otherSet)
		{
			return otherSet.IsSupersetOf(this);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0008AE59 File Offset: 0x00089059
		public bool IsProperSubsetOf(OrderedSet<T> otherSet)
		{
			return otherSet.IsProperSupersetOf(this);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0008AE64 File Offset: 0x00089064
		public bool IsEqualTo(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			if (otherSet.Count != this.Count)
			{
				return false;
			}
			bool flag3;
			using (IEnumerator<T> enumerator = this.GetEnumerator())
			{
				using (IEnumerator<T> enumerator2 = otherSet.GetEnumerator())
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

		// Token: 0x06001C00 RID: 7168 RVA: 0x0008AF04 File Offset: 0x00089104
		public void UnionWith(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			this.AddMany(otherSet);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0008AF14 File Offset: 0x00089114
		public bool IsDisjointFrom(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			OrderedSet<T> orderedSet;
			OrderedSet<T> orderedSet2;
			if (otherSet.Count > this.Count)
			{
				orderedSet = this;
				orderedSet2 = otherSet;
			}
			else
			{
				orderedSet = otherSet;
				orderedSet2 = this;
			}
			foreach (T t in orderedSet)
			{
				if (orderedSet2.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x0008AF88 File Offset: 0x00089188
		public OrderedSet<T> Union(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			OrderedSet<T> orderedSet;
			OrderedSet<T> orderedSet2;
			if (otherSet.Count > this.Count)
			{
				orderedSet = this;
				orderedSet2 = otherSet;
			}
			else
			{
				orderedSet = otherSet;
				orderedSet2 = this;
			}
			OrderedSet<T> orderedSet3 = orderedSet2.Clone();
			orderedSet3.AddMany(orderedSet);
			return orderedSet3;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0008AFC4 File Offset: 0x000891C4
		public void IntersectionWith(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			this.tree.StopEnumerations();
			OrderedSet<T> orderedSet;
			OrderedSet<T> orderedSet2;
			if (otherSet.Count > this.Count)
			{
				orderedSet = this;
				orderedSet2 = otherSet;
			}
			else
			{
				orderedSet = otherSet;
				orderedSet2 = this;
			}
			RedBlackTree<T> redBlackTree = new RedBlackTree<T>(this.comparer);
			foreach (T t in orderedSet)
			{
				if (orderedSet2.Contains(t))
				{
					T t2;
					redBlackTree.Insert(t, DuplicatePolicy.ReplaceFirst, out t2);
				}
			}
			this.tree = redBlackTree;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x0008B060 File Offset: 0x00089260
		public OrderedSet<T> Intersection(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			OrderedSet<T> orderedSet;
			OrderedSet<T> orderedSet2;
			if (otherSet.Count > this.Count)
			{
				orderedSet = this;
				orderedSet2 = otherSet;
			}
			else
			{
				orderedSet = otherSet;
				orderedSet2 = this;
			}
			OrderedSet<T> orderedSet3 = new OrderedSet<T>(this.comparer);
			foreach (T t in orderedSet)
			{
				if (orderedSet2.Contains(t))
				{
					orderedSet3.Add(t);
				}
			}
			return orderedSet3;
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x0008B0E4 File Offset: 0x000892E4
		public void DifferenceWith(OrderedSet<T> otherSet)
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

		// Token: 0x06001C06 RID: 7174 RVA: 0x0008B180 File Offset: 0x00089380
		public OrderedSet<T> Difference(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			OrderedSet<T> orderedSet = this.Clone();
			orderedSet.DifferenceWith(otherSet);
			return orderedSet;
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x0008B198 File Offset: 0x00089398
		public void SymmetricDifferenceWith(OrderedSet<T> otherSet)
		{
			if (this == otherSet)
			{
				this.Clear();
			}
			this.CheckConsistentComparison(otherSet);
			foreach (T t in otherSet)
			{
				if (this.Contains(t))
				{
					this.Remove(t);
				}
				else
				{
					this.Add(t);
				}
			}
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0008B208 File Offset: 0x00089408
		public OrderedSet<T> SymmetricDifference(OrderedSet<T> otherSet)
		{
			this.CheckConsistentComparison(otherSet);
			OrderedSet<T> orderedSet;
			OrderedSet<T> orderedSet2;
			if (otherSet.Count > this.Count)
			{
				orderedSet = this;
				orderedSet2 = otherSet;
			}
			else
			{
				orderedSet = otherSet;
				orderedSet2 = this;
			}
			OrderedSet<T> orderedSet3 = orderedSet2.Clone();
			foreach (T t in orderedSet)
			{
				if (orderedSet3.Contains(t))
				{
					orderedSet3.Remove(t);
				}
				else
				{
					orderedSet3.Add(t);
				}
			}
			return orderedSet3;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0008B290 File Offset: 0x00089490
		public IList<T> AsList()
		{
			return new OrderedSet<T>.ListView(this, new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), true, false);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0008B2AB File Offset: 0x000894AB
		public OrderedSet<T>.View Reversed()
		{
			return new OrderedSet<T>.View(this, new RedBlackTree<T>.RangeTester(this.tree.EntireRangeTester), true, true);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x0008B2C6 File Offset: 0x000894C6
		public OrderedSet<T>.View Range(T from, bool fromInclusive, T to, bool toInclusive)
		{
			return new OrderedSet<T>.View(this, this.tree.DoubleBoundedRangeTester(from, fromInclusive, to, toInclusive), false, false);
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x0008B2E0 File Offset: 0x000894E0
		public OrderedSet<T>.View RangeFrom(T from, bool fromInclusive)
		{
			return new OrderedSet<T>.View(this, this.tree.LowerBoundedRangeTester(from, fromInclusive), false, false);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x0008B2F7 File Offset: 0x000894F7
		public OrderedSet<T>.View RangeTo(T to, bool toInclusive)
		{
			return new OrderedSet<T>.View(this, this.tree.UpperBoundedRangeTester(to, toInclusive), false, false);
		}

		// Token: 0x04000F1D RID: 3869
		private IComparer<T> comparer;

		// Token: 0x04000F1E RID: 3870
		private RedBlackTree<T> tree;

		// Token: 0x020016F7 RID: 5879
		[Serializable]
		private class ListView : ReadOnlyListBase<T>
		{
			// Token: 0x0600DB25 RID: 56101 RVA: 0x005056BC File Offset: 0x005038BC
			public ListView(OrderedSet<T> mySet, RedBlackTree<T>.RangeTester rangeTester, bool entireTree, bool reversed)
			{
				this.mySet = mySet;
				this.rangeTester = rangeTester;
				this.entireTree = entireTree;
				this.reversed = reversed;
			}

			// Token: 0x17000DCE RID: 3534
			// (get) Token: 0x0600DB26 RID: 56102 RVA: 0x005056E1 File Offset: 0x005038E1
			public override int Count
			{
				get
				{
					if (this.entireTree)
					{
						return this.mySet.Count;
					}
					return this.mySet.tree.CountRange(this.rangeTester);
				}
			}

			// Token: 0x17000DCF RID: 3535
			public override T this[int index]
			{
				get
				{
					if (this.entireTree)
					{
						if (this.reversed)
						{
							return this.mySet[this.mySet.Count - 1 - index];
						}
						return this.mySet[index];
					}
					else
					{
						T t;
						int num = this.mySet.tree.FirstItemInRange(this.rangeTester, out t);
						int num2 = this.mySet.tree.LastItemInRange(this.rangeTester, out t);
						if (num < 0 || num2 < 0 || index < 0 || index >= num2 - num + 1)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.reversed)
						{
							return this.mySet[num2 - index];
						}
						return this.mySet[num + index];
					}
				}
			}

			// Token: 0x0600DB28 RID: 56104 RVA: 0x005057CC File Offset: 0x005039CC
			public override int IndexOf(T item)
			{
				if (this.entireTree)
				{
					if (this.reversed)
					{
						return this.mySet.Count - 1 - this.mySet.IndexOf(item);
					}
					return this.mySet.IndexOf(item);
				}
				else
				{
					if (this.rangeTester(item) != 0)
					{
						return -1;
					}
					if (this.reversed)
					{
						int num = this.mySet.tree.FindIndex(item, false);
						if (num < 0)
						{
							return -1;
						}
						T t;
						return this.mySet.tree.LastItemInRange(this.rangeTester, out t) - num;
					}
					else
					{
						int num2 = this.mySet.tree.FindIndex(item, true);
						if (num2 < 0)
						{
							return -1;
						}
						T t;
						int num3 = this.mySet.tree.FirstItemInRange(this.rangeTester, out t);
						return num2 - num3;
					}
				}
			}

			// Token: 0x04005C61 RID: 23649
			private OrderedSet<T> mySet;

			// Token: 0x04005C62 RID: 23650
			private RedBlackTree<T>.RangeTester rangeTester;

			// Token: 0x04005C63 RID: 23651
			private bool entireTree;

			// Token: 0x04005C64 RID: 23652
			private bool reversed;
		}

		// Token: 0x020016F8 RID: 5880
		[Serializable]
		public class View : CollectionBase<T>, ICollection<T>, IEnumerable<T>, IEnumerable
		{
			// Token: 0x0600DB29 RID: 56105 RVA: 0x00505892 File Offset: 0x00503A92
			internal View(OrderedSet<T> mySet, RedBlackTree<T>.RangeTester rangeTester, bool entireTree, bool reversed)
			{
				this.mySet = mySet;
				this.rangeTester = rangeTester;
				this.entireTree = entireTree;
				this.reversed = reversed;
			}

			// Token: 0x0600DB2A RID: 56106 RVA: 0x005058B7 File Offset: 0x00503AB7
			private bool ItemInView(T item)
			{
				return this.rangeTester(item) == 0;
			}

			// Token: 0x0600DB2B RID: 56107 RVA: 0x005058C8 File Offset: 0x00503AC8
			public sealed override IEnumerator<T> GetEnumerator()
			{
				if (this.reversed)
				{
					return this.mySet.tree.EnumerateRangeReversed(this.rangeTester).GetEnumerator();
				}
				return this.mySet.tree.EnumerateRange(this.rangeTester).GetEnumerator();
			}

			// Token: 0x17000DD0 RID: 3536
			// (get) Token: 0x0600DB2C RID: 56108 RVA: 0x00505914 File Offset: 0x00503B14
			public sealed override int Count
			{
				get
				{
					if (this.entireTree)
					{
						return this.mySet.Count;
					}
					return this.mySet.tree.CountRange(this.rangeTester);
				}
			}

			// Token: 0x0600DB2D RID: 56109 RVA: 0x00505940 File Offset: 0x00503B40
			public sealed override void Clear()
			{
				if (this.entireTree)
				{
					this.mySet.Clear();
					return;
				}
				this.mySet.tree.DeleteRange(this.rangeTester);
			}

			// Token: 0x0600DB2E RID: 56110 RVA: 0x0050596D File Offset: 0x00503B6D
			public new bool Add(T item)
			{
				if (!this.ItemInView(item))
				{
					throw new ArgumentException(Strings.OutOfViewRange, "item");
				}
				return this.mySet.Add(item);
			}

			// Token: 0x0600DB2F RID: 56111 RVA: 0x00505994 File Offset: 0x00503B94
			void ICollection<T>.Add(T item)
			{
				this.Add(item);
			}

			// Token: 0x0600DB30 RID: 56112 RVA: 0x0050599E File Offset: 0x00503B9E
			public sealed override bool Remove(T item)
			{
				return this.ItemInView(item) && this.mySet.Remove(item);
			}

			// Token: 0x0600DB31 RID: 56113 RVA: 0x005059B7 File Offset: 0x00503BB7
			public sealed override bool Contains(T item)
			{
				return this.ItemInView(item) && this.mySet.Contains(item);
			}

			// Token: 0x0600DB32 RID: 56114 RVA: 0x005059D0 File Offset: 0x00503BD0
			public int IndexOf(T item)
			{
				if (this.entireTree)
				{
					if (!this.reversed)
					{
						return this.mySet.tree.FindIndex(item, true);
					}
					int num = this.mySet.tree.FindIndex(item, false);
					if (num < 0)
					{
						return -1;
					}
					return this.mySet.Count - 1 - num;
				}
				else
				{
					if (!this.ItemInView(item))
					{
						return -1;
					}
					if (this.reversed)
					{
						int num2 = this.mySet.tree.FindIndex(item, false);
						if (num2 < 0)
						{
							return -1;
						}
						T t;
						return this.mySet.tree.LastItemInRange(this.rangeTester, out t) - num2;
					}
					else
					{
						int num3 = this.mySet.tree.FindIndex(item, true);
						if (num3 < 0)
						{
							return -1;
						}
						T t;
						int num4 = this.mySet.tree.FirstItemInRange(this.rangeTester, out t);
						return num3 - num4;
					}
				}
			}

			// Token: 0x17000DD1 RID: 3537
			public T this[int index]
			{
				get
				{
					if (this.entireTree)
					{
						if (this.reversed)
						{
							return this.mySet[this.mySet.Count - 1 - index];
						}
						return this.mySet[index];
					}
					else
					{
						T t;
						int num = this.mySet.tree.FirstItemInRange(this.rangeTester, out t);
						int num2 = this.mySet.tree.LastItemInRange(this.rangeTester, out t);
						if (num < 0 || num2 < 0 || index < 0 || index >= num2 - num + 1)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.reversed)
						{
							return this.mySet[num2 - index];
						}
						return this.mySet[num + index];
					}
				}
			}

			// Token: 0x0600DB34 RID: 56116 RVA: 0x00505B63 File Offset: 0x00503D63
			public IList<T> AsList()
			{
				return new OrderedSet<T>.ListView(this.mySet, this.rangeTester, this.entireTree, this.reversed);
			}

			// Token: 0x0600DB35 RID: 56117 RVA: 0x00505B82 File Offset: 0x00503D82
			public OrderedSet<T>.View Reversed()
			{
				return new OrderedSet<T>.View(this.mySet, this.rangeTester, this.entireTree, !this.reversed);
			}

			// Token: 0x0600DB36 RID: 56118 RVA: 0x00505BA4 File Offset: 0x00503DA4
			public T GetFirst()
			{
				T t;
				int num;
				if (this.reversed)
				{
					num = this.mySet.tree.LastItemInRange(this.rangeTester, out t);
				}
				else
				{
					num = this.mySet.tree.FirstItemInRange(this.rangeTester, out t);
				}
				if (num < 0)
				{
					throw new InvalidOperationException(Strings.CollectionIsEmpty);
				}
				return t;
			}

			// Token: 0x0600DB37 RID: 56119 RVA: 0x00505C00 File Offset: 0x00503E00
			public T GetLast()
			{
				T t;
				int num;
				if (this.reversed)
				{
					num = this.mySet.tree.FirstItemInRange(this.rangeTester, out t);
				}
				else
				{
					num = this.mySet.tree.LastItemInRange(this.rangeTester, out t);
				}
				if (num < 0)
				{
					throw new InvalidOperationException(Strings.CollectionIsEmpty);
				}
				return t;
			}

			// Token: 0x04005C65 RID: 23653
			private OrderedSet<T> mySet;

			// Token: 0x04005C66 RID: 23654
			private RedBlackTree<T>.RangeTester rangeTester;

			// Token: 0x04005C67 RID: 23655
			private bool entireTree;

			// Token: 0x04005C68 RID: 23656
			private bool reversed;
		}
	}
}
