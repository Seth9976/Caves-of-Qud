using System;

namespace UnityEngine.XR
{
	// Token: 0x0200002D RID: 45
	internal static class HashCodeHelper
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00005A58 File Offset: 0x00003C58
		public static int Combine(int hash1, int hash2)
		{
			return hash1 * 486187739 + hash2;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005A74 File Offset: 0x00003C74
		public static int Combine(int hash1, int hash2, int hash3)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2), hash3);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005A83 File Offset: 0x00003C83
		public static int Combine(int hash1, int hash2, int hash3, int hash4)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3), hash4);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005A93 File Offset: 0x00003C93
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4), hash5);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005AA5 File Offset: 0x00003CA5
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5), hash6);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005AB9 File Offset: 0x00003CB9
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6), hash7);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005ACF File Offset: 0x00003CCF
		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7, int hash8)
		{
			return HashCodeHelper.Combine(HashCodeHelper.Combine(hash1, hash2, hash3, hash4, hash5, hash6, hash7), hash8);
		}

		// Token: 0x040000FC RID: 252
		private const int k_HashCodeMultiplier = 486187739;
	}
}
