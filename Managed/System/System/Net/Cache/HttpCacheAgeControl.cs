using System;

namespace System.Net.Cache
{
	/// <summary>Specifies the meaning of time values that control caching behavior for resources obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x0200078E RID: 1934
	public enum HttpCacheAgeControl
	{
		/// <summary>For internal use only. The Framework will throw an <see cref="T:System.ArgumentException" /> if you try to use this member.</summary>
		// Token: 0x04002420 RID: 9248
		None,
		/// <summary>Content can be taken from the cache if the time remaining before expiration is greater than or equal to the time specified with this value.</summary>
		// Token: 0x04002421 RID: 9249
		MinFresh,
		/// <summary>Content can be taken from the cache until it is older than the age specified with this value.</summary>
		// Token: 0x04002422 RID: 9250
		MaxAge,
		/// <summary>Content can be taken from the cache after it has expired, until the time specified with this value elapses.</summary>
		// Token: 0x04002423 RID: 9251
		MaxStale = 4,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MinFresh" />.</summary>
		// Token: 0x04002424 RID: 9252
		MaxAgeAndMinFresh = 3,
		/// <summary>
		///   <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxAge" /> and <see cref="P:System.Net.Cache.HttpRequestCachePolicy.MaxStale" />.</summary>
		// Token: 0x04002425 RID: 9253
		MaxAgeAndMaxStale = 6
	}
}
