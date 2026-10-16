using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020007EE RID: 2030
	internal static class DomainLiteralReader
	{
		// Token: 0x060040F6 RID: 16630 RVA: 0x000DE64C File Offset: 0x000DC84C
		internal static int ReadReverse(string data, int index)
		{
			index--;
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					goto IL_007A;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, false);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (data[index] == MailBnfHelper.StartSquareBracket)
					{
						break;
					}
					if ((int)data[index] > MailBnfHelper.Ascii7bitMaxValue || !MailBnfHelper.Dtext[(int)data[index]])
					{
						goto IL_0055;
					}
					index--;
				}
				if (index < 0)
				{
					goto IL_007A;
				}
			}
			return index - 1;
			IL_0055:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			IL_007A:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", MailBnfHelper.EndSquareBracket));
		}
	}
}
