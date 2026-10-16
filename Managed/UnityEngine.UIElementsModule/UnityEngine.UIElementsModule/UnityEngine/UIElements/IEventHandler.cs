using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023D RID: 573
	public interface IEventHandler
	{
		// Token: 0x0600110B RID: 4363
		void SendEvent(EventBase e);

		// Token: 0x0600110C RID: 4364
		void HandleEvent(EventBase evt);

		// Token: 0x0600110D RID: 4365
		bool HasTrickleDownHandlers();

		// Token: 0x0600110E RID: 4366
		bool HasBubbleUpHandlers();
	}
}
