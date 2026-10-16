using System;

namespace NAudio.MediaFoundation
{
	// Token: 0x02000064 RID: 100
	[Flags]
	public enum _MFT_PROCESS_OUTPUT_FLAGS
	{
		// Token: 0x04000340 RID: 832
		None = 0,
		// Token: 0x04000341 RID: 833
		MFT_PROCESS_OUTPUT_DISCARD_WHEN_NO_BUFFER = 1,
		// Token: 0x04000342 RID: 834
		MFT_PROCESS_OUTPUT_REGENERATE_LAST_OUTPUT = 2
	}
}
