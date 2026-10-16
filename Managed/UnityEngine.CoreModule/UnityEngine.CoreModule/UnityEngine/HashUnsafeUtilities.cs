using System;

namespace UnityEngine
{
	// Token: 0x02000200 RID: 512
	public static class HashUnsafeUtilities
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x0002FF0F File Offset: 0x0002E10F
		public unsafe static void ComputeHash128(void* data, ulong dataSize, ulong* hash1, ulong* hash2)
		{
			SpookyHash.Hash(data, dataSize, hash1, hash2);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0002FF1C File Offset: 0x0002E11C
		public unsafe static void ComputeHash128(void* data, ulong dataSize, Hash128* hash)
		{
			ulong u64_ = hash->u64_0;
			ulong u64_2 = hash->u64_1;
			HashUnsafeUtilities.ComputeHash128(data, dataSize, &u64_, &u64_2);
			*hash = new Hash128(u64_, u64_2);
		}
	}
}
