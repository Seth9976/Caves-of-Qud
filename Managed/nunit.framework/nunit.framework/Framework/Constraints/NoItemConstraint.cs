using System;
using System.Collections;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200011C RID: 284
	public class NoItemConstraint : PrefixConstraint
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x00014D2E File Offset: 0x00012F2E
		public NoItemConstraint(IConstraint itemConstraint)
			: base(itemConstraint)
		{
			base.DescriptionPrefix = "no item";
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00014D42 File Offset: 0x00012F42
		public override string DisplayName
		{
			get
			{
				return "None";
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00014D4C File Offset: 0x00012F4C
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
					return new ConstraintResult(this, actual, ConstraintStatus.Failure);
				}
			}
			return new ConstraintResult(this, actual, ConstraintStatus.Success);
		}
	}
}
