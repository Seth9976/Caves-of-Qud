using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BF RID: 191
	internal enum DnsRCode : ushort
	{
		// Token: 0x04000328 RID: 808
		NoError,
		// Token: 0x04000329 RID: 809
		FormErr,
		// Token: 0x0400032A RID: 810
		ServFail,
		// Token: 0x0400032B RID: 811
		NXDomain,
		// Token: 0x0400032C RID: 812
		NotImp,
		// Token: 0x0400032D RID: 813
		Refused,
		// Token: 0x0400032E RID: 814
		YXDomain,
		// Token: 0x0400032F RID: 815
		YXRRSet,
		// Token: 0x04000330 RID: 816
		NXRRSet,
		// Token: 0x04000331 RID: 817
		NotAuth,
		// Token: 0x04000332 RID: 818
		NotZone,
		// Token: 0x04000333 RID: 819
		BadVers = 16,
		// Token: 0x04000334 RID: 820
		BadSig = 16,
		// Token: 0x04000335 RID: 821
		BadKey,
		// Token: 0x04000336 RID: 822
		BadTime,
		// Token: 0x04000337 RID: 823
		BadMode,
		// Token: 0x04000338 RID: 824
		BadName,
		// Token: 0x04000339 RID: 825
		BadAlg,
		// Token: 0x0400033A RID: 826
		BadTrunc
	}
}
