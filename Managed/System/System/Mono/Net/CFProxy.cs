using System;

namespace Mono.Net
{
	// Token: 0x02000083 RID: 131
	internal class CFProxy
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00005684 File Offset: 0x00003884
		static CFProxy()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreServices.framework/Frameworks/CFNetwork.framework/CFNetwork", 0);
			CFProxy.kCFProxyAutoConfigurationJavaScriptKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyAutoConfigurationJavaScriptKey");
			CFProxy.kCFProxyAutoConfigurationURLKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyAutoConfigurationURLKey");
			CFProxy.kCFProxyHostNameKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyHostNameKey");
			CFProxy.kCFProxyPasswordKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyPasswordKey");
			CFProxy.kCFProxyPortNumberKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyPortNumberKey");
			CFProxy.kCFProxyTypeKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeKey");
			CFProxy.kCFProxyUsernameKey = CFObject.GetCFObjectHandle(intPtr, "kCFProxyUsernameKey");
			CFProxy.kCFProxyTypeAutoConfigurationURL = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeAutoConfigurationURL");
			CFProxy.kCFProxyTypeAutoConfigurationJavaScript = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeAutoConfigurationJavaScript");
			CFProxy.kCFProxyTypeFTP = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeFTP");
			CFProxy.kCFProxyTypeHTTP = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeHTTP");
			CFProxy.kCFProxyTypeHTTPS = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeHTTPS");
			CFProxy.kCFProxyTypeSOCKS = CFObject.GetCFObjectHandle(intPtr, "kCFProxyTypeSOCKS");
			CFObject.dlclose(intPtr);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005771 File Offset: 0x00003971
		internal CFProxy(CFDictionary settings)
		{
			this.settings = settings;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005780 File Offset: 0x00003980
		private static CFProxyType CFProxyTypeToEnum(IntPtr type)
		{
			if (type == CFProxy.kCFProxyTypeAutoConfigurationJavaScript)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (type == CFProxy.kCFProxyTypeAutoConfigurationURL)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (type == CFProxy.kCFProxyTypeFTP)
			{
				return CFProxyType.FTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTP)
			{
				return CFProxyType.HTTP;
			}
			if (type == CFProxy.kCFProxyTypeHTTPS)
			{
				return CFProxyType.HTTPS;
			}
			if (type == CFProxy.kCFProxyTypeSOCKS)
			{
				return CFProxyType.SOCKS;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeAutoConfigurationJavaScript, 0) == 0)
			{
				return CFProxyType.AutoConfigurationJavaScript;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeAutoConfigurationURL, 0) == 0)
			{
				return CFProxyType.AutoConfigurationUrl;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeFTP, 0) == 0)
			{
				return CFProxyType.FTP;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeHTTP, 0) == 0)
			{
				return CFProxyType.HTTP;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeHTTPS, 0) == 0)
			{
				return CFProxyType.HTTPS;
			}
			if (CFString.Compare(type, CFProxy.kCFProxyTypeSOCKS, 0) == 0)
			{
				return CFProxyType.SOCKS;
			}
			return CFProxyType.None;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00005848 File Offset: 0x00003A48
		public IntPtr AutoConfigurationJavaScript
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationJavaScriptKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationJavaScriptKey];
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00005871 File Offset: 0x00003A71
		public IntPtr AutoConfigurationUrl
		{
			get
			{
				if (CFProxy.kCFProxyAutoConfigurationURLKey == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return this.settings[CFProxy.kCFProxyAutoConfigurationURLKey];
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000589A File Offset: 0x00003A9A
		public string HostName
		{
			get
			{
				if (CFProxy.kCFProxyHostNameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyHostNameKey]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000058C4 File Offset: 0x00003AC4
		public string Password
		{
			get
			{
				if (CFProxy.kCFProxyPasswordKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyPasswordKey]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000058EE File Offset: 0x00003AEE
		public int Port
		{
			get
			{
				if (CFProxy.kCFProxyPortNumberKey == IntPtr.Zero)
				{
					return 0;
				}
				return CFNumber.AsInt32(this.settings[CFProxy.kCFProxyPortNumberKey]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00005918 File Offset: 0x00003B18
		public CFProxyType ProxyType
		{
			get
			{
				if (CFProxy.kCFProxyTypeKey == IntPtr.Zero)
				{
					return CFProxyType.None;
				}
				return CFProxy.CFProxyTypeToEnum(this.settings[CFProxy.kCFProxyTypeKey]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005942 File Offset: 0x00003B42
		public string Username
		{
			get
			{
				if (CFProxy.kCFProxyUsernameKey == IntPtr.Zero)
				{
					return null;
				}
				return CFString.AsString(this.settings[CFProxy.kCFProxyUsernameKey]);
			}
		}

		// Token: 0x040001FB RID: 507
		private static IntPtr kCFProxyAutoConfigurationJavaScriptKey;

		// Token: 0x040001FC RID: 508
		private static IntPtr kCFProxyAutoConfigurationURLKey;

		// Token: 0x040001FD RID: 509
		private static IntPtr kCFProxyHostNameKey;

		// Token: 0x040001FE RID: 510
		private static IntPtr kCFProxyPasswordKey;

		// Token: 0x040001FF RID: 511
		private static IntPtr kCFProxyPortNumberKey;

		// Token: 0x04000200 RID: 512
		private static IntPtr kCFProxyTypeKey;

		// Token: 0x04000201 RID: 513
		private static IntPtr kCFProxyUsernameKey;

		// Token: 0x04000202 RID: 514
		private static IntPtr kCFProxyTypeAutoConfigurationURL;

		// Token: 0x04000203 RID: 515
		private static IntPtr kCFProxyTypeAutoConfigurationJavaScript;

		// Token: 0x04000204 RID: 516
		private static IntPtr kCFProxyTypeFTP;

		// Token: 0x04000205 RID: 517
		private static IntPtr kCFProxyTypeHTTP;

		// Token: 0x04000206 RID: 518
		private static IntPtr kCFProxyTypeHTTPS;

		// Token: 0x04000207 RID: 519
		private static IntPtr kCFProxyTypeSOCKS;

		// Token: 0x04000208 RID: 520
		private CFDictionary settings;
	}
}
