using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000424 RID: 1060
	public interface IObjectPool<T> where T : class
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002993 RID: 10643
		int CountInactive { get; }

		// Token: 0x06002994 RID: 10644
		T Get();

		// Token: 0x06002995 RID: 10645
		PooledObject<T> Get(out T v);

		// Token: 0x06002996 RID: 10646
		void Release(T element);

		// Token: 0x06002997 RID: 10647
		void Clear();
	}
}
