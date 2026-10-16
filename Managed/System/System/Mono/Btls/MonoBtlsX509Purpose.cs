using System;

namespace Mono.Btls
{
	// Token: 0x02000108 RID: 264
	internal enum MonoBtlsX509Purpose
	{
		// Token: 0x04000467 RID: 1127
		SSL_CLIENT = 1,
		// Token: 0x04000468 RID: 1128
		SSL_SERVER,
		// Token: 0x04000469 RID: 1129
		NS_SSL_SERVER,
		// Token: 0x0400046A RID: 1130
		SMIME_SIGN,
		// Token: 0x0400046B RID: 1131
		SMIME_ENCRYPT,
		// Token: 0x0400046C RID: 1132
		CRL_SIGN,
		// Token: 0x0400046D RID: 1133
		ANY,
		// Token: 0x0400046E RID: 1134
		OCSP_HELPER,
		// Token: 0x0400046F RID: 1135
		TIMESTAMP_SIGN
	}
}
