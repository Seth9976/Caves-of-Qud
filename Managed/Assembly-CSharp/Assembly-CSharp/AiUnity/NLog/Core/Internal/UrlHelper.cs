using System;
using System.Text;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000272 RID: 626
	internal class UrlHelper
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x00078DEC File Offset: 0x00076FEC
		internal static string UrlEncode(string str, bool spaceAsPlus)
		{
			StringBuilder stringBuilder = new StringBuilder(str.Length + 20);
			foreach (char c in str)
			{
				if (c == ' ' && spaceAsPlus)
				{
					stringBuilder.Append('+');
				}
				else if (UrlHelper.IsSafeUrlCharacter(c))
				{
					stringBuilder.Append(c);
				}
				else if (c < 'Ā')
				{
					stringBuilder.Append('%');
					stringBuilder.Append(UrlHelper.hexChars[(int)((c >> 4) & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)(c & '\u000f')]);
				}
				else
				{
					stringBuilder.Append('%');
					stringBuilder.Append('u');
					stringBuilder.Append(UrlHelper.hexChars[(int)((c >> 12) & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)((c >> 8) & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)((c >> 4) & '\u000f')]);
					stringBuilder.Append(UrlHelper.hexChars[(int)(c & '\u000f')]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00078F05 File Offset: 0x00077105
		private static bool IsSafeUrlCharacter(char ch)
		{
			return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9') || UrlHelper.safeUrlPunctuation.IndexOf(ch) >= 0;
		}

		// Token: 0x04000DE0 RID: 3552
		private static string safeUrlPunctuation = ".()*-_!'";

		// Token: 0x04000DE1 RID: 3553
		private static string hexChars = "0123456789abcdef";
	}
}
