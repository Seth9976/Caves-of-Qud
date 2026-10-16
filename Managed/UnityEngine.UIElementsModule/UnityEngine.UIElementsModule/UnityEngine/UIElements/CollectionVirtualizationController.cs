using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006F RID: 111
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal abstract class CollectionVirtualizationController
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000403 RID: 1027
		// (set) Token: 0x06000404 RID: 1028
		public abstract int firstVisibleIndex { get; protected set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000405 RID: 1029
		public abstract int visibleItemCount { get; }

		// Token: 0x06000406 RID: 1030 RVA: 0x00011C32 File Offset: 0x0000FE32
		protected CollectionVirtualizationController(ScrollView scrollView)
		{
			this.m_ScrollView = scrollView;
		}

		// Token: 0x06000407 RID: 1031
		public abstract void Refresh(bool rebuild);

		// Token: 0x06000408 RID: 1032
		public abstract void ScrollToItem(int id);

		// Token: 0x06000409 RID: 1033
		public abstract void Resize(Vector2 size);

		// Token: 0x0600040A RID: 1034
		public abstract void OnScroll(Vector2 offset);

		// Token: 0x0600040B RID: 1035
		public abstract int GetIndexFromPosition(Vector2 position);

		// Token: 0x0600040C RID: 1036
		public abstract float GetExpectedItemHeight(int index);

		// Token: 0x0600040D RID: 1037
		public abstract float GetExpectedContentHeight();

		// Token: 0x0600040E RID: 1038
		public abstract void OnFocusIn(VisualElement leafTarget);

		// Token: 0x0600040F RID: 1039
		public abstract void OnFocusOut(VisualElement willFocus);

		// Token: 0x06000410 RID: 1040
		public abstract void UpdateBackground();

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000411 RID: 1041
		public abstract IEnumerable<ReusableCollectionItem> activeItems { get; }

		// Token: 0x06000412 RID: 1042
		internal abstract void StartDragItem(ReusableCollectionItem item);

		// Token: 0x06000413 RID: 1043
		internal abstract void EndDrag(int dropIndex);

		// Token: 0x06000414 RID: 1044
		public abstract void UnbindAll();

		// Token: 0x040001FE RID: 510
		protected readonly ScrollView m_ScrollView;
	}
}
