using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200012A RID: 298
	public class OrOperator : BinaryOperator
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x00016580 File Offset: 0x00014780
		public OrOperator()
		{
			this.left_precedence = (this.right_precedence = 3);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000165A3 File Offset: 0x000147A3
		public override IConstraint ApplyOperator(IConstraint left, IConstraint right)
		{
			return new OrConstraint(left, right);
		}
	}
}
