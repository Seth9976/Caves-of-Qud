using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FF RID: 511
	internal static class ListViewDraggerExtension
	{
		// Token: 0x06000FB8 RID: 4024 RVA: 0x000453E4 File Offset: 0x000435E4
		public static ReusableCollectionItem GetRecycledItemFromId(this BaseVerticalCollectionView listView, int id)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in listView.activeItems)
			{
				bool flag = reusableCollectionItem.id.Equals(id);
				if (flag)
				{
					return reusableCollectionItem;
				}
			}
			return null;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00045450 File Offset: 0x00043650
		public static ReusableCollectionItem GetRecycledItemFromIndex(this BaseVerticalCollectionView listView, int index)
		{
			foreach (ReusableCollectionItem reusableCollectionItem in listView.activeItems)
			{
				bool flag = reusableCollectionItem.index.Equals(index);
				if (flag)
				{
					return reusableCollectionItem;
				}
			}
			return null;
		}
	}
}
