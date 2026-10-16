using System;
using System.Runtime.InteropServices;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200005D RID: 93
	[StructLayout(0)]
	public class MF_SINK_WRITER_STATISTICS
	{
		// Token: 0x04000304 RID: 772
		public int cb;

		// Token: 0x04000305 RID: 773
		public long llLastTimestampReceived;

		// Token: 0x04000306 RID: 774
		public long llLastTimestampEncoded;

		// Token: 0x04000307 RID: 775
		public long llLastTimestampProcessed;

		// Token: 0x04000308 RID: 776
		public long llLastStreamTickReceived;

		// Token: 0x04000309 RID: 777
		public long llLastSinkSampleRequest;

		// Token: 0x0400030A RID: 778
		public long qwNumSamplesReceived;

		// Token: 0x0400030B RID: 779
		public long qwNumSamplesEncoded;

		// Token: 0x0400030C RID: 780
		public long qwNumSamplesProcessed;

		// Token: 0x0400030D RID: 781
		public long qwNumStreamTicksReceived;

		// Token: 0x0400030E RID: 782
		public int dwByteCountQueued;

		// Token: 0x0400030F RID: 783
		public long qwByteCountProcessed;

		// Token: 0x04000310 RID: 784
		public int dwNumOutstandingSinkSampleRequests;

		// Token: 0x04000311 RID: 785
		public int dwAverageSampleRateReceived;

		// Token: 0x04000312 RID: 786
		public int dwAverageSampleRateEncoded;

		// Token: 0x04000313 RID: 787
		public int dwAverageSampleRateProcessed;
	}
}
