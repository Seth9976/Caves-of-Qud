using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000331 RID: 817
	[NativeContainerIsReadOnly]
	[NativeContainer]
	public struct TempMeshAllocator
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x0005BB08 File Offset: 0x00059D08
		internal static void Create(GCHandle handle, out TempMeshAllocator allocator)
		{
			allocator = new TempMeshAllocator
			{
				m_Handle = handle
			};
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005BB30 File Offset: 0x00059D30
		public void AllocateTempMesh(int vertexCount, int indexCount, out NativeSlice<Vertex> vertices, out NativeSlice<ushort> indices)
		{
			TempMeshAllocatorImpl tempMeshAllocatorImpl = this.m_Handle.Target as TempMeshAllocatorImpl;
			Debug.Assert(tempMeshAllocatorImpl != null);
			tempMeshAllocatorImpl.AllocateTempMesh(vertexCount, indexCount, out vertices, out indices);
		}

		// Token: 0x04000CF4 RID: 3316
		private GCHandle m_Handle;
	}
}
