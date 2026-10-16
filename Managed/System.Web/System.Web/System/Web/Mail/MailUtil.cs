using System;
using System.Text;

namespace System.Web.Mail
{
	// Token: 0x020000F8 RID: 248
	internal class MailUtil
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x000232F8 File Offset: 0x000214F8
		public static bool NeedEncoding(string str)
		{
			foreach (int num in str)
			{
				if ((num <= 61 || num >= 127) && (num <= 31 || num >= 61))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00023337 File Offset: 0x00021537
		public static string Base64Encode(string str)
		{
			return Convert.ToBase64String(Encoding.Default.GetBytes(str));
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0002334C File Offset: 0x0002154C
		public static string GenerateBoundary()
		{
			StringBuilder stringBuilder = new StringBuilder("__MONO__Boundary");
			stringBuilder.Append("__");
			DateTime now = DateTime.Now;
			stringBuilder.Append(now.Year);
			stringBuilder.Append(now.Month);
			stringBuilder.Append(now.Day);
			stringBuilder.Append(now.Hour);
			stringBuilder.Append(now.Minute);
			stringBuilder.Append(now.Second);
			stringBuilder.Append(now.Millisecond);
			stringBuilder.Append("__");
			stringBuilder.Append(new Random().Next());
			stringBuilder.Append("__");
			return stringBuilder.ToString();
		}
	}
}
