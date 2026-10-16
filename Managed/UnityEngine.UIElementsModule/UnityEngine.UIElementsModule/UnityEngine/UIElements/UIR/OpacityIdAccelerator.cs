using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200065A RID: 1626
	internal class OpacityIdAccelerator : IDisposable
	{
		// Token: 0x06002F91 RID: 12177 RVA: 0x000B1460 File Offset: 0x000AF660
		public void CreateJob(NativeSlice<Vertex> oldVerts, NativeSlice<Vertex> newVerts, Color32 opacityData, int vertexCount)
		{
			JobHandle jobHandle = new OpacityIdAccelerator.OpacityIdUpdateJob
			{
				oldVerts = oldVerts,
				newVerts = newVerts,
				opacityData = opacityData
			}.ScheduleOrRunJob(vertexCount, 128, default(JobHandle));
			bool flag = this.m_NextJobIndex == this.m_Jobs.Length;
			if (flag)
			{
				this.m_Jobs[0] = JobHandle.CombineDependencies(this.m_Jobs);
				this.m_NextJobIndex = 1;
				JobHandle.ScheduleBatchedJobs();
			}
			int nextJobIndex = this.m_NextJobIndex;
			this.m_NextJobIndex = nextJobIndex + 1;
			this.m_Jobs[nextJobIndex] = jobHandle;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000B1504 File Offset: 0x000AF704
		public void CompleteJobs()
		{
			bool flag = this.m_NextJobIndex > 0;
			if (flag)
			{
				bool flag2 = this.m_NextJobIndex > 1;
				if (flag2)
				{
					JobHandle.CombineDependencies(this.m_Jobs.Slice<JobHandle>(0, this.m_NextJobIndex)).Complete();
				}
				else
				{
					this.m_Jobs[0].Complete();
				}
			}
			this.m_NextJobIndex = 0;
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002F93 RID: 12179 RVA: 0x000B156C File Offset: 0x000AF76C
		// (set) Token: 0x06002F94 RID: 12180 RVA: 0x000B1574 File Offset: 0x000AF774
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002F95 RID: 12181 RVA: 0x000B157D File Offset: 0x000AF77D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000B1590 File Offset: 0x000AF790
		protected virtual void Dispose(bool disposing)
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

		// Token: 0x040016CE RID: 5838
		private const int k_VerticesPerBatch = 128;

		// Token: 0x040016CF RID: 5839
		private const int k_JobLimit = 256;

		// Token: 0x040016D0 RID: 5840
		private NativeArray<JobHandle> m_Jobs = new NativeArray<JobHandle>(256, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);

		// Token: 0x040016D1 RID: 5841
		private int m_NextJobIndex;

		// Token: 0x0200065B RID: 1627
		private struct OpacityIdUpdateJob : IJobParallelFor
		{
			// Token: 0x06002F98 RID: 12184 RVA: 0x000B15E0 File Offset: 0x000AF7E0
			public void Execute(int i)
			{
				Vertex vertex = this.oldVerts[i];
				vertex.opacityColorPages.r = this.opacityData.r;
				vertex.opacityColorPages.g = this.opacityData.g;
				vertex.ids.b = this.opacityData.b;
				this.newVerts[i] = vertex;
			}

			// Token: 0x040016D3 RID: 5843
			[NativeDisableContainerSafetyRestriction]
			public NativeSlice<Vertex> oldVerts;

			// Token: 0x040016D4 RID: 5844
			[NativeDisableContainerSafetyRestriction]
			public NativeSlice<Vertex> newVerts;

			// Token: 0x040016D5 RID: 5845
			public Color32 opacityData;
		}
	}
}
