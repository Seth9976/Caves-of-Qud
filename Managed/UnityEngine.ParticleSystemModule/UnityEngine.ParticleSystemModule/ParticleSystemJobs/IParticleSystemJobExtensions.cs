using System;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000068 RID: 104
	public static class IParticleSystemJobExtensions
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x000088D8 File Offset: 0x00006AD8
		public static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystem
		{
			bool flag = ParticleSystem.UserJobCanBeScheduled();
			if (flag)
			{
				JobsUtility.JobScheduleParameters jobScheduleParameters = ParticleSystemJobUtility.CreateScheduleParams<T>(ref jobData, ps, dependsOn, IJobParticleSystemExtensions.GetReflectionData<T>());
				JobHandle jobHandle = ParticleSystem.ScheduleManagedJob(ref jobScheduleParameters, ps.GetManagedJobData());
				ps.SetManagedJobHandle(jobHandle);
				return jobHandle;
			}
			throw new InvalidOperationException(IParticleSystemJobExtensions.k_UserJobScheduledOutsideOfCallbackErrorMsg);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00008928 File Offset: 0x00006B28
		public static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, int minIndicesPerJobCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelFor
		{
			bool flag = ParticleSystem.UserJobCanBeScheduled();
			if (flag)
			{
				JobsUtility.JobScheduleParameters jobScheduleParameters = ParticleSystemJobUtility.CreateScheduleParams<T>(ref jobData, ps, dependsOn, IJobParticleSystemParallelForExtensions.GetReflectionData<T>());
				JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, minIndicesPerJobCount, ps.GetManagedJobData(), null);
				ps.SetManagedJobHandle(jobHandle);
				return jobHandle;
			}
			throw new InvalidOperationException(IParticleSystemJobExtensions.k_UserJobScheduledOutsideOfCallbackErrorMsg);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000897C File Offset: 0x00006B7C
		public static JobHandle ScheduleBatch<T>(this T jobData, ParticleSystem ps, int innerLoopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelForBatch
		{
			bool flag = ParticleSystem.UserJobCanBeScheduled();
			if (flag)
			{
				JobsUtility.JobScheduleParameters jobScheduleParameters = ParticleSystemJobUtility.CreateScheduleParams<T>(ref jobData, ps, dependsOn, IJobParticleSystemParallelForBatchExtensions.GetReflectionData<T>());
				JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref jobScheduleParameters, innerLoopBatchCount, ps.GetManagedJobData(), null);
				ps.SetManagedJobHandle(jobHandle);
				return jobHandle;
			}
			throw new InvalidOperationException(IParticleSystemJobExtensions.k_UserJobScheduledOutsideOfCallbackErrorMsg);
		}

		// Token: 0x0400019E RID: 414
		private static readonly string k_UserJobScheduledOutsideOfCallbackErrorMsg = "Particle System jobs can only be scheduled in MonoBehaviour.OnParticleUpdateJobScheduled()";
	}
}
