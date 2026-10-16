using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000012 RID: 18
	[DebuggerDisplay("Script = {tag},  Language Count = {languages.Length}")]
	[UsedByNativeCode]
	internal struct OTL_Script
	{
		// Token: 0x04000096 RID: 150
		public OTL_Tag tag;

		// Token: 0x04000097 RID: 151
		public OTL_Language[] languages;
	}
}
