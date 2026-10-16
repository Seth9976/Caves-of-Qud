using System;

namespace NAudio.Dmo
{
	// Token: 0x02000201 RID: 513
	[Flags]
	internal enum InputStreamInfoFlags
	{
		// Token: 0x04000C15 RID: 3093
		None = 0,
		// Token: 0x04000C16 RID: 3094
		DMO_INPUT_STREAMF_WHOLE_SAMPLES = 1,
		// Token: 0x04000C17 RID: 3095
		DMO_INPUT_STREAMF_SINGLE_SAMPLE_PER_BUFFER = 2,
		// Token: 0x04000C18 RID: 3096
		DMO_INPUT_STREAMF_FIXED_SAMPLE_SIZE = 4,
		// Token: 0x04000C19 RID: 3097
		DMO_INPUT_STREAMF_HOLDS_BUFFERS = 8
	}
}
