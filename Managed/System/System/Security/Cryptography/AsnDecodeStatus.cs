using System;

namespace System.Security.Cryptography
{
	// Token: 0x020002BC RID: 700
	internal enum AsnDecodeStatus
	{
		// Token: 0x04000C67 RID: 3175
		NotDecoded = -1,
		// Token: 0x04000C68 RID: 3176
		Ok,
		// Token: 0x04000C69 RID: 3177
		BadAsn,
		// Token: 0x04000C6A RID: 3178
		BadTag,
		// Token: 0x04000C6B RID: 3179
		BadLength,
		// Token: 0x04000C6C RID: 3180
		InformationNotAvailable
	}
}
