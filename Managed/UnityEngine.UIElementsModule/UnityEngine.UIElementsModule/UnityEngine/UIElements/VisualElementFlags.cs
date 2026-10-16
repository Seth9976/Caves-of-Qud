using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005AA RID: 1450
	[Flags]
	internal enum VisualElementFlags
	{
		// Token: 0x0400138D RID: 5005
		WorldTransformDirty = 1,
		// Token: 0x0400138E RID: 5006
		WorldTransformInverseDirty = 2,
		// Token: 0x0400138F RID: 5007
		WorldClipDirty = 4,
		// Token: 0x04001390 RID: 5008
		BoundingBoxDirty = 8,
		// Token: 0x04001391 RID: 5009
		WorldBoundingBoxDirty = 16,
		// Token: 0x04001392 RID: 5010
		EventInterestParentCategoriesDirty = 32,
		// Token: 0x04001393 RID: 5011
		LayoutManual = 64,
		// Token: 0x04001394 RID: 5012
		CompositeRoot = 128,
		// Token: 0x04001395 RID: 5013
		RequireMeasureFunction = 256,
		// Token: 0x04001396 RID: 5014
		EnableViewDataPersistence = 512,
		// Token: 0x04001397 RID: 5015
		DisableClipping = 1024,
		// Token: 0x04001398 RID: 5016
		NeedsAttachToPanelEvent = 2048,
		// Token: 0x04001399 RID: 5017
		HierarchyDisplayed = 4096,
		// Token: 0x0400139A RID: 5018
		StyleInitialized = 8192,
		// Token: 0x0400139B RID: 5019
		DisableRendering = 16384,
		// Token: 0x0400139C RID: 5020
		Needs3DBounds = 32768,
		// Token: 0x0400139D RID: 5021
		LocalBounds3DDirty = 65536,
		// Token: 0x0400139E RID: 5022
		DetachedDataSource = 131072,
		// Token: 0x0400139F RID: 5023
		ReceivesHierarchyGeometryChangedEvents = 262144,
		// Token: 0x040013A0 RID: 5024
		BoundingBoxDirtiedSinceLastLayoutPass = 524288,
		// Token: 0x040013A1 RID: 5025
		Init = 196671
	}
}
