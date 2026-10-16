using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000049 RID: 73
	public static class IJobForExtensions
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public static void EarlyJobInit<T>() where T : struct, IJobFor
		{
			IJobForExtensions.ForJobStruct<T>.Initialize();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobFor
		{
			IJobForExtensions.ForJobStruct<T>.Initialize();
			return *IJobForExtensions.ForJobStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static JobHandle Schedule<T>(this T jobData, int arrayLength, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), dependency, ScheduleMode.Single);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002B2C File Offset: 0x00000D2C
		public static JobHandle ScheduleParallel<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002B5C File Offset: 0x00000D5C
		public static void Run<T>(this T jobData, int arrayLength) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002B94 File Offset: 0x00000D94
		public static JobHandle ScheduleByRef<T>(this T jobData, int arrayLength, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), dependency, ScheduleMode.Single);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public static JobHandle ScheduleParallelByRef<T>(this T jobData, int arrayLength, int innerloopBatchCount, JobHandle dependency) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), dependency, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, innerloopBatchCount);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public static void RunByRef<T>(this T jobData, int arrayLength) where T : struct, IJobFor
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobForExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelFor(ref jobScheduleParameters, arrayLength, arrayLength);
		}

		// Token: 0x0200004A RID: 74
		internal struct ForJobStruct<T> where T : struct, IJobFor
		{
			// Token: 0x060000E1 RID: 225 RVA: 0x00002C28 File Offset: 0x00000E28
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobForExtensions.ForJobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobForExtensions.ForJobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobForExtensions.ForJobStruct<T>.ExecuteJobFunction(IJobForExtensions.ForJobStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x060000E2 RID: 226 RVA: 0x00002C78 File Offset: 0x00000E78
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

			// Token: 0x04000104 RID: 260
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobForExtensions.ForJobStruct<T>>(0U);

			// Token: 0x0200004B RID: 75
			// (Invoke) Token: 0x060000E5 RID: 229
			public delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
