using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Enables you to define which HTTP verbs are allowed when ASP.NET routing determines whether a URL matches a route.</summary>
	// Token: 0x020004DA RID: 1242
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpMethodConstraint : IRouteConstraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.HttpMethodConstraint" /> class by using the HTTP verbs that are allowed for the route. </summary>
		/// <param name="allowedMethods">The HTTP verbs that are valid for the route.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="allowedMethods" /> parameter is null.</exception>
		// Token: 0x06003813 RID: 14355 RVA: 0x00094BB6 File Offset: 0x00092DB6
		public HttpMethodConstraint(params string[] allowedMethods)
		{
			if (allowedMethods == null)
			{
				throw new ArgumentNullException("allowedMethods");
			}
			this.AllowedMethods = allowedMethods.ToList<string>().AsReadOnly();
		}

		/// <summary>Gets the collection of allowed HTTP verbs for the route.</summary>
		/// <returns>A collection of allowed HTTP verbs for the route.</returns>
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x00094BDD File Offset: 0x00092DDD
		// (set) Token: 0x06003815 RID: 14357 RVA: 0x00094BE5 File Offset: 0x00092DE5
		public ICollection<string> AllowedMethods { get; private set; }

		/// <summary>Determines whether the request was made with an HTTP verb that is one of the allowed verbs for the route.</summary>
		/// <returns>When ASP.NET routing is processing a request, true if the request was made by using an allowed HTTP verb; otherwise, false. When ASP.NET routing is constructing a URL, true if the supplied values contain an HTTP verb that matches one of the allowed HTTP verbs; otherwise, false. The default is true.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <param name="route">The object that is being checked to determine whether it matches the URL.</param>
		/// <param name="parameterName">The name of the parameter that is being checked.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		/// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is processed or when a URL is generated.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the following parameters is null: <paramref name="httpContext" />, <paramref name="route" />, <paramref name="parameterName" />, or <paramref name="values" />.</exception>
		// Token: 0x06003816 RID: 14358 RVA: 0x00094BF0 File Offset: 0x00092DF0
		protected virtual bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (route == null)
			{
				throw new ArgumentNullException("route");
			}
			if (parameterName == null)
			{
				throw new ArgumentNullException("parameterName");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			if (routeDirection == RouteDirection.IncomingRequest)
			{
				return this.AllowedMethods.Any<string>((string method) => string.Equals(method, httpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase));
			}
			if (routeDirection != RouteDirection.UrlGeneration)
			{
				return true;
			}
			object obj;
			if (!values.TryGetValue(parameterName, out obj))
			{
				return true;
			}
			string parameterValueString = obj as string;
			if (parameterValueString == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The constraint for route parameter '{0}' on the route with URL '{1}' must have a string value in order to use an HttpMethodConstraint."), parameterName, route.Url));
			}
			return this.AllowedMethods.Any<string>((string method) => string.Equals(method, parameterValueString, StringComparison.OrdinalIgnoreCase));
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.Routing.IRouteConstraint.Match(System.Web.HttpContextBase,System.Web.Routing.Route,System.String,System.Web.Routing.RouteValueDictionary,System.Web.Routing.RouteDirection)" />. </summary>
		/// <returns>true if the request was made by using an allowed HTTP verb; otherwise, false. The default is true.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <param name="route">The object that is being checked to determine whether it matches the URL.</param>
		/// <param name="parameterName">The name of the parameter that is being checked.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		/// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is handled or when a URL is generated.</param>
		/// <exception cref="T:System.ArgumentNullException">One or more of the following parameters is null: <paramref name="httpContext" />, <paramref name="route" />, <paramref name="parameterName" />, or <paramref name="values" />.</exception>
		// Token: 0x06003817 RID: 14359 RVA: 0x00094CCD File Offset: 0x00092ECD
		bool IRouteConstraint.Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			return this.Match(httpContext, route, parameterName, values, routeDirection);
		}
	}
}
