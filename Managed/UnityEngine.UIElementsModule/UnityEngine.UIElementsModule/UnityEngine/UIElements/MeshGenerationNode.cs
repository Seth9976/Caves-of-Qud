using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000328 RID: 808
	[NativeContainer]
	public struct MeshGenerationNode
	{
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x0005A470 File Offset: 0x00058670
		internal UnsafeMeshGenerationNode unsafeNode
		{
			get
			{
				return this.m_UnsafeNode;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0005A488 File Offset: 0x00058688
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void Create(GCHandle handle, out MeshGenerationNode node)
		{
			node = default(MeshGenerationNode);
			UnsafeMeshGenerationNode.Create(handle, out node.m_UnsafeNode);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0005A49F File Offset: 0x0005869F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DrawMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null)
		{
			this.m_UnsafeNode.DrawMesh(vertices, indices, texture);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0005A4B1 File Offset: 0x000586B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Entry GetParentEntry()
		{
			return this.m_UnsafeNode.GetParentEntry();
		}

		// Token: 0x04000CD2 RID: 3282
		private UnsafeMeshGenerationNode m_UnsafeNode;
	}
}
