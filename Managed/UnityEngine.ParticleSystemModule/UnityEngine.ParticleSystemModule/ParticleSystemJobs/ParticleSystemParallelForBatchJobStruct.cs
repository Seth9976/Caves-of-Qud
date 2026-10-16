using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000074 RID: 116
	internal struct ParticleSystemParallelForBatchJobStruct<T> where T : struct, IJobParticleSystemParallelForBatch
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x00008F0C File Offset: 0x0000710C
		[BurstDiscard]
		public unsafe static void Initialize()
		{
			bool flag = *ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
			if (flag)
			{
				*ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemParallelForBatchJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForBatchJobStruct<T>.Execute), null, null);
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00008F5C File Offset: 0x0000715C
		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData particleSystemJobData = new ParticleSystemJobData(ref nativeParticleData);
			for (;;)
			{
				int num;
				int num2;
				bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
				if (flag)
				{
					break;
				}
				data.Execute(particleSystemJobData, num, num2 - num);
			}
		}

		// Token: 0x040001CE RID: 462
		public static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<ParticleSystemParallelForBatchJobStruct<T>>(0U);

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x060006F9 RID: 1785
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
	}
}
