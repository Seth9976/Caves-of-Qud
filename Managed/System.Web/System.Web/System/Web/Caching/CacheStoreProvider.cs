using System;
using System.Collections;
using System.Configuration.Provider;
using Unity;

namespace System.Web.Caching
{
	// Token: 0x02000780 RID: 1920
	public abstract class CacheStoreProvider : ProviderBase, IDisposable
	{
		// Token: 0x06004E00 RID: 19968 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected CacheStoreProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06004E01 RID: 19969
		public abstract long ItemCount { get; }

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06004E02 RID: 19970
		public abstract long SizeInBytes { get; }

		// Token: 0x06004E03 RID: 19971
		public abstract object Add(string key, object item, CacheInsertOptions options);

		// Token: 0x06004E04 RID: 19972
		public abstract bool AddDependent(string key, CacheDependency dependency, out DateTime utcLastUpdated);

		// Token: 0x06004E05 RID: 19973
		public abstract void Dispose();

		// Token: 0x06004E06 RID: 19974
		public abstract object Get(string key);

		// Token: 0x06004E07 RID: 19975
		public abstract IDictionaryEnumerator GetEnumerator();

		// Token: 0x06004E08 RID: 19976
		public abstract void Insert(string key, object item, CacheInsertOptions options);

		// Token: 0x06004E09 RID: 19977
		public abstract object Remove(string key);

		// Token: 0x06004E0A RID: 19978
		public abstract object Remove(string key, CacheItemRemovedReason reason);

		// Token: 0x06004E0B RID: 19979
		public abstract void RemoveDependent(string key, CacheDependency dependency);

		// Token: 0x06004E0C RID: 19980
		public abstract long Trim(int percent);
	}
}
