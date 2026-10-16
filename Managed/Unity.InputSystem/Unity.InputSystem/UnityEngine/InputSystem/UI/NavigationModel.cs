using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x0200008E RID: 142
	internal struct NavigationModel
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0003E80C File Offset: 0x0003CA0C
		public void Reset()
		{
			this.move = Vector2.zero;
		}

		// Token: 0x04000428 RID: 1064
		public Vector2 move;

		// Token: 0x04000429 RID: 1065
		public int consecutiveMoveCount;

		// Token: 0x0400042A RID: 1066
		public MoveDirection lastMoveDirection;

		// Token: 0x0400042B RID: 1067
		public float lastMoveTime;

		// Token: 0x0400042C RID: 1068
		public AxisEventData eventData;

		// Token: 0x0400042D RID: 1069
		public InputDevice device;
	}
}
