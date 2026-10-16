using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F9 RID: 505
	public readonly struct SetupDragAndDropArgs
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x00043DA6 File Offset: 0x00041FA6
		internal SetupDragAndDropArgs(VisualElement draggedElement, IEnumerable<int> selectedIds, StartDragArgs startDragArgs)
		{
			this.draggedElement = draggedElement;
			this.selectedIds = selectedIds;
			this.startDragArgs = startDragArgs;
		}

		// Token: 0x04000969 RID: 2409
		public readonly VisualElement draggedElement;

		// Token: 0x0400096A RID: 2410
		public readonly IEnumerable<int> selectedIds;

		// Token: 0x0400096B RID: 2411
		public readonly StartDragArgs startDragArgs;
	}
}
