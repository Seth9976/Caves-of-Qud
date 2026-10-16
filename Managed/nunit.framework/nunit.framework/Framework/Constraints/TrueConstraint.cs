using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000149 RID: 329
	public class TrueConstraint : Constraint
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x000178F6 File Offset: 0x00015AF6
		public TrueConstraint()
			: base(new object[0])
		{
			this.Description = "True";
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00017910 File Offset: 0x00015B10
		public override ConstraintResult ApplyTo(object actual)
		{
			return new ConstraintResult(this, actual, true.Equals(actual));
		}
	}
}
