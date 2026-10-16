using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000078 RID: 120
	internal class ReusableMultiColumnListViewItem : ReusableListViewItem
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00014772 File Offset: 0x00012972
		public override VisualElement rootElement
		{
			get
			{
				return base.bindableElement;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00002152 File Offset: 0x00000352
		public override void Init(VisualElement item)
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014D98 File Offset: 0x00012F98
		public void Init(VisualElement container, Columns columns, bool usesAnimatedDrag)
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
					base.UpdateHierarchy(visualElement, visualElement2, usesAnimatedDrag);
					break;
				}
				num++;
			}
		}
	}
}
