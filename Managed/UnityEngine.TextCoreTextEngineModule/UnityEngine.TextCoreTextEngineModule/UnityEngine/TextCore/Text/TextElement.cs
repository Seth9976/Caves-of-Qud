using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public abstract class TextElement
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		public uint unicode
		{
			get
			{
				return this.m_Unicode;
			}
			set
			{
				this.m_Unicode = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000C004 File Offset: 0x0000A204
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000C01C File Offset: 0x0000A21C
		public TextAsset textAsset
		{
			get
			{
				return this.m_TextAsset;
			}
			set
			{
				this.m_TextAsset = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000C028 File Offset: 0x0000A228
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000C040 File Offset: 0x0000A240
		public Glyph glyph
		{
			get
			{
				return this.m_Glyph;
			}
			set
			{
				this.m_Glyph = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000C04C File Offset: 0x0000A24C
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000C064 File Offset: 0x0000A264
		public uint glyphIndex
		{
			get
			{
				return this.m_GlyphIndex;
			}
			set
			{
				this.m_GlyphIndex = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000C070 File Offset: 0x0000A270
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000C088 File Offset: 0x0000A288
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x0400018F RID: 399
		[SerializeField]
		protected TextElementType m_ElementType;

		// Token: 0x04000190 RID: 400
		[SerializeField]
		internal uint m_Unicode;

		// Token: 0x04000191 RID: 401
		internal TextAsset m_TextAsset;

		// Token: 0x04000192 RID: 402
		internal Glyph m_Glyph;

		// Token: 0x04000193 RID: 403
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x04000194 RID: 404
		[SerializeField]
		internal float m_Scale;
	}
}
