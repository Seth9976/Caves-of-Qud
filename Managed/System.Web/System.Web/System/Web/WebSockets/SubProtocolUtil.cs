using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.WebSockets
{
	// Token: 0x02000108 RID: 264
	internal static class SubProtocolUtil
	{
		// Token: 0x06000D9C RID: 3484 RVA: 0x0002502A File Offset: 0x0002322A
		public static bool IsValidSubProtocolName(string subprotocol)
		{
			return !string.IsNullOrEmpty(subprotocol) && subprotocol.All<char>(new Func<char, bool>(SubProtocolUtil.IsValidSubProtocolChar));
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00025048 File Offset: 0x00023248
		private static bool IsValidSubProtocolChar(char c)
		{
			return '!' <= c && c <= '~' && !SubProtocolUtil.IsSeparatorChar(c);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00025060 File Offset: 0x00023260
		private static bool IsSeparatorChar(char c)
		{
			if (c <= ',')
			{
				if (c <= ' ')
				{
					if (c != '\t' && c != ' ')
					{
						return false;
					}
				}
				else if (c != '"')
				{
					switch (c)
					{
					case '(':
					case ')':
					case ',':
						break;
					case '*':
					case '+':
						return false;
					default:
						return false;
					}
				}
			}
			else if (c <= '@')
			{
				if (c != '/')
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
						break;
					default:
						return false;
					}
				}
			}
			else
			{
				switch (c)
				{
				case '[':
				case '\\':
				case ']':
					break;
				default:
					if (c != '{' && c != '}')
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000250FC File Offset: 0x000232FC
		public static List<string> ParseHeader(string headerValue)
		{
			if (headerValue == null)
			{
				return null;
			}
			List<string> list = new List<string>();
			string[] array = headerValue.Split(SubProtocolUtil._splitChars);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim(SubProtocolUtil._lwsTrimChars);
				if (text.Length != 0)
				{
					if (!SubProtocolUtil.IsValidSubProtocolName(text))
					{
						return null;
					}
					list.Add(text);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Distinct<string>(StringComparer.Ordinal).Count<string>() != list.Count)
			{
				return null;
			}
			return list;
		}

		// Token: 0x04001170 RID: 4464
		private static readonly char[] _lwsTrimChars = new char[] { ' ', '\t' };

		// Token: 0x04001171 RID: 4465
		private static readonly char[] _splitChars = new char[] { ',' };
	}
}
