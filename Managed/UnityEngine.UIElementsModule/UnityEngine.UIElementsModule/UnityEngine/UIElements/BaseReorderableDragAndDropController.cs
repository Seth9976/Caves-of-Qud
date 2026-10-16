using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EA RID: 490
	internal abstract class BaseReorderableDragAndDropController : ICollectionDragAndDropController, IDragAndDropController<IListDragAndDropArgs>, IReorderable
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x00043163 File Offset: 0x00041363
		public IEnumerable<int> GetSortedSelectedIds()
		{
			return this.m_SortedSelectedIds;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0004316B File Offset: 0x0004136B
		protected BaseReorderableDragAndDropController(BaseVerticalCollectionView view)
		{
			this.m_View = view;
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0004318E File Offset: 0x0004138E
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00043196 File Offset: 0x00041396
		public virtual bool enableReordering { get; set; } = true;

		// Token: 0x06000F23 RID: 3875 RVA: 0x000431A0 File Offset: 0x000413A0
		public virtual bool CanStartDrag(IEnumerable<int> itemIds)
		{
			return true;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000431B4 File Offset: 0x000413B4
		public virtual bool CanDrop()
		{
			return true;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000431C8 File Offset: 0x000413C8
		public virtual StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIds, bool skipText = false)
		{
			this.m_SortedSelectedIds.Clear();
			string text = string.Empty;
			bool flag = itemIds != null;
			if (flag)
			{
				foreach (int num in itemIds)
				{
					this.m_SortedSelectedIds.Add(num);
					bool flag2 = skipText;
					if (!flag2)
					{
						bool flag3 = string.IsNullOrEmpty(text);
						if (flag3)
						{
							ReusableCollectionItem recycledItemFromId = this.m_View.GetRecycledItemFromId(num);
							Label label = ((recycledItemFromId != null) ? recycledItemFromId.rootElement.Q<Label>(null, null) : null);
							text = ((label != null) ? label.text : string.Format("Item {0}", num));
						}
						else
						{
							text = "<Multiple>";
							skipText = true;
						}
					}
				}
			}
			this.m_SortedSelectedIds.Sort(new Comparison<int>(this.CompareId));
			bool flag4 = !this.m_View.reorderable;
			StartDragArgs startDragArgs;
			if (flag4)
			{
				startDragArgs = new StartDragArgs(string.Empty, DragVisualMode.Rejected);
			}
			else
			{
				startDragArgs = new StartDragArgs(text, DragVisualMode.Move);
			}
			return startDragArgs;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000432EC File Offset: 0x000414EC
		protected virtual int CompareId(int id1, int id2)
		{
			return id1.CompareTo(id2);
		}

		// Token: 0x06000F27 RID: 3879
		public abstract DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args);

		// Token: 0x06000F28 RID: 3880 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void OnDrop(IListDragAndDropArgs args)
		{
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void DragCleanup()
		{
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void HandleAutoExpand(ReusableCollectionItem item, Vector2 pointerPosition)
		{
		}

		// Token: 0x04000942 RID: 2370
		protected readonly BaseVerticalCollectionView m_View;

		// Token: 0x04000943 RID: 2371
		protected List<int> m_SortedSelectedIds = new List<int>();
	}
}
