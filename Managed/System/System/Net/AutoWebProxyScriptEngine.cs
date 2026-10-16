using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x02000661 RID: 1633
	internal class AutoWebProxyScriptEngine
	{
		// Token: 0x0600339D RID: 13213 RVA: 0x0000219B File Offset: 0x0000039B
		public AutoWebProxyScriptEngine(WebProxy proxy, bool useRegistry)
		{
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600339E RID: 13214 RVA: 0x000B3CDF File Offset: 0x000B1EDF
		// (set) Token: 0x0600339F RID: 13215 RVA: 0x000B3CE7 File Offset: 0x000B1EE7
		public Uri AutomaticConfigurationScript { get; set; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060033A0 RID: 13216 RVA: 0x000B3CF0 File Offset: 0x000B1EF0
		// (set) Token: 0x060033A1 RID: 13217 RVA: 0x000B3CF8 File Offset: 0x000B1EF8
		public bool AutomaticallyDetectSettings { get; set; }

		// Token: 0x060033A2 RID: 13218 RVA: 0x000B3D04 File Offset: 0x000B1F04
		public bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			int num = 0;
			return this.GetProxies(destination, out proxyList, ref num);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000B3D1D File Offset: 0x000B1F1D
		public bool GetProxies(Uri destination, out IList<string> proxyList, ref int syncStatus)
		{
			proxyList = null;
			return false;
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x00003917 File Offset: 0x00001B17
		public void Close()
		{
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x00003917 File Offset: 0x00001B17
		public void Abort(ref int syncStatus)
		{
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x00003917 File Offset: 0x00001B17
		public void CheckForChanges()
		{
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000B3D24 File Offset: 0x000B1F24
		public WebProxyData GetWebProxyData()
		{
			WebProxyData webProxyData;
			if (AutoWebProxyScriptEngine.IsWindows())
			{
				webProxyData = this.InitializeRegistryGlobalProxy();
				if (webProxyData != null)
				{
					return webProxyData;
				}
			}
			webProxyData = this.ReadEnvVariables();
			return webProxyData ?? new WebProxyData();
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000B3D58 File Offset: 0x000B1F58
		private WebProxyData ReadEnvVariables()
		{
			string text = Environment.GetEnvironmentVariable("http_proxy") ?? Environment.GetEnvironmentVariable("HTTP_PROXY");
			if (text != null)
			{
				try
				{
					if (!text.StartsWith("http://"))
					{
						text = "http://" + text;
					}
					Uri uri = new Uri(text);
					IPAddress ipaddress;
					if (IPAddress.TryParse(uri.Host, out ipaddress))
					{
						if (IPAddress.Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "127.0.0.1"
							}.Uri;
						}
						else if (IPAddress.IPv6Any.Equals(ipaddress))
						{
							uri = new UriBuilder(uri)
							{
								Host = "[::1]"
							}.Uri;
						}
					}
					bool flag = false;
					ArrayList arrayList = new ArrayList();
					string text2 = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
					if (text2 != null)
					{
						foreach (string text3 in text2.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
						{
							if (text3 != "*.local")
							{
								arrayList.Add(text3);
							}
							else
							{
								flag = true;
							}
						}
					}
					return new WebProxyData
					{
						proxyAddress = uri,
						bypassOnLocal = flag,
						bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
					};
				}
				catch (UriFormatException)
				{
				}
			}
			return null;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000B3EB8 File Offset: 0x000B20B8
		private static bool IsWindows()
		{
			return Environment.OSVersion.Platform < PlatformID.Unix;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000B3EC8 File Offset: 0x000B20C8
		private WebProxyData InitializeRegistryGlobalProxy()
		{
			if ((int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable", 0) <= 0)
			{
				return null;
			}
			string text = "";
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			string text2 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer", null);
			if (text2 == null)
			{
				return null;
			}
			string text3 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyOverride", null);
			if (text2.Contains("="))
			{
				foreach (string text4 in text2.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text4.StartsWith("http="))
					{
						text = text4.Substring(5);
						break;
					}
				}
			}
			else
			{
				text = text2;
			}
			if (text3 != null)
			{
				foreach (string text5 in text3.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text5 != "<local>")
					{
						arrayList.Add(text5);
					}
					else
					{
						flag = true;
					}
				}
			}
			return new WebProxyData
			{
				proxyAddress = AutoWebProxyScriptEngine.ToUri(text),
				bypassOnLocal = flag,
				bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
			};
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000B4001 File Offset: 0x000B2201
		private static Uri ToUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://", StringComparison.Ordinal) == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000B402C File Offset: 0x000B222C
		private static ArrayList CreateBypassList(ArrayList al)
		{
			string[] array = al.ToArray(typeof(string)) as string[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "^" + Regex.Escape(array[i]).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			}
			return new ArrayList(array);
		}
	}
}
