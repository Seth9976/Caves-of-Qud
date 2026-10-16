using System;
using System.Collections.Generic;

namespace UnityEngine.Pool
{
	// Token: 0x0200041D RID: 1053
	public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new()
	{
		// Token: 0x0600297F RID: 10623 RVA: 0x0004FFF5 File Offset: 0x0004E1F5
		public static TCollection Get()
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get();
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00050001 File Offset: 0x0004E201
		public static PooledObject<TCollection> Get(out TCollection value)
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get(out value);
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0005000E File Offset: 0x0004E20E
		public static void Release(TCollection toRelease)
		{
			CollectionPool<TCollection, TItem>.s_Pool.Release(toRelease);
		}

		// Token: 0x04000BC1 RID: 3009
		internal static readonly ObjectPool<TCollection> s_Pool = new ObjectPool<TCollection>(() => new TCollection(), null, delegate(TCollection l)
		{
			l.Clear();
		}, null, true, 10, 10000);
	}
}
