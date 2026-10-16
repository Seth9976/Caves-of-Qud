using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004D RID: 77
	public class AxisEventData : BaseEventData
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00018106 File Offset: 0x00016306
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001810E File Offset: 0x0001630E
		public Vector2 moveVector { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00018117 File Offset: 0x00016317
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0001811F File Offset: 0x0001631F
		public MoveDirection moveDir { get; set; }

		// Token: 0x06000533 RID: 1331 RVA: 0x00018128 File Offset: 0x00016328
		public AxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}
	}
}
