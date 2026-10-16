using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020007F1 RID: 2033
	internal static class QuotedPairReader
	{
		// Token: 0x06004100 RID: 16640 RVA: 0x000DEAE4 File Offset: 0x000DCCE4
		internal static int CountQuotedChars(string data, int index, bool permitUnicodeEscaping)
		{
			if (index <= 0 || data[index - 1] != MailBnfHelper.Backslash)
			{
				return 0;
			}
			int num = QuotedPairReader.CountBackslashes(data, index - 1);
			if (num % 2 == 0)
			{
				return 0;
			}
			if (!permitUnicodeEscaping && (int)data[index] > MailBnfHelper.Ascii7bitMaxValue)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			}
			return num + 1;
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000DEB48 File Offset: 0x000DCD48
		private static int CountBackslashes(string data, int index)
		{
			int num = 0;
			do
			{
				num++;
				index--;
			}
			while (index >= 0 && data[index] == MailBnfHelper.Backslash);
			return num;
		}
	}
}
