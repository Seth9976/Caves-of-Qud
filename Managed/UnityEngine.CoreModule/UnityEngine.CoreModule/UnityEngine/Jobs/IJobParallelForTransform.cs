using System;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x02000303 RID: 771
	[JobProducerType(typeof(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<>))]
	public interface IJobParallelForTransform
	{
		// Token: 0x06002654 RID: 9812
		void Execute(int index, TransformAccess transform);
	}
}
