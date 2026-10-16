using System;

namespace System.Web
{
	// Token: 0x0200004E RID: 78
	public interface ITlsTokenBindingInfo
	{
		// Token: 0x060003D3 RID: 979
		byte[] GetProvidedTokenBindingId();

		// Token: 0x060003D4 RID: 980
		byte[] GetReferredTokenBindingId();
	}
}
