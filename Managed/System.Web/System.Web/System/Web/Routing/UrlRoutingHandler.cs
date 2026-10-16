using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Serves as base class for classes that enable you to customize how ASP.NET routing processes a request.</summary>
	// Token: 0x020004F7 RID: 1271
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class UrlRoutingHandler : IHttpHandler
	{
		/// <summary>Gets a value that indicates whether another request can use the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> instance.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the collection of defined routes for the ASP.NET application.</summary>
		/// <returns>An object that contains the routes.</returns>
		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x00097023 File Offset: 0x00095223
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x0009703E File Offset: 0x0009523E
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

		/// <summary>Processes an HTTP request that matches a route.</summary>
		/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		/// <exception cref="T:System.Web.HttpException">The request does not match any route.</exception>
		/// <exception cref="T:System.InvalidOperationException">No handler is defined for the route.</exception>
		// Token: 0x060038C8 RID: 14536 RVA: 0x00097047 File Offset: 0x00095247
		protected virtual void ProcessRequest(HttpContext httpContext)
		{
			this.ProcessRequest(new HttpContextWrapper(httpContext));
		}

		/// <summary>Processes an HTTP request that matches a route.</summary>
		/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		/// <exception cref="T:System.Web.HttpException">The request does not match any route.</exception>
		/// <exception cref="T:System.InvalidOperationException">No handler is defined for the route.</exception>
		// Token: 0x060038C9 RID: 14537 RVA: 0x00097058 File Offset: 0x00095258
		protected virtual void ProcessRequest(HttpContextBase httpContext)
		{
			RouteData routeData = this.RouteCollection.GetRouteData(httpContext);
			if (routeData == null)
			{
				throw new HttpException(404, global::SR.GetString("The incoming request does not match any route."));
			}
			IRouteHandler routeHandler = routeData.RouteHandler;
			if (routeHandler == null)
			{
				throw new InvalidOperationException(global::SR.GetString("A RouteHandler must be specified for the selected route."));
			}
			RequestContext requestContext = new RequestContext(httpContext, routeData);
			IHttpHandler httpHandler = routeHandler.GetHttpHandler(requestContext);
			if (httpHandler == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The route handler '{0}' did not return an IHttpHandler from its GetHttpHandler() method."), routeHandler.GetType()));
			}
			this.VerifyAndProcessRequest(httpHandler, httpContext);
		}

		/// <summary>When overridden in a derived class, validates the HTTP handler and performs the steps that are required to process the request.</summary>
		/// <param name="httpHandler">The object that is used to process an HTTP request.</param>
		/// <param name="httpContext">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		// Token: 0x060038CA RID: 14538
		protected abstract void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext);

		/// <summary>Gets a value that indicates whether another request can use the <see cref="T:System.Web.Routing.UrlRoutingHandler" /> instance.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060038CB RID: 14539 RVA: 0x000970DF File Offset: 0x000952DF
		bool IHttpHandler.IsReusable
		{
			get
			{
				return this.IsReusable;
			}
		}

		/// <summary>Processes an HTTP request that matches a route.</summary>
		/// <param name="context">An object that provides references to the intrinsic server objects (for example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />).</param>
		// Token: 0x060038CC RID: 14540 RVA: 0x000970E7 File Offset: 0x000952E7
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.ProcessRequest(context);
		}

		// Token: 0x04001EFE RID: 7934
		private RouteCollection _routeCollection;
	}
}
