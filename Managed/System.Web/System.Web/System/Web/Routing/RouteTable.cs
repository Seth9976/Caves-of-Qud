using System;
using System.Runtime.CompilerServices;

namespace System.Web.Routing
{
	/// <summary>Stores the URL routes for an application.</summary>
	// Token: 0x020004F2 RID: 1266
	[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class RouteTable
	{
		/// <summary>Gets a collection of objects that derive from the <see cref="T:System.Web.Routing.RouteBase" /> class.</summary>
		/// <returns>An object that contains all the routes in the collection.</returns>
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x060038A2 RID: 14498 RVA: 0x00096E3C File Offset: 0x0009503C
		public static RouteCollection Routes
		{
			get
			{
				return RouteTable._instance;
			}
		}

		// Token: 0x04001EFC RID: 7932
		private static RouteCollection _instance = new RouteCollection();
	}
}
