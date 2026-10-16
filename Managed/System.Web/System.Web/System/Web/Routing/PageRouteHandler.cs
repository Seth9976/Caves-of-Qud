using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Compilation;
using System.Web.Security;
using System.Web.UI;

namespace System.Web.Routing
{
	/// <summary>Provides properties and methods for defining how a URL maps to a physical file.</summary>
	// Token: 0x020004E0 RID: 1248
	public class PageRouteHandler : IRouteHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.PageRouteHandler" /> class. </summary>
		/// <param name="virtualPath">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="virtualPath" /> parameter is null or is an empty string or does not start with "~/".</exception>
		// Token: 0x06003821 RID: 14369 RVA: 0x00094D24 File Offset: 0x00092F24
		public PageRouteHandler(string virtualPath)
			: this(virtualPath, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.PageRouteHandler" /> class. </summary>
		/// <param name="virtualPath">The virtual path of the physical file of this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
		/// <param name="checkPhysicalUrlAccess">If this property is set to false, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to true, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="virtualPath" /> parameter is null or is an empty string or does not start with "~/".</exception>
		// Token: 0x06003822 RID: 14370 RVA: 0x00094D30 File Offset: 0x00092F30
		public PageRouteHandler(string virtualPath, bool checkPhysicalUrlAccess)
		{
			if (string.IsNullOrEmpty(virtualPath) || !virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(global::SR.GetString("VirtualPath must be a non-empty string starting with ~/."), "virtualPath");
			}
			this.VirtualPath = virtualPath;
			this.CheckPhysicalUrlAccess = checkPhysicalUrlAccess;
			this._useRouteVirtualPath = this.VirtualPath.Contains("{");
		}

		/// <summary>Gets the virtual path of the Web page that is associated with this route.</summary>
		/// <returns>The URL of the Web page, before substitutions have been applied for any replacement parameters.</returns>
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06003823 RID: 14371 RVA: 0x00094D92 File Offset: 0x00092F92
		// (set) Token: 0x06003824 RID: 14372 RVA: 0x00094D9A File Offset: 0x00092F9A
		public string VirtualPath { get; private set; }

		/// <summary>Gets a value that determines whether authorization rules are applied to the physical file's URL.</summary>
		/// <returns>true if authorization is checked for the URL of the physical file that is associated with the route; otherwise, false. The default is true.</returns>
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003825 RID: 14373 RVA: 0x00094DA3 File Offset: 0x00092FA3
		// (set) Token: 0x06003826 RID: 14374 RVA: 0x00094DAB File Offset: 0x00092FAB
		public bool CheckPhysicalUrlAccess { get; private set; }

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06003827 RID: 14375 RVA: 0x00094DB4 File Offset: 0x00092FB4
		private Route RouteVirtualPath
		{
			get
			{
				if (this._routeVirtualPath == null)
				{
					this._routeVirtualPath = new Route(this.VirtualPath.Substring(2), this);
				}
				return this._routeVirtualPath;
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x00094DDC File Offset: 0x00092FDC
		private bool CheckUrlAccess(string virtualPath, RequestContext requestContext)
		{
			IPrincipal principal = requestContext.HttpContext.User;
			if (principal == null)
			{
				principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), new string[0]);
			}
			return this.CheckUrlAccessWithAssert(virtualPath, requestContext, principal);
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x00094E1C File Offset: 0x0009301C
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		private bool CheckUrlAccessWithAssert(string virtualPath, RequestContext requestContext, IPrincipal user)
		{
			return UrlAuthorizationModule.CheckUrlAccessForPrincipal(virtualPath, user, requestContext.HttpContext.Request.HttpMethod);
		}

		/// <summary>Returns the object that processes the request.</summary>
		/// <returns>The object that processes the request.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is null.</exception>
		// Token: 0x0600382A RID: 14378 RVA: 0x00094E38 File Offset: 0x00093038
		public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			string text = this.GetSubstitutedVirtualPath(requestContext);
			int num = text.IndexOf('?');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			if (this.CheckPhysicalUrlAccess && !this.CheckUrlAccess(text, requestContext))
			{
				return new UrlAuthFailureHandler();
			}
			return BuildManager.CreateInstanceFromVirtualPath(text, typeof(Page)) as Page;
		}

		/// <summary>Returns the virtual path of the physical file for the route after substitutions have been applied to any replacement parameters.</summary>
		/// <returns>The URL of the physical file that was generated from a route.</returns>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is null.</exception>
		// Token: 0x0600382B RID: 14379 RVA: 0x00094EA0 File Offset: 0x000930A0
		public string GetSubstitutedVirtualPath(RequestContext requestContext)
		{
			if (requestContext == null)
			{
				throw new ArgumentNullException("requestContext");
			}
			if (!this._useRouteVirtualPath)
			{
				return this.VirtualPath;
			}
			VirtualPathData virtualPath = this.RouteVirtualPath.GetVirtualPath(requestContext, requestContext.RouteData.Values);
			if (virtualPath == null)
			{
				return this.VirtualPath;
			}
			return "~/" + virtualPath.VirtualPath;
		}

		// Token: 0x04001ED4 RID: 7892
		private bool _useRouteVirtualPath;

		// Token: 0x04001ED5 RID: 7893
		private Route _routeVirtualPath;
	}
}
