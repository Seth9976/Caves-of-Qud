using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000015 RID: 21
	[DebuggerDisplay("{(OTL_LookupType)lookupType}")]
	[UsedByNativeCode]
	internal struct OTL_Lookup
	{
		// Token: 0x0400009C RID: 156
		public uint lookupType;

		// Token: 0x0400009D RID: 157
		public uint lookupFlag;

		// Token: 0x0400009E RID: 158
		public uint markFilteringSet;
	}
}
