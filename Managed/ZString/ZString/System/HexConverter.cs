using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000A RID: 10
	internal static class HexConverter
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000264C File Offset: 0x0000084C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToBytesBuffer(byte value, Span<byte> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U) | casing);
			*buffer[startingIndex + 1] = (byte)num2;
			*buffer[startingIndex] = (byte)(num2 >> 8);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026A0 File Offset: 0x000008A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void ToCharsBuffer(byte value, Span<char> buffer, int startingIndex = 0, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			uint num = (uint)(((int)(value & 240) << 4) + (int)(value & 15) - 35209);
			uint num2 = (uint)((((-num & 28784U) >> 4) + num + (HexConverter.Casing)47545U) | casing);
			*buffer[startingIndex + 1] = (char)(num2 & 255U);
			*buffer[startingIndex] = (char)(num2 >> 8);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026F8 File Offset: 0x000008F8
		[return: Nullable(1)]
		public unsafe static string ToString(ReadOnlySpan<byte> bytes, HexConverter.Casing casing = HexConverter.Casing.Upper)
		{
			Span<char> span = default(Span<char>);
			if (bytes.Length > 16)
			{
				span = new char[bytes.Length * 2].AsSpan<char>();
			}
			else
			{
				int i = bytes.Length * 2;
				checked
				{
					span = new Span<char>(stackalloc byte[unchecked((UIntPtr)i) * 2], i);
				}
			}
			int num = 0;
			ReadOnlySpan<byte> readOnlySpan = bytes;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				HexConverter.ToCharsBuffer(*readOnlySpan[i], span, num, casing);
				num += 2;
			}
			return span.ToString();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000277D File Offset: 0x0000097D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharUpper(int value)
		{
			value &= 15;
			value += 48;
			if (value > 57)
			{
				value += 7;
			}
			return (char)value;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002797 File Offset: 0x00000997
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static char ToCharLower(int value)
		{
			value &= 15;
			value += 48;
			if (value > 57)
			{
				value += 39;
			}
			return (char)value;
		}

		// Token: 0x02000048 RID: 72
		public enum Casing : uint
		{
			// Token: 0x040000F2 RID: 242
			Upper,
			// Token: 0x040000F3 RID: 243
			Lower = 8224U
		}
	}
}
