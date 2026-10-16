using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200010C RID: 268
	public class FalseConstraint : Constraint
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00013F37 File Offset: 0x00012137
		public FalseConstraint()
			: base(new object[0])
		{
			this.Description = "False";
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00013F50 File Offset: 0x00012150
		public override ConstraintResult ApplyTo(object actual)
		{
			return new ConstraintResult(this, actual, false.Equals(actual));
		}
	}
}
