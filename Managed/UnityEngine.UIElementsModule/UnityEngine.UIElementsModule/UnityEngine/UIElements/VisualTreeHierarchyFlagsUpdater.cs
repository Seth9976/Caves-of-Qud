using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020005CD RID: 1485
	internal class VisualTreeHierarchyFlagsUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x0009E4ED File Offset: 0x0009C6ED
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeHierarchyFlagsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x0009E4F4 File Offset: 0x0009C6F4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.EventCallbackCategories | VersionChangeType.Picking)) == (VersionChangeType)0;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & (VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
				bool flag4 = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.EventCallbackCategories)) > (VersionChangeType)0;
				VisualElementFlags visualElementFlags = (flag2 ? (VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldBoundingBoxDirty) : ((VisualElementFlags)0)) | (flag3 ? VisualElementFlags.WorldClipDirty : ((VisualElementFlags)0)) | (flag4 ? VisualElementFlags.EventInterestParentCategoriesDirty : ((VisualElementFlags)0));
				VisualElementFlags visualElementFlags2 = visualElementFlags & ~ve.m_Flags;
				bool flag5 = visualElementFlags2 > (VisualElementFlags)0;
				if (flag5)
				{
					VisualTreeHierarchyFlagsUpdater.DirtyHierarchy(ve, visualElementFlags2);
				}
				VisualTreeHierarchyFlagsUpdater.DirtyBoundingBoxHierarchy(ve);
				this.m_Version += 1U;
			}
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x0009E588 File Offset: 0x0009C788
		protected static VisualElementFlags GetChildrenMustDirtyFlags(VisualElement ve, VersionChangeType versionChangeType)
		{
			VisualElementFlags visualElementFlags = (VisualElementFlags)0;
			bool flag = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
			if (flag)
			{
				visualElementFlags |= VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldBoundingBoxDirty;
			}
			bool flag2 = (versionChangeType & (VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
			if (flag2)
			{
				visualElementFlags |= VisualElementFlags.WorldClipDirty;
			}
			bool flag3 = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.EventCallbackCategories)) > (VersionChangeType)0;
			if (flag3)
			{
				visualElementFlags |= VisualElementFlags.EventInterestParentCategoriesDirty;
			}
			return visualElementFlags;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x0009E5D8 File Offset: 0x0009C7D8
		protected static void DirtyHierarchy(VisualElement ve, VisualElementFlags mustDirtyFlags)
		{
			ve.m_Flags |= mustDirtyFlags;
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				VisualElementFlags visualElementFlags = mustDirtyFlags & ~visualElement.m_Flags;
				bool flag = visualElementFlags > (VisualElementFlags)0;
				if (flag)
				{
					VisualTreeHierarchyFlagsUpdater.DirtyHierarchy(visualElement, visualElementFlags);
				}
			}
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x0009E648 File Offset: 0x0009C848
		private static void DirtyBoundingBoxHierarchy(VisualElement ve)
		{
			ve.m_Flags |= VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty | VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass;
			VisualTreeHierarchyFlagsUpdater.DirtyParentHierarchy(ve.hierarchy.parent, VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty | VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0009E684 File Offset: 0x0009C884
		protected static void DirtyParentHierarchy(VisualElement ve, VisualElementFlags flags)
		{
			while (ve != null && (ve.m_Flags & flags) != flags)
			{
				ve.m_Flags |= flags;
				ve = ve.hierarchy.parent;
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x0009E6CC File Offset: 0x0009C8CC
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				base.panel.UpdateElementUnderPointers();
				base.panel.visualTree.UpdateBoundingBox();
			}
		}

		// Token: 0x040013F8 RID: 5112
		private uint m_Version = 0U;

		// Token: 0x040013F9 RID: 5113
		private uint m_LastVersion = 0U;

		// Token: 0x040013FA RID: 5114
		private static readonly string s_Description = "Update Hierarchy Flags";

		// Token: 0x040013FB RID: 5115
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeHierarchyFlagsUpdater.s_Description);

		// Token: 0x040013FC RID: 5116
		private const VersionChangeType WorldTransformChanged = VersionChangeType.Transform;

		// Token: 0x040013FD RID: 5117
		private const VersionChangeType WorldClipChanged = VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size;

		// Token: 0x040013FE RID: 5118
		private const VersionChangeType EventParentCategoriesChanged = VersionChangeType.Hierarchy | VersionChangeType.EventCallbackCategories;

		// Token: 0x040013FF RID: 5119
		private const VersionChangeType BoundingBoxChanged = VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.Transform | VersionChangeType.Size;

		// Token: 0x04001400 RID: 5120
		protected const VersionChangeType ChildrenChanged = VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.EventCallbackCategories;

		// Token: 0x04001401 RID: 5121
		protected const VersionChangeType ParentChanged = VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.Transform | VersionChangeType.Size;

		// Token: 0x04001402 RID: 5122
		protected const VersionChangeType VersionChanged = VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.Picking;

		// Token: 0x04001403 RID: 5123
		protected const VersionChangeType AnythingChanged = VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.EventCallbackCategories | VersionChangeType.Picking;

		// Token: 0x04001404 RID: 5124
		protected const VisualElementFlags BoundingBoxDirtyFlags = VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty | VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass;
	}
}
