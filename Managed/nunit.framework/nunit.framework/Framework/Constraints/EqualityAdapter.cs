using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Compatibility;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000109 RID: 265
	public abstract class EqualityAdapter
	{
		// Token: 0x06000894 RID: 2196
		public abstract bool AreEqual(object x, object y);

		// Token: 0x06000895 RID: 2197 RVA: 0x00013E8B File Offset: 0x0001208B
		public virtual bool CanCompare(object x, object y)
		{
			return (x is string && y is string) || (!(x is IEnumerable) && !(y is IEnumerable));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00013EB2 File Offset: 0x000120B2
		public static EqualityAdapter For(IComparer comparer)
		{
			return new EqualityAdapter.ComparerAdapter(comparer);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00013EBA File Offset: 0x000120BA
		public static EqualityAdapter For(IEqualityComparer comparer)
		{
			return new EqualityAdapter.EqualityComparerAdapter(comparer);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00013EC2 File Offset: 0x000120C2
		public static EqualityAdapter For<TExpected, TActual>(Func<TExpected, TActual, bool> comparison)
		{
			return new EqualityAdapter.PredicateEqualityAdapter<TExpected, TActual>(comparison);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00013ECA File Offset: 0x000120CA
		public static EqualityAdapter For<T>(IEqualityComparer<T> comparer)
		{
			return new EqualityAdapter.EqualityComparerAdapter<T>(comparer);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00013ED2 File Offset: 0x000120D2
		public static EqualityAdapter For<T>(IComparer<T> comparer)
		{
			return new EqualityAdapter.ComparerAdapter<T>(comparer);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00013EDA File Offset: 0x000120DA
		public static EqualityAdapter For<T>(Comparison<T> comparer)
		{
			return new EqualityAdapter.ComparisonAdapter<T>(comparer);
		}

		// Token: 0x020001A0 RID: 416
		private class ComparerAdapter : EqualityAdapter
		{
			// Token: 0x06000B12 RID: 2834 RVA: 0x0001B02D File Offset: 0x0001922D
			public ComparerAdapter(IComparer comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x0001B03C File Offset: 0x0001923C
			public override bool AreEqual(object x, object y)
			{
				return this.comparer.Compare(x, y) == 0;
			}

			// Token: 0x040002B0 RID: 688
			private IComparer comparer;
		}

		// Token: 0x020001A1 RID: 417
		private class EqualityComparerAdapter : EqualityAdapter
		{
			// Token: 0x06000B14 RID: 2836 RVA: 0x0001B04E File Offset: 0x0001924E
			public EqualityComparerAdapter(IEqualityComparer comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B15 RID: 2837 RVA: 0x0001B05D File Offset: 0x0001925D
			public override bool AreEqual(object x, object y)
			{
				return this.comparer.Equals(x, y);
			}

			// Token: 0x040002B1 RID: 689
			private IEqualityComparer comparer;
		}

		// Token: 0x020001A2 RID: 418
		internal class PredicateEqualityAdapter<TActual, TExpected> : EqualityAdapter
		{
			// Token: 0x06000B16 RID: 2838 RVA: 0x0000C168 File Offset: 0x0000A368
			public override bool CanCompare(object x, object y)
			{
				return true;
			}

			// Token: 0x06000B17 RID: 2839 RVA: 0x0001B06C File Offset: 0x0001926C
			public override bool AreEqual(object x, object y)
			{
				return this._comparison((TActual)((object)y), (TExpected)((object)x));
			}

			// Token: 0x06000B18 RID: 2840 RVA: 0x0001B085 File Offset: 0x00019285
			public PredicateEqualityAdapter(Func<TActual, TExpected, bool> comparison)
			{
				this._comparison = comparison;
			}

			// Token: 0x040002B2 RID: 690
			private readonly Func<TActual, TExpected, bool> _comparison;
		}

		// Token: 0x020001A3 RID: 419
		private abstract class GenericEqualityAdapter<T> : EqualityAdapter
		{
			// Token: 0x06000B19 RID: 2841 RVA: 0x0001B094 File Offset: 0x00019294
			public override bool CanCompare(object x, object y)
			{
				return typeof(T).GetTypeInfo().IsAssignableFrom(x.GetType().GetTypeInfo()) && typeof(T).GetTypeInfo().IsAssignableFrom(y.GetType().GetTypeInfo());
			}

			// Token: 0x06000B1A RID: 2842 RVA: 0x0001B0E4 File Offset: 0x000192E4
			protected void ThrowIfNotCompatible(object x, object y)
			{
				if (!typeof(T).GetTypeInfo().IsAssignableFrom(x.GetType().GetTypeInfo()))
				{
					throw new ArgumentException("Cannot compare " + x.ToString());
				}
				if (!typeof(T).GetTypeInfo().IsAssignableFrom(y.GetType().GetTypeInfo()))
				{
					throw new ArgumentException("Cannot compare " + y.ToString());
				}
			}
		}

		// Token: 0x020001A4 RID: 420
		private class EqualityComparerAdapter<T> : EqualityAdapter.GenericEqualityAdapter<T>
		{
			// Token: 0x06000B1C RID: 2844 RVA: 0x0001B167 File Offset: 0x00019367
			public EqualityComparerAdapter(IEqualityComparer<T> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B1D RID: 2845 RVA: 0x0001B176 File Offset: 0x00019376
			public override bool AreEqual(object x, object y)
			{
				base.ThrowIfNotCompatible(x, y);
				return this.comparer.Equals((T)((object)x), (T)((object)y));
			}

			// Token: 0x040002B3 RID: 691
			private IEqualityComparer<T> comparer;
		}

		// Token: 0x020001A5 RID: 421
		private class ComparerAdapter<T> : EqualityAdapter.GenericEqualityAdapter<T>
		{
			// Token: 0x06000B1E RID: 2846 RVA: 0x0001B197 File Offset: 0x00019397
			public ComparerAdapter(IComparer<T> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B1F RID: 2847 RVA: 0x0001B1A6 File Offset: 0x000193A6
			public override bool AreEqual(object x, object y)
			{
				base.ThrowIfNotCompatible(x, y);
				return this.comparer.Compare((T)((object)x), (T)((object)y)) == 0;
			}

			// Token: 0x040002B4 RID: 692
			private IComparer<T> comparer;
		}

		// Token: 0x020001A6 RID: 422
		private class ComparisonAdapter<T> : EqualityAdapter.GenericEqualityAdapter<T>
		{
			// Token: 0x06000B20 RID: 2848 RVA: 0x0001B1CA File Offset: 0x000193CA
			public ComparisonAdapter(Comparison<T> comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06000B21 RID: 2849 RVA: 0x0001B1D9 File Offset: 0x000193D9
			public override bool AreEqual(object x, object y)
			{
				base.ThrowIfNotCompatible(x, y);
				return this.comparer((T)((object)x), (T)((object)y)) == 0;
			}

			// Token: 0x040002B5 RID: 693
			private Comparison<T> comparer;
		}
	}
}
