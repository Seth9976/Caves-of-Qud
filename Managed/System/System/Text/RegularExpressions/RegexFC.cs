using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000206 RID: 518
	internal sealed class RegexFC
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x0003E4D7 File Offset: 0x0003C6D7
		public RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003E4F4 File Offset: 0x0003C6F4
		public RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '\uffff')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this.CaseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003E563 File Offset: 0x0003C763
		public RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this.CaseInsensitive = caseInsensitive;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003E588 File Offset: 0x0003C788
		public bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this.CaseInsensitive |= fc.CaseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003E603 File Offset: 0x0003C803
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x0003E60B File Offset: 0x0003C80B
		public bool CaseInsensitive { get; private set; }

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003E614 File Offset: 0x0003C814
		public string GetFirstChars(CultureInfo culture)
		{
			if (this.CaseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x04000921 RID: 2337
		private RegexCharClass _cc;

		// Token: 0x04000922 RID: 2338
		public bool _nullable;
	}
}
