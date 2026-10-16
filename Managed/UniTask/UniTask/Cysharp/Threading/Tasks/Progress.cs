using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000030 RID: 48
	public static class Progress
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003C0A File Offset: 0x00001E0A
		public static IProgress<T> Create<T>(Action<T> handler)
		{
			if (handler == null)
			{
				return Progress.NullProgress<T>.Instance;
			}
			return new Progress.AnonymousProgress<T>(handler);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C1B File Offset: 0x00001E1B
		public static IProgress<T> CreateOnlyValueChanged<T>(Action<T> handler, IEqualityComparer<T> comparer = null)
		{
			if (handler == null)
			{
				return Progress.NullProgress<T>.Instance;
			}
			return new Progress.OnlyValueChangedProgress<T>(handler, comparer ?? UnityEqualityComparer.GetDefault<T>());
		}

		// Token: 0x02000161 RID: 353
		private sealed class NullProgress<T> : IProgress<T>
		{
			// Token: 0x06000771 RID: 1905 RVA: 0x00011528 File Offset: 0x0000F728
			private NullProgress()
			{
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x00011530 File Offset: 0x0000F730
			public void Report(T value)
			{
			}

			// Token: 0x040001CC RID: 460
			public static readonly IProgress<T> Instance = new Progress.NullProgress<T>();
		}

		// Token: 0x02000162 RID: 354
		private sealed class AnonymousProgress<T> : IProgress<T>
		{
			// Token: 0x06000774 RID: 1908 RVA: 0x0001153E File Offset: 0x0000F73E
			public AnonymousProgress(Action<T> action)
			{
				this.action = action;
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x0001154D File Offset: 0x0000F74D
			public void Report(T value)
			{
				this.action(value);
			}

			// Token: 0x040001CD RID: 461
			private readonly Action<T> action;
		}

		// Token: 0x02000163 RID: 355
		private sealed class OnlyValueChangedProgress<T> : IProgress<T>
		{
			// Token: 0x06000776 RID: 1910 RVA: 0x0001155B File Offset: 0x0000F75B
			public OnlyValueChangedProgress(Action<T> action, IEqualityComparer<T> comparer)
			{
				this.action = action;
				this.comparer = comparer;
				this.isFirstCall = true;
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x00011578 File Offset: 0x0000F778
			public void Report(T value)
			{
				if (this.isFirstCall)
				{
					this.isFirstCall = false;
				}
				else if (this.comparer.Equals(value, this.latestValue))
				{
					return;
				}
				this.latestValue = value;
				this.action(value);
			}

			// Token: 0x040001CE RID: 462
			private readonly Action<T> action;

			// Token: 0x040001CF RID: 463
			private readonly IEqualityComparer<T> comparer;

			// Token: 0x040001D0 RID: 464
			private bool isFirstCall;

			// Token: 0x040001D1 RID: 465
			private T latestValue;
		}
	}
}
