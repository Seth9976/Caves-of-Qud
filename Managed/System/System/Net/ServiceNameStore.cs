using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200063E RID: 1598
	internal class ServiceNameStore
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x0600324B RID: 12875 RVA: 0x000ADDFB File Offset: 0x000ABFFB
		public ServiceNameCollection ServiceNames
		{
			get
			{
				if (this.serviceNameCollection == null)
				{
					this.serviceNameCollection = new ServiceNameCollection(this.serviceNames);
				}
				return this.serviceNameCollection;
			}
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000ADE1C File Offset: 0x000AC01C
		public ServiceNameStore()
		{
			this.serviceNames = new List<string>();
			this.serviceNameCollection = null;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000ADE36 File Offset: 0x000AC036
		private bool AddSingleServiceName(string spn)
		{
			spn = ServiceNameCollection.NormalizeServiceName(spn);
			if (this.Contains(spn))
			{
				return false;
			}
			this.serviceNames.Add(spn);
			return true;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000ADE58 File Offset: 0x000AC058
		public bool Add(string uriPrefix)
		{
			string[] array = this.BuildServiceNames(uriPrefix);
			bool flag = false;
			foreach (string text in array)
			{
				if (this.AddSingleServiceName(text))
				{
					flag = true;
					bool on = Logging.On;
				}
			}
			if (flag)
			{
				this.serviceNameCollection = null;
			}
			else
			{
				bool on2 = Logging.On;
			}
			return flag;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000ADEA8 File Offset: 0x000AC0A8
		public bool Remove(string uriPrefix)
		{
			string text = this.BuildSimpleServiceName(uriPrefix);
			text = ServiceNameCollection.NormalizeServiceName(text);
			bool flag = this.Contains(text);
			if (flag)
			{
				this.serviceNames.Remove(text);
				this.serviceNameCollection = null;
			}
			if (Logging.On)
			{
			}
			return flag;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000ADEED File Offset: 0x000AC0ED
		private bool Contains(string newServiceName)
		{
			return newServiceName != null && ServiceNameCollection.Contains(newServiceName, this.serviceNames);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000ADF00 File Offset: 0x000AC100
		public void Clear()
		{
			this.serviceNames.Clear();
			this.serviceNameCollection = null;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000ADF14 File Offset: 0x000AC114
		private string ExtractHostname(string uriPrefix, bool allowInvalidUriStrings)
		{
			if (Uri.IsWellFormedUriString(uriPrefix, UriKind.Absolute))
			{
				return new Uri(uriPrefix).Host;
			}
			if (allowInvalidUriStrings)
			{
				int num = uriPrefix.IndexOf("://") + 3;
				int num2 = num;
				bool flag = false;
				while (num2 < uriPrefix.Length && uriPrefix[num2] != '/' && (uriPrefix[num2] != ':' || flag))
				{
					if (uriPrefix[num2] == '[')
					{
						if (flag)
						{
							num2 = num;
							break;
						}
						flag = true;
					}
					if (flag && uriPrefix[num2] == ']')
					{
						flag = false;
					}
					num2++;
				}
				return uriPrefix.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000ADFA8 File Offset: 0x000AC1A8
		public string BuildSimpleServiceName(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, false);
			if (text != null)
			{
				return "HTTP/" + text;
			}
			return null;
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000ADFD0 File Offset: 0x000AC1D0
		public string[] BuildServiceNames(string uriPrefix)
		{
			string text = this.ExtractHostname(uriPrefix, true);
			IPAddress ipaddress = null;
			if (string.Compare(text, "*", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(text, "+", StringComparison.InvariantCultureIgnoreCase) == 0 || IPAddress.TryParse(text, out ipaddress))
			{
				try
				{
					string hostName = Dns.GetHostEntry(string.Empty).HostName;
					return new string[] { "HTTP/" + hostName };
				}
				catch (SocketException)
				{
					return new string[0];
				}
				catch (SecurityException)
				{
					return new string[0];
				}
			}
			if (!text.Contains("."))
			{
				try
				{
					string hostName2 = Dns.GetHostEntry(text).HostName;
					return new string[]
					{
						"HTTP/" + text,
						"HTTP/" + hostName2
					};
				}
				catch (SocketException)
				{
					return new string[] { "HTTP/" + text };
				}
				catch (SecurityException)
				{
					return new string[] { "HTTP/" + text };
				}
			}
			return new string[] { "HTTP/" + text };
		}

		// Token: 0x04001D85 RID: 7557
		private List<string> serviceNames;

		// Token: 0x04001D86 RID: 7558
		private ServiceNameCollection serviceNameCollection;
	}
}
