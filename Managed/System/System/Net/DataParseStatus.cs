using System;

namespace System.Net
{
	// Token: 0x02000624 RID: 1572
	internal enum DataParseStatus
	{
		// Token: 0x04001D01 RID: 7425
		NeedMoreData,
		// Token: 0x04001D02 RID: 7426
		ContinueParsing,
		// Token: 0x04001D03 RID: 7427
		Done,
		// Token: 0x04001D04 RID: 7428
		Invalid,
		// Token: 0x04001D05 RID: 7429
		DataTooBig
	}
}
