using System;

namespace TMPro
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public struct FontAssetCreationSettings
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00008CCC File Offset: 0x00006ECC
		internal FontAssetCreationSettings(string sourceFontFileGUID, int pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
			this.sourceFontFileName = string.Empty;
			this.sourceFontFileGUID = sourceFontFileGUID;
			this.faceIndex = 0;
			this.pointSize = pointSize;
			this.pointSizeSamplingMode = pointSizeSamplingMode;
			this.padding = padding;
			this.paddingMode = 2;
			this.packingMode = packingMode;
			this.atlasWidth = atlasWidth;
			this.atlasHeight = atlasHeight;
			this.characterSequence = characterSet;
			this.characterSetSelectionMode = characterSelectionMode;
			this.renderMode = renderMode;
			this.referencedFontAssetGUID = string.Empty;
			this.referencedTextAssetGUID = string.Empty;
			this.fontStyle = 0;
			this.fontStyleModifier = 0f;
			this.includeFontFeatures = false;
		}

		// Token: 0x04000101 RID: 257
		public string sourceFontFileName;

		// Token: 0x04000102 RID: 258
		public string sourceFontFileGUID;

		// Token: 0x04000103 RID: 259
		public int faceIndex;

		// Token: 0x04000104 RID: 260
		public int pointSizeSamplingMode;

		// Token: 0x04000105 RID: 261
		public int pointSize;

		// Token: 0x04000106 RID: 262
		public int padding;

		// Token: 0x04000107 RID: 263
		public int paddingMode;

		// Token: 0x04000108 RID: 264
		public int packingMode;

		// Token: 0x04000109 RID: 265
		public int atlasWidth;

		// Token: 0x0400010A RID: 266
		public int atlasHeight;

		// Token: 0x0400010B RID: 267
		public int characterSetSelectionMode;

		// Token: 0x0400010C RID: 268
		public string characterSequence;

		// Token: 0x0400010D RID: 269
		public string referencedFontAssetGUID;

		// Token: 0x0400010E RID: 270
		public string referencedTextAssetGUID;

		// Token: 0x0400010F RID: 271
		public int fontStyle;

		// Token: 0x04000110 RID: 272
		public float fontStyleModifier;

		// Token: 0x04000111 RID: 273
		public int renderMode;

		// Token: 0x04000112 RID: 274
		public bool includeFontFeatures;
	}
}
