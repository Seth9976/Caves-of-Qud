using System;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002E0 RID: 736
	public static class StringExtensions
	{
		// Token: 0x06001957 RID: 6487 RVA: 0x000810BC File Offset: 0x0007F2BC
		public static string After(this string value, char a, bool last = true)
		{
			int num = (last ? value.LastIndexOf(a) : value.IndexOf(a));
			return value.Substring(num + 1);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000810E8 File Offset: 0x0007F2E8
		public static string After(this string value, string a, bool last = true)
		{
			int num = (last ? value.LastIndexOf(a) : value.IndexOf(a));
			return value.Substring(num + 1);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00081114 File Offset: 0x0007F314
		public static string Before(this string value, char a, bool last = true)
		{
			int num = (last ? value.LastIndexOf(a) : value.IndexOf(a));
			if (num != -1)
			{
				return value.Remove(num);
			}
			return string.Empty;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00081148 File Offset: 0x0007F348
		public static string Before(this string value, string a, bool last = true)
		{
			int num = (last ? value.LastIndexOf(a) : value.IndexOf(a));
			if (num != -1)
			{
				return value.Remove(num);
			}
			return string.Empty;
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0008117A File Offset: 0x0007F37A
		public static string TrimEnd(this string source, string value)
		{
			if (!source.EndsWith(value))
			{
				return source;
			}
			return source.Remove(source.LastIndexOf(value));
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00081194 File Offset: 0x0007F394
		public static string LowercaseLetter(this string s, int index = 0)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToCharArray();
			array[index] = char.ToLower(array[index]);
			return new string(array);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000811C8 File Offset: 0x0007F3C8
		public static string UppercaseLetter(this string s, int index = 0)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToCharArray();
			array[index] = char.ToUpper(array[index]);
			return new string(array);
		}
	}
}
