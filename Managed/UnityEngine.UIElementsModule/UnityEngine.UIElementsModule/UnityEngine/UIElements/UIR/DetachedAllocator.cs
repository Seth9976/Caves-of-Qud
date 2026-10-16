using System;
using System.Collections.Generic;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200061B RID: 1563
	internal class DetachedAllocator : IDisposable
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000A4735 File Offset: 0x000A2935
		public List<MeshWriteData> meshes
		{
			get
			{
				return this.m_MeshWriteDataPool.GetRange(0, this.m_MeshWriteDataCount);
			}
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000A474C File Offset: 0x000A294C
		public DetachedAllocator()
		{
			this.m_MeshWriteDataPool = new List<MeshWriteData>(16);
			this.m_MeshWriteDataCount = 0;
			this.m_VertsPool = new TempAllocator<Vertex>(8192, 2048, 65536);
			this.m_IndexPool = new TempAllocator<ushort>(16384, 4096, 131072);
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000A47A9 File Offset: 0x000A29A9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000A47BC File Offset: 0x000A29BC
		protected void Dispose(bool disposing)
		{
			bool disposed = this.m_Disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_VertsPool.Dispose();
					this.m_IndexPool.Dispose();
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000A4800 File Offset: 0x000A2A00
		public MeshWriteData Alloc(int vertexCount, int indexCount)
		{
			bool flag = this.m_MeshWriteDataCount < this.m_MeshWriteDataPool.Count;
			MeshWriteData meshWriteData;
			if (flag)
			{
				meshWriteData = this.m_MeshWriteDataPool[this.m_MeshWriteDataCount];
			}
			else
			{
				meshWriteData = new MeshWriteData();
				this.m_MeshWriteDataPool.Add(meshWriteData);
			}
			this.m_MeshWriteDataCount++;
			bool flag2 = vertexCount == 0 || indexCount == 0;
			MeshWriteData meshWriteData2;
			if (flag2)
			{
				meshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
				meshWriteData2 = meshWriteData;
			}
			else
			{
				meshWriteData.Reset(this.m_VertsPool.Alloc(vertexCount), this.m_IndexPool.Alloc(indexCount));
				meshWriteData2 = meshWriteData;
			}
			return meshWriteData2;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000A48B2 File Offset: 0x000A2AB2
		public void Clear()
		{
			this.m_VertsPool.Reset();
			this.m_IndexPool.Reset();
			this.m_MeshWriteDataCount = 0;
		}

		// Token: 0x040014FE RID: 5374
		private TempAllocator<Vertex> m_VertsPool;

		// Token: 0x040014FF RID: 5375
		private TempAllocator<ushort> m_IndexPool;

		// Token: 0x04001500 RID: 5376
		private List<MeshWriteData> m_MeshWriteDataPool;

		// Token: 0x04001501 RID: 5377
		private int m_MeshWriteDataCount;

		// Token: 0x04001502 RID: 5378
		private bool m_Disposed;
	}
}
