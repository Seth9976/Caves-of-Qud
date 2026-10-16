using System;

namespace TMPro
{
	// Token: 0x02000065 RID: 101
	internal struct CharacterElement
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0002B193 File Offset: 0x00029393
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0002B19B File Offset: 0x0002939B
		public uint Unicode
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

		// Token: 0x0600052D RID: 1325 RVA: 0x0002B1A4 File Offset: 0x000293A4
		public CharacterElement(TMP_TextElement textElement)
		{
			this.m_Unicode = textElement.unicode;
			this.m_TextElement = textElement;
		}

		// Token: 0x040004B9 RID: 1209
		private uint m_Unicode;

		// Token: 0x040004BA RID: 1210
		private TMP_TextElement m_TextElement;
	}
}
