using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000048 RID: 72
	[JobProducerType(typeof(IJobForExtensions.ForJobStruct<>))]
	public interface IJobFor
	{
		// Token: 0x060000D8 RID: 216
		void Execute(int index);
	}
}
