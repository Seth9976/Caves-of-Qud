using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000068 RID: 104
	internal interface IDefaultTreeViewController<T>
	{
		// Token: 0x060003C4 RID: 964
		void SetRootItems(IList<TreeViewItemData<T>> items);

		// Token: 0x060003C5 RID: 965
		void AddItem(in TreeViewItemData<T> item, int parentId, int childIndex, bool rebuildTree = true);

		// Token: 0x060003C6 RID: 966
		TreeViewItemData<T> GetTreeViewItemDataForId(int id);

		// Token: 0x060003C7 RID: 967
		TreeViewItemData<T> GetTreeViewItemDataForIndex(int index);

		// Token: 0x060003C8 RID: 968
		T GetDataForId(int id);

		// Token: 0x060003C9 RID: 969
		T GetDataForIndex(int index);
	}
}
