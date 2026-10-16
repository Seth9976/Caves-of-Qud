using System;

namespace System.Web.Caching
{
	/// <summary>Specifies the relative priority of items stored in the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
	// Token: 0x0200067D RID: 1661
	public enum CacheItemPriority
	{
		/// <summary>Cache items with this priority level are the most likely to be deleted from the cache as the server frees system memory.</summary>
		// Token: 0x04002580 RID: 9600
		Low = 1,
		/// <summary>Cache items with this priority level are more likely to be deleted from the cache as the server frees system memory than items assigned a <see cref="F:System.Web.Caching.CacheItemPriority.Normal" /> priority.</summary>
		// Token: 0x04002581 RID: 9601
		BelowNormal,
		/// <summary>Cache items with this priority level are likely to be deleted from the cache as the server frees system memory only after those items with <see cref="F:System.Web.Caching.CacheItemPriority.Low" /> or <see cref="F:System.Web.Caching.CacheItemPriority.BelowNormal" /> priority. This is the default.</summary>
		// Token: 0x04002582 RID: 9602
		Normal,
		/// <summary>The default value for a cached item's priority is <see cref="F:System.Web.Caching.CacheItemPriority.Normal" />.</summary>
		// Token: 0x04002583 RID: 9603
		Default = 3,
		/// <summary>Cache items with this priority level are less likely to be deleted as the server frees system memory than those assigned a <see cref="F:System.Web.Caching.CacheItemPriority.Normal" /> priority.</summary>
		// Token: 0x04002584 RID: 9604
		AboveNormal,
		/// <summary>Cache items with this priority level are the least likely to be deleted from the cache as the server frees system memory.</summary>
		// Token: 0x04002585 RID: 9605
		High,
		/// <summary>The cache items with this priority level will not be automatically deleted from the cache as the server frees system memory. However, items with this priority level are removed along with other items according to the item's absolute or sliding expiration time. </summary>
		// Token: 0x04002586 RID: 9606
		NotRemovable
	}
}
