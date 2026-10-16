using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F8 RID: 504
	public readonly struct CanStartDragArgs
	{
		// Token: 0x06000F81 RID: 3969 RVA: 0x00043D8E File Offset: 0x00041F8E
		internal CanStartDragArgs(VisualElement draggedElement, int id, IEnumerable<int> selectedIds)
		{
			this.draggedElement = draggedElement;
			this.id = id;
			this.selectedIds = selectedIds;
		}

		// Token: 0x04000966 RID: 2406
		public readonly VisualElement draggedElement;

		// Token: 0x04000967 RID: 2407
		public readonly int id;

		// Token: 0x04000968 RID: 2408
		public readonly IEnumerable<int> selectedIds;
	}
}
