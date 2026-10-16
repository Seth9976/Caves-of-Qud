using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000137 RID: 311
	public class RangeConstraint : Constraint
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x00016B64 File Offset: 0x00014D64
		public RangeConstraint(IComparable from, IComparable to)
			: base(new object[] { from, to })
		{
			if (this.comparer.Compare(from, to) > 0)
			{
				throw new ArgumentException("from must be less than to");
			}
			this.from = from;
			this.to = to;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00016BB9 File Offset: 0x00014DB9
		public override string Description
		{
			get
			{
				return string.Format("in range ({0},{1})", this.from, this.to);
			}
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00016BD4 File Offset: 0x00014DD4
		public override ConstraintResult ApplyTo(object actual)
		{
			if (this.from == null || this.to == null || actual == null)
			{
				throw new ArgumentException("Cannot compare using a null reference", "actual");
			}
			bool flag = this.comparer.Compare(this.from, actual) <= 0 && this.comparer.Compare(this.to, actual) >= 0;
			return new ConstraintResult(this, actual, flag);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00016C3D File Offset: 0x00014E3D
		public RangeConstraint Using(IComparer comparer)
		{
			this.comparer = ComparisonAdapter.For(comparer);
			return this;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00016C4C File Offset: 0x00014E4C
		public RangeConstraint Using<T>(IComparer<T> comparer)
		{
			this.comparer = ComparisonAdapter.For<T>(comparer);
			return this;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00016C5B File Offset: 0x00014E5B
		public RangeConstraint Using<T>(Comparison<T> comparer)
		{
			this.comparer = ComparisonAdapter.For<T>(comparer);
			return this;
		}

		// Token: 0x040001EA RID: 490
		private readonly IComparable from;

		// Token: 0x040001EB RID: 491
		private readonly IComparable to;

		// Token: 0x040001EC RID: 492
		private ComparisonAdapter comparer = ComparisonAdapter.Default;
	}
}
