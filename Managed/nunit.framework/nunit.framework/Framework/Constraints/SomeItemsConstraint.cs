using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200013E RID: 318
	public class SomeItemsConstraint : PrefixConstraint
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x00016DF4 File Offset: 0x00014FF4
		public SomeItemsConstraint(IConstraint itemConstraint)
			: base(itemConstraint)
		{
			base.DescriptionPrefix = "some item";
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00016E08 File Offset: 0x00015008
		public override string DisplayName
		{
			get
			{
				return "Some";
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00016E10 File Offset: 0x00015010
		public override ConstraintResult ApplyTo(object actual)
		{
			if (!(actual is IEnumerable))
			{
				throw new ArgumentException("The actual value must be an IEnumerable", "actual");
			}
			foreach (object obj in ((IEnumerable)actual))
			{
				if (base.BaseConstraint.ApplyTo(obj).IsSuccess)
				{
					return new ConstraintResult(this, actual, ConstraintStatus.Success);
				}
			}
			return new ConstraintResult(this, actual, ConstraintStatus.Failure);
		}
	}
}
