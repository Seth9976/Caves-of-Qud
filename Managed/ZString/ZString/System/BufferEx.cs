using System;

namespace System
{
	// Token: 0x02000006 RID: 6
	internal static class BufferEx
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		internal unsafe static void ZeroMemory(byte* dest, uint len)
		{
			if (len == 0U)
			{
				return;
			}
			int num = 0;
			while ((long)num < (long)((ulong)len))
			{
				dest[num] = 0;
				num++;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002124 File Offset: 0x00000324
		internal unsafe static void Memcpy(byte* dest, byte* src, int len)
		{
			if (len == 0)
			{
				return;
			}
			for (int i = 0; i < len; i++)
			{
				dest[i] = src[i];
			}
		}
	}
}
