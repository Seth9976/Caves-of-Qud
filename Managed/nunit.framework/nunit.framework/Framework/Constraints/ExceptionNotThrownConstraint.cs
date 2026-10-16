using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200010B RID: 267
	internal class ExceptionNotThrownConstraint : Constraint
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00013F0F File Offset: 0x0001210F
		public override string Description
		{
			get
			{
				return "No Exception to be thrown";
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00013F18 File Offset: 0x00012118
		public override ConstraintResult ApplyTo(object actual)
		{
			Exception ex = actual as Exception;
			return new ConstraintResult(this, ex, ex == null);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00011D90 File Offset: 0x0000FF90
		public ExceptionNotThrownConstraint()
			: base(new object[0])
		{
		}
	}
}
