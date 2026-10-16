using System;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.Jobs
{
	// Token: 0x02000304 RID: 772
	public static class IJobParallelForTransformExtensions
	{
		// Token: 0x06002655 RID: 9813 RVA: 0x0004A047 File Offset: 0x00048247
		public static void EarlyJobInit<T>() where T : struct, IJobParallelForTransform
		{
			IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize();
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0004A050 File Offset: 0x00048250
		private unsafe static IntPtr GetReflectionData<T>() where T : struct, IJobParallelForTransform
		{
			IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Initialize();
			return *IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData.Data;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x0004A078 File Offset: 0x00048278
		public static JobHandle Schedule<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule());
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x0004A0B0 File Offset: 0x000482B0
		public static JobHandle ScheduleReadOnly<T>(this T jobData, TransformAccessArray transforms, int batchSize, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), batchSize);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0004A0E8 File Offset: 0x000482E8
		public static void RunReadOnly<T>(this T jobData, TransformAccessArray transforms) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), transforms.length);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0004A12C File Offset: 0x0004832C
		public static JobHandle ScheduleByRef<T>(this T jobData, TransformAccessArray transforms, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransform(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule());
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0004A160 File Offset: 0x00048360
		public static JobHandle ScheduleReadOnlyByRef<T>(this T jobData, TransformAccessArray transforms, int batchSize, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), dependsOn, ScheduleMode.Batched);
			return JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), batchSize);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x0004A198 File Offset: 0x00048398
		public static void RunReadOnlyByRef<T>(this T jobData, TransformAccessArray transforms) where T : struct, IJobParallelForTransform
		{
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<T>(ref jobData), IJobParallelForTransformExtensions.GetReflectionData<T>(), default(JobHandle), ScheduleMode.Run);
			JobsUtility.ScheduleParallelForTransformReadOnly(ref jobScheduleParameters, transforms.GetTransformAccessArrayForSchedule(), transforms.length);
		}

		// Token: 0x02000305 RID: 773
		internal struct TransformParallelForLoopStruct<T> where T : struct, IJobParallelForTransform
		{
			// Token: 0x0600265D RID: 9821 RVA: 0x0004A1D8 File Offset: 0x000483D8
			[BurstDiscard]
			internal unsafe static void Initialize()
			{
				bool flag = *IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData.Data == IntPtr.Zero;
				if (flag)
				{
					*IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.ExecuteJobFunction(IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.Execute), null, null);
				}
			}

			// Token: 0x0600265E RID: 9822 RVA: 0x0004A228 File Offset: 0x00048428
			public unsafe static void Execute(ref T jobData, IntPtr jobData2, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
			{
				IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData transformJobData;
				UnsafeUtility.CopyPtrToStructure<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>.TransformJobData>((void*)jobData2, out transformJobData);
				int* ptr = (int*)(void*)TransformAccessArray.GetSortedToUserIndex(transformJobData.TransformAccessArray);
				TransformAccess* ptr2 = (TransformAccess*)(void*)TransformAccessArray.GetSortedTransformAccess(transformJobData.TransformAccessArray);
				bool flag = transformJobData.IsReadOnly == 1;
				if (flag)
				{
					for (;;)
					{
						int num;
						int num2;
						bool flag2 = !JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out num, out num2);
						if (flag2)
						{
							break;
						}
						int num3 = num2;
						for (int i = num; i < num3; i++)
						{
							int num4 = i;
							int num5 = ptr[num4];
							TransformAccess transformAccess = ptr2[num4];
							jobData.Execute(num5, transformAccess);
						}
					}
				}
				else
				{
					int num6;
					int num7;
					JobsUtility.GetJobRange(ref ranges, jobIndex, out num6, out num7);
					for (int j = num6; j < num7; j++)
					{
						int num8 = j;
						int num9 = ptr[num8];
						TransformAccess transformAccess2 = ptr2[num8];
						jobData.Execute(num9, transformAccess2);
					}
				}
			}

			// Token: 0x04000ABF RID: 2751
			internal static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<IJobParallelForTransformExtensions.TransformParallelForLoopStruct<T>>(0U);

			// Token: 0x02000306 RID: 774
			private struct TransformJobData
			{
				// Token: 0x04000AC0 RID: 2752
				public IntPtr TransformAccessArray;

				// Token: 0x04000AC1 RID: 2753
				public int IsReadOnly;
			}

			// Token: 0x02000307 RID: 775
			// (Invoke) Token: 0x06002661 RID: 9825
			public delegate void ExecuteJobFunction(ref T jobData, IntPtr additionalPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);
		}
	}
}
