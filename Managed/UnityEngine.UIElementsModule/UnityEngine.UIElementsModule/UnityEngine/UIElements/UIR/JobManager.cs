using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000643 RID: 1603
	internal class JobManager : IDisposable
	{
		// Token: 0x06002F12 RID: 12050 RVA: 0x000ABF29 File Offset: 0x000AA129
		public void Add(ref NudgeJobData job)
		{
			this.m_NudgeJobs.Add(ref job);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000ABF39 File Offset: 0x000AA139
		public void Add(ref ConvertMeshJobData job)
		{
			this.m_ConvertMeshJobs.Add(ref job);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000ABF49 File Offset: 0x000AA149
		public void Add(ref CopyMeshJobData job)
		{
			this.m_CopyMeshJobs.Add(ref job);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000ABF5C File Offset: 0x000AA15C
		public void CompleteNudgeJobs()
		{
			foreach (NativeSlice<NudgeJobData> nativeSlice in this.m_NudgeJobs.GetPages())
			{
				this.m_JobMerger.Add(JobProcessor.ScheduleNudgeJobs((IntPtr)nativeSlice.GetUnsafePtr<NudgeJobData>(), nativeSlice.Length));
			}
			this.m_JobMerger.MergeAndReset().Complete();
			this.m_NudgeJobs.Reset();
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000ABFF4 File Offset: 0x000AA1F4
		public void CompleteConvertMeshJobs()
		{
			foreach (NativeSlice<ConvertMeshJobData> nativeSlice in this.m_ConvertMeshJobs.GetPages())
			{
				this.m_JobMerger.Add(JobProcessor.ScheduleConvertMeshJobs((IntPtr)nativeSlice.GetUnsafePtr<ConvertMeshJobData>(), nativeSlice.Length));
			}
			this.m_JobMerger.MergeAndReset().Complete();
			this.m_ConvertMeshJobs.Reset();
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000AC08C File Offset: 0x000AA28C
		public void CompleteCopyMeshJobs()
		{
			foreach (NativeSlice<CopyMeshJobData> nativeSlice in this.m_CopyMeshJobs.GetPages())
			{
				this.m_JobMerger.Add(JobProcessor.ScheduleCopyMeshJobs((IntPtr)nativeSlice.GetUnsafePtr<CopyMeshJobData>(), nativeSlice.Length));
			}
			this.m_JobMerger.MergeAndReset().Complete();
			this.m_CopyMeshJobs.Reset();
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000AC124 File Offset: 0x000AA324
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000AC12C File Offset: 0x000AA32C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F1A RID: 12058 RVA: 0x000AC135 File Offset: 0x000AA335
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000AC148 File Offset: 0x000AA348
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_NudgeJobs.Dispose();
					this.m_ConvertMeshJobs.Dispose();
					this.m_CopyMeshJobs.Dispose();
					this.m_JobMerger.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x0400162D RID: 5677
		private NativePagedList<NudgeJobData> m_NudgeJobs = new NativePagedList<NudgeJobData>(64, Allocator.Persistent, Allocator.Persistent);

		// Token: 0x0400162E RID: 5678
		private NativePagedList<ConvertMeshJobData> m_ConvertMeshJobs = new NativePagedList<ConvertMeshJobData>(64, Allocator.Persistent, Allocator.Persistent);

		// Token: 0x0400162F RID: 5679
		private NativePagedList<CopyMeshJobData> m_CopyMeshJobs = new NativePagedList<CopyMeshJobData>(64, Allocator.Persistent, Allocator.Persistent);

		// Token: 0x04001630 RID: 5680
		private JobMerger m_JobMerger = new JobMerger(128);
	}
}
