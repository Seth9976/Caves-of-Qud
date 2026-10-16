using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000079 RID: 121
	internal class ReusableMultiColumnTreeViewItem : ReusableTreeViewItem
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00014772 File Offset: 0x00012972
		public override VisualElement rootElement
		{
			get
			{
				return base.bindableElement;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00002152 File Offset: 0x00000352
		public override void Init(VisualElement item)
		{
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00014E34 File Offset: 0x00013034
		public void Init(VisualElement container, Columns columns)
		{
			int num = 0;
			base.bindableElement = container;
			foreach (Column column in columns.visibleList)
			{
				bool flag = columns.IsPrimary(column);
				if (flag)
				{
					VisualElement visualElement = container[num];
					VisualElement visualElement2 = visualElement.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					base.InitExpandHierarchy(visualElement, visualElement2);
					break;
				}
				num++;
			}
		}
	}
}
