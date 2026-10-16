using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x0200072A RID: 1834
	internal class MultiColumnHeaderColumnResizePreview : VisualElement
	{
		// Token: 0x06003546 RID: 13638 RVA: 0x000CECC8 File Offset: 0x000CCEC8
		public MultiColumnHeaderColumnResizePreview()
		{
			base.AddToClassList(MultiColumnHeaderColumnResizePreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			VisualElement visualElement = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			visualElement.AddToClassList(MultiColumnHeaderColumnResizePreview.visualUssClassName);
			base.Add(visualElement);
		}

		// Token: 0x04001B1D RID: 6941
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__resize-preview";

		// Token: 0x04001B1E RID: 6942
		public static readonly string visualUssClassName = MultiColumnHeaderColumnResizePreview.ussClassName + "__visual";
	}
}
