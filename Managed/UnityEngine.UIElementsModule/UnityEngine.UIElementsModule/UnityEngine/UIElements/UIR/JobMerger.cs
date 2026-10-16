using System;
using Unity.Collections;
using Unity.Jobs;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000647 RID: 1607
	internal class JobMerger : IDisposable
	{
		// Token: 0x06002F1D RID: 12061 RVA: 0x000AC1F5 File Offset: 0x000AA3F5
		public JobMerger(int capacity)
		{
			Debug.Assert(capacity > 1);
			this.m_Jobs = new NativeArray<JobHandle>(capacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000AC218 File Offset: 0x000AA418
		public void Add(JobHandle job)
		{
			bool flag = this.m_JobCount < this.m_Jobs.Length;
			if (flag)
			{
				int jobCount = this.m_JobCount;
				this.m_JobCount = jobCount + 1;
				this.m_Jobs[jobCount] = job;
			}
			else
			{
				this.m_Jobs[0] = JobHandle.CombineDependencies(this.m_Jobs);
				this.m_Jobs[1] = job;
				this.m_JobCount = 2;
			}
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000AC28C File Offset: 0x000AA48C
		public JobHandle MergeAndReset()
		{
			JobHandle jobHandle = default(JobHandle);
			bool flag = this.m_JobCount > 1;
			if (flag)
			{
				jobHandle = JobHandle.CombineDependencies(this.m_Jobs.Slice<JobHandle>(0, this.m_JobCount));
			}
			else
			{
				bool flag2 = this.m_JobCount == 1;
				if (flag2)
				{
					jobHandle = this.m_Jobs[0];
				}
			}
			this.m_JobCount = 0;
			return jobHandle;
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x000AC2EF File Offset: 0x000AA4EF
		// (set) Token: 0x06002F21 RID: 12065 RVA: 0x000AC2F7 File Offset: 0x000AA4F7
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F22 RID: 12066 RVA: 0x000AC300 File Offset: 0x000AA500
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000AC314 File Offset: 0x000AA514
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Jobs.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001656 RID: 5718
		private NativeArray<JobHandle> m_Jobs;

		// Token: 0x04001657 RID: 5719
		private int m_JobCount;
	}
}
