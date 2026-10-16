using System;

namespace System.Web.Security.Cryptography
{
	// Token: 0x020004D1 RID: 1233
	internal interface ICryptoService
	{
		// Token: 0x06003805 RID: 14341
		byte[] Protect(byte[] clearData);

		// Token: 0x06003806 RID: 14342
		byte[] Unprotect(byte[] protectedData);
	}
}
