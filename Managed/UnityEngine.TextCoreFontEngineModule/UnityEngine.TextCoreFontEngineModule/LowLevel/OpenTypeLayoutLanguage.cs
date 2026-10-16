using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001C RID: 28
	[DebuggerDisplay("Language = {tag},  Feature Count = {featureIndexes.Length}")]
	[Serializable]
	internal struct OpenTypeLayoutLanguage
	{
		// Token: 0x040000C7 RID: 199
		public string tag;

		// Token: 0x040000C8 RID: 200
		public uint[] featureIndexes;
	}
}
