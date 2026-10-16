using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000110 RID: 272
	public class GreaterThanConstraint : ComparisonConstraint
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x0001434B File Offset: 0x0001254B
		public GreaterThanConstraint(object expected)
			: base(expected, false, false, true, "greater than")
		{
		}
	}
}
