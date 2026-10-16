using System;

namespace System.Net
{
	// Token: 0x0200064A RID: 1610
	[Serializable]
	internal sealed class EmptyWebProxy : IAutoWebProxy, IWebProxy
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x00003914 File Offset: 0x00001B14
		public Uri GetProxy(Uri uri)
		{
			return uri;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x0000390E File Offset: 0x00001B0E
		public bool IsBypassed(Uri uri)
		{
			return true;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000AEEE8 File Offset: 0x000AD0E8
		// (set) Token: 0x0600328E RID: 12942 RVA: 0x000AEEF0 File Offset: 0x000AD0F0
		public ICredentials Credentials
		{
			get
			{
				return this.m_credentials;
			}
			set
			{
				this.m_credentials = value;
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000AEEF9 File Offset: 0x000AD0F9
		ProxyChain IAutoWebProxy.GetProxies(Uri destination)
		{
			return new DirectProxy(destination);
		}

		// Token: 0x04001DAC RID: 7596
		[NonSerialized]
		private ICredentials m_credentials;
	}
}
