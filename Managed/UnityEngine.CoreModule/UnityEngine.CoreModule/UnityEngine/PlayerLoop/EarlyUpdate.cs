using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	// Token: 0x02000396 RID: 918
	[MovedFrom("UnityEngine.Experimental.PlayerLoop")]
	[RequiredByNativeCode]
	public struct EarlyUpdate
	{
		// Token: 0x02000397 RID: 919
		[RequiredByNativeCode]
		public struct PollPlayerConnection
		{
		}

		// Token: 0x02000398 RID: 920
		[Obsolete("ProfilerStartFrame player loop component has been moved to the Initialization category. (UnityUpgradable) -> UnityEngine.PlayerLoop.Initialization/ProfilerStartFrame", true)]
		public struct ProfilerStartFrame
		{
		}

		// Token: 0x02000399 RID: 921
		[RequiredByNativeCode]
		public struct PollHtcsPlayerConnection
		{
		}

		// Token: 0x0200039A RID: 922
		[RequiredByNativeCode]
		public struct GpuTimestamp
		{
		}

		// Token: 0x0200039B RID: 923
		[RequiredByNativeCode]
		public struct AnalyticsCoreStatsUpdate
		{
		}

		// Token: 0x0200039C RID: 924
		[RequiredByNativeCode]
		public struct InsightsUpdate
		{
		}

		// Token: 0x0200039D RID: 925
		[RequiredByNativeCode]
		public struct UnityWebRequestUpdate
		{
		}

		// Token: 0x0200039E RID: 926
		[RequiredByNativeCode]
		public struct UpdateStreamingManager
		{
		}

		// Token: 0x0200039F RID: 927
		[RequiredByNativeCode]
		public struct ExecuteMainThreadJobs
		{
		}

		// Token: 0x020003A0 RID: 928
		[RequiredByNativeCode]
		public struct ProcessMouseInWindow
		{
		}

		// Token: 0x020003A1 RID: 929
		[RequiredByNativeCode]
		public struct ClearIntermediateRenderers
		{
		}

		// Token: 0x020003A2 RID: 930
		[RequiredByNativeCode]
		public struct ClearLines
		{
		}

		// Token: 0x020003A3 RID: 931
		[RequiredByNativeCode]
		public struct PresentBeforeUpdate
		{
		}

		// Token: 0x020003A4 RID: 932
		[RequiredByNativeCode]
		public struct ResetFrameStatsAfterPresent
		{
		}

		// Token: 0x020003A5 RID: 933
		[RequiredByNativeCode]
		public struct UpdateAsyncReadbackManager
		{
		}

		// Token: 0x020003A6 RID: 934
		[RequiredByNativeCode]
		public struct UpdateTextureStreamingManager
		{
		}

		// Token: 0x020003A7 RID: 935
		[RequiredByNativeCode]
		public struct UpdatePreloading
		{
		}

		// Token: 0x020003A8 RID: 936
		[RequiredByNativeCode]
		public struct UpdateContentLoading
		{
		}

		// Token: 0x020003A9 RID: 937
		[RequiredByNativeCode]
		public struct UpdateAsyncInstantiate
		{
		}

		// Token: 0x020003AA RID: 938
		[RequiredByNativeCode]
		public struct RendererNotifyInvisible
		{
		}

		// Token: 0x020003AB RID: 939
		[RequiredByNativeCode]
		public struct PlayerCleanupCachedData
		{
		}

		// Token: 0x020003AC RID: 940
		[RequiredByNativeCode]
		public struct UpdateMainGameViewRect
		{
		}

		// Token: 0x020003AD RID: 941
		[RequiredByNativeCode]
		public struct UpdateCanvasRectTransform
		{
		}

		// Token: 0x020003AE RID: 942
		[RequiredByNativeCode]
		public struct UpdateInputManager
		{
		}

		// Token: 0x020003AF RID: 943
		[RequiredByNativeCode]
		public struct ProcessRemoteInput
		{
		}

		// Token: 0x020003B0 RID: 944
		[RequiredByNativeCode]
		public struct XRUpdate
		{
		}

		// Token: 0x020003B1 RID: 945
		[RequiredByNativeCode]
		public struct ScriptRunDelayedStartupFrame
		{
		}

		// Token: 0x020003B2 RID: 946
		[RequiredByNativeCode]
		public struct UpdateKinect
		{
		}

		// Token: 0x020003B3 RID: 947
		[RequiredByNativeCode]
		public struct DeliverIosPlatformEvents
		{
		}

		// Token: 0x020003B4 RID: 948
		[RequiredByNativeCode]
		public struct DispatchEventQueueEvents
		{
		}

		// Token: 0x020003B5 RID: 949
		[RequiredByNativeCode]
		public struct Physics2DEarlyUpdate
		{
		}

		// Token: 0x020003B6 RID: 950
		[RequiredByNativeCode]
		public struct PhysicsResetInterpolatedTransformPosition
		{
		}

		// Token: 0x020003B7 RID: 951
		[RequiredByNativeCode]
		public struct SpriteAtlasManagerUpdate
		{
		}

		// Token: 0x020003B8 RID: 952
		[RequiredByNativeCode]
		[Obsolete("TangoUpdate has been deprecated. Use ARCoreUpdate instead (UnityUpgradable) -> UnityEngine.PlayerLoop.EarlyUpdate/ARCoreUpdate", false)]
		public struct TangoUpdate
		{
		}

		// Token: 0x020003B9 RID: 953
		[RequiredByNativeCode]
		public struct ARCoreUpdate
		{
		}

		// Token: 0x020003BA RID: 954
		[RequiredByNativeCode]
		public struct PerformanceAnalyticsUpdate
		{
		}

		// Token: 0x020003BB RID: 955
		[RequiredByNativeCode]
		public struct TilemapRendererEarlyUpdate
		{
		}
	}
}
