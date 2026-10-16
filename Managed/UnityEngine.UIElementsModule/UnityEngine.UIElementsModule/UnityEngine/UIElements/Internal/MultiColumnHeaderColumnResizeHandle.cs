using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000736 RID: 1846
	internal class MultiColumnHeaderColumnResizeHandle : VisualElement
	{
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000D17C9 File Offset: 0x000CF9C9
		public VisualElement dragArea { get; }

		// Token: 0x060035BF RID: 13759 RVA: 0x000D17D4 File Offset: 0x000CF9D4
		public MultiColumnHeaderColumnResizeHandle()
		{
			base.AddToClassList(MultiColumnHeaderColumnResizeHandle.ussClassName);
			this.dragArea = new VisualElement
			{
				focusable = true,
				tabIndex = -1
			};
			this.dragArea.AddToClassList(MultiColumnHeaderColumnResizeHandle.dragAreaUssClassName);
			base.Add(this.dragArea);
		}

		// Token: 0x04001B6A RID: 7018
		public static readonly string ussClassName = MultiColumnCollectionHeader.ussClassName + "__column-resize-handle";

		// Token: 0x04001B6B RID: 7019
		public static readonly string dragAreaUssClassName = MultiColumnHeaderColumnResizeHandle.ussClassName + "__drag-area";
	}
}
