using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000063 RID: 99
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StringExtensions
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00007380 File Offset: 0x00005580
		internal static string GetNumeral(int number)
		{
			ImmutableArray<string> immutableArray = StringExtensions.s_lazyNumerals;
			if (immutableArray.IsDefault)
			{
				immutableArray = ImmutableArray.Create<string>(new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
				ImmutableInterlocked.InterlockedInitialize<string>(ref StringExtensions.s_lazyNumerals, immutableArray);
			}
			if (number >= immutableArray.Length)
			{
				return number.ToString();
			}
			return immutableArray[number];
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007420 File Offset: 0x00005620
		public static string Join([Nullable(new byte[] { 1, 2 })] this IEnumerable<string> source, string separator)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (separator == null)
			{
				throw new ArgumentNullException("separator");
			}
			return string.Join(separator, source);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007445 File Offset: 0x00005645
		public static bool LooksLikeInterfaceName(this string name)
		{
			return name.Length >= 3 && name.get_Chars(0) == 'I' && char.IsUpper(name.get_Chars(1)) && char.IsLower(name.get_Chars(2));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007477 File Offset: 0x00005677
		public static bool LooksLikeTypeParameterName(this string name)
		{
			return name.Length >= 3 && name.get_Chars(0) == 'T' && char.IsUpper(name.get_Chars(1)) && char.IsLower(name.get_Chars(2));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000074A9 File Offset: 0x000056A9
		[NullableContext(2)]
		[return: NotNullIfNotNull("shortName")]
		public static string ToPascalCase(this string shortName, bool trimLeadingTypePrefix = true)
		{
			return shortName.ConvertCase(trimLeadingTypePrefix, StringExtensions.s_toUpper);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000074B7 File Offset: 0x000056B7
		[NullableContext(2)]
		[return: NotNullIfNotNull("shortName")]
		public static string ToCamelCase(this string shortName, bool trimLeadingTypePrefix = true)
		{
			return shortName.ConvertCase(trimLeadingTypePrefix, StringExtensions.s_toLower);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000074C8 File Offset: 0x000056C8
		[NullableContext(2)]
		[return: NotNullIfNotNull("shortName")]
		private static string ConvertCase(this string shortName, bool trimLeadingTypePrefix, [Nullable(1)] Func<char, char> convert)
		{
			if (!RoslynString.IsNullOrEmpty(shortName))
			{
				if (trimLeadingTypePrefix && (shortName.LooksLikeInterfaceName() || shortName.LooksLikeTypeParameterName()))
				{
					return convert.Invoke(shortName.get_Chars(1)).ToString() + shortName.Substring(2);
				}
				if (convert.Invoke(shortName.get_Chars(0)) != shortName.get_Chars(0))
				{
					return convert.Invoke(shortName.get_Chars(0)).ToString() + shortName.Substring(1);
				}
			}
			return shortName;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000754B File Offset: 0x0000574B
		[NullableContext(2)]
		internal static bool IsValidClrTypeName([NotNullWhen(true)] this string name)
		{
			return !RoslynString.IsNullOrEmpty(name) && name.IndexOf('\0') == -1;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007564 File Offset: 0x00005764
		[NullableContext(2)]
		internal static bool IsValidClrNamespaceName([NotNullWhen(true)] this string name)
		{
			if (RoslynString.IsNullOrEmpty(name))
			{
				return false;
			}
			char c = '.';
			for (int i = 0; i < name.Length; i++)
			{
				char c2 = name.get_Chars(i);
				if (c2 == '\0' || (c2 == '.' && c == '.'))
				{
					return false;
				}
				c = c2;
			}
			return c != '.';
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000075B4 File Offset: 0x000057B4
		internal static string GetWithSingleAttributeSuffix(this string name, bool isCaseSensitive)
		{
			string text = name;
			while ((text = text.GetWithoutAttributeSuffix(isCaseSensitive)) != null)
			{
				name = text;
			}
			return name + "Attribute";
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000075DE File Offset: 0x000057DE
		internal static bool TryGetWithoutAttributeSuffix(this string name, [Nullable(2)] [NotNullWhen(true)] out string result)
		{
			return name.TryGetWithoutAttributeSuffix(true, out result);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000075E8 File Offset: 0x000057E8
		[return: Nullable(2)]
		internal static string GetWithoutAttributeSuffix(this string name, bool isCaseSensitive)
		{
			string text;
			if (!name.TryGetWithoutAttributeSuffix(isCaseSensitive, out text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007603 File Offset: 0x00005803
		internal static bool TryGetWithoutAttributeSuffix(this string name, bool isCaseSensitive, [Nullable(2)] [NotNullWhen(true)] out string result)
		{
			if (name.HasAttributeSuffix(isCaseSensitive))
			{
				result = name.Substring(0, name.Length - "Attribute".Length);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007630 File Offset: 0x00005830
		internal static bool HasAttributeSuffix(this string name, bool isCaseSensitive)
		{
			StringComparison stringComparison = (isCaseSensitive ? 4 : 5);
			return name.Length > "Attribute".Length && name.EndsWith("Attribute", stringComparison);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007668 File Offset: 0x00005868
		internal static bool IsValidUnicodeString(this string str)
		{
			int i = 0;
			while (i < str.Length)
			{
				char c = str.get_Chars(i++);
				if (char.IsHighSurrogate(c))
				{
					if (i >= str.Length || !char.IsLowSurrogate(str.get_Chars(i)))
					{
						return false;
					}
					i++;
				}
				else if (char.IsLowSurrogate(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000076C0 File Offset: 0x000058C0
		internal static string Unquote(this string arg)
		{
			bool flag;
			return arg.Unquote(out flag);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000076D5 File Offset: 0x000058D5
		internal static string Unquote(this string arg, out bool quoted)
		{
			if (arg.Length > 1 && arg.get_Chars(0) == '"' && arg.get_Chars(arg.Length - 1) == '"')
			{
				quoted = true;
				return arg.Substring(1, arg.Length - 2);
			}
			quoted = false;
			return arg;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007714 File Offset: 0x00005914
		internal static int IndexOfBalancedParenthesis(this string str, int openingOffset, char closing)
		{
			char c = str.get_Chars(openingOffset);
			int num = 1;
			for (int i = openingOffset + 1; i < str.Length; i++)
			{
				char c2 = str.get_Chars(i);
				if (c2 == c)
				{
					num++;
				}
				else if (c2 == closing)
				{
					num--;
					if (num == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000775E File Offset: 0x0000595E
		internal static char First(this string arg)
		{
			return arg.get_Chars(0);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007767 File Offset: 0x00005967
		internal static char Last(this string arg)
		{
			return arg.get_Chars(arg.Length - 1);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007778 File Offset: 0x00005978
		internal static bool All(this string arg, Predicate<char> predicate)
		{
			for (int i = 0; i < arg.Length; i++)
			{
				char c = arg.get_Chars(i);
				if (!predicate.Invoke(c))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000077AC File Offset: 0x000059AC
		public static int GetCaseInsensitivePrefixLength(this string string1, string string2)
		{
			int num = 0;
			while (num < string1.Length && num < string2.Length && char.ToUpper(string1.get_Chars(num)) == char.ToUpper(string2.get_Chars(num)))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000077F0 File Offset: 0x000059F0
		public static int GetCaseSensitivePrefixLength(this string string1, string string2)
		{
			int num = 0;
			while (num < string1.Length && num < string2.Length && string1.get_Chars(num) == string2.get_Chars(num))
			{
				num++;
			}
			return num;
		}

		// Token: 0x0400008B RID: 139
		[Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<string> s_lazyNumerals;

		// Token: 0x0400008C RID: 140
		private static readonly Func<char, char> s_toLower = new Func<char, char>(char.ToLower);

		// Token: 0x0400008D RID: 141
		private static readonly Func<char, char> s_toUpper = new Func<char, char>(char.ToUpper);

		// Token: 0x0400008E RID: 142
		private const string AttributeSuffix = "Attribute";
	}
}
