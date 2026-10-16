using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000678 RID: 1656
	internal class TempMeshAllocatorImpl : IDisposable
	{
		// Token: 0x0600304A RID: 12362 RVA: 0x000B6B58 File Offset: 0x000B4D58
		public TempMeshAllocatorImpl()
		{
			this.m_GCHandle = GCHandle.Alloc(this);
			this.m_ThreadData = new TempMeshAllocatorImpl.ThreadData[JobsUtility.ThreadIndexCount];
			for (int i = 0; i < JobsUtility.ThreadIndexCount; i++)
			{
				this.m_ThreadData[i].allocations = new List<IntPtr>();
			}
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000B6BE9 File Offset: 0x000B4DE9
		public void CreateNativeHandle(out TempMeshAllocator allocator)
		{
			TempMeshAllocator.Create(this.m_GCHandle, out allocator);
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000B6BFC File Offset: 0x000B4DFC
		private unsafe NativeSlice<T> Allocate<T>(int count, int alignment) where T : struct
		{
			ref TempMeshAllocatorImpl.ThreadData ptr = ref this.m_ThreadData[UIRUtility.GetThreadIndex()];
			Debug.Assert(count > 0);
			long num = (long)(UnsafeUtility.SizeOf<T>() * count);
			void* ptr2 = UnsafeUtility.Malloc(num, UnsafeUtility.AlignOf<T>(), Allocator.TempJob);
			ptr.allocations.Add((IntPtr)ptr2);
			NativeArray<T> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr2, count, Allocator.Invalid);
			return nativeArray;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000B6C64 File Offset: 0x000B4E64
		public void AllocateTempMesh(int vertexCount, int indexCount, out NativeSlice<Vertex> vertices, out NativeSlice<ushort> indices)
		{
			bool flag = (long)vertexCount > (long)((ulong)UIRenderDevice.maxVerticesPerPage);
			if (flag)
			{
				throw new ArgumentOutOfRangeException("vertexCount", string.Format("Attempting to allocate {0} vertices which exceeds the limit of {1}.", vertexCount, UIRenderDevice.maxVerticesPerPage));
			}
			bool flag2 = !JobsUtility.IsExecutingJob;
			if (flag2)
			{
				bool disposed = this.disposed;
				if (disposed)
				{
					DisposeHelper.NotifyDisposedUsed(this);
					vertices = default(NativeSlice<Vertex>);
					indices = default(NativeSlice<ushort>);
				}
				else
				{
					vertices = ((vertexCount > 0) ? this.m_VertexPool.Alloc(vertexCount) : default(NativeSlice<Vertex>));
					indices = ((indexCount > 0) ? this.m_IndexPool.Alloc(indexCount) : default(NativeSlice<ushort>));
				}
			}
			else
			{
				vertices = ((vertexCount > 0) ? this.Allocate<Vertex>(vertexCount, 4) : default(NativeSlice<Vertex>));
				indices = ((indexCount > 0) ? this.Allocate<ushort>(indexCount, 2) : default(NativeSlice<ushort>));
			}
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000B6D5C File Offset: 0x000B4F5C
		public void Clear()
		{
			for (int i = 0; i < this.m_ThreadData.Length; i++)
			{
				foreach (IntPtr intPtr in this.m_ThreadData[i].allocations)
				{
					UnsafeUtility.Free(intPtr.ToPointer(), Allocator.TempJob);
				}
				this.m_ThreadData[i].allocations.Clear();
			}
			this.m_VertexPool.Reset();
			this.m_IndexPool.Reset();
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600304F RID: 12367 RVA: 0x000B6E0C File Offset: 0x000B500C
		// (set) Token: 0x06003050 RID: 12368 RVA: 0x000B6E14 File Offset: 0x000B5014
		private protected bool disposed { protected get; private set; }

		// Token: 0x06003051 RID: 12369 RVA: 0x000B6E1D File Offset: 0x000B501D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000B6E30 File Offset: 0x000B5030
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Clear();
					this.m_GCHandle.Free();
					this.m_VertexPool.Dispose();
					this.m_IndexPool.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x040017A3 RID: 6051
		private GCHandle m_GCHandle;

		// Token: 0x040017A4 RID: 6052
		private TempMeshAllocatorImpl.ThreadData[] m_ThreadData;

		// Token: 0x040017A5 RID: 6053
		private TempAllocator<Vertex> m_VertexPool = new TempAllocator<Vertex>(8192, 2048, 65536);

		// Token: 0x040017A6 RID: 6054
		private TempAllocator<ushort> m_IndexPool = new TempAllocator<ushort>(16384, 4096, 131072);

		// Token: 0x02000679 RID: 1657
		private struct ThreadData
		{
			// Token: 0x040017A8 RID: 6056
			public List<IntPtr> allocations;
		}
	}
}
