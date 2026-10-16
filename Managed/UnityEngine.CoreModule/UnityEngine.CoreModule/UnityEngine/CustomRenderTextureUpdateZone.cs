using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FA RID: 506
	[UsedByNativeCode]
	[Serializable]
	public struct CustomRenderTextureUpdateZone
	{
		// Token: 0x040006E5 RID: 1765
		public Vector3 updateZoneCenter;

		// Token: 0x040006E6 RID: 1766
		public Vector3 updateZoneSize;

		// Token: 0x040006E7 RID: 1767
		public float rotation;

		// Token: 0x040006E8 RID: 1768
		public int passIndex;

		// Token: 0x040006E9 RID: 1769
		public bool needSwap;
	}
}
