using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[Flags]
	[Obsolete("JointDriveMode is no longer supported")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum JointDriveMode
	{
		// Token: 0x0400002C RID: 44
		[Obsolete("JointDriveMode.None is no longer supported")]
		None = 0,
		// Token: 0x0400002D RID: 45
		[Obsolete("JointDriveMode.Position is no longer supported")]
		Position = 1,
		// Token: 0x0400002E RID: 46
		[Obsolete("JointDriveMode.Velocity is no longer supported")]
		Velocity = 2,
		// Token: 0x0400002F RID: 47
		[Obsolete("JointDriveMode.PositionAndvelocity is no longer supported")]
		PositionAndVelocity = 3
	}
}
