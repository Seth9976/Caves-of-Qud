using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000131 RID: 305
	public class OrConstraint : BinaryConstraint
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00011A44 File Offset: 0x0000FC44
		public OrConstraint(IConstraint left, IConstraint right)
			: base(left, right)
		{
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001669E File Offset: 0x0001489E
		public override string Description
		{
			get
			{
				return this.Left.Description + " or " + this.Right.Description;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000166C0 File Offset: 0x000148C0
		public override ConstraintResult ApplyTo(object actual)
		{
			bool flag = this.Left.ApplyTo(actual).IsSuccess || this.Right.ApplyTo(actual).IsSuccess;
			return new ConstraintResult(this, actual, flag);
		}
	}
}
