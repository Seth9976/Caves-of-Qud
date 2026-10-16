using System;

namespace System.Net
{
	// Token: 0x02000639 RID: 1593
	internal class DirectProxy : ProxyChain
	{
		// Token: 0x0600323D RID: 12861 RVA: 0x000ADB52 File Offset: 0x000ABD52
		internal DirectProxy(Uri destination)
			: base(destination)
		{
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000ADB5B File Offset: 0x000ABD5B
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = null;
			if (this.m_ProxyRetrieved)
			{
				return false;
			}
			this.m_ProxyRetrieved = true;
			return true;
		}

		// Token: 0x04001D7B RID: 7547
		private bool m_ProxyRetrieved;
	}
}
