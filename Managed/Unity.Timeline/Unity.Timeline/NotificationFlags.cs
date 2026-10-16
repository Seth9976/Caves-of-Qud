using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003A RID: 58
	[Flags]
	[Serializable]
	public enum NotificationFlags : short
	{
		// Token: 0x040000E0 RID: 224
		TriggerInEditMode = 1,
		// Token: 0x040000E1 RID: 225
		Retroactive = 2,
		// Token: 0x040000E2 RID: 226
		TriggerOnce = 4
	}
}
