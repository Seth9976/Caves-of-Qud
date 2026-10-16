using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000061 RID: 97
	[JobProducerType(typeof(ParticleSystemJobStruct<>))]
	public interface IJobParticleSystem
	{
		// Token: 0x060006C3 RID: 1731
		void Execute(ParticleSystemJobData jobData);
	}
}
