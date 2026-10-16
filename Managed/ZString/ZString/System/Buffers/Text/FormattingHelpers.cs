using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	// Token: 0x02000010 RID: 16
	internal static class FormattingHelpers
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x000085D0 File Offset: 0x000067D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(ulong value)
		{
			int num = 1;
			uint num2;
			if (value >= 10000000UL)
			{
				if (value >= 100000000000000UL)
				{
					num2 = (uint)(value / 100000000000000UL);
					num += 14;
				}
				else
				{
					num2 = (uint)(value / 10000000UL);
					num += 7;
				}
			}
			else
			{
				num2 = (uint)value;
			}
			if (num2 >= 10U)
			{
				if (num2 < 100U)
				{
					num++;
				}
				else if (num2 < 1000U)
				{
					num += 2;
				}
				else if (num2 < 10000U)
				{
					num += 3;
				}
				else if (num2 < 100000U)
				{
					num += 4;
				}
				else if (num2 < 1000000U)
				{
					num += 5;
				}
				else
				{
					num += 6;
				}
			}
			return num;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00008668 File Offset: 0x00006868
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDigits(uint value)
		{
			int num = 1;
			if (value >= 100000U)
			{
				value /= 100000U;
				num += 5;
			}
			if (value >= 10U)
			{
				if (value < 100U)
				{
					num++;
				}
				else if (value < 1000U)
				{
					num += 2;
				}
				else if (value < 10000U)
				{
					num += 3;
				}
				else
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000086BD File Offset: 0x000068BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountHexDigits(ulong value)
		{
			return 64 - BitOperations.LeadingZeroCount(value | 1UL) + 3 >> 2;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000086D0 File Offset: 0x000068D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int CountDecimalTrailingZeros(uint value, out uint valueWithoutTrailingZeros)
		{
			int num = 0;
			if (value != 0U)
			{
				for (;;)
				{
					uint num2 = value / 10U;
					if (value != num2 * 10U)
					{
						break;
					}
					value = num2;
					num++;
				}
			}
			valueWithoutTrailingZeros = value;
			return num;
		}
	}
}
