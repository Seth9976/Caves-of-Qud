using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x0200004D RID: 77
	public static class IJobParallelForExtensions
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public static void EarlyJobInit<T>() where T : struct, IJobParallelFor
		{
			IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParallelFor
		{
			IJobParallelForExtensions.ParallelForJobStruct<T>.Initialize();
			return *IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002D0C File Offset: 0x00000F0C
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002D3C File Offset: 0x00000F3C
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002D74 File Offset: 0x00000F74
		public static JobHandle ScheduleByRef<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public static void RunByRef<T>(this T jobData, int arrayLength) where T : struct, IJobParallelFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x0200004E RID: 78
		internal struct ParallelForJobStruct<T> where T : struct, IJobParallelFor
		{
			// Token: 0x060000EF RID: 239 RVA: 0x00002DD8 File Offset: 0x00000FD8
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobParallelForExtensions.ParallelForJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForExtensions.ParallelForJobStruct<T>.ExecuteJobFunction(IJobParallelForExtensions.ParallelForJobStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x00002E28 File Offset: 0x00001028
			public static void Execute(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				for (;;)
				{
					int num;
					int num2;
					bool flag = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
					if (flag)
					{
						break;
					}
					int num3 = num2;
					for (int i = num; i < num3; i++)
					{
						jobData.Execute(i);
					}
				}
			}

			// Token: 0x04000105 RID: 261
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobParallelForExtensions.ParallelForJobStruct<T>>(0U);

			// Token: 0x0200004F RID: 79
			// (Invoke) Token: 0x060000F3 RID: 243
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
