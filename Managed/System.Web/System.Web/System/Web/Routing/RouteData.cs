using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Encapsulates information about a route.</summary>
	// Token: 0x020004EE RID: 1262
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteData" /> class. </summary>
		// Token: 0x0600388B RID: 14475 RVA: 0x00096739 File Offset: 0x00094939
		public RouteData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteData" /> class by using the specified route and route handler. </summary>
		/// <param name="route">An object that defines the route.</param>
		/// <param name="routeHandler">An object that processes the request.</param>
		// Token: 0x0600388C RID: 14476 RVA: 0x00096757 File Offset: 0x00094957
		public RouteData(RouteBase route, IRouteHandler routeHandler)
		{
			this.Route = route;
			this.RouteHandler = routeHandler;
		}

		/// <summary>Gets a collection of custom values that are passed to the route handler but are not used when ASP.NET routing determines whether the route matches a request.</summary>
		/// <returns>An object that contains custom values.</returns>
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x0600388D RID: 14477 RVA: 0x00096783 File Offset: 0x00094983
		public RouteValueDictionary DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		/// <summary>Gets or sets the object that represents a route.</summary>
		/// <returns>An object that represents the route definition.</returns>
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x0009678B File Offset: 0x0009498B
		// (set) Token: 0x0600388F RID: 14479 RVA: 0x00096793 File Offset: 0x00094993
		public RouteBase Route { get; set; }

		/// <summary>Gets or sets the object that processes a requested route.</summary>
		/// <returns>An object that processes the route request.</returns>
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x0009679C File Offset: 0x0009499C
		// (set) Token: 0x06003891 RID: 14481 RVA: 0x000967A4 File Offset: 0x000949A4
		public IRouteHandler RouteHandler
		{
			get
			{
				return this._routeHandler;
			}
			set
			{
				this._routeHandler = value;
			}
		}

		/// <summary>Gets a collection of URL parameter values and default values for the route.</summary>
		/// <returns>An object that contains values that are parsed from the URL and from default values.</returns>
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06003892 RID: 14482 RVA: 0x000967AD File Offset: 0x000949AD
		public RouteValueDictionary Values
		{
			get
			{
				return this._values;
			}
		}

		/// <summary>Retrieves the value with the specified identifier.</summary>
		/// <returns>The element in the <see cref="P:System.Web.Routing.RouteData.Values" /> property whose key matches <paramref name="valueName" />.</returns>
		/// <param name="valueName">The key of the value to retrieve.</param>
		/// <exception cref="T:System.InvalidOperationException">A value does not exist for <paramref name="valueName" />.</exception>
		// Token: 0x06003893 RID: 14483 RVA: 0x000967B8 File Offset: 0x000949B8
		public string GetRequiredString(string valueName)
		{
			object obj;
			if (this.Values.TryGetValue(valueName, out obj))
			{
				string text = obj as string;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The RouteData must contain an item named '{0}' with a non-empty string value."), valueName));
		}

		// Token: 0x04001EF3 RID: 7923
		private IRouteHandler _routeHandler;

		// Token: 0x04001EF4 RID: 7924
		private RouteValueDictionary _values = new RouteValueDictionary();

		// Token: 0x04001EF5 RID: 7925
		private RouteValueDictionary _dataTokens = new RouteValueDictionary();
	}
}
