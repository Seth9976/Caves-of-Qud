using System;
using UnityEngine.Internal;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002C RID: 44
	[ExcludeFromDocs]
	[Serializable]
	public struct FontAssetCreationEditorSettings
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00005294 File Offset: 0x00003494
		internal FontAssetCreationEditorSettings(string sourceFontFileGUID, float pointSize, int pointSizeSamplingMode, int padding, int packingMode, int atlasWidth, int atlasHeight, int characterSelectionMode, string characterSet, int renderMode)
		{
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

		// Token: 0x040000DC RID: 220
		public string sourceFontFileGUID;

		// Token: 0x040000DD RID: 221
		public int faceIndex;

		// Token: 0x040000DE RID: 222
		public int pointSizeSamplingMode;

		// Token: 0x040000DF RID: 223
		public float pointSize;

		// Token: 0x040000E0 RID: 224
		public int padding;

		// Token: 0x040000E1 RID: 225
		public int paddingMode;

		// Token: 0x040000E2 RID: 226
		public int packingMode;

		// Token: 0x040000E3 RID: 227
		public int atlasWidth;

		// Token: 0x040000E4 RID: 228
		public int atlasHeight;

		// Token: 0x040000E5 RID: 229
		public int characterSetSelectionMode;

		// Token: 0x040000E6 RID: 230
		public string characterSequence;

		// Token: 0x040000E7 RID: 231
		public string referencedFontAssetGUID;

		// Token: 0x040000E8 RID: 232
		public string referencedTextAssetGUID;

		// Token: 0x040000E9 RID: 233
		public int fontStyle;

		// Token: 0x040000EA RID: 234
		public float fontStyleModifier;

		// Token: 0x040000EB RID: 235
		public int renderMode;

		// Token: 0x040000EC RID: 236
		public bool includeFontFeatures;
	}
}
