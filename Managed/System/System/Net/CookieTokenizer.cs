using System;

namespace System.Net
{
	// Token: 0x0200064E RID: 1614
	internal class CookieTokenizer
	{
		// Token: 0x060032C5 RID: 12997 RVA: 0x000AFF5C File Offset: 0x000AE15C
		internal CookieTokenizer(string tokenStream)
		{
			this.m_length = tokenStream.Length;
			this.m_tokenStream = tokenStream;
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x000AFF77 File Offset: 0x000AE177
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x000AFF7F File Offset: 0x000AE17F
		internal bool EndOfCookie
		{
			get
			{
				return this.m_eofCookie;
			}
			set
			{
				this.m_eofCookie = value;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000AFF88 File Offset: 0x000AE188
		internal bool Eof
		{
			get
			{
				return this.m_index >= this.m_length;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000AFF9B File Offset: 0x000AE19B
		// (set) Token: 0x060032CA RID: 13002 RVA: 0x000AFFA3 File Offset: 0x000AE1A3
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000AFFAC File Offset: 0x000AE1AC
		// (set) Token: 0x060032CC RID: 13004 RVA: 0x000AFFB4 File Offset: 0x000AE1B4
		internal bool Quoted
		{
			get
			{
				return this.m_quoted;
			}
			set
			{
				this.m_quoted = value;
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000AFFBD File Offset: 0x000AE1BD
		// (set) Token: 0x060032CE RID: 13006 RVA: 0x000AFFC5 File Offset: 0x000AE1C5
		internal CookieToken Token
		{
			get
			{
				return this.m_token;
			}
			set
			{
				this.m_token = value;
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000AFFCE File Offset: 0x000AE1CE
		// (set) Token: 0x060032D0 RID: 13008 RVA: 0x000AFFD6 File Offset: 0x000AE1D6
		internal string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000AFFE0 File Offset: 0x000AE1E0
		internal string Extract()
		{
			string text = string.Empty;
			if (this.m_tokenLength != 0)
			{
				text = this.m_tokenStream.Substring(this.m_start, this.m_tokenLength);
				if (!this.Quoted)
				{
					text = text.Trim();
				}
			}
			return text;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000B0024 File Offset: 0x000AE224
		internal CookieToken FindNext(bool ignoreComma, bool ignoreEquals)
		{
			this.m_tokenLength = 0;
			this.m_start = this.m_index;
			while (this.m_index < this.m_length && char.IsWhiteSpace(this.m_tokenStream[this.m_index]))
			{
				this.m_index++;
				this.m_start++;
			}
			CookieToken cookieToken = CookieToken.End;
			int num = 1;
			if (!this.Eof)
			{
				if (this.m_tokenStream[this.m_index] == '"')
				{
					this.Quoted = true;
					this.m_index++;
					bool flag = false;
					while (this.m_index < this.m_length)
					{
						char c = this.m_tokenStream[this.m_index];
						if (!flag && c == '"')
						{
							break;
						}
						if (flag)
						{
							flag = false;
						}
						else if (c == '\\')
						{
							flag = true;
						}
						this.m_index++;
					}
					if (this.m_index < this.m_length)
					{
						this.m_index++;
					}
					this.m_tokenLength = this.m_index - this.m_start;
					num = 0;
					ignoreComma = false;
				}
				while (this.m_index < this.m_length && this.m_tokenStream[this.m_index] != ';' && (ignoreEquals || this.m_tokenStream[this.m_index] != '=') && (ignoreComma || this.m_tokenStream[this.m_index] != ','))
				{
					if (this.m_tokenStream[this.m_index] == ',')
					{
						this.m_start = this.m_index + 1;
						this.m_tokenLength = -1;
						ignoreComma = false;
					}
					this.m_index++;
					this.m_tokenLength += num;
				}
				if (!this.Eof)
				{
					char c2 = this.m_tokenStream[this.m_index];
					if (c2 != ';')
					{
						if (c2 != '=')
						{
							cookieToken = CookieToken.EndCookie;
						}
						else
						{
							cookieToken = CookieToken.Equals;
						}
					}
					else
					{
						cookieToken = CookieToken.EndToken;
					}
					this.m_index++;
				}
			}
			return cookieToken;
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000B0228 File Offset: 0x000AE428
		internal CookieToken Next(bool first, bool parseResponseCookies)
		{
			this.Reset();
			CookieToken cookieToken = this.FindNext(false, false);
			if (cookieToken == CookieToken.EndCookie)
			{
				this.EndOfCookie = true;
			}
			if (cookieToken == CookieToken.End || cookieToken == CookieToken.EndCookie)
			{
				if ((this.Name = this.Extract()).Length != 0)
				{
					this.Token = this.TokenFromName(parseResponseCookies);
					return CookieToken.Attribute;
				}
				return cookieToken;
			}
			else
			{
				this.Name = this.Extract();
				if (first)
				{
					this.Token = CookieToken.CookieName;
				}
				else
				{
					this.Token = this.TokenFromName(parseResponseCookies);
				}
				if (cookieToken == CookieToken.Equals)
				{
					cookieToken = this.FindNext(!first && this.Token == CookieToken.Expires, true);
					if (cookieToken == CookieToken.EndCookie)
					{
						this.EndOfCookie = true;
					}
					this.Value = this.Extract();
					return CookieToken.NameValuePair;
				}
				return CookieToken.Attribute;
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000B02DA File Offset: 0x000AE4DA
		internal void Reset()
		{
			this.m_eofCookie = false;
			this.m_name = string.Empty;
			this.m_quoted = false;
			this.m_start = this.m_index;
			this.m_token = CookieToken.Nothing;
			this.m_tokenLength = 0;
			this.m_value = string.Empty;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000B031C File Offset: 0x000AE51C
		internal CookieToken TokenFromName(bool parseResponseCookies)
		{
			if (!parseResponseCookies)
			{
				for (int i = 0; i < CookieTokenizer.RecognizedServerAttributes.Length; i++)
				{
					if (CookieTokenizer.RecognizedServerAttributes[i].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedServerAttributes[i].Token;
					}
				}
			}
			else
			{
				for (int j = 0; j < CookieTokenizer.RecognizedAttributes.Length; j++)
				{
					if (CookieTokenizer.RecognizedAttributes[j].IsEqualTo(this.Name))
					{
						return CookieTokenizer.RecognizedAttributes[j].Token;
					}
				}
			}
			return CookieToken.Unknown;
		}

		// Token: 0x04001DF1 RID: 7665
		private bool m_eofCookie;

		// Token: 0x04001DF2 RID: 7666
		private int m_index;

		// Token: 0x04001DF3 RID: 7667
		private int m_length;

		// Token: 0x04001DF4 RID: 7668
		private string m_name;

		// Token: 0x04001DF5 RID: 7669
		private bool m_quoted;

		// Token: 0x04001DF6 RID: 7670
		private int m_start;

		// Token: 0x04001DF7 RID: 7671
		private CookieToken m_token;

		// Token: 0x04001DF8 RID: 7672
		private int m_tokenLength;

		// Token: 0x04001DF9 RID: 7673
		private string m_tokenStream;

		// Token: 0x04001DFA RID: 7674
		private string m_value;

		// Token: 0x04001DFB RID: 7675
		private static CookieTokenizer.RecognizedAttribute[] RecognizedAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("Max-Age", CookieToken.MaxAge),
			new CookieTokenizer.RecognizedAttribute("Expires", CookieToken.Expires),
			new CookieTokenizer.RecognizedAttribute("Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("Secure", CookieToken.Secure),
			new CookieTokenizer.RecognizedAttribute("Discard", CookieToken.Discard),
			new CookieTokenizer.RecognizedAttribute("Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("Comment", CookieToken.Comment),
			new CookieTokenizer.RecognizedAttribute("CommentURL", CookieToken.CommentUrl),
			new CookieTokenizer.RecognizedAttribute("HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x04001DFC RID: 7676
		private static CookieTokenizer.RecognizedAttribute[] RecognizedServerAttributes = new CookieTokenizer.RecognizedAttribute[]
		{
			new CookieTokenizer.RecognizedAttribute("$Path", CookieToken.Path),
			new CookieTokenizer.RecognizedAttribute("$Version", CookieToken.Version),
			new CookieTokenizer.RecognizedAttribute("$Domain", CookieToken.Domain),
			new CookieTokenizer.RecognizedAttribute("$Port", CookieToken.Port),
			new CookieTokenizer.RecognizedAttribute("$HttpOnly", CookieToken.HttpOnly)
		};

		// Token: 0x0200064F RID: 1615
		private struct RecognizedAttribute
		{
			// Token: 0x060032D7 RID: 13015 RVA: 0x000B04FC File Offset: 0x000AE6FC
			internal RecognizedAttribute(string name, CookieToken token)
			{
				this.m_name = name;
				this.m_token = token;
			}

			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000B050C File Offset: 0x000AE70C
			internal CookieToken Token
			{
				get
				{
					return this.m_token;
				}
			}

			// Token: 0x060032D9 RID: 13017 RVA: 0x000B0514 File Offset: 0x000AE714
			internal bool IsEqualTo(string value)
			{
				return string.Compare(this.m_name, value, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04001DFD RID: 7677
			private string m_name;

			// Token: 0x04001DFE RID: 7678
			private CookieToken m_token;
		}
	}
}
