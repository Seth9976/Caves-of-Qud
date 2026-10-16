using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000273 RID: 627
	public interface INavigationEvent
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600124D RID: 4685
		EventModifiers modifiers { get; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x0600124E RID: 4686
		NavigationDeviceType deviceType { get; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600124F RID: 4687
		bool shiftKey { get; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001250 RID: 4688
		bool ctrlKey { get; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001251 RID: 4689
		bool commandKey { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001252 RID: 4690
		bool altKey { get; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001253 RID: 4691
		bool actionKey { get; }
	}
}
