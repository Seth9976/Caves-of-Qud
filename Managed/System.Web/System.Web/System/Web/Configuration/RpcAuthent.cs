using System;

namespace System.Web.Configuration
{
	// Token: 0x02000570 RID: 1392
	internal enum RpcAuthent
	{
		// Token: 0x04002044 RID: 8260
		None,
		// Token: 0x04002045 RID: 8261
		DcePrivate,
		// Token: 0x04002046 RID: 8262
		DcePublic,
		// Token: 0x04002047 RID: 8263
		DecPublic = 4,
		// Token: 0x04002048 RID: 8264
		GssNegotiate = 9,
		// Token: 0x04002049 RID: 8265
		WinNT,
		// Token: 0x0400204A RID: 8266
		GssSchannel = 14,
		// Token: 0x0400204B RID: 8267
		GssKerberos = 16,
		// Token: 0x0400204C RID: 8268
		DPA,
		// Token: 0x0400204D RID: 8269
		MSN,
		// Token: 0x0400204E RID: 8270
		Digest = 21,
		// Token: 0x0400204F RID: 8271
		MQ = 100,
		// Token: 0x04002050 RID: 8272
		Default = -1
	}
}
