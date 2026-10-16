using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005CE RID: 1486
	internal class VisualTreeWorldSpaceHierarchyFlagsUpdater : VisualTreeHierarchyFlagsUpdater
	{
		// Token: 0x06002BE1 RID: 11233 RVA: 0x0009E74C File Offset: 0x0009C94C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.EventCallbackCategories | VersionChangeType.Picking)) == (VersionChangeType)0;
			if (!flag)
			{
				bool flag2 = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.EventCallbackCategories)) > (VersionChangeType)0;
				if (flag2)
				{
					VisualTreeHierarchyFlagsUpdater.DirtyHierarchy(ve, VisualTreeHierarchyFlagsUpdater.GetChildrenMustDirtyFlags(ve, versionChangeType));
				}
				bool flag3 = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
				if (flag3)
				{
					VisualTreeHierarchyFlagsUpdater.DirtyParentHierarchy(ve, this.GetParentMustDirtyFlags(ve));
				}
			}
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0009E7B0 File Offset: 0x0009C9B0
		private VisualElementFlags GetParentMustDirtyFlags(VisualElement ve)
		{
			VisualElementFlags visualElementFlags = VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty | VisualElementFlags.LocalBounds3DDirty | VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass;
			bool has3DTransform = ve.has3DTransform;
			if (has3DTransform)
			{
				visualElementFlags |= VisualElementFlags.Needs3DBounds;
			}
			return visualElementFlags;
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00002152 File Offset: 0x00000352
		public override void Update()
		{
		}

		// Token: 0x04001405 RID: 5125
		private new const VisualElementFlags BoundingBoxDirtyFlags = VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty | VisualElementFlags.LocalBounds3DDirty | VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass;
	}
}
