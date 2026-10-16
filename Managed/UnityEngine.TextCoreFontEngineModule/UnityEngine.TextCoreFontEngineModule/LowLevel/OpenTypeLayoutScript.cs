using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001B RID: 27
	[DebuggerDisplay("Script = {tag},  Language Count = {languages.Count}")]
	[Serializable]
	internal struct OpenTypeLayoutScript
	{
		// Token: 0x040000C5 RID: 197
		public string tag;

		// Token: 0x040000C6 RID: 198
		public List<OpenTypeLayoutLanguage> languages;
	}
}
