using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000728 RID: 1832
	internal class MultiColumnHeaderColumnMoveLocationPreview : VisualElement
	{
		// Token: 0x06003528 RID: 13608 RVA: 0x000CE254 File Offset: 0x000CC454
		public MultiColumnHeaderColumnMoveLocationPreview()
		{
			base.AddToClassList(MultiColumnHeaderColumnMoveLocationPreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList(MultiColumnHeaderColumnMoveLocationPreview.visualUssClassName);
			visualElement.pickingMode = PickingMode.Ignore;
			base.Add(visualElement);
		}

		// Token: 0x04001B0A RID: 6922
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__move-location-preview";

		// Token: 0x04001B0B RID: 6923
		public static readonly string visualUssClassName = MultiColumnHeaderColumnMoveLocationPreview.ussClassName + "__visual";
	}
}
