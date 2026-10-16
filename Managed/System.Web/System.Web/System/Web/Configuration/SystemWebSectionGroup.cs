using System;
using System.Configuration;
using System.Web.Services.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Allows the user to programmatically access the system.web group of the configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005D8 RID: 1496
	public sealed class SystemWebSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the anonymousIdentification section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AnonymousIdentificationSection" /> object.</returns>
		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060040B6 RID: 16566 RVA: 0x000A822A File Offset: 0x000A642A
		[ConfigurationProperty("anonymousIdentification")]
		public AnonymousIdentificationSection AnonymousIdentification
		{
			get
			{
				return (AnonymousIdentificationSection)base.Sections["anonymousIdentification"];
			}
		}

		/// <summary>Gets the authentication section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthenticationSection" /> object.</returns>
		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x000A8241 File Offset: 0x000A6441
		[ConfigurationProperty("authentication")]
		public AuthenticationSection Authentication
		{
			get
			{
				return (AuthenticationSection)base.Sections["authentication"];
			}
		}

		/// <summary>Gets the authorization section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationSection" /> object.</returns>
		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000A8258 File Offset: 0x000A6458
		[ConfigurationProperty("authorization")]
		public AuthorizationSection Authorization
		{
			get
			{
				return (AuthorizationSection)base.Sections["authorization"];
			}
		}

		/// <summary>Gets the browserCaps section.</summary>
		/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object.</returns>
		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x000A826F File Offset: 0x000A646F
		[ConfigurationProperty("browserCaps")]
		public DefaultSection BrowserCaps
		{
			get
			{
				return (DefaultSection)base.Sections["browserCaps"];
			}
		}

		/// <summary>Gets the clientTarget section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ClientTargetSection" /> object.</returns>
		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060040BA RID: 16570 RVA: 0x000A8286 File Offset: 0x000A6486
		[ConfigurationProperty("clientTarget")]
		public ClientTargetSection ClientTarget
		{
			get
			{
				return (ClientTargetSection)base.Sections["clientTarget"];
			}
		}

		/// <summary>Gets the compilation section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CompilationSection" /> object.</returns>
		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060040BB RID: 16571 RVA: 0x000A829D File Offset: 0x000A649D
		[ConfigurationProperty("compilation")]
		public CompilationSection Compilation
		{
			get
			{
				return (CompilationSection)base.Sections["compilation"];
			}
		}

		/// <summary>Gets the customErrors section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.CustomErrorsSection" /> object.</returns>
		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060040BC RID: 16572 RVA: 0x000A82B4 File Offset: 0x000A64B4
		[ConfigurationProperty("customErrors")]
		public CustomErrorsSection CustomErrors
		{
			get
			{
				return (CustomErrorsSection)base.Sections["customErrors"];
			}
		}

		/// <summary>Gets the deployment section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.DeploymentSection" /> object.</returns>
		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060040BD RID: 16573 RVA: 0x000A82CB File Offset: 0x000A64CB
		[ConfigurationProperty("deployment")]
		public DeploymentSection Deployment
		{
			get
			{
				return (DeploymentSection)base.Sections["deployment"];
			}
		}

		/// <summary>Gets the deviceFilters section.</summary>
		/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object.</returns>
		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x000A82E2 File Offset: 0x000A64E2
		[ConfigurationProperty("deviceFilters")]
		public DefaultSection DeviceFilters
		{
			get
			{
				return (DefaultSection)base.Sections["deviceFilters"];
			}
		}

		/// <summary>Gets the globalization section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.GlobalizationSection" /> object.</returns>
		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060040BF RID: 16575 RVA: 0x000A82F9 File Offset: 0x000A64F9
		[ConfigurationProperty("globalization")]
		public GlobalizationSection Globalization
		{
			get
			{
				return (GlobalizationSection)base.Sections["globalization"];
			}
		}

		/// <summary>Gets the healthMonitoring section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HealthMonitoringSection" /> object.</returns>
		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000A8310 File Offset: 0x000A6510
		[ConfigurationProperty("healthMonitoring")]
		public HealthMonitoringSection HealthMonitoring
		{
			get
			{
				return (HealthMonitoringSection)base.Sections["healthMonitoring"];
			}
		}

		/// <summary>Gets the hostingEnvironment section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HostingEnvironmentSection" /> object refers to the hostingEnvironment section of the configuration file. </returns>
		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x060040C1 RID: 16577 RVA: 0x000A8327 File Offset: 0x000A6527
		[ConfigurationProperty("hostingEnvironment")]
		public HostingEnvironmentSection HostingEnvironment
		{
			get
			{
				return (HostingEnvironmentSection)base.Sections["hostingEnvironment"];
			}
		}

		/// <summary>Gets the httpCookies section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpCookiesSection" /> object.</returns>
		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x000A833E File Offset: 0x000A653E
		[ConfigurationProperty("httpCookies")]
		public HttpCookiesSection HttpCookies
		{
			get
			{
				return (HttpCookiesSection)base.Sections["httpCookies"];
			}
		}

		/// <summary>Gets the httpHandlers section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlersSection" /> object.</returns>
		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x000A8355 File Offset: 0x000A6555
		[ConfigurationProperty("httpHandlers")]
		public HttpHandlersSection HttpHandlers
		{
			get
			{
				return (HttpHandlersSection)base.Sections["httpHandlers"];
			}
		}

		/// <summary>Gets the httpModules section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpModulesSection" /> object.</returns>
		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x000A836C File Offset: 0x000A656C
		[ConfigurationProperty("httpModules")]
		public HttpModulesSection HttpModules
		{
			get
			{
				return (HttpModulesSection)base.Sections["httpModules"];
			}
		}

		/// <summary>Gets the httpRuntime section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> object.</returns>
		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x060040C5 RID: 16581 RVA: 0x000A8383 File Offset: 0x000A6583
		[ConfigurationProperty("httpRuntime")]
		public HttpRuntimeSection HttpRuntime
		{
			get
			{
				return (HttpRuntimeSection)base.Sections["httpRuntime"];
			}
		}

		/// <summary>Gets the identity section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.IdentitySection" /> object.</returns>
		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x060040C6 RID: 16582 RVA: 0x000A839A File Offset: 0x000A659A
		[ConfigurationProperty("identity")]
		public IdentitySection Identity
		{
			get
			{
				return (IdentitySection)base.Sections["identity"];
			}
		}

		/// <summary>Gets the machineKey section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.MachineKeySection" /> object.</returns>
		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x060040C7 RID: 16583 RVA: 0x000A83B1 File Offset: 0x000A65B1
		[ConfigurationProperty("machineKey")]
		public MachineKeySection MachineKey
		{
			get
			{
				return (MachineKeySection)base.Sections["machineKey"];
			}
		}

		/// <summary>Gets the membership section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.MembershipSection" /> object.</returns>
		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x060040C8 RID: 16584 RVA: 0x000A83C8 File Offset: 0x000A65C8
		[ConfigurationProperty("membership")]
		public MembershipSection Membership
		{
			get
			{
				return (MembershipSection)base.Sections["membership"];
			}
		}

		/// <summary>Gets the mobileControls section.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object refers to the mobileControls section of the configuration file.</returns>
		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x060040C9 RID: 16585 RVA: 0x000A83DF File Offset: 0x000A65DF
		[ConfigurationProperty("mobileControls")]
		[Obsolete("System.Web.Mobile.dll is obsolete.")]
		public ConfigurationSection MobileControls
		{
			get
			{
				return base.Sections["MobileControls"];
			}
		}

		/// <summary>Gets the pages section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.PagesSection" /> object.</returns>
		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x000A83F1 File Offset: 0x000A65F1
		[ConfigurationProperty("pages")]
		public PagesSection Pages
		{
			get
			{
				return (PagesSection)base.Sections["pages"];
			}
		}

		/// <summary>Gets the processModel section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProcessModelSection" /> object.</returns>
		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x000A8408 File Offset: 0x000A6608
		[ConfigurationProperty("processModel")]
		public ProcessModelSection ProcessModel
		{
			get
			{
				return (ProcessModelSection)base.Sections["processModel"];
			}
		}

		/// <summary>Gets the profile section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object.</returns>
		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x000A841F File Offset: 0x000A661F
		[ConfigurationProperty("profile")]
		public ProfileSection Profile
		{
			get
			{
				return (ProfileSection)base.Sections["profile"];
			}
		}

		/// <summary>Gets the protocols section.</summary>
		/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object refers to the protocols section of the configuration file. </returns>
		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x000A8436 File Offset: 0x000A6636
		[ConfigurationProperty("protocols")]
		public DefaultSection Protocols
		{
			get
			{
				return (DefaultSection)base.Sections["protocols"];
			}
		}

		/// <summary>Gets the roleManager section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.RoleManagerSection" /> object.</returns>
		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x060040CE RID: 16590 RVA: 0x000A844D File Offset: 0x000A664D
		[ConfigurationProperty("roleManager")]
		public RoleManagerSection RoleManager
		{
			get
			{
				return (RoleManagerSection)base.Sections["roleManager"];
			}
		}

		/// <summary>Gets the securityPolicy section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SecurityPolicySection" /> object.</returns>
		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x060040CF RID: 16591 RVA: 0x000A8464 File Offset: 0x000A6664
		[ConfigurationProperty("securityPolicy")]
		public SecurityPolicySection SecurityPolicy
		{
			get
			{
				return (SecurityPolicySection)base.Sections["securityPolicy"];
			}
		}

		/// <summary>Gets the sessionState section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SessionStateSection" /> object.</returns>
		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x000A847B File Offset: 0x000A667B
		[ConfigurationProperty("sessionState")]
		public SessionStateSection SessionState
		{
			get
			{
				return (SessionStateSection)base.Sections["sessionState"];
			}
		}

		/// <summary>Gets the siteMap section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SiteMapSection" /> object.</returns>
		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x060040D1 RID: 16593 RVA: 0x000A8492 File Offset: 0x000A6692
		[ConfigurationProperty("siteMap")]
		public SiteMapSection SiteMap
		{
			get
			{
				return (SiteMapSection)base.Sections["siteMap"];
			}
		}

		/// <summary>Gets the trace section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TraceSection" /> object.</returns>
		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x060040D2 RID: 16594 RVA: 0x000A84A9 File Offset: 0x000A66A9
		[ConfigurationProperty("trace")]
		public TraceSection Trace
		{
			get
			{
				return (TraceSection)base.Sections["trace"];
			}
		}

		/// <summary>Gets the trust section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.TrustSection" /> object.</returns>
		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x000A84C0 File Offset: 0x000A66C0
		[ConfigurationProperty("trust")]
		public TrustSection Trust
		{
			get
			{
				return (TrustSection)base.Sections["trust"];
			}
		}

		/// <summary>Gets the urlMappings section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.UrlMappingsSection" /> object refers to the urlMappings section of the configuration file. </returns>
		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060040D4 RID: 16596 RVA: 0x000A84D7 File Offset: 0x000A66D7
		[ConfigurationProperty("urlMappings")]
		public UrlMappingsSection UrlMappings
		{
			get
			{
				return (UrlMappingsSection)base.Sections["urlMappings"];
			}
		}

		/// <summary>Gets the webControls section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.WebControlsSection" /> object refers to the webControls section of the configuration file. </returns>
		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x000A84EE File Offset: 0x000A66EE
		[ConfigurationProperty("webControls")]
		public WebControlsSection WebControls
		{
			get
			{
				return (WebControlsSection)base.Sections["webControls"];
			}
		}

		/// <summary>Gets the webParts section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.WebPartsSection" /> object refers to the webParts section of the configuration file.</returns>
		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000A8505 File Offset: 0x000A6705
		[ConfigurationProperty("webParts")]
		public WebPartsSection WebParts
		{
			get
			{
				return (WebPartsSection)base.Sections["webParts"];
			}
		}

		/// <summary>Gets the webServices section.</summary>
		/// <returns>The <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object refers to the webServices section of the configuration file.</returns>
		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x000A851C File Offset: 0x000A671C
		[ConfigurationProperty("webServices")]
		public WebServicesSection WebServices
		{
			get
			{
				return (WebServicesSection)base.Sections["webServices"];
			}
		}

		/// <summary>Gets the xhtmlConformance section.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.XhtmlConformanceSection" /> object refers to the xhtmlConformance section of the configuration file. </returns>
		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x000A8533 File Offset: 0x000A6733
		[ConfigurationProperty("xhtmlConformance")]
		public XhtmlConformanceSection XhtmlConformance
		{
			get
			{
				return (XhtmlConformanceSection)base.Sections["xhtmlConformance"];
			}
		}

		/// <summary>Gets the FullTrustAssemblies section of the configuration file. </summary>
		/// <returns>The FullTrustAssemblies section of the configuration file.</returns>
		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public FullTrustAssembliesSection FullTrustAssemblies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the PartialTrustVisibleAssemblies section of the configuration file. </summary>
		/// <returns>The PartialTrustVisibleAssemblies section of the configuration file.</returns>
		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public PartialTrustVisibleAssembliesSection PartialTrustVisibleAssemblies
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
