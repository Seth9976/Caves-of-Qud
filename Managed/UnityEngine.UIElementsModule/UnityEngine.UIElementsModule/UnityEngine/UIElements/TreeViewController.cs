using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006E RID: 110
	public abstract class TreeViewController : BaseTreeViewController
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00011B05 File Offset: 0x0000FD05
		protected TreeView treeView
		{
			get
			{
				return base.view as TreeView;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011B14 File Offset: 0x0000FD14
		protected override VisualElement MakeItem()
		{
			bool flag = this.treeView.makeItem == null;
			VisualElement visualElement;
			if (flag)
			{
				bool flag2 = this.treeView.bindItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify makeItem if bindItem is specified.");
				}
				visualElement = new Label();
			}
			else
			{
				visualElement = this.treeView.makeItem();
			}
			return visualElement;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011B70 File Offset: 0x0000FD70
		protected override void BindItem(VisualElement element, int index)
		{
			bool flag = this.treeView.bindItem == null;
			if (flag)
			{
				bool flag2 = this.treeView.makeItem != null;
				bool flag3 = flag2;
				if (flag3)
				{
					throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
				}
				Label label = (Label)element;
				object itemForIndex = this.GetItemForIndex(index);
				label.text = ((itemForIndex != null) ? itemForIndex.ToString() : null) ?? "null";
			}
			else
			{
				this.treeView.bindItem(element, index);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00011BF2 File Offset: 0x0000FDF2
		protected override void UnbindItem(VisualElement element, int index)
		{
			Action<VisualElement, int> unbindItem = this.treeView.unbindItem;
			if (unbindItem != null)
			{
				unbindItem(element, index);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00011C0E File Offset: 0x0000FE0E
		protected override void DestroyItem(VisualElement element)
		{
			Action<VisualElement> destroyItem = this.treeView.destroyItem;
			if (destroyItem != null)
			{
				destroyItem(element);
			}
		}
	}
}
