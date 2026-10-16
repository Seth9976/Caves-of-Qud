using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000116 RID: 278
	public class LessThanOrEqualConstraint : ComparisonConstraint
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x000143A6 File Offset: 0x000125A6
		public LessThanOrEqualConstraint(object expected)
			: base(expected, true, true, false, "less than or equal to")
		{
		}
	}
}
