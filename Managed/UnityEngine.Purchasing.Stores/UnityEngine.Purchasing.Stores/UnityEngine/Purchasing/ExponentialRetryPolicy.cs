using System;
using System.Runtime.CompilerServices;
using UnityEngine.Purchasing.Stores.Util;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200008B RID: 139
	internal class ExponentialRetryPolicy : IRetryPolicy
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0000AA03 File Offset: 0x00008C03
		public ExponentialRetryPolicy(int baseRetryDelay = 1000, int maxRetryDelay = 30000, int exponentialFactor = 2)
		{
			this.m_BaseRetryDelay = baseRetryDelay;
			this.m_MaxRetryDelay = maxRetryDelay;
			this.m_ExponentialFactor = exponentialFactor;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000AA20 File Offset: 0x00008C20
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public void Invoke(Action<Action> actionToTry, [global::System.Runtime.CompilerServices.Nullable(2)] Action onRetryAction)
		{
			ExponentialRetryPolicy.<>c__DisplayClass4_0 CS$<>8__locals1 = new ExponentialRetryPolicy.<>c__DisplayClass4_0();
			CS$<>8__locals1.onRetryAction = onRetryAction;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.actionToTry = actionToTry;
			CS$<>8__locals1.currentRetryDelay = this.m_BaseRetryDelay;
			CS$<>8__locals1.actionToTry(new Action(CS$<>8__locals1.<Invoke>g__Retry|0));
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000AA6B File Offset: 0x00008C6B
		private int AdjustDelay(int delay)
		{
			return Math.Min(this.m_MaxRetryDelay, delay * this.m_ExponentialFactor);
		}

		// Token: 0x040001D1 RID: 465
		private readonly int m_BaseRetryDelay;

		// Token: 0x040001D2 RID: 466
		private readonly int m_MaxRetryDelay;

		// Token: 0x040001D3 RID: 467
		private readonly int m_ExponentialFactor;
	}
}
