using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000302 RID: 770
	[Flags]
	internal enum RenderHints
	{
		// Token: 0x04000C08 RID: 3080
		None = 0,
		// Token: 0x04000C09 RID: 3081
		GroupTransform = 1,
		// Token: 0x04000C0A RID: 3082
		BoneTransform = 2,
		// Token: 0x04000C0B RID: 3083
		ClipWithScissors = 4,
		// Token: 0x04000C0C RID: 3084
		MaskContainer = 8,
		// Token: 0x04000C0D RID: 3085
		DynamicColor = 16,
		// Token: 0x04000C0E RID: 3086
		DirtyOffset = 5,
		// Token: 0x04000C0F RID: 3087
		DirtyGroupTransform = 32,
		// Token: 0x04000C10 RID: 3088
		DirtyBoneTransform = 64,
		// Token: 0x04000C11 RID: 3089
		DirtyClipWithScissors = 128,
		// Token: 0x04000C12 RID: 3090
		DirtyMaskContainer = 256,
		// Token: 0x04000C13 RID: 3091
		DirtyDynamicColor = 512,
		// Token: 0x04000C14 RID: 3092
		DirtyAll = 992
	}
}
