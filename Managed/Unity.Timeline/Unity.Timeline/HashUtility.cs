using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000051 RID: 81
	internal static class HashUtility
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000AB74 File Offset: 0x00008D74
		public static int CombineHash(this int h1, int h2)
		{
			return h1 ^ (int)((long)h2 + (long)((ulong)(-1640531527)) + (long)((long)h1 << 6) + (long)(h1 >> 2));
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000AB8C File Offset: 0x00008D8C
		public static int CombineHash(int h1, int h2, int h3)
		{
			return h1.CombineHash(h2).CombineHash(h3);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000AB9B File Offset: 0x00008D9B
		public static int CombineHash(int h1, int h2, int h3, int h4)
		{
			return HashUtility.CombineHash(h1, h2, h3).CombineHash(h4);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000ABAB File Offset: 0x00008DAB
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4).CombineHash(h5);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000ABBD File Offset: 0x00008DBD
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4, h5).CombineHash(h6);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		public static int CombineHash(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return HashUtility.CombineHash(h1, h2, h3, h4, h5, h6).CombineHash(h7);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		public static int CombineHash(int[] hashes)
		{
			if (hashes == null || hashes.Length == 0)
			{
				return 0;
			}
			int num = hashes[0];
			for (int i = 1; i < hashes.Length; i++)
			{
				num = num.CombineHash(hashes[i]);
			}
			return num;
		}
	}
}
