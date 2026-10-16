using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000036 RID: 54
	[Serializable]
	public class SpriteGlyph : Glyph
	{
		// Token: 0x0600017F RID: 383 RVA: 0x0000B1C1 File Offset: 0x000093C1
		public SpriteGlyph()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B1CB File Offset: 0x000093CB
		public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B1FF File Offset: 0x000093FF
		public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x0400014F RID: 335
		public Sprite sprite;
	}
}
