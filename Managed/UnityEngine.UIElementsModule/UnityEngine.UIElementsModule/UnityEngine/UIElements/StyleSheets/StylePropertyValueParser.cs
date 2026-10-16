using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006F1 RID: 1777
	internal class StylePropertyValueParser
	{
		// Token: 0x0600336B RID: 13163 RVA: 0x000C85E4 File Offset: 0x000C67E4
		public string[] Parse(string propertyValue)
		{
			this.m_PropertyValue = propertyValue;
			this.m_ValueList.Clear();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ParseIndex = 0;
			while (this.m_ParseIndex < this.m_PropertyValue.Length)
			{
				char c = this.m_PropertyValue[this.m_ParseIndex];
				bool flag = char.IsWhiteSpace(c);
				if (flag)
				{
					this.EatSpace();
					this.AddValuePart();
				}
				else
				{
					char c2 = c;
					char c3 = c2;
					if (c3 != '(')
					{
						if (c3 != ',')
						{
							this.m_StringBuilder.Append(c);
						}
						else
						{
							this.EatSpace();
							this.AddValuePart();
							this.m_ValueList.Add(",");
						}
					}
					else
					{
						this.AppendFunction();
					}
				}
				this.m_ParseIndex++;
			}
			string text = this.m_StringBuilder.ToString();
			bool flag2 = !string.IsNullOrEmpty(text);
			if (flag2)
			{
				this.m_ValueList.Add(text);
			}
			return this.m_ValueList.ToArray();
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000C870C File Offset: 0x000C690C
		private void AddValuePart()
		{
			string text = this.m_StringBuilder.ToString();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ValueList.Add(text);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000C874C File Offset: 0x000C694C
		private void AppendFunction()
		{
			while (this.m_ParseIndex < this.m_PropertyValue.Length && this.m_PropertyValue[this.m_ParseIndex] != ')')
			{
				this.m_StringBuilder.Append(this.m_PropertyValue[this.m_ParseIndex]);
				this.m_ParseIndex++;
			}
			this.m_StringBuilder.Append(this.m_PropertyValue[this.m_ParseIndex]);
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000C87D8 File Offset: 0x000C69D8
		private void EatSpace()
		{
			while (this.m_ParseIndex + 1 < this.m_PropertyValue.Length && char.IsWhiteSpace(this.m_PropertyValue[this.m_ParseIndex + 1]))
			{
				this.m_ParseIndex++;
			}
		}

		// Token: 0x04001A33 RID: 6707
		private string m_PropertyValue;

		// Token: 0x04001A34 RID: 6708
		private List<string> m_ValueList = new List<string>();

		// Token: 0x04001A35 RID: 6709
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04001A36 RID: 6710
		private int m_ParseIndex = 0;
	}
}
