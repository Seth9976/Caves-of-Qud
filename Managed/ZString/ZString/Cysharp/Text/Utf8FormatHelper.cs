using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Cysharp.Text
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Utf8FormatHelper
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000098B4 File Offset: 0x00007AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void FormatTo<[Nullable(0)] TBufferWriter, [Nullable(2)] T>(ref TBufferWriter sb, T arg, int width, StandardFormat format, string argName) where TBufferWriter : IBufferWriter<byte>
		{
			if (width <= 0)
			{
				Span<byte> span = sb.GetSpan(0);
				int num;
				if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num, format))
				{
					sb.Advance(0);
					span = sb.GetSpan(Math.Max(span.Length + 1, num));
					if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num, format))
					{
						ExceptionUtil.ThrowArgumentException(argName);
					}
				}
				sb.Advance(num);
				width *= -1;
				int num2 = width - num;
				if (width > 0 && num2 > 0)
				{
					sb.GetSpan(num2).Fill(32);
					sb.Advance(num2);
					return;
				}
			}
			else
			{
				Utf8FormatHelper.FormatToRightJustify<TBufferWriter, T>(ref sb, arg, width, format, argName);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00009978 File Offset: 0x00007B78
		private unsafe static void FormatToRightJustify<[Nullable(0)] TBufferWriter, [Nullable(2)] T>(ref TBufferWriter sb, T arg, int width, StandardFormat format, string argName) where TBufferWriter : IBufferWriter<byte>
		{
			if (typeof(T) == typeof(string))
			{
				string text = Unsafe.As<string>(arg);
				int num = width - text.Length;
				if (num > 0)
				{
					sb.GetSpan(num).Fill(32);
					sb.Advance(num);
				}
				ZString.AppendChars<TBufferWriter>(ref sb, text.AsSpan());
				return;
			}
			int num2 = (typeof(T).IsValueType ? (Unsafe.SizeOf<T>() * 8) : 1024);
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)num2], num2);
			int num3;
			if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num3, format))
			{
				num2 = span.Length * 2;
				span = new Span<byte>(stackalloc byte[(UIntPtr)num2], num2);
				if (!Utf8ValueStringBuilder.FormatterCache<T>.TryFormatDelegate(arg, span, out num3, format))
				{
					ExceptionUtil.ThrowArgumentException(argName);
				}
			}
			int num4 = width - num3;
			if (num4 > 0)
			{
				sb.GetSpan(num4).Fill(32);
				sb.Advance(num4);
			}
			Span<byte> span2 = sb.GetSpan(num3);
			span.CopyTo(span2);
			sb.Advance(num3);
		}

		// Token: 0x04000038 RID: 56
		private const byte sp = 32;
	}
}
