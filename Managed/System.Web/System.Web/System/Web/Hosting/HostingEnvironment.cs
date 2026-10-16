using System;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using Unity;

namespace System.Web.Hosting
{
	/// <summary>Provides application-management functions and application services to a managed application within its application domain. This class cannot be inherited.</summary>
	// Token: 0x0200054B RID: 1355
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
	public sealed class HostingEnvironment : MarshalByRefObject
	{
		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x0009BE2E File Offset: 0x0009A02E
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x0009BE35 File Offset: 0x0009A035
		internal static bool HaveCustomVPP { get; private set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> class. </summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Hosting.HostingEnvironment.#ctor" /> constructor is called more than once.</exception>
		// Token: 0x06003A94 RID: 14996 RVA: 0x0009BE3D File Offset: 0x0009A03D
		public HostingEnvironment()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets the unique identifier of the application.</summary>
		/// <returns>The unique identifier of the application.</returns>
		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x0009BE4A File Offset: 0x0009A04A
		public static string ApplicationID
		{
			get
			{
				return HttpRuntime.AppDomainAppId;
			}
		}

		/// <summary>Gets the physical path on disk to the application's directory.</summary>
		/// <returns>The physical path on disk to the application's directory.</returns>
		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x0001729A File Offset: 0x0001549A
		public static string ApplicationPhysicalPath
		{
			get
			{
				return HttpRuntime.AppDomainAppPath;
			}
		}

		/// <summary>Gets the root virtual path of the application.</summary>
		/// <returns>The root virtual path of the application with no trailing slash (/).</returns>
		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06003A97 RID: 14999 RVA: 0x000172A1 File Offset: 0x000154A1
		public static string ApplicationVirtualPath
		{
			get
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> instance for the current application.</summary>
		/// <returns>The current <see cref="T:System.Web.Caching.Cache" /> instance.</returns>
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x0000F085 File Offset: 0x0000D285
		public static Cache Cache
		{
			get
			{
				return HttpRuntime.Cache;
			}
		}

		/// <summary>Gets any exception thrown during initialization of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object.</summary>
		/// <returns>The exception thrown during initialization of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object. If no exception was thrown, returns null.</returns>
		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x0009BE51 File Offset: 0x0009A051
		public static Exception InitializationException
		{
			get
			{
				return HttpApplication.InitializationException;
			}
		}

		/// <summary>Gets a value indicating whether the current application domain is being hosted by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object.</summary>
		/// <returns>true if the application domain is hosted by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object; otherwise, false.</returns>
		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003A9A RID: 15002 RVA: 0x0009BE58 File Offset: 0x0009A058
		// (set) Token: 0x06003A9B RID: 15003 RVA: 0x0009BE5F File Offset: 0x0009A05F
		public static bool IsHosted
		{
			get
			{
				return HostingEnvironment.is_hosted;
			}
			internal set
			{
				HostingEnvironment.is_hosted = value;
			}
		}

		/// <summary>Returns an enumerated value that indicates why the application terminated.</summary>
		/// <returns>One of the <see cref="T:System.Web.ApplicationShutdownReason" /> values.</returns>
		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x0009BE67 File Offset: 0x0009A067
		public static ApplicationShutdownReason ShutdownReason
		{
			get
			{
				return HostingEnvironment.shutdown_reason;
			}
		}

		/// <summary>Gets the name of the site.</summary>
		/// <returns>The name of the site.</returns>
		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x0009BE6E File Offset: 0x0009A06E
		// (set) Token: 0x06003A9E RID: 15006 RVA: 0x0009BE75 File Offset: 0x0009A075
		public static string SiteName
		{
			get
			{
				return HostingEnvironment.site_name;
			}
			internal set
			{
				HostingEnvironment.site_name = value;
			}
		}

		/// <summary>Gets the virtual path provider for this application.</summary>
		/// <returns>The <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance for this application.</returns>
		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x0009BE7D File Offset: 0x0009A07D
		public static VirtualPathProvider VirtualPathProvider
		{
			get
			{
				return HostingEnvironment.vpath_provider;
			}
		}

		/// <summary>Gets a value that indicates whether the hosting environment has access to the ASP.NET build system.</summary>
		/// <returns>true if the application domain is the ASP.NET hosted application domain used in ClientBuildManager scenarios; otherwise, false.</returns>
		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x00008A4D File Offset: 0x00006C4D
		public static bool InClientBuildManager
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reduces the count of busy objects in the hosted environment by one.</summary>
		// Token: 0x06003AA1 RID: 15009 RVA: 0x0009BE84 File Offset: 0x0009A084
		public static void DecrementBusyCount()
		{
			Interlocked.Decrement(ref HostingEnvironment.busy_count);
		}

		/// <summary>Impersonates the user represented by the application identity.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
		// Token: 0x06003AA2 RID: 15010 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public static IDisposable Impersonate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Impersonates the user represented by the specified user token.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <param name="token">The handle of a Windows account token.</param>
		/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
		// Token: 0x06003AA3 RID: 15011 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public static IDisposable Impersonate(IntPtr token)
		{
			throw new NotImplementedException();
		}

		/// <summary>Impersonates the user specified by the configuration settings for the specified virtual path, or the specified user token.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
		/// <param name="userToken">The handle of a Windows account token.</param>
		/// <param name="virtualPath">The path to the requested resource.</param>
		/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
		// Token: 0x06003AA4 RID: 15012 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public static IDisposable Impersonate(IntPtr userToken, string virtualPath)
		{
			throw new NotImplementedException();
		}

		/// <summary>Increases the count of busy objects in the hosted environment by one.</summary>
		// Token: 0x06003AA5 RID: 15013 RVA: 0x0009BE91 File Offset: 0x0009A091
		public static void IncrementBusyCount()
		{
			Interlocked.Increment(ref HostingEnvironment.busy_count);
		}

		/// <summary>Gives the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object an infinite lifetime by preventing a lease from being created.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x06003AA6 RID: 15014 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		/// <summary>Starts shutting down the web application associated with this host and removes registered objects from the system.</summary>
		// Token: 0x06003AA7 RID: 15015 RVA: 0x0009BE9E File Offset: 0x0009A09E
		public static void InitiateShutdown()
		{
			HttpRuntime.UnloadAppDomain();
		}

		/// <summary>Maps a virtual path to a physical path on the server.</summary>
		/// <returns>The physical path on the server specified by <paramref name="virtualPath" />.</returns>
		/// <param name="virtualPath">The virtual path (absolute or relative).</param>
		// Token: 0x06003AA8 RID: 15016 RVA: 0x0009BEA8 File Offset: 0x0009A0A8
		public static string MapPath(string virtualPath)
		{
			if (virtualPath == null || virtualPath == "")
			{
				throw new ArgumentNullException("virtualPath");
			}
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext == null) ? null : httpContext.Request);
			if (httpRequest == null)
			{
				return null;
			}
			return httpRequest.MapPath(virtualPath);
		}

		/// <summary>Places an object in the list of registered objects for the application.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x06003AA9 RID: 15017 RVA: 0x0009BEEF File Offset: 0x0009A0EF
		public static void RegisterObject(IRegisteredObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (HostingEnvironment.Host != null)
			{
				HostingEnvironment.Host.RegisterObject(obj, false);
			}
		}

		/// <summary>Registers a new <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance with the ASP.NET compilation system.</summary>
		/// <param name="virtualPathProvider">The new <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance to add to the compilation system.</param>
		// Token: 0x06003AAA RID: 15018 RVA: 0x0009BF14 File Offset: 0x0009A114
		public static void RegisterVirtualPathProvider(VirtualPathProvider virtualPathProvider)
		{
			if (HttpRuntime.AppDomainAppVirtualPath == null)
			{
				throw new InvalidOperationException();
			}
			if (virtualPathProvider == null)
			{
				throw new ArgumentNullException("virtualPathProvider");
			}
			VirtualPathProvider virtualPathProvider2 = HostingEnvironment.vpath_provider;
			HostingEnvironment.vpath_provider = virtualPathProvider;
			HostingEnvironment.vpath_provider.InitializeAndSetPrevious(virtualPathProvider2);
			if (!(virtualPathProvider is DefaultVirtualPathProvider))
			{
				HostingEnvironment.HaveCustomVPP = true;
				return;
			}
			HostingEnvironment.HaveCustomVPP = false;
		}

		/// <summary>Sets the current thread to the culture of the specified virtual path.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the culture prior to changing; this object can be used to revert to the previous culture.</returns>
		/// <param name="virtualPath">The path that contains the culture information.</param>
		// Token: 0x06003AAB RID: 15019 RVA: 0x0009BF68 File Offset: 0x0009A168
		public static IDisposable SetCultures(string virtualPath)
		{
			GlobalizationSection globalizationSection = WebConfigurationManager.GetSection("system.web/globalization", virtualPath) as GlobalizationSection;
			IDisposable disposable = Thread.CurrentThread.CurrentCulture as IDisposable;
			string culture = globalizationSection.Culture;
			if (string.IsNullOrEmpty(culture))
			{
				return disposable;
			}
			Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
			return disposable;
		}

		/// <summary>Sets the current thread to the culture specified in the application configuration file.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> object that represents the culture prior to changing; this object can be used to revert to the previous culture.</returns>
		// Token: 0x06003AAC RID: 15020 RVA: 0x0009BFB6 File Offset: 0x0009A1B6
		public static IDisposable SetCultures()
		{
			return HostingEnvironment.SetCultures("~/");
		}

		/// <summary>Removes an object from the list of registered objects for the application.</summary>
		/// <param name="obj">The object to remove.</param>
		// Token: 0x06003AAD RID: 15021 RVA: 0x0009BFC2 File Offset: 0x0009A1C2
		public static void UnregisterObject(IRegisteredObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (HostingEnvironment.Host != null)
			{
				HostingEnvironment.Host.UnregisterObject(obj);
			}
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x0009BFE5 File Offset: 0x0009A1E5
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void QueueBackgroundWorkItem(Action<CancellationToken> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			HostingEnvironment.QueueBackgroundWorkItem(delegate(CancellationToken ct)
			{
				workItem(ct);
				return HostingEnvironment._completedTask;
			});
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x0009C016 File Offset: 0x0009A216
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public static void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
		{
			if (workItem == null)
			{
				throw new ArgumentNullException("workItem");
			}
			if (HostingEnvironment.Host == null)
			{
				throw new InvalidOperationException();
			}
			HostingEnvironment.QueueBackgroundWorkItemInternal(workItem);
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x0009C03C File Offset: 0x0009A23C
		private static void QueueBackgroundWorkItemInternal(Func<CancellationToken, Task> workItem)
		{
			BackgroundWorkScheduler backgroundWorkScheduler = Volatile.Read<BackgroundWorkScheduler>(ref HostingEnvironment._backgroundWorkScheduler);
			if (backgroundWorkScheduler == null)
			{
				BackgroundWorkScheduler backgroundWorkScheduler2 = new BackgroundWorkScheduler(new Action<BackgroundWorkScheduler>(HostingEnvironment.UnregisterObject), new Action<AppDomain, Exception>(HostingEnvironment.WriteUnhandledException), null);
				backgroundWorkScheduler = Interlocked.CompareExchange<BackgroundWorkScheduler>(ref HostingEnvironment._backgroundWorkScheduler, backgroundWorkScheduler2, null) ?? backgroundWorkScheduler2;
				if (backgroundWorkScheduler == backgroundWorkScheduler2)
				{
					HostingEnvironment.RegisterObject(backgroundWorkScheduler);
				}
			}
			backgroundWorkScheduler.ScheduleWorkItem(workItem);
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x0009C099 File Offset: 0x0009A299
		private static void WriteUnhandledException(AppDomain appDomain, Exception exception)
		{
			Console.Error.WriteLine("Error in background work item: " + ((exception != null) ? exception.ToString() : null));
		}

		/// <summary>This property supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An object that contains information about the application host.</returns>
		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003AB3 RID: 15027 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static IApplicationHost ApplicationHost
		{
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static ApplicationMonitors ApplicationMonitors
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicats whether the current application is in a development environment.</summary>
		/// <returns>true if the application is in a development environment; otherwise, false.</returns>
		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003AB5 RID: 15029 RVA: 0x0009C0E4 File Offset: 0x0009A2E4
		public static bool IsDevelopmentEnvironment
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the maximum concurrent requests per CPU.</summary>
		/// <returns>The maximum concurrent requests per CPU.</returns>
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x0009C100 File Offset: 0x0009A300
		// (set) Token: 0x06003AB7 RID: 15031 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static int MaxConcurrentRequestsPerCPU
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the maximum concurrent threads per CPU.</summary>
		/// <returns>The maximum concurrent threads per CPU.</returns>
		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x0009C11C File Offset: 0x0009A31C
		// (set) Token: 0x06003AB9 RID: 15033 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static int MaxConcurrentThreadsPerCPU
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the worker process or application pool associated with this host has stopped listening for new requests and will eventually shut down.</summary>
		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06003ABA RID: 15034 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06003ABB RID: 15035 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static event EventHandler StopListening
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>This method supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x06003ABC RID: 15036 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void MessageReceived()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001FDB RID: 8155
		private static bool is_hosted;

		// Token: 0x04001FDC RID: 8156
		private static string site_name;

		// Token: 0x04001FDD RID: 8157
		private static ApplicationShutdownReason shutdown_reason;

		// Token: 0x04001FDE RID: 8158
		internal static BareApplicationHost Host;

		// Token: 0x04001FDF RID: 8159
		private static VirtualPathProvider vpath_provider = ((HttpRuntime.AppDomainAppVirtualPath == null) ? null : new DefaultVirtualPathProvider());

		// Token: 0x04001FE0 RID: 8160
		private static int busy_count;

		// Token: 0x04001FE1 RID: 8161
		private static BackgroundWorkScheduler _backgroundWorkScheduler = null;

		// Token: 0x04001FE2 RID: 8162
		private static readonly Task<object> _completedTask = Task.FromResult<object>(null);
	}
}
