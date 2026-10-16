using System;

namespace System.Net
{
	// Token: 0x02000677 RID: 1655
	internal class DigestHeaderParser
	{
		// Token: 0x060033FC RID: 13308 RVA: 0x000B51E0 File Offset: 0x000B33E0
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000B5206 File Offset: 0x000B3406
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000B5210 File Offset: 0x000B3410
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000B521A File Offset: 0x000B341A
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000B5224 File Offset: 0x000B3424
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000B522E File Offset: 0x000B342E
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000B5238 File Offset: 0x000B3438
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string text;
				string text2;
				if (!this.GetKeywordAndValue(out text, out text2))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, text);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text2;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000B5304 File Offset: 0x000B3504
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				c = text[num];
			}
			this.pos--;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000B5364 File Offset: 0x000B3564
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000B53D0 File Offset: 0x000B35D0
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 < this.length)
			{
				string text = this.header;
				int num = this.pos;
				this.pos = num + 1;
				if (text[num] == '=')
				{
					this.SkipWhitespace();
					if (this.pos + 1 >= this.length)
					{
						return false;
					}
					bool flag = false;
					if (this.header[this.pos] == '"')
					{
						this.pos++;
						flag = true;
					}
					int num2 = this.pos;
					if (flag)
					{
						this.pos = this.header.IndexOf('"', this.pos);
						if (this.pos == -1)
						{
							return false;
						}
					}
					else
					{
						do
						{
							char c = this.header[this.pos];
							if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
							{
								break;
							}
							num = this.pos + 1;
							this.pos = num;
						}
						while (num < this.length);
						if (this.pos >= this.length && num2 == this.pos)
						{
							return false;
						}
					}
					value = this.header.Substring(num2, this.pos - num2);
					this.pos += (flag ? 2 : 1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001E99 RID: 7833
		private string header;

		// Token: 0x04001E9A RID: 7834
		private int length;

		// Token: 0x04001E9B RID: 7835
		private int pos;

		// Token: 0x04001E9C RID: 7836
		private static string[] keywords = new string[] { "realm", "opaque", "nonce", "algorithm", "qop" };

		// Token: 0x04001E9D RID: 7837
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
