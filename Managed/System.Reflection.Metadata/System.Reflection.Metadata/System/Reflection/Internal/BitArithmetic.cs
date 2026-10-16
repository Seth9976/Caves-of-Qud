using System;

namespace System.Reflection.Internal
{
	// Token: 0x0200017C RID: 380
	internal static class BitArithmetic
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x000230F8 File Offset: 0x000212F8
		internal static int CountBits(int v)
		{
			return BitArithmetic.CountBits((uint)v);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00023100 File Offset: 0x00021300
		internal static int CountBits(uint v)
		{
			v -= (v >> 1) & 1431655765U;
			v = (v & 858993459U) + ((v >> 2) & 858993459U);
			return (int)(((v + (v >> 4)) & 252645135U) * 16843009U) >> 24;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00023138 File Offset: 0x00021338
		internal static int CountBits(ulong v)
		{
			v -= (v >> 1) & 6148914691236517205UL;
			v = (v & 3689348814741910323UL) + ((v >> 2) & 3689348814741910323UL);
			return (int)(((v + (v >> 4)) & 1085102592571150095UL) * 72340172838076673UL >> 56);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00023190 File Offset: 0x00021390
		internal static uint Align(uint position, uint alignment)
		{
			uint num = position & ~(alignment - 1U);
			if (num == position)
			{
				return num;
			}
			return num + alignment;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000231B0 File Offset: 0x000213B0
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
