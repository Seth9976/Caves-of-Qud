using System;
using System.Text;
using AiUnity.NLog.Core.Internal;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002B1 RID: 689
	internal sealed class ConditionTokenizer
	{
		// Token: 0x06001825 RID: 6181 RVA: 0x0007E150 File Offset: 0x0007C350
		public ConditionTokenizer(SimpleStringReader stringReader)
		{
			this.stringReader = stringReader;
			this.TokenType = ConditionTokenType.BeginningOfInput;
			this.GetNextToken();
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0007E16C File Offset: 0x0007C36C
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0007E174 File Offset: 0x0007C374
		public int TokenPosition { get; private set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0007E17D File Offset: 0x0007C37D
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0007E185 File Offset: 0x0007C385
		public ConditionTokenType TokenType { get; private set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0007E18E File Offset: 0x0007C38E
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0007E196 File Offset: 0x0007C396
		public string TokenValue { get; private set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0007E1A0 File Offset: 0x0007C3A0
		public string StringTokenValue
		{
			get
			{
				string tokenValue = this.TokenValue;
				return tokenValue.Substring(1, tokenValue.Length - 2).Replace("''", "'");
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0007E1D4 File Offset: 0x0007C3D4
		public void Expect(ConditionTokenType tokenType)
		{
			if (this.TokenType != tokenType)
			{
				throw new ConditionParseException(string.Concat(new string[]
				{
					"Expected token of type: ",
					tokenType.ToString(),
					", got ",
					this.TokenType.ToString(),
					" (",
					this.TokenValue,
					")."
				}));
			}
			this.GetNextToken();
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0007E251 File Offset: 0x0007C451
		public string EatKeyword()
		{
			if (this.TokenType != ConditionTokenType.Keyword)
			{
				throw new ConditionParseException("Identifier expected");
			}
			string tokenValue = this.TokenValue;
			this.GetNextToken();
			return tokenValue;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0007E273 File Offset: 0x0007C473
		public bool IsKeyword(string keyword)
		{
			return this.TokenType == ConditionTokenType.Keyword && this.TokenValue.Equals(keyword, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0007E292 File Offset: 0x0007C492
		public bool IsEOF()
		{
			return this.TokenType == ConditionTokenType.EndOfInput;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0007E29F File Offset: 0x0007C49F
		public bool IsNumber()
		{
			return this.TokenType == ConditionTokenType.Number;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0007E2AA File Offset: 0x0007C4AA
		public bool IsToken(ConditionTokenType tokenType)
		{
			return this.TokenType == tokenType;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0007E2B8 File Offset: 0x0007C4B8
		public void GetNextToken()
		{
			if (this.TokenType == ConditionTokenType.EndOfInput)
			{
				throw new ConditionParseException("Cannot read past end of stream.");
			}
			this.SkipWhitespace();
			this.TokenPosition = this.TokenPosition;
			int num = this.PeekChar();
			if (num == -1)
			{
				this.TokenType = ConditionTokenType.EndOfInput;
				return;
			}
			char c = (char)num;
			if (char.IsDigit(c))
			{
				this.ParseNumber(c);
				return;
			}
			if (c == '\'')
			{
				this.ParseSingleQuotedString(c);
				return;
			}
			if (c == '_' || char.IsLetter(c))
			{
				this.ParseKeyword(c);
				return;
			}
			if (c == '}' || c == ':')
			{
				this.TokenType = ConditionTokenType.EndOfInput;
				return;
			}
			this.TokenValue = c.ToString();
			if (c == '<')
			{
				this.ReadChar();
				int num2 = this.PeekChar();
				if (num2 == 62)
				{
					this.TokenType = ConditionTokenType.NotEqual;
					this.TokenValue = "<>";
					this.ReadChar();
					return;
				}
				if (num2 == 61)
				{
					this.TokenType = ConditionTokenType.LessThanOrEqualTo;
					this.TokenValue = "<=";
					this.ReadChar();
					return;
				}
				this.TokenType = ConditionTokenType.LessThan;
				this.TokenValue = "<";
				return;
			}
			else if (c == '>')
			{
				this.ReadChar();
				if (this.PeekChar() == 61)
				{
					this.TokenType = ConditionTokenType.GreaterThanOrEqualTo;
					this.TokenValue = ">=";
					this.ReadChar();
					return;
				}
				this.TokenType = ConditionTokenType.GreaterThan;
				this.TokenValue = ">";
				return;
			}
			else if (c == '!')
			{
				this.ReadChar();
				if (this.PeekChar() == 61)
				{
					this.TokenType = ConditionTokenType.NotEqual;
					this.TokenValue = "!=";
					this.ReadChar();
					return;
				}
				this.TokenType = ConditionTokenType.Not;
				this.TokenValue = "!";
				return;
			}
			else if (c == '&')
			{
				this.ReadChar();
				if (this.PeekChar() == 38)
				{
					this.TokenType = ConditionTokenType.And;
					this.TokenValue = "&&";
					this.ReadChar();
					return;
				}
				throw new ConditionParseException("Expected '&&' but got '&'");
			}
			else if (c == '|')
			{
				this.ReadChar();
				if (this.PeekChar() == 124)
				{
					this.TokenType = ConditionTokenType.Or;
					this.TokenValue = "||";
					this.ReadChar();
					return;
				}
				throw new ConditionParseException("Expected '||' but got '|'");
			}
			else if (c == '=')
			{
				this.ReadChar();
				if (this.PeekChar() == 61)
				{
					this.TokenType = ConditionTokenType.EqualTo;
					this.TokenValue = "==";
					this.ReadChar();
					return;
				}
				this.TokenType = ConditionTokenType.EqualTo;
				this.TokenValue = "=";
				return;
			}
			else
			{
				if (c < ' ' || c >= '\u0080')
				{
					throw new ConditionParseException("Invalid token: " + c.ToString());
				}
				ConditionTokenType conditionTokenType = ConditionTokenizer.charIndexToTokenType[(int)c];
				if (conditionTokenType != ConditionTokenType.Invalid)
				{
					this.TokenType = conditionTokenType;
					this.TokenValue = new string(c, 1);
					this.ReadChar();
					return;
				}
				throw new ConditionParseException("Invalid punctuation: " + c.ToString());
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0007E560 File Offset: 0x0007C760
		private static ConditionTokenType[] BuildCharIndexToTokenType()
		{
			ConditionTokenizer.CharToTokenType[] array = new ConditionTokenizer.CharToTokenType[]
			{
				new ConditionTokenizer.CharToTokenType('(', ConditionTokenType.LeftParen),
				new ConditionTokenizer.CharToTokenType(')', ConditionTokenType.RightParen),
				new ConditionTokenizer.CharToTokenType('.', ConditionTokenType.Dot),
				new ConditionTokenizer.CharToTokenType(',', ConditionTokenType.Comma),
				new ConditionTokenizer.CharToTokenType('!', ConditionTokenType.Not),
				new ConditionTokenizer.CharToTokenType('-', ConditionTokenType.Minus)
			};
			ConditionTokenType[] array2 = new ConditionTokenType[128];
			for (int i = 0; i < 128; i++)
			{
				array2[i] = ConditionTokenType.Invalid;
			}
			foreach (ConditionTokenizer.CharToTokenType charToTokenType in array)
			{
				array2[(int)charToTokenType.Character] = charToTokenType.TokenType;
			}
			return array2;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0007E624 File Offset: 0x0007C824
		private void ParseSingleQuotedString(char ch)
		{
			this.TokenType = ConditionTokenType.String;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				ch = (char)num;
				stringBuilder.Append((char)this.ReadChar());
				if (ch == '\'')
				{
					if (this.PeekChar() != 39)
					{
						break;
					}
					stringBuilder.Append('\'');
					this.ReadChar();
				}
			}
			if (num == -1)
			{
				throw new ConditionParseException("String literal is missing a closing quote character.");
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0007E6A8 File Offset: 0x0007C8A8
		private void ParseKeyword(char ch)
		{
			this.TokenType = ConditionTokenType.Keyword;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1 && ((ushort)num == 95 || (ushort)num == 45 || char.IsLetterOrDigit((char)num)))
			{
				stringBuilder.Append((char)this.ReadChar());
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0007E710 File Offset: 0x0007C910
		private void ParseNumber(char ch)
		{
			this.TokenType = ConditionTokenType.Number;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ch);
			this.ReadChar();
			int num;
			while ((num = this.PeekChar()) != -1)
			{
				ch = (char)num;
				if (!char.IsDigit(ch) && ch != '.')
				{
					break;
				}
				stringBuilder.Append((char)this.ReadChar());
			}
			this.TokenValue = stringBuilder.ToString();
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0007E774 File Offset: 0x0007C974
		private void SkipWhitespace()
		{
			int num;
			while ((num = this.PeekChar()) != -1 && char.IsWhiteSpace((char)num))
			{
				this.ReadChar();
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0007E79E File Offset: 0x0007C99E
		private int PeekChar()
		{
			return this.stringReader.Peek();
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0007E7AB File Offset: 0x0007C9AB
		private int ReadChar()
		{
			return this.stringReader.Read();
		}

		// Token: 0x04000E70 RID: 3696
		private static readonly ConditionTokenType[] charIndexToTokenType = ConditionTokenizer.BuildCharIndexToTokenType();

		// Token: 0x04000E71 RID: 3697
		private readonly SimpleStringReader stringReader;

		// Token: 0x02001684 RID: 5764
		private struct CharToTokenType
		{
			// Token: 0x0600D845 RID: 55365 RVA: 0x004FD901 File Offset: 0x004FBB01
			public CharToTokenType(char character, ConditionTokenType tokenType)
			{
				this.Character = character;
				this.TokenType = tokenType;
			}

			// Token: 0x04005A92 RID: 23186
			public readonly char Character;

			// Token: 0x04005A93 RID: 23187
			public readonly ConditionTokenType TokenType;
		}
	}
}
