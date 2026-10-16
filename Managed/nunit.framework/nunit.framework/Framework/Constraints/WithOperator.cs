using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000130 RID: 304
	public class WithOperator : PrefixOperator
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x00016685 File Offset: 0x00014885
		public WithOperator()
		{
			this.left_precedence = 1;
			this.right_precedence = 4;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0001669B File Offset: 0x0001489B
		public override IConstraint ApplyPrefix(IConstraint constraint)
		{
			return constraint;
		}
	}
}
