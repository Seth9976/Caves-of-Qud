using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200057B RID: 1403
	public struct SpotLight
	{
		// Token: 0x0400128F RID: 4751
		public int instanceID;

		// Token: 0x04001290 RID: 4752
		public bool shadow;

		// Token: 0x04001291 RID: 4753
		public LightMode mode;

		// Token: 0x04001292 RID: 4754
		public Vector3 position;

		// Token: 0x04001293 RID: 4755
		public Quaternion orientation;

		// Token: 0x04001294 RID: 4756
		public LinearColor color;

		// Token: 0x04001295 RID: 4757
		public LinearColor indirectColor;

		// Token: 0x04001296 RID: 4758
		public float range;

		// Token: 0x04001297 RID: 4759
		public float sphereRadius;

		// Token: 0x04001298 RID: 4760
		public float coneAngle;

		// Token: 0x04001299 RID: 4761
		public float innerConeAngle;

		// Token: 0x0400129A RID: 4762
		public FalloffType falloff;

		// Token: 0x0400129B RID: 4763
		public AngularFalloffType angularFalloff;
	}
}
