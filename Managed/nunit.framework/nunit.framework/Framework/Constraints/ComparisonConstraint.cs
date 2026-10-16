using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F7 RID: 247
	public abstract class ComparisonConstraint : Constraint
	{
		// Token: 0x06000792 RID: 1938 RVA: 0x00012794 File Offset: 0x00010994
		protected ComparisonConstraint(object value, bool lessComparisonResult, bool equalComparisonResult, bool greaterComparisonResult, string predicate)
			: base(new object[] { value })
		{
			this.expected = value;
			this.lessComparisonResult = lessComparisonResult;
			this.equalComparisonResult = equalComparisonResult;
			this.greaterComparisonResult = greaterComparisonResult;
			this.Description = predicate + " " + MsgUtils.FormatValue(this.expected);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000127F8 File Offset: 0x000109F8
		public override ConstraintResult ApplyTo(object actual)
		{
			if (this.expected == null)
			{
				throw new ArgumentException("Cannot compare using a null reference", "expected");
			}
			if (actual == null)
			{
				throw new ArgumentException("Cannot compare to null reference", "actual");
			}
			int num = this.comparer.Compare(this.expected, actual);
			bool flag = (num < 0 && this.greaterComparisonResult) || (num == 0 && this.equalComparisonResult) || (num > 0 && this.lessComparisonResult);
			return new ConstraintResult(this, actual, flag);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00012873 File Offset: 0x00010A73
		public ComparisonConstraint Using(IComparer comparer)
		{
			this.comparer = ComparisonAdapter.For(comparer);
			return this;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00012882 File Offset: 0x00010A82
		public ComparisonConstraint Using<T>(IComparer<T> comparer)
		{
			this.comparer = ComparisonAdapter.For<T>(comparer);
			return this;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00012891 File Offset: 0x00010A91
		public ComparisonConstraint Using<T>(Comparison<T> comparer)
		{
			this.comparer = ComparisonAdapter.For<T>(comparer);
			return this;
		}

		// Token: 0x040001A4 RID: 420
		protected object expected;

		// Token: 0x040001A5 RID: 421
		protected bool lessComparisonResult;

		// Token: 0x040001A6 RID: 422
		protected bool equalComparisonResult;

		// Token: 0x040001A7 RID: 423
		protected bool greaterComparisonResult;

		// Token: 0x040001A8 RID: 424
		private ComparisonAdapter comparer = ComparisonAdapter.Default;
	}
}
