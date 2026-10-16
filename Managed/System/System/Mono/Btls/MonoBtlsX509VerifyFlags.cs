using System;

namespace Mono.Btls
{
	// Token: 0x02000112 RID: 274
	[Flags]
	internal enum MonoBtlsX509VerifyFlags
	{
		// Token: 0x0400048B RID: 1163
		DEFAULT = 0,
		// Token: 0x0400048C RID: 1164
		CRL_CHECK = 1,
		// Token: 0x0400048D RID: 1165
		CRL_CHECK_ALL = 2,
		// Token: 0x0400048E RID: 1166
		X509_STRIC = 4
	}
}
