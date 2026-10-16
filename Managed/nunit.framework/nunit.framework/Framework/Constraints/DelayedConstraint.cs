using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000100 RID: 256
	public class DelayedConstraint : PrefixConstraint
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00013523 File Offset: 0x00011723
		public DelayedConstraint(IConstraint baseConstraint, int delayInMilliseconds)
			: this(baseConstraint, delayInMilliseconds, 0)
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001352E File Offset: 0x0001172E
		public DelayedConstraint(IConstraint baseConstraint, int delayInMilliseconds, int pollingInterval)
			: base(baseConstraint)
		{
			if (delayInMilliseconds < 0)
			{
				throw new ArgumentException("Cannot check a condition in the past", "delayInMilliseconds");
			}
			this.delayInMilliseconds = delayInMilliseconds;
			this.pollingInterval = pollingInterval;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00013559 File Offset: 0x00011759
		public override string Description
		{
			get
			{
				return string.Format("{0} after {1} millisecond delay", base.BaseConstraint.Description, this.delayInMilliseconds);
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001357C File Offset: 0x0001177C
		public override ConstraintResult ApplyTo(object actual)
		{
			long num = Stopwatch.GetTimestamp();
			long num2 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.delayInMilliseconds));
			if (this.pollingInterval > 0)
			{
				long num3 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.pollingInterval));
				while ((num = Stopwatch.GetTimestamp()) < num2)
				{
					if (num3 > num)
					{
						Thread.Sleep((int)DelayedConstraint.TimestampDiff((num2 < num3) ? num2 : num3, num).TotalMilliseconds);
					}
					num3 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.pollingInterval));
					if (base.BaseConstraint.ApplyTo(actual).IsSuccess)
					{
						return new ConstraintResult(this, actual, true);
					}
				}
			}
			if ((num = Stopwatch.GetTimestamp()) < num2)
			{
				Thread.Sleep((int)DelayedConstraint.TimestampDiff(num2, num).TotalMilliseconds);
			}
			return new ConstraintResult(this, actual, base.BaseConstraint.ApplyTo(actual).IsSuccess);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00013650 File Offset: 0x00011850
		public override ConstraintResult ApplyTo<TActual>(ActualValueDelegate<TActual> del)
		{
			long num = Stopwatch.GetTimestamp();
			long num2 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.delayInMilliseconds));
			object obj;
			if (this.pollingInterval > 0)
			{
				long num3 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.pollingInterval));
				while ((num = Stopwatch.GetTimestamp()) < num2)
				{
					if (num3 > num)
					{
						Thread.Sleep((int)DelayedConstraint.TimestampDiff((num2 < num3) ? num2 : num3, num).TotalMilliseconds);
					}
					num3 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.pollingInterval));
					obj = DelayedConstraint.InvokeDelegate<TActual>(del);
					try
					{
						if (base.BaseConstraint.ApplyTo(obj).IsSuccess)
						{
							return new ConstraintResult(this, obj, true);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			if ((num = Stopwatch.GetTimestamp()) < num2)
			{
				Thread.Sleep((int)DelayedConstraint.TimestampDiff(num2, num).TotalMilliseconds);
			}
			obj = DelayedConstraint.InvokeDelegate<TActual>(del);
			return new ConstraintResult(this, obj, base.BaseConstraint.ApplyTo(obj).IsSuccess);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00013750 File Offset: 0x00011950
		private static object InvokeDelegate<T>(ActualValueDelegate<T> del)
		{
			if (AsyncInvocationRegion.IsAsyncOperation(del))
			{
				using (AsyncInvocationRegion asyncInvocationRegion = AsyncInvocationRegion.Create(del))
				{
					return asyncInvocationRegion.WaitForPendingOperationsToComplete(del());
				}
			}
			return del();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000137A8 File Offset: 0x000119A8
		public override ConstraintResult ApplyTo<TActual>(ref TActual actual)
		{
			long num = Stopwatch.GetTimestamp();
			long num2 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.delayInMilliseconds));
			if (this.pollingInterval > 0)
			{
				long num3 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.pollingInterval));
				while ((num = Stopwatch.GetTimestamp()) < num2)
				{
					if (num3 > num)
					{
						Thread.Sleep((int)DelayedConstraint.TimestampDiff((num2 < num3) ? num2 : num3, num).TotalMilliseconds);
					}
					num3 = DelayedConstraint.TimestampOffset(num, TimeSpan.FromMilliseconds((double)this.pollingInterval));
					try
					{
						if (base.BaseConstraint.ApplyTo(actual).IsSuccess)
						{
							return new ConstraintResult(this, actual, true);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			if ((num = Stopwatch.GetTimestamp()) < num2)
			{
				Thread.Sleep((int)DelayedConstraint.TimestampDiff(num2, num).TotalMilliseconds);
			}
			return new ConstraintResult(this, actual, base.BaseConstraint.ApplyTo(actual).IsSuccess);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000138C4 File Offset: 0x00011AC4
		protected override string GetStringRepresentation()
		{
			return string.Format("<after {0} {1}>", this.delayInMilliseconds, base.BaseConstraint);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000138E1 File Offset: 0x00011AE1
		private static long TimestampOffset(long timestamp, TimeSpan offset)
		{
			return timestamp + (long)(offset.TotalSeconds * (double)Stopwatch.Frequency);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000138F4 File Offset: 0x00011AF4
		private static TimeSpan TimestampDiff(long timestamp1, long timestamp2)
		{
			return TimeSpan.FromSeconds((double)(timestamp1 - timestamp2) / (double)Stopwatch.Frequency);
		}

		// Token: 0x040001BC RID: 444
		private readonly int delayInMilliseconds;

		// Token: 0x040001BD RID: 445
		private readonly int pollingInterval;
	}
}
