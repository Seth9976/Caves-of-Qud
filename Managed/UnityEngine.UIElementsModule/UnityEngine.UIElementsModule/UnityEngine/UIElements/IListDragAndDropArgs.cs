using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F3 RID: 499
	internal interface IListDragAndDropArgs
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F67 RID: 3943
		object target { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F68 RID: 3944
		int insertAtIndex { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000F69 RID: 3945
		int parentId { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000F6A RID: 3946
		int childIndex { get; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000F6B RID: 3947
		DragAndDropData dragAndDropData { get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000F6C RID: 3948
		DragAndDropPosition dragAndDropPosition { get; }
	}
}
