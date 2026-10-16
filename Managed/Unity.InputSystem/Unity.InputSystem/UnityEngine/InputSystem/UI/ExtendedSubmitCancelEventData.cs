using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008A RID: 138
	internal class ExtendedSubmitCancelEventData : BaseEventData, INavigationEventData
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0003BE18 File Offset: 0x0003A018
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0003BE20 File Offset: 0x0003A020
		public InputDevice device { get; set; }

		// Token: 0x06000B26 RID: 2854 RVA: 0x0003BE29 File Offset: 0x0003A029
		public ExtendedSubmitCancelEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}
	}
}
