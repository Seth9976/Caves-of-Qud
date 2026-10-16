using System;

namespace System.Web.Caching
{
	/// <summary>Specifies the reason an item was removed from the <see cref="T:System.Web.Caching.Cache" />.</summary>
	// Token: 0x02000681 RID: 1665
	public enum CacheItemRemovedReason
	{
		/// <summary>The item is removed from the cache by a <see cref="M:System.Web.Caching.Cache.Remove(System.String)" /> method call or by an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> method call that specified the same key.</summary>
		// Token: 0x04002594 RID: 9620
		Removed = 1,
		/// <summary>The item is removed from the cache because it expired.</summary>
		// Token: 0x04002595 RID: 9621
		Expired,
		/// <summary>The item is removed from the cache because the system removed it to free memory.</summary>
		// Token: 0x04002596 RID: 9622
		Underused,
		/// <summary>The item is removed from the cache because the cache dependency associated with it changed.</summary>
		// Token: 0x04002597 RID: 9623
		DependencyChanged
	}
}
