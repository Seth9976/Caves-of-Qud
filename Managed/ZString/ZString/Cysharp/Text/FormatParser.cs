using System;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000017 RID: 23
	internal static class FormatParser
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00009AB4 File Offset: 0x00007CB4
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ParserScanResult ScanFormatString(string format, ref int i)
		{
			int length = format.Length;
			char c = format[i];
			i++;
			if (c == '}')
			{
				if (i < length && format[i] == '}')
				{
					i++;
					return ParserScanResult.EscapedChar;
				}
				ExceptionUtil.ThrowFormatError();
				return ParserScanResult.NormalChar;
			}
			else
			{
				if (c != '{')
				{
					return ParserScanResult.NormalChar;
				}
				if (i < length && format[i] == '{')
				{
					i++;
					return ParserScanResult.EscapedChar;
				}
				i--;
				return ParserScanResult.BraceOpen;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00009B24 File Offset: 0x00007D24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ParserScanResult ScanFormatString(ReadOnlySpan<char> format, ref int i)
		{
			int length = format.Length;
			char c = (char)(*format[i]);
			i++;
			if (c == '}')
			{
				if (i < length && *format[i] == 125)
				{
					i++;
					return ParserScanResult.EscapedChar;
				}
				ExceptionUtil.ThrowFormatError();
				return ParserScanResult.NormalChar;
			}
			else
			{
				if (c != '{')
				{
					return ParserScanResult.NormalChar;
				}
				if (i < length && *format[i] == 123)
				{
					i++;
					return ParserScanResult.EscapedChar;
				}
				i--;
				return ParserScanResult.BraceOpen;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009B9A File Offset: 0x00007D9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsDigit(char c)
		{
			return '0' <= c && c <= '9';
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00009BAC File Offset: 0x00007DAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static FormatParser.ParseResult Parse(ReadOnlySpan<char> format, int i)
		{
			char c = '\0';
			int length = format.Length;
			i++;
			if (i == length || !FormatParser.IsDigit(c = (char)(*format[i])))
			{
				ExceptionUtil.ThrowFormatError();
			}
			int num = 0;
			do
			{
				num = num * 10 + (int)c - 48;
				if (++i == length)
				{
					ExceptionUtil.ThrowFormatError();
				}
				c = (char)(*format[i]);
			}
			while (FormatParser.IsDigit(c) && num < 16);
			if (num >= 16)
			{
				ExceptionUtil.ThrowFormatException();
			}
			while (i < length && (c = (char)(*format[i])) == ' ')
			{
				i++;
			}
			int num2 = 0;
			if (c == ',')
			{
				i++;
				while (i < length && (c = (char)(*format[i])) == ' ')
				{
					i++;
				}
				if (i == length)
				{
					ExceptionUtil.ThrowFormatError();
				}
				bool flag = false;
				if (c == '-')
				{
					flag = true;
					if (++i == length)
					{
						ExceptionUtil.ThrowFormatError();
					}
					c = (char)(*format[i]);
				}
				if (!FormatParser.IsDigit(c))
				{
					ExceptionUtil.ThrowFormatError();
				}
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					if (++i == length)
					{
						ExceptionUtil.ThrowFormatError();
					}
					c = (char)(*format[i]);
				}
				while (FormatParser.IsDigit(c) && num2 < 1000);
				if (flag)
				{
					num2 *= -1;
				}
			}
			while (i < length && (c = (char)(*format[i])) == ' ')
			{
				i++;
			}
			ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
			if (c == ':')
			{
				i++;
				int num3 = i;
				for (;;)
				{
					if (i == length)
					{
						ExceptionUtil.ThrowFormatError();
					}
					c = (char)(*format[i]);
					if (c == '}')
					{
						break;
					}
					if (c == '{')
					{
						ExceptionUtil.ThrowFormatError();
					}
					i++;
				}
				if (i > num3)
				{
					readOnlySpan = format.Slice(num3, i - num3);
				}
			}
			else if (c != '}')
			{
				ExceptionUtil.ThrowFormatError();
			}
			i++;
			return new FormatParser.ParseResult(num, readOnlySpan, i, num2);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00009D5C File Offset: 0x00007F5C
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FormatParser.ParseResult Parse(string format, int i)
		{
			char c = '\0';
			int length = format.Length;
			i++;
			if (i == length || !FormatParser.IsDigit(c = format[i]))
			{
				ExceptionUtil.ThrowFormatError();
			}
			int num = 0;
			do
			{
				num = num * 10 + (int)c - 48;
				if (++i == length)
				{
					ExceptionUtil.ThrowFormatError();
				}
				c = format[i];
			}
			while (FormatParser.IsDigit(c) && num < 16);
			if (num >= 16)
			{
				ExceptionUtil.ThrowFormatException();
			}
			while (i < length && (c = format[i]) == ' ')
			{
				i++;
			}
			int num2 = 0;
			if (c == ',')
			{
				i++;
				while (i < length && (c = format[i]) == ' ')
				{
					i++;
				}
				if (i == length)
				{
					ExceptionUtil.ThrowFormatError();
				}
				bool flag = false;
				if (c == '-')
				{
					flag = true;
					if (++i == length)
					{
						ExceptionUtil.ThrowFormatError();
					}
					c = format[i];
				}
				if (!FormatParser.IsDigit(c))
				{
					ExceptionUtil.ThrowFormatError();
				}
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					if (++i == length)
					{
						ExceptionUtil.ThrowFormatError();
					}
					c = format[i];
				}
				while (FormatParser.IsDigit(c) && num2 < 1000);
				if (flag)
				{
					num2 *= -1;
				}
			}
			while (i < length && (c = format[i]) == ' ')
			{
				i++;
			}
			ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
			if (c == ':')
			{
				i++;
				int num3 = i;
				for (;;)
				{
					if (i == length)
					{
						ExceptionUtil.ThrowFormatError();
					}
					c = format[i];
					if (c == '}')
					{
						break;
					}
					if (c == '{')
					{
						ExceptionUtil.ThrowFormatError();
					}
					i++;
				}
				if (i > num3)
				{
					readOnlySpan = format.AsSpan(num3, i - num3);
				}
			}
			else if (c != '}')
			{
				ExceptionUtil.ThrowFormatError();
			}
			i++;
			return new FormatParser.ParseResult(num, readOnlySpan, i, num2);
		}

		// Token: 0x04000039 RID: 57
		internal const int ArgLengthLimit = 16;

		// Token: 0x0400003A RID: 58
		internal const int WidthLimit = 1000;

		// Token: 0x02000050 RID: 80
		public readonly ref struct ParseResult
		{
			// Token: 0x060003B2 RID: 946 RVA: 0x0002450E File Offset: 0x0002270E
			public ParseResult(int index, ReadOnlySpan<char> formatString, int lastIndex, int alignment)
			{
				this.Index = index;
				this.FormatString = formatString;
				this.LastIndex = lastIndex;
				this.Alignment = alignment;
			}

			// Token: 0x0400012A RID: 298
			public readonly int Index;

			// Token: 0x0400012B RID: 299
			public readonly ReadOnlySpan<char> FormatString;

			// Token: 0x0400012C RID: 300
			public readonly int LastIndex;

			// Token: 0x0400012D RID: 301
			public readonly int Alignment;
		}
	}
}
