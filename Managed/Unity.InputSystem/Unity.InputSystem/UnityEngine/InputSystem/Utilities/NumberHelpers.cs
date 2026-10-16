using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013D RID: 317
	internal static class NumberHelpers
	{
		// Token: 0x060011A7 RID: 4519 RVA: 0x00054B1C File Offset: 0x00052D1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int AlignToMultipleOf(this int number, int alignment)
		{
			int num = number % alignment;
			if (num == 0)
			{
				return number;
			}
			return number + alignment - num;
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00054B38 File Offset: 0x00052D38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long AlignToMultipleOf(this long number, long alignment)
		{
			long num = number % alignment;
			if (num == 0L)
			{
				return number;
			}
			return number + alignment - num;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00054B54 File Offset: 0x00052D54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint AlignToMultipleOf(this uint number, uint alignment)
		{
			uint num = number % alignment;
			if (num == 0U)
			{
				return number;
			}
			return number + alignment - num;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00054B6F File Offset: 0x00052D6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Approximately(double a, double b)
		{
			return Math.Abs(b - a) < Math.Max(1E-06 * Math.Max(Math.Abs(a), Math.Abs(b)), 4E-323);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00054BA4 File Offset: 0x00052DA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IntToNormalizedFloat(int value, int minValue, int maxValue)
		{
			if (value <= minValue)
			{
				return 0f;
			}
			if (value >= maxValue)
			{
				return 1f;
			}
			return (float)(((double)value - (double)minValue) / ((double)maxValue - (double)minValue));
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00054BC6 File Offset: 0x00052DC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int NormalizedFloatToInt(float value, int intMinValue, int intMaxValue)
		{
			if (value <= 0f)
			{
				return intMinValue;
			}
			if (value >= 1f)
			{
				return intMaxValue;
			}
			return (int)((double)value * ((double)intMaxValue - (double)intMinValue) + (double)intMinValue);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00054BE8 File Offset: 0x00052DE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float UIntToNormalizedFloat(uint value, uint minValue, uint maxValue)
		{
			if (value <= minValue)
			{
				return 0f;
			}
			if (value >= maxValue)
			{
				return 1f;
			}
			return (float)((value - minValue) / (maxValue - minValue));
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00054C0E File Offset: 0x00052E0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint NormalizedFloatToUInt(float value, uint uintMinValue, uint uintMaxValue)
		{
			if (value <= 0f)
			{
				return uintMinValue;
			}
			if (value >= 1f)
			{
				return uintMaxValue;
			}
			return (uint)((double)value * (uintMaxValue - uintMinValue) + uintMinValue);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00054C34 File Offset: 0x00052E34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RemapUIntBitsToNormalizeFloatToUIntBits(uint value, uint inBitSize, uint outBitSize)
		{
			uint num = (uint)((1L << (int)inBitSize) - 1L);
			uint num2 = (uint)((1L << (int)outBitSize) - 1L);
			return NumberHelpers.NormalizedFloatToUInt(NumberHelpers.UIntToNormalizedFloat(value, 0U, num), 0U, num2);
		}
	}
}
