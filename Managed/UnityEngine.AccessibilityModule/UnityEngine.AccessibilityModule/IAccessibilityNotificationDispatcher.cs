using System;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000014 RID: 20
	public interface IAccessibilityNotificationDispatcher
	{
		// Token: 0x06000128 RID: 296
		void SendAnnouncement(string announcement);

		// Token: 0x06000129 RID: 297
		void SendScreenChanged(AccessibilityNode nodeToFocus = null);

		// Token: 0x0600012A RID: 298
		void SendLayoutChanged(AccessibilityNode nodeToFocus = null);
	}
}
