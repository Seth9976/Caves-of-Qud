using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000063 RID: 99
	[JobProducerType(typeof(ParticleSystemParallelForBatchJobStruct<>))]
	public interface IJobParticleSystemParallelForBatch
	{
		// Token: 0x060006C5 RID: 1733
		void Execute(ParticleSystemJobData jobData, int startIndex, int count);
	}
}
