using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000611 RID: 1553
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRendererJobProcessor.h")]
	internal static class JobProcessor
	{
		// Token: 0x06002E06 RID: 11782 RVA: 0x000A2C38 File Offset: 0x000A0E38
		internal static JobHandle ScheduleNudgeJobs(IntPtr buffer, int jobCount)
		{
			JobHandle jobHandle;
			JobProcessor.ScheduleNudgeJobs_Injected(buffer, jobCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000A2C50 File Offset: 0x000A0E50
		internal static JobHandle ScheduleConvertMeshJobs(IntPtr buffer, int jobCount)
		{
			JobHandle jobHandle;
			JobProcessor.ScheduleConvertMeshJobs_Injected(buffer, jobCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000A2C68 File Offset: 0x000A0E68
		internal static JobHandle ScheduleCopyMeshJobs(IntPtr buffer, int jobCount)
		{
			JobHandle jobHandle;
			JobProcessor.ScheduleCopyMeshJobs_Injected(buffer, jobCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06002E09 RID: 11785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleNudgeJobs_Injected(IntPtr buffer, int jobCount, out JobHandle ret);

		// Token: 0x06002E0A RID: 11786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleConvertMeshJobs_Injected(IntPtr buffer, int jobCount, out JobHandle ret);

		// Token: 0x06002E0B RID: 11787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleCopyMeshJobs_Injected(IntPtr buffer, int jobCount, out JobHandle ret);
	}
}
