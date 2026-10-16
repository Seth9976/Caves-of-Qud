using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Represents information about the route and virtual path that are the result of generating a URL with the ASP.NET routing framework.</summary>
	// Token: 0x020004F9 RID: 1273
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class VirtualPathData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.VirtualPathData" /> class. </summary>
		/// <param name="route">The object that is used to generate the URL.</param>
		/// <param name="virtualPath">The generated URL.</param>
		// Token: 0x060038D9 RID: 14553 RVA: 0x0009727D File Offset: 0x0009547D
		public VirtualPathData(RouteBase route, string virtualPath)
		{
			this.Route = route;
			this.VirtualPath = virtualPath;
		}

		/// <summary>Gets the collection of custom values for the route definition.</summary>
		/// <returns>A collection of custom values for a route.</returns>
		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x0009729E File Offset: 0x0009549E
		public RouteValueDictionary DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		/// <summary>Gets or sets the route that is used to create the URL.</summary>
		/// <returns>An object that represents the route that matched the parameters that were used to generate a URL.</returns>
		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000972A6 File Offset: 0x000954A6
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x000972AE File Offset: 0x000954AE
		public RouteBase Route { get; set; }

		/// <summary>Gets or sets the URL that was created from the route definition.</summary>
		/// <returns>The URL that was generated from a route.</returns>
		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000972B7 File Offset: 0x000954B7
		// (set) Token: 0x060038DE RID: 14558 RVA: 0x000972C8 File Offset: 0x000954C8
		public string VirtualPath
		{
			get
			{
				return this._virtualPath ?? string.Empty;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x04001F02 RID: 7938
		private string _virtualPath;

		// Token: 0x04001F03 RID: 7939
		private RouteValueDictionary _dataTokens = new RouteValueDictionary();
	}
}
