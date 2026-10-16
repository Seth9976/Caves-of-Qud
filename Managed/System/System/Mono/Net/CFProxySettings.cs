using System;

namespace Mono.Net
{
	// Token: 0x02000084 RID: 132
	internal class CFProxySettings
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000596C File Offset: 0x00003B6C
		static CFProxySettings()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxySettings.kCFNetworkProxiesHTTPEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPEnable");
			CFProxySettings.kCFNetworkProxiesHTTPPort = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPPort");
			CFProxySettings.kCFNetworkProxiesHTTPProxy = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesHTTPProxy");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigEnable");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigJavaScript");
			CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString = CFObject.GetCFObjectHandle(intPtr, "kCFNetworkProxiesProxyAutoConfigURLString");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000059E9 File Offset: 0x00003BE9
		public CFProxySettings(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000059F8 File Offset: 0x00003BF8
		public CFDictionary Dictionary
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00005A00 File Offset: 0x00003C00
		public bool HTTPEnable
		{
			get
			{
				return !(CFProxySettings.kCFNetworkProxiesHTTPEnable == IntPtr.Zero) && CFNumber.AsBool(this.settings[CFProxySettings.kCFNetworkProxiesHTTPEnable]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005A2A File Offset: 0x00003C2A
		public int HTTPPort
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesHTTPPort == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxySettings.kCFNetworkProxiesHTTPPort]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00005A54 File Offset: 0x00003C54
		public string HTTPProxy
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesHTTPProxy == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesHTTPProxy]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005A7E File Offset: 0x00003C7E
		public bool ProxyAutoConfigEnable
		{
			get
			{
				return !(CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable == IntPtr.Zero) && CFNumber.AsBool(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigEnable]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public string ProxyAutoConfigJavaScript
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigJavaScript]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00005AD2 File Offset: 0x00003CD2
		public string ProxyAutoConfigURLString
		{
			get
			{
				if (CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxySettings.kCFNetworkProxiesProxyAutoConfigURLString]);
			}
		}

		// Token: 0x04000209 RID: 521
		private static IntPtr kCFNetworkProxiesHTTPEnable;

		// Token: 0x0400020A RID: 522
		private static IntPtr kCFNetworkProxiesHTTPPort;

		// Token: 0x0400020B RID: 523
		private static IntPtr kCFNetworkProxiesHTTPProxy;

		// Token: 0x0400020C RID: 524
		private static IntPtr kCFNetworkProxiesProxyAutoConfigEnable;

		// Token: 0x0400020D RID: 525
		private static IntPtr kCFNetworkProxiesProxyAutoConfigJavaScript;

		// Token: 0x0400020E RID: 526
		private static IntPtr kCFNetworkProxiesProxyAutoConfigURLString;

		// Token: 0x0400020F RID: 527
		private CFDictionary settings;
	}
}
