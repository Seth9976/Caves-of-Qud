using System;

namespace UnityEngine.Pool
{
	// Token: 0x0200042B RID: 1067
	public static class UnsafeGenericPool<T> where T : class, new()
	{
		// Token: 0x060029B3 RID: 10675 RVA: 0x00050725 File Offset: 0x0004E925
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x00050731 File Offset: 0x0004E931
		public static PooledObject<T> Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x0005073E File Offset: 0x0004E93E
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x04000BDC RID: 3036
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), null, null, null, false, 10, 10000);
	}
}
