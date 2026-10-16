using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000013 RID: 19
	[UsedByNativeCode]
	[DebuggerDisplay("Language = {tag},  Feature Count = {featureIndexes.Length}")]
	internal struct OTL_Language
	{
		// Token: 0x04000098 RID: 152
		public OTL_Tag tag;

		// Token: 0x04000099 RID: 153
		public uint[] featureIndexes;
	}
}
