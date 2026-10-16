using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x0200077B RID: 1915
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06003C3B RID: 15419 RVA: 0x000CDE70 File Offset: 0x000CC070
		static SettingsSection()
		{
			SettingsSection.properties.Add(SettingsSection.httpWebRequestProp);
			SettingsSection.properties.Add(SettingsSection.ipv6Prop);
			SettingsSection.properties.Add(SettingsSection.performanceCountersProp);
			SettingsSection.properties.Add(SettingsSection.servicePointManagerProp);
			SettingsSection.properties.Add(SettingsSection.socketProp);
			SettingsSection.properties.Add(SettingsSection.webProxyScriptProp);
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpWebRequestElement" /> object.The configuration element that controls the maximum response header length and other settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x000CDF77 File Offset: 0x000CC177
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[SettingsSection.httpWebRequestProp];
			}
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.Ipv6Element" />.The configuration element that controls setting used by IPv6.</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003C3E RID: 15422 RVA: 0x000CDF89 File Offset: 0x000CC189
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[SettingsSection.ipv6Prop];
			}
		}

		/// <summary>Gets the configuration element that controls whether network performance counters are enabled.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.PerformanceCountersElement" />.The configuration element that controls setting used network performance counters.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003C3F RID: 15423 RVA: 0x000CDF9B File Offset: 0x000CC19B
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[SettingsSection.performanceCountersProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ServicePointManagerElement" /> object.The configuration element that that controls setting used network performance counters for connections to remote host computers.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x000CDFAD File Offset: 0x000CC1AD
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[SettingsSection.servicePointManagerProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for sockets.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SocketElement" /> object.The configuration element that controls settings for sockets.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003C41 RID: 15425 RVA: 0x000CDFBF File Offset: 0x000CC1BF
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[SettingsSection.socketProp];
			}
		}

		/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebProxyScriptElement" /> object.The configuration element that controls settings for the execution timeout and download timeout used by the Web proxy scripts.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x000CDFD1 File Offset: 0x000CC1D1
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[SettingsSection.webProxyScriptProp];
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000CDFE3 File Offset: 0x000CC1E3
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingsSection.properties;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.Configuration.HttpListenerElement" /> object.The configuration element that controls the settings used by an <see cref="T:System.Net.HttpListener" /> object.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x00032868 File Offset: 0x00030A68
		public HttpListenerElement HttpListener
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.WebUtilityElement" />.The configuration element that controls the settings used by an <see cref="T:System.Net.WebUtility" /> object.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x00032868 File Offset: 0x00030A68
		public WebUtilityElement WebUtility
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x00032868 File Offset: 0x00030A68
		public WindowsAuthenticationElement WindowsAuthentication
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x040023E1 RID: 9185
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040023E2 RID: 9186
		private static ConfigurationProperty httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));

		// Token: 0x040023E3 RID: 9187
		private static ConfigurationProperty ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));

		// Token: 0x040023E4 RID: 9188
		private static ConfigurationProperty performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));

		// Token: 0x040023E5 RID: 9189
		private static ConfigurationProperty servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));

		// Token: 0x040023E6 RID: 9190
		private static ConfigurationProperty webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));

		// Token: 0x040023E7 RID: 9191
		private static ConfigurationProperty socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
	}
}
