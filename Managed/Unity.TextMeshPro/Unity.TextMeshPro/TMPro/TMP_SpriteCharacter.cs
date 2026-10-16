using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class TMP_SpriteCharacter : TMP_TextElement
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00014059 File Offset: 0x00012259
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00014061 File Offset: 0x00012261
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001406A File Offset: 0x0001226A
		public TMP_SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00014079 File Offset: 0x00012279
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000140AD File Offset: 0x000122AD
		public TMP_SpriteCharacter(uint unicode, TMP_SpriteAsset spriteAsset, TMP_SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000140E8 File Offset: 0x000122E8
		internal TMP_SpriteCharacter(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}

		// Token: 0x0400030F RID: 783
		[SerializeField]
		private string m_Name;
	}
}
