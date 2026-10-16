using System;

namespace System.Reflection.Internal
{
	// Token: 0x02000170 RID: 368
	internal static class DecimalUtilities
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x00022B0E File Offset: 0x00020D0E
		public static int GetScale(this decimal value)
		{
			return (int)((byte)(decimal.GetBits(value)[3] >> 16));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00022B1C File Offset: 0x00020D1C
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
