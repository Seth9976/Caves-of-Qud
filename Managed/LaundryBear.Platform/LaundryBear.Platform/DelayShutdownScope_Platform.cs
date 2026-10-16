using System;
using UnityEngine;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000032 RID: 50
	public struct DelayShutdownScope_Platform : IDisposable
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000025CD File Offset: 0x000007CD
		public DelayShutdownScope_Platform(ICanDelayShutdown canDelayShutdown)
		{
			this.didDelay = false;
			if (canDelayShutdown == null)
			{
				Debug.LogError("Argument is null");
				this.canDelayShutdown = null;
				return;
			}
			this.canDelayShutdown = canDelayShutdown;
			if (this.canDelayShutdown.IsUserHandlingShutdownDelay)
			{
				return;
			}
			canDelayShutdown.BeginDelayShutdown();
			this.didDelay = true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000260D File Offset: 0x0000080D
		public void Dispose()
		{
			if (this.canDelayShutdown == null)
			{
				Debug.LogError("DelayShutdownScope_Platform must have not been initialized properly. This should never be initialized with the default constructor.");
				return;
			}
			if (!this.didDelay)
			{
				return;
			}
			this.canDelayShutdown.BeginDelayShutdown();
		}

		// Token: 0x04000071 RID: 113
		private ICanDelayShutdown canDelayShutdown;

		// Token: 0x04000072 RID: 114
		private bool didDelay;
	}
}
