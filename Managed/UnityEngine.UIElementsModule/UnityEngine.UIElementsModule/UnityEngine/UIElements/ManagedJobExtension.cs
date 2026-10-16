using System;
using Unity.Jobs;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E7 RID: 743
	internal static class ManagedJobExtension
	{
		// Token: 0x06001531 RID: 5425 RVA: 0x00055208 File Offset: 0x00053408
		public static JobHandle ScheduleOrRunJob<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			return jobData.Schedule(arrayLength, innerloopBatchCount, dependsOn);
		}
	}
}
