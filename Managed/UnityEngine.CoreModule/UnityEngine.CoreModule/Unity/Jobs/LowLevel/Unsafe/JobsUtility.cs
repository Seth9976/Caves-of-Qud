using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000058 RID: 88
	[NativeHeader("Runtime/Jobs/JobSystem.h")]
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public static class JobsUtility
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000031B4 File Offset: 0x000013B4
		public unsafe static void GetJobRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex)
		{
			int* ptr = (int*)(void*)ranges.StartEndIndex;
			beginIndex = ptr[jobIndex * 2];
			endIndex = ptr[jobIndex * 2 + 1];
		}

		// Token: 0x06000116 RID: 278
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetWorkStealingRange(ref JobRanges ranges, int jobIndex, out int beginIndex, out int endIndex);

		// Token: 0x06000117 RID: 279 RVA: 0x000031E8 File Offset: 0x000013E8
		[FreeFunction("ScheduleManagedJob", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle Schedule(ref JobsUtility.JobScheduleParameters parameters)
		{
			JobHandle jobHandle;
			JobsUtility.Schedule_Injected(ref parameters, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003200 File Offset: 0x00001400
		[FreeFunction("ScheduleManagedJobParallelFor", ThrowsException = true, IsThreadSafe = true)]
		public static JobHandle ScheduleParallelFor(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelFor_Injected(ref parameters, arrayLength, innerloopBatchCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003218 File Offset: 0x00001418
		[FreeFunction("ScheduleManagedJobParallelForDeferArraySize", ThrowsException = true, IsThreadSafe = true)]
		public unsafe static JobHandle ScheduleParallelForDeferArraySize(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForDeferArraySize_Injected(ref parameters, innerloopBatchCount, listData, listDataAtomicSafetyHandle, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003234 File Offset: 0x00001434
		[FreeFunction("ScheduleManagedJobParallelForTransform", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransform(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForTransform_Injected(ref parameters, transfromAccesssArray, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000324C File Offset: 0x0000144C
		[FreeFunction("ScheduleManagedJobParallelForTransformReadOnly", ThrowsException = true)]
		public static JobHandle ScheduleParallelForTransformReadOnly(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount)
		{
			JobHandle jobHandle;
			JobsUtility.ScheduleParallelForTransformReadOnly_Injected(ref parameters, transfromAccesssArray, innerloopBatchCount, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600011C RID: 284
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void PatchBufferMinMaxRanges(IntPtr bufferRangePatchData, void* jobdata, int startIndex, int rangeSize);

		// Token: 0x0600011D RID: 285
		[FreeFunction(ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0, object managedJobFunction1, object managedJobFunction2);

		// Token: 0x0600011E RID: 286 RVA: 0x00003264 File Offset: 0x00001464
		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !1")]
		public static IntPtr CreateJobReflectionData(Type type, JobType jobType, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003284 File Offset: 0x00001484
		public static IntPtr CreateJobReflectionData(Type type, object managedJobFunction0, object managedJobFunction1 = null, object managedJobFunction2 = null)
		{
			return JobsUtility.CreateJobReflectionData(type, type, managedJobFunction0, managedJobFunction1, managedJobFunction2);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000032A0 File Offset: 0x000014A0
		[Obsolete("JobType is obsolete. The parameter should be removed. (UnityUpgradable) -> !2")]
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, JobType jobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000032BC File Offset: 0x000014BC
		public static IntPtr CreateJobReflectionData(Type wrapperJobType, Type userJobType, object managedJobFunction0)
		{
			return JobsUtility.CreateJobReflectionData(wrapperJobType, userJobType, managedJobFunction0, null, null);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000122 RID: 290
		public static extern bool IsExecutingJob
		{
			[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000123 RID: 291
		// (set) Token: 0x06000124 RID: 292
		public static extern bool JobDebuggerEnabled
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000125 RID: 293
		// (set) Token: 0x06000126 RID: 294
		public static extern bool JobCompilerEnabled
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000127 RID: 295
		[FreeFunction("JobSystem::GetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetJobQueueWorkerThreadCount();

		// Token: 0x06000128 RID: 296
		[FreeFunction("JobSystem::ForceSetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetJobQueueMaximumActiveThreadCount(int count);

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000129 RID: 297
		public static extern int JobWorkerMaximumCount
		{
			[FreeFunction("JobSystem::GetJobQueueMaximumThreadCount")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600012A RID: 298
		[FreeFunction("JobSystem::ResetJobQueueWorkerThreadCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetJobWorkerCount();

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000032D8 File Offset: 0x000014D8
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000032F0 File Offset: 0x000014F0
		public static int JobWorkerCount
		{
			get
			{
				return JobsUtility.GetJobQueueWorkerThreadCount();
			}
			set
			{
				bool flag = value < 0 || value > JobsUtility.JobWorkerMaximumCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("JobWorkerCount", string.Format("Invalid JobWorkerCount {0} must be in the range 0 -> {1}", value, JobsUtility.JobWorkerMaximumCount));
				}
				JobsUtility.SetJobQueueMaximumActiveThreadCount(value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600012D RID: 301
		public static extern int ThreadIndex
		{
			[FreeFunction("GetJobWorkerIndex", IsThreadSafe = true)]
			[BurstAuthorizedExternalMethod]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600012E RID: 302
		public static extern int ThreadIndexCount
		{
			[BurstAuthorizedExternalMethod]
			[FreeFunction("GetJobWorkerIndexCount", IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600012F RID: 303
		[FreeFunction("IsJobQueueBatchingEnabled")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetJobBatchingEnabled();

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000333E File Offset: 0x0000153E
		internal static bool JobBatchingEnabled
		{
			get
			{
				return JobsUtility.GetJobBatchingEnabled();
			}
		}

		// Token: 0x06000131 RID: 305
		[FreeFunction("JobDebuggerGetSystemIdCellPtr")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSystemIdCellPtr();

		// Token: 0x06000132 RID: 306
		[FreeFunction("JobDebuggerClearSystemIds")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ClearSystemIds();

		// Token: 0x06000133 RID: 307
		[FreeFunction("JobDebuggerGetSystemIdMappings")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int GetSystemIdMappings(JobHandle* handles, int* systemIds, int maxCount);

		// Token: 0x06000134 RID: 308 RVA: 0x00003348 File Offset: 0x00001548
		[RequiredByNativeCode]
		private static void InvokePanicFunction()
		{
			JobsUtility.PanicFunction_ panicFunction = JobsUtility.PanicFunction;
			bool flag = panicFunction == null;
			if (!flag)
			{
				panicFunction();
			}
		}

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Schedule_Injected(ref JobsUtility.JobScheduleParameters parameters, out JobHandle ret);

		// Token: 0x06000136 RID: 310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelFor_Injected(ref JobsUtility.JobScheduleParameters parameters, int arrayLength, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x06000137 RID: 311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleParallelForDeferArraySize_Injected(ref JobsUtility.JobScheduleParameters parameters, int innerloopBatchCount, void* listData, void* listDataAtomicSafetyHandle, out JobHandle ret);

		// Token: 0x06000138 RID: 312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransform_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, out JobHandle ret);

		// Token: 0x06000139 RID: 313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleParallelForTransformReadOnly_Injected(ref JobsUtility.JobScheduleParameters parameters, IntPtr transfromAccesssArray, int innerloopBatchCount, out JobHandle ret);

		// Token: 0x04000118 RID: 280
		public const int MaxJobThreadCount = 128;

		// Token: 0x04000119 RID: 281
		public const int CacheLineSize = 64;

		// Token: 0x0400011A RID: 282
		internal static JobsUtility.PanicFunction_ PanicFunction;

		// Token: 0x02000059 RID: 89
		public struct JobScheduleParameters
		{
			// Token: 0x0600013A RID: 314 RVA: 0x0000336D File Offset: 0x0000156D
			public unsafe JobScheduleParameters(void* i_jobData, IntPtr i_reflectionData, JobHandle i_dependency, ScheduleMode i_scheduleMode)
			{
				this.Dependency = i_dependency;
				this.JobDataPtr = (IntPtr)i_jobData;
				this.ReflectionData = i_reflectionData;
				this.ScheduleMode = (int)i_scheduleMode;
			}

			// Token: 0x0400011B RID: 283
			public JobHandle Dependency;

			// Token: 0x0400011C RID: 284
			public int ScheduleMode;

			// Token: 0x0400011D RID: 285
			public IntPtr ReflectionData;

			// Token: 0x0400011E RID: 286
			public IntPtr JobDataPtr;
		}

		// Token: 0x0200005A RID: 90
		// (Invoke) Token: 0x0600013C RID: 316
		internal delegate void PanicFunction_();
	}
}
