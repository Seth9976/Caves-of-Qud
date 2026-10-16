using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F0 RID: 496
	internal interface IDragAndDrop
	{
		// Token: 0x06000F53 RID: 3923
		void StartDrag(StartDragArgs args, Vector3 pointerPosition);

		// Token: 0x06000F54 RID: 3924
		void UpdateDrag(Vector3 pointerPosition);

		// Token: 0x06000F55 RID: 3925
		void AcceptDrag();

		// Token: 0x06000F56 RID: 3926
		void DragCleanup();

		// Token: 0x06000F57 RID: 3927
		void SetVisualMode(DragVisualMode visualMode);

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F58 RID: 3928
		DragAndDropData data { get; }
	}
}
