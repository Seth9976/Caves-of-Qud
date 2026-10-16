using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Jobs
{
	// Token: 0x02000050 RID: 80
	[NativeType(Header = "Runtime/Jobs/ScriptBindings/JobsBindings.h")]
	public struct JobHandle : IEquatable<JobHandle>
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00002E88 File Offset: 0x00001088
		public void Complete()
		{
			bool flag = this.jobGroup == 0UL;
			if (!flag)
			{
				JobHandle.ScheduleBatchedJobsAndComplete(ref this);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002EB0 File Offset: 0x000010B0
		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle* ptr;
			checked
			{
				ptr = stackalloc JobHandle[unchecked((UIntPtr)2) * (UIntPtr)sizeof(JobHandle)];
				*ptr = job0;
			}
			ptr[1] = job1;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 2);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002F00 File Offset: 0x00001100
		public unsafe static void CompleteAll(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle* ptr;
			checked
			{
				ptr = stackalloc JobHandle[unchecked((UIntPtr)3) * (UIntPtr)sizeof(JobHandle)];
				*ptr = job0;
			}
			ptr[1] = job1;
			ptr[2] = job2;
			JobHandle.ScheduleBatchedJobsAndCompleteAll((void*)ptr, 3);
			job0 = default(JobHandle);
			job1 = default(JobHandle);
			job2 = default(JobHandle);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002F6C File Offset: 0x0000116C
		public static void CompleteAll(NativeArray<JobHandle> jobs)
		{
			JobHandle.ScheduleBatchedJobsAndCompleteAll(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002F84 File Offset: 0x00001184
		public bool IsCompleted
		{
			get
			{
				return JobHandle.ScheduleBatchedJobsAndIsCompleted(ref this);
			}
		}

		// Token: 0x060000FB RID: 251
		[NativeMethod("ScheduleBatchedScriptingJobs", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ScheduleBatchedJobs();

		// Token: 0x060000FC RID: 252
		[NativeMethod("ScheduleBatchedScriptingJobsAndComplete", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScheduleBatchedJobsAndComplete(ref JobHandle job);

		// Token: 0x060000FD RID: 253
		[NativeMethod("ScheduleBatchedScriptingJobsAndIsCompleted", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ScheduleBatchedJobsAndIsCompleted(ref JobHandle job);

		// Token: 0x060000FE RID: 254
		[NativeMethod("ScheduleBatchedScriptingJobsAndCompleteAll", IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleBatchedJobsAndCompleteAll(void* jobs, int count);

		// Token: 0x060000FF RID: 255 RVA: 0x00002F9C File Offset: 0x0000119C
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1)
		{
			return JobHandle.CombineDependenciesInternal2(ref job0, ref job1);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002FB8 File Offset: 0x000011B8
		public static JobHandle CombineDependencies(JobHandle job0, JobHandle job1, JobHandle job2)
		{
			return JobHandle.CombineDependenciesInternal3(ref job0, ref job1, ref job2);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002FD8 File Offset: 0x000011D8
		public static JobHandle CombineDependencies(NativeArray<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002FFC File Offset: 0x000011FC
		public static JobHandle CombineDependencies(NativeSlice<JobHandle> jobs)
		{
			return JobHandle.CombineDependenciesInternalPtr(jobs.GetUnsafeReadOnlyPtr<JobHandle>(), jobs.Length);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003020 File Offset: 0x00001220
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		private static JobHandle CombineDependenciesInternal2(ref JobHandle job0, ref JobHandle job1)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternal2_Injected(ref job0, ref job1, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003038 File Offset: 0x00001238
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		private static JobHandle CombineDependenciesInternal3(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternal3_Injected(ref job0, ref job1, ref job2, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003050 File Offset: 0x00001250
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe static JobHandle CombineDependenciesInternalPtr(void* jobs, int count)
		{
			JobHandle jobHandle;
			JobHandle.CombineDependenciesInternalPtr_Injected(jobs, count, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003068 File Offset: 0x00001268
		[NativeMethod(IsFreeFunction = true, IsThreadSafe = true)]
		public static bool CheckFenceIsDependencyOrDidSyncFence(JobHandle jobHandle, JobHandle dependsOn)
		{
			return JobHandle.CheckFenceIsDependencyOrDidSyncFence_Injected(ref jobHandle, ref dependsOn);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003080 File Offset: 0x00001280
		public bool Equals(JobHandle other)
		{
			return this.jobGroup == other.jobGroup;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000030A0 File Offset: 0x000012A0
		public override bool Equals(object obj)
		{
			return obj is JobHandle && this == (JobHandle)obj;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000030D0 File Offset: 0x000012D0
		public static bool operator ==(JobHandle a, JobHandle b)
		{
			return a.jobGroup == b.jobGroup;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000030F0 File Offset: 0x000012F0
		public static bool operator !=(JobHandle a, JobHandle b)
		{
			return !(a == b);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000310C File Offset: 0x0000130C
		public override int GetHashCode()
		{
			return this.jobGroup.GetHashCode();
		}

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal2_Injected(ref JobHandle job0, ref JobHandle job1, out JobHandle ret);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineDependenciesInternal3_Injected(ref JobHandle job0, ref JobHandle job1, ref JobHandle job2, out JobHandle ret);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void CombineDependenciesInternalPtr_Injected(void* jobs, int count, out JobHandle ret);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckFenceIsDependencyOrDidSyncFence_Injected([In] ref JobHandle jobHandle, [In] ref JobHandle dependsOn);

		// Token: 0x04000106 RID: 262
		internal ulong jobGroup;

		// Token: 0x04000107 RID: 263
		internal int version;
	}
}
