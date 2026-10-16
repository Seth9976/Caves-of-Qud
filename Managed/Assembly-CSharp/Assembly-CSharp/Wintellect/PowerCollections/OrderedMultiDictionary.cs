using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002FE RID: 766
	[Serializable]
	public class OrderedMultiDictionary<TKey, TValue> : MultiDictionaryBase<TKey, TValue>, ICloneable
	{
		// Token: 0x06001BB9 RID: 7097 RVA: 0x0008A429 File Offset: 0x00088629
		private static KeyValuePair<TKey, TValue> NewPair(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0008A434 File Offset: 0x00088634
		private static KeyValuePair<TKey, TValue> NewPair(TKey key)
		{
			return new KeyValuePair<TKey, TValue>(key, default(TValue));
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0008A450 File Offset: 0x00088650
		private RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester KeyRange(TKey key)
		{
			return (KeyValuePair<TKey, TValue> pair) => this.keyComparer.Compare(pair.Key, key);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x0008A470 File Offset: 0x00088670
		private RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester DoubleBoundedKeyRangeTester(TKey first, bool firstInclusive, TKey last, bool lastInclusive)
		{
			return delegate(KeyValuePair<TKey, TValue> pair)
			{
				if (firstInclusive)
				{
					if (this.keyComparer.Compare(first, pair.Key) > 0)
					{
						return -1;
					}
				}
				else if (this.keyComparer.Compare(first, pair.Key) >= 0)
				{
					return -1;
				}
				if (lastInclusive)
				{
					if (this.keyComparer.Compare(last, pair.Key) < 0)
					{
						return 1;
					}
				}
				else if (this.keyComparer.Compare(last, pair.Key) <= 0)
				{
					return 1;
				}
				return 0;
			};
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x0008A4A6 File Offset: 0x000886A6
		private RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester LowerBoundedKeyRangeTester(TKey first, bool inclusive)
		{
			return delegate(KeyValuePair<TKey, TValue> pair)
			{
				if (inclusive)
				{
					if (this.keyComparer.Compare(first, pair.Key) > 0)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (this.keyComparer.Compare(first, pair.Key) >= 0)
					{
						return -1;
					}
					return 0;
				}
			};
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x0008A4CD File Offset: 0x000886CD
		private RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester UpperBoundedKeyRangeTester(TKey last, bool inclusive)
		{
			return delegate(KeyValuePair<TKey, TValue> pair)
			{
				if (inclusive)
				{
					if (this.keyComparer.Compare(last, pair.Key) < 0)
					{
						return 1;
					}
					return 0;
				}
				else
				{
					if (this.keyComparer.Compare(last, pair.Key) <= 0)
					{
						return 1;
					}
					return 0;
				}
			};
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x0008A4F4 File Offset: 0x000886F4
		public OrderedMultiDictionary(bool allowDuplicateValues)
			: this(allowDuplicateValues, Comparers.DefaultComparer<TKey>(), Comparers.DefaultComparer<TValue>())
		{
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x0008A507 File Offset: 0x00088707
		public OrderedMultiDictionary(bool allowDuplicateValues, Comparison<TKey> keyComparison)
			: this(allowDuplicateValues, Comparers.ComparerFromComparison<TKey>(keyComparison), Comparers.DefaultComparer<TValue>())
		{
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x0008A51B File Offset: 0x0008871B
		public OrderedMultiDictionary(bool allowDuplicateValues, Comparison<TKey> keyComparison, Comparison<TValue> valueComparison)
			: this(allowDuplicateValues, Comparers.ComparerFromComparison<TKey>(keyComparison), Comparers.ComparerFromComparison<TValue>(valueComparison))
		{
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0008A530 File Offset: 0x00088730
		public OrderedMultiDictionary(bool allowDuplicateValues, IComparer<TKey> keyComparer)
			: this(allowDuplicateValues, keyComparer, Comparers.DefaultComparer<TValue>())
		{
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0008A540 File Offset: 0x00088740
		public OrderedMultiDictionary(bool allowDuplicateValues, IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				throw new ArgumentNullException("keyComparer");
			}
			if (valueComparer == null)
			{
				throw new ArgumentNullException("valueComparer");
			}
			this.allowDuplicateValues = allowDuplicateValues;
			this.keyComparer = keyComparer;
			this.valueComparer = valueComparer;
			this.comparer = Comparers.ComparerPairFromKeyValueComparers<TKey, TValue>(keyComparer, valueComparer);
			this.tree = new RedBlackTree<KeyValuePair<TKey, TValue>>(this.comparer);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0008A5A2 File Offset: 0x000887A2
		private OrderedMultiDictionary(bool allowDuplicateValues, int keyCount, IComparer<TKey> keyComparer, IComparer<TValue> valueComparer, IComparer<KeyValuePair<TKey, TValue>> comparer, RedBlackTree<KeyValuePair<TKey, TValue>> tree)
		{
			this.allowDuplicateValues = allowDuplicateValues;
			this.keyCount = keyCount;
			this.keyComparer = keyComparer;
			this.valueComparer = valueComparer;
			this.comparer = comparer;
			this.tree = tree;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0008A5D8 File Offset: 0x000887D8
		public sealed override void Add(TKey key, TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair = OrderedMultiDictionary<TKey, TValue>.NewPair(key, value);
			if (!this.ContainsKey(key))
			{
				this.keyCount++;
			}
			KeyValuePair<TKey, TValue> keyValuePair2;
			this.tree.Insert(keyValuePair, this.allowDuplicateValues ? DuplicatePolicy.InsertLast : DuplicatePolicy.ReplaceLast, out keyValuePair2);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0008A620 File Offset: 0x00088820
		public sealed override bool Remove(TKey key, TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair;
			bool flag = this.tree.Delete(OrderedMultiDictionary<TKey, TValue>.NewPair(key, value), false, out keyValuePair);
			if (flag && !this.ContainsKey(key))
			{
				this.keyCount--;
			}
			return flag;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0008A65C File Offset: 0x0008885C
		public sealed override bool Remove(TKey key)
		{
			if (this.tree.DeleteRange(this.KeyRange(key)) > 0)
			{
				this.keyCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0008A684 File Offset: 0x00088884
		public sealed override void Clear()
		{
			this.tree.StopEnumerations();
			this.tree = new RedBlackTree<KeyValuePair<TKey, TValue>>(this.comparer);
			this.keyCount = 0;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0008A6A9 File Offset: 0x000888A9
		public IComparer<TKey> KeyComparer
		{
			get
			{
				return this.keyComparer;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x0008A6B1 File Offset: 0x000888B1
		public IComparer<TValue> ValueComparer
		{
			get
			{
				return this.valueComparer;
			}
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x0008A6B9 File Offset: 0x000888B9
		protected sealed override bool EqualValues(TValue value1, TValue value2)
		{
			return this.valueComparer.Compare(value1, value2) == 0;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0008A6CB File Offset: 0x000888CB
		public sealed override int Count
		{
			get
			{
				return this.keyCount;
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x0008A6D4 File Offset: 0x000888D4
		public sealed override bool Contains(TKey key, TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair;
			return this.tree.Find(OrderedMultiDictionary<TKey, TValue>.NewPair(key, value), true, false, out keyValuePair);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x0008A6F8 File Offset: 0x000888F8
		public sealed override bool ContainsKey(TKey key)
		{
			KeyValuePair<TKey, TValue> keyValuePair;
			return this.tree.FirstItemInRange(this.KeyRange(key), out keyValuePair) >= 0;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x0008A71F File Offset: 0x0008891F
		private IEnumerator<TKey> EnumerateKeys(RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester rangeTester, bool reversed)
		{
			bool flag = true;
			TKey lastKey = default(TKey);
			IEnumerable<KeyValuePair<TKey, TValue>> enumerable;
			if (reversed)
			{
				enumerable = this.tree.EnumerateRangeReversed(rangeTester);
			}
			else
			{
				enumerable = this.tree.EnumerateRange(rangeTester);
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in enumerable)
			{
				if (flag || this.keyComparer.Compare(lastKey, keyValuePair.Key) != 0)
				{
					lastKey = keyValuePair.Key;
					yield return lastKey;
				}
				flag = false;
			}
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x0008A73C File Offset: 0x0008893C
		private IEnumerator<TValue> EnumerateValuesForKey(TKey key)
		{
			foreach (KeyValuePair<TKey, TValue> keyValuePair in this.tree.EnumerateRange(this.KeyRange(key)))
			{
				yield return keyValuePair.Value;
			}
			IEnumerator<KeyValuePair<TKey, TValue>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x0008A752 File Offset: 0x00088952
		protected sealed override bool TryEnumerateValuesForKey(TKey key, out IEnumerator<TValue> values)
		{
			if (this.ContainsKey(key))
			{
				values = this.EnumerateValuesForKey(key);
				return true;
			}
			values = null;
			return false;
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x0008A76C File Offset: 0x0008896C
		protected sealed override IEnumerator<TKey> EnumerateKeys()
		{
			return this.EnumerateKeys(new RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester(this.tree.EntireRangeTester), false);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x0008A786 File Offset: 0x00088986
		protected sealed override int CountValues(TKey key)
		{
			return this.tree.CountRange(this.KeyRange(key));
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x0008A79A File Offset: 0x0008899A
		protected sealed override int CountAllValues()
		{
			return this.tree.ElementCount;
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x0008A7A7 File Offset: 0x000889A7
		public OrderedMultiDictionary<TKey, TValue> Clone()
		{
			return new OrderedMultiDictionary<TKey, TValue>(this.allowDuplicateValues, this.keyCount, this.keyComparer, this.valueComparer, this.comparer, this.tree.Clone());
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0008A7D7 File Offset: 0x000889D7
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x0008A7DF File Offset: 0x000889DF
		private void NonCloneableType(Type t)
		{
			throw new InvalidOperationException(string.Format(Strings.TypeNotCloneable, t.FullName));
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0008A7F8 File Offset: 0x000889F8
		public OrderedMultiDictionary<TKey, TValue> CloneContents()
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
			OrderedMultiDictionary<TKey, TValue> orderedMultiDictionary = new OrderedMultiDictionary<TKey, TValue>(this.allowDuplicateValues, this.keyComparer, this.valueComparer);
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
				orderedMultiDictionary.Add(tkey, tvalue);
			}
			return orderedMultiDictionary;
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001BD9 RID: 7129 RVA: 0x0008A934 File Offset: 0x00088B34
		public sealed override ICollection<KeyValuePair<TKey, TValue>> KeyValuePairs
		{
			get
			{
				return new OrderedMultiDictionary<TKey, TValue>.KeyValuePairsCollection(this);
			}
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x0008A93C File Offset: 0x00088B3C
		public OrderedMultiDictionary<TKey, TValue>.View Reversed()
		{
			return new OrderedMultiDictionary<TKey, TValue>.View(this, new RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester(this.tree.EntireRangeTester), true, true);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x0008A957 File Offset: 0x00088B57
		public OrderedMultiDictionary<TKey, TValue>.View Range(TKey from, bool fromInclusive, TKey to, bool toInclusive)
		{
			return new OrderedMultiDictionary<TKey, TValue>.View(this, this.DoubleBoundedKeyRangeTester(from, fromInclusive, to, toInclusive), false, false);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x0008A96C File Offset: 0x00088B6C
		public OrderedMultiDictionary<TKey, TValue>.View RangeFrom(TKey from, bool fromInclusive)
		{
			return new OrderedMultiDictionary<TKey, TValue>.View(this, this.LowerBoundedKeyRangeTester(from, fromInclusive), false, false);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0008A97E File Offset: 0x00088B7E
		public OrderedMultiDictionary<TKey, TValue>.View RangeTo(TKey to, bool toInclusive)
		{
			return new OrderedMultiDictionary<TKey, TValue>.View(this, this.UpperBoundedKeyRangeTester(to, toInclusive), false, false);
		}

		// Token: 0x04000F17 RID: 3863
		private IComparer<TKey> keyComparer;

		// Token: 0x04000F18 RID: 3864
		private IComparer<TValue> valueComparer;

		// Token: 0x04000F19 RID: 3865
		private IComparer<KeyValuePair<TKey, TValue>> comparer;

		// Token: 0x04000F1A RID: 3866
		private RedBlackTree<KeyValuePair<TKey, TValue>> tree;

		// Token: 0x04000F1B RID: 3867
		private bool allowDuplicateValues;

		// Token: 0x04000F1C RID: 3868
		private int keyCount;

		// Token: 0x020016EF RID: 5871
		[Serializable]
		private sealed class KeyValuePairsCollection : ReadOnlyCollectionBase<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DAFE RID: 56062 RVA: 0x00504FE7 File Offset: 0x005031E7
			public KeyValuePairsCollection(OrderedMultiDictionary<TKey, TValue> myDictionary)
			{
				this.myDictionary = myDictionary;
			}

			// Token: 0x17000DC8 RID: 3528
			// (get) Token: 0x0600DAFF RID: 56063 RVA: 0x00504FF6 File Offset: 0x005031F6
			public override int Count
			{
				get
				{
					return this.myDictionary.CountAllValues();
				}
			}

			// Token: 0x0600DB00 RID: 56064 RVA: 0x00505003 File Offset: 0x00503203
			public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				return this.myDictionary.tree.GetEnumerator();
			}

			// Token: 0x0600DB01 RID: 56065 RVA: 0x00505018 File Offset: 0x00503218
			public override bool Contains(KeyValuePair<TKey, TValue> pair)
			{
				KeyValuePair<TKey, TValue> keyValuePair;
				return this.myDictionary.tree.Find(pair, true, false, out keyValuePair);
			}

			// Token: 0x04005C43 RID: 23619
			private OrderedMultiDictionary<TKey, TValue> myDictionary;
		}

		// Token: 0x020016F0 RID: 5872
		[Serializable]
		public class View : MultiDictionaryBase<TKey, TValue>
		{
			// Token: 0x0600DB02 RID: 56066 RVA: 0x0050503A File Offset: 0x0050323A
			internal View(OrderedMultiDictionary<TKey, TValue> myDictionary, RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester rangeTester, bool entireTree, bool reversed)
			{
				this.myDictionary = myDictionary;
				this.rangeTester = rangeTester;
				this.entireTree = entireTree;
				this.reversed = reversed;
			}

			// Token: 0x0600DB03 RID: 56067 RVA: 0x00505060 File Offset: 0x00503260
			private bool KeyInView(TKey key)
			{
				return this.rangeTester(OrderedMultiDictionary<TKey, TValue>.NewPair(key, default(TValue))) == 0;
			}

			// Token: 0x0600DB04 RID: 56068 RVA: 0x0050508A File Offset: 0x0050328A
			protected sealed override IEnumerator<TKey> EnumerateKeys()
			{
				return this.myDictionary.EnumerateKeys(this.rangeTester, this.reversed);
			}

			// Token: 0x0600DB05 RID: 56069 RVA: 0x005050A3 File Offset: 0x005032A3
			protected sealed override bool TryEnumerateValuesForKey(TKey key, out IEnumerator<TValue> values)
			{
				if (!this.KeyInView(key))
				{
					values = null;
					return false;
				}
				return this.myDictionary.TryEnumerateValuesForKey(key, out values);
			}

			// Token: 0x17000DC9 RID: 3529
			// (get) Token: 0x0600DB06 RID: 56070 RVA: 0x005050C0 File Offset: 0x005032C0
			public sealed override int Count
			{
				get
				{
					if (this.entireTree)
					{
						return this.myDictionary.Count;
					}
					int num = 0;
					using (IEnumerator<TKey> enumerator = this.myDictionary.EnumerateKeys(this.rangeTester, this.reversed))
					{
						while (enumerator.MoveNext())
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x0600DB07 RID: 56071 RVA: 0x00505128 File Offset: 0x00503328
			public sealed override bool ContainsKey(TKey key)
			{
				return this.KeyInView(key) && this.myDictionary.ContainsKey(key);
			}

			// Token: 0x0600DB08 RID: 56072 RVA: 0x00505141 File Offset: 0x00503341
			public sealed override bool Contains(TKey key, TValue value)
			{
				return this.KeyInView(key) && this.myDictionary.Contains(key, value);
			}

			// Token: 0x0600DB09 RID: 56073 RVA: 0x0050515B File Offset: 0x0050335B
			protected sealed override int CountValues(TKey key)
			{
				if (!this.KeyInView(key))
				{
					return 0;
				}
				return this.myDictionary.CountValues(key);
			}

			// Token: 0x0600DB0A RID: 56074 RVA: 0x00505174 File Offset: 0x00503374
			public sealed override void Add(TKey key, TValue value)
			{
				if (!this.KeyInView(key))
				{
					throw new ArgumentException(Strings.OutOfViewRange, "key");
				}
				this.myDictionary.Add(key, value);
			}

			// Token: 0x0600DB0B RID: 56075 RVA: 0x0050519C File Offset: 0x0050339C
			public sealed override bool Remove(TKey key)
			{
				return this.KeyInView(key) && this.myDictionary.Remove(key);
			}

			// Token: 0x0600DB0C RID: 56076 RVA: 0x005051B5 File Offset: 0x005033B5
			public sealed override bool Remove(TKey key, TValue value)
			{
				return this.KeyInView(key) && this.myDictionary.Remove(key, value);
			}

			// Token: 0x0600DB0D RID: 56077 RVA: 0x005051D0 File Offset: 0x005033D0
			public sealed override void Clear()
			{
				if (this.entireTree)
				{
					this.myDictionary.Clear();
					return;
				}
				this.myDictionary.keyCount -= this.Count;
				this.myDictionary.tree.DeleteRange(this.rangeTester);
			}

			// Token: 0x0600DB0E RID: 56078 RVA: 0x00505220 File Offset: 0x00503420
			public OrderedMultiDictionary<TKey, TValue>.View Reversed()
			{
				return new OrderedMultiDictionary<TKey, TValue>.View(this.myDictionary, this.rangeTester, this.entireTree, !this.reversed);
			}

			// Token: 0x04005C44 RID: 23620
			private OrderedMultiDictionary<TKey, TValue> myDictionary;

			// Token: 0x04005C45 RID: 23621
			private RedBlackTree<KeyValuePair<TKey, TValue>>.RangeTester rangeTester;

			// Token: 0x04005C46 RID: 23622
			private bool entireTree;

			// Token: 0x04005C47 RID: 23623
			private bool reversed;
		}
	}
}
