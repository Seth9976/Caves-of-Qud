using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Encapsulates information about an HTTP request that matches a defined route.</summary>
	// Token: 0x020004E7 RID: 1255
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RequestContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RequestContext" /> class.</summary>
		// Token: 0x06003848 RID: 14408 RVA: 0x00002050 File Offset: 0x00000250
		public RequestContext()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RequestContext" /> class. </summary>
		/// <param name="httpContext">An object that contains information about the HTTP request.</param>
		/// <param name="routeData">An object that contains information about the route that matched the current request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="httpContext" /> or <paramref name="routeData" /> is null.</exception>
		// Token: 0x06003849 RID: 14409 RVA: 0x00095C10 File Offset: 0x00093E10
		public RequestContext(HttpContextBase httpContext, RouteData routeData)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (routeData == null)
			{
				throw new ArgumentNullException("routeData");
			}
			this.HttpContext = httpContext;
			this.RouteData = routeData;
		}

		/// <summary>Gets information about the HTTP request.</summary>
		/// <returns>An object that contains information about the HTTP request.</returns>
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x00095C42 File Offset: 0x00093E42
		// (set) Token: 0x0600384B RID: 14411 RVA: 0x00095C4A File Offset: 0x00093E4A
		public virtual HttpContextBase HttpContext { get; set; }

		/// <summary>Gets information about the requested route.</summary>
		/// <returns>An object that contains information about the requested route.</returns>
		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x00095C53 File Offset: 0x00093E53
		// (set) Token: 0x0600384D RID: 14413 RVA: 0x00095C5B File Offset: 0x00093E5B
		public virtual RouteData RouteData { get; set; }
	}
}
