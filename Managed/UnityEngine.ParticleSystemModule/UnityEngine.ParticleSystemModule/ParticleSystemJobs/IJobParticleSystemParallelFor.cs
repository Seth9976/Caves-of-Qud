using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000062 RID: 98
	[JobProducerType(typeof(ParticleSystemParallelForJobStruct<>))]
	public interface IJobParticleSystemParallelFor
	{
		// Token: 0x060006C4 RID: 1732
		void Execute(ParticleSystemJobData jobData, int index);
	}
}
