using System;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000067 RID: 103
	internal static class ParticleSystemJobUtility
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x000088A8 File Offset: 0x00006AA8
		internal static JobsUtility.JobScheduleParameters CreateScheduleParams<T>(ref T jobData, ParticleSystem ps, JobHandle dependsOn, IntPtr jobReflectionData) where T : struct
		{
			dependsOn = JobHandle.CombineDependencies(ps.GetManagedJobHandle(), dependsOn);
			return new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), jobReflectionData, dependsOn, ScheduleMode.Batched);
		}
	}
}
