using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200032C RID: 812
	public class Painter2D : IDisposable
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0005A893 File Offset: 0x00058A93
		internal bool isDetached
		{
			get
			{
				return this.m_DetachedAllocator != null;
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0005A8A0 File Offset: 0x00058AA0
		internal Painter2D(MeshGenerationContext ctx)
		{
			this.m_Handle = new SafeHandleAccess(UIPainter2D.Create(false));
			this.m_Ctx = ctx;
			this.m_JobSnapshots = new List<Painter2D.Painter2DJobData>(32);
			this.m_OnMeshGenerationDelegate = new MeshGenerationCallback(this.OnMeshGeneration);
			this.Reset();
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0005A8FC File Offset: 0x00058AFC
		public Painter2D()
		{
			this.m_Handle = new SafeHandleAccess(UIPainter2D.Create(true));
			this.m_DetachedAllocator = new DetachedAllocator();
			Painter2D.isPainterActive = true;
			this.m_OnMeshGenerationDelegate = new MeshGenerationCallback(this.OnMeshGeneration);
			this.Reset();
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0005A954 File Offset: 0x00058B54
		internal void Reset()
		{
			UIPainter2D.Reset(this.m_Handle);
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0005A968 File Offset: 0x00058B68
		internal MeshWriteData Allocate(int vertexCount, int indexCount)
		{
			bool isDetached = this.isDetached;
			MeshWriteData meshWriteData;
			if (isDetached)
			{
				meshWriteData = this.m_DetachedAllocator.Alloc(vertexCount, indexCount);
			}
			else
			{
				meshWriteData = this.m_Ctx.Allocate(vertexCount, indexCount, null);
			}
			return meshWriteData;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0005A9A4 File Offset: 0x00058BA4
		public void Clear()
		{
			bool flag = !this.isDetached;
			if (flag)
			{
				Debug.LogError("Clear() cannot be called on a Painter2D associated with a MeshGenerationContext. You should create your own instance of Painter2D instead.");
			}
			else
			{
				this.m_DetachedAllocator.Clear();
				this.Reset();
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0005A9E0 File Offset: 0x00058BE0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0005A9F4 File Offset: 0x00058BF4
		private void Dispose(bool disposing)
		{
			bool disposed = this.m_Disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = !this.m_Handle.IsNull();
					if (flag)
					{
						UIPainter2D.Destroy(this.m_Handle);
						this.m_Handle = new SafeHandleAccess(IntPtr.Zero);
					}
					bool flag2 = this.m_DetachedAllocator != null;
					if (flag2)
					{
						this.m_DetachedAllocator.Dispose();
					}
					this.m_JobParameters.Dispose();
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x0005AA78 File Offset: 0x00058C78
		// (set) Token: 0x06001774 RID: 6004 RVA: 0x0005AA8A File Offset: 0x00058C8A
		public float lineWidth
		{
			get
			{
				return UIPainter2D.GetLineWidth(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetLineWidth(this.m_Handle, value);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x0005AA9E File Offset: 0x00058C9E
		// (set) Token: 0x06001776 RID: 6006 RVA: 0x0005AAB0 File Offset: 0x00058CB0
		public Color strokeColor
		{
			get
			{
				return UIPainter2D.GetStrokeColor(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetStrokeColor(this.m_Handle, value);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0005AAC4 File Offset: 0x00058CC4
		// (set) Token: 0x06001778 RID: 6008 RVA: 0x0005AAD6 File Offset: 0x00058CD6
		public Gradient strokeGradient
		{
			get
			{
				return UIPainter2D.GetStrokeGradient(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetStrokeGradient(this.m_Handle, value);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0005AAEA File Offset: 0x00058CEA
		// (set) Token: 0x0600177A RID: 6010 RVA: 0x0005AAFC File Offset: 0x00058CFC
		public Color fillColor
		{
			get
			{
				return UIPainter2D.GetFillColor(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetFillColor(this.m_Handle, value);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x0005AB10 File Offset: 0x00058D10
		// (set) Token: 0x0600177C RID: 6012 RVA: 0x0005AB22 File Offset: 0x00058D22
		public LineJoin lineJoin
		{
			get
			{
				return UIPainter2D.GetLineJoin(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetLineJoin(this.m_Handle, value);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600177D RID: 6013 RVA: 0x0005AB36 File Offset: 0x00058D36
		// (set) Token: 0x0600177E RID: 6014 RVA: 0x0005AB48 File Offset: 0x00058D48
		public LineCap lineCap
		{
			get
			{
				return UIPainter2D.GetLineCap(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetLineCap(this.m_Handle, value);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600177F RID: 6015 RVA: 0x0005AB5C File Offset: 0x00058D5C
		// (set) Token: 0x06001780 RID: 6016 RVA: 0x0005AB6E File Offset: 0x00058D6E
		public float miterLimit
		{
			get
			{
				return UIPainter2D.GetMiterLimit(this.m_Handle);
			}
			set
			{
				UIPainter2D.SetMiterLimit(this.m_Handle, value);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0005AB82 File Offset: 0x00058D82
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x0005AB89 File Offset: 0x00058D89
		internal static bool isPainterActive { get; set; }

		// Token: 0x06001783 RID: 6019 RVA: 0x0005AB94 File Offset: 0x00058D94
		private bool ValidateState()
		{
			bool flag = this.isDetached || Painter2D.isPainterActive;
			bool flag2 = !flag;
			if (flag2)
			{
				Debug.LogError("Cannot issue vector graphics commands outside of generateVisualContent callback");
			}
			return flag;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0005ABCC File Offset: 0x00058DCC
		public void BeginPath()
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.BeginPath(this.m_Handle);
			}
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0005ABFC File Offset: 0x00058DFC
		public void ClosePath()
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.ClosePath(this.m_Handle);
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0005AC2C File Offset: 0x00058E2C
		public void MoveTo(Vector2 pos)
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.MoveTo(this.m_Handle, pos);
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0005AC5C File Offset: 0x00058E5C
		public void LineTo(Vector2 pos)
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.LineTo(this.m_Handle, pos);
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0005AC8C File Offset: 0x00058E8C
		public void ArcTo(Vector2 p1, Vector2 p2, float radius)
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.ArcTo(this.m_Handle, p1, p2, radius);
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0005ACC0 File Offset: 0x00058EC0
		public void Arc(Vector2 center, float radius, Angle startAngle, Angle endAngle, ArcDirection direction = ArcDirection.Clockwise)
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.Arc(this.m_Handle, center, radius, startAngle.ToRadians(), endAngle.ToRadians(), direction);
			}
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0005AD00 File Offset: 0x00058F00
		public void BezierCurveTo(Vector2 p1, Vector2 p2, Vector2 p3)
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.BezierCurveTo(this.m_Handle, p1, p2, p3);
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0005AD34 File Offset: 0x00058F34
		public void QuadraticCurveTo(Vector2 p1, Vector2 p2)
		{
			bool flag = !this.ValidateState();
			if (!flag)
			{
				UIPainter2D.QuadraticCurveTo(this.m_Handle, p1, p2);
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0005AD64 File Offset: 0x00058F64
		public unsafe void Stroke()
		{
			using (Painter2D.s_StrokeMarker.Auto())
			{
				bool flag = !this.ValidateState();
				if (!flag)
				{
					bool isDetached = this.isDetached;
					if (isDetached)
					{
						MeshWriteDataInterface meshWriteDataInterface = UIPainter2D.Stroke(this.m_Handle);
						bool flag2 = meshWriteDataInterface.vertexCount == 0;
						if (!flag2)
						{
							MeshWriteData meshWriteData = this.Allocate(meshWriteDataInterface.vertexCount, meshWriteDataInterface.indexCount);
							NativeSlice<Vertex> nativeSlice = UIRenderDevice.PtrToSlice<Vertex>((void*)meshWriteDataInterface.vertices, meshWriteDataInterface.vertexCount);
							NativeSlice<ushort> nativeSlice2 = UIRenderDevice.PtrToSlice<ushort>((void*)meshWriteDataInterface.indices, meshWriteDataInterface.indexCount);
							meshWriteData.SetAllVertices(nativeSlice);
							meshWriteData.SetAllIndices(nativeSlice2);
						}
					}
					else
					{
						UnsafeMeshGenerationNode unsafeMeshGenerationNode;
						this.m_Ctx.InsertUnsafeMeshGenerationNode(out unsafeMeshGenerationNode);
						int num = UIPainter2D.TakeStrokeSnapshot(this.m_Handle);
						this.m_JobSnapshots.Add(new Painter2D.Painter2DJobData
						{
							node = unsafeMeshGenerationNode,
							snapshotIndex = num
						});
					}
				}
			}
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0005AE98 File Offset: 0x00059098
		public unsafe void Fill(FillRule fillRule = FillRule.NonZero)
		{
			using (Painter2D.s_FillMarker.Auto())
			{
				bool flag = !this.ValidateState();
				if (!flag)
				{
					bool isDetached = this.isDetached;
					if (isDetached)
					{
						MeshWriteDataInterface meshWriteDataInterface = UIPainter2D.Fill(this.m_Handle, fillRule);
						bool flag2 = meshWriteDataInterface.vertexCount == 0;
						if (!flag2)
						{
							MeshWriteData meshWriteData = this.Allocate(meshWriteDataInterface.vertexCount, meshWriteDataInterface.indexCount);
							NativeSlice<Vertex> nativeSlice = UIRenderDevice.PtrToSlice<Vertex>((void*)meshWriteDataInterface.vertices, meshWriteDataInterface.vertexCount);
							NativeSlice<ushort> nativeSlice2 = UIRenderDevice.PtrToSlice<ushort>((void*)meshWriteDataInterface.indices, meshWriteDataInterface.indexCount);
							meshWriteData.SetAllVertices(nativeSlice);
							meshWriteData.SetAllIndices(nativeSlice2);
						}
					}
					else
					{
						UnsafeMeshGenerationNode unsafeMeshGenerationNode;
						this.m_Ctx.InsertUnsafeMeshGenerationNode(out unsafeMeshGenerationNode);
						int num = UIPainter2D.TakeFillSnapshot(this.m_Handle, fillRule);
						this.m_JobSnapshots.Add(new Painter2D.Painter2DJobData
						{
							node = unsafeMeshGenerationNode,
							snapshotIndex = num
						});
					}
				}
			}
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0005AFD0 File Offset: 0x000591D0
		internal void ScheduleJobs(MeshGenerationContext mgc)
		{
			int count = this.m_JobSnapshots.Count;
			bool flag = count == 0;
			if (!flag)
			{
				bool flag2 = this.m_JobParameters.Length < count;
				if (flag2)
				{
					this.m_JobParameters.Dispose();
					this.m_JobParameters = new NativeArray<Painter2D.Painter2DJobData>(count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				}
				for (int i = 0; i < count; i++)
				{
					this.m_JobParameters[i] = this.m_JobSnapshots[i];
				}
				this.m_JobSnapshots.Clear();
				Painter2D.Painter2DJob painter2DJob = new Painter2D.Painter2DJob
				{
					painterHandle = this.m_Handle,
					jobParameters = this.m_JobParameters.Slice<Painter2D.Painter2DJobData>(0, count)
				};
				mgc.GetTempMeshAllocator(out painter2DJob.allocator);
				JobHandle jobHandle = painter2DJob.ScheduleOrRunJob(count, 1, default(JobHandle));
				mgc.AddMeshGenerationJob(jobHandle);
				mgc.AddMeshGenerationCallback(this.m_OnMeshGenerationDelegate, null, MeshGenerationCallbackType.Work, true);
			}
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005B0CE File Offset: 0x000592CE
		private void OnMeshGeneration(MeshGenerationContext ctx, object data)
		{
			UIPainter2D.ClearSnapshots(this.m_Handle);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0005B0E4 File Offset: 0x000592E4
		public bool SaveToVectorImage(VectorImage vectorImage)
		{
			bool flag = !this.isDetached;
			bool flag2;
			if (flag)
			{
				Debug.LogError("SaveToVectorImage cannot be called on a Painter2D associated with a MeshGenerationContext. You should create your own instance of Painter2D instead.");
				flag2 = false;
			}
			else
			{
				bool flag3 = vectorImage == null;
				if (flag3)
				{
					throw new NullReferenceException("The provided vectorImage is null");
				}
				List<MeshWriteData> meshes = this.m_DetachedAllocator.meshes;
				int num = 0;
				int num2 = 0;
				foreach (MeshWriteData meshWriteData in meshes)
				{
					num += meshWriteData.m_Vertices.Length;
					num2 += meshWriteData.m_Indices.Length;
				}
				Rect bbox = UIPainter2D.GetBBox(this.m_Handle);
				VectorImageVertex[] array = new VectorImageVertex[num];
				ushort[] array2 = new ushort[num2];
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				foreach (MeshWriteData meshWriteData2 in meshes)
				{
					NativeSlice<Vertex> vertices = meshWriteData2.m_Vertices;
					for (int i = 0; i < vertices.Length; i++)
					{
						Vertex vertex = vertices[i];
						Vector3 position = vertex.position;
						position.x -= bbox.x;
						position.y -= bbox.y;
						array[num3++] = new VectorImageVertex
						{
							position = new Vector3(position.x, position.y, Vertex.nearZ),
							tint = vertex.tint,
							uv = vertex.uv,
							flags = vertex.flags,
							circle = vertex.circle
						};
					}
					NativeSlice<ushort> indices = meshWriteData2.m_Indices;
					for (int j = 0; j < indices.Length; j++)
					{
						array2[num4++] = (ushort)((int)indices[j] + num5);
					}
					num5 += vertices.Length;
				}
				vectorImage.version = 0;
				vectorImage.vertices = array;
				vectorImage.indices = array2;
				vectorImage.size = bbox.size;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x04000CDC RID: 3292
		private MeshGenerationContext m_Ctx;

		// Token: 0x04000CDD RID: 3293
		internal DetachedAllocator m_DetachedAllocator;

		// Token: 0x04000CDE RID: 3294
		internal SafeHandleAccess m_Handle;

		// Token: 0x04000CDF RID: 3295
		private List<Painter2D.Painter2DJobData> m_JobSnapshots = null;

		// Token: 0x04000CE0 RID: 3296
		private NativeArray<Painter2D.Painter2DJobData> m_JobParameters;

		// Token: 0x04000CE1 RID: 3297
		private bool m_Disposed;

		// Token: 0x04000CE3 RID: 3299
		private static readonly ProfilerMarker s_StrokeMarker = new ProfilerMarker("Painter2D.Stroke");

		// Token: 0x04000CE4 RID: 3300
		private static readonly ProfilerMarker s_FillMarker = new ProfilerMarker("Painter2D.Fill");

		// Token: 0x04000CE5 RID: 3301
		private MeshGenerationCallback m_OnMeshGenerationDelegate;

		// Token: 0x0200032D RID: 813
		private struct Painter2DJobData
		{
			// Token: 0x04000CE6 RID: 3302
			public UnsafeMeshGenerationNode node;

			// Token: 0x04000CE7 RID: 3303
			public int snapshotIndex;
		}

		// Token: 0x0200032E RID: 814
		private struct Painter2DJob : IJobParallelFor
		{
			// Token: 0x06001792 RID: 6034 RVA: 0x0005B388 File Offset: 0x00059588
			public unsafe void Execute(int i)
			{
				Painter2D.Painter2DJobData painter2DJobData = this.jobParameters[i];
				MeshWriteDataInterface meshWriteDataInterface = UIPainter2D.ExecuteSnapshotFromJob(this.painterHandle, painter2DJobData.snapshotIndex);
				NativeSlice<Vertex> nativeSlice = UIRenderDevice.PtrToSlice<Vertex>((void*)meshWriteDataInterface.vertices, meshWriteDataInterface.vertexCount);
				NativeSlice<ushort> nativeSlice2 = UIRenderDevice.PtrToSlice<ushort>((void*)meshWriteDataInterface.indices, meshWriteDataInterface.indexCount);
				bool flag = nativeSlice.Length == 0 || nativeSlice2.Length == 0;
				if (!flag)
				{
					NativeSlice<Vertex> nativeSlice3;
					NativeSlice<ushort> nativeSlice4;
					this.allocator.AllocateTempMesh(nativeSlice.Length, nativeSlice2.Length, out nativeSlice3, out nativeSlice4);
					Debug.Assert(nativeSlice3.Length == nativeSlice.Length);
					Debug.Assert(nativeSlice4.Length == nativeSlice2.Length);
					nativeSlice3.CopyFrom(nativeSlice);
					nativeSlice4.CopyFrom(nativeSlice2);
					painter2DJobData.node.DrawMesh(nativeSlice3, nativeSlice4, null);
				}
			}

			// Token: 0x04000CE8 RID: 3304
			[NativeDisableUnsafePtrRestriction]
			public IntPtr painterHandle;

			// Token: 0x04000CE9 RID: 3305
			[ReadOnly]
			public TempMeshAllocator allocator;

			// Token: 0x04000CEA RID: 3306
			[ReadOnly]
			public NativeSlice<Painter2D.Painter2DJobData> jobParameters;
		}
	}
}
