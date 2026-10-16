using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000014 RID: 20
	[DebuggerDisplay("Feature = {tag},  Lookup Count = {lookupIndexes.Length}")]
	[UsedByNativeCode]
	internal struct OTL_Feature
	{
		// Token: 0x0400009A RID: 154
		public OTL_Tag tag;

		// Token: 0x0400009B RID: 155
		public uint[] lookupIndexes;
	}
}
