using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200013B RID: 315
	public class SameAsConstraint : Constraint
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x00016D06 File Offset: 0x00014F06
		public SameAsConstraint(object expected)
			: base(new object[] { expected })
		{
			this.expected = expected;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00016D1F File Offset: 0x00014F1F
		public override string Description
		{
			get
			{
				return "same as " + MsgUtils.FormatValue(this.expected);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00016D38 File Offset: 0x00014F38
		public override ConstraintResult ApplyTo(object actual)
		{
			bool flag = this.expected == actual;
			return new ConstraintResult(this, actual, flag);
		}

		// Token: 0x040001EE RID: 494
		private readonly object expected;
	}
}
