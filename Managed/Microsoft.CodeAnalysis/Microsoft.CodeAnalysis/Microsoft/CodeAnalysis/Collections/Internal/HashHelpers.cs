using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004AE RID: 1198
	internal static class HashHelpers
	{
		// Token: 0x06003027 RID: 12327 RVA: 0x00089D58 File Offset: 0x00087F58
		public static bool IsPrime(int candidate)
		{
			if ((candidate & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)candidate);
				for (int i = 3; i <= num; i += 2)
				{
					if (candidate % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return candidate == 2;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x00089D8C File Offset: 0x00087F8C
		public static int GetPrime(int min)
		{
			if (min < 0)
			{
				throw new ArgumentException(SR.Arg_HTCapacityOverflow);
			}
			foreach (int num in HashHelpers.s_primes)
			{
				if (num >= min)
				{
					return num;
				}
			}
			for (int j = min | 1; j < 2147483647; j += 2)
			{
				if (HashHelpers.IsPrime(j) && (j - 1) % 101 != 0)
				{
					return j;
				}
			}
			return min;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00089DEC File Offset: 0x00087FEC
		public static int ExpandPrime(int oldSize)
		{
			int num = 2 * oldSize;
			if (num > 2146435069 && 2146435069 > oldSize)
			{
				return 2146435069;
			}
			return HashHelpers.GetPrime(num);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x00089E19 File Offset: 0x00088019
		public static ulong GetFastModMultiplier(uint divisor)
		{
			return ulong.MaxValue / (ulong)divisor + 1UL;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x00089E23 File Offset: 0x00088023
		[MethodImpl(256)]
		public static uint FastMod(uint value, uint divisor, ulong multiplier)
		{
			return (uint)(((multiplier * (ulong)value >> 32) + 1UL) * (ulong)divisor >> 32);
		}

		// Token: 0x0400129E RID: 4766
		public const int MaxPrimeArrayLength = 2146435069;

		// Token: 0x0400129F RID: 4767
		public const int HashPrime = 101;

		// Token: 0x040012A0 RID: 4768
		[Nullable(1)]
		private static readonly int[] s_primes = new int[]
		{
			3, 7, 11, 17, 23, 29, 37, 47, 59, 71,
			89, 107, 131, 163, 197, 239, 293, 353, 431, 521,
			631, 761, 919, 1103, 1327, 1597, 1931, 2333, 2801, 3371,
			4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519, 21023,
			25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363,
			156437, 187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403,
			968897, 1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559,
			5999471, 7199369
		};
	}
}
