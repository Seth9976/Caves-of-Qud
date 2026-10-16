using System;

namespace System.Net
{
	// Token: 0x0200063A RID: 1594
	internal class StaticProxy : ProxyChain
	{
		// Token: 0x0600323F RID: 12863 RVA: 0x000ADB72 File Offset: 0x000ABD72
		internal StaticProxy(Uri destination, Uri proxy)
			: base(destination)
		{
			if (proxy == null)
			{
				throw new ArgumentNullException("proxy");
			}
			this.m_Proxy = proxy;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000ADB96 File Offset: 0x000ABD96
		protected override bool GetNextProxy(out Uri proxy)
		{
			proxy = this.m_Proxy;
			if (proxy == null)
			{
				return false;
			}
			this.m_Proxy = null;
			return true;
		}

		// Token: 0x04001D7C RID: 7548
		private Uri m_Proxy;
	}
}
