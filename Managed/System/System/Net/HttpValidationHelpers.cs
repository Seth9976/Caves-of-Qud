using System;

namespace System.Net
{
	// Token: 0x02000567 RID: 1383
	internal static class HttpValidationHelpers
	{
		// Token: 0x06002CA8 RID: 11432 RVA: 0x0009868C File Offset: 0x0009688C
		internal static string CheckBadHeaderNameChars(string name)
		{
			if (HttpValidationHelpers.IsInvalidMethodOrHeaderString(name))
			{
				throw new ArgumentException("Specified value has invalid HTTP Header characters.", "name");
			}
			if (HttpValidationHelpers.ContainsNonAsciiChars(name))
			{
				throw new ArgumentException("Specified value has invalid HTTP Header characters.", "name");
			}
			return name;
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000986C0 File Offset: 0x000968C0
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000986F7 File Offset: 0x000968F7
		internal static bool IsValidToken(string token)
		{
			return token.Length > 0 && !HttpValidationHelpers.IsInvalidMethodOrHeaderString(token) && !HttpValidationHelpers.ContainsNonAsciiChars(token);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x00098718 File Offset: 0x00096918
		public static string CheckBadHeaderValueChars(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			value = value.Trim(HttpValidationHelpers.s_httpTrimCharacters);
			int num = 0;
			for (int i = 0; i < value.Length; i++)
			{
				char c = 'ÿ' & value[i];
				switch (num)
				{
				case 0:
					if (c == '\r')
					{
						num = 1;
					}
					else if (c == '\n')
					{
						num = 2;
					}
					else if (c == '\u007f' || (c < ' ' && c != '\t'))
					{
						throw new ArgumentException("Specified value has invalid Control characters.", "value");
					}
					break;
				case 1:
					if (c != '\n')
					{
						throw new ArgumentException("Specified value has invalid CRLF characters.", "value");
					}
					num = 2;
					break;
				case 2:
					if (c != ' ' && c != '\t')
					{
						throw new ArgumentException("Specified value has invalid Control characters.", "value");
					}
					num = 0;
					break;
				}
			}
			if (num != 0)
			{
				throw new ArgumentException("Specified value has invalid CRLF characters.", "value");
			}
			return value;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000987F8 File Offset: 0x000969F8
		public static bool IsInvalidMethodOrHeaderString(string stringValue)
		{
			foreach (char c in stringValue)
			{
				if (c <= '/')
				{
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
						case '\n':
						case '\r':
							return true;
						case '\v':
						case '\f':
							break;
						default:
							if (c == ' ')
							{
								return true;
							}
							break;
						}
					}
					else
					{
						if (c == '"')
						{
							return true;
						}
						switch (c)
						{
						case '\'':
						case '(':
						case ')':
						case ',':
						case '/':
							return true;
						}
					}
				}
				else if (c <= ']')
				{
					switch (c)
					{
					case ':':
					case ';':
					case '<':
					case '=':
					case '>':
					case '?':
					case '@':
						return true;
					default:
						switch (c)
						{
						case '[':
						case '\\':
						case ']':
							return true;
						}
						break;
					}
				}
				else if (c == '{' || c == '}')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001835 RID: 6197
		private static readonly char[] s_httpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };
	}
}
