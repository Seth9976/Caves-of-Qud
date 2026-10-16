using System;

namespace Roslyn.Utilities
{
	// Token: 0x0200002F RID: 47
	internal static class BitArithmeticUtilities
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004440 File Offset: 0x00002640
		public static int CountBits(int v)
		{
			return BitArithmeticUtilities.CountBits((uint)v);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004448 File Offset: 0x00002648
		public static int CountBits(uint v)
		{
			v -= (v >> 1) & 1431655765U;
			v = (v & 858993459U) + ((v >> 2) & 858993459U);
			return (int)(((v + (v >> 4)) & 252645135U) * 16843009U) >> 24;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000447E File Offset: 0x0000267E
		public static int CountBits(long v)
		{
			return BitArithmeticUtilities.CountBits((ulong)v);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004488 File Offset: 0x00002688
		public static int CountBits(ulong v)
		{
			v = (v & 6148914691236517205UL) + ((v >> 1) & 6148914691236517205UL);
			v = (v & 3689348814741910323UL) + ((v >> 2) & 3689348814741910323UL);
			v = (v & 1085102592571150095UL) + ((v >> 4) & 1085102592571150095UL);
			v = (v & 71777214294589695UL) + ((v >> 8) & 71777214294589695UL);
			v = (v & 281470681808895UL) + ((v >> 16) & 281470681808895UL);
			v = (v & (ulong)(-1)) + ((v >> 32) & (ulong)(-1));
			return (int)v;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004530 File Offset: 0x00002730
		internal static uint Align(uint position, uint alignment)
		{
			uint num = position & ~(alignment - 1U);
			if (num == position)
			{
				return num;
			}
			return num + alignment;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004550 File Offset: 0x00002750
		internal static int Align(int position, int alignment)
		{
			int num = position & ~(alignment - 1);
			if (num == position)
			{
				return num;
			}
			return num + alignment;
		}
	}
}
