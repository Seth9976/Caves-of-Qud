using System;

namespace System.Net
{
	// Token: 0x02000627 RID: 1575
	internal enum WebParseErrorCode
	{
		// Token: 0x04001D11 RID: 7441
		Generic,
		// Token: 0x04001D12 RID: 7442
		InvalidHeaderName,
		// Token: 0x04001D13 RID: 7443
		InvalidContentLength,
		// Token: 0x04001D14 RID: 7444
		IncompleteHeaderLine,
		// Token: 0x04001D15 RID: 7445
		CrLfError,
		// Token: 0x04001D16 RID: 7446
		InvalidChunkFormat,
		// Token: 0x04001D17 RID: 7447
		UnexpectedServerResponse
	}
}
