using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000387 RID: 903
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public struct PlayerLoopSystem
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x0004FDCC File Offset: 0x0004DFCC
		public override string ToString()
		{
			return this.type.Name;
		}

		// Token: 0x04000BBC RID: 3004
		public Type type;

		// Token: 0x04000BBD RID: 3005
		public PlayerLoopSystem[] subSystemList;

		// Token: 0x04000BBE RID: 3006
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x04000BBF RID: 3007
		public IntPtr updateFunction;

		// Token: 0x04000BC0 RID: 3008
		public IntPtr loopConditionFunction;

		// Token: 0x02000388 RID: 904
		// (Invoke) Token: 0x06002973 RID: 10611
		public delegate void UpdateFunction();
	}
}
