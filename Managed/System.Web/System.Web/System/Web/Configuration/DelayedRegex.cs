using System;
using System.Text.RegularExpressions;

namespace System.Web.Configuration
{
	// Token: 0x0200055F RID: 1375
	internal class DelayedRegex
	{
		// Token: 0x06003B0D RID: 15117 RVA: 0x0009C6C7 File Offset: 0x0009A8C7
		internal DelayedRegex(string s)
		{
			this._regex = null;
			this._regstring = s;
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x0009C6DD File Offset: 0x0009A8DD
		internal Match Match(string s)
		{
			this.EnsureRegex();
			return this._regex.Match(s);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x0009C6F1 File Offset: 0x0009A8F1
		internal int GroupNumberFromName(string name)
		{
			this.EnsureRegex();
			return this._regex.GroupNumberFromName(name);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x0009C708 File Offset: 0x0009A908
		internal void EnsureRegex()
		{
			string regstring = this._regstring;
			if (this._regex == null)
			{
				this._regex = new Regex(regstring);
				this._regstring = null;
			}
		}

		// Token: 0x0400200D RID: 8205
		private string _regstring;

		// Token: 0x0400200E RID: 8206
		private Regex _regex;
	}
}
