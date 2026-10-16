using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200031C RID: 796
	public struct PhraseRecognizedEventArgs
	{
		// Token: 0x06002708 RID: 9992 RVA: 0x0004B012 File Offset: 0x00049212
		internal PhraseRecognizedEventArgs(string text, ConfidenceLevel confidence, SemanticMeaning[] semanticMeanings, DateTime phraseStartTime, TimeSpan phraseDuration)
		{
			this.text = text;
			this.confidence = confidence;
			this.semanticMeanings = semanticMeanings;
			this.phraseStartTime = phraseStartTime;
			this.phraseDuration = phraseDuration;
		}

		// Token: 0x04000AF1 RID: 2801
		public readonly ConfidenceLevel confidence;

		// Token: 0x04000AF2 RID: 2802
		public readonly SemanticMeaning[] semanticMeanings;

		// Token: 0x04000AF3 RID: 2803
		public readonly string text;

		// Token: 0x04000AF4 RID: 2804
		public readonly DateTime phraseStartTime;

		// Token: 0x04000AF5 RID: 2805
		public readonly TimeSpan phraseDuration;
	}
}
