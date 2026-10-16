using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200011D RID: 285
	public class NotConstraint : PrefixConstraint
	{
		// Token: 0x060008F5 RID: 2293 RVA: 0x00014DDC File Offset: 0x00012FDC
		public NotConstraint(IConstraint baseConstraint)
			: base(baseConstraint)
		{
			base.DescriptionPrefix = "not";
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00014DF0 File Offset: 0x00012FF0
		public override ConstraintResult ApplyTo(object actual)
		{
			ConstraintResult constraintResult = base.BaseConstraint.ApplyTo(actual);
			return new ConstraintResult(this, constraintResult.ActualValue, !constraintResult.IsSuccess);
		}
	}
}
