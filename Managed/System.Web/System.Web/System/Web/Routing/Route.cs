using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace System.Web.Routing
{
	/// <summary>Provides properties and methods for defining a route and for obtaining information about the route.</summary>
	// Token: 0x020004E8 RID: 1256
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class Route : RouteBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x0600384E RID: 14414 RVA: 0x00095C64 File Offset: 0x00093E64
		public Route(string url, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x0600384F RID: 14415 RVA: 0x00095C7A File Offset: 0x00093E7A
		public Route(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, constraints, and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
		/// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x06003850 RID: 14416 RVA: 0x00095C97 File Offset: 0x00093E97
		public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.Constraints = constraints;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, constraints, custom values, and handler class. </summary>
		/// <param name="url">The URL pattern for the route.</param>
		/// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
		/// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
		/// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
		/// <param name="routeHandler">The object that processes requests for the route.</param>
		// Token: 0x06003851 RID: 14417 RVA: 0x00095CBC File Offset: 0x00093EBC
		public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
		{
			this.Url = url;
			this.Defaults = defaults;
			this.Constraints = constraints;
			this.DataTokens = dataTokens;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Gets or sets a dictionary of expressions that specify valid values for a URL parameter.</summary>
		/// <returns>An object that contains the parameter names and expressions.</returns>
		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x00095CE9 File Offset: 0x00093EE9
		// (set) Token: 0x06003853 RID: 14419 RVA: 0x00095CF1 File Offset: 0x00093EF1
		public RouteValueDictionary Constraints { get; set; }

		/// <summary>Gets or sets custom values that are passed to the route handler, but which are not used to determine whether the route matches a URL pattern.</summary>
		/// <returns>An object that contains custom values.</returns>
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x00095CFA File Offset: 0x00093EFA
		// (set) Token: 0x06003855 RID: 14421 RVA: 0x00095D02 File Offset: 0x00093F02
		public RouteValueDictionary DataTokens { get; set; }

		/// <summary>Gets or sets the values to use if the URL does not contain all the parameters.</summary>
		/// <returns>An object that contains the parameter names and default values.</returns>
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x00095D0B File Offset: 0x00093F0B
		// (set) Token: 0x06003857 RID: 14423 RVA: 0x00095D13 File Offset: 0x00093F13
		public RouteValueDictionary Defaults { get; set; }

		/// <summary>Gets or sets the object that processes requests for the route.</summary>
		/// <returns>The object that processes the request.</returns>
		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x00095D1C File Offset: 0x00093F1C
		// (set) Token: 0x06003859 RID: 14425 RVA: 0x00095D24 File Offset: 0x00093F24
		public IRouteHandler RouteHandler { get; set; }

		/// <summary>Gets or sets the URL pattern for the route.</summary>
		/// <returns>The pattern for matching the route to a URL.</returns>
		/// <exception cref="T:System.ArgumentException">Any of the following:The value starts with ~ or /.The value contains a ? character.The catch-all parameter is not last.</exception>
		/// <exception cref="T:System.Exception">URL segments are not separated by a delimiter or a literal constant.</exception>
		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x00095D2D File Offset: 0x00093F2D
		// (set) Token: 0x0600385B RID: 14427 RVA: 0x00095D3E File Offset: 0x00093F3E
		public string Url
		{
			get
			{
				return this._url ?? string.Empty;
			}
			set
			{
				this._parsedRoute = RouteParser.Parse(value);
				this._url = value;
			}
		}

		/// <summary>Returns information about the requested route.</summary>
		/// <returns>An object that contains the values from the route definition.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		// Token: 0x0600385C RID: 14428 RVA: 0x00095D54 File Offset: 0x00093F54
		public override RouteData GetRouteData(HttpContextBase httpContext)
		{
			string text = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
			RouteValueDictionary routeValueDictionary = this._parsedRoute.Match(text, this.Defaults);
			if (routeValueDictionary == null)
			{
				return null;
			}
			RouteData routeData = new RouteData(this, this.RouteHandler);
			if (!this.ProcessConstraints(httpContext, routeValueDictionary, RouteDirection.IncomingRequest))
			{
				return null;
			}
			foreach (KeyValuePair<string, object> keyValuePair in routeValueDictionary)
			{
				routeData.Values.Add(keyValuePair.Key, keyValuePair.Value);
			}
			if (this.DataTokens != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair2 in this.DataTokens)
				{
					routeData.DataTokens[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			return routeData;
		}

		/// <summary>Returns information about the URL that is associated with the route.</summary>
		/// <returns>An object that contains information about the URL that is associated with the route.</returns>
		/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
		/// <param name="values">An object that contains the parameters for a route.</param>
		// Token: 0x0600385D RID: 14429 RVA: 0x00095E68 File Offset: 0x00094068
		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
		{
			BoundUrl boundUrl = this._parsedRoute.Bind(requestContext.RouteData.Values, values, this.Defaults, this.Constraints);
			if (boundUrl == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(requestContext.HttpContext, boundUrl.Values, RouteDirection.UrlGeneration))
			{
				return null;
			}
			VirtualPathData virtualPathData = new VirtualPathData(this, boundUrl.Url);
			if (this.DataTokens != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.DataTokens)
				{
					virtualPathData.DataTokens[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			return virtualPathData;
		}

		/// <summary>Determines whether a parameter value matches the constraint for that parameter.</summary>
		/// <returns>true if the parameter value matches the constraint; otherwise, false.</returns>
		/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
		/// <param name="constraint">The regular expression or object to use to test <paramref name="parameterName" />.</param>
		/// <param name="parameterName">The name of the parameter to test.</param>
		/// <param name="values">The values to test.</param>
		/// <param name="routeDirection">A value that specifies whether URL routing is processing an incoming request or constructing a URL.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="constraint" /> is not a string that contains a regular expression.</exception>
		// Token: 0x0600385E RID: 14430 RVA: 0x00095F24 File Offset: 0x00094124
		protected virtual bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
		{
			IRouteConstraint routeConstraint = constraint as IRouteConstraint;
			if (routeConstraint != null)
			{
				return routeConstraint.Match(httpContext, this, parameterName, values, routeDirection);
			}
			string text = constraint as string;
			if (text == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The constraint entry '{0}' on the route with URL '{1}' must have a string value or be of a type which implements IRouteConstraint."), parameterName, this.Url));
			}
			object obj;
			values.TryGetValue(parameterName, out obj);
			string text2 = Convert.ToString(obj, CultureInfo.InvariantCulture);
			string text3 = "^(" + text + ")$";
			return Regex.IsMatch(text2, text3, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x00095FA8 File Offset: 0x000941A8
		private bool ProcessConstraints(HttpContextBase httpContext, RouteValueDictionary values, RouteDirection routeDirection)
		{
			if (this.Constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.Constraints)
				{
					if (!this.ProcessConstraint(httpContext, keyValuePair.Value, keyValuePair.Key, values, routeDirection))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x04001EE3 RID: 7907
		private const string HttpMethodParameterName = "httpMethod";

		// Token: 0x04001EE4 RID: 7908
		private string _url;

		// Token: 0x04001EE5 RID: 7909
		private ParsedRoute _parsedRoute;
	}
}
