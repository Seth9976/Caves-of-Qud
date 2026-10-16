using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.Security;

namespace System.Web.Routing
{
	/// <summary>Matches a URL request to a defined route.</summary>
	// Token: 0x020004F8 RID: 1272
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UrlRoutingModule : IHttpModule
	{
		/// <summary>Gets or sets the collection of defined routes for the ASP.NET application.</summary>
		/// <returns>An object that contains the routes.</returns>
		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x000970F0 File Offset: 0x000952F0
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x0009710B File Offset: 0x0009530B
		public RouteCollection RouteCollection
		{
			get
			{
				if (this._routeCollection == null)
				{
					this._routeCollection = RouteTable.Routes;
				}
				return this._routeCollection;
			}
			set
			{
				this._routeCollection = value;
			}
		}

		/// <summary>Disposes of the resources (other than memory) that are used by the module.</summary>
		// Token: 0x060038D0 RID: 14544 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void Dispose()
		{
		}

		/// <summary>Initializes a module and prepares it to handle requests.</summary>
		/// <param name="application">An object that provides access to the methods, properties, and events common to all application objects in an ASP.NET application.</param>
		// Token: 0x060038D1 RID: 14545 RVA: 0x00097114 File Offset: 0x00095314
		protected virtual void Init(HttpApplication application)
		{
			if (application.Context.Items[UrlRoutingModule._contextKey] != null)
			{
				return;
			}
			application.Context.Items[UrlRoutingModule._contextKey] = UrlRoutingModule._contextKey;
			application.PostResolveRequestCache += this.OnApplicationPostResolveRequestCache;
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x00097168 File Offset: 0x00095368
		private void OnApplicationPostResolveRequestCache(object sender, EventArgs e)
		{
			HttpContextBase httpContextBase = new HttpContextWrapper(((HttpApplication)sender).Context);
			this.PostResolveRequestCache(httpContextBase);
		}

		/// <summary>Assigns the HTTP handler for the current request to the context.</summary>
		/// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Routing.RouteData.RouteHandler" /> property for the route is null.</exception>
		// Token: 0x060038D3 RID: 14547 RVA: 0x00003946 File Offset: 0x00001B46
		[Obsolete("This method is obsolete. Override the Init method to use the PostMapRequestHandler event.")]
		public virtual void PostMapRequestHandler(HttpContextBase context)
		{
		}

		/// <summary>Matches the HTTP request to a route, retrieves the handler for that route, and sets the handler as the HTTP handler for the current request.</summary>
		/// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
		// Token: 0x060038D4 RID: 14548 RVA: 0x00097190 File Offset: 0x00095390
		public virtual void PostResolveRequestCache(HttpContextBase context)
		{
			RouteData routeData = this.RouteCollection.GetRouteData(context);
			if (routeData == null)
			{
				return;
			}
			IRouteHandler routeHandler = routeData.RouteHandler;
			if (routeHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("A RouteHandler must be specified for the selected route."), Array.Empty<object>()));
			}
			if (routeHandler is StopRoutingHandler)
			{
				return;
			}
			RequestContext requestContext = new RequestContext(context, routeData);
			context.Request.RequestContext = requestContext;
			IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
			if (httpHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method."), routeHandler.GetType()));
			}
			if (!(httpHandler is UrlAuthFailureHandler))
			{
				context.RemapHandler(httpHandler);
				return;
			}
			if (FormsAuthenticationModule.FormsAuthRequired)
			{
				UrlAuthorizationModule.ReportUrlAuthorizationFailure(HttpContext.Current, this);
				return;
			}
			throw new HttpException(401, global::SR.GetString("An error occurred while accessing the resources required to serve this request. You might not have permission to view the requested resources."));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpModule.Dispose" />.</summary>
		// Token: 0x060038D5 RID: 14549 RVA: 0x00097256 File Offset: 0x00095456
		void IHttpModule.Dispose()
		{
			this.Dispose();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpModule.Init" />.</summary>
		/// <param name="application">An object that provides access to the methods, properties, and events that are common to all application objects in an ASP.NET application.</param>
		// Token: 0x060038D6 RID: 14550 RVA: 0x0009725E File Offset: 0x0009545E
		void IHttpModule.Init(HttpApplication application)
		{
			this.Init(application);
		}

		// Token: 0x04001EFF RID: 7935
		private static readonly object _contextKey = new object();

		// Token: 0x04001F00 RID: 7936
		private static readonly object _requestDataKey = new object();

		// Token: 0x04001F01 RID: 7937
		private RouteCollection _routeCollection;
	}
}
