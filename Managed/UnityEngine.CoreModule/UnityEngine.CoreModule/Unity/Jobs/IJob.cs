using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000044 RID: 68
	[JobProducerType(typeof(IJobExtensions.JobStruct<>))]
	public interface IJob
	{
		// Token: 0x060000CA RID: 202
		void Execute();
	}
}
