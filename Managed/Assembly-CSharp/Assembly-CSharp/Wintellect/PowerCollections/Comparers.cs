using System;
using System.Collections.Generic;

namespace Wintellect.PowerCollections
{
	// Token: 0x020002F5 RID: 757
	internal static class Comparers
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x0008664C File Offset: 0x0008484C
		public static IComparer<T> ComparerFromComparison<T>(Comparison<T> comparison)
		{
			if (comparison == null)
			{
				throw new ArgumentNullException("comparison");
			}
			return new Comparers.ComparisonComparer<T>(comparison);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00086662 File Offset: 0x00084862
		public static IComparer<KeyValuePair<TKey, TValue>> ComparerKeyValueFromComparerKey<TKey, TValue>(IComparer<TKey> keyComparer)
		{
			if (keyComparer == null)
			{
				throw new ArgumentNullException("keyComparer");
			}
			return new Comparers.KeyValueComparer<TKey, TValue>(keyComparer);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00086678 File Offset: 0x00084878
		public static IEqualityComparer<KeyValuePair<TKey, TValue>> EqualityComparerKeyValueFromComparerKey<TKey, TValue>(IEqualityComparer<TKey> keyEqualityComparer)
		{
			if (keyEqualityComparer == null)
			{
				throw new ArgumentNullException("keyEqualityComparer");
			}
			return new Comparers.KeyValueEqualityComparer<TKey, TValue>(keyEqualityComparer);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0008668E File Offset: 0x0008488E
		public static IComparer<KeyValuePair<TKey, TValue>> ComparerPairFromKeyValueComparers<TKey, TValue>(IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
		{
			if (keyComparer == null)
			{
				throw new ArgumentNullException("keyComparer");
			}
			if (valueComparer == null)
			{
				throw new ArgumentNullException("valueComparer");
			}
			return new Comparers.PairComparer<TKey, TValue>(keyComparer, valueComparer);
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000866B3 File Offset: 0x000848B3
		public static IComparer<KeyValuePair<TKey, TValue>> ComparerKeyValueFromComparisonKey<TKey, TValue>(Comparison<TKey> keyComparison)
		{
			if (keyComparison == null)
			{
				throw new ArgumentNullException("keyComparison");
			}
			return new Comparers.ComparisonKeyValueComparer<TKey, TValue>(keyComparison);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000866CC File Offset: 0x000848CC
		public static IComparer<T> DefaultComparer<T>()
		{
			if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)) || typeof(IComparable).IsAssignableFrom(typeof(T)))
			{
				return Comparer<T>.Default;
			}
			throw new InvalidOperationException(string.Format(Strings.UncomparableType, typeof(T).FullName));
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00086733 File Offset: 0x00084933
		public static IComparer<KeyValuePair<TKey, TValue>> DefaultKeyValueComparer<TKey, TValue>()
		{
			return Comparers.ComparerKeyValueFromComparerKey<TKey, TValue>(Comparers.DefaultComparer<TKey>());
		}

		// Token: 0x020016D5 RID: 5845
		[Serializable]
		private class KeyValueEqualityComparer<TKey, TValue> : IEqualityComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DA6A RID: 55914 RVA: 0x0050390B File Offset: 0x00501B0B
			public KeyValueEqualityComparer(IEqualityComparer<TKey> keyEqualityComparer)
			{
				this.keyEqualityComparer = keyEqualityComparer;
			}

			// Token: 0x0600DA6B RID: 55915 RVA: 0x0050391A File Offset: 0x00501B1A
			public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.keyEqualityComparer.Equals(x.Key, y.Key);
			}

			// Token: 0x0600DA6C RID: 55916 RVA: 0x00503935 File Offset: 0x00501B35
			public int GetHashCode(KeyValuePair<TKey, TValue> obj)
			{
				return Util.GetHashCode<TKey>(obj.Key, this.keyEqualityComparer);
			}

			// Token: 0x0600DA6D RID: 55917 RVA: 0x00503949 File Offset: 0x00501B49
			public override bool Equals(object obj)
			{
				return obj is Comparers.KeyValueEqualityComparer<TKey, TValue> && object.Equals(this.keyEqualityComparer, ((Comparers.KeyValueEqualityComparer<TKey, TValue>)obj).keyEqualityComparer);
			}

			// Token: 0x0600DA6E RID: 55918 RVA: 0x0050396B File Offset: 0x00501B6B
			public override int GetHashCode()
			{
				return this.keyEqualityComparer.GetHashCode();
			}

			// Token: 0x04005BFC RID: 23548
			private IEqualityComparer<TKey> keyEqualityComparer;
		}

		// Token: 0x020016D6 RID: 5846
		[Serializable]
		private class KeyValueComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DA6F RID: 55919 RVA: 0x00503978 File Offset: 0x00501B78
			public KeyValueComparer(IComparer<TKey> keyComparer)
			{
				this.keyComparer = keyComparer;
			}

			// Token: 0x0600DA70 RID: 55920 RVA: 0x00503987 File Offset: 0x00501B87
			public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.keyComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x0600DA71 RID: 55921 RVA: 0x005039A2 File Offset: 0x00501BA2
			public override bool Equals(object obj)
			{
				return obj is Comparers.KeyValueComparer<TKey, TValue> && object.Equals(this.keyComparer, ((Comparers.KeyValueComparer<TKey, TValue>)obj).keyComparer);
			}

			// Token: 0x0600DA72 RID: 55922 RVA: 0x005039C4 File Offset: 0x00501BC4
			public override int GetHashCode()
			{
				return this.keyComparer.GetHashCode();
			}

			// Token: 0x04005BFD RID: 23549
			private IComparer<TKey> keyComparer;
		}

		// Token: 0x020016D7 RID: 5847
		[Serializable]
		private class PairComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DA73 RID: 55923 RVA: 0x005039D1 File Offset: 0x00501BD1
			public PairComparer(IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
			{
				this.keyComparer = keyComparer;
				this.valueComparer = valueComparer;
			}

			// Token: 0x0600DA74 RID: 55924 RVA: 0x005039E8 File Offset: 0x00501BE8
			public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				int num = this.keyComparer.Compare(x.Key, y.Key);
				if (num == 0)
				{
					return this.valueComparer.Compare(x.Value, y.Value);
				}
				return num;
			}

			// Token: 0x0600DA75 RID: 55925 RVA: 0x00503A2D File Offset: 0x00501C2D
			public override bool Equals(object obj)
			{
				return obj is Comparers.PairComparer<TKey, TValue> && object.Equals(this.keyComparer, ((Comparers.PairComparer<TKey, TValue>)obj).keyComparer) && object.Equals(this.valueComparer, ((Comparers.PairComparer<TKey, TValue>)obj).valueComparer);
			}

			// Token: 0x0600DA76 RID: 55926 RVA: 0x00503A69 File Offset: 0x00501C69
			public override int GetHashCode()
			{
				return this.keyComparer.GetHashCode() ^ this.valueComparer.GetHashCode();
			}

			// Token: 0x04005BFE RID: 23550
			private IComparer<TKey> keyComparer;

			// Token: 0x04005BFF RID: 23551
			private IComparer<TValue> valueComparer;
		}

		// Token: 0x020016D8 RID: 5848
		[Serializable]
		private class ComparisonComparer<T> : IComparer<T>
		{
			// Token: 0x0600DA77 RID: 55927 RVA: 0x00503A82 File Offset: 0x00501C82
			public ComparisonComparer(Comparison<T> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x0600DA78 RID: 55928 RVA: 0x00503A91 File Offset: 0x00501C91
			public int Compare(T x, T y)
			{
				return this.comparison(x, y);
			}

			// Token: 0x0600DA79 RID: 55929 RVA: 0x00503AA0 File Offset: 0x00501CA0
			public override bool Equals(object obj)
			{
				return obj is Comparers.ComparisonComparer<T> && this.comparison.Equals(((Comparers.ComparisonComparer<T>)obj).comparison);
			}

			// Token: 0x0600DA7A RID: 55930 RVA: 0x00503AC2 File Offset: 0x00501CC2
			public override int GetHashCode()
			{
				return this.comparison.GetHashCode();
			}

			// Token: 0x04005C00 RID: 23552
			private Comparison<T> comparison;
		}

		// Token: 0x020016D9 RID: 5849
		[Serializable]
		private class ComparisonKeyValueComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600DA7B RID: 55931 RVA: 0x00503ACF File Offset: 0x00501CCF
			public ComparisonKeyValueComparer(Comparison<TKey> comparison)
			{
				this.comparison = comparison;
			}

			// Token: 0x0600DA7C RID: 55932 RVA: 0x00503ADE File Offset: 0x00501CDE
			public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.comparison(x.Key, y.Key);
			}

			// Token: 0x0600DA7D RID: 55933 RVA: 0x00503AF9 File Offset: 0x00501CF9
			public override bool Equals(object obj)
			{
				return obj is Comparers.ComparisonKeyValueComparer<TKey, TValue> && this.comparison.Equals(((Comparers.ComparisonKeyValueComparer<TKey, TValue>)obj).comparison);
			}

			// Token: 0x0600DA7E RID: 55934 RVA: 0x00503B1B File Offset: 0x00501D1B
			public override int GetHashCode()
			{
				return this.comparison.GetHashCode();
			}

			// Token: 0x04005C01 RID: 23553
			private Comparison<TKey> comparison;
		}
	}
}
