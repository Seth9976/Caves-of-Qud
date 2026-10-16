using System;

namespace System.Web.UI
{
	/// <summary>Defines a service implemented by objects to resolve relative URLs based on contextual information.</summary>
	// Token: 0x02000182 RID: 386
	public interface IUrlResolutionService
	{
		/// <summary>Returns a resolved URL that is suitable for use by the client.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the resolved URL.</returns>
		/// <param name="relativeUrl">A URL relative to the current page.</param>
		// Token: 0x06000F5D RID: 3933
		string ResolveClientUrl(string relativeUrl);
	}
}
