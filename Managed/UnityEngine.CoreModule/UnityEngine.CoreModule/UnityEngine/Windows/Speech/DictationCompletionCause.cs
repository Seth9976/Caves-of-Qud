using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200031A RID: 794
	public enum DictationCompletionCause
	{
		// Token: 0x04000AE7 RID: 2791
		Complete,
		// Token: 0x04000AE8 RID: 2792
		AudioQualityFailure,
		// Token: 0x04000AE9 RID: 2793
		Canceled,
		// Token: 0x04000AEA RID: 2794
		TimeoutExceeded,
		// Token: 0x04000AEB RID: 2795
		PauseLimitExceeded,
		// Token: 0x04000AEC RID: 2796
		NetworkFailure,
		// Token: 0x04000AED RID: 2797
		MicrophoneUnavailable,
		// Token: 0x04000AEE RID: 2798
		UnknownError
	}
}
