using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x02000318 RID: 792
	public enum SpeechError
	{
		// Token: 0x04000AD8 RID: 2776
		NoError,
		// Token: 0x04000AD9 RID: 2777
		TopicLanguageNotSupported,
		// Token: 0x04000ADA RID: 2778
		GrammarLanguageMismatch,
		// Token: 0x04000ADB RID: 2779
		GrammarCompilationFailure,
		// Token: 0x04000ADC RID: 2780
		AudioQualityFailure,
		// Token: 0x04000ADD RID: 2781
		PauseLimitExceeded,
		// Token: 0x04000ADE RID: 2782
		TimeoutExceeded,
		// Token: 0x04000ADF RID: 2783
		NetworkFailure,
		// Token: 0x04000AE0 RID: 2784
		MicrophoneUnavailable,
		// Token: 0x04000AE1 RID: 2785
		UnknownError
	}
}
