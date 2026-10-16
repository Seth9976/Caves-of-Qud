using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000086 RID: 134
	internal class ExtendedAxisEventData : AxisEventData, INavigationEventData
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0003B8EA File Offset: 0x00039AEA
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0003B8F2 File Offset: 0x00039AF2
		public InputDevice device { get; set; }

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003B8FB File Offset: 0x00039AFB
		public ExtendedAxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0003B904 File Offset: 0x00039B04
		public override string ToString()
		{
			return string.Format("MoveDir: {0}\nMoveVector: {1}", base.moveDir, base.moveVector);
		}
	}
}
