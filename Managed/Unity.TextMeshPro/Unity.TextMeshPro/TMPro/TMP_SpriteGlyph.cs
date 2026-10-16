using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class TMP_SpriteGlyph : Glyph
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0001411E File Offset: 0x0001231E
		public TMP_SpriteGlyph()
		{
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00014126 File Offset: 0x00012326
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014153 File Offset: 0x00012353
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x04000310 RID: 784
		public Sprite sprite;
	}
}
