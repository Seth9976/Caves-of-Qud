using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000429 RID: 1065
	public struct PooledObject<T> : IDisposable where T : class
	{
		// Token: 0x060029AE RID: 10670 RVA: 0x00050680 File Offset: 0x0004E880
		public PooledObject(T value, IObjectPool<T> pool)
		{
			this.m_ToReturn = value;
			this.m_Pool = pool;
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00050691 File Offset: 0x0004E891
		void IDisposable.Dispose()
		{
			this.m_Pool.Release(this.m_ToReturn);
		}

		// Token: 0x04000BD9 RID: 3033
		private readonly T m_ToReturn;

		// Token: 0x04000BDA RID: 3034
		private readonly IObjectPool<T> m_Pool;
	}
}
