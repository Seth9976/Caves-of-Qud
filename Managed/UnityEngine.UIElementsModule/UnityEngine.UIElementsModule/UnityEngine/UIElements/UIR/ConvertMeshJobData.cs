using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000645 RID: 1605
	internal struct ConvertMeshJobData
	{
		// Token: 0x0400163A RID: 5690
		public IntPtr vertSrc;

		// Token: 0x0400163B RID: 5691
		public IntPtr vertDst;

		// Token: 0x0400163C RID: 5692
		public int vertCount;

		// Token: 0x0400163D RID: 5693
		public Matrix4x4 transform;

		// Token: 0x0400163E RID: 5694
		public Color32 xformClipPages;

		// Token: 0x0400163F RID: 5695
		public Color32 ids;

		// Token: 0x04001640 RID: 5696
		public Color32 addFlags;

		// Token: 0x04001641 RID: 5697
		public Color32 opacityPage;

		// Token: 0x04001642 RID: 5698
		public Color32 textCoreSettingsPage;

		// Token: 0x04001643 RID: 5699
		public int usesTextCoreSettings;

		// Token: 0x04001644 RID: 5700
		public float textureId;

		// Token: 0x04001645 RID: 5701
		public int gradientSettingsIndexOffset;

		// Token: 0x04001646 RID: 5702
		public IntPtr indexSrc;

		// Token: 0x04001647 RID: 5703
		public IntPtr indexDst;

		// Token: 0x04001648 RID: 5704
		public int indexCount;

		// Token: 0x04001649 RID: 5705
		public int indexOffset;

		// Token: 0x0400164A RID: 5706
		public int flipIndices;

		// Token: 0x0400164B RID: 5707
		public int forceZ;

		// Token: 0x0400164C RID: 5708
		public float positionZ;

		// Token: 0x0400164D RID: 5709
		public int remapUVs;

		// Token: 0x0400164E RID: 5710
		public Rect atlasRect;
	}
}
