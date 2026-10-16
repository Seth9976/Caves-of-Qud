using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200032A RID: 810
	internal class MeshGenerationNodeImpl : IDisposable
	{
		// Token: 0x06001756 RID: 5974 RVA: 0x0005A53B File Offset: 0x0005873B
		public MeshGenerationNodeImpl()
		{
			this.m_SelfHandle = GCHandle.Alloc(this);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0005A551 File Offset: 0x00058751
		public void Init(Entry parentEntry, EntryRecorder entryRecorder, bool safe)
		{
			Debug.Assert(this.m_ParentEntry == null);
			Debug.Assert(parentEntry != null);
			Debug.Assert(entryRecorder != null);
			this.m_ParentEntry = parentEntry;
			this.m_EntryRecorder = entryRecorder;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0005A585 File Offset: 0x00058785
		public void Reset()
		{
			Debug.Assert(this.m_ParentEntry != null);
			Debug.Assert(this.m_EntryRecorder != null);
			this.m_ParentEntry = null;
			this.m_EntryRecorder = null;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0005A5B4 File Offset: 0x000587B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetNode(out MeshGenerationNode node)
		{
			MeshGenerationNode.Create(this.m_SelfHandle, out node);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0005A5C4 File Offset: 0x000587C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetUnsafeNode(out UnsafeMeshGenerationNode node)
		{
			UnsafeMeshGenerationNode.Create(this.m_SelfHandle, out node);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0005A5D4 File Offset: 0x000587D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entry GetParentEntry()
		{
			return this.m_ParentEntry;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005A5DC File Offset: 0x000587DC
		public void DrawMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null, bool skipAtlas = false)
		{
			bool flag = vertices.Length == 0 || indices.Length == 0;
			if (!flag)
			{
				this.m_EntryRecorder.DrawMesh(this.m_ParentEntry, vertices, indices, texture, skipAtlas);
			}
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005A620 File Offset: 0x00058820
		public void DrawGradients(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, VectorImage gradientsOwner)
		{
			bool flag = vertices.Length == 0 || indices.Length == 0 || gradientsOwner == null;
			if (!flag)
			{
				this.m_EntryRecorder.DrawGradients(this.m_ParentEntry, vertices, indices, gradientsOwner);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0005A665 File Offset: 0x00058865
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x0005A66D File Offset: 0x0005886D
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001760 RID: 5984 RVA: 0x0005A676 File Offset: 0x00058876
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0005A688 File Offset: 0x00058888
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.m_ParentEntry != null;
					if (flag)
					{
						this.Reset();
					}
					this.m_SelfHandle.Free();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000CD4 RID: 3284
		private GCHandle m_SelfHandle;

		// Token: 0x04000CD5 RID: 3285
		private Entry m_ParentEntry;

		// Token: 0x04000CD6 RID: 3286
		private EntryRecorder m_EntryRecorder;
	}
}
