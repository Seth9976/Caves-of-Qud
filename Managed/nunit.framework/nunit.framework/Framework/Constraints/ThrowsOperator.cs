using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200012F RID: 303
	public class ThrowsOperator : SelfResolvingOperator
	{
		// Token: 0x06000946 RID: 2374 RVA: 0x0001663A File Offset: 0x0001483A
		public ThrowsOperator()
		{
			this.left_precedence = 1;
			this.right_precedence = 100;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00016651 File Offset: 0x00014851
		public override void Reduce(ConstraintBuilder.ConstraintStack stack)
		{
			if (base.RightContext == null || base.RightContext is BinaryOperator)
			{
				stack.Push(new ThrowsExceptionConstraint());
				return;
			}
			stack.Push(new ThrowsConstraint(stack.Pop()));
		}
	}
}
