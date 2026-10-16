using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200011E RID: 286
	public class NullConstraint : Constraint
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x00014E1F File Offset: 0x0001301F
		public NullConstraint()
			: base(new object[0])
		{
			this.Description = "null";
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00014E38 File Offset: 0x00013038
		public override ConstraintResult ApplyTo(object actual)
		{
			NUnitEqualityComparer.CheckGameObjectReference<object>(ref actual);
			return new ConstraintResult(this, actual, actual == null);
		}
	}
}
