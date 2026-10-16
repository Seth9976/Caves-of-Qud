using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Stores.Util;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200008D RID: 141
	internal class GoogleConnectionRetryPolicy : IRetryPolicy
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public GoogleConnectionRetryPolicy(int baseRetryDelay = 2000, int maxRetryDelay = 30000, int exponentialFactor = 2)
		{
			this.m_BaseRetryDelay = baseRetryDelay;
			this.m_MaxRetryDelay = maxRetryDelay;
			this.m_ExponentialFactor = exponentialFactor;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000ABC9 File Offset: 0x00008DC9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public void Invoke(Action<Action> actionToTry, [global::System.Runtime.CompilerServices.Nullable(2)] Action onRetryAction)
		{
			GoogleConnectionRetryPolicy.<>c__DisplayClass4_0 CS$<>8__locals1 = new GoogleConnectionRetryPolicy.<>c__DisplayClass4_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.actionToTry = actionToTry;
			CS$<>8__locals1.onRetryAction = onRetryAction;
			CS$<>8__locals1.retryAttempts = 0;
			CS$<>8__locals1.currentRetryDelay = this.m_BaseRetryDelay;
			CS$<>8__locals1.<Invoke>g__WaitAndRetry|0();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000ABFD File Offset: 0x00008DFD
		private int AdjustDelay(int delay)
		{
			return Math.Min(this.m_MaxRetryDelay, delay * this.m_ExponentialFactor);
		}

		// Token: 0x040001D6 RID: 470
		private readonly int m_BaseRetryDelay;

		// Token: 0x040001D7 RID: 471
		private readonly int m_MaxRetryDelay;

		// Token: 0x040001D8 RID: 472
		private readonly int m_ExponentialFactor;
	}
}
