using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000733 RID: 1843
	internal class MultiColumnHeaderColumnSortIndicator : VisualElement
	{
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000D0B9B File Offset: 0x000CED9B
		// (set) Token: 0x06003598 RID: 13720 RVA: 0x000D0BA8 File Offset: 0x000CEDA8
		public string sortOrderLabel
		{
			get
			{
				return this.m_IndexLabel.text;
			}
			set
			{
				this.m_IndexLabel.text = value;
			}
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000D0BB8 File Offset: 0x000CEDB8
		public MultiColumnHeaderColumnSortIndicator()
		{
			base.AddToClassList(MultiColumnHeaderColumnSortIndicator.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			VisualElement visualElement = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			visualElement.AddToClassList(MultiColumnHeaderColumnSortIndicator.arrowUssClassName);
			base.Add(visualElement);
			this.m_IndexLabel = new Label
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_IndexLabel.AddToClassList(MultiColumnHeaderColumnSortIndicator.indexLabelUssClassName);
			base.Add(this.m_IndexLabel);
		}

		// Token: 0x04001B4C RID: 6988
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__sort-indicator";

		// Token: 0x04001B4D RID: 6989
		public static readonly string arrowUssClassName = MultiColumnHeaderColumnSortIndicator.ussClassName + "__arrow";

		// Token: 0x04001B4E RID: 6990
		public static readonly string indexLabelUssClassName = MultiColumnHeaderColumnSortIndicator.ussClassName + "__index-label";

		// Token: 0x04001B4F RID: 6991
		private Label m_IndexLabel;
	}
}
