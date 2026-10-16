using System;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000144 RID: 324
	public class ThrowsConstraint : PrefixConstraint
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x0001760E File Offset: 0x0001580E
		public ThrowsConstraint(IConstraint baseConstraint)
			: base(baseConstraint)
		{
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00017617 File Offset: 0x00015817
		public Exception ActualException
		{
			get
			{
				return this.caughtException;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001761F File Offset: 0x0001581F
		public override string Description
		{
			get
			{
				return base.BaseConstraint.Description;
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001762C File Offset: 0x0001582C
		public override ConstraintResult ApplyTo(object actual)
		{
			this.caughtException = ThrowsConstraint.ExceptionInterceptor.Intercept(actual);
			return new ThrowsConstraint.ThrowsConstraintResult(this, this.caughtException, (this.caughtException != null) ? base.BaseConstraint.ApplyTo(this.caughtException) : null);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00017662 File Offset: 0x00015862
		public override ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del)
		{
			return this.ApplyTo(new ThrowsConstraint.GenericInvocationDescriptor<TActual>(del));
		}

		// Token: 0x040001FF RID: 511
		private Exception caughtException;

		// Token: 0x020001B5 RID: 437
		private class ThrowsConstraintResult : ConstraintResult
		{
			// Token: 0x06000B44 RID: 2884 RVA: 0x0001B46D File Offset: 0x0001966D
			public ThrowsConstraintResult(ThrowsConstraint constraint, Exception caughtException, ConstraintResult baseResult)
				: base(constraint, caughtException)
			{
				if (caughtException != null && baseResult.IsSuccess)
				{
					base.Status = ConstraintStatus.Success;
				}
				else
				{
					base.Status = ConstraintStatus.Failure;
				}
				this.baseResult = baseResult;
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x0001B499 File Offset: 0x00019699
			public override void WriteActualValueTo(MessageWriter writer)
			{
				if (base.ActualValue == null)
				{
					writer.Write("no exception thrown");
					return;
				}
				this.baseResult.WriteActualValueTo(writer);
			}

			// Token: 0x040002CC RID: 716
			private readonly ConstraintResult baseResult;
		}

		// Token: 0x020001B6 RID: 438
		internal class ExceptionInterceptor
		{
			// Token: 0x06000B46 RID: 2886 RVA: 0x00002050 File Offset: 0x00000250
			private ExceptionInterceptor()
			{
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x0001B4BC File Offset: 0x000196BC
			internal static Exception Intercept(object invocation)
			{
				ThrowsConstraint.IInvocationDescriptor invocationDescriptor = ThrowsConstraint.ExceptionInterceptor.GetInvocationDescriptor(invocation);
				if (AsyncInvocationRegion.IsAsyncOperation(invocationDescriptor.Delegate))
				{
					using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(invocationDescriptor.Delegate))
					{
						try
						{
							object obj = invocationDescriptor.Invoke();
							asyncInvocationRegion.WaitForPendingOperationsToComplete(obj);
							return null;
						}
						catch (Exception ex)
						{
							return ex;
						}
					}
				}
				Exception ex;
				try
				{
					invocationDescriptor.Invoke();
					ex = null;
				}
				catch (Exception ex)
				{
				}
				return ex;
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x0001B544 File Offset: 0x00019744
			private static ThrowsConstraint.IInvocationDescriptor GetInvocationDescriptor(object actual)
			{
				ThrowsConstraint.IInvocationDescriptor invocationDescriptor = actual as ThrowsConstraint.IInvocationDescriptor;
				if (invocationDescriptor == null)
				{
					TestDelegate testDelegate = actual as TestDelegate;
					if (testDelegate != null)
					{
						invocationDescriptor = new ThrowsConstraint.VoidInvocationDescriptor(testDelegate);
					}
					else
					{
						AsyncTestDelegate asyncTestDelegate = actual as AsyncTestDelegate;
						if (asyncTestDelegate != null)
						{
							invocationDescriptor = new ThrowsConstraint.GenericInvocationDescriptor<Task>(() => asyncTestDelegate());
						}
					}
				}
				if (invocationDescriptor == null)
				{
					throw new ArgumentException(string.Format("The actual value must be a TestDelegate or AsyncTestDelegate but was {0}", actual.GetType().Name), "actual");
				}
				return invocationDescriptor;
			}
		}

		// Token: 0x020001B7 RID: 439
		internal class GenericInvocationDescriptor<T> : ThrowsConstraint.IInvocationDescriptor
		{
			// Token: 0x06000B49 RID: 2889 RVA: 0x0001B5BE File Offset: 0x000197BE
			public GenericInvocationDescriptor(ActualValueDelegate<T> del)
			{
				this._del = del;
			}

			// Token: 0x06000B4A RID: 2890 RVA: 0x0001B5CD File Offset: 0x000197CD
			public object Invoke()
			{
				return this._del();
			}

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0001B5DF File Offset: 0x000197DF
			public Delegate Delegate
			{
				get
				{
					return this._del;
				}
			}

			// Token: 0x040002CD RID: 717
			private readonly ActualValueDelegate<T> _del;
		}

		// Token: 0x020001B8 RID: 440
		private interface IInvocationDescriptor
		{
			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000B4C RID: 2892
			Delegate Delegate { get; }

			// Token: 0x06000B4D RID: 2893
			object Invoke();
		}

		// Token: 0x020001B9 RID: 441
		private class VoidInvocationDescriptor : ThrowsConstraint.IInvocationDescriptor
		{
			// Token: 0x06000B4E RID: 2894 RVA: 0x0001B5E7 File Offset: 0x000197E7
			public VoidInvocationDescriptor(TestDelegate del)
			{
				this._del = del;
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x0001B5F6 File Offset: 0x000197F6
			public object Invoke()
			{
				this._del();
				return null;
			}

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001B604 File Offset: 0x00019804
			public Delegate Delegate
			{
				get
				{
					return this._del;
				}
			}

			// Token: 0x040002CE RID: 718
			private readonly TestDelegate _del;
		}
	}
}
