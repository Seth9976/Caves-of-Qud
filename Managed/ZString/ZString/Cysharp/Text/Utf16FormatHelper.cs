using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000015 RID: 21
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Utf16FormatHelper
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00009684 File Offset: 0x00007884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void FormatTo<[Nullable(0)] TBufferWriter, [Nullable(2)] T>(ref TBufferWriter sb, T arg, int width, [Nullable(0)] ReadOnlySpan<char> format, string argName) where TBufferWriter : IBufferWriter<char>
		{
			if (width <= 0)
			{
				Span<char> span = sb.GetSpan(0);
				int num;
				if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num, format))
				{
					sb.Advance(0);
					span = sb.GetSpan(Math.Max(span.Length + 1, num));
					if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num, format))
					{
						ExceptionUtil.ThrowArgumentException(argName);
					}
				}
				sb.Advance(num);
				width *= -1;
				int num2 = width - num;
				if (width > 0 && num2 > 0)
				{
					sb.GetSpan(num2).Fill(' ');
					sb.Advance(num2);
					return;
				}
			}
			else
			{
				Utf16FormatHelper.FormatToRightJustify<TBufferWriter, T>(ref sb, arg, width, format, argName);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00009748 File Offset: 0x00007948
		private unsafe static void FormatToRightJustify<[Nullable(0)] TBufferWriter, [Nullable(2)] T>(ref TBufferWriter sb, T arg, int width, [Nullable(0)] ReadOnlySpan<char> format, string argName) where TBufferWriter : IBufferWriter<char>
		{
			if (typeof(T) == typeof(string))
			{
				string text = Unsafe.As<string>(arg);
				int num = width - text.Length;
				if (num > 0)
				{
					sb.GetSpan(num).Fill(' ');
					sb.Advance(num);
				}
				Span<char> span = sb.GetSpan(text.Length);
				text.AsSpan().CopyTo(span);
				sb.Advance(text.Length);
				return;
			}
			int num2 = (typeof(T).IsValueType ? (Unsafe.SizeOf<T>() * 8) : 1024);
			Span<char> span2;
			int num3;
			checked
			{
				span2 = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span2, out num3, format))
				{
					num2 = unchecked(span2.Length * 2);
					span2 = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
					if (!Utf16ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span2, out num3, format))
					{
						ExceptionUtil.ThrowArgumentException(argName);
					}
				}
			}
			int num4 = width - num3;
			if (num4 > 0)
			{
				sb.GetSpan(num4).Fill(' ');
				sb.Advance(num4);
			}
			Span<char> span3 = sb.GetSpan(num3);
			span2.CopyTo(span3);
			sb.Advance(num3);
		}

		// Token: 0x04000037 RID: 55
		private const char sp = ' ';
	}
}
