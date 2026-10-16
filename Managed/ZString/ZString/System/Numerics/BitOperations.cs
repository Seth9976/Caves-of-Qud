using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Numerics
{
	// Token: 0x02000011 RID: 17
	internal static class BitOperations
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000086FB File Offset: 0x000068FB
		private unsafe static ReadOnlySpan<byte> TrailingZeroCountDeBruijn
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.3BF63951626584EB1653F9B8DBB590A5EE1EAE1135A904B9317C3773896DF076), 32);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00008709 File Offset: 0x00006909
		private unsafe static ReadOnlySpan<byte> Log2DeBruijn
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.4BCD43D478B9229AB7A13406353712C7944B60348C36B4D0E6B789D10F697652), 32);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00008717 File Offset: 0x00006917
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(uint value)
		{
			if (value == 0U)
			{
				return 32;
			}
			return 31 - BitOperations.Log2SoftwareFallback(value);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00008728 File Offset: 0x00006928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(ulong value)
		{
			uint num = (uint)(value >> 32);
			if (num == 0U)
			{
				return 32 + BitOperations.LeadingZeroCount((uint)value);
			}
			return BitOperations.LeadingZeroCount(num);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000874F File Offset: 0x0000694F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Log2(uint value)
		{
			return BitOperations.Log2SoftwareFallback(value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008758 File Offset: 0x00006958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Log2(ulong value)
		{
			uint num = (uint)(value >> 32);
			if (num == 0U)
			{
				return BitOperations.Log2((uint)value);
			}
			return 32 + BitOperations.Log2(num);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00008780 File Offset: 0x00006980
		private unsafe static int Log2SoftwareFallback(uint value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return (int)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(BitOperations.Log2DeBruijn), (IntPtr)((int)(value * 130329821U >> 27))));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000087D0 File Offset: 0x000069D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(uint value)
		{
			value -= (value >> 1) & 1431655765U;
			value = (value & 858993459U) + ((value >> 2) & 858993459U);
			value = ((value + (value >> 4)) & 252645135U) * 16843009U >> 24;
			return (int)value;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000880C File Offset: 0x00006A0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(ulong value)
		{
			if (IntPtr.Size == 4)
			{
				return BitOperations.PopCount((uint)value) + BitOperations.PopCount((uint)(value >> 32));
			}
			value -= (value >> 1) & 6148914691236517205UL;
			value = (value & 3689348814741910323UL) + ((value >> 2) & 3689348814741910323UL);
			value = ((value + (value >> 4)) & 1085102592571150095UL) * 72340172838076673UL >> 56;
			return (int)value;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00008880 File Offset: 0x00006A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(int value)
		{
			return BitOperations.TrailingZeroCount((uint)value);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008888 File Offset: 0x00006A88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int TrailingZeroCount(uint value)
		{
			if (value == 0U)
			{
				return 32;
			}
			return (int)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(BitOperations.TrailingZeroCountDeBruijn), (IntPtr)((int)((value & -value) * 125613361U >> 27))));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000088B2 File Offset: 0x00006AB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(long value)
		{
			return BitOperations.TrailingZeroCount((ulong)value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000088BC File Offset: 0x00006ABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(ulong value)
		{
			uint num = (uint)value;
			if (num == 0U)
			{
				return 32 + BitOperations.TrailingZeroCount((uint)(value >> 32));
			}
			return BitOperations.TrailingZeroCount(num);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000088E3 File Offset: 0x00006AE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 32 - offset);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000088F5 File Offset: 0x00006AF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong RotateLeft(ulong value, int offset)
		{
			return (value << offset) | (value >> 64 - offset);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00008907 File Offset: 0x00006B07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateRight(uint value, int offset)
		{
			return (value >> offset) | (value << 32 - offset);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00008919 File Offset: 0x00006B19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong RotateRight(ulong value, int offset)
		{
			return (value >> offset) | (value << 64 - offset);
		}
	}
}
