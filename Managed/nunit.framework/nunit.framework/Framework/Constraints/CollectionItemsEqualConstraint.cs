using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F1 RID: 241
	public abstract class CollectionItemsEqualConstraint : CollectionConstraint
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x00011FBE File Offset: 0x000101BE
		protected CollectionItemsEqualConstraint()
		{
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00011FD1 File Offset: 0x000101D1
		protected CollectionItemsEqualConstraint(object arg)
			: base(arg)
		{
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00011FE5 File Offset: 0x000101E5
		public CollectionItemsEqualConstraint IgnoreCase
		{
			get
			{
				this.comparer.IgnoreCase = true;
				return this;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00011FF4 File Offset: 0x000101F4
		public CollectionItemsEqualConstraint Using(IComparer comparer)
		{
			this.comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
			return this;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001200D File Offset: 0x0001020D
		public CollectionItemsEqualConstraint Using<T>(IComparer<T> comparer)
		{
			this.comparer.ExternalComparers.Add(EqualityAdapter.For<T>(comparer));
			return this;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00012026 File Offset: 0x00010226
		public CollectionItemsEqualConstraint Using<T>(Comparison<T> comparer)
		{
			this.comparer.ExternalComparers.Add(EqualityAdapter.For<T>(comparer));
			return this;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001203F File Offset: 0x0001023F
		public CollectionItemsEqualConstraint Using(IEqualityComparer comparer)
		{
			this.comparer.ExternalComparers.Add(EqualityAdapter.For(comparer));
			return this;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00012058 File Offset: 0x00010258
		public CollectionItemsEqualConstraint Using<T>(IEqualityComparer<T> comparer)
		{
			this.comparer.ExternalComparers.Add(EqualityAdapter.For<T>(comparer));
			return this;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00012071 File Offset: 0x00010271
		internal CollectionItemsEqualConstraint Using(EqualityAdapter adapter)
		{
			this.comparer.ExternalComparers.Add(adapter);
			return this;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00012088 File Offset: 0x00010288
		protected bool ItemsEqual(object x, object y)
		{
			Tolerance @default = Tolerance.Default;
			return this.comparer.AreEqual(x, y, ref @default);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000120AA File Offset: 0x000102AA
		protected CollectionTally Tally(IEnumerable c)
		{
			return new CollectionTally(this.comparer, c);
		}

		// Token: 0x0400019D RID: 413
		private readonly NUnitEqualityComparer comparer = NUnitEqualityComparer.Default;
	}
}
