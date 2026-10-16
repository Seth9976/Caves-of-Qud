using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000727 RID: 1831
	internal class MultiColumnHeaderColumnMovePreview : VisualElement
	{
		// Token: 0x06003526 RID: 13606 RVA: 0x000CE21D File Offset: 0x000CC41D
		public MultiColumnHeaderColumnMovePreview()
		{
			base.AddToClassList(MultiColumnHeaderColumnMovePreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
		}

		// Token: 0x04001B09 RID: 6921
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__move-preview";
	}
}
