using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000E6 RID: 230
	public class AllItemsConstraint : PrefixConstraint
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00011997 File Offset: 0x0000FB97
		public AllItemsConstraint(IConstraint itemConstraint)
			: base(itemConstraint)
		{
			base.DescriptionPrefix = "all items";
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000119AB File Offset: 0x0000FBAB
		public override string DisplayName
		{
			get
			{
				return "All";
			}
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000119B4 File Offset: 0x0000FBB4
		public override ConstraintResult ApplyTo(object actual)
		{
			if (!(actual is IEnumerable))
			{
				throw new ArgumentException("The actual value must be an IEnumerable", "actual");
			}
			foreach (object obj in ((IEnumerable)actual))
			{
				if (!base.BaseConstraint.ApplyTo(obj).IsSuccess)
				{
					return new ConstraintResult(this, actual, ConstraintStatus.Failure);
				}
			}
			return new ConstraintResult(this, actual, ConstraintStatus.Success);
		}
	}
}
