using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200011B RID: 283
	public class NaNConstraint : Constraint
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00014CF0 File Offset: 0x00012EF0
		public override string Description
		{
			get
			{
				return "NaN";
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00014CF7 File Offset: 0x00012EF7
		public override ConstraintResult ApplyTo(object actual)
		{
			return new ConstraintResult(this, actual, (actual is double && double.IsNaN((double)actual)) || (actual is float && float.IsNaN((float)actual)));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00011D90 File Offset: 0x0000FF90
		public NaNConstraint()
			: base(new object[0])
		{
		}
	}
}
