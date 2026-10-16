using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000329 RID: 809
	internal struct UnsafeMeshGenerationNode
	{
		// Token: 0x06001750 RID: 5968 RVA: 0x0005A4BE File Offset: 0x000586BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private MeshGenerationNodeImpl GetManaged()
		{
			return (MeshGenerationNodeImpl)this.m_Handle.Target;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0005A4D0 File Offset: 0x000586D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Create(GCHandle handle, out UnsafeMeshGenerationNode node)
		{
			node = new UnsafeMeshGenerationNode
			{
				m_Handle = handle
			};
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0005A4F5 File Offset: 0x000586F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DrawMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null)
		{
			this.GetManaged().DrawMesh(vertices, indices, texture, false);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0005A508 File Offset: 0x00058708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void DrawMeshInternal(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null, bool skipAtlas = false)
		{
			this.GetManaged().DrawMesh(vertices, indices, texture, skipAtlas);
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0005A51C File Offset: 0x0005871C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void DrawGradientsInternal(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, VectorImage gradientsOwner)
		{
			this.GetManaged().DrawGradients(vertices, indices, gradientsOwner);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0005A52E File Offset: 0x0005872E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Entry GetParentEntry()
		{
			return this.GetManaged().GetParentEntry();
		}

		// Token: 0x04000CD3 RID: 3283
		private GCHandle m_Handle;
	}
}
