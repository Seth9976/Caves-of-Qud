using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000072 RID: 114
	internal struct ParticleSystemParallelForJobStruct<T> where T : struct, IJobParticleSystemParallelFor
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x00008E3C File Offset: 0x0000703C
		[BurstDiscard]
		public unsafe static void Initialize()
		{
			bool flag = *ParticleSystemParallelForJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
			if (flag)
			{
				*ParticleSystemParallelForJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemParallelForJobStruct<T>.ExecuteJobFunction(ParticleSystemParallelForJobStruct<T>.Execute), null, null);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00008E8C File Offset: 0x0000708C
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
				for (int i = num; i < num2; i++)
				{
					data.Execute(particleSystemJobData, i);
				}
			}
		}

		// Token: 0x040001CD RID: 461
		public static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<ParticleSystemParallelForJobStruct<T>>(0U);

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x060006F2 RID: 1778
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
	}
}
