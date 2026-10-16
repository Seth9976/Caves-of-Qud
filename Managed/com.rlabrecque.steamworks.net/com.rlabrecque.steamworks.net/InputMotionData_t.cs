using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct InputMotionData_t
	{
		// Token: 0x04000A3B RID: 2619
		public float rotQuatX;

		// Token: 0x04000A3C RID: 2620
		public float rotQuatY;

		// Token: 0x04000A3D RID: 2621
		public float rotQuatZ;

		// Token: 0x04000A3E RID: 2622
		public float rotQuatW;

		// Token: 0x04000A3F RID: 2623
		public float posAccelX;

		// Token: 0x04000A40 RID: 2624
		public float posAccelY;

		// Token: 0x04000A41 RID: 2625
		public float posAccelZ;

		// Token: 0x04000A42 RID: 2626
		public float rotVelX;

		// Token: 0x04000A43 RID: 2627
		public float rotVelY;

		// Token: 0x04000A44 RID: 2628
		public float rotVelZ;
	}
}
