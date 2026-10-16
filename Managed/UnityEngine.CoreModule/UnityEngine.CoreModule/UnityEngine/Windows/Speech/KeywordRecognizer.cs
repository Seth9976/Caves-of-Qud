using System;
using System.Collections.Generic;

namespace UnityEngine.Windows.Speech
{
	// Token: 0x0200031D RID: 797
	public sealed class KeywordRecognizer : PhraseRecognizer
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x0004B03A File Offset: 0x0004923A
		// (set) Token: 0x0600270A RID: 9994 RVA: 0x0004B042 File Offset: 0x00049242
		public IEnumerable<string> Keywords { get; private set; }

		// Token: 0x0600270B RID: 9995 RVA: 0x0004B04B File Offset: 0x0004924B
		public KeywordRecognizer(string[] keywords)
			: this(keywords, ConfidenceLevel.Medium)
		{
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x0004B058 File Offset: 0x00049258
		public KeywordRecognizer(string[] keywords, ConfidenceLevel minimumConfidence)
		{
			bool flag = keywords == null;
			if (flag)
			{
				throw new ArgumentNullException("keywords");
			}
			bool flag2 = keywords.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("At least one keyword must be specified.", "keywords");
			}
			int num = keywords.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag3 = keywords[i] == null;
				if (flag3)
				{
					throw new ArgumentNullException(string.Format("Keyword at index {0} is null.", i));
				}
			}
			this.Keywords = keywords;
			this.m_Recognizer = PhraseRecognizer.CreateFromKeywords(this, keywords, minimumConfidence);
		}
	}
}
