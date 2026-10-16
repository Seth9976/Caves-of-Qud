using System;
using System.Configuration.Provider;

namespace System.Web.Caching
{
	/// <summary>Serves as a base class that contains abstract methods for implementing an output-cache provider. </summary>
	// Token: 0x0200068E RID: 1678
	public abstract class OutputCacheProvider : ProviderBase
	{
		/// <summary>Inserts the specified entry into the output cache. </summary>
		/// <returns>A reference to the specified provider. </returns>
		/// <param name="key">A unique identifier for <paramref name="entry" />.</param>
		/// <param name="entry">The content to add to the output cache.</param>
		/// <param name="utcExpiry">The time and date on which the cached entry expires.</param>
		// Token: 0x06004769 RID: 18281
		public abstract object Add(string key, object entry, DateTime utcExpiry);

		/// <summary>Returns a reference to the specified entry in the output cache.</summary>
		/// <returns>The <paramref name="key" /> value that identifies the specified entry in the cache, or null if the specified entry is not in the cache.</returns>
		/// <param name="key">A unique identifier for a cached entry in the output cache. </param>
		// Token: 0x0600476A RID: 18282
		public abstract object Get(string key);

		/// <summary>Removes the specified entry from the output cache.</summary>
		/// <param name="key">The unique identifier for the entry to remove from the output cache. </param>
		// Token: 0x0600476B RID: 18283
		public abstract void Remove(string key);

		/// <summary>Inserts the specified entry into the output cache, overwriting the entry if it is already cached.</summary>
		/// <param name="key">A unique identifier for <paramref name="entry" />.</param>
		/// <param name="entry">The content to add to the output cache.</param>
		/// <param name="utcExpiry">The time and date on which the cached <paramref name="entry" /> expires.</param>
		// Token: 0x0600476C RID: 18284
		public abstract void Set(string key, object entry, DateTime utcExpiry);
	}
}
