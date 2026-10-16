using System;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007CD RID: 1997
	internal static class MailBnfHelper
	{
		// Token: 0x06003FDF RID: 16351 RVA: 0x000D9B80 File Offset: 0x000D7D80
		private static bool[] CreateCharactersAllowedInAtoms()
		{
			bool[] array = new bool[128];
			for (int i = 48; i <= 57; i++)
			{
				array[i] = true;
			}
			for (int j = 65; j <= 90; j++)
			{
				array[j] = true;
			}
			for (int k = 97; k <= 122; k++)
			{
				array[k] = true;
			}
			array[33] = true;
			array[35] = true;
			array[36] = true;
			array[37] = true;
			array[38] = true;
			array[39] = true;
			array[42] = true;
			array[43] = true;
			array[45] = true;
			array[47] = true;
			array[61] = true;
			array[63] = true;
			array[94] = true;
			array[95] = true;
			array[96] = true;
			array[123] = true;
			array[124] = true;
			array[125] = true;
			array[126] = true;
			return array;
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000D9C30 File Offset: 0x000D7E30
		private static bool[] CreateCharactersAllowedInQuotedStrings()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 9; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 33; j++)
			{
				array[j] = true;
			}
			for (int k = 35; k <= 91; k++)
			{
				array[k] = true;
			}
			for (int l = 93; l <= 127; l++)
			{
				array[l] = true;
			}
			return array;
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000D9CA0 File Offset: 0x000D7EA0
		private static bool[] CreateCharactersAllowedInDomainLiterals()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 8; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 31; j++)
			{
				array[j] = true;
			}
			for (int k = 33; k <= 90; k++)
			{
				array[k] = true;
			}
			for (int l = 94; l <= 127; l++)
			{
				array[l] = true;
			}
			return array;
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000D9D10 File Offset: 0x000D7F10
		private static bool[] CreateCharactersAllowedInHeaderNames()
		{
			bool[] array = new bool[128];
			for (int i = 33; i <= 57; i++)
			{
				array[i] = true;
			}
			for (int j = 59; j <= 126; j++)
			{
				array[j] = true;
			}
			return array;
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000D9D50 File Offset: 0x000D7F50
		private static bool[] CreateCharactersAllowedInTokens()
		{
			bool[] array = new bool[128];
			for (int i = 33; i <= 126; i++)
			{
				array[i] = true;
			}
			array[40] = false;
			array[41] = false;
			array[60] = false;
			array[62] = false;
			array[64] = false;
			array[44] = false;
			array[59] = false;
			array[58] = false;
			array[92] = false;
			array[34] = false;
			array[47] = false;
			array[91] = false;
			array[93] = false;
			array[63] = false;
			array[61] = false;
			return array;
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000D9DC8 File Offset: 0x000D7FC8
		private static bool[] CreateCharactersAllowedInComments()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 8; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 31; j++)
			{
				array[j] = true;
			}
			for (int k = 33; k <= 39; k++)
			{
				array[k] = true;
			}
			for (int l = 42; l <= 91; l++)
			{
				array[l] = true;
			}
			for (int m = 93; m <= 127; m++)
			{
				array[m] = true;
			}
			return array;
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000D9E50 File Offset: 0x000D8050
		internal static bool SkipCFWS(string data, ref int offset)
		{
			int num = 0;
			while (offset < data.Length)
			{
				if (data[offset] > '\u007f')
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				if (data[offset] == '\\' && num > 0)
				{
					offset += 2;
				}
				else if (data[offset] == '(')
				{
					num++;
				}
				else if (data[offset] == ')')
				{
					num--;
				}
				else if (data[offset] != ' ' && data[offset] != '\t' && num == 0)
				{
					return true;
				}
				if (num < 0)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				offset++;
			}
			return false;
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000D9F1C File Offset: 0x000D811C
		internal static void ValidateHeaderName(string data)
		{
			int i;
			for (i = 0; i < data.Length; i++)
			{
				if ((int)data[i] > MailBnfHelper.Ftext.Length || !MailBnfHelper.Ftext[(int)data[i]])
				{
					throw new FormatException("An invalid character was found in header name.");
				}
			}
			if (i == 0)
			{
				throw new FormatException("An invalid character was found in header name.");
			}
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000D9F72 File Offset: 0x000D8172
		internal static string ReadQuotedString(string data, ref int offset, StringBuilder builder)
		{
			return MailBnfHelper.ReadQuotedString(data, ref offset, builder, false, false);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000D9F80 File Offset: 0x000D8180
		internal static string ReadQuotedString(string data, ref int offset, StringBuilder builder, bool doesntRequireQuotes, bool permitUnicodeInDisplayName)
		{
			if (!doesntRequireQuotes)
			{
				offset++;
			}
			int num = offset;
			StringBuilder stringBuilder = ((builder != null) ? builder : new StringBuilder());
			while (offset < data.Length)
			{
				if (data[offset] == '\\')
				{
					stringBuilder.Append(data, num, offset - num);
					int num2 = offset + 1;
					offset = num2;
					num = num2;
				}
				else if (data[offset] == '"')
				{
					stringBuilder.Append(data, num, offset - num);
					offset++;
					if (builder == null)
					{
						return stringBuilder.ToString();
					}
					return null;
				}
				else if (data[offset] == '=' && data.Length > offset + 3 && data[offset + 1] == '\r' && data[offset + 2] == '\n' && (data[offset + 3] == ' ' || data[offset + 3] == '\t'))
				{
					offset += 3;
				}
				else if (permitUnicodeInDisplayName)
				{
					if ((int)data[offset] <= MailBnfHelper.Ascii7bitMaxValue && !MailBnfHelper.Qtext[(int)data[offset]])
					{
						throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
					}
				}
				else if ((int)data[offset] > MailBnfHelper.Ascii7bitMaxValue || !MailBnfHelper.Qtext[(int)data[offset]])
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				offset++;
			}
			if (!doesntRequireQuotes)
			{
				throw new FormatException("The mail header is malformed.");
			}
			stringBuilder.Append(data, num, offset - num);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000DA10A File Offset: 0x000D830A
		internal static string ReadParameterAttribute(string data, ref int offset, StringBuilder builder)
		{
			if (!MailBnfHelper.SkipCFWS(data, ref offset))
			{
				return null;
			}
			return MailBnfHelper.ReadToken(data, ref offset, null);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000DA120 File Offset: 0x000D8320
		internal static string ReadToken(string data, ref int offset, StringBuilder builder)
		{
			int num = offset;
			while (offset < data.Length)
			{
				if ((int)data[offset] > MailBnfHelper.Ascii7bitMaxValue)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				if (!MailBnfHelper.Ttext[(int)data[offset]])
				{
					break;
				}
				offset++;
			}
			if (num == offset)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
			}
			return data.Substring(num, offset - num);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000DA1AC File Offset: 0x000D83AC
		internal static string GetDateTimeString(DateTime value, StringBuilder builder)
		{
			StringBuilder stringBuilder = ((builder != null) ? builder : new StringBuilder());
			stringBuilder.Append(value.Day);
			stringBuilder.Append(' ');
			stringBuilder.Append(MailBnfHelper.s_months[value.Month]);
			stringBuilder.Append(' ');
			stringBuilder.Append(value.Year);
			stringBuilder.Append(' ');
			if (value.Hour <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Hour);
			stringBuilder.Append(':');
			if (value.Minute <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Minute);
			stringBuilder.Append(':');
			if (value.Second <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Second);
			string text = TimeZoneInfo.Local.GetUtcOffset(value).ToString();
			if (text[0] != '-')
			{
				stringBuilder.Append(" +");
			}
			else
			{
				stringBuilder.Append(' ');
			}
			string[] array = text.Split(MailBnfHelper.s_colonSeparator);
			stringBuilder.Append(array[0]);
			stringBuilder.Append(array[1]);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000DA2F0 File Offset: 0x000D84F0
		internal static void GetTokenOrQuotedString(string data, StringBuilder builder, bool allowUnicode)
		{
			int i = 0;
			int num = 0;
			while (i < data.Length)
			{
				if (!MailBnfHelper.CheckForUnicode(data[i], allowUnicode) && (!MailBnfHelper.Ttext[(int)data[i]] || data[i] == ' '))
				{
					builder.Append('"');
					while (i < data.Length)
					{
						if (!MailBnfHelper.CheckForUnicode(data[i], allowUnicode))
						{
							if (MailBnfHelper.IsFWSAt(data, i))
							{
								i += 2;
							}
							else if (!MailBnfHelper.Qtext[(int)data[i]])
							{
								builder.Append(data, num, i - num);
								builder.Append('\\');
								num = i;
							}
						}
						i++;
					}
					builder.Append(data, num, i - num);
					builder.Append('"');
					return;
				}
				i++;
			}
			if (data.Length == 0)
			{
				builder.Append("\"\"");
			}
			builder.Append(data);
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000DA3D0 File Offset: 0x000D85D0
		private static bool CheckForUnicode(char ch, bool allowUnicode)
		{
			if ((int)ch < MailBnfHelper.Ascii7bitMaxValue)
			{
				return false;
			}
			if (!allowUnicode)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", ch));
			}
			return true;
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000DA3F6 File Offset: 0x000D85F6
		internal static bool IsAllowedWhiteSpace(char c)
		{
			return c == MailBnfHelper.Tab || c == MailBnfHelper.Space || c == MailBnfHelper.CR || c == MailBnfHelper.LF;
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000DA41C File Offset: 0x000D861C
		internal static bool HasCROrLF(string data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] == '\r' || data[i] == '\n')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000DA454 File Offset: 0x000D8654
		internal static bool IsFWSAt(string data, int index)
		{
			return data[index] == MailBnfHelper.CR && index + 2 < data.Length && data[index + 1] == MailBnfHelper.LF && (data[index + 2] == MailBnfHelper.Space || data[index + 2] == MailBnfHelper.Tab);
		}

		// Token: 0x04002659 RID: 9817
		internal static readonly bool[] Atext = MailBnfHelper.CreateCharactersAllowedInAtoms();

		// Token: 0x0400265A RID: 9818
		internal static readonly bool[] Qtext = MailBnfHelper.CreateCharactersAllowedInQuotedStrings();

		// Token: 0x0400265B RID: 9819
		internal static readonly bool[] Dtext = MailBnfHelper.CreateCharactersAllowedInDomainLiterals();

		// Token: 0x0400265C RID: 9820
		internal static readonly bool[] Ftext = MailBnfHelper.CreateCharactersAllowedInHeaderNames();

		// Token: 0x0400265D RID: 9821
		internal static readonly bool[] Ttext = MailBnfHelper.CreateCharactersAllowedInTokens();

		// Token: 0x0400265E RID: 9822
		internal static readonly bool[] Ctext = MailBnfHelper.CreateCharactersAllowedInComments();

		// Token: 0x0400265F RID: 9823
		internal static readonly int Ascii7bitMaxValue = 127;

		// Token: 0x04002660 RID: 9824
		internal static readonly char Quote = '"';

		// Token: 0x04002661 RID: 9825
		internal static readonly char Space = ' ';

		// Token: 0x04002662 RID: 9826
		internal static readonly char Tab = '\t';

		// Token: 0x04002663 RID: 9827
		internal static readonly char CR = '\r';

		// Token: 0x04002664 RID: 9828
		internal static readonly char LF = '\n';

		// Token: 0x04002665 RID: 9829
		internal static readonly char StartComment = '(';

		// Token: 0x04002666 RID: 9830
		internal static readonly char EndComment = ')';

		// Token: 0x04002667 RID: 9831
		internal static readonly char Backslash = '\\';

		// Token: 0x04002668 RID: 9832
		internal static readonly char At = '@';

		// Token: 0x04002669 RID: 9833
		internal static readonly char EndAngleBracket = '>';

		// Token: 0x0400266A RID: 9834
		internal static readonly char StartAngleBracket = '<';

		// Token: 0x0400266B RID: 9835
		internal static readonly char StartSquareBracket = '[';

		// Token: 0x0400266C RID: 9836
		internal static readonly char EndSquareBracket = ']';

		// Token: 0x0400266D RID: 9837
		internal static readonly char Comma = ',';

		// Token: 0x0400266E RID: 9838
		internal static readonly char Dot = '.';

		// Token: 0x0400266F RID: 9839
		private static readonly char[] s_colonSeparator = new char[] { ':' };

		// Token: 0x04002670 RID: 9840
		private static string[] s_months = new string[]
		{
			null, "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep",
			"Oct", "Nov", "Dec"
		};
	}
}
