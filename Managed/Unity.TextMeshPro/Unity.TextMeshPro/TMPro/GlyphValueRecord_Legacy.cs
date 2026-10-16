using System;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public struct GlyphValueRecord_Legacy
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00008D8A File Offset: 0x00006F8A
		internal GlyphValueRecord_Legacy(GlyphValueRecord valueRecord)
		{
			this.xPlacement = valueRecord.xPlacement;
			this.yPlacement = valueRecord.yPlacement;
			this.xAdvance = valueRecord.xAdvance;
			this.yAdvance = valueRecord.yAdvance;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008DC0 File Offset: 0x00006FC0
		public static GlyphValueRecord_Legacy operator +(GlyphValueRecord_Legacy a, GlyphValueRecord_Legacy b)
		{
			GlyphValueRecord_Legacy glyphValueRecord_Legacy;
			glyphValueRecord_Legacy.xPlacement = a.xPlacement + b.xPlacement;
			glyphValueRecord_Legacy.yPlacement = a.yPlacement + b.yPlacement;
			glyphValueRecord_Legacy.xAdvance = a.xAdvance + b.xAdvance;
			glyphValueRecord_Legacy.yAdvance = a.yAdvance + b.yAdvance;
			return glyphValueRecord_Legacy;
		}

		// Token: 0x04000118 RID: 280
		public float xPlacement;

		// Token: 0x04000119 RID: 281
		public float yPlacement;

		// Token: 0x0400011A RID: 282
		public float xAdvance;

		// Token: 0x0400011B RID: 283
		public float yAdvance;
	}
}
