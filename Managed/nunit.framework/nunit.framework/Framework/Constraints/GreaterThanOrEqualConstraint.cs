using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000111 RID: 273
	public class GreaterThanOrEqualConstraint : ComparisonConstraint
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x0001435C File Offset: 0x0001255C
		public GreaterThanOrEqualConstraint(object expected)
			: base(expected, false, true, true, "greater than or equal to")
		{
		}
	}
}
