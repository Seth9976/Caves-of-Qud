using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200021A RID: 538
	internal static class ObjectDisplay
	{
		// Token: 0x0600274B RID: 10059 RVA: 0x000D6508 File Offset: 0x000D4708
		public static string FormatPrimitive(object obj, ObjectDisplayOptions options)
		{
			if (obj == null)
			{
				return ObjectDisplay.NullLiteral;
			}
			Type type = obj.GetType();
			if (IntrospectionExtensions.GetTypeInfo(type).IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (type == typeof(int))
			{
				return ObjectDisplay.FormatLiteral((int)obj, options, null);
			}
			if (type == typeof(string))
			{
				return ObjectDisplay.FormatLiteral((string)obj, options);
			}
			if (type == typeof(bool))
			{
				return ObjectDisplay.FormatLiteral((bool)obj);
			}
			if (type == typeof(char))
			{
				return ObjectDisplay.FormatLiteral((char)obj, options);
			}
			if (type == typeof(byte))
			{
				return ObjectDisplay.FormatLiteral((byte)obj, options, null);
			}
			if (type == typeof(short))
			{
				return ObjectDisplay.FormatLiteral((short)obj, options, null);
			}
			if (type == typeof(long))
			{
				return ObjectDisplay.FormatLiteral((long)obj, options, null);
			}
			if (type == typeof(double))
			{
				return ObjectDisplay.FormatLiteral((double)obj, options, null);
			}
			if (type == typeof(ulong))
			{
				return ObjectDisplay.FormatLiteral((ulong)obj, options, null);
			}
			if (type == typeof(uint))
			{
				return ObjectDisplay.FormatLiteral((uint)obj, options, null);
			}
			if (type == typeof(ushort))
			{
				return ObjectDisplay.FormatLiteral((ushort)obj, options, null);
			}
			if (type == typeof(sbyte))
			{
				return ObjectDisplay.FormatLiteral((sbyte)obj, options, null);
			}
			if (type == typeof(float))
			{
				return ObjectDisplay.FormatLiteral((float)obj, options, null);
			}
			if (type == typeof(decimal))
			{
				return ObjectDisplay.FormatLiteral((decimal)obj, options, null);
			}
			return null;
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600274C RID: 10060 RVA: 0x000D66F6 File Offset: 0x000D48F6
		internal static string NullLiteral
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000D66FD File Offset: 0x000D48FD
		internal static string FormatLiteral(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000D6710 File Offset: 0x000D4910
		private static bool TryReplaceChar(char c, out string replaceWith)
		{
			replaceWith = null;
			switch (c)
			{
			case '\0':
				replaceWith = "\\0";
				break;
			case '\u0001':
			case '\u0002':
			case '\u0003':
			case '\u0004':
			case '\u0005':
			case '\u0006':
				break;
			case '\a':
				replaceWith = "\\a";
				break;
			case '\b':
				replaceWith = "\\b";
				break;
			case '\t':
				replaceWith = "\\t";
				break;
			case '\n':
				replaceWith = "\\n";
				break;
			case '\v':
				replaceWith = "\\v";
				break;
			case '\f':
				replaceWith = "\\f";
				break;
			case '\r':
				replaceWith = "\\r";
				break;
			default:
				if (c == '\\')
				{
					replaceWith = "\\\\";
				}
				break;
			}
			if (replaceWith != null)
			{
				return true;
			}
			if (ObjectDisplay.NeedsEscaping(CharUnicodeInfo.GetUnicodeCategory(c)))
			{
				string text = "\\u";
				int num = (int)c;
				replaceWith = text + num.ToString("x4");
				return true;
			}
			return false;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000D67E2 File Offset: 0x000D49E2
		private static bool NeedsEscaping(UnicodeCategory category)
		{
			return category - 12 <= 2 || category == 16 || category == 29;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000D67F8 File Offset: 0x000D49F8
		public static string FormatLiteral(string value, ObjectDisplayOptions options)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			bool flag = options.IncludesOption(ObjectDisplayOptions.UseQuotes);
			bool flag2 = options.IncludesOption(ObjectDisplayOptions.EscapeNonPrintableCharacters);
			bool flag3 = flag && !flag2 && ObjectDisplay.ContainsNewLine(value);
			if (flag)
			{
				if (flag3)
				{
					builder.Append('@');
				}
				builder.Append('"');
			}
			for (int i = 0; i < value.Length; i++)
			{
				char c = value.get_Chars(i);
				string text2;
				if (flag2 && CharUnicodeInfo.GetUnicodeCategory(c) == 16)
				{
					UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(value, i);
					if (unicodeCategory == 16)
					{
						StringBuilder stringBuilder = builder;
						string text = "\\u";
						int num = (int)c;
						stringBuilder.Append(text + num.ToString("x4"));
					}
					else if (ObjectDisplay.NeedsEscaping(unicodeCategory))
					{
						builder.Append("\\U" + char.ConvertToUtf32(value, i).ToString("x8"));
						i++;
					}
					else
					{
						builder.Append(c);
						builder.Append(value.get_Chars(++i));
					}
				}
				else if (flag2 && ObjectDisplay.TryReplaceChar(c, out text2))
				{
					builder.Append(text2);
				}
				else if (flag && c == '"')
				{
					if (flag3)
					{
						builder.Append('"');
						builder.Append('"');
					}
					else
					{
						builder.Append('\\');
						builder.Append('"');
					}
				}
				else
				{
					builder.Append(c);
				}
			}
			if (flag)
			{
				builder.Append('"');
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000D698C File Offset: 0x000D4B8C
		private static bool ContainsNewLine(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (SyntaxFacts.IsNewLine(s.get_Chars(i)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000D69C0 File Offset: 0x000D4BC0
		internal static string FormatLiteral(char c, ObjectDisplayOptions options)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			if (options.IncludesOption(ObjectDisplayOptions.IncludeCodePoints))
			{
				StringBuilder stringBuilder = builder;
				string text;
				if (!options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
				{
					int num = (int)c;
					text = num.ToString();
				}
				else
				{
					string text2 = "0x";
					int num = (int)c;
					text = text2 + num.ToString("x4");
				}
				stringBuilder.Append(text);
				builder.Append(" ");
			}
			bool flag = options.IncludesOption(ObjectDisplayOptions.UseQuotes);
			bool flag2 = options.IncludesOption(ObjectDisplayOptions.EscapeNonPrintableCharacters);
			if (flag)
			{
				builder.Append('\'');
			}
			string text3;
			if (flag2 && ObjectDisplay.TryReplaceChar(c, out text3))
			{
				builder.Append(text3);
			}
			else if (flag && c == '\'')
			{
				builder.Append('\\');
				builder.Append('\'');
			}
			else
			{
				builder.Append(c);
			}
			if (flag)
			{
				builder.Append('\'');
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000D6A8C File Offset: 0x000D4C8C
		internal static string FormatLiteral(sbyte value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				string text = "0x";
				string text2;
				if (value < 0)
				{
					int num = (int)value;
					text2 = num.ToString("x8");
				}
				else
				{
					text2 = value.ToString("x2");
				}
				return text + text2;
			}
			return value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo));
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000D6ADA File Offset: 0x000D4CDA
		internal static string FormatLiteral(byte value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				return "0x" + value.ToString("x2");
			}
			return value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo));
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000D6B0C File Offset: 0x000D4D0C
		internal static string FormatLiteral(short value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				string text = "0x";
				string text2;
				if (value < 0)
				{
					int num = (int)value;
					text2 = num.ToString("x8");
				}
				else
				{
					text2 = value.ToString("x4");
				}
				return text + text2;
			}
			return value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo));
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000D6B5A File Offset: 0x000D4D5A
		internal static string FormatLiteral(ushort value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				return "0x" + value.ToString("x4");
			}
			return value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo));
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000D6B89 File Offset: 0x000D4D89
		internal static string FormatLiteral(int value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				return "0x" + value.ToString("x8");
			}
			return value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo));
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000D6BB8 File Offset: 0x000D4DB8
		internal static string FormatLiteral(uint value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				builder.Append("0x");
				builder.Append(value.ToString("x8"));
			}
			else
			{
				builder.Append(value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo)));
			}
			if (options.IncludesOption(ObjectDisplayOptions.IncludeTypeSuffix))
			{
				builder.Append('U');
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000D6C28 File Offset: 0x000D4E28
		internal static string FormatLiteral(long value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				builder.Append("0x");
				builder.Append(value.ToString("x16"));
			}
			else
			{
				builder.Append(value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo)));
			}
			if (options.IncludesOption(ObjectDisplayOptions.IncludeTypeSuffix))
			{
				builder.Append('L');
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000D6C98 File Offset: 0x000D4E98
		internal static string FormatLiteral(ulong value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
			{
				builder.Append("0x");
				builder.Append(value.ToString("x16"));
			}
			else
			{
				builder.Append(value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo)));
			}
			if (options.IncludesOption(ObjectDisplayOptions.IncludeTypeSuffix))
			{
				builder.Append("UL");
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000D6D0C File Offset: 0x000D4F0C
		internal static string FormatLiteral(double value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			string text = value.ToString("R", ObjectDisplay.GetFormatCulture(cultureInfo));
			if (!options.IncludesOption(ObjectDisplayOptions.IncludeTypeSuffix))
			{
				return text;
			}
			return text + "D";
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000D6D44 File Offset: 0x000D4F44
		internal static string FormatLiteral(float value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			string text = value.ToString("R", ObjectDisplay.GetFormatCulture(cultureInfo));
			if (!options.IncludesOption(ObjectDisplayOptions.IncludeTypeSuffix))
			{
				return text;
			}
			return text + "F";
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000D6D7C File Offset: 0x000D4F7C
		internal static string FormatLiteral(decimal value, ObjectDisplayOptions options, CultureInfo cultureInfo = null)
		{
			string text = value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo));
			if (!options.IncludesOption(ObjectDisplayOptions.IncludeTypeSuffix))
			{
				return text;
			}
			return text + "M";
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x000D6DAD File Offset: 0x000D4FAD
		private static CultureInfo GetFormatCulture(CultureInfo cultureInfo)
		{
			return cultureInfo ?? CultureInfo.InvariantCulture;
		}
	}
}
