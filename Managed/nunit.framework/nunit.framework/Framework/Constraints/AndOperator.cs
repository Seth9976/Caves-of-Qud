using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000123 RID: 291
	public class AndOperator : BinaryOperator
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x000163FC File Offset: 0x000145FC
		public AndOperator()
		{
			this.left_precedence = (this.right_precedence = 2);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001641F File Offset: 0x0001461F
		public override IConstraint ApplyOperator(IConstraint left, IConstraint right)
		{
			return new AndConstraint(left, right);
		}
	}
}
