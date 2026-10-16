using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x02000386 RID: 902
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	[RequiredByNativeCode]
	[NativeType(Header = "Runtime/Misc/PlayerLoop.h")]
	internal struct PlayerLoopSystemInternal
	{
		// Token: 0x04000BB7 RID: 2999
		public Type type;

		// Token: 0x04000BB8 RID: 3000
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x04000BB9 RID: 3001
		public IntPtr updateFunction;

		// Token: 0x04000BBA RID: 3002
		public IntPtr loopConditionFunction;

		// Token: 0x04000BBB RID: 3003
		public int numSubSystems;
	}
}
