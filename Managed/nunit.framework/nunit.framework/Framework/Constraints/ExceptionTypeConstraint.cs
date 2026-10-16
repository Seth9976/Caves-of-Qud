using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200014F RID: 335
	public class ExceptionTypeConstraint : ExactTypeConstraint
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00017BBD File Offset: 0x00015DBD
		public ExceptionTypeConstraint(Type type)
			: base(type)
		{
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00017BC8 File Offset: 0x00015DC8
		public override ConstraintResult ApplyTo(object actual)
		{
			Exception ex = actual as Exception;
			if (actual != null && ex == null)
			{
				throw new ArgumentException("Actual value must be an Exception", "actual");
			}
			this.actualType = ((actual == null) ? null : actual.GetType());
			return new ExceptionTypeConstraint.ExceptionTypeConstraintResult(this, actual, this.actualType, this.Matches(actual));
		}

		// Token: 0x020001BC RID: 444
		private class ExceptionTypeConstraintResult : ConstraintResult
		{
			// Token: 0x06000B55 RID: 2901 RVA: 0x0001B646 File Offset: 0x00019846
			public ExceptionTypeConstraintResult(ExceptionTypeConstraint constraint, object caughtException, Type type, bool matches)
				: base(constraint, type, matches)
			{
				this.caughtException = caughtException;
			}

			// Token: 0x06000B56 RID: 2902 RVA: 0x0001B65C File Offset: 0x0001985C
			public override void WriteActualValueTo(MessageWriter writer)
			{
				if (base.Status == ConstraintStatus.Failure)
				{
					Exception ex = this.caughtException as Exception;
					if (ex == null)
					{
						base.WriteActualValueTo(writer);
						return;
					}
					writer.WriteActualValue(ex);
				}
			}

			// Token: 0x040002D0 RID: 720
			private readonly object caughtException;
		}
	}
}
