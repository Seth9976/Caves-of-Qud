using System;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000059 RID: 89
	public struct MFT_OUTPUT_DATA_BUFFER
	{
		// Token: 0x040002F0 RID: 752
		public int dwStreamID;

		// Token: 0x040002F1 RID: 753
		public IMFSample pSample;

		// Token: 0x040002F2 RID: 754
		public _MFT_OUTPUT_DATA_BUFFER_FLAGS dwStatus;

		// Token: 0x040002F3 RID: 755
		public IMFCollection pEvents;
	}
}
