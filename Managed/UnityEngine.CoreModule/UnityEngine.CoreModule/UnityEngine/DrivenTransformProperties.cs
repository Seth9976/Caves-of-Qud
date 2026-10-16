using System;

namespace UnityEngine
{
	// Token: 0x020002F3 RID: 755
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x04000A7D RID: 2685
		None = 0,
		// Token: 0x04000A7E RID: 2686
		All = -1,
		// Token: 0x04000A7F RID: 2687
		AnchoredPositionX = 2,
		// Token: 0x04000A80 RID: 2688
		AnchoredPositionY = 4,
		// Token: 0x04000A81 RID: 2689
		AnchoredPositionZ = 8,
		// Token: 0x04000A82 RID: 2690
		Rotation = 16,
		// Token: 0x04000A83 RID: 2691
		ScaleX = 32,
		// Token: 0x04000A84 RID: 2692
		ScaleY = 64,
		// Token: 0x04000A85 RID: 2693
		ScaleZ = 128,
		// Token: 0x04000A86 RID: 2694
		AnchorMinX = 256,
		// Token: 0x04000A87 RID: 2695
		AnchorMinY = 512,
		// Token: 0x04000A88 RID: 2696
		AnchorMaxX = 1024,
		// Token: 0x04000A89 RID: 2697
		AnchorMaxY = 2048,
		// Token: 0x04000A8A RID: 2698
		SizeDeltaX = 4096,
		// Token: 0x04000A8B RID: 2699
		SizeDeltaY = 8192,
		// Token: 0x04000A8C RID: 2700
		PivotX = 16384,
		// Token: 0x04000A8D RID: 2701
		PivotY = 32768,
		// Token: 0x04000A8E RID: 2702
		AnchoredPosition = 6,
		// Token: 0x04000A8F RID: 2703
		AnchoredPosition3D = 14,
		// Token: 0x04000A90 RID: 2704
		Scale = 224,
		// Token: 0x04000A91 RID: 2705
		AnchorMin = 768,
		// Token: 0x04000A92 RID: 2706
		AnchorMax = 3072,
		// Token: 0x04000A93 RID: 2707
		Anchors = 3840,
		// Token: 0x04000A94 RID: 2708
		SizeDelta = 12288,
		// Token: 0x04000A95 RID: 2709
		Pivot = 49152
	}
}
