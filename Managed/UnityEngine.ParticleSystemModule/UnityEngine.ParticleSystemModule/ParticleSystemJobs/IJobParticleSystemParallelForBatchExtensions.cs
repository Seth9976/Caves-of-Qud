using System;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000066 RID: 102
	public static class IJobParticleSystemParallelForBatchExtensions
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00008875 File Offset: 0x00006A75
		public static void EarlyJobInit<T>() where T : struct, IJobParticleSystemParallelForBatch
		{
			ParticleSystemParallelForBatchJobStruct<T>.Initialize();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00008880 File Offset: 0x00006A80
		internal unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParticleSystemParallelForBatch
		{
			ParticleSystemParallelForBatchJobStruct<T>.Initialize();
			return *ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData.Data;
		}
	}
}
