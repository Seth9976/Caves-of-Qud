using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace Unity.Jobs
{
	// Token: 0x02000045 RID: 69
	public static class IJobExtensions
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00002962 File Offset: 0x00000B62
		public static void EarlyJobInit<T>() where T : struct, IJob
		{
			IJobExtensions.JobStruct<T>.Initialize();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000296C File Offset: 0x00000B6C
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJob
		{
			IJobExtensions.JobStruct<T>.Initialize();
			return *IJobExtensions.JobStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002994 File Offset: 0x00000B94
		public static JobHandle Schedule<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static void Run<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000029F8 File Offset: 0x00000BF8
		public static JobHandle ScheduleByRef<T>(this T jobData, JobHandle dependsOn = default(JobHandle)) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Single);
			return JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00002A28 File Offset: 0x00000C28
		public static void RunByRef<T>(this T jobData) where T : struct, IJob
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.Schedule(ref jobScheduleParameters);
		}

		// Token: 0x02000046 RID: 70
		internal struct JobStruct<T> where T : struct, IJob
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x00002A5C File Offset: 0x00000C5C
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobExtensions.JobStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobExtensions.JobStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobExtensions.JobStruct<T>.ExecuteJobFunction(IJobExtensions.JobStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00002AAB File Offset: 0x00000CAB
			public static void Execute(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				data.Execute();
			}

			// Token: 0x04000103 RID: 259
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobExtensions.JobStruct<T>>(0U);

			// Token: 0x02000047 RID: 71
			// (Invoke) Token: 0x060000D5 RID: 213
			internal delegate void ExecuteJobFunction(ref T data, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
