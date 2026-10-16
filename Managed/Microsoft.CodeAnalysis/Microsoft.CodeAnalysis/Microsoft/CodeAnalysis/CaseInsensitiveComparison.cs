using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200010E RID: 270
	[NullableContext(1)]
	[Nullable(0)]
	public static class CaseInsensitiveComparison
	{
		// Token: 0x06000965 RID: 2405 RVA: 0x0001AE08 File Offset: 0x00019008
		private static CultureInfo GetUnicodeCulture()
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = new CultureInfo("en");
			}
			catch (ArgumentException)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			return cultureInfo;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001AE3C File Offset: 0x0001903C
		public static char ToLower(char c)
		{
			if (c - 'A' <= '\u0019')
			{
				return c | ' ';
			}
			if (c < 'À')
			{
				return c;
			}
			return CaseInsensitiveComparison.ToLowerNonAscii(c);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001AE5C File Offset: 0x0001905C
		private static char ToLowerNonAscii(char c)
		{
			if (c == 'İ')
			{
				return 'i';
			}
			return CaseInsensitiveComparison.s_unicodeCultureTextInfo.ToLower(c);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001AE74 File Offset: 0x00019074
		public static StringComparer Comparer
		{
			get
			{
				return CaseInsensitiveComparison.s_comparer;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001AE7B File Offset: 0x0001907B
		public static bool Equals(string left, string right)
		{
			return CaseInsensitiveComparison.s_comparer.Equals(left, right);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001AE89 File Offset: 0x00019089
		[NullableContext(0)]
		public static bool Equals(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
		{
			return CaseInsensitiveComparison.s_comparer.Equals(left, right);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001AE97 File Offset: 0x00019097
		public static bool EndsWith(string value, string possibleEnd)
		{
			return CaseInsensitiveComparison.OneToOneUnicodeComparer.EndsWith(value, possibleEnd);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0001AEA0 File Offset: 0x000190A0
		public static bool StartsWith(string value, string possibleStart)
		{
			return CaseInsensitiveComparison.OneToOneUnicodeComparer.StartsWith(value, possibleStart);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0001AEA9 File Offset: 0x000190A9
		public static int Compare(string left, string right)
		{
			return CaseInsensitiveComparison.s_comparer.Compare(left, right);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001AEB7 File Offset: 0x000190B7
		[NullableContext(0)]
		public static int Compare(ReadOnlySpan<char> left, ReadOnlySpan<char> right)
		{
			return CaseInsensitiveComparison.s_comparer.Compare(left, right);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001AEC5 File Offset: 0x000190C5
		public static int GetHashCode(string value)
		{
			return CaseInsensitiveComparison.s_comparer.GetHashCode(value);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001AED2 File Offset: 0x000190D2
		[NullableContext(2)]
		[return: NotNullIfNotNull("value")]
		public static string ToLower(string value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return value;
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append(value);
			CaseInsensitiveComparison.ToLower(builder);
			return instance.ToStringAndFree();
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001AF00 File Offset: 0x00019100
		public static void ToLower(StringBuilder builder)
		{
			if (builder == null)
			{
				return;
			}
			for (int i = 0; i < builder.Length; i++)
			{
				builder.set_Chars(i, CaseInsensitiveComparison.ToLower(builder.get_Chars(i)));
			}
		}

		// Token: 0x040002A2 RID: 674
		private static readonly TextInfo s_unicodeCultureTextInfo = CaseInsensitiveComparison.GetUnicodeCulture().TextInfo;

		// Token: 0x040002A3 RID: 675
		private static readonly CaseInsensitiveComparison.OneToOneUnicodeComparer s_comparer = new CaseInsensitiveComparison.OneToOneUnicodeComparer();

		// Token: 0x0200053B RID: 1339
		[NullableContext(0)]
		private sealed class OneToOneUnicodeComparer : StringComparer
		{
			// Token: 0x060033A3 RID: 13219 RVA: 0x00091E2C File Offset: 0x0009002C
			private static int CompareLowerUnicode(char c1, char c2)
			{
				if (c1 != c2)
				{
					return (int)(CaseInsensitiveComparison.ToLower(c1) - CaseInsensitiveComparison.ToLower(c2));
				}
				return 0;
			}

			// Token: 0x060033A4 RID: 13220 RVA: 0x00091E44 File Offset: 0x00090044
			[NullableContext(2)]
			public override int Compare(string str1, string str2)
			{
				if (str1 == str2)
				{
					return 0;
				}
				if (str1 == null)
				{
					return -1;
				}
				if (str2 == null)
				{
					return 1;
				}
				int num = Math.Min(str1.Length, str2.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = CaseInsensitiveComparison.OneToOneUnicodeComparer.CompareLowerUnicode(str1.get_Chars(i), str2.get_Chars(i));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return str1.Length - str2.Length;
			}

			// Token: 0x060033A5 RID: 13221 RVA: 0x00091EA8 File Offset: 0x000900A8
			public unsafe int Compare(ReadOnlySpan<char> str1, ReadOnlySpan<char> str2)
			{
				int num = Math.Min(str1.Length, str2.Length);
				for (int i = 0; i < num; i++)
				{
					int num2 = CaseInsensitiveComparison.OneToOneUnicodeComparer.CompareLowerUnicode((char)(*str1[i]), (char)(*str2[i]));
					if (num2 != 0)
					{
						return num2;
					}
				}
				return str1.Length - str2.Length;
			}

			// Token: 0x060033A6 RID: 13222 RVA: 0x00091F01 File Offset: 0x00090101
			private static bool AreEqualLowerUnicode(char c1, char c2)
			{
				return c1 == c2 || CaseInsensitiveComparison.ToLower(c1) == CaseInsensitiveComparison.ToLower(c2);
			}

			// Token: 0x060033A7 RID: 13223 RVA: 0x00091F18 File Offset: 0x00090118
			[NullableContext(2)]
			public override bool Equals(string str1, string str2)
			{
				if (str1 == str2)
				{
					return true;
				}
				if (str1 == null || str2 == null)
				{
					return false;
				}
				if (str1.Length != str2.Length)
				{
					return false;
				}
				for (int i = 0; i < str1.Length; i++)
				{
					if (!CaseInsensitiveComparison.OneToOneUnicodeComparer.AreEqualLowerUnicode(str1.get_Chars(i), str2.get_Chars(i)))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060033A8 RID: 13224 RVA: 0x00091F6C File Offset: 0x0009016C
			public unsafe bool Equals(ReadOnlySpan<char> str1, ReadOnlySpan<char> str2)
			{
				if (str1.Length != str2.Length)
				{
					return false;
				}
				for (int i = 0; i < str1.Length; i++)
				{
					if (!CaseInsensitiveComparison.OneToOneUnicodeComparer.AreEqualLowerUnicode((char)(*str1[i]), (char)(*str2[i])))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060033A9 RID: 13225 RVA: 0x00091FBC File Offset: 0x000901BC
			[NullableContext(1)]
			public static bool EndsWith(string value, string possibleEnd)
			{
				if (value == possibleEnd)
				{
					return true;
				}
				if (value == null || possibleEnd == null)
				{
					return false;
				}
				int num = value.Length - 1;
				int i = possibleEnd.Length - 1;
				if (num < i)
				{
					return false;
				}
				while (i >= 0)
				{
					if (!CaseInsensitiveComparison.OneToOneUnicodeComparer.AreEqualLowerUnicode(value.get_Chars(num), possibleEnd.get_Chars(i)))
					{
						return false;
					}
					num--;
					i--;
				}
				return true;
			}

			// Token: 0x060033AA RID: 13226 RVA: 0x00092014 File Offset: 0x00090214
			[NullableContext(1)]
			public static bool StartsWith(string value, string possibleStart)
			{
				if (value == possibleStart)
				{
					return true;
				}
				if (value == null || possibleStart == null)
				{
					return false;
				}
				if (value.Length < possibleStart.Length)
				{
					return false;
				}
				for (int i = 0; i < possibleStart.Length; i++)
				{
					if (!CaseInsensitiveComparison.OneToOneUnicodeComparer.AreEqualLowerUnicode(value.get_Chars(i), possibleStart.get_Chars(i)))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060033AB RID: 13227 RVA: 0x00092068 File Offset: 0x00090268
			[NullableContext(1)]
			public override int GetHashCode(string str)
			{
				int num = -2128831035;
				for (int i = 0; i < str.Length; i++)
				{
					num = Hash.CombineFNVHash(num, CaseInsensitiveComparison.ToLower(str.get_Chars(i)));
				}
				return num;
			}
		}
	}
}
