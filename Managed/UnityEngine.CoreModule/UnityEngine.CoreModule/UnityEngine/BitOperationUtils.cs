using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000242 RID: 578
	internal static class BitOperationUtils
	{
		// Token: 0x06001E9D RID: 7837 RVA: 0x0003AE59 File Offset: 0x00039059
		internal static int CountBits(int mask)
		{
			return BitOperationUtils.CountBits((uint)mask);
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x0003AE64 File Offset: 0x00039064
		internal static int CountBits(uint mask)
		{
			uint num = 0U;
			while (mask > 0U)
			{
				num += mask & 1U;
				mask >>= 1;
			}
			return (int)num;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x0003AE90 File Offset: 0x00039090
		private static bool IsValueWithinMaskedBitsRange(uint value, uint mask, int bitCount)
		{
			return BitOperationUtils.AnyBitMatch(mask, value) && BitOperationUtils.IsValueSmallerOrEqualThanIndex(value, BitOperationUtils.BitCountToIndex(bitCount));
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0003AEBC File Offset: 0x000390BC
		internal static uint ModifyMaskByValuesArrayAndBitCount(uint mask, IEnumerable<int> values, int bitCount = 32)
		{
			BitOperationUtils.AssertBitCount(bitCount);
			uint num = 0U;
			foreach (int num2 in values)
			{
				uint num3 = (uint)num2;
				bool flag = BitOperationUtils.IsValueWithinMaskedBitsRange(num3, mask, bitCount);
				if (flag)
				{
					num += num3;
				}
			}
			return num;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x0003AF28 File Offset: 0x00039128
		internal static bool AreAllBitsSetForValues(uint mask, IEnumerable<int> values, int bitCount = 32)
		{
			BitOperationUtils.AssertBitCount(bitCount);
			foreach (int num in values)
			{
				uint num2 = (uint)num;
				bool flag = BitOperationUtils.AnyBitMatch(mask, num2);
				bool flag2 = !flag || BitOperationUtils.IsValueBiggerOrEqualThanIndex(num2, BitOperationUtils.BitCountToIndex(bitCount));
				if (flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x0003AFA4 File Offset: 0x000391A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint IndexToValue(int index)
		{
			BitOperationUtils.AssertIndex(index);
			return 1U << index;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0003AFC4 File Offset: 0x000391C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsValueSmallerThanIndex(uint value, int index)
		{
			BitOperationUtils.AssertIndex(index);
			return value < BitOperationUtils.IndexToValue(index);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x0003AFE8 File Offset: 0x000391E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsValueBiggerThanIndex(uint value, int index)
		{
			BitOperationUtils.AssertIndex(index);
			return value > BitOperationUtils.IndexToValue(index);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0003B00C File Offset: 0x0003920C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsValueSmallerOrEqualThanIndex(uint value, int index)
		{
			BitOperationUtils.AssertIndex(index);
			return value <= BitOperationUtils.IndexToValue(index);
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0003B034 File Offset: 0x00039234
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsValueBiggerOrEqualThanIndex(uint value, int index)
		{
			BitOperationUtils.AssertIndex(index);
			return value >= BitOperationUtils.IndexToValue(index);
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x0003B05C File Offset: 0x0003925C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool AnyBitMatch(uint mask, uint value)
		{
			return (mask & value) > 0U;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0003B074 File Offset: 0x00039274
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int BitCountToIndex(int bitCount)
		{
			BitOperationUtils.AssertBitCount(bitCount);
			return bitCount - 1;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x0003B090 File Offset: 0x00039290
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertBitCount(int bitCount)
		{
			Debug.Assert(bitCount >= 1 && bitCount <= 32, "Bit count must be between 1 and 32.");
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0003B0AD File Offset: 0x000392AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void AssertIndex(int index)
		{
			Debug.Assert(index >= 0 && index <= 31, "Index must be between 0 and 31.");
		}
	}
}
