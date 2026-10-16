using System;
using System.Threading.Tasks;
using Unity;

namespace System.Web.Caching
{
	// Token: 0x02000781 RID: 1921
	public abstract class OutputCacheProviderAsync : OutputCacheProvider
	{
		// Token: 0x06004E0D RID: 19981 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected OutputCacheProviderAsync()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004E0E RID: 19982
		public abstract Task<object> AddAsync(string key, object entry, DateTime utcExpiry);

		// Token: 0x06004E0F RID: 19983
		public abstract Task<object> GetAsync(string key);

		// Token: 0x06004E10 RID: 19984
		public abstract Task RemoveAsync(string key);

		// Token: 0x06004E11 RID: 19985
		public abstract Task SetAsync(string key, object entry, DateTime utcExpiry);
	}
}
