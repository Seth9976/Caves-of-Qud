using System;

namespace System.Web.Caching
{
	// Token: 0x0200068A RID: 1674
	internal sealed class InMemoryOutputCacheProvider : OutputCacheProvider
	{
		// Token: 0x0600474A RID: 18250 RVA: 0x000C6728 File Offset: 0x000C4928
		public override object Add(string key, object entry, DateTime utcExpiry)
		{
			return HttpRuntime.InternalCache.Add("@InMemoryOCP_" + key, entry, null, utcExpiry.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x000C674F File Offset: 0x000C494F
		public override object Get(string key)
		{
			return HttpRuntime.InternalCache.Get("@InMemoryOCP_" + key);
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x000C6766 File Offset: 0x000C4966
		public override void Remove(string key)
		{
			HttpRuntime.InternalCache.Remove("@InMemoryOCP_" + key);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x000C6780 File Offset: 0x000C4980
		public override void Set(string key, object entry, DateTime utcExpiry)
		{
			Cache internalCache = HttpRuntime.InternalCache;
			string text = "@InMemoryOCP_" + key;
			if (internalCache.Get(text) != null)
			{
				internalCache.Remove(text);
			}
			internalCache.Add(text, entry, null, utcExpiry.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x040025AF RID: 9647
		private const string CACHE_PREFIX = "@InMemoryOCP_";
	}
}
