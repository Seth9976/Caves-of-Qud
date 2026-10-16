using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200057D RID: 1405
	public struct DiscLight
	{
		// Token: 0x040012A7 RID: 4775
		public int instanceID;

		// Token: 0x040012A8 RID: 4776
		public bool shadow;

		// Token: 0x040012A9 RID: 4777
		public LightMode mode;

		// Token: 0x040012AA RID: 4778
		public Vector3 position;

		// Token: 0x040012AB RID: 4779
		public Quaternion orientation;

		// Token: 0x040012AC RID: 4780
		public LinearColor color;

		// Token: 0x040012AD RID: 4781
		public LinearColor indirectColor;

		// Token: 0x040012AE RID: 4782
		public float range;

		// Token: 0x040012AF RID: 4783
		public float radius;

		// Token: 0x040012B0 RID: 4784
		public FalloffType falloff;
	}
}
