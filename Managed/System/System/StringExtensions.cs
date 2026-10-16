using System;

namespace System
{
	// Token: 0x02000148 RID: 328
	internal static class StringExtensions
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x0001FE56 File Offset: 0x0001E056
		internal static string SubstringTrim(this string value, int startIndex)
		{
			return value.SubstringTrim(startIndex, value.Length - startIndex);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001FE68 File Offset: 0x0001E068
		internal static string SubstringTrim(this string value, int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = startIndex + length - 1;
			while (startIndex <= num)
			{
				if (!char.IsWhiteSpace(value[startIndex]))
				{
					break;
				}
				startIndex++;
			}
			while (num >= startIndex && char.IsWhiteSpace(value[num]))
			{
				num--;
			}
			int num2 = num - startIndex + 1;
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (num2 != value.Length)
			{
				return value.Substring(startIndex, num2);
			}
			return value;
		}
	}
}
