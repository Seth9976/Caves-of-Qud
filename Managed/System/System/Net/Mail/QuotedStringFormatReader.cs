using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020007F2 RID: 2034
	internal static class QuotedStringFormatReader
	{
		// Token: 0x06004102 RID: 16642 RVA: 0x000DEB74 File Offset: 0x000DCD74
		internal static int ReadReverseQuoted(string data, int index, bool permitUnicode)
		{
			index--;
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					goto IL_006C;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, permitUnicode);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (data[index] == MailBnfHelper.Quote)
					{
						break;
					}
					if (!QuotedStringFormatReader.IsValidQtext(permitUnicode, data[index]))
					{
						goto Block_4;
					}
					index--;
				}
				if (index < 0)
				{
					goto IL_006C;
				}
			}
			return index - 1;
			Block_4:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			IL_006C:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", MailBnfHelper.Quote));
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x000DEC08 File Offset: 0x000DCE08
		internal static int ReadReverseUnQuoted(string data, int index, bool permitUnicode, bool expectCommaDelimiter)
		{
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					return index;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, permitUnicode);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (expectCommaDelimiter && data[index] == MailBnfHelper.Comma)
					{
						return index;
					}
					if (!QuotedStringFormatReader.IsValidQtext(permitUnicode, data[index]))
					{
						break;
					}
					index--;
				}
				if (index < 0)
				{
					return index;
				}
			}
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x000DEC7C File Offset: 0x000DCE7C
		private static bool IsValidQtext(bool allowUnicode, char ch)
		{
			if ((int)ch > MailBnfHelper.Ascii7bitMaxValue)
			{
				return allowUnicode;
			}
			return MailBnfHelper.Qtext[(int)ch];
		}
	}
}
