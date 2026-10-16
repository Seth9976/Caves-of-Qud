using System;

namespace TMPro
{
	// Token: 0x02000079 RID: 121
	public struct TMP_WordInfo
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x0002E460 File Offset: 0x0002C660
		public string GetWord()
		{
			string text = string.Empty;
			TMP_CharacterInfo[] characterInfo = this.textComponent.textInfo.characterInfo;
			for (int i = this.firstCharacterIndex; i < this.lastCharacterIndex + 1; i++)
			{
				text += characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x04000524 RID: 1316
		public TMP_Text textComponent;

		// Token: 0x04000525 RID: 1317
		public int firstCharacterIndex;

		// Token: 0x04000526 RID: 1318
		public int lastCharacterIndex;

		// Token: 0x04000527 RID: 1319
		public int characterCount;
	}
}
