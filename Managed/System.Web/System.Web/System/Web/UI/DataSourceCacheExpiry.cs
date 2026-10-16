using System;

namespace System.Web.UI
{
	/// <summary>Describes the way data cached using ASP.NET caching mechanisms expires when a time-out is set. </summary>
	// Token: 0x02000156 RID: 342
	public enum DataSourceCacheExpiry
	{
		/// <summary>Cached data expires when the amount of time specified by the CacheDuration property has passed since the data was first cached.</summary>
		// Token: 0x0400122C RID: 4652
		Absolute,
		/// <summary>Cached data expires only when the cache entry has not been used for the amount of time specified by the CacheDuration property.</summary>
		// Token: 0x0400122D RID: 4653
		Sliding
	}
}
