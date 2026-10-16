using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute that Web Forms user controls (.ascx files) use to indicate if and how their output is cached. This class cannot be inherited.</summary>
	// Token: 0x02000214 RID: 532
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PartialCachingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class with the specified duration assigned to the user control to be cached.</summary>
		/// <param name="duration">The amount of time, in seconds, a user control should remain in the output cache. </param>
		// Token: 0x060015D3 RID: 5587 RVA: 0x00038C7F File Offset: 0x00036E7F
		public PartialCachingAttribute(int duration)
		{
			this.duration = duration;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, and custom output caching requirements used to vary the cache.</summary>
		/// <param name="duration">The amount of time, in seconds, that the user control is cached. </param>
		/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes or to a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name. </param>
		/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property. </param>
		/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file. </param>
		// Token: 0x060015D4 RID: 5588 RVA: 0x00038C8E File Offset: 0x00036E8E
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom)
		{
			this.duration = duration;
			this.varyByParams = varyByParams;
			this.varyByControls = varyByControls;
			this.varyByCustom = varyByCustom;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, custom output caching requirements used to vary the cache, and whether the user control output can be shared with multiple pages.</summary>
		/// <param name="duration">The amount of time, in seconds, that the user control is cached.</param>
		/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes, or a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name.</param>
		/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property.</param>
		/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file.</param>
		/// <param name="shared">true to indicate that the user control output can be shared with multiple pages; otherwise, false. </param>
		// Token: 0x060015D5 RID: 5589 RVA: 0x00038CB3 File Offset: 0x00036EB3
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, bool shared)
		{
			this.duration = duration;
			this.varyByParams = varyByParams;
			this.varyByControls = varyByControls;
			this.varyByCustom = varyByCustom;
			this.shared = shared;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, custom output caching requirements used to vary the cache, the database dependencies, and whether the user control output can be shared with multiple pages. </summary>
		/// <param name="duration">The amount of time, in seconds, that the user control is cached.</param>
		/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes, or a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name.</param>
		/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property.</param>
		/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file.</param>
		/// <param name="sqlDependency">A delimited list of database names and table names that, when changed, explicitly expire a cache entry in the ASP.NET cache. These database names match those SQL Server cache dependencies identified in your Web configuration section.</param>
		/// <param name="shared">true to indicate that the user control output can be shared with multiple pages; otherwise, false.</param>
		// Token: 0x060015D6 RID: 5590 RVA: 0x00038CE0 File Offset: 0x00036EE0
		public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, bool shared)
		{
			this.duration = duration;
			this.varyByParams = varyByParams;
			this.varyByControls = varyByControls;
			this.varyByCustom = varyByCustom;
			this.sqlDependency = sqlDependency;
			this.shared = shared;
		}

		/// <summary>Gets the amount of time, in seconds, that cached items should remain in the output cache.</summary>
		/// <returns>The amount of time, in seconds, a user control should remain in the output cache.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00038D15 File Offset: 0x00036F15
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x00003946 File Offset: 0x00001B46
		public int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the name of the provider that is used to store the output-cached data for the associated control.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x00038D1D File Offset: 0x00036F1D
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x00038D25 File Offset: 0x00036F25
		public string ProviderName { get; set; }

		/// <summary>Gets a list of query string or form POST parameters that the output cache will use to vary the user control.</summary>
		/// <returns>The list of query string or form POST parameters.</returns>
		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00038D2E File Offset: 0x00036F2E
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x00003946 File Offset: 0x00001B46
		public string VaryByParams
		{
			get
			{
				return this.varyByParams;
			}
			set
			{
			}
		}

		/// <summary>Gets a list of user control properties that the output cache uses to vary the user control.</summary>
		/// <returns>The list of user control properties.</returns>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00038D36 File Offset: 0x00036F36
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x00003946 File Offset: 0x00001B46
		public string VaryByControls
		{
			get
			{
				return this.varyByControls;
			}
			set
			{
			}
		}

		/// <summary>Gets a list of custom strings that the output cache will use to vary the user control.</summary>
		/// <returns>The list of custom strings.</returns>
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x00038D3E File Offset: 0x00036F3E
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00003946 File Offset: 0x00001B46
		public string VaryByCustom
		{
			get
			{
				return this.varyByCustom;
			}
			set
			{
			}
		}

		/// <summary>Gets a value indicating whether user control output can be shared with multiple pages.</summary>
		/// <returns>true if user control output can be shared between multiple pages; otherwise, false. The default is false.</returns>
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00038D46 File Offset: 0x00036F46
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x00003946 File Offset: 0x00001B46
		public bool Shared
		{
			get
			{
				return this.shared;
			}
			set
			{
			}
		}

		/// <summary>Gets a delimited string that identifies one or more database and table name pairs that the cached user control is dependent on.</summary>
		/// <returns>A delimited string that identifies a set of database and table names that the user control cache entry is dependent on.</returns>
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00038D4E File Offset: 0x00036F4E
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x00003946 File Offset: 0x00001B46
		public string SqlDependency
		{
			get
			{
				return this.sqlDependency;
			}
			set
			{
			}
		}

		// Token: 0x0400153D RID: 5437
		private int duration;

		// Token: 0x0400153E RID: 5438
		private string varyByControls;

		// Token: 0x0400153F RID: 5439
		private string varyByCustom;

		// Token: 0x04001540 RID: 5440
		private string varyByParams;

		// Token: 0x04001541 RID: 5441
		private bool shared;

		// Token: 0x04001542 RID: 5442
		private string sqlDependency;
	}
}
