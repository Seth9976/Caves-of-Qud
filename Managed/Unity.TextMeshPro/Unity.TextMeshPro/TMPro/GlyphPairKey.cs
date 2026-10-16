using System;

namespace TMPro
{
	// Token: 0x02000035 RID: 53
	public struct GlyphPairKey
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public GlyphPairKey(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			this.firstGlyphIndex = firstGlyphIndex;
			this.secondGlyphIndex = secondGlyphIndex;
			this.key = (secondGlyphIndex << 16) | firstGlyphIndex;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009BFC File Offset: 0x00007DFC
		internal GlyphPairKey(TMP_GlyphPairAdjustmentRecord record)
		{
			this.firstGlyphIndex = record.firstAdjustmentRecord.glyphIndex;
			this.secondGlyphIndex = record.secondAdjustmentRecord.glyphIndex;
			this.key = (this.secondGlyphIndex << 16) | this.firstGlyphIndex;
		}

		// Token: 0x0400013D RID: 317
		public uint firstGlyphIndex;

		// Token: 0x0400013E RID: 318
		public uint secondGlyphIndex;

		// Token: 0x0400013F RID: 319
		public uint key;
	}
}
