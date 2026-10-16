using System;
using System.Collections.Generic;
using System.Text;

namespace Genkit
{
	// Token: 0x02000377 RID: 887
	public static class Hash
	{
		// Token: 0x0600211C RID: 8476 RVA: 0x000A3AE0 File Offset: 0x000A1CE0
		public static int String(string read)
		{
			ulong num = 3074457345618258791UL;
			for (int i = 0; i < read.Length; i++)
			{
				num += (ulong)read[i];
				num *= 3074457345618258799UL;
			}
			return (int)(num >> 32);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000A3B24 File Offset: 0x000A1D24
		public static uint FNV1A32(int Value, uint Hash = 2166136261U)
		{
			for (int i = 0; i < 4; i++)
			{
				Hash ^= (uint)((byte)Value);
				Hash *= 16777619U;
				Value >>= 8;
			}
			return Hash;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000A3B54 File Offset: 0x000A1D54
		public unsafe static uint FNV1A32(ReadOnlySpan<int> Value, uint Hash = 2166136261U)
		{
			int i = 0;
			int length = Value.Length;
			while (i < length)
			{
				int num = *Value[i];
				for (int j = 0; j < 4; j++)
				{
					Hash ^= (uint)((byte)num);
					Hash *= 16777619U;
					num >>= 8;
				}
				i++;
			}
			return Hash;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000A3BA0 File Offset: 0x000A1DA0
		public static uint FNV1A32(string Value, uint Hash = 2166136261U)
		{
			int i = 0;
			int length = Value.Length;
			while (i < length)
			{
				Hash ^= (uint)((byte)Value[i]);
				Hash *= 16777619U;
				Hash ^= (uint)((byte)(Value[i] >> 8));
				Hash *= 16777619U;
				i++;
			}
			return Hash;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000A3BED File Offset: 0x000A1DED
		public static uint FNV1A32(string Value, int Index, int Length, uint Hash = 2166136261U)
		{
			Length += Index;
			while (Index < Length)
			{
				Hash ^= (uint)((byte)Value[Index]);
				Hash *= 16777619U;
				Hash ^= (uint)((byte)(Value[Index++] >> 8));
				Hash *= 16777619U;
			}
			return Hash;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000A3C2C File Offset: 0x000A1E2C
		public static uint FNV1A32(StringBuilder Value, int Index, int Length, uint Hash = 2166136261U)
		{
			Length += Index;
			while (Index < Length)
			{
				char c = Value[Index++];
				Hash ^= (uint)((byte)c);
				Hash *= 16777619U;
				Hash ^= (uint)((byte)(c >> 8));
				Hash *= 16777619U;
			}
			return Hash;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000A3C74 File Offset: 0x000A1E74
		public unsafe static uint FNV1A32(ReadOnlySpan<char> Value, uint Hash = 2166136261U)
		{
			int i = 0;
			int length = Value.Length;
			while (i < length)
			{
				Hash ^= (uint)((byte)(*Value[i]));
				Hash *= 16777619U;
				Hash ^= (uint)((byte)(*Value[i] >> 8));
				Hash *= 16777619U;
				i++;
			}
			return Hash;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000A3CC8 File Offset: 0x000A1EC8
		public unsafe static uint FNV1A32(ReadOnlySpan<char> Value, int Index, int Length, uint Hash = 2166136261U)
		{
			Length += Index;
			while (Index < Length)
			{
				Hash ^= (uint)((byte)(*Value[Index]));
				Hash *= 16777619U;
				Hash ^= (uint)((byte)(*Value[Index++] >> 8));
				Hash *= 16777619U;
			}
			return Hash;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000A3D18 File Offset: 0x000A1F18
		public static uint FNV1A32(IList<string> Values, uint Hash = 2166136261U)
		{
			int i = 0;
			int count = Values.Count;
			while (i < count)
			{
				string text = Values[i];
				int j = 0;
				int length = text.Length;
				while (j < length)
				{
					Hash ^= (uint)((byte)text[j]);
					Hash *= 16777619U;
					Hash ^= (uint)((byte)(text[j] >> 8));
					Hash *= 16777619U;
					j++;
				}
				i++;
			}
			return Hash;
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x000A3D84 File Offset: 0x000A1F84
		public static ulong FNV1A64(int Value, ulong Hash = 14695981039346656037UL)
		{
			for (int i = 0; i < 4; i++)
			{
				Hash ^= (ulong)((byte)Value);
				Hash *= 1099511628211UL;
				Value >>= 8;
			}
			return Hash;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000A3DB8 File Offset: 0x000A1FB8
		public static ulong FNV1A64(string Value, ulong Hash = 14695981039346656037UL)
		{
			int i = 0;
			int length = Value.Length;
			while (i < length)
			{
				Hash ^= (ulong)((byte)Value[i]);
				Hash *= 1099511628211UL;
				Hash ^= (ulong)((byte)(Value[i] >> 8));
				Hash *= 1099511628211UL;
				i++;
			}
			return Hash;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x000A3E10 File Offset: 0x000A2010
		public static ulong FNV1A64(string Value, int Index, int Length, ulong Hash = 14695981039346656037UL)
		{
			Length += Index;
			while (Index < Length)
			{
				Hash ^= (ulong)((byte)Value[Index]);
				Hash *= 1099511628211UL;
				Hash ^= (ulong)((byte)(Value[Index++] >> 8));
				Hash *= 1099511628211UL;
			}
			return Hash;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000A3E64 File Offset: 0x000A2064
		public static ulong FNV1A64(StringBuilder Value, int Index, int Length, ulong Hash = 14695981039346656037UL)
		{
			Length += Index;
			while (Index < Length)
			{
				char c = Value[Index++];
				Hash ^= (ulong)((byte)c);
				Hash *= 1099511628211UL;
				Hash ^= (ulong)((byte)(c >> 8));
				Hash *= 1099511628211UL;
			}
			return Hash;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x000A3EB4 File Offset: 0x000A20B4
		public unsafe static ulong FNV1A64(ReadOnlySpan<char> Value, ulong Hash = 14695981039346656037UL)
		{
			int i = 0;
			int length = Value.Length;
			while (i < length)
			{
				Hash ^= (ulong)((byte)(*Value[i]));
				Hash *= 1099511628211UL;
				Hash ^= (ulong)((byte)(*Value[i] >> 8));
				Hash *= 1099511628211UL;
				i++;
			}
			return Hash;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x000A3F10 File Offset: 0x000A2110
		public unsafe static ulong FNV1A64(ReadOnlySpan<char> Value, int Index, int Length, ulong Hash = 14695981039346656037UL)
		{
			Length += Index;
			while (Index < Length)
			{
				Hash ^= (ulong)((byte)(*Value[Index]));
				Hash *= 1099511628211UL;
				Hash ^= (ulong)((byte)(*Value[Index++] >> 8));
				Hash *= 1099511628211UL;
			}
			return Hash;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000A3F68 File Offset: 0x000A2168
		public static ulong FNV1A64(IList<string> Values, ulong Hash = 14695981039346656037UL)
		{
			int i = 0;
			int count = Values.Count;
			while (i < count)
			{
				string text = Values[i];
				int j = 0;
				int length = text.Length;
				while (j < length)
				{
					Hash ^= (ulong)((byte)text[j]);
					Hash *= 1099511628211UL;
					Hash ^= (ulong)((byte)(text[j] >> 8));
					Hash *= 1099511628211UL;
					j++;
				}
				i++;
			}
			return Hash;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000A3FDC File Offset: 0x000A21DC
		public static bool IsPrime(int Number)
		{
			if ((Number & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)Number);
				for (int i = 3; i <= num; i += 2)
				{
					if (Number % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return Number == 2;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x000A4010 File Offset: 0x000A2210
		public static int GetSharedCapacity(int Minimum)
		{
			foreach (int num in Hash.HASH_POWERS)
			{
				if (num >= Minimum)
				{
					return num;
				}
			}
			return Hash.GetCapacity(Minimum);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x000A4044 File Offset: 0x000A2244
		public static int GetCapacity(int Minimum)
		{
			foreach (int num in Hash.HASH_CAPACITY)
			{
				if (num >= Minimum)
				{
					return num;
				}
			}
			for (int i = Minimum | 1; i < 2147483647; i += 2)
			{
				if (Hash.IsPrime(i))
				{
					Hash.HASH_CAPACITY.Add(i);
					return i;
				}
			}
			return Minimum;
		}

		// Token: 0x040010B7 RID: 4279
		public const uint FNV32_OFFSET = 2166136261U;

		// Token: 0x040010B8 RID: 4280
		public const uint FNV32_PRIME = 16777619U;

		// Token: 0x040010B9 RID: 4281
		public const ulong FNV64_OFFSET = 14695981039346656037UL;

		// Token: 0x040010BA RID: 4282
		public const ulong FNV64_PRIME = 1099511628211UL;

		// Token: 0x040010BB RID: 4283
		public static readonly SortedSet<int> HASH_CAPACITY = new SortedSet<int>
		{
			3, 7, 11, 17, 23, 29, 37, 47, 59, 71,
			89, 107, 131, 163, 197, 239, 293, 353, 431, 521,
			631, 761, 919, 1103, 1327, 1597, 1931, 2333, 2801, 3371,
			4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591, 17519, 21023,
			25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363,
			156437, 187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403,
			968897, 1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559,
			5999471, 7199369, 7999379, 8499377, 8999371
		};

		// Token: 0x040010BC RID: 4284
		public static readonly int[] HASH_POWERS = new int[]
		{
			13, 31, 61, 127, 251, 509, 1021, 2039, 4093, 8191,
			16381, 32749, 65521, 131071, 262139, 524287, 1048573
		};
	}
}
