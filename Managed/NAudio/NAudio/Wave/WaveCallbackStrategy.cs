using System;

namespace NAudio.Wave
{
	// Token: 0x020001C1 RID: 449
	public enum WaveCallbackStrategy
	{
		// Token: 0x04000AEB RID: 2795
		FunctionCallback,
		// Token: 0x04000AEC RID: 2796
		NewWindow,
		// Token: 0x04000AED RID: 2797
		ExistingWindow,
		// Token: 0x04000AEE RID: 2798
		Event
	}
}
