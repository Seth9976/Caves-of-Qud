using System;

namespace System.Web
{
	/// <summary>Provides data for an event that is raised by calling the <see cref="P:System.Web.SiteMapProvider.CurrentNode" /> property of the <see cref="T:System.Web.SiteMapProvider" /> class. </summary>
	// Token: 0x020000D6 RID: 214
	public class SiteMapResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapResolveEventArgs" /> class using the specified <see cref="T:System.Web.HttpContext" /> and <see cref="T:System.Web.SiteMapProvider" /> objects. </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that represents the context of the current page request.</param>
		/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> that raised the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event.</param>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001E97E File Offset: 0x0001CB7E
		public SiteMapResolveEventArgs(HttpContext context, SiteMapProvider provider)
		{
			this._context = context;
			this._provider = provider;
		}

		/// <summary>Gets the context of the page request that the requested node represents.</summary>
		/// <returns>An <see cref="T:System.Web.HttpContext" />, if one is specified; otherwise, null.</returns>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0001E994 File Offset: 0x0001CB94
		public HttpContext Context
		{
			get
			{
				return this._context;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SiteMapProvider" /> object that raised the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event. </summary>
		/// <returns>The <see cref="T:System.Web.SiteMapProvider" /> that raised the event; otherwise, null, if no provider is specified during the EventArgs object construction.</returns>
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0001E99C File Offset: 0x0001CB9C
		public SiteMapProvider Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x040010AE RID: 4270
		private HttpContext _context;

		// Token: 0x040010AF RID: 4271
		private SiteMapProvider _provider;
	}
}
