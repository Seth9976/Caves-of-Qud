using System;

namespace System.Web.Caching
{
	/// <summary>Defines a callback method for notifying applications when a cached item is removed from the <see cref="T:System.Web.Caching.Cache" />.</summary>
	/// <param name="key">The key that is removed from the cache. </param>
	/// <param name="value">The <see cref="T:System.Object" /> item associated with the key removed from the cache. </param>
	/// <param name="reason">The reason the item was removed from the cache, as specified by the <see cref="T:System.Web.Caching.CacheItemRemovedReason" /> enumeration. </param>
	// Token: 0x02000680 RID: 1664
	// (Invoke) Token: 0x0600471E RID: 18206
	public delegate void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason);
}
