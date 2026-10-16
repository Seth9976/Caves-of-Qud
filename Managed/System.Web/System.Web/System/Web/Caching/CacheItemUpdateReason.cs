using System;

namespace System.Web.Caching
{
	/// <summary>Specifies the reason that a cached item is being removed from the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
	// Token: 0x02000683 RID: 1667
	public enum CacheItemUpdateReason
	{
		/// <summary>Specifies that the item is being removed from the cache because the absolute or sliding expiration interval expired.</summary>
		// Token: 0x04002599 RID: 9625
		Expired = 1,
		/// <summary>Specifies that the item is being removed from the cache because the associated <see cref="T:System.Web.Caching.CacheDependency" /> object changed.</summary>
		// Token: 0x0400259A RID: 9626
		DependencyChanged
	}
}
