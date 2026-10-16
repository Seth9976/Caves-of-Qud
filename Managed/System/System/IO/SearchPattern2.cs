using System;

namespace System.IO
{
	// Token: 0x02000528 RID: 1320
	internal class SearchPattern2
	{
		// Token: 0x06002A86 RID: 10886 RVA: 0x000929CD File Offset: 0x00090BCD
		public SearchPattern2(string pattern)
			: this(pattern, false)
		{
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000929D7 File Offset: 0x00090BD7
		public SearchPattern2(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.pattern = pattern;
			this.Compile(pattern);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000929F4 File Offset: 0x00090BF4
		public bool IsMatch(string text, bool ignorecase)
		{
			if (!this.hasWildcard && string.Compare(this.pattern, text, ignorecase) == 0)
			{
				return true;
			}
			string fileName = Path.GetFileName(text);
			if (!this.hasWildcard)
			{
				return string.Compare(this.pattern, fileName, ignorecase) == 0;
			}
			return this.Match(this.ops, fileName, 0);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00092A4B File Offset: 0x00090C4B
		public bool IsMatch(string text)
		{
			return this.IsMatch(text, this.ignore);
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x00092A5A File Offset: 0x00090C5A
		public bool HasWildcard
		{
			get
			{
				return this.hasWildcard;
			}
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x00092A64 File Offset: 0x00090C64
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern2.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern: '" + pattern + "'");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.True);
				this.hasWildcard = true;
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern2.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern2.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyChar);
						i++;
						this.hasWildcard = true;
					}
					else
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern2.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLower();
						}
						i = num;
					}
				}
				else
				{
					op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyString);
					i++;
					this.hasWildcard = true;
				}
				if (op == null)
				{
					this.ops = op2;
				}
				else
				{
					op.Next = op2;
				}
				op = op2;
			}
			if (op == null)
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.End);
				return;
			}
			op.Next = new SearchPattern2.Op(SearchPattern2.OpCode.End);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00092B90 File Offset: 0x00090D90
		private bool Match(SearchPattern2.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern2.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLower();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern2.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern2.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern2.OpCode.End:
					return ptr == text.Length;
				case SearchPattern2.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x04001711 RID: 5905
		private SearchPattern2.Op ops;

		// Token: 0x04001712 RID: 5906
		private bool ignore;

		// Token: 0x04001713 RID: 5907
		private bool hasWildcard;

		// Token: 0x04001714 RID: 5908
		private string pattern;

		// Token: 0x04001715 RID: 5909
		internal static readonly char[] WildcardChars = new char[] { '*', '?' };

		// Token: 0x04001716 RID: 5910
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x02000529 RID: 1321
		private class Op
		{
			// Token: 0x06002A8E RID: 10894 RVA: 0x00092C92 File Offset: 0x00090E92
			public Op(SearchPattern2.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04001717 RID: 5911
			public SearchPattern2.OpCode Code;

			// Token: 0x04001718 RID: 5912
			public string Argument;

			// Token: 0x04001719 RID: 5913
			public SearchPattern2.Op Next;
		}

		// Token: 0x0200052A RID: 1322
		private enum OpCode
		{
			// Token: 0x0400171B RID: 5915
			ExactString,
			// Token: 0x0400171C RID: 5916
			AnyChar,
			// Token: 0x0400171D RID: 5917
			AnyString,
			// Token: 0x0400171E RID: 5918
			End,
			// Token: 0x0400171F RID: 5919
			True
		}
	}
}
