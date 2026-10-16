using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x0200065F RID: 1631
	internal class WebProxyData
	{
		// Token: 0x04001E3C RID: 7740
		internal bool bypassOnLocal;

		// Token: 0x04001E3D RID: 7741
		internal bool automaticallyDetectSettings;

		// Token: 0x04001E3E RID: 7742
		internal Uri proxyAddress;

		// Token: 0x04001E3F RID: 7743
		internal Hashtable proxyHostAddresses;

		// Token: 0x04001E40 RID: 7744
		internal Uri scriptLocation;

		// Token: 0x04001E41 RID: 7745
		internal ArrayList bypassList;
	}
}
