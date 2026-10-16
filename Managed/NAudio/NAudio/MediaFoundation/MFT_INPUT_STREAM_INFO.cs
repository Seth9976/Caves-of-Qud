using System;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000058 RID: 88
	public struct MFT_INPUT_STREAM_INFO
	{
		// Token: 0x040002EB RID: 747
		public long hnsMaxLatency;

		// Token: 0x040002EC RID: 748
		public _MFT_INPUT_STREAM_INFO_FLAGS dwFlags;

		// Token: 0x040002ED RID: 749
		public int cbSize;

		// Token: 0x040002EE RID: 750
		public int cbMaxLookahead;

		// Token: 0x040002EF RID: 751
		public int cbAlignment;
	}
}
