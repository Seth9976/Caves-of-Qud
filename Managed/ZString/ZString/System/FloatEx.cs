using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000008 RID: 8
	internal static class FloatEx
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000021AB File Offset: 0x000003AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(double d)
		{
			return (BitConverter.DoubleToInt64Bits(d) & long.MaxValue) < 9218868437227405312L;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021C8 File Offset: 0x000003C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(double d)
		{
			return BitConverter.DoubleToInt64Bits(d) < 0L;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021D4 File Offset: 0x000003D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(float f)
		{
			return (FloatEx.SingleToInt32Bits(f) & int.MaxValue) < 2139095040;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021E9 File Offset: 0x000003E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(float f)
		{
			return FloatEx.SingleToInt32Bits(f) < 0;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021F4 File Offset: 0x000003F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int SingleToInt32Bits(float value)
		{
			return *(int*)(&value);
		}
	}
}
