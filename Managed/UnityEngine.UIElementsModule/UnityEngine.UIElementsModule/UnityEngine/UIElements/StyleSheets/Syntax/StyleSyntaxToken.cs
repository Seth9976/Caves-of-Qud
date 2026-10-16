using System;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006FD RID: 1789
	internal struct StyleSyntaxToken
	{
		// Token: 0x0600338A RID: 13194 RVA: 0x000C9745 File Offset: 0x000C7945
		public StyleSyntaxToken(StyleSyntaxTokenType t)
		{
			this.type = t;
			this.text = null;
			this.number = 0;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000C975D File Offset: 0x000C795D
		public StyleSyntaxToken(StyleSyntaxTokenType type, string text)
		{
			this.type = type;
			this.text = text;
			this.number = 0;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000C9775 File Offset: 0x000C7975
		public StyleSyntaxToken(StyleSyntaxTokenType type, int number)
		{
			this.type = type;
			this.text = null;
			this.number = number;
		}

		// Token: 0x04001A85 RID: 6789
		public StyleSyntaxTokenType type;

		// Token: 0x04001A86 RID: 6790
		public string text;

		// Token: 0x04001A87 RID: 6791
		public int number;
	}
}
