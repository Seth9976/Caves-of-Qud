using System;
using System.Collections.Generic;

namespace System.Web.Caching
{
	/// <summary>Defines collections of HTTP header and response elements that together make up one kind of output-cached data that ASP.NET can pass to a provider. </summary>
	// Token: 0x02000672 RID: 1650
	public interface IOutputCacheEntry
	{
		/// <summary>Gets the collection of HTTP header elements in an output-cache entry.</summary>
		/// <returns>A list of HTTP header elements.</returns>
		// Token: 0x170015EF RID: 5615
		// (get) Token: 0x0600469B RID: 18075
		// (set) Token: 0x0600469C RID: 18076
		List<HeaderElement> HeaderElements { get; set; }

		/// <summary>Gets the collection of HTTP response elements in an output-cache entry.</summary>
		/// <returns>A list of HTTP response elements.</returns>
		// Token: 0x170015F0 RID: 5616
		// (get) Token: 0x0600469D RID: 18077
		// (set) Token: 0x0600469E RID: 18078
		List<ResponseElement> ResponseElements { get; set; }
	}
}
