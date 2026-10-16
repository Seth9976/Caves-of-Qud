using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000022 RID: 34
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct LinkInfo
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000478C File Offset: 0x0000298C
		internal void SetLinkId(char[] text, int startIndex, int length)
		{
			bool flag = this.linkId == null || this.linkId.Length < length;
			if (flag)
			{
				this.linkId = new char[length];
			}
			for (int i = 0; i < length; i++)
			{
				this.linkId[i] = text[startIndex + i];
			}
			this.linkIdLength = length;
			this.m_LinkIdString = null;
			this.m_LinkTextString = null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000047F4 File Offset: 0x000029F4
		public string GetLinkText(TextInfo textInfo)
		{
			bool flag = string.IsNullOrEmpty(this.m_LinkTextString);
			if (flag)
			{
				for (int i = this.linkTextfirstCharacterIndex; i < this.linkTextfirstCharacterIndex + this.linkTextLength; i++)
				{
					this.m_LinkTextString += ((char)textInfo.textElementInfo[i].character).ToString();
				}
			}
			return this.m_LinkTextString;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000486C File Offset: 0x00002A6C
		public string GetLinkId()
		{
			bool flag = string.IsNullOrEmpty(this.m_LinkIdString);
			if (flag)
			{
				this.m_LinkIdString = new string(this.linkId, 0, this.linkIdLength);
			}
			return this.m_LinkIdString;
		}

		// Token: 0x040000AA RID: 170
		public int hashCode;

		// Token: 0x040000AB RID: 171
		public int linkIdFirstCharacterIndex;

		// Token: 0x040000AC RID: 172
		public int linkIdLength;

		// Token: 0x040000AD RID: 173
		public int linkTextfirstCharacterIndex;

		// Token: 0x040000AE RID: 174
		public int linkTextLength;

		// Token: 0x040000AF RID: 175
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal char[] linkId;

		// Token: 0x040000B0 RID: 176
		private string m_LinkIdString;

		// Token: 0x040000B1 RID: 177
		private string m_LinkTextString;
	}
}
