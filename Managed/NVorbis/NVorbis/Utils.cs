using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NVorbis
{
	// Token: 0x02000018 RID: 24
	internal static class Utils
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x0000639C File Offset: 0x0000459C
		internal static int ilog(int x)
		{
			int num = 0;
			while (x > 0)
			{
				num++;
				x >>= 1;
			}
			return num;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000063BB File Offset: 0x000045BB
		internal static uint BitReverse(uint n)
		{
			return Utils.BitReverse(n, 32);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000063C8 File Offset: 0x000045C8
		internal static uint BitReverse(uint n, int bits)
		{
			n = ((n & 2863311530U) >> 1) | ((n & 1431655765U) << 1);
			n = ((n & 3435973836U) >> 2) | ((n & 858993459U) << 2);
			n = ((n & 4042322160U) >> 4) | ((n & 252645135U) << 4);
			n = ((n & 4278255360U) >> 8) | ((n & 16711935U) << 8);
			return ((n >> 16) | (n << 16)) >> 32 - bits;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000643C File Offset: 0x0000463C
		internal static float ClipValue(float value, ref bool clipped)
		{
			Utils.FloatBits floatBits;
			floatBits.Bits = 0U;
			floatBits.Float = value;
			if ((floatBits.Bits & 2147483647U) > 1065353215U)
			{
				clipped = true;
				floatBits.Bits = 1065353215U | (floatBits.Bits & 2147483648U);
			}
			return floatBits.Float;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006494 File Offset: 0x00004694
		internal static float ConvertFromVorbisFloat32(uint bits)
		{
			int num = (int)bits >> 31;
			double num2 = (double)(((bits & 2145386496U) >> 21) - 788U);
			float num3 = (float)(((ulong)(bits & 2097151U) ^ (ulong)((long)num)) + (ulong)((long)(num & 1)));
			return num3 * (float)Math.Pow(2.0, num2);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000064DC File Offset: 0x000046DC
		internal static int Sum(Queue<int> queue)
		{
			int num = 0;
			for (int i = 0; i < queue.Count; i++)
			{
				int num2 = queue.Dequeue();
				num += num2;
				queue.Enqueue(num2);
			}
			return num;
		}

		// Token: 0x02000019 RID: 25
		[StructLayout(2)]
		private struct FloatBits
		{
			// Token: 0x04000078 RID: 120
			[FieldOffset(0)]
			public float Float;

			// Token: 0x04000079 RID: 121
			[FieldOffset(0)]
			public uint Bits;
		}
	}
}
