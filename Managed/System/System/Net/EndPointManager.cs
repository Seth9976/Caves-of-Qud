using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000684 RID: 1668
	internal sealed class EndPointManager
	{
		// Token: 0x0600347A RID: 13434 RVA: 0x0000219B File Offset: 0x0000039B
		private EndPointManager()
		{
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000B6F44 File Offset: 0x000B5144
		public static void AddListener(HttpListener listener)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable hashtable = EndPointManager.ip_to_endpoints;
				lock (hashtable)
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.AddPrefixInternal(text, listener);
						arrayList.Add(text);
					}
				}
			}
			catch
			{
				foreach (object obj in arrayList)
				{
					EndPointManager.RemovePrefix((string)obj, listener);
				}
				throw;
			}
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000B7024 File Offset: 0x000B5224
		public static void AddPrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.AddPrefixInternal(prefix, listener);
			}
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000B7064 File Offset: 0x000B5264
		private static void AddPrefixInternal(string p, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(p);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).AddPrefix(listenerPrefix, listener);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000B70DC File Offset: 0x000B52DC
		private static EndPointListener GetEPListener(string host, int port, HttpListener listener, bool secure)
		{
			IPAddress ipaddress;
			if (host == "*")
			{
				ipaddress = IPAddress.Any;
			}
			else if (!IPAddress.TryParse(host, out ipaddress))
			{
				try
				{
					IPHostEntry hostByName = Dns.GetHostByName(host);
					if (hostByName != null)
					{
						ipaddress = hostByName.AddressList[0];
					}
					else
					{
						ipaddress = IPAddress.Any;
					}
				}
				catch
				{
					ipaddress = IPAddress.Any;
				}
			}
			Hashtable hashtable;
			if (EndPointManager.ip_to_endpoints.ContainsKey(ipaddress))
			{
				hashtable = (Hashtable)EndPointManager.ip_to_endpoints[ipaddress];
			}
			else
			{
				hashtable = new Hashtable();
				EndPointManager.ip_to_endpoints[ipaddress] = hashtable;
			}
			EndPointListener endPointListener;
			if (hashtable.ContainsKey(port))
			{
				endPointListener = (EndPointListener)hashtable[port];
			}
			else
			{
				endPointListener = new EndPointListener(listener, ipaddress, port, secure);
				hashtable[port] = endPointListener;
			}
			return endPointListener;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000B71B0 File Offset: 0x000B53B0
		public static void RemoveEndPoint(EndPointListener epl, IPEndPoint ep)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				Hashtable hashtable2 = (Hashtable)EndPointManager.ip_to_endpoints[ep.Address];
				hashtable2.Remove(ep.Port);
				if (hashtable2.Count == 0)
				{
					EndPointManager.ip_to_endpoints.Remove(ep.Address);
				}
				epl.Close();
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000B722C File Offset: 0x000B542C
		public static void RemoveListener(HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				foreach (string text in listener.Prefixes)
				{
					EndPointManager.RemovePrefixInternal(text, listener);
				}
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000B72A0 File Offset: 0x000B54A0
		public static void RemovePrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.RemovePrefixInternal(prefix, listener);
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000B72E0 File Offset: 0x000B54E0
		private static void RemovePrefixInternal(string prefix, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(prefix);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				return;
			}
			if (listenerPrefix.Path.IndexOf("//", StringComparison.Ordinal) != -1)
			{
				return;
			}
			EndPointManager.GetEPListener(listenerPrefix.Host, listenerPrefix.Port, listener, listenerPrefix.Secure).RemovePrefix(listenerPrefix, listener);
		}

		// Token: 0x04001EBA RID: 7866
		private static Hashtable ip_to_endpoints = new Hashtable();
	}
}
