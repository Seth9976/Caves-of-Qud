using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class Character : TextElement
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00003EA8 File Offset: 0x000020A8
		public Character()
		{
			this.m_ElementType = TextElementType.Character;
			base.scale = 1f;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003EC8 File Offset: 0x000020C8
		public Character(uint unicode, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003F18 File Offset: 0x00002118
		public Character(uint unicode, FontAsset fontAsset, Glyph glyph)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = fontAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003F65 File Offset: 0x00002165
		internal Character(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Character;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}
	}
}
