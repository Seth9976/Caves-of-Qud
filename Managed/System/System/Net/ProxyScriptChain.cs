using System;

namespace System.Net
{
	// Token: 0x02000638 RID: 1592
	internal class ProxyScriptChain : ProxyChain
	{
		// Token: 0x0600323A RID: 12858 RVA: 0x000ADAA0 File Offset: 0x000ABCA0
		internal ProxyScriptChain(WebProxy proxy, Uri destination)
			: base(destination)
		{
			this.m_Proxy = proxy;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000ADAB0 File Offset: 0x000ABCB0
		protected override bool GetNextProxy(out Uri proxy)
		{
			if (this.m_CurrentIndex < 0)
			{
				proxy = null;
				return false;
			}
			if (this.m_CurrentIndex == 0)
			{
				this.m_ScriptProxies = this.m_Proxy.GetProxiesAuto(base.Destination, ref this.m_SyncStatus);
			}
			if (this.m_ScriptProxies == null || this.m_CurrentIndex >= this.m_ScriptProxies.Length)
			{
				proxy = this.m_Proxy.GetProxyAutoFailover(base.Destination);
				this.m_CurrentIndex = -1;
				return true;
			}
			Uri[] scriptProxies = this.m_ScriptProxies;
			int currentIndex = this.m_CurrentIndex;
			this.m_CurrentIndex = currentIndex + 1;
			proxy = scriptProxies[currentIndex];
			return true;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000ADB3F File Offset: 0x000ABD3F
		internal override void Abort()
		{
			this.m_Proxy.AbortGetProxiesAuto(ref this.m_SyncStatus);
		}

		// Token: 0x04001D77 RID: 7543
		private WebProxy m_Proxy;

		// Token: 0x04001D78 RID: 7544
		private Uri[] m_ScriptProxies;

		// Token: 0x04001D79 RID: 7545
		private int m_CurrentIndex;

		// Token: 0x04001D7A RID: 7546
		private int m_SyncStatus;
	}
}
