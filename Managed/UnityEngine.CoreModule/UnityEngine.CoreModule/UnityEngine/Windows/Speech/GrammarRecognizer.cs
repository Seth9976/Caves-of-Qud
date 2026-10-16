using System;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200031E RID: 798
	public sealed class GrammarRecognizer : PhraseRecognizer
	{
		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x0004B0E9 File Offset: 0x000492E9
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x0004B0F1 File Offset: 0x000492F1
		public string GrammarFilePath { get; private set; }

		// Token: 0x0600270F RID: 9999 RVA: 0x0004B0FA File Offset: 0x000492FA
		public GrammarRecognizer(string grammarFilePath)
			: this(grammarFilePath, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x0004B108 File Offset: 0x00049308
		public GrammarRecognizer(string grammarFilePath, ConfidenceLevel minimumConfidence)
		{
			bool flag = grammarFilePath == null;
			if (flag)
			{
				throw new ArgumentNullException("grammarFilePath");
			}
			bool flag2 = grammarFilePath.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Grammar file path cannot be empty.");
			}
			this.GrammarFilePath = grammarFilePath;
			this.m_Recognizer = PhraseRecognizer.CreateFromGrammarFile(this, grammarFilePath, minimumConfidence);
		}
	}
}
