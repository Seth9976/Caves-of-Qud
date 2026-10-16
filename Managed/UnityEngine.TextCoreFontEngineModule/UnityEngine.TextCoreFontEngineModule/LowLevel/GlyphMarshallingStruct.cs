using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000016 RID: 22
	[UsedByNativeCode]
	internal struct GlyphMarshallingStruct
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005944 File Offset: 0x00003B44
		public GlyphMarshallingStruct(Glyph glyph)
		{
			this.index = glyph.index;
			this.metrics = glyph.metrics;
			this.glyphRect = glyph.glyphRect;
			this.scale = glyph.scale;
			this.atlasIndex = glyph.atlasIndex;
			this.classDefinitionType = glyph.classDefinitionType;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000599A File Offset: 0x00003B9A
		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
			this.classDefinitionType = GlyphClassDefinitionType.Undefined;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000059C9 File Offset: 0x00003BC9
		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, GlyphClassDefinitionType classDefinitionType)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
			this.classDefinitionType = classDefinitionType;
		}

		// Token: 0x0400009F RID: 159
		public uint index;

		// Token: 0x040000A0 RID: 160
		public GlyphMetrics metrics;

		// Token: 0x040000A1 RID: 161
		public GlyphRect glyphRect;

		// Token: 0x040000A2 RID: 162
		public float scale;

		// Token: 0x040000A3 RID: 163
		public int atlasIndex;

		// Token: 0x040000A4 RID: 164
		public GlyphClassDefinitionType classDefinitionType;
	}
}
