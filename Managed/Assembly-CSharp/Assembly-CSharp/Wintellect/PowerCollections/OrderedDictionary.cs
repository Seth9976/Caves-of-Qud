using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002FD RID: 765
	[Serializable]
	public class OrderedDictionary<TKey, TValue> : DictionaryBase<TKey, TValue>, ICloneable
	{
		// Token: 0x06001B99 RID: 7065 RVA: 0x00089EC7 File Offset: 0x000880C7
		private static KeyValuePair<TKey, TValue> NewPair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00089ED0 File Offset: 0x000880D0
		private static KeyValuePair<TKey, TValue> NewPair(TKey key)
		{
			return new KeyValuePair<TKey, TValue>(key, default(TValue));
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x00089EEC File Offset: 0x000880EC
		public OrderedDictionary()
			: this(Comparers.DefaultComparer<TKey>())
		{
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00089EF9 File Offset: 0x000880F9
		public OrderedDictionary(IComparer<TKey> comparer)
			: this(null, comparer, Comparers.ComparerKeyValueFromComparerKey<TKey, TValue>(comparer))
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x00089F17 File Offset: 0x00088117
		public OrderedDictionary(Comparison<TKey> comparison)
			: this(null, Comparers.ComparerFromComparison<TKey>(comparison), Comparers.ComparerKeyValueFromComparisonKey<TKey, TValue>(comparison))
		{
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00089F2C File Offset: 0x0008812C
		public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> keysAndValues)
			: this(keysAndValues, Comparers.DefaultComparer<TKey>())
		{
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x00089F3A File Offset: 0x0008813A
		public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> keysAndValues, IComparer<TKey> comparer)
			: this(keysAndValues, comparer, Comparers.ComparerKeyValueFromComparerKey<TKey, TValue>(comparer))
		{
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00089F58 File Offset: 0x00088158
		public OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> keysAndValues, Comparison<TKey> comparison)
			: this(keysAndValues, Comparers.ComparerFromComparison<TKey>(comparison), Comparers.ComparerKeyValueFromComparisonKey<TKey, TValue>(comparison))
		{
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00089F6D File Offset: 0x0008816D
		private OrderedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> keysAndValues, IComparer<TKey> keyComparer, IComparer<KeyValuePair<TKey, TValue>> pairComparer)
		{
			this.keyComparer = keyComparer;
			this.pairComparer = pairComparer;
			this.tree = new RedBlackTree<KeyValuePair<TKey, TValue>>(this.pairComparer);
			if (keysAndValues != null)
			{
				this.AddMany(keysAndValues);
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x00089F9E File Offset: 0x0008819E
		private OrderedDictionary(IComparer<TKey> keyComparer, IComparer<KeyValuePair<TKey, TValue>> pairComparer, RedBlackTree<KeyValuePair<TKey, TValue>> tree)
		{
			this.keyComparer = keyComparer;
			this.pairComparer = pairComparer;
			this.tree = tree;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x00089FBB File Offset: 0x000881BB
		public OrderedDictionary<TKey, TValue> Clone()
		{
			return new OrderedDictionary<TKey, TValue>(this.keyComparer, this.pairComparer, this.tree.Clone());
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00089FD9 File Offset: 0x000881D9
		private void NonCloneableType(Type t)
		{
			throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, t.FullName));
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00089FF0 File Offset: 0x000881F0
		public OrderedDictionary<TKey, TValue> CloneContents()
		{
			bool flag;
			if (!Util.IsCloneableType(typeof(TKey), out flag))
			{
				this.NonCloneableType(typeof(TKey));
			}
			bool flag2;
			if (!Util.IsCloneableType(typeof(TValue), out flag2))
			{
				this.NonCloneableType(typeof(TValue));
			}
			OrderedDictionary<TKey, TValue> orderedDictionary = new OrderedDictionary<TKey, TValue>(null, this.keyComparer, this.pairComparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this.tree)
			{
				TKey tkey;
				if (flag)
				{
					tkey = keyValuePair.Key;
				}
				else if (keyValuePair.Key == null)
				{
					tkey = default(TKey);
				}
				else
				{
					tkey = (TKey)((object)((ICloneable)((object)keyValuePair.Key)).Clone());
				}
				TValue tvalue;
				if (flag2)
				{
					tvalue = keyValuePair.Value;
				}
				else if (keyValuePair.Value == null)
				{
					tvalue = default(TValue);
				}
				else
				{
					tvalue = (TValue)((object)((ICloneable)((object)keyValuePair.Value)).Clone());
				}
				orderedDictionary.Add(tkey, tvalue);
			}
			return orderedDictionary;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0008A128 File Offset: 0x00088328
		public IComparer<TKey> Comparer
		{
			get
			{
				return this.keyComparer;
			}
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0008A130 File Offset: 0x00088330
		public OrderedDictionary<TKey, TValue>.View Reversed()
		{
			return new OrderedDictionary<TKey, TValue>.View(this, new RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester(this.tree.EntireRangeTester), true, true);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0008A14B File Offset: 0x0008834B
		public OrderedDictionary<TKey, TValue>.View Range(TKey from, bool fromInclusive, TKey to, bool toInclusive)
		{
			return new OrderedDictionary<TKey, TValue>.View(this, this.tree.DoubleBoundedRangeTester(OrderedDictionary<TKey, TValue>.NewPair(from), fromInclusive, OrderedDictionary<TKey, TValue>.NewPair(to), toInclusive), false, false);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0008A16F File Offset: 0x0008836F
		public OrderedDictionary<TKey, TValue>.View RangeFrom(TKey from, bool fromInclusive)
		{
			return new OrderedDictionary<TKey, TValue>.View(this, this.tree.LowerBoundedRangeTester(OrderedDictionary<TKey, TValue>.NewPair(from), fromInclusive), false, false);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0008A18B File Offset: 0x0008838B
		public OrderedDictionary<TKey, TValue>.View RangeTo(TKey to, bool toInclusive)
		{
			return new OrderedDictionary<TKey, TValue>.View(this, this.tree.UpperBoundedRangeTester(OrderedDictionary<TKey, TValue>.NewPair(to), toInclusive), false, false);
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0008A1A8 File Offset: 0x000883A8
		public sealed override bool Remove(TKey key)
		{
			KeyValuePair<TKey, TValue> keyValuePair = OrderedDictionary<TKey, TValue>.NewPair(key);
			KeyValuePair<TKey, TValue> keyValuePair2;
			return this.tree.Delete(keyValuePair, true, out keyValuePair2);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0008A1CB File Offset: 0x000883CB
		public sealed override void Clear()
		{
			this.tree.StopEnumerations();
			this.tree = new RedBlackTree<KeyValuePair<TKey, TValue>>(this.pairComparer);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0008A1EC File Offset: 0x000883EC
		public bool GetValueElseAdd(TKey key, ref TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair = OrderedDictionary<TKey, TValue>.NewPair(key, value);
			KeyValuePair<TKey, TValue> keyValuePair2;
			bool flag = this.tree.Insert(keyValuePair, DuplicatePolicy.DoNothing, out keyValuePair2);
			if (!flag)
			{
				value = keyValuePair2.Value;
			}
			return !flag;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0008A228 File Offset: 0x00088428
		public sealed override void Add(TKey key, TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair = OrderedDictionary<TKey, TValue>.NewPair(key, value);
			KeyValuePair<TKey, TValue> keyValuePair2;
			if (!this.tree.Insert(keyValuePair, DuplicatePolicy.DoNothing, out keyValuePair2))
			{
				throw new ArgumentException(Strings.KeyAlreadyPresent, "key");
			}
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0008A260 File Offset: 0x00088460
		public void Replace(TKey key, TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair = OrderedDictionary<TKey, TValue>.NewPair(key, value);
			KeyValuePair<TKey, TValue> keyValuePair2;
			if (!this.tree.Find(keyValuePair, true, true, out keyValuePair2))
			{
				throw new KeyNotFoundException(Strings.KeyNotFound);
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0008A294 File Offset: 0x00088494
		public void AddMany(IEnumerable<KeyValuePair<TKey, TValue>> keysAndValues)
		{
			if (keysAndValues == null)
			{
				throw new ArgumentNullException("keysAndValues");
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in keysAndValues)
			{
				this[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0008A2F8 File Offset: 0x000884F8
		public int RemoveMany(IEnumerable<TKey> keyCollectionToRemove)
		{
			if (keyCollectionToRemove == null)
			{
				throw new ArgumentNullException("keyCollectionToRemove");
			}
			int num = 0;
			foreach (TKey tkey in keyCollectionToRemove)
			{
				if (this.Remove(tkey))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x170003AF RID: 943
		public sealed override TValue this[TKey key]
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair;
				if (this.tree.Find(OrderedDictionary<TKey, TValue>.NewPair(key), false, false, out keyValuePair))
				{
					return keyValuePair.Value;
				}
				throw new KeyNotFoundException(Strings.KeyNotFound);
			}
			set
			{
				KeyValuePair<TKey, TValue> keyValuePair;
				this.tree.Insert(OrderedDictionary<TKey, TValue>.NewPair(key, value), DuplicatePolicy.ReplaceLast, out keyValuePair);
			}
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0008A3B4 File Offset: 0x000885B4
		public sealed override bool ContainsKey(TKey key)
		{
			KeyValuePair<TKey, TValue> keyValuePair;
			return this.tree.Find(OrderedDictionary<TKey, TValue>.NewPair(key), false, false, out keyValuePair);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0008A3D8 File Offset: 0x000885D8
		public sealed override bool TryGetValue(TKey key, out TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair;
			bool flag = this.tree.Find(OrderedDictionary<TKey, TValue>.NewPair(key), false, false, out keyValuePair);
			value = keyValuePair.Value;
			return flag;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x0008A407 File Offset: 0x00088607
		public sealed override int Count
		{
			get
			{
				return this.tree.ElementCount;
			}
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0008A414 File Offset: 0x00088614
		public sealed override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.tree.GetEnumerator();
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0008A421 File Offset: 0x00088621
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x04000F14 RID: 3860
		private IComparer<TKey> keyComparer;

		// Token: 0x04000F15 RID: 3861
		private IComparer<KeyValuePair<TKey, TValue>> pairComparer;

		// Token: 0x04000F16 RID: 3862
		private RedBlackTree<KeyValuePair<TKey, TValue>> tree;

		// Token: 0x020016EE RID: 5870
		[Serializable]
		public class View : DictionaryBase<TKey, TValue>
		{
			// Token: 0x0600DAF3 RID: 56051 RVA: 0x00504E4B File Offset: 0x0050304B
			internal View(OrderedDictionary<TKey, TValue> myDictionary, RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester rangeTester, bool entireTree, bool reversed)
			{
				this.myDictionary = myDictionary;
				this.rangeTester = rangeTester;
				this.entireTree = entireTree;
				this.reversed = reversed;
			}

			// Token: 0x0600DAF4 RID: 56052 RVA: 0x00504E70 File Offset: 0x00503070
			private bool KeyInView(TKey key)
			{
				return this.rangeTester(OrderedDictionary<TKey, TValue>.NewPair(key, default(TValue))) == 0;
			}

			// Token: 0x0600DAF5 RID: 56053 RVA: 0x00504E9C File Offset: 0x0050309C
			public sealed override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				if (this.reversed)
				{
					return this.myDictionary.tree.EnumerateRangeReversed(this.rangeTester).GetEnumerator();
				}
				return this.myDictionary.tree.EnumerateRange(this.rangeTester).GetEnumerator();
			}

			// Token: 0x17000DC6 RID: 3526
			// (get) Token: 0x0600DAF6 RID: 56054 RVA: 0x00504EE8 File Offset: 0x005030E8
			public sealed override int Count
			{
				get
				{
					if (this.entireTree)
					{
						return this.myDictionary.Count;
					}
					return this.myDictionary.tree.CountRange(this.rangeTester);
				}
			}

			// Token: 0x0600DAF7 RID: 56055 RVA: 0x00504F14 File Offset: 0x00503114
			public sealed override bool ContainsKey(TKey key)
			{
				return this.KeyInView(key) && this.myDictionary.ContainsKey(key);
			}

			// Token: 0x0600DAF8 RID: 56056 RVA: 0x00504F2D File Offset: 0x0050312D
			public sealed override bool TryGetValue(TKey key, out TValue value)
			{
				if (!this.KeyInView(key))
				{
					value = default(TValue);
					return false;
				}
				return this.myDictionary.TryGetValue(key, out value);
			}

			// Token: 0x17000DC7 RID: 3527
			public sealed override TValue this[TKey key]
			{
				get
				{
					return base[key];
				}
				set
				{
					if (!this.KeyInView(key))
					{
						throw new ArgumentException(Strings.OutOfViewRange, "key");
					}
					this.myDictionary[key] = value;
				}
			}

			// Token: 0x0600DAFB RID: 56059 RVA: 0x00504F7F File Offset: 0x0050317F
			public sealed override bool Remove(TKey key)
			{
				return this.KeyInView(key) && this.myDictionary.Remove(key);
			}

			// Token: 0x0600DAFC RID: 56060 RVA: 0x00504F98 File Offset: 0x00503198
			public sealed override void Clear()
			{
				if (this.entireTree)
				{
					this.myDictionary.Clear();
					return;
				}
				this.myDictionary.tree.DeleteRange(this.rangeTester);
			}

			// Token: 0x0600DAFD RID: 56061 RVA: 0x00504FC5 File Offset: 0x005031C5
			public OrderedDictionary<TKey, TValue>.View Reversed()
			{
				return new OrderedDictionary<TKey, TValue>.View(this.myDictionary, this.rangeTester, this.entireTree, !this.reversed);
			}

			// Token: 0x04005C3F RID: 23615
			private OrderedDictionary<TKey, TValue> myDictionary;

			// Token: 0x04005C40 RID: 23616
			private RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester rangeTester;

			// Token: 0x04005C41 RID: 23617
			private bool entireTree;

			// Token: 0x04005C42 RID: 23618
			private bool reversed;
		}
	}
}
