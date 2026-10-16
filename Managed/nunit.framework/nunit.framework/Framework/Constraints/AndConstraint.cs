using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000E7 RID: 231
	public class AndConstraint : BinaryConstraint
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x00011A44 File Offset: 0x0000FC44
		public AndConstraint(IConstraint left, IConstraint right)
			: base(left, right)
		{
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00011A4E File Offset: 0x0000FC4E
		public override string Description
		{
			get
			{
				return this.Left.Description + " and " + this.Right.Description;
			}
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00011A70 File Offset: 0x0000FC70
		public override ConstraintResult ApplyTo(object actual)
		{
			ConstraintResult constraintResult = this.Left.ApplyTo(actual);
			ConstraintResult constraintResult2 = (constraintResult.IsSuccess ? this.Right.ApplyTo(actual) : new ConstraintResult(this.Right, actual));
			return new AndConstraint.AndConstraintResult(this, actual, constraintResult, constraintResult2);
		}

		// Token: 0x02000195 RID: 405
		private class AndConstraintResult : ConstraintResult
		{
			// Token: 0x06000AF3 RID: 2803 RVA: 0x0001AD3B File Offset: 0x00018F3B
			public AndConstraintResult(AndConstraint constraint, object actual, ConstraintResult leftResult, ConstraintResult rightResult)
				: base(constraint, actual, leftResult.IsSuccess && rightResult.IsSuccess)
			{
				this.leftResult = leftResult;
				this.rightResult = rightResult;
			}

			// Token: 0x06000AF4 RID: 2804 RVA: 0x0001AD66 File Offset: 0x00018F66
			public override void WriteActualValueTo(MessageWriter writer)
			{
				if (this.IsSuccess)
				{
					base.WriteActualValueTo(writer);
					return;
				}
				if (!this.leftResult.IsSuccess)
				{
					this.leftResult.WriteActualValueTo(writer);
					return;
				}
				this.rightResult.WriteActualValueTo(writer);
			}

			// Token: 0x0400029E RID: 670
			private ConstraintResult leftResult;

			// Token: 0x0400029F RID: 671
			private ConstraintResult rightResult;
		}
	}
}
