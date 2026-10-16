using System;
using System.ComponentModel;
using System.Data;
using System.Web.Routing;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of a URL segment to a parameter object.</summary>
	// Token: 0x020003FF RID: 1023
	[DefaultProperty("RouteKey")]
	public class RouteParameter : Parameter
	{
		/// <summary>Gets or sets the name of the route segment from which to retrieve the value for the route parameter.</summary>
		/// <returns>The name of the route segment that contains the value for the parameter.</returns>
		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x000763C4 File Offset: 0x000745C4
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x000763CC File Offset: 0x000745CC
		[DefaultValue("")]
		public string RouteKey
		{
			get
			{
				return this.routeKey;
			}
			set
			{
				this.routeKey = value ?? string.Empty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class. </summary>
		// Token: 0x06002D5C RID: 11612 RVA: 0x000763DE File Offset: 0x000745DE
		public RouteParameter()
		{
			this.RouteKey = string.Empty;
			base.Name = string.Empty;
			base.Type = TypeCode.Empty;
			base.Direction = ParameterDirection.Input;
			base.DefaultValue = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the values of the specified instance. </summary>
		/// <param name="original">An object from which the current instance is initialized.</param>
		// Token: 0x06002D5D RID: 11613 RVA: 0x00076411 File Offset: 0x00074611
		protected RouteParameter(RouteParameter original)
			: base(original)
		{
			this.RouteKey = original.RouteKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name for the parameter and the specified key for route data. </summary>
		/// <param name="name">The name of the parameter instance.</param>
		/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
		// Token: 0x06002D5E RID: 11614 RVA: 0x00076426 File Offset: 0x00074626
		public RouteParameter(string name, string routeKey)
			: base(name)
		{
			this.RouteKey = routeKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name and database type for the parameter, and by using the specified key for the route data. </summary>
		/// <param name="name">The name of the parameter instance.</param>
		/// <param name="dbType">The database type of the parameter instance.</param>
		/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
		// Token: 0x06002D5F RID: 11615 RVA: 0x00076436 File Offset: 0x00074636
		public RouteParameter(string name, DbType dbType, string routeKey)
			: base(name, dbType)
		{
			this.RouteKey = routeKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RouteParameter" /> class by using the specified name and type for the parameter, and by using the specified key for the route data. </summary>
		/// <param name="name">The name of the parameter instance.</param>
		/// <param name="type">The type that the parameter represents.</param>
		/// <param name="routeKey">The name of the route segment that contains the value for the parameter.</param>
		// Token: 0x06002D60 RID: 11616 RVA: 0x00076447 File Offset: 0x00074647
		public RouteParameter(string name, TypeCode type, string routeKey)
			: base(name, type)
		{
			this.RouteKey = routeKey;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.RouteParameter" /> instance.</summary>
		/// <returns>An object that is a duplicate of the current one.</returns>
		// Token: 0x06002D61 RID: 11617 RVA: 0x00076458 File Offset: 0x00074658
		protected override Parameter Clone()
		{
			return new RouteParameter(this);
		}

		/// <summary>Evaluates the request URL and returns the value of the parameter.</summary>
		/// <returns>The current value of the parameter.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> instance of the request.</param>
		/// <param name="control">The control that the parameter is bound to.</param>
		// Token: 0x06002D62 RID: 11618 RVA: 0x00076460 File Offset: 0x00074660
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || control == null)
			{
				return null;
			}
			Page page = control.Page;
			if (page == null)
			{
				throw new NullReferenceException(".NET emulation");
			}
			RouteData routeData = page.RouteData;
			if (routeData == null)
			{
				return null;
			}
			return routeData.Values[this.RouteKey];
		}

		// Token: 0x04001B78 RID: 7032
		private string routeKey;
	}
}
