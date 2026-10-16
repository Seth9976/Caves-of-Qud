using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006A RID: 106
	public class ListViewController : BaseListViewController
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011332 File Offset: 0x0000F532
		protected ListView listView
		{
			get
			{
				return base.view as ListView;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00011340 File Offset: 0x0000F540
		protected override VisualElement MakeItem()
		{
			bool flag = this.listView.makeItem == null;
			VisualElement visualElement;
			if (flag)
			{
				bool flag2 = this.listView.bindItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify makeItem if bindItem is specified.");
				}
				visualElement = new Label();
			}
			else
			{
				visualElement = this.listView.makeItem();
			}
			return visualElement;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001139C File Offset: 0x0000F59C
		protected override void BindItem(VisualElement element, int index)
		{
			bool flag = this.listView.bindItem == null;
			if (flag)
			{
				bool flag2 = this.listView.makeItem != null;
				bool flag3 = this.listView.autoAssignSource && flag2;
				if (!flag3)
				{
					bool flag4 = flag2;
					if (flag4)
					{
						throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
					}
					Label label = (Label)element;
					object obj = this.listView.itemsSource[index];
					label.text = ((obj != null) ? obj.ToString() : null) ?? "null";
				}
			}
			else
			{
				this.listView.bindItem(element, index);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001143D File Offset: 0x0000F63D
		protected override void UnbindItem(VisualElement element, int index)
		{
			Action<VisualElement, int> unbindItem = this.listView.unbindItem;
			if (unbindItem != null)
			{
				unbindItem(element, index);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00011459 File Offset: 0x0000F659
		protected override void DestroyItem(VisualElement element)
		{
			Action<VisualElement> destroyItem = this.listView.destroyItem;
			if (destroyItem != null)
			{
				destroyItem(element);
			}
		}
	}
}
