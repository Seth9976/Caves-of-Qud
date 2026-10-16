using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/Physics/ArticulationBody.h")]
	public struct ArticulationDrive
	{
		// Token: 0x04000043 RID: 67
		public float lowerLimit;

		// Token: 0x04000044 RID: 68
		public float upperLimit;

		// Token: 0x04000045 RID: 69
		public float stiffness;

		// Token: 0x04000046 RID: 70
		public float damping;

		// Token: 0x04000047 RID: 71
		public float forceLimit;

		// Token: 0x04000048 RID: 72
		public float target;

		// Token: 0x04000049 RID: 73
		public float targetVelocity;

		// Token: 0x0400004A RID: 74
		public ArticulationDriveType driveType;
	}
}
