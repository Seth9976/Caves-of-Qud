using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000324 RID: 804
	public class MeshWriteData
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x000572B6 File Offset: 0x000554B6
		internal MeshWriteData()
		{
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x00059CE8 File Offset: 0x00057EE8
		public int vertexCount
		{
			get
			{
				return this.m_Vertices.Length;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x00059D08 File Offset: 0x00057F08
		public int indexCount
		{
			get
			{
				return this.m_Indices.Length;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00059D25 File Offset: 0x00057F25
		[Obsolete("Texture coordinates are now automatically remapped by the renderer. You are no longer required to remap the UV coordinates in the provided rectangle.")]
		public Rect uvRegion
		{
			get
			{
				return new Rect(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00059D40 File Offset: 0x00057F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetNextVertex(Vertex vertex)
		{
			int num = this.currentVertex;
			this.currentVertex = num + 1;
			this.m_Vertices[num] = vertex;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00059D6C File Offset: 0x00057F6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetNextIndex(ushort index)
		{
			int num = this.currentIndex;
			this.currentIndex = num + 1;
			this.m_Indices[num] = index;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00059D98 File Offset: 0x00057F98
		public void SetAllVertices(Vertex[] vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00059DE0 File Offset: 0x00057FE0
		public void SetAllVertices(NativeSlice<Vertex> vertices)
		{
			bool flag = this.currentVertex == 0;
			if (flag)
			{
				this.m_Vertices.CopyFrom(vertices);
				this.currentVertex = this.m_Vertices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllVertices may not be called after using SetNextVertex");
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00059E28 File Offset: 0x00058028
		public void SetAllIndices(ushort[] indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00059E70 File Offset: 0x00058070
		public void SetAllIndices(NativeSlice<ushort> indices)
		{
			bool flag = this.currentIndex == 0;
			if (flag)
			{
				this.m_Indices.CopyFrom(indices);
				this.currentIndex = this.m_Indices.Length;
				return;
			}
			throw new InvalidOperationException("SetAllIndices may not be called after using SetNextIndex");
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00059EB8 File Offset: 0x000580B8
		internal void Reset(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices)
		{
			this.m_Vertices = vertices;
			this.m_Indices = indices;
			this.currentIndex = (this.currentVertex = 0);
		}

		// Token: 0x04000CBB RID: 3259
		internal NativeSlice<Vertex> m_Vertices;

		// Token: 0x04000CBC RID: 3260
		internal NativeSlice<ushort> m_Indices;

		// Token: 0x04000CBD RID: 3261
		internal int currentIndex;

		// Token: 0x04000CBE RID: 3262
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int currentVertex;
	}
}
