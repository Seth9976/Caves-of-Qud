using System;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000145 RID: 325
	public class ThrowsExceptionConstraint : Constraint
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00017670 File Offset: 0x00015870
		public override string Description
		{
			get
			{
				return "an exception to be thrown";
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00017678 File Offset: 0x00015878
		public override ConstraintResult ApplyTo(object actual)
		{
			TestDelegate testDelegate = actual as TestDelegate;
			Exception ex = null;
			if (testDelegate != null)
			{
				try
				{
					testDelegate();
				}
				catch (Exception ex)
				{
				}
			}
			AsyncTestDelegate asyncTestDelegate = actual as AsyncTestDelegate;
			if (asyncTestDelegate != null)
			{
				using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(asyncTestDelegate))
				{
					try
					{
						Task task = asyncTestDelegate();
						asyncInvocationRegion.WaitForPendingOperationsToComplete(task);
					}
					catch (Exception ex)
					{
					}
				}
			}
			if (testDelegate == null && asyncTestDelegate == null)
			{
				throw new ArgumentException(string.Format("The actual value must be a TestDelegate or AsyncTestDelegate but was {0}", actual.GetType().Name), "actual");
			}
			return new ThrowsExceptionConstraint.ThrowsExceptionConstraintResult(this, ex);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00017724 File Offset: 0x00015924
		protected override object GetTestObject<TActual>(ActualValueDelegate<TActual> del)
		{
			return new TestDelegate(delegate
			{
				del();
			});
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00011D90 File Offset: 0x0000FF90
		public ThrowsExceptionConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x020001BA RID: 442
		private class ThrowsExceptionConstraintResult : ConstraintResult
		{
			// Token: 0x06000B51 RID: 2897 RVA: 0x0001B60C File Offset: 0x0001980C
			public ThrowsExceptionConstraintResult(ThrowsExceptionConstraint constraint, Exception caughtException)
				: base(constraint, caughtException, caughtException != null)
			{
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x0001B61A File Offset: 0x0001981A
			public override void WriteActualValueTo(MessageWriter writer)
			{
				if (base.Status == ConstraintStatus.Failure)
				{
					writer.Write("no exception thrown");
					return;
				}
				base.WriteActualValueTo(writer);
			}
		}
	}
}
