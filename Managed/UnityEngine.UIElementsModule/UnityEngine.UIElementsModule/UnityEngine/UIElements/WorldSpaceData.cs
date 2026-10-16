using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005DC RID: 1500
	internal struct WorldSpaceData
	{
		// Token: 0x0400143E RID: 5182
		public static readonly Bounds k_Empty3DBounds = new Bounds(Vector3.zero, Vector3.one * float.NegativeInfinity);

		// Token: 0x0400143F RID: 5183
		public Bounds localBounds3D;
	}
}
