using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000070 RID: 112
	internal struct ParticleSystemJobStruct<T> where T : struct, IJobParticleSystem
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00008DA4 File Offset: 0x00006FA4
		[BurstDiscard]
		public unsafe static void Initialize()
		{
			bool flag = *ParticleSystemJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
			if (flag)
			{
				*ParticleSystemJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new ParticleSystemJobStruct<T>.ExecuteJobFunction(ParticleSystemJobStruct<T>.Execute), null, null);
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			NativeParticleData nativeParticleData;
			ParticleSystem.CopyManagedJobData(ptr->system, out nativeParticleData);
			ParticleSystemJobData particleSystemJobData = new ParticleSystemJobData(ref nativeParticleData);
			data.Execute(particleSystemJobData);
		}

		// Token: 0x040001CC RID: 460
		public static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<ParticleSystemJobStruct<T>>(0U);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x060006EB RID: 1771
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);
	}
}
