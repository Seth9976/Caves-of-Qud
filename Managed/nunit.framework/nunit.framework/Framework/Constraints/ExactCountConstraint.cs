using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200014D RID: 333
	public class ExactCountConstraint : PrefixConstraint
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00017AD3 File Offset: 0x00015CD3
		public ExactCountConstraint(int expectedCount, IConstraint itemConstraint)
			: base(itemConstraint)
		{
			this.expectedCount = expectedCount;
			base.DescriptionPrefix = ((expectedCount == 0) ? "no item" : ((expectedCount == 1) ? "exactly one item" : string.Format("exactly {0} items", expectedCount)));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00017B10 File Offset: 0x00015D10
		public override ConstraintResult ApplyTo(object actual)
		{
			if (!(actual is IEnumerable))
			{
				throw new ArgumentException("The actual value must be an IEnumerable", "actual");
			}
			int num = 0;
			foreach (object obj in ((IEnumerable)actual))
			{
				if (base.BaseConstraint.ApplyTo(obj).IsSuccess)
				{
					num++;
				}
			}
			return new ConstraintResult(this, actual, num == this.expectedCount);
		}

		// Token: 0x0400020E RID: 526
		private int expectedCount;
	}
}
