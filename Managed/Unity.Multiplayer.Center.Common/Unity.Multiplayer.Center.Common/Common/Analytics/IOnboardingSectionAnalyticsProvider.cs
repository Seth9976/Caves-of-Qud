using System;

namespace Unity.Multiplayer.Center.Common.Analytics
{
	// Token: 0x02000010 RID: 16
	public interface IOnboardingSectionAnalyticsProvider
	{
		// Token: 0x0600001D RID: 29
		void SendInteractionEvent(InteractionDataType type, string displayName);
	}
}
