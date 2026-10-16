using System;

namespace TMPro
{
	// Token: 0x02000078 RID: 120
	public struct TMP_LinkInfo
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x0002E388 File Offset: 0x0002C588
		internal void SetLinkID(char[] text, int startIndex, int length)
		{
			if (this.linkID == null || this.linkID.Length < length)
			{
				this.linkID = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkID[i] = text[startIndex + i];
			}
			this.linkIdLength = length;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0002E3D4 File Offset: 0x0002C5D4
		public string GetLinkText()
		{
			string text = string.Empty;
			TMP_TextInfo textInfo = this.textComponent.textInfo;
			for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
			{
				text += textInfo.characterInfo[i].character.ToString();
			}
			return text;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0002E42E File Offset: 0x0002C62E
		public string GetLink()
		{
			return this.GetLinkID();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0002E436 File Offset: 0x0002C636
		public string GetLinkID()
		{
			if (this.textComponent == null)
			{
				return string.Empty;
			}
			return new string(this.linkID, 0, this.linkIdLength);
		}

		// Token: 0x0400051D RID: 1309
		public TMP_Text textComponent;

		// Token: 0x0400051E RID: 1310
		public int hashCode;

		// Token: 0x0400051F RID: 1311
		public int linkIdFirstCharacterIndex;

		// Token: 0x04000520 RID: 1312
		public int linkIdLength;

		// Token: 0x04000521 RID: 1313
		public int linkTextfirstCharacterIndex;

		// Token: 0x04000522 RID: 1314
		public int linkTextLength;

		// Token: 0x04000523 RID: 1315
		internal char[] linkID;
	}
}
