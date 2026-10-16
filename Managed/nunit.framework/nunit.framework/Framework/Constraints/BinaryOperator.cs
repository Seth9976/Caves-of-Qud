using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000125 RID: 293
	public abstract class BinaryOperator : ConstraintOperator
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00016494 File Offset: 0x00014694
		public override void Reduce(ConstraintBuilder.ConstraintStack stack)
		{
			IConstraint constraint = stack.Pop();
			IConstraint constraint2 = stack.Pop();
			stack.Push(this.ApplyOperator(constraint2, constraint));
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x000164BD File Offset: 0x000146BD
		public override int LeftPrecedence
		{
			get
			{
				if (!(base.RightContext is CollectionOperator))
				{
					return base.LeftPrecedence;
				}
				return base.LeftPrecedence + 10;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000164DC File Offset: 0x000146DC
		public override int RightPrecedence
		{
			get
			{
				if (!(base.RightContext is CollectionOperator))
				{
					return base.RightPrecedence;
				}
				return base.RightPrecedence + 10;
			}
		}

		// Token: 0x0600092C RID: 2348
		public abstract IConstraint ApplyOperator(IConstraint left, IConstraint right);
	}
}
