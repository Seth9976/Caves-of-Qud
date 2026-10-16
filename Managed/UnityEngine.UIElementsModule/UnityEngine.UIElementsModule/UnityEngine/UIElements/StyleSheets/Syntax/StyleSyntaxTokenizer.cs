using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006FE RID: 1790
	internal class StyleSyntaxTokenizer
	{
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x000C9790 File Offset: 0x000C7990
		public StyleSyntaxToken current
		{
			get
			{
				bool flag = this.m_CurrentTokenIndex < 0 || this.m_CurrentTokenIndex >= this.m_Tokens.Count;
				StyleSyntaxToken styleSyntaxToken;
				if (flag)
				{
					styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
				}
				else
				{
					styleSyntaxToken = this.m_Tokens[this.m_CurrentTokenIndex];
				}
				return styleSyntaxToken;
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000C97E4 File Offset: 0x000C79E4
		public StyleSyntaxToken MoveNext()
		{
			StyleSyntaxToken styleSyntaxToken = this.current;
			bool flag = styleSyntaxToken.type == StyleSyntaxTokenType.Unknown;
			StyleSyntaxToken styleSyntaxToken2;
			if (flag)
			{
				styleSyntaxToken2 = styleSyntaxToken;
			}
			else
			{
				this.m_CurrentTokenIndex++;
				styleSyntaxToken = this.current;
				bool flag2 = this.m_CurrentTokenIndex == this.m_Tokens.Count;
				if (flag2)
				{
					this.m_CurrentTokenIndex = -1;
				}
				styleSyntaxToken2 = styleSyntaxToken;
			}
			return styleSyntaxToken2;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000C9844 File Offset: 0x000C7A44
		public StyleSyntaxToken PeekNext()
		{
			int num = this.m_CurrentTokenIndex + 1;
			bool flag = this.m_CurrentTokenIndex < 0 || num >= this.m_Tokens.Count;
			StyleSyntaxToken styleSyntaxToken;
			if (flag)
			{
				styleSyntaxToken = new StyleSyntaxToken(StyleSyntaxTokenType.Unknown);
			}
			else
			{
				styleSyntaxToken = this.m_Tokens[num];
			}
			return styleSyntaxToken;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000C9898 File Offset: 0x000C7A98
		public void Tokenize(string syntax)
		{
			this.m_Tokens.Clear();
			this.m_CurrentTokenIndex = 0;
			syntax = syntax.Trim(' ').ToLowerInvariant();
			int i = 0;
			while (i < syntax.Length)
			{
				char c = syntax[i];
				char c2 = c;
				char c3 = c2;
				if (c3 <= '?')
				{
					switch (c3)
					{
					case ' ':
						i = StyleSyntaxTokenizer.GlobCharacter(syntax, i, ' ');
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Space));
						break;
					case '!':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.ExclamationPoint));
						break;
					case '"':
					case '$':
					case '%':
					case '(':
					case ')':
						goto IL_02E1;
					case '#':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.HashMark));
						break;
					case '&':
					{
						bool flag = !StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '&');
						if (flag)
						{
							string text = ((i + 1 < syntax.Length) ? syntax[i + 1].ToString() : "EOF");
							Debug.LogAssertionFormat("Expected '&' got '{0}'", new object[] { text });
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
						}
						else
						{
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleAmpersand));
							i++;
						}
						break;
					}
					case '\'':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleQuote));
						break;
					case '*':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Asterisk));
						break;
					case '+':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Plus));
						break;
					case ',':
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Comma));
						break;
					default:
						switch (c3)
						{
						case '<':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.LessThan));
							break;
						case '=':
							goto IL_02E1;
						case '>':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.GreaterThan));
							break;
						case '?':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.QuestionMark));
							break;
						default:
							goto IL_02E1;
						}
						break;
					}
				}
				else if (c3 != '[')
				{
					if (c3 != ']')
					{
						switch (c3)
						{
						case '{':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBrace));
							break;
						case '|':
						{
							bool flag2 = StyleSyntaxTokenizer.IsNextCharacter(syntax, i, '|');
							if (flag2)
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.DoubleBar));
								i++;
							}
							else
							{
								this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.SingleBar));
							}
							break;
						}
						case '}':
							this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBrace));
							break;
						default:
							goto IL_02E1;
						}
					}
					else
					{
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.CloseBracket));
					}
				}
				else
				{
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.OpenBracket));
				}
				IL_03BC:
				i++;
				continue;
				IL_02E1:
				bool flag3 = char.IsNumber(c);
				if (flag3)
				{
					int num = i;
					int num2 = 1;
					while (StyleSyntaxTokenizer.IsNextNumber(syntax, i))
					{
						i++;
						num2++;
					}
					string text2 = syntax.Substring(num, num2);
					int num3 = int.Parse(text2);
					this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Number, num3));
				}
				else
				{
					bool flag4 = char.IsLetter(c);
					if (flag4)
					{
						int num4 = i;
						int num5 = 1;
						while (StyleSyntaxTokenizer.IsNextLetterOrDash(syntax, i))
						{
							i++;
							num5++;
						}
						string text3 = syntax.Substring(num4, num5);
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.String, text3));
					}
					else
					{
						Debug.LogAssertionFormat("Expected letter or number got '{0}'", new object[] { c });
						this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.Unknown));
					}
				}
				goto IL_03BC;
			}
			this.m_Tokens.Add(new StyleSyntaxToken(StyleSyntaxTokenType.End));
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000C9C8C File Offset: 0x000C7E8C
		private static bool IsNextCharacter(string s, int index, char c)
		{
			return index + 1 < s.Length && s[index + 1] == c;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000C9CB8 File Offset: 0x000C7EB8
		private static bool IsNextLetterOrDash(string s, int index)
		{
			return index + 1 < s.Length && (char.IsLetter(s[index + 1]) || s[index + 1] == '-');
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000C9CF8 File Offset: 0x000C7EF8
		private static bool IsNextNumber(string s, int index)
		{
			return index + 1 < s.Length && char.IsNumber(s[index + 1]);
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000C9D28 File Offset: 0x000C7F28
		private static int GlobCharacter(string s, int index, char c)
		{
			while (StyleSyntaxTokenizer.IsNextCharacter(s, index, c))
			{
				index++;
			}
			return index;
		}

		// Token: 0x04001A88 RID: 6792
		private List<StyleSyntaxToken> m_Tokens = new List<StyleSyntaxToken>();

		// Token: 0x04001A89 RID: 6793
		private int m_CurrentTokenIndex = -1;
	}
}
