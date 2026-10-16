using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000115 RID: 277
	public class LessThanConstraint : ComparisonConstraint
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x00014395 File Offset: 0x00012595
		public LessThanConstraint(object expected)
			: base(expected, true, false, false, "less than")
		{
		}
	}
}
