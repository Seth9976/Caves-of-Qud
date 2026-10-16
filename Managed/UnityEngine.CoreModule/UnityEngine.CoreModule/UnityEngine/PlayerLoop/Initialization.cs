using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	// Token: 0x0200038D RID: 909
	[MovedFrom("UnityEngine.Experimental.PlayerLoop")]
	[RequiredByNativeCode]
	public struct Initialization
	{
		// Token: 0x0200038E RID: 910
		[RequiredByNativeCode]
		public struct ProfilerStartFrame
		{
		}

		// Token: 0x0200038F RID: 911
		[Obsolete("PlayerUpdateTime player loop component has been moved to its own category called TimeUpdate. (UnityUpgradable) -> UnityEngine.PlayerLoop.TimeUpdate/WaitForLastPresentationAndUpdateTime", true)]
		public struct PlayerUpdateTime
		{
		}

		// Token: 0x02000390 RID: 912
		[RequiredByNativeCode]
		public struct UpdateCameraMotionVectors
		{
		}

		// Token: 0x02000391 RID: 913
		[RequiredByNativeCode]
		public struct DirectorSampleTime
		{
		}

		// Token: 0x02000392 RID: 914
		[RequiredByNativeCode]
		public struct AsyncUploadTimeSlicedUpdate
		{
		}

		// Token: 0x02000393 RID: 915
		[RequiredByNativeCode]
		public struct SynchronizeState
		{
		}

		// Token: 0x02000394 RID: 916
		[RequiredByNativeCode]
		public struct SynchronizeInputs
		{
		}

		// Token: 0x02000395 RID: 917
		[RequiredByNativeCode]
		public struct XREarlyUpdate
		{
		}
	}
}
