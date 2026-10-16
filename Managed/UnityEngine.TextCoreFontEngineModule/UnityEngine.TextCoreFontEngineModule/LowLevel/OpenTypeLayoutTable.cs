using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal struct OpenTypeLayoutTable
	{
		// Token: 0x040000C2 RID: 194
		public List<OpenTypeLayoutScript> scripts;

		// Token: 0x040000C3 RID: 195
		public List<OpenTypeLayoutFeature> features;

		// Token: 0x040000C4 RID: 196
		[SerializeReference]
		public List<OpenTypeLayoutLookup> lookups;
	}
}
