using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public class SpriteCharacter : TextElement
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000B0E8 File Offset: 0x000092E8
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000B100 File Offset: 0x00009300
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value == this.m_Name;
				if (!flag)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B127 File Offset: 0x00009327
		public SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B138 File Offset: 0x00009338
		public SpriteCharacter(uint unicode, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B174 File Offset: 0x00009374
		public SpriteCharacter(uint unicode, SpriteAsset spriteAsset, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x0400014E RID: 334
		[SerializeField]
		private string m_Name;
	}
}
