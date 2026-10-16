using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F6 RID: 246
	public abstract class ComparisonAdapter
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00012774 File Offset: 0x00010974
		public static ComparisonAdapter Default
		{
			get
			{
				return new ComparisonAdapter.DefaultComparisonAdapter();
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001277B File Offset: 0x0001097B
		public static ComparisonAdapter For(IComparer comparer)
		{
			return new ComparisonAdapter.ComparerAdapter(comparer);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00012783 File Offset: 0x00010983
		public static ComparisonAdapter For<T>(IComparer<T> comparer)
		{
			return new ComparisonAdapter.ComparerAdapter<T>(comparer);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001278B File Offset: 0x0001098B
		public static ComparisonAdapter For<T>(Comparison<T> comparer)
		{
			return new ComparisonAdapter.ComparisonAdapterForComparison<T>(comparer);
		}

		// Token: 0x06000790 RID: 1936
		public abstract int Compare(object expected, object actual);

		// Token: 0x0200019A RID: 410
		private class DefaultComparisonAdapter : ComparisonAdapter.ComparerAdapter
		{
			// Token: 0x06000B02 RID: 2818 RVA: 0x0001AE1A File Offset: 0x0001901A
			public DefaultComparisonAdapter()
				: base(NUnitComparer.Default)
			{
			}
		}

		// Token: 0x0200019B RID: 411
		private class ComparerAdapter : ComparisonAdapter
		{
			// Token: 0x06000B03 RID: 2819 RVA: 0x0001AE27 File Offset: 0x00019027
			public ComparerAdapter(IComparer comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B04 RID: 2820 RVA: 0x0001AE36 File Offset: 0x00019036
			public override int Compare(object expected, object actual)
			{
				return this.comparer.Compare(expected, actual);
			}

			// Token: 0x040002AA RID: 682
			private readonly IComparer comparer;
		}

		// Token: 0x0200019C RID: 412
		private class ComparerAdapter<T> : ComparisonAdapter
		{
			// Token: 0x06000B05 RID: 2821 RVA: 0x0001AE45 File Offset: 0x00019045
			public ComparerAdapter(IComparer<T> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B06 RID: 2822 RVA: 0x0001AE54 File Offset: 0x00019054
			public override int Compare(object expected, object actual)
			{
				if (!typeof(T).GetTypeInfo().IsAssignableFrom(expected.GetType().GetTypeInfo()))
				{
					throw new ArgumentException("Cannot compare " + expected.ToString());
				}
				if (!typeof(T).GetTypeInfo().IsAssignableFrom(actual.GetType().GetTypeInfo()))
				{
					throw new ArgumentException("Cannot compare to " + actual.ToString());
				}
				return this.comparer.Compare((T)((object)expected), (T)((object)actual));
			}

			// Token: 0x040002AB RID: 683
			private readonly IComparer<T> comparer;
		}

		// Token: 0x0200019D RID: 413
		private class ComparisonAdapterForComparison<T> : ComparisonAdapter
		{
			// Token: 0x06000B07 RID: 2823 RVA: 0x0001AEE6 File Offset: 0x000190E6
			public ComparisonAdapterForComparison(Comparison<T> comparer)
			{
				this.comparison = comparer;
			}

			// Token: 0x06000B08 RID: 2824 RVA: 0x0001AEF8 File Offset: 0x000190F8
			public override int Compare(object expected, object actual)
			{
				if (!typeof(T).GetTypeInfo().IsAssignableFrom(expected.GetType().GetTypeInfo()))
				{
					throw new ArgumentException("Cannot compare " + expected.ToString());
				}
				if (!typeof(T).GetTypeInfo().IsAssignableFrom(actual.GetType().GetTypeInfo()))
				{
					throw new ArgumentException("Cannot compare to " + actual.ToString());
				}
				return this.comparison((T)((object)expected), (T)((object)actual));
			}

			// Token: 0x040002AC RID: 684
			private readonly Comparison<T> comparison;
		}
	}
}
