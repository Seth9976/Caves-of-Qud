using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000422 RID: 1058
	public class GenericPool<T> where T : class, new()
	{
		// Token: 0x0600298B RID: 10635 RVA: 0x0005008E File Offset: 0x0004E28E
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0005009A File Offset: 0x0004E29A
		public static PooledObject<T> Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000500A7 File Offset: 0x0004E2A7
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x04000BC3 RID: 3011
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), null, null, null, true, 10, 10000);
	}
}
