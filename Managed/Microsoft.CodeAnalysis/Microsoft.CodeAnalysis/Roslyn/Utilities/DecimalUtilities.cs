using System;

namespace Roslyn.Utilities
{
	// Token: 0x02000039 RID: 57
	internal static class DecimalUtilities
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00004C3A File Offset: 0x00002E3A
		public static int GetScale(this decimal value)
		{
			return (int)((byte)(decimal.GetBits(value)[3] >> 16));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004C48 File Offset: 0x00002E48
		public static void GetBits(this decimal value, out bool isNegative, out byte scale, out uint low, out uint mid, out uint high)
		{
			int[] bits = decimal.GetBits(value);
			low = (uint)bits[0];
			mid = (uint)bits[1];
			high = (uint)bits[2];
			scale = (byte)(bits[3] >> 16);
			isNegative = ((long)bits[3] & (long)((ulong)int.MinValue)) != 0L;
		}
	}
}
