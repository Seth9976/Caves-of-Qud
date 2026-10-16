using System;

namespace System.Net
{
	// Token: 0x020005EF RID: 1519
	internal enum CertificateEncoding
	{
		// Token: 0x04001BAA RID: 7082
		Zero,
		// Token: 0x04001BAB RID: 7083
		X509AsnEncoding,
		// Token: 0x04001BAC RID: 7084
		X509NdrEncoding,
		// Token: 0x04001BAD RID: 7085
		Pkcs7AsnEncoding = 65536,
		// Token: 0x04001BAE RID: 7086
		Pkcs7NdrEncoding = 131072,
		// Token: 0x04001BAF RID: 7087
		AnyAsnEncoding = 65537
	}
}
