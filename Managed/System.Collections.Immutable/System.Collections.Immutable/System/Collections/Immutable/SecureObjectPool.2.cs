using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000045 RID: 69
	[NullableContext(1)]
	[Nullable(0)]
	internal class SecureObjectPool<[Nullable(2)] T, [Nullable(0)] TCaller> where TCaller : ISecurePooledObjectUser
	{
		// Token: 0x06000372 RID: 882 RVA: 0x00009259 File Offset: 0x00007459
		public void TryAdd(TCaller caller, SecurePooledObject<T> item)
		{
			if (caller.PoolUserId == item.Owner)
			{
				item.Owner = -1;
				AllocFreeConcurrentStack<SecurePooledObject<T>>.TryAdd(item);
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000927D File Offset: 0x0000747D
		public bool TryTake(TCaller caller, [Nullable(new byte[] { 2, 1 })] out SecurePooledObject<T> item)
		{
			if (caller.PoolUserId != -1 && AllocFreeConcurrentStack<SecurePooledObject<T>>.TryTake(out item))
			{
				item.Owner = caller.PoolUserId;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000092B4 File Offset: 0x000074B4
		public SecurePooledObject<T> PrepNew(TCaller caller, T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			return new SecurePooledObject<T>(newValue)
			{
				Owner = caller.PoolUserId
			};
		}
	}
}
