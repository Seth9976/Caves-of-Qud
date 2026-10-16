using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001D RID: 29
	[DebuggerDisplay("Feature = {tag},  Lookup Count = {lookupIndexes.Length}")]
	[Serializable]
	internal struct OpenTypeLayoutFeature
	{
		// Token: 0x040000C9 RID: 201
		public string tag;

		// Token: 0x040000CA RID: 202
		public uint[] lookupIndexes;
	}
}
