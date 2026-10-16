using System;
using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000326 RID: 806
	public class MeshGenerationContext
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00059F6B File Offset: 0x0005816B
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x00059F73 File Offset: 0x00058173
		public VisualElement visualElement { get; private set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00059F7C File Offset: 0x0005817C
		public Painter2D painter2D
		{
			get
			{
				bool disposed = this.disposed;
				Painter2D painter2D;
				if (disposed)
				{
					Debug.LogError("Accessing painter2D on disposed MeshGenerationContext");
					painter2D = null;
				}
				else
				{
					bool flag = this.m_Painter2D == null;
					if (flag)
					{
						this.m_Painter2D = new Painter2D(this);
					}
					painter2D = this.m_Painter2D;
				}
				return painter2D;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00059FC7 File Offset: 0x000581C7
		internal bool hasPainter2D
		{
			get
			{
				return this.m_Painter2D != null;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x00059FD2 File Offset: 0x000581D2
		// (set) Token: 0x06001735 RID: 5941 RVA: 0x00059FDA File Offset: 0x000581DA
		internal IMeshGenerator meshGenerator { get; set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00059FE3 File Offset: 0x000581E3
		// (set) Token: 0x06001737 RID: 5943 RVA: 0x00059FEB File Offset: 0x000581EB
		internal EntryRecorder entryRecorder { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001738 RID: 5944 RVA: 0x00059FF4 File Offset: 0x000581F4
		// (set) Token: 0x06001739 RID: 5945 RVA: 0x00059FFC File Offset: 0x000581FC
		internal Entry parentEntry { get; private set; }

		// Token: 0x0600173A RID: 5946 RVA: 0x0005A005 File Offset: 0x00058205
		internal MeshGenerationContext(MeshWriteDataPool meshWriteDataPool, EntryRecorder entryRecorder, TempMeshAllocatorImpl allocator, MeshGenerationDeferrer meshGenerationDeferrer, MeshGenerationNodeManager meshGenerationNodeManager)
		{
			this.m_MeshWriteDataPool = meshWriteDataPool;
			this.m_Allocator = allocator;
			this.m_MeshGenerationDeferrer = meshGenerationDeferrer;
			this.m_MeshGenerationNodeManager = meshGenerationNodeManager;
			this.entryRecorder = entryRecorder;
			this.meshGenerator = new MeshGenerator(this);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0005A042 File Offset: 0x00058242
		public void AllocateTempMesh(int vertexCount, int indexCount, out NativeSlice<Vertex> vertices, out NativeSlice<ushort> indices)
		{
			this.m_Allocator.AllocateTempMesh(vertexCount, indexCount, out vertices, out indices);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0005A058 File Offset: 0x00058258
		public MeshWriteData Allocate(int vertexCount, int indexCount, Texture texture = null)
		{
			MeshWriteData meshWriteData2;
			using (MeshGenerationContext.k_AllocateMarker.Auto())
			{
				MeshWriteData meshWriteData = this.m_MeshWriteDataPool.Get();
				bool flag = vertexCount == 0 || indexCount == 0;
				if (flag)
				{
					meshWriteData.Reset(default(NativeSlice<Vertex>), default(NativeSlice<ushort>));
					meshWriteData2 = meshWriteData;
				}
				else
				{
					bool flag2 = (long)vertexCount > (long)((ulong)UIRenderDevice.maxVerticesPerPage);
					if (flag2)
					{
						throw new ArgumentOutOfRangeException("vertexCount", string.Format("Attempting to allocate {0} vertices which exceeds the limit of {1}.", vertexCount, UIRenderDevice.maxVerticesPerPage));
					}
					NativeSlice<Vertex> nativeSlice;
					NativeSlice<ushort> nativeSlice2;
					this.m_Allocator.AllocateTempMesh(vertexCount, indexCount, out nativeSlice, out nativeSlice2);
					Debug.Assert(nativeSlice.Length == vertexCount);
					Debug.Assert(nativeSlice2.Length == indexCount);
					meshWriteData.Reset(nativeSlice, nativeSlice2);
					this.entryRecorder.DrawMesh(this.parentEntry, meshWriteData.m_Vertices, meshWriteData.m_Indices, texture, false);
					meshWriteData2 = meshWriteData;
				}
			}
			return meshWriteData2;
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0005A16C File Offset: 0x0005836C
		public void DrawMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null)
		{
			bool flag = vertices.Length == 0 || indices.Length == 0;
			if (!flag)
			{
				this.entryRecorder.DrawMesh(this.parentEntry, vertices, indices, texture, false);
			}
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0005A1AC File Offset: 0x000583AC
		public void DrawVectorImage(VectorImage vectorImage, Vector2 offset, Angle rotationAngle, Vector2 scale)
		{
			using (MeshGenerationContext.k_DrawVectorImageMarker.Auto())
			{
				this.meshGenerator.DrawVectorImage(vectorImage, offset, rotationAngle, scale);
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0005A1FC File Offset: 0x000583FC
		public void DrawText(string text, Vector2 pos, float fontSize, Color color, FontAsset font = null)
		{
			bool flag = font == null;
			if (flag)
			{
				font = TextUtilities.GetFontAsset(this.visualElement);
			}
			this.meshGenerator.DrawText(text, pos, fontSize, color, font);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0005A236 File Offset: 0x00058436
		public void GetTempMeshAllocator(out TempMeshAllocator allocator)
		{
			this.m_Allocator.CreateNativeHandle(out allocator);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0005A248 File Offset: 0x00058448
		public void InsertMeshGenerationNode(out MeshGenerationNode node)
		{
			Entry entry = this.entryRecorder.InsertPlaceholder(this.parentEntry);
			this.m_MeshGenerationNodeManager.CreateNode(entry, out node);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0005A278 File Offset: 0x00058478
		internal void InsertUnsafeMeshGenerationNode(out UnsafeMeshGenerationNode node)
		{
			Entry entry = this.entryRecorder.InsertPlaceholder(this.parentEntry);
			this.m_MeshGenerationNodeManager.CreateUnsafeNode(entry, out node);
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0005A2A6 File Offset: 0x000584A6
		public void AddMeshGenerationJob(JobHandle jobHandle)
		{
			this.m_MeshGenerationDeferrer.AddMeshGenerationJob(jobHandle);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0005A2B6 File Offset: 0x000584B6
		internal void AddMeshGenerationCallback(MeshGenerationCallback callback, object userData, MeshGenerationCallbackType callbackType, bool isJobDependent)
		{
			this.m_MeshGenerationDeferrer.AddMeshGenerationCallback(callback, userData, callbackType, isJobDependent);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005A2CC File Offset: 0x000584CC
		internal void Begin(Entry parentEntry, VisualElement ve)
		{
			bool flag = this.visualElement != null;
			if (flag)
			{
				throw new InvalidOperationException("Begin can only be called when there is no target set. Did you forget to call End?");
			}
			bool flag2 = parentEntry == null;
			if (flag2)
			{
				throw new ArgumentException("The state of the provided MeshGenerationNode is invalid (entry is null).");
			}
			bool flag3 = parentEntry.firstChild != null;
			if (flag3)
			{
				throw new ArgumentException("The state of the provided MeshGenerationNode is invalid (entry isn't empty).");
			}
			bool flag4 = ve == null;
			if (flag4)
			{
				throw new ArgumentException("ve");
			}
			this.parentEntry = parentEntry;
			this.visualElement = ve;
			this.meshGenerator.currentElement = ve;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0005A350 File Offset: 0x00058550
		internal void End()
		{
			bool flag = this.visualElement == null;
			if (flag)
			{
				throw new InvalidOperationException("End can only be called after a successful call to Begin.");
			}
			this.meshGenerator.currentElement = null;
			this.visualElement = null;
			this.parentEntry = null;
			Painter2D painter2D = this.m_Painter2D;
			if (painter2D != null)
			{
				painter2D.Reset();
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0005A3A5 File Offset: 0x000585A5
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x0005A3AD File Offset: 0x000585AD
		internal bool disposed { get; private set; }

		// Token: 0x06001749 RID: 5961 RVA: 0x0005A3B6 File Offset: 0x000585B6
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0005A3C8 File Offset: 0x000585C8
		private void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Painter2D painter2D = this.m_Painter2D;
					if (painter2D != null)
					{
						painter2D.Dispose();
					}
					this.m_Painter2D = null;
					this.m_MeshWriteDataPool = null;
					this.entryRecorder = null;
					MeshGenerator meshGenerator = this.meshGenerator as MeshGenerator;
					if (meshGenerator != null)
					{
						meshGenerator.Dispose();
					}
					this.meshGenerator = null;
					this.m_Allocator = null;
					this.m_MeshGenerationDeferrer = null;
					this.m_MeshGenerationNodeManager = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000CC2 RID: 3266
		private Painter2D m_Painter2D;

		// Token: 0x04000CC3 RID: 3267
		private MeshWriteDataPool m_MeshWriteDataPool;

		// Token: 0x04000CC4 RID: 3268
		private TempMeshAllocatorImpl m_Allocator;

		// Token: 0x04000CC5 RID: 3269
		private MeshGenerationDeferrer m_MeshGenerationDeferrer;

		// Token: 0x04000CC6 RID: 3270
		private MeshGenerationNodeManager m_MeshGenerationNodeManager;

		// Token: 0x04000CCA RID: 3274
		private static readonly ProfilerMarker k_AllocateMarker = new ProfilerMarker("UIR.MeshGenerationContext.Allocate");

		// Token: 0x04000CCB RID: 3275
		private static readonly ProfilerMarker k_DrawVectorImageMarker = new ProfilerMarker("UIR.MeshGenerationContext.DrawVectorImage");

		// Token: 0x02000327 RID: 807
		[Flags]
		internal enum MeshFlags
		{
			// Token: 0x04000CCE RID: 3278
			None = 0,
			// Token: 0x04000CCF RID: 3279
			SkipDynamicAtlas = 2,
			// Token: 0x04000CD0 RID: 3280
			IsUsingVectorImageGradients = 4,
			// Token: 0x04000CD1 RID: 3281
			SliceTiled = 8
		}
	}
}
