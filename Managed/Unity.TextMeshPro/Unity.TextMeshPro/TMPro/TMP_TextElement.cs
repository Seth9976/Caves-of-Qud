using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class TMP_TextElement
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00022804 File Offset: 0x00020A04
		public TextElementType elementType
		{
			get
			{
				return this.m_ElementType;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0002280C File Offset: 0x00020A0C
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00022814 File Offset: 0x00020A14
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0002281D File Offset: 0x00020A1D
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00022825 File Offset: 0x00020A25
		public TMP_Asset textAsset
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0002282E File Offset: 0x00020A2E
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00022836 File Offset: 0x00020A36
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0002283F File Offset: 0x00020A3F
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00022847 File Offset: 0x00020A47
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00022850 File Offset: 0x00020A50
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00022858 File Offset: 0x00020A58
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

		// Token: 0x0400048F RID: 1167
		[SerializeField]
		internal TextElementType m_ElementType;

		// Token: 0x04000490 RID: 1168
		[SerializeField]
		internal uint m_Unicode;

		// Token: 0x04000491 RID: 1169
		internal TMP_Asset m_TextAsset;

		// Token: 0x04000492 RID: 1170
		internal Glyph m_Glyph;

		// Token: 0x04000493 RID: 1171
		[SerializeField]
		internal uint m_GlyphIndex;

		// Token: 0x04000494 RID: 1172
		[SerializeField]
		internal float m_Scale;
	}
}
