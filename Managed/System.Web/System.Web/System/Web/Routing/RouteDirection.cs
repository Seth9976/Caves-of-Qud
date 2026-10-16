using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Indicates whether ASP.NET routing is processing a URL from a client or generating a URL.</summary>
	// Token: 0x020004EF RID: 1263
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public enum RouteDirection
	{
		/// <summary>A URL from a client is being processed.</summary>
		// Token: 0x04001EF8 RID: 7928
		IncomingRequest,
		/// <summary>A URL is being created based on the route definition.</summary>
		// Token: 0x04001EF9 RID: 7929
		UrlGeneration
	}
}
