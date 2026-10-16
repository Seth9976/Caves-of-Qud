using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000146 RID: 326
	public class ThrowsNothingConstraint : Constraint
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00013F0F File Offset: 0x0001210F
		public override string Description
		{
			get
			{
				return "No Exception to be thrown";
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001773D File Offset: 0x0001593D
		public override ConstraintResult ApplyTo(object actual)
		{
			this.caughtException = ThrowsConstraint.ExceptionInterceptor.Intercept(actual);
			return new ConstraintResult(this, this.caughtException, this.caughtException == null);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00017760 File Offset: 0x00015960
		public override ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del)
		{
			return this.ApplyTo(new ThrowsConstraint.GenericInvocationDescriptor<TActual>(del));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00011D90 File Offset: 0x0000FF90
		public ThrowsNothingConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x04000200 RID: 512
		private Exception caughtException;
	}
}
