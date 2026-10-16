using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Internal;
using System.Reflection;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;
using Mono.Web.Util;

namespace System.Web.Configuration
{
	/// <summary>Provides access to configuration files as they apply to Web applications.</summary>
	// Token: 0x020005EB RID: 1515
	public static class WebConfigurationManager
	{
		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x060041C9 RID: 16841 RVA: 0x000A9BFF File Offset: 0x000A7DFF
		internal static ArrayList ExtraAssemblies
		{
			get
			{
				if (WebConfigurationManager.extra_assemblies == null)
				{
					WebConfigurationManager.extra_assemblies = new ArrayList();
				}
				return WebConfigurationManager.extra_assemblies;
			}
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000A9C18 File Offset: 0x000A7E18
		static WebConfigurationManager()
		{
			int num = 100;
			bool flag = false;
			int num2;
			if (int.TryParse(Environment.GetEnvironmentVariable("MONO_ASPNET_WEBCONFIG_CACHESIZE"), out num2))
			{
				num = num2;
				flag = true;
				Console.WriteLine("WebConfigurationManager's LRUcache Size overriden to: {0} (via {1})", num2, "MONO_ASPNET_WEBCONFIG_CACHESIZE");
			}
			WebConfigurationManager.sectionCache = new LruCache<int, object>(num);
			string text = "WebConfigurationManager's LRUcache evictions count reached its max size";
			if (!flag)
			{
				text += string.Format("{0}Cache Size: {1} (overridable via {2})", Environment.NewLine, num, "MONO_ASPNET_WEBCONFIG_CACHESIZE");
			}
			WebConfigurationManager.sectionCache.EvictionWarning = text;
			WebConfigurationManager.configFactory = ConfigurationManager.ConfigurationFactory;
			Configuration.SaveStart += WebConfigurationManager.ConfigurationSaveHandler;
			Configuration.SaveEnd += WebConfigurationManager.ConfigurationSaveHandler;
			Type type = Type.GetType("System.Configuration.CustomizableFileSettingsProvider, System", false);
			if (type != null)
			{
				FieldInfo field = type.GetField("webConfigurationFileMapType", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null && field.FieldType == Type.GetType("System.Type"))
				{
					field.SetValue(null, typeof(ApplicationSettingsConfigurationFileMap));
				}
			}
			WebConfigurationManager.sectionCacheLock = new ReaderWriterLockSlim();
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x000A9D84 File Offset: 0x000A7F84
		private static void ReenableWatcherOnConfigLocation(object state)
		{
			string text = state as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			object obj = WebConfigurationManager.saveLocationsCacheLock;
			DateTime minValue;
			lock (obj)
			{
				if (!WebConfigurationManager.saveLocationsCache.TryGetValue(text, out minValue))
				{
					minValue = DateTime.MinValue;
				}
			}
			DateTime now = DateTime.Now;
			if (minValue == DateTime.MinValue || now.Subtract(minValue).TotalMilliseconds >= 6000.0)
			{
				WebConfigurationManager.saveLocationsTimer.Dispose();
				WebConfigurationManager.saveLocationsTimer = null;
				HttpApplicationFactory.EnableWatcher(VirtualPathUtility.RemoveTrailingSlash(HttpRuntime.AppDomainAppPath), "?eb.?onfig");
				return;
			}
			WebConfigurationManager.saveLocationsTimer.Change(6000, 6000);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000A9E50 File Offset: 0x000A8050
		private static void ConfigurationSaveHandler(Configuration sender, ConfigurationSaveEventArgs args)
		{
			try
			{
				WebConfigurationManager.sectionCacheLock.EnterWriteLock();
				WebConfigurationManager.sectionCache.Clear();
			}
			finally
			{
				WebConfigurationManager.sectionCacheLock.ExitWriteLock();
			}
			object obj = WebConfigurationManager.suppressAppReloadLock;
			lock (obj)
			{
				string webConfigFileName = WebConfigurationHost.GetWebConfigFileName(HttpRuntime.AppDomainAppPath);
				if (string.Compare(args.StreamPath, webConfigFileName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					WebConfigurationManager.SuppressAppReload(args.Start);
					if (args.Start)
					{
						HttpApplicationFactory.DisableWatcher(VirtualPathUtility.RemoveTrailingSlash(HttpRuntime.AppDomainAppPath), "?eb.?onfig");
						object obj2 = WebConfigurationManager.saveLocationsCacheLock;
						lock (obj2)
						{
							if (WebConfigurationManager.saveLocationsCache == null)
							{
								WebConfigurationManager.saveLocationsCache = new Dictionary<string, DateTime>(StringComparer.Ordinal);
							}
							if (WebConfigurationManager.saveLocationsCache.ContainsKey(webConfigFileName))
							{
								WebConfigurationManager.saveLocationsCache[webConfigFileName] = DateTime.Now;
							}
							else
							{
								WebConfigurationManager.saveLocationsCache.Add(webConfigFileName, DateTime.Now);
							}
							if (WebConfigurationManager.saveLocationsTimer == null)
							{
								WebConfigurationManager.saveLocationsTimer = new Timer(new TimerCallback(WebConfigurationManager.ReenableWatcherOnConfigLocation), webConfigFileName, 6000, 6000);
							}
						}
					}
				}
			}
		}

		/// <summary>Opens the machine-configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041CD RID: 16845 RVA: 0x000A9F98 File Offset: 0x000A8198
		public static Configuration OpenMachineConfiguration()
		{
			return ConfigurationManager.OpenMachineConfiguration();
		}

		/// <summary>Opens the machine-configuration file on the current computer as a <see cref="T:System.Configuration.Configuration" /> object to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the machine configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041CE RID: 16846 RVA: 0x000A9F9F File Offset: 0x000A819F
		[global::System.MonoLimitation("locationSubPath is not handled")]
		public static Configuration OpenMachineConfiguration(string locationSubPath)
		{
			return WebConfigurationManager.OpenMachineConfiguration();
		}

		/// <summary>Opens the specified machine-configuration file on the specified server as a <see cref="T:System.Configuration.Configuration" /> object to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the configuration applies.</param>
		/// <param name="server">The fully qualified name of the server to return the configuration for.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041CF RID: 16847 RVA: 0x000A9FA6 File Offset: 0x000A81A6
		[global::System.MonoLimitation("Mono does not support remote configuration")]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server)
		{
			if (server == null)
			{
				return WebConfigurationManager.OpenMachineConfiguration(locationSubPath);
			}
			throw new NotSupportedException("Mono doesn't support remote configuration");
		}

		/// <summary>Opens the specified machine-configuration file on the specified server as a <see cref="T:System.Configuration.Configuration" /> object, using the specified security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the configuration applies.</param>
		/// <param name="server">The fully qualified name of the server to return the configuration for.</param>
		/// <param name="userToken">An account token to use.</param>
		/// <exception cref="T:System.ArgumentException">Valid values were not supplied for the <paramref name="server" /> or <paramref name="userToken" /> parameters.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D0 RID: 16848 RVA: 0x000A9FA6 File Offset: 0x000A81A6
		[global::System.MonoLimitation("Mono does not support remote configuration")]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, IntPtr userToken)
		{
			if (server == null)
			{
				return WebConfigurationManager.OpenMachineConfiguration(locationSubPath);
			}
			throw new NotSupportedException("Mono doesn't support remote configuration");
		}

		/// <summary>Opens the specified machine-configuration file on the specified server as a <see cref="T:System.Configuration.Configuration" /> object, using the specified security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="locationSubPath">The application path to which the configuration applies. </param>
		/// <param name="server">The fully qualified name of the server to return the configuration for.</param>
		/// <param name="userName">The full user name (Domain\User) to use when opening the file.</param>
		/// <param name="password">The password for the user name.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="server" /> or <paramref name="userName" /> and <paramref name="password" /> parameters were invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D1 RID: 16849 RVA: 0x000A9FA6 File Offset: 0x000A81A6
		[global::System.MonoLimitation("Mono does not support remote configuration")]
		public static Configuration OpenMachineConfiguration(string locationSubPath, string server, string userName, string password)
		{
			if (server == null)
			{
				return WebConfigurationManager.OpenMachineConfiguration(locationSubPath);
			}
			throw new NotSupportedException("Mono doesn't support remote configuration");
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. If null, the root Web.config file is opened.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D2 RID: 16850 RVA: 0x000A9FBC File Offset: 0x000A81BC
		public static Configuration OpenWebConfiguration(string path)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, null, null, null, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path and site name to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D3 RID: 16851 RVA: 0x000A9FC9 File Offset: 0x000A81C9
		public static Configuration OpenWebConfiguration(string path, string site)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, null, null, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, and location to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D4 RID: 16852 RVA: 0x000A9FD6 File Offset: 0x000A81D6
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, null, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, location, and server to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies. </param>
		/// <param name="server">The network name of the server the Web application resides on.</param>
		/// <exception cref="T:System.ArgumentException">The server parameter was invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D5 RID: 16853 RVA: 0x000A9FE3 File Offset: 0x000A81E3
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, server, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, location, server, and security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <param name="server">The network name of the server the Web application resides on.</param>
		/// <param name="userToken">An account token to use.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="server" /> or <paramref name="userToken" /> parameters were invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D6 RID: 16854 RVA: 0x000A9FE3 File Offset: 0x000A81E3
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, IntPtr userToken)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, server, null, null);
		}

		/// <summary>Opens the Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified virtual path, site name, location, server, and security context to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies. </param>
		/// <param name="server">The network name of the server the Web application resides on.</param>
		/// <param name="userName">The full user name (Domain\User) to use when opening the file.</param>
		/// <param name="password">The password for the user name.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="server" /> or <paramref name="userName" /> and <paramref name="password" /> parameters were invalid.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Could not load a valid configuration file.</exception>
		// Token: 0x060041D7 RID: 16855 RVA: 0x000A9FF0 File Offset: 0x000A81F0
		public static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, string userName, string password)
		{
			return WebConfigurationManager.OpenWebConfiguration(path, site, locationSubPath, server, null, null, false);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000AA000 File Offset: 0x000A8200
		private static Configuration OpenWebConfiguration(string path, string site, string locationSubPath, string server, string userName, string password, bool fweb)
		{
			if (string.IsNullOrEmpty(path))
			{
				path = "/";
			}
			bool flag = false;
			if (!fweb && !string.IsNullOrEmpty(path))
			{
				path = WebConfigurationManager.FindWebConfig(path, out flag);
			}
			string text = string.Concat(new string[] { path, site, locationSubPath, server, userName, password });
			Configuration configuration = (Configuration)WebConfigurationManager.configurations[text];
			if (configuration == null)
			{
				configuration = WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { null, path, site, locationSubPath, server, userName, password, flag });
				WebConfigurationManager.configurations[text] = configuration;
			}
			return configuration;
		}

		/// <summary>Opens the specified Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping and virtual path to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> object to use in place of a default Web-application configuration file.</param>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041D9 RID: 16857 RVA: 0x000AA0BB File Offset: 0x000A82BB
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap, path });
		}

		/// <summary>Opens the specified Web application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping, virtual path, and site name to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> object to use in place of a default Web-application configuration-file mapping.</param>
		/// <param name="path">The virtual path to the configuration file.</param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041DA RID: 16858 RVA: 0x000AA0DF File Offset: 0x000A82DF
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap, path, site });
		}

		/// <summary>Opens the specified Web-application configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping, virtual path, site name, and location to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Web.Configuration.WebConfigurationFileMap" /> object to use in place of a default Web-application configuration-file mapping.</param>
		/// <param name="path">The virtual path to the configuration file. </param>
		/// <param name="site">The name of the application Web site, as displayed in Internet Information Services (IIS) configuration.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041DB RID: 16859 RVA: 0x000AA107 File Offset: 0x000A8307
		public static Configuration OpenMappedWebConfiguration(WebConfigurationFileMap fileMap, string path, string site, string locationSubPath)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap, path, site, locationSubPath });
		}

		/// <summary>Opens the machine-configuration file as a <see cref="T:System.Configuration.Configuration" /> object, using the specified file mapping to allow read or write operations. </summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Configuration.ConfigurationFileMap" /> object to use in place of the default machine-configuration file.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041DC RID: 16860 RVA: 0x000AA133 File Offset: 0x000A8333
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return WebConfigurationManager.ConfigurationFactory.Create(typeof(WebConfigurationHost), new object[] { fileMap });
		}

		/// <summary>Opens the machine-configuration file as a <see cref="T:System.Configuration.Configuration" /> object using the specified file mapping and location to allow read or write operations.</summary>
		/// <returns>A <see cref="T:System.Configuration.Configuration" /> object.</returns>
		/// <param name="fileMap">The <see cref="T:System.Configuration.ConfigurationFileMap" /> object to use in place of a default machine-configuration file.</param>
		/// <param name="locationSubPath">The specific resource to which the configuration applies.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041DD RID: 16861 RVA: 0x000AA153 File Offset: 0x000A8353
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap, string locationSubPath)
		{
			return WebConfigurationManager.OpenMappedMachineConfiguration(fileMap);
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000AA15C File Offset: 0x000A835C
		internal static object SafeGetSection(string sectionName, Type configSectionType)
		{
			object obj;
			try
			{
				obj = WebConfigurationManager.GetSection(sectionName);
			}
			catch (Exception)
			{
				if (configSectionType != null)
				{
					obj = Activator.CreateInstance(configSectionType);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000AA19C File Offset: 0x000A839C
		internal static object SafeGetSection(string sectionName, string path, Type configSectionType)
		{
			object obj;
			try
			{
				obj = WebConfigurationManager.GetSection(sectionName, path);
			}
			catch (Exception)
			{
				if (configSectionType != null)
				{
					obj = Activator.CreateInstance(configSectionType);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		/// <summary>Retrieves the specified configuration section from the current Web application's configuration file.</summary>
		/// <returns>The specified configuration section object, or null if the section does not exist. Remember that security restrictions exist on the use of <see cref="M:System.Web.Configuration.WebConfigurationManager.GetSection(System.String)" /> as a runtime operation. You might not be able to access a section at run time for modifications, for example.</returns>
		/// <param name="sectionName">The configuration section name.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041E0 RID: 16864 RVA: 0x000AA1DC File Offset: 0x000A83DC
		public static object GetSection(string sectionName)
		{
			HttpContext httpContext = HttpContext.Current;
			return WebConfigurationManager.GetSection(sectionName, WebConfigurationManager.GetCurrentPath(httpContext), httpContext);
		}

		/// <summary>Retrieves the specified configuration section from the Web application's configuration file at the specified location.</summary>
		/// <returns>The specified configuration section object, or null if the section does not exist. Remember that security restrictions exist on the use of <see cref="M:System.Web.Configuration.WebConfigurationManager.GetSection(System.String,System.String)" /> as a run-time operation. You might not be able to access a section at run time for modifications, for instance.</returns>
		/// <param name="sectionName">The configuration section name.</param>
		/// <param name="path">The virtual configuration file path.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called from outside a Web application.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041E1 RID: 16865 RVA: 0x000AA1FC File Offset: 0x000A83FC
		public static object GetSection(string sectionName, string path)
		{
			return WebConfigurationManager.GetSection(sectionName, path, HttpContext.Current);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000AA20C File Offset: 0x000A840C
		private static bool LookUpLocation(string relativePath, ref Configuration defaultConfiguration)
		{
			if (string.IsNullOrEmpty(relativePath))
			{
				return false;
			}
			Configuration configuration = defaultConfiguration.FindLocationConfiguration(relativePath, defaultConfiguration);
			if (configuration == defaultConfiguration)
			{
				return false;
			}
			defaultConfiguration = configuration;
			return true;
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000AA23C File Offset: 0x000A843C
		internal static object GetSection(string sectionName, string path, HttpContext context)
		{
			if (string.IsNullOrEmpty(sectionName))
			{
				return null;
			}
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration(path, null, null, null, null, null, false);
			string configPath = configuration.ConfigPath;
			int num = 0;
			bool flag = !string.IsNullOrEmpty(path);
			string text = null;
			if (flag)
			{
				text = "location_" + path;
			}
			num = sectionName.GetHashCode();
			if (configPath != null)
			{
				num ^= configPath.GetHashCode();
			}
			int num2;
			try
			{
				WebConfigurationManager.sectionCacheLock.EnterWriteLock();
				object obj;
				if (flag)
				{
					num2 = num ^ text.GetHashCode();
					if (WebConfigurationManager.sectionCache.TryGetValue(num2, out obj))
					{
						return obj;
					}
					num2 = num ^ path.GetHashCode();
					if (WebConfigurationManager.sectionCache.TryGetValue(num2, out obj))
					{
						return obj;
					}
				}
				if (WebConfigurationManager.sectionCache.TryGetValue(num, out obj))
				{
					return obj;
				}
			}
			finally
			{
				WebConfigurationManager.sectionCacheLock.ExitWriteLock();
			}
			string text2 = null;
			if (flag)
			{
				string text3;
				if (VirtualPathUtility.IsRooted(path))
				{
					if (path[0] == '~')
					{
						text3 = ((path.Length > 1) ? path.Substring(2) : string.Empty);
					}
					else if (path[0] == '/')
					{
						text3 = path.Substring(1);
					}
					else
					{
						text3 = path;
					}
				}
				else
				{
					text3 = path;
				}
				HttpRequest httpRequest = ((context != null) ? context.Request : null);
				if (httpRequest != null)
				{
					string text4 = VirtualPathUtility.GetDirectory(httpRequest.PathNoValidation);
					if (text4 != null)
					{
						text4 = text4.TrimEnd(WebConfigurationManager.pathTrimChars);
						if (string.Compare(configuration.ConfigPath, text4, StringComparison.Ordinal) != 0 && WebConfigurationManager.LookUpLocation(text4.Trim(WebConfigurationManager.pathTrimChars), ref configuration))
						{
							text2 = path;
						}
					}
				}
				if (WebConfigurationManager.LookUpLocation(text3, ref configuration))
				{
					text2 = text;
				}
				else
				{
					text2 = path;
				}
			}
			object obj2 = WebConfigurationManager.getSectionLock;
			ConfigurationSection section;
			lock (obj2)
			{
				section = configuration.GetSection(sectionName);
			}
			if (section == null)
			{
				return null;
			}
			object obj3 = SettingsMappingManager.MapSection(section.GetRuntimeObject());
			if (text2 != null)
			{
				num2 = num ^ text2.GetHashCode();
			}
			else
			{
				num2 = num;
			}
			WebConfigurationManager.AddSectionToCache(num2, obj3);
			return obj3;
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000AA450 File Offset: 0x000A8650
		private static string MapPath(HttpRequest req, string virtualPath)
		{
			if (req != null)
			{
				return req.MapPath(virtualPath);
			}
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			if (string.IsNullOrEmpty(appDomainAppVirtualPath) || !virtualPath.StartsWith(appDomainAppVirtualPath, StringComparison.Ordinal))
			{
				return null;
			}
			if (string.Compare(virtualPath, appDomainAppVirtualPath, StringComparison.Ordinal) == 0)
			{
				return HttpRuntime.AppDomainAppPath;
			}
			return UrlUtils.Combine(HttpRuntime.AppDomainAppPath, virtualPath.Substring(appDomainAppVirtualPath.Length));
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000AA4A8 File Offset: 0x000A86A8
		private static string GetParentDir(string rootPath, string curPath)
		{
			int num = curPath.Length - 1;
			if (num > 0 && curPath[num] == '/')
			{
				curPath = curPath.Substring(0, num);
			}
			if (string.Compare(curPath, rootPath, StringComparison.Ordinal) == 0)
			{
				return null;
			}
			int num2 = curPath.LastIndexOf('/');
			if (num2 == -1)
			{
				return curPath;
			}
			if (num2 == 0)
			{
				return "/";
			}
			return curPath.Substring(0, num2);
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000AA504 File Offset: 0x000A8704
		internal static string FindWebConfig(string path)
		{
			bool flag;
			return WebConfigurationManager.FindWebConfig(path, out flag);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000AA51C File Offset: 0x000A871C
		internal static string FindWebConfig(string path, out bool inAnotherApp)
		{
			inAnotherApp = false;
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			if (HostingEnvironment.VirtualPathProvider != null && HostingEnvironment.VirtualPathProvider.DirectoryExists(path))
			{
				path = VirtualPathUtility.AppendTrailingSlash(path);
			}
			string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
			WebConfigurationManager.ConfigPath configPath = WebConfigurationManager.configPaths[path] as WebConfigurationManager.ConfigPath;
			if (configPath != null)
			{
				inAnotherApp = configPath.InAnotherApp;
				return configPath.Path;
			}
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			string text = ((httpRequest != null) ? VirtualPathUtility.AppendTrailingSlash(WebConfigurationManager.MapPath(httpRequest, path)) : null);
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (text != null && appDomainAppPath != null && !text.StartsWith(appDomainAppPath, StringComparison.Ordinal))
			{
				inAnotherApp = true;
			}
			string text2;
			if (inAnotherApp || path[path.Length - 1] == '/')
			{
				text2 = path;
			}
			else
			{
				text2 = VirtualPathUtility.GetDirectory(path, false);
				if (text2 == null)
				{
					return path;
				}
			}
			configPath = WebConfigurationManager.configPaths[text2] as WebConfigurationManager.ConfigPath;
			if (configPath != null)
			{
				inAnotherApp = configPath.InAnotherApp;
				return configPath.Path;
			}
			if (httpRequest == null)
			{
				return path;
			}
			configPath = new WebConfigurationManager.ConfigPath(path, inAnotherApp);
			while (string.Compare(configPath.Path, appDomainAppVirtualPath, StringComparison.Ordinal) != 0)
			{
				text = WebConfigurationManager.MapPath(httpRequest, configPath.Path);
				if (text == null)
				{
					configPath.Path = appDomainAppVirtualPath;
					break;
				}
				if (WebConfigurationHost.GetWebConfigFileName(text) != null)
				{
					break;
				}
				configPath.Path = WebConfigurationManager.GetParentDir(appDomainAppVirtualPath, configPath.Path);
				if (configPath.Path == null || configPath.Path == "~")
				{
					configPath.Path = appDomainAppVirtualPath;
					break;
				}
			}
			if (string.Compare(configPath.Path, path, StringComparison.Ordinal) != 0)
			{
				WebConfigurationManager.configPaths[path] = configPath;
			}
			else
			{
				WebConfigurationManager.configPaths[text2] = configPath;
			}
			return configPath.Path;
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000AA6B8 File Offset: 0x000A88B8
		private static string GetCurrentPath(HttpContext ctx)
		{
			HttpRequest httpRequest = ((ctx != null) ? ctx.Request : null);
			if (httpRequest == null)
			{
				return HttpRuntime.AppDomainAppVirtualPath;
			}
			return httpRequest.PathNoValidation;
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000AA6E4 File Offset: 0x000A88E4
		internal static bool SuppressAppReload(bool newValue)
		{
			object obj = WebConfigurationManager.suppressAppReloadLock;
			bool flag2;
			lock (obj)
			{
				flag2 = WebConfigurationManager.suppressAppReload;
				WebConfigurationManager.suppressAppReload = newValue;
			}
			return flag2;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000AA72C File Offset: 0x000A892C
		internal static void RemoveConfigurationFromCache(HttpContext ctx)
		{
			WebConfigurationManager.configurations.Remove(WebConfigurationManager.GetCurrentPath(ctx));
		}

		/// <summary>Retrieves the specified configuration section from the current Web application's configuration file.</summary>
		/// <returns>The specified configuration section object, or null if the section does not exist, or an internal object if the section is not accessible at run time.</returns>
		/// <param name="sectionName">The configuration section name.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid configuration file could not be loaded.</exception>
		// Token: 0x060041EB RID: 16875 RVA: 0x000AA740 File Offset: 0x000A8940
		public static object GetWebApplicationSection(string sectionName)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			string text = ((httpRequest != null) ? httpRequest.ApplicationPath : null);
			return WebConfigurationManager.GetSection(sectionName, string.IsNullOrEmpty(text) ? string.Empty : text);
		}

		/// <summary>Gets the Web site's application settings.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains the <see cref="T:System.Configuration.AppSettingsSection" /> object for the current Web application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid <see cref="T:System.Collections.Specialized.NameValueCollection" /> object could not be retrieved with the application settings data.</exception>
		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060041EC RID: 16876 RVA: 0x000AA783 File Offset: 0x000A8983
		public static NameValueCollection AppSettings
		{
			get
			{
				return ConfigurationManager.AppSettings;
			}
		}

		/// <summary>Gets the Web site's connection strings.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object that contains the contents of the <see cref="T:System.Configuration.ConnectionStringsSection" /> object for the current Web application's default configuration. </returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">A valid <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object could not be retrieved.</exception>
		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060041ED RID: 16877 RVA: 0x000AA78A File Offset: 0x000A898A
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060041EE RID: 16878 RVA: 0x000AA791 File Offset: 0x000A8991
		internal static IInternalConfigConfigurationFactory ConfigurationFactory
		{
			get
			{
				return WebConfigurationManager.configFactory;
			}
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000AA798 File Offset: 0x000A8998
		private static void AddSectionToCache(int key, object section)
		{
			bool flag = false;
			try
			{
				if (WebConfigurationManager.sectionCacheLock.TryEnterWriteLock(200))
				{
					flag = true;
					object obj;
					if (!WebConfigurationManager.sectionCache.TryGetValue(key, out obj) || obj == null)
					{
						WebConfigurationManager.sectionCache.Add(key, section);
					}
				}
			}
			finally
			{
				if (flag)
				{
					WebConfigurationManager.sectionCacheLock.ExitWriteLock();
				}
			}
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000AA7FC File Offset: 0x000A89FC
		internal static void Init()
		{
			object obj = WebConfigurationManager.lockobj;
			lock (obj)
			{
				if (WebConfigurationManager.config == null)
				{
					Web20DefaultConfig instance = Web20DefaultConfig.GetInstance();
					MethodInfo method = typeof(ConfigurationSettings).GetMethod("ChangeConfigurationSystem", BindingFlags.Static | BindingFlags.NonPublic);
					if (method == null)
					{
						throw new ConfigurationException("Cannot find method CCS");
					}
					object[] array = new object[] { instance };
					WebConfigurationManager.oldConfig = (IConfigurationSystem)method.Invoke(null, array);
					WebConfigurationManager.config = instance;
					WebConfigurationManager.config.Init();
					HttpConfigurationSystem httpConfigurationSystem = new HttpConfigurationSystem();
					MethodInfo method2 = typeof(ConfigurationManager).GetMethod("ChangeConfigurationSystem", BindingFlags.Static | BindingFlags.NonPublic);
					if (method2 == null)
					{
						throw new ConfigurationException("Cannot find method CCS");
					}
					object[] array2 = new object[] { httpConfigurationSystem };
					method2.Invoke(null, array2);
				}
			}
		}

		// Token: 0x04002347 RID: 9031
		private const int SAVE_LOCATIONS_CHECK_INTERVAL = 6000;

		// Token: 0x04002348 RID: 9032
		private const int SECTION_CACHE_LOCK_TIMEOUT = 200;

		// Token: 0x04002349 RID: 9033
		private static readonly char[] pathTrimChars = new char[] { '/' };

		// Token: 0x0400234A RID: 9034
		private static readonly object suppressAppReloadLock = new object();

		// Token: 0x0400234B RID: 9035
		private static readonly object saveLocationsCacheLock = new object();

		// Token: 0x0400234C RID: 9036
		private static readonly object getSectionLock = new object();

		// Token: 0x0400234D RID: 9037
		private static readonly ReaderWriterLockSlim sectionCacheLock;

		// Token: 0x0400234E RID: 9038
		private static IInternalConfigConfigurationFactory configFactory;

		// Token: 0x0400234F RID: 9039
		private static Hashtable configurations = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002350 RID: 9040
		private static Hashtable configPaths = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04002351 RID: 9041
		private static bool suppressAppReload;

		// Token: 0x04002352 RID: 9042
		private static Dictionary<string, DateTime> saveLocationsCache;

		// Token: 0x04002353 RID: 9043
		private static Timer saveLocationsTimer;

		// Token: 0x04002354 RID: 9044
		private static ArrayList extra_assemblies = null;

		// Token: 0x04002355 RID: 9045
		private const int DEFAULT_SECTION_CACHE_SIZE = 100;

		// Token: 0x04002356 RID: 9046
		private const string CACHE_SIZE_OVERRIDING_KEY = "MONO_ASPNET_WEBCONFIG_CACHESIZE";

		// Token: 0x04002357 RID: 9047
		private static LruCache<int, object> sectionCache;

		// Token: 0x04002358 RID: 9048
		internal static IConfigurationSystem oldConfig;

		// Token: 0x04002359 RID: 9049
		private static Web20DefaultConfig config;

		// Token: 0x0400235A RID: 9050
		private const BindingFlags privStatic = BindingFlags.Static | BindingFlags.NonPublic;

		// Token: 0x0400235B RID: 9051
		private static readonly object lockobj = new object();

		// Token: 0x020005EC RID: 1516
		private sealed class ConfigPath
		{
			// Token: 0x060041F1 RID: 16881 RVA: 0x000AA8F0 File Offset: 0x000A8AF0
			public ConfigPath(string path, bool inAnotherApp)
			{
				this.Path = path;
				this.InAnotherApp = inAnotherApp;
			}

			// Token: 0x0400235C RID: 9052
			public string Path;

			// Token: 0x0400235D RID: 9053
			public bool InAnotherApp;
		}
	}
}
