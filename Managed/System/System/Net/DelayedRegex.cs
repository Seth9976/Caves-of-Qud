using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000613 RID: 1555
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x0600312C RID: 12588 RVA: 0x000A9A6C File Offset: 0x000A7C6C
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000A9A89 File Offset: 0x000A7C89
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000A9AA6 File Offset: 0x000A7CA6
		internal Regex AsRegex
		{
			get
			{
				if (this._AsRegex == null)
				{
					this._AsRegex = new Regex(this._AsString + "[/]?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
				}
				return this._AsRegex;
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000A9AD8 File Offset: 0x000A7CD8
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x04001CBA RID: 7354
		private Regex _AsRegex;

		// Token: 0x04001CBB RID: 7355
		private string _AsString;
	}
}
