using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000124 RID: 292
	public class AttributeOperator : SelfResolvingOperator
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00016428 File Offset: 0x00014628
		public AttributeOperator(Type type)
		{
			this.type = type;
			this.left_precedence = (this.right_precedence = 1);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00016452 File Offset: 0x00014652
		public override void Reduce(ConstraintBuilder.ConstraintStack stack)
		{
			if (base.RightContext == null || base.RightContext is BinaryOperator)
			{
				stack.Push(new AttributeExistsConstraint(this.type));
				return;
			}
			stack.Push(new AttributeConstraint(this.type, stack.Pop()));
		}

		// Token: 0x040001DA RID: 474
		private readonly Type type;
	}
}
