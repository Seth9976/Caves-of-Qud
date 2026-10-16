using System;
using System.Text;

namespace System.Web.Util
{
	// Token: 0x02000146 RID: 326
	internal sealed class StrUtils
	{
		// Token: 0x06000EA9 RID: 3753 RVA: 0x00002050 File Offset: 0x00000250
		private StrUtils()
		{
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00027CE8 File Offset: 0x00025EE8
		public static bool StartsWith(string str1, string str2)
		{
			return StrUtils.StartsWith(str1, str2, false);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00027CF4 File Offset: 0x00025EF4
		public static bool StartsWith(string str1, string str2, bool ignore_case)
		{
			int length = str2.Length;
			if (length == 0)
			{
				return true;
			}
			int length2 = str1.Length;
			return length <= length2 && string.Compare(str1, 0, str2, 0, length, ignore_case, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00027D2D File Offset: 0x00025F2D
		public static bool EndsWith(string str1, string str2)
		{
			return StrUtils.EndsWith(str1, str2, false);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00027D38 File Offset: 0x00025F38
		public static bool EndsWith(string str1, string str2, bool ignore_case)
		{
			int length = str2.Length;
			if (length == 0)
			{
				return true;
			}
			int length2 = str1.Length;
			return length <= length2 && string.Compare(str1, length2 - length, str2, 0, length, ignore_case, Helpers.InvariantCulture) == 0;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00027D74 File Offset: 0x00025F74
		public static string EscapeQuotesAndBackslashes(string attributeValue)
		{
			StringBuilder stringBuilder = null;
			for (int i = 0; i < attributeValue.Length; i++)
			{
				char c = attributeValue[i];
				if (c == '\'' || c == '"' || c == '\\')
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(attributeValue.Substring(0, i));
					}
					stringBuilder.Append('\\');
					stringBuilder.Append(c);
				}
				else if (stringBuilder != null)
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return attributeValue;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00027DEC File Offset: 0x00025FEC
		public static bool IsNullOrEmpty(string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00027DF4 File Offset: 0x00025FF4
		public static string[] SplitRemoveEmptyEntries(string value, char[] separator)
		{
			return value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
