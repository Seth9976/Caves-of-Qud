using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F6 RID: 502
	internal interface IDragAndDropController<in TArgs>
	{
		// Token: 0x06000F79 RID: 3961
		bool CanStartDrag(IEnumerable<int> itemIds);

		// Token: 0x06000F7A RID: 3962 RVA: 0x00043D74 File Offset: 0x00041F74
		bool CanDrop()
		{
			return true;
		}

		// Token: 0x06000F7B RID: 3963
		StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIds, bool skipText = false);

		// Token: 0x06000F7C RID: 3964
		DragVisualMode HandleDragAndDrop(TArgs args);

		// Token: 0x06000F7D RID: 3965
		void OnDrop(TArgs args);

		// Token: 0x06000F7E RID: 3966 RVA: 0x00002152 File Offset: 0x00000352
		void DragCleanup()
		{
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00002152 File Offset: 0x00000352
		void HandleAutoExpand(ReusableCollectionItem item, Vector2 pointerPosition)
		{
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00043D87 File Offset: 0x00041F87
		IEnumerable<int> GetSortedSelectedIds()
		{
			return Enumerable.Empty<int>();
		}
	}
}
