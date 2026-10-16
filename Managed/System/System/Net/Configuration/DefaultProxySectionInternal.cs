using System;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200075A RID: 1882
	internal sealed class DefaultProxySectionInternal
	{
		// Token: 0x06003B54 RID: 15188 RVA: 0x000CC160 File Offset: 0x000CA360
		private static IWebProxy GetDefaultProxy_UsingOldMonoCode()
		{
			DefaultProxySection defaultProxySection = ConfigurationManager.GetSection("system.net/defaultProxy") as DefaultProxySection;
			if (defaultProxySection == null)
			{
				return DefaultProxySectionInternal.GetSystemWebProxy();
			}
			ProxyElement proxy = defaultProxySection.Proxy;
			WebProxy webProxy;
			if (proxy.UseSystemDefault != ProxyElement.UseSystemDefaultValues.False && proxy.ProxyAddress == null)
			{
				IWebProxy systemWebProxy = DefaultProxySectionInternal.GetSystemWebProxy();
				if (!(systemWebProxy is WebProxy))
				{
					return systemWebProxy;
				}
				webProxy = (WebProxy)systemWebProxy;
			}
			else
			{
				webProxy = new WebProxy();
			}
			if (proxy.ProxyAddress != null)
			{
				webProxy.Address = proxy.ProxyAddress;
			}
			if (proxy.BypassOnLocal != ProxyElement.BypassOnLocalValues.Unspecified)
			{
				webProxy.BypassProxyOnLocal = proxy.BypassOnLocal == ProxyElement.BypassOnLocalValues.True;
			}
			foreach (object obj in defaultProxySection.BypassList)
			{
				BypassElement bypassElement = (BypassElement)obj;
				webProxy.BypassArrayList.Add(bypassElement.Address);
			}
			return webProxy;
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000AB946 File Offset: 0x000A9B46
		private static IWebProxy GetSystemWebProxy()
		{
			return global::System.Net.WebProxy.CreateDefaultProxy();
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000CC258 File Offset: 0x000CA458
		internal static object ClassSyncObject
		{
			get
			{
				if (DefaultProxySectionInternal.classSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref DefaultProxySectionInternal.classSyncObject, obj, null);
				}
				return DefaultProxySectionInternal.classSyncObject;
			}
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000CC284 File Offset: 0x000CA484
		internal static DefaultProxySectionInternal GetSection()
		{
			object obj = DefaultProxySectionInternal.ClassSyncObject;
			DefaultProxySectionInternal defaultProxySectionInternal;
			lock (obj)
			{
				defaultProxySectionInternal = new DefaultProxySectionInternal
				{
					webProxy = DefaultProxySectionInternal.GetDefaultProxy_UsingOldMonoCode()
				};
			}
			return defaultProxySectionInternal;
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000CC2D0 File Offset: 0x000CA4D0
		internal IWebProxy WebProxy
		{
			get
			{
				return this.webProxy;
			}
		}

		// Token: 0x04002385 RID: 9093
		private IWebProxy webProxy;

		// Token: 0x04002386 RID: 9094
		private static object classSyncObject;
	}
}
