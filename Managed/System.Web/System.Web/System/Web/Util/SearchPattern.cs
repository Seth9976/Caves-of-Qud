using System;

namespace System.Web.Util
{
	// Token: 0x02000140 RID: 320
	internal class SearchPattern
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00027730 File Offset: 0x00025930
		public SearchPattern(string pattern)
			: this(pattern, false)
		{
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002773A File Offset: 0x0002593A
		public SearchPattern(string pattern, bool ignore)
		{
			this.SetPattern(pattern, ignore);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0002774A File Offset: 0x0002594A
		public void SetPattern(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.Compile(pattern);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002775A File Offset: 0x0002595A
		public bool IsMatch(string text)
		{
			return this.Match(this.ops, text, 0);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002776C File Offset: 0x0002596C
		private void Compile(string pattern)
		{
			if (pattern == null)
			{
				throw new ArgumentException("Invalid search pattern.");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern.Op(SearchPattern.OpCode.True);
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern.Op(SearchPattern.OpCode.AnyChar);
						i++;
					}
					else
					{
						op2 = new SearchPattern.Op(SearchPattern.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern.WildcardChars, i);
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
					op2 = new SearchPattern.Op(SearchPattern.OpCode.AnyString);
					i++;
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
				this.ops = new SearchPattern.Op(SearchPattern.OpCode.End);
				return;
			}
			op.Next = new SearchPattern.Op(SearchPattern.OpCode.End);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00027868 File Offset: 0x00025A68
		private bool Match(SearchPattern.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern.OpCode.ExactString:
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
				case SearchPattern.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern.OpCode.End:
					return ptr == text.Length;
				case SearchPattern.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x04001206 RID: 4614
		private SearchPattern.Op ops;

		// Token: 0x04001207 RID: 4615
		private bool ignore;

		// Token: 0x04001208 RID: 4616
		internal static readonly char[] WildcardChars = new char[] { '*', '?' };

		// Token: 0x02000141 RID: 321
		private class Op
		{
			// Token: 0x06000E97 RID: 3735 RVA: 0x0002794F File Offset: 0x00025B4F
			public Op(SearchPattern.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04001209 RID: 4617
			public SearchPattern.OpCode Code;

			// Token: 0x0400120A RID: 4618
			public string Argument;

			// Token: 0x0400120B RID: 4619
			public SearchPattern.Op Next;
		}

		// Token: 0x02000142 RID: 322
		private enum OpCode
		{
			// Token: 0x0400120D RID: 4621
			ExactString,
			// Token: 0x0400120E RID: 4622
			AnyChar,
			// Token: 0x0400120F RID: 4623
			AnyString,
			// Token: 0x04001210 RID: 4624
			End,
			// Token: 0x04001211 RID: 4625
			True
		}
	}
}
