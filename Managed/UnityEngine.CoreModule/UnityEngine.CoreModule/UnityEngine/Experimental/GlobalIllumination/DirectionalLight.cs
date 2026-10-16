using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x02000579 RID: 1401
	public struct DirectionalLight
	{
		// Token: 0x0400127C RID: 4732
		public int instanceID;

		// Token: 0x0400127D RID: 4733
		public bool shadow;

		// Token: 0x0400127E RID: 4734
		public LightMode mode;

		// Token: 0x0400127F RID: 4735
		public Vector3 position;

		// Token: 0x04001280 RID: 4736
		public Quaternion orientation;

		// Token: 0x04001281 RID: 4737
		public LinearColor color;

		// Token: 0x04001282 RID: 4738
		public LinearColor indirectColor;

		// Token: 0x04001283 RID: 4739
		public float penumbraWidthRadian;

		// Token: 0x04001284 RID: 4740
		[Obsolete("Directional lights support cookies now. In order to position the cookie projection in the world, a position and full orientation are necessary. Use the position and orientation members instead of the direction parameter.", true)]
		public Vector3 direction;
	}
}
