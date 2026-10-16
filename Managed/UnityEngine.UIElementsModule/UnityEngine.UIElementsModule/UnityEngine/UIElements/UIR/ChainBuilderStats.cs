using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200065C RID: 1628
	internal struct ChainBuilderStats
	{
		// Token: 0x040016D6 RID: 5846
		public uint elementsAdded;

		// Token: 0x040016D7 RID: 5847
		public uint elementsRemoved;

		// Token: 0x040016D8 RID: 5848
		public uint recursiveClipUpdates;

		// Token: 0x040016D9 RID: 5849
		public uint recursiveClipUpdatesExpanded;

		// Token: 0x040016DA RID: 5850
		public uint nonRecursiveClipUpdates;

		// Token: 0x040016DB RID: 5851
		public uint recursiveTransformUpdates;

		// Token: 0x040016DC RID: 5852
		public uint recursiveTransformUpdatesExpanded;

		// Token: 0x040016DD RID: 5853
		public uint recursiveOpacityUpdates;

		// Token: 0x040016DE RID: 5854
		public uint recursiveOpacityUpdatesExpanded;

		// Token: 0x040016DF RID: 5855
		public uint opacityIdUpdates;

		// Token: 0x040016E0 RID: 5856
		public uint colorUpdates;

		// Token: 0x040016E1 RID: 5857
		public uint colorUpdatesExpanded;

		// Token: 0x040016E2 RID: 5858
		public uint recursiveVisualUpdates;

		// Token: 0x040016E3 RID: 5859
		public uint recursiveVisualUpdatesExpanded;

		// Token: 0x040016E4 RID: 5860
		public uint nonRecursiveVisualUpdates;

		// Token: 0x040016E5 RID: 5861
		public uint dirtyProcessed;

		// Token: 0x040016E6 RID: 5862
		public uint nudgeTransformed;

		// Token: 0x040016E7 RID: 5863
		public uint boneTransformed;

		// Token: 0x040016E8 RID: 5864
		public uint skipTransformed;

		// Token: 0x040016E9 RID: 5865
		public uint visualUpdateTransformed;

		// Token: 0x040016EA RID: 5866
		public uint updatedMeshAllocations;

		// Token: 0x040016EB RID: 5867
		public uint newMeshAllocations;

		// Token: 0x040016EC RID: 5868
		public uint groupTransformElementsChanged;
	}
}
