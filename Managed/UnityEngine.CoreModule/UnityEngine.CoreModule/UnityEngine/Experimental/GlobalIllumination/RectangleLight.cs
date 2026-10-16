using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200057C RID: 1404
	public struct RectangleLight
	{
		// Token: 0x0400129C RID: 4764
		public int instanceID;

		// Token: 0x0400129D RID: 4765
		public bool shadow;

		// Token: 0x0400129E RID: 4766
		public LightMode mode;

		// Token: 0x0400129F RID: 4767
		public Vector3 position;

		// Token: 0x040012A0 RID: 4768
		public Quaternion orientation;

		// Token: 0x040012A1 RID: 4769
		public LinearColor color;

		// Token: 0x040012A2 RID: 4770
		public LinearColor indirectColor;

		// Token: 0x040012A3 RID: 4771
		public float range;

		// Token: 0x040012A4 RID: 4772
		public float width;

		// Token: 0x040012A5 RID: 4773
		public float height;

		// Token: 0x040012A6 RID: 4774
		public FalloffType falloff;
	}
}
