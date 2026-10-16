using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000CA RID: 202
	internal enum ResolverError
	{
		// Token: 0x04000396 RID: 918
		NoError,
		// Token: 0x04000397 RID: 919
		FormatError,
		// Token: 0x04000398 RID: 920
		ServerFailure,
		// Token: 0x04000399 RID: 921
		NameError,
		// Token: 0x0400039A RID: 922
		NotImplemented,
		// Token: 0x0400039B RID: 923
		Refused,
		// Token: 0x0400039C RID: 924
		ResponseHeaderError,
		// Token: 0x0400039D RID: 925
		ResponseFormatError,
		// Token: 0x0400039E RID: 926
		Timeout
	}
}
