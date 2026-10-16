using System;

namespace System.Net
{
	// Token: 0x020005EB RID: 1515
	internal enum BufferType
	{
		// Token: 0x04001B81 RID: 7041
		Empty,
		// Token: 0x04001B82 RID: 7042
		Data,
		// Token: 0x04001B83 RID: 7043
		Token,
		// Token: 0x04001B84 RID: 7044
		Parameters,
		// Token: 0x04001B85 RID: 7045
		Missing,
		// Token: 0x04001B86 RID: 7046
		Extra,
		// Token: 0x04001B87 RID: 7047
		Trailer,
		// Token: 0x04001B88 RID: 7048
		Header,
		// Token: 0x04001B89 RID: 7049
		Padding = 9,
		// Token: 0x04001B8A RID: 7050
		Stream,
		// Token: 0x04001B8B RID: 7051
		ChannelBindings = 14,
		// Token: 0x04001B8C RID: 7052
		TargetHost = 16,
		// Token: 0x04001B8D RID: 7053
		ReadOnlyFlag = -2147483648,
		// Token: 0x04001B8E RID: 7054
		ReadOnlyWithChecksum = 268435456
	}
}
