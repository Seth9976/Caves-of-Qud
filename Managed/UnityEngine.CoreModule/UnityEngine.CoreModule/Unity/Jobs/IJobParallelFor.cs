using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200004C RID: 76
	[JobProducerType(typeof(IJobParallelForExtensions.ParallelForJobStruct<>))]
	public interface IJobParallelFor
	{
		// Token: 0x060000E8 RID: 232
		void Execute(int index);
	}
}
