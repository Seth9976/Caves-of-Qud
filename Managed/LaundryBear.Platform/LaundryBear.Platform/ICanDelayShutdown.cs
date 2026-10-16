using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000031 RID: 49
	public interface ICanDelayShutdown
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009A RID: 154
		// (set) Token: 0x0600009B RID: 155
		bool IsUserHandlingShutdownDelay { get; set; }

		// Token: 0x0600009C RID: 156
		void BeginDelayShutdown();

		// Token: 0x0600009D RID: 157
		void EndShutdownDelay();
	}
}
