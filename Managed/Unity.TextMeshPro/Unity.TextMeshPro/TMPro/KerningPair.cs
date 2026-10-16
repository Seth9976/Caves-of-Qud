using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TMPro
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class KerningPair
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008E1E File Offset: 0x0000701E
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00008E26 File Offset: 0x00007026
		public uint firstGlyph
		{
			get
			{
				return this.m_FirstGlyph;
			}
			set
			{
				this.m_FirstGlyph = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00008E2F File Offset: 0x0000702F
		public GlyphValueRecord_Legacy firstGlyphAdjustments
		{
			get
			{
				return this.m_FirstGlyphAdjustments;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00008E37 File Offset: 0x00007037
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00008E3F File Offset: 0x0000703F
		public uint secondGlyph
		{
			get
			{
				return this.m_SecondGlyph;
			}
			set
			{
				this.m_SecondGlyph = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008E48 File Offset: 0x00007048
		public GlyphValueRecord_Legacy secondGlyphAdjustments
		{
			get
			{
				return this.m_SecondGlyphAdjustments;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008E50 File Offset: 0x00007050
		public bool ignoreSpacingAdjustments
		{
			get
			{
				return this.m_IgnoreSpacingAdjustments;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008E58 File Offset: 0x00007058
		public KerningPair()
		{
			this.m_FirstGlyph = 0U;
			this.m_FirstGlyphAdjustments = default(GlyphValueRecord_Legacy);
			this.m_SecondGlyph = 0U;
			this.m_SecondGlyphAdjustments = default(GlyphValueRecord_Legacy);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008E86 File Offset: 0x00007086
		public KerningPair(uint left, uint right, float offset)
		{
			this.firstGlyph = left;
			this.m_SecondGlyph = right;
			this.xOffset = offset;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008EA3 File Offset: 0x000070A3
		public KerningPair(uint firstGlyph, GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord_Legacy secondGlyphAdjustments)
		{
			this.m_FirstGlyph = firstGlyph;
			this.m_FirstGlyphAdjustments = firstGlyphAdjustments;
			this.m_SecondGlyph = secondGlyph;
			this.m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008EC8 File Offset: 0x000070C8
		internal void ConvertLegacyKerningData()
		{
			this.m_FirstGlyphAdjustments.xAdvance = this.xOffset;
		}

		// Token: 0x0400011C RID: 284
		[FormerlySerializedAs("AscII_Left")]
		[SerializeField]
		private uint m_FirstGlyph;

		// Token: 0x0400011D RID: 285
		[SerializeField]
		private GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

		// Token: 0x0400011E RID: 286
		[FormerlySerializedAs("AscII_Right")]
		[SerializeField]
		private uint m_SecondGlyph;

		// Token: 0x0400011F RID: 287
		[SerializeField]
		private GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

		// Token: 0x04000120 RID: 288
		[FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;

		// Token: 0x04000121 RID: 289
		internal static KerningPair empty = new KerningPair(0U, default(GlyphValueRecord_Legacy), 0U, default(GlyphValueRecord_Legacy));

		// Token: 0x04000122 RID: 290
		[SerializeField]
		private bool m_IgnoreSpacingAdjustments;
	}
}
