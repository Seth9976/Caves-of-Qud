using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	public enum JointProjectionMode
	{
		// Token: 0x04000068 RID: 104
		None,
		// Token: 0x04000069 RID: 105
		PositionAndRotation,
		// Token: 0x0400006A RID: 106
		[Obsolete("JointProjectionMode.PositionOnly is no longer supported", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		PositionOnly
	}
}
