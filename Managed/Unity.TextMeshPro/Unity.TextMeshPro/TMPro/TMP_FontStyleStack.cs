using System;

namespace TMPro
{
	// Token: 0x02000069 RID: 105
	public struct TMP_FontStyleStack
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x0002B4C0 File Offset: 0x000296C0
		public void Clear()
		{
			this.bold = 0;
			this.italic = 0;
			this.underline = 0;
			this.strikethrough = 0;
			this.highlight = 0;
			this.superscript = 0;
			this.subscript = 0;
			this.uppercase = 0;
			this.lowercase = 0;
			this.smallcaps = 0;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0002B514 File Offset: 0x00029714
		public byte Add(FontStyles style)
		{
			if (style <= FontStyles.UpperCase)
			{
				switch (style)
				{
				case FontStyles.Bold:
					this.bold += 1;
					return this.bold;
				case FontStyles.Italic:
					this.italic += 1;
					return this.italic;
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
					this.underline += 1;
					return this.underline;
				default:
					if (style == FontStyles.LowerCase)
					{
						this.lowercase += 1;
						return this.lowercase;
					}
					if (style == FontStyles.UpperCase)
					{
						this.uppercase += 1;
						return this.uppercase;
					}
					break;
				}
			}
			else if (style <= FontStyles.Superscript)
			{
				if (style == FontStyles.Strikethrough)
				{
					this.strikethrough += 1;
					return this.strikethrough;
				}
				if (style == FontStyles.Superscript)
				{
					this.superscript += 1;
					return this.superscript;
				}
			}
			else
			{
				if (style == FontStyles.Subscript)
				{
					this.subscript += 1;
					return this.subscript;
				}
				if (style == FontStyles.Highlight)
				{
					this.highlight += 1;
					return this.highlight;
				}
			}
			return 0;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002B654 File Offset: 0x00029854
		public byte Remove(FontStyles style)
		{
			if (style <= FontStyles.UpperCase)
			{
				switch (style)
				{
				case FontStyles.Bold:
					if (this.bold > 1)
					{
						this.bold -= 1;
					}
					else
					{
						this.bold = 0;
					}
					return this.bold;
				case FontStyles.Italic:
					if (this.italic > 1)
					{
						this.italic -= 1;
					}
					else
					{
						this.italic = 0;
					}
					return this.italic;
				case FontStyles.Bold | FontStyles.Italic:
					break;
				case FontStyles.Underline:
					if (this.underline > 1)
					{
						this.underline -= 1;
					}
					else
					{
						this.underline = 0;
					}
					return this.underline;
				default:
					if (style == FontStyles.LowerCase)
					{
						if (this.lowercase > 1)
						{
							this.lowercase -= 1;
						}
						else
						{
							this.lowercase = 0;
						}
						return this.lowercase;
					}
					if (style == FontStyles.UpperCase)
					{
						if (this.uppercase > 1)
						{
							this.uppercase -= 1;
						}
						else
						{
							this.uppercase = 0;
						}
						return this.uppercase;
					}
					break;
				}
			}
			else if (style <= FontStyles.Superscript)
			{
				if (style == FontStyles.Strikethrough)
				{
					if (this.strikethrough > 1)
					{
						this.strikethrough -= 1;
					}
					else
					{
						this.strikethrough = 0;
					}
					return this.strikethrough;
				}
				if (style == FontStyles.Superscript)
				{
					if (this.superscript > 1)
					{
						this.superscript -= 1;
					}
					else
					{
						this.superscript = 0;
					}
					return this.superscript;
				}
			}
			else
			{
				if (style == FontStyles.Subscript)
				{
					if (this.subscript > 1)
					{
						this.subscript -= 1;
					}
					else
					{
						this.subscript = 0;
					}
					return this.subscript;
				}
				if (style == FontStyles.Highlight)
				{
					if (this.highlight > 1)
					{
						this.highlight -= 1;
					}
					else
					{
						this.highlight = 0;
					}
					return this.highlight;
				}
			}
			return 0;
		}

		// Token: 0x040004C5 RID: 1221
		public byte bold;

		// Token: 0x040004C6 RID: 1222
		public byte italic;

		// Token: 0x040004C7 RID: 1223
		public byte underline;

		// Token: 0x040004C8 RID: 1224
		public byte strikethrough;

		// Token: 0x040004C9 RID: 1225
		public byte highlight;

		// Token: 0x040004CA RID: 1226
		public byte superscript;

		// Token: 0x040004CB RID: 1227
		public byte subscript;

		// Token: 0x040004CC RID: 1228
		public byte uppercase;

		// Token: 0x040004CD RID: 1229
		public byte lowercase;

		// Token: 0x040004CE RID: 1230
		public byte smallcaps;
	}
}
