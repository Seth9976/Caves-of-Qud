using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200057A RID: 1402
	public struct PointLight
	{
		// Token: 0x04001285 RID: 4741
		public int instanceID;

		// Token: 0x04001286 RID: 4742
		public bool shadow;

		// Token: 0x04001287 RID: 4743
		public LightMode mode;

		// Token: 0x04001288 RID: 4744
		public Vector3 position;

		// Token: 0x04001289 RID: 4745
		public Quaternion orientation;

		// Token: 0x0400128A RID: 4746
		public LinearColor color;

		// Token: 0x0400128B RID: 4747
		public LinearColor indirectColor;

		// Token: 0x0400128C RID: 4748
		public float range;

		// Token: 0x0400128D RID: 4749
		public float sphereRadius;

		// Token: 0x0400128E RID: 4750
		public FalloffType falloff;
	}
}
