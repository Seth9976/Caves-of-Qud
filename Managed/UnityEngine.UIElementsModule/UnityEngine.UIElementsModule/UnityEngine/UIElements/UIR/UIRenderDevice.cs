using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000620 RID: 1568
	internal class UIRenderDevice : IDisposable
	{
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000A56D7 File Offset: 0x000A38D7
		internal static uint maxVerticesPerPage
		{
			get
			{
				return 65535U;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000A56DE File Offset: 0x000A38DE
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x000A56E6 File Offset: 0x000A38E6
		internal bool breakBatches { get; set; }

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000A56EF File Offset: 0x000A38EF
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000A56F7 File Offset: 0x000A38F7
		internal bool isFlat { get; set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000A5700 File Offset: 0x000A3900
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x000A5708 File Offset: 0x000A3908
		internal bool drawsInCameras { get; set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000A5711 File Offset: 0x000A3911
		internal uint frameIndex
		{
			get
			{
				return this.m_FrameIndex;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000A5719 File Offset: 0x000A3919
		internal List<CommandList>[] commandLists
		{
			get
			{
				return this.m_CommandLists;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000A5721 File Offset: 0x000A3921
		internal List<CommandList> currentFrameCommandLists
		{
			get
			{
				return (this.m_CommandLists == null) ? null : this.m_CommandLists[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_CommandLists.Length))];
			}
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000A5748 File Offset: 0x000A3948
		static UIRenderDevice()
		{
			Utility.EngineUpdate += UIRenderDevice.OnEngineUpdateGlobal;
			Utility.FlushPendingResources += UIRenderDevice.OnFlushPendingResources;
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x000A57F3 File Offset: 0x000A39F3
		public UIRenderDevice(uint initialVertexCapacity = 0U, uint initialIndexCapacity = 0U)
			: this(initialVertexCapacity, initialIndexCapacity, false)
		{
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x000A5800 File Offset: 0x000A3A00
		protected UIRenderDevice(uint initialVertexCapacity, uint initialIndexCapacity, bool mockDevice)
		{
			this.m_MockDevice = mockDevice;
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			Debug.Assert(true);
			bool flag = UIRenderDevice.m_ActiveDeviceCount++ == 0;
			if (flag)
			{
				bool flag2 = !UIRenderDevice.m_SubscribedToNotifications && !this.m_MockDevice;
				if (flag2)
				{
					Utility.NotifyOfUIREvents(true);
					UIRenderDevice.m_SubscribedToNotifications = true;
				}
			}
			this.m_NextPageVertexCount = Math.Max(initialVertexCapacity / 2U, 2048U);
			this.m_LargeMeshVertexCount = this.m_NextPageVertexCount;
			this.m_IndexToVertexCountRatio = initialIndexCapacity / initialVertexCapacity;
			this.m_IndexToVertexCountRatio = Mathf.Max(this.m_IndexToVertexCountRatio, 2f);
			this.m_DeferredFrees = new List<List<UIRenderDevice.AllocToFree>>(4);
			this.m_Updates = new List<List<UIRenderDevice.AllocToUpdate>>(4);
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_DeferredFrees.Add(new List<UIRenderDevice.AllocToFree>());
				this.m_Updates.Add(new List<UIRenderDevice.AllocToUpdate>());
				num++;
			}
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000A5970 File Offset: 0x000A3B70
		private void InitVertexDeclaration()
		{
			VertexAttributeDescriptor[] array = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord5, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord6, VertexAttributeFormat.Float32, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord7, VertexAttributeFormat.Float32, 1, 0)
			};
			this.m_VertexDecl = Utility.GetVertexDeclaration(array);
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000A5A38 File Offset: 0x000A3C38
		private void CompleteCreation()
		{
			bool flag = this.m_MockDevice || this.fullyCreated;
			if (!flag)
			{
				this.InitVertexDeclaration();
				this.m_Fences = new uint[4];
				this.m_ConstantProps = new MaterialPropertyBlock();
				this.m_BatchProps = new MaterialPropertyBlock();
				this.m_DefaultStencilState = Utility.CreateStencilState(new StencilState
				{
					enabled = this.isFlat,
					readMask = byte.MaxValue,
					writeMask = byte.MaxValue,
					compareFunctionFront = CompareFunction.Equal,
					passOperationFront = StencilOp.Keep,
					failOperationFront = StencilOp.Keep,
					zFailOperationFront = StencilOp.IncrementSaturate,
					compareFunctionBack = CompareFunction.Less,
					passOperationBack = StencilOp.Keep,
					failOperationBack = StencilOp.Keep,
					zFailOperationBack = StencilOp.DecrementSaturate
				});
				this.m_CommandLists = new List<CommandList>[4];
				int num = 0;
				while ((long)num < 4L)
				{
					this.m_CommandLists[num] = new List<CommandList>();
					num++;
				}
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000A5B38 File Offset: 0x000A3D38
		private bool fullyCreated
		{
			get
			{
				return this.m_Fences != null;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000A5B53 File Offset: 0x000A3D53
		// (set) Token: 0x06002E61 RID: 11873 RVA: 0x000A5B5B File Offset: 0x000A3D5B
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002E62 RID: 11874 RVA: 0x000A5B64 File Offset: 0x000A3D64
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000A5B76 File Offset: 0x000A3D76
		internal void DisposeImmediate()
		{
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			UIRenderDevice.m_SynchronousFree = true;
			this.Dispose();
			UIRenderDevice.m_SynchronousFree = false;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000A5B9C File Offset: 0x000A3D9C
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				UIRenderDevice.m_ActiveDeviceCount--;
				if (disposing)
				{
					UIRenderDevice.DeviceToFree deviceToFree = new UIRenderDevice.DeviceToFree
					{
						handle = (this.m_MockDevice ? 0U : Utility.InsertCPUFence()),
						page = this.m_FirstPage,
						commandLists = this.m_CommandLists
					};
					bool flag = deviceToFree.handle == 0U;
					if (flag)
					{
						deviceToFree.Dispose();
					}
					else
					{
						UIRenderDevice.m_DeviceFreeQueue.AddLast(deviceToFree);
						bool synchronousFree = UIRenderDevice.m_SynchronousFree;
						if (synchronousFree)
						{
							UIRenderDevice.ProcessDeviceFreeQueue();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000A5C4C File Offset: 0x000A3E4C
		public MeshHandle Allocate(uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			MeshHandle meshHandle = this.m_MeshHandles.Get();
			meshHandle.triangleCount = indexCount / 3U;
			this.Allocate(meshHandle, vertexCount, indexCount, out vertexData, out indexData, false);
			indexOffset = (ushort)meshHandle.allocVerts.start;
			return meshHandle;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000A5C94 File Offset: 0x000A3E94
		public void Update(MeshHandle mesh, uint vertexCount, out NativeSlice<Vertex> vertexData)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice<Vertex>((int)mesh.allocVerts.start, (int)vertexCount);
			}
			else
			{
				uint start = mesh.allocVerts.start;
				NativeSlice<ushort> nativeSlice = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)mesh.allocIndices.size);
				NativeSlice<ushort> nativeSlice2;
				ushort num;
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, mesh.allocIndices.size, out vertexData, out nativeSlice2, out num, out allocToUpdate, false);
				int size = (int)mesh.allocIndices.size;
				int num2 = (int)((uint)num - start);
				for (int i = 0; i < size; i++)
				{
					nativeSlice2[i] = (ushort)((int)nativeSlice[i] + num2);
				}
			}
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000A5D90 File Offset: 0x000A3F90
		public void Update(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			Debug.Assert(mesh.allocIndices.size >= indexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice<Vertex>((int)mesh.allocVerts.start, (int)vertexCount);
				indexData = mesh.allocPage.indices.cpuData.Slice<ushort>((int)mesh.allocIndices.start, (int)indexCount);
				indexOffset = (ushort)mesh.allocVerts.start;
				this.UpdateCopyBackIndices(mesh, true);
			}
			else
			{
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, indexCount, out vertexData, out indexData, out indexOffset, out allocToUpdate, true);
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000A5E5C File Offset: 0x000A405C
		private void UpdateCopyBackIndices(MeshHandle mesh, bool copyBackIndices)
		{
			bool flag = mesh.updateAllocID == 0U;
			if (!flag)
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.ActiveUpdatesForMeshHandle(mesh);
				UIRenderDevice.AllocToUpdate allocToUpdate = list[num];
				allocToUpdate.copyBackIndices = true;
				list[num] = allocToUpdate;
			}
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000A5EA4 File Offset: 0x000A40A4
		internal List<UIRenderDevice.AllocToUpdate> ActiveUpdatesForMeshHandle(MeshHandle mesh)
		{
			return this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000A5ED4 File Offset: 0x000A40D4
		private bool TryAllocFromPage(Page page, uint vertexCount, uint indexCount, ref Alloc va, ref Alloc ia, bool shortLived)
		{
			va = page.vertices.allocator.Allocate(vertexCount, shortLived);
			bool flag = va.size > 0U;
			if (flag)
			{
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
				bool flag2 = ia.size > 0U;
				if (flag2)
				{
					return true;
				}
				page.vertices.allocator.Free(va);
				va.size = 0U;
			}
			return false;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000A5F60 File Offset: 0x000A4160
		private void Allocate(MeshHandle meshHandle, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, bool shortLived)
		{
			Page page = null;
			Alloc alloc = default(Alloc);
			Alloc alloc2 = default(Alloc);
			bool flag = vertexCount <= this.m_LargeMeshVertexCount;
			if (flag)
			{
				bool flag2 = this.m_FirstPage != null;
				if (flag2)
				{
					page = this.m_FirstPage;
					for (;;)
					{
						bool flag3 = this.TryAllocFromPage(page, vertexCount, indexCount, ref alloc, ref alloc2, shortLived) || page.next == null;
						if (flag3)
						{
							break;
						}
						page = page.next;
					}
				}
				else
				{
					this.CompleteCreation();
				}
				bool flag4 = alloc2.size == 0U;
				if (flag4)
				{
					this.m_NextPageVertexCount <<= 1;
					this.m_NextPageVertexCount = Math.Max(this.m_NextPageVertexCount, vertexCount * 2U);
					this.m_NextPageVertexCount = Math.Min(this.m_NextPageVertexCount, UIRenderDevice.maxVerticesPerPage);
					uint num = (uint)(this.m_NextPageVertexCount * this.m_IndexToVertexCountRatio + 0.5f);
					num = Math.Max(num, indexCount * 2U);
					Debug.Assert(((page != null) ? page.next : null) == null);
					page = new Page(this.m_NextPageVertexCount, num, 4U, this.m_MockDevice);
					page.next = this.m_FirstPage;
					this.m_FirstPage = page;
					alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
					alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
					Debug.Assert(alloc.size > 0U);
					Debug.Assert(alloc2.size > 0U);
				}
			}
			else
			{
				this.CompleteCreation();
				Page page2 = this.m_FirstPage;
				Page page3 = this.m_FirstPage;
				int num2 = int.MaxValue;
				while (page2 != null)
				{
					int num3 = page2.vertices.cpuData.Length - (int)vertexCount;
					int num4 = page2.indices.cpuData.Length - (int)indexCount;
					bool flag5 = page2.isEmpty && num3 >= 0 && num4 >= 0 && num3 < num2;
					if (flag5)
					{
						page = page2;
						num2 = num3;
					}
					page3 = page2;
					page2 = page2.next;
				}
				bool flag6 = page == null;
				if (flag6)
				{
					uint num5 = ((vertexCount > UIRenderDevice.maxVerticesPerPage) ? 2U : vertexCount);
					Debug.Assert(vertexCount <= UIRenderDevice.maxVerticesPerPage, "Requested Vertex count is above the limit. Alloc will fail.");
					page = new Page(num5, indexCount, 4U, this.m_MockDevice);
					bool flag7 = page3 != null;
					if (flag7)
					{
						page3.next = page;
					}
					else
					{
						this.m_FirstPage = page;
					}
				}
				alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
				alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
			}
			Debug.Assert(alloc.size == vertexCount, "Vertices allocated != Vertices requested");
			Debug.Assert(alloc2.size == indexCount, "Indices allocated != Indices requested");
			bool flag8 = alloc.size != vertexCount || alloc2.size != indexCount;
			if (flag8)
			{
				bool flag9 = alloc.handle != null;
				if (flag9)
				{
					page.vertices.allocator.Free(alloc);
				}
				bool flag10 = alloc2.handle != null;
				if (flag10)
				{
					page.vertices.allocator.Free(alloc2);
				}
				alloc2 = default(Alloc);
				alloc = default(Alloc);
			}
			page.vertices.RegisterUpdate(alloc.start, alloc.size);
			page.indices.RegisterUpdate(alloc2.start, alloc2.size);
			vertexData = new NativeSlice<Vertex>(page.vertices.cpuData, (int)alloc.start, (int)alloc.size);
			indexData = new NativeSlice<ushort>(page.indices.cpuData, (int)alloc2.start, (int)alloc2.size);
			meshHandle.allocPage = page;
			meshHandle.allocVerts = alloc;
			meshHandle.allocIndices = alloc2;
			meshHandle.allocTime = this.m_FrameIndex;
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x000A6328 File Offset: 0x000A4528
		private void UpdateAfterGPUUsedData(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset, out UIRenderDevice.AllocToUpdate allocToUpdate, bool copyBackIndices)
		{
			UIRenderDevice.AllocToUpdate allocToUpdate2 = default(UIRenderDevice.AllocToUpdate);
			uint nextUpdateID = this.m_NextUpdateID;
			this.m_NextUpdateID = nextUpdateID + 1U;
			allocToUpdate2.id = nextUpdateID;
			allocToUpdate2.allocTime = this.m_FrameIndex;
			allocToUpdate2.meshHandle = mesh;
			allocToUpdate2.copyBackIndices = copyBackIndices;
			allocToUpdate = allocToUpdate2;
			Debug.Assert(this.m_NextUpdateID > 0U);
			bool flag = mesh.updateAllocID == 0U;
			if (flag)
			{
				allocToUpdate.permAllocVerts = mesh.allocVerts;
				allocToUpdate.permAllocIndices = mesh.allocIndices;
				allocToUpdate.permPage = mesh.allocPage;
			}
			else
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate3 = list[num];
				Debug.Assert(allocToUpdate3.id == mesh.updateAllocID);
				allocToUpdate.copyBackIndices |= allocToUpdate3.copyBackIndices;
				allocToUpdate.permAllocVerts = allocToUpdate3.permAllocVerts;
				allocToUpdate.permAllocIndices = allocToUpdate3.permAllocIndices;
				allocToUpdate.permPage = allocToUpdate3.permPage;
				allocToUpdate3.allocTime = uint.MaxValue;
				list[num] = allocToUpdate3;
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
			}
			bool flag2 = this.TryAllocFromPage(mesh.allocPage, vertexCount, indexCount, ref mesh.allocVerts, ref mesh.allocIndices, true);
			if (flag2)
			{
				mesh.allocPage.vertices.RegisterUpdate(mesh.allocVerts.start, mesh.allocVerts.size);
				mesh.allocPage.indices.RegisterUpdate(mesh.allocIndices.start, mesh.allocIndices.size);
			}
			else
			{
				this.Allocate(mesh, vertexCount, indexCount, out vertexData, out indexData, true);
			}
			mesh.triangleCount = indexCount / 3U;
			mesh.updateAllocID = allocToUpdate.id;
			mesh.allocTime = allocToUpdate.allocTime;
			this.m_Updates[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Updates.Count))].Add(allocToUpdate);
			vertexData = new NativeSlice<Vertex>(mesh.allocPage.vertices.cpuData, (int)mesh.allocVerts.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)indexCount);
			indexOffset = (ushort)mesh.allocVerts.start;
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x000A6618 File Offset: 0x000A4818
		public void Free(MeshHandle mesh)
		{
			bool flag = mesh.updateAllocID > 0U;
			if (flag)
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate = list[num];
				Debug.Assert(allocToUpdate.id == mesh.updateAllocID);
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocVerts,
					page = allocToUpdate.permPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocIndices,
					page = allocToUpdate.permPage,
					vertices = false
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
				allocToUpdate.allocTime = uint.MaxValue;
				list[num] = allocToUpdate;
			}
			else
			{
				bool flag2 = mesh.allocTime != this.m_FrameIndex;
				if (flag2)
				{
					int num2 = (int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count);
					this.m_DeferredFrees[num2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocVerts,
						page = mesh.allocPage,
						vertices = true
					});
					this.m_DeferredFrees[num2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocIndices,
						page = mesh.allocPage,
						vertices = false
					});
				}
				else
				{
					mesh.allocPage.vertices.allocator.Free(mesh.allocVerts);
					mesh.allocPage.indices.allocator.Free(mesh.allocIndices);
				}
			}
			mesh.allocVerts = default(Alloc);
			mesh.allocIndices = default(Alloc);
			mesh.allocPage = null;
			mesh.updateAllocID = 0U;
			this.m_MeshHandles.Return(mesh);
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x000A68A0 File Offset: 0x000A4AA0
		public void OnFrameRenderingBegin()
		{
			this.AdvanceFrame();
			this.m_DrawStats = default(UIRenderDevice.DrawStatistics);
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				page.vertices.SendUpdates();
				page.indices.SendUpdates();
			}
			this.UpdateFenceValue();
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x000A690C File Offset: 0x000A4B0C
		internal unsafe static NativeSlice<T> PtrToSlice<T>(void* p, int count) where T : struct
		{
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(p, UnsafeUtility.SizeOf<T>(), count);
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x000A692C File Offset: 0x000A4B2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ApplyDrawCommandState(RenderChainCommand cmd, int textureSlot, Material newMat, bool newMatDiffers, ref UIRenderDevice.EvaluationState st)
		{
			if (newMatDiffers)
			{
				st.curState.material = newMat;
				st.mustApplyMaterial = true;
			}
			st.curPage = cmd.mesh.allocPage;
			bool flag = cmd.state.texture != TextureId.invalid;
			if (flag)
			{
				bool flag2 = textureSlot < 0;
				if (flag2)
				{
					textureSlot = this.m_TextureSlotManager.FindOldestSlot();
					this.m_TextureSlotManager.Bind(cmd.state.texture, cmd.state.sdfScale, cmd.state.sharpness, textureSlot, st.batchProps, st.activeCommandList);
					st.mustApplyBatchProps = true;
				}
				else
				{
					this.m_TextureSlotManager.MarkUsed(textureSlot);
				}
			}
			bool flag3 = cmd.state.stencilRef != st.curState.stencilRef;
			if (flag3)
			{
				st.curState.stencilRef = cmd.state.stencilRef;
				st.mustApplyStencil = true;
			}
		}

		// Token: 0x06002E71 RID: 11889 RVA: 0x000A6A30 File Offset: 0x000A4C30
		private void ApplyBatchState(ref UIRenderDevice.EvaluationState st)
		{
			bool flag = !this.m_MockDevice;
			if (flag)
			{
				bool mustApplyMaterial = st.mustApplyMaterial;
				if (mustApplyMaterial)
				{
					bool drawsInCameras = this.drawsInCameras;
					if (drawsInCameras)
					{
						Debug.LogError("Attempted to change material when it is not allowed to do so.");
						return;
					}
					this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1U;
					st.curState.material.SetPass(0);
					bool flag2 = st.constantProps != null;
					if (flag2)
					{
						Utility.SetPropertyBlock(st.constantProps);
					}
					st.mustApplyBatchProps = true;
					st.mustApplyStencil = true;
				}
				bool mustApplyBatchProps = st.mustApplyBatchProps;
				if (mustApplyBatchProps)
				{
					bool flag3 = st.activeCommandList == null;
					if (flag3)
					{
						Utility.SetPropertyBlock(st.batchProps);
					}
					else
					{
						st.activeCommandList.ApplyBatchProps();
					}
				}
				bool mustApplyStencil = st.mustApplyStencil;
				if (mustApplyStencil)
				{
					this.m_DrawStats.stencilRefChanges = this.m_DrawStats.stencilRefChanges + 1U;
					bool flag4 = st.activeCommandList == null;
					if (flag4)
					{
						Utility.SetStencilState(this.m_DefaultStencilState, st.curState.stencilRef);
					}
				}
			}
			st.mustApplyMaterial = false;
			st.mustApplyBatchProps = false;
			st.mustApplyStencil = false;
			this.m_TextureSlotManager.StartNewBatch();
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x000A6B5C File Offset: 0x000A4D5C
		public unsafe void EvaluateChain(RenderChainCommand head, Material initialMat, Material defaultMat, Texture gradientSettings, Texture shaderInfo, float pixelsPerPoint, ref Exception immediateException)
		{
			Utility.ProfileDrawChainBegin();
			bool breakBatches = this.breakBatches;
			int num = 1024;
			DrawBufferRange* ptr;
			checked
			{
				ptr = stackalloc DrawBufferRange[unchecked((UIntPtr)num) * (UIntPtr)sizeof(DrawBufferRange)];
			}
			int num2 = num - 1;
			int num3 = 0;
			int num4 = 0;
			DrawBufferRange drawBufferRange = default(DrawBufferRange);
			int num5 = -1;
			int num6 = 0;
			this.currentFrameCommandListCount = 0;
			UIRenderDevice.EvaluationState evaluationState = new UIRenderDevice.EvaluationState
			{
				constantProps = this.m_ConstantProps,
				batchProps = this.m_BatchProps,
				defaultMat = defaultMat,
				curState = new State
				{
					material = initialMat
				},
				mustApplyBatchProps = true,
				mustApplyStencil = true
			};
			DrawParams drawParams = this.m_DrawParams;
			drawParams.Reset();
			bool flag = !this.drawsInCameras;
			if (flag)
			{
				drawParams.renderTexture.Add(RenderTexture.active);
			}
			this.m_TextureSlotManager.Reset();
			bool fullyCreated = this.fullyCreated;
			if (fullyCreated)
			{
				evaluationState.batchProps.Clear();
				bool flag2 = gradientSettings != null;
				if (flag2)
				{
					evaluationState.constantProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
				}
				bool flag3 = shaderInfo != null;
				if (flag3)
				{
					evaluationState.constantProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
				}
				bool flag4 = !this.drawsInCameras;
				if (flag4)
				{
					Utility.SetPropertyBlock(evaluationState.constantProps);
				}
			}
			while (head != null)
			{
				bool flag5 = head.type == CommandType.BeginDisable;
				if (flag5)
				{
					this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
					num6++;
					head = head.next;
				}
				else
				{
					bool flag6 = head.type == CommandType.EndDisable;
					if (flag6)
					{
						this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
						num6--;
						head = head.next;
					}
					else
					{
						bool flag7 = num6 > 0;
						if (flag7)
						{
							this.m_DrawStats.skippedCommandCount = this.m_DrawStats.skippedCommandCount + 1U;
							head = head.next;
						}
						else
						{
							this.m_DrawStats.drawCommandCount = this.m_DrawStats.drawCommandCount + ((head.type == CommandType.Draw) ? 1U : 0U);
							bool flag8 = drawBufferRange.indexCount > 0 && num4 == num - 1;
							bool flag9 = false;
							bool flag10 = false;
							bool flag11 = false;
							int num7 = -1;
							Material material = null;
							bool flag12 = false;
							bool flag13 = head.type == CommandType.Draw;
							if (flag13)
							{
								material = ((head.state.material != null) ? head.state.material : defaultMat);
								bool flag14 = material != evaluationState.curState.material;
								if (flag14)
								{
									flag11 = true;
									flag12 = true;
									flag9 = true;
									flag10 = true;
								}
								bool flag15 = head.mesh.allocPage != evaluationState.curPage;
								if (flag15)
								{
									flag11 = true;
									flag9 = true;
									flag10 = true;
								}
								else
								{
									bool flag16 = (long)num5 != (long)((ulong)head.mesh.allocIndices.start + (ulong)((long)head.indexOffset));
									if (flag16)
									{
										flag9 = true;
									}
								}
								bool flag17 = head.state.texture != TextureId.invalid;
								if (flag17)
								{
									flag11 = true;
									num7 = this.m_TextureSlotManager.IndexOf(head.state.texture);
									bool flag18 = num7 < 0 && this.m_TextureSlotManager.FreeSlots < 1;
									if (flag18)
									{
										flag9 = true;
										flag10 = true;
									}
								}
								bool flag19 = head.state.stencilRef != evaluationState.curState.stencilRef;
								if (flag19)
								{
									flag11 = true;
									flag9 = true;
									flag10 = true;
								}
								bool flag20 = flag9 && flag8;
								if (flag20)
								{
									flag10 = true;
								}
							}
							else
							{
								flag9 = true;
								flag10 = true;
							}
							bool flag21 = breakBatches;
							if (flag21)
							{
								flag9 = true;
								flag10 = true;
							}
							bool flag22 = flag9;
							if (flag22)
							{
								bool flag23 = drawBufferRange.indexCount > 0;
								if (flag23)
								{
									int num8 = (num3 + num4++) & num2;
									ptr[num8] = drawBufferRange;
									Debug.Assert(num4 < num || flag10);
									drawBufferRange = default(DrawBufferRange);
									this.m_DrawStats.drawRangeCount = this.m_DrawStats.drawRangeCount + 1U;
								}
								bool flag24 = head.type == CommandType.Draw;
								if (flag24)
								{
									drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset);
									drawBufferRange.indexCount = head.indexCount;
									drawBufferRange.vertsReferenced = (int)head.mesh.allocVerts.size;
									drawBufferRange.minIndexVal = (int)head.mesh.allocVerts.start;
									num5 = drawBufferRange.firstIndex + head.indexCount;
									this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
								}
								bool flag25 = flag10;
								if (flag25)
								{
									bool flag26 = num4 > 0;
									if (flag26)
									{
										this.ApplyBatchState(ref evaluationState);
										this.KickRanges(ptr, ref num4, ref num3, num, evaluationState.curPage, evaluationState.activeCommandList);
									}
									bool flag27 = this.fullyCreated && head.type == CommandType.CutRenderChain;
									if (flag27)
									{
										bool flag28 = this.currentFrameCommandListCount < this.currentFrameCommandLists.Count;
										CommandList commandList;
										if (flag28)
										{
											commandList = this.currentFrameCommandLists[this.currentFrameCommandListCount];
											commandList.Reset(head.owner);
										}
										else
										{
											commandList = new CommandList(head.owner, this.m_VertexDecl, this.m_DefaultStencilState);
											this.currentFrameCommandLists.Add(commandList);
										}
										this.currentFrameCommandListCount++;
										evaluationState.activeCommandList = commandList;
										evaluationState.constantProps = evaluationState.activeCommandList.constantProps;
										evaluationState.batchProps = evaluationState.activeCommandList.batchProps;
										evaluationState.batchProps.Clear();
										bool flag29 = gradientSettings != null;
										if (flag29)
										{
											evaluationState.constantProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
										}
										bool flag30 = shaderInfo != null;
										if (flag30)
										{
											evaluationState.constantProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
										}
										this.m_TextureSlotManager.Reset();
									}
									bool flag31 = head.type > CommandType.Draw;
									if (flag31)
									{
										bool flag32 = !this.m_MockDevice;
										if (flag32)
										{
											head.ExecuteNonDrawMesh(drawParams, pixelsPerPoint, ref immediateException);
										}
										bool flag33 = head.type == CommandType.Immediate || head.type == CommandType.ImmediateCull || head.type == CommandType.BlitToPreviousRT || head.type == CommandType.PushRenderTexture || head.type == CommandType.PopDefaultMaterial || head.type == CommandType.PushDefaultMaterial;
										if (flag33)
										{
											evaluationState.curState.material = null;
											evaluationState.mustApplyMaterial = false;
											this.m_DrawStats.immediateDraws = this.m_DrawStats.immediateDraws + 1U;
											bool flag34 = head.type == CommandType.PopDefaultMaterial;
											if (flag34)
											{
												int num9 = drawParams.defaultMaterial.Count - 1;
												defaultMat = drawParams.defaultMaterial[num9];
												drawParams.defaultMaterial.RemoveAt(num9);
											}
											bool flag35 = head.type == CommandType.PushDefaultMaterial;
											if (flag35)
											{
												drawParams.defaultMaterial.Add(defaultMat);
												defaultMat = head.state.material;
											}
										}
									}
								}
								bool flag36 = head.type == CommandType.Draw && flag11;
								if (flag36)
								{
									this.ApplyDrawCommandState(head, num7, material, flag12, ref evaluationState);
								}
								head = head.next;
							}
							else
							{
								bool flag37 = drawBufferRange.indexCount == 0;
								if (flag37)
								{
									num5 = (drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset));
								}
								drawBufferRange.indexCount += head.indexCount;
								int minIndexVal = drawBufferRange.minIndexVal;
								int start = (int)head.mesh.allocVerts.start;
								int num10 = drawBufferRange.minIndexVal + drawBufferRange.vertsReferenced;
								int num11 = (int)(head.mesh.allocVerts.start + head.mesh.allocVerts.size);
								drawBufferRange.minIndexVal = Mathf.Min(minIndexVal, start);
								drawBufferRange.vertsReferenced = Mathf.Max(num10, num11) - drawBufferRange.minIndexVal;
								num5 += head.indexCount;
								this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
								bool flag38 = flag11;
								if (flag38)
								{
									this.ApplyDrawCommandState(head, num7, material, flag12, ref evaluationState);
								}
								head = head.next;
							}
						}
					}
				}
			}
			bool flag39 = drawBufferRange.indexCount > 0;
			if (flag39)
			{
				int num12 = (num3 + num4++) & num2;
				ptr[num12] = drawBufferRange;
			}
			bool flag40 = num4 > 0;
			if (flag40)
			{
				this.ApplyBatchState(ref evaluationState);
				this.KickRanges(ptr, ref num4, ref num3, num, evaluationState.curPage, evaluationState.activeCommandList);
			}
			Debug.Assert(num6 == 0, "Rendering disabled counter is not 0, indicating a mismatch of commands");
			this.UpdateFenceValue();
			Utility.ProfileDrawChainEnd();
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x000A741C File Offset: 0x000A561C
		private unsafe void UpdateFenceValue()
		{
			bool flag = this.m_Fences != null;
			if (flag)
			{
				uint num = Utility.InsertCPUFence();
				fixed (uint* ptr = &this.m_Fences[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length))])
				{
					uint* ptr2 = ptr;
					bool flag3;
					do
					{
						uint num2 = *ptr2;
						bool flag2 = num - num2 <= 0U;
						if (flag2)
						{
							break;
						}
						int num3 = Interlocked.CompareExchange(ref *(int*)ptr2, (int)num, (int)num2);
						flag3 = (long)num3 == (long)((ulong)num2);
					}
					while (!flag3);
				}
			}
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x000A749C File Offset: 0x000A569C
		private unsafe void KickRanges(DrawBufferRange* ranges, ref int rangesReady, ref int rangesStart, int rangesCount, Page curPage, CommandList commandList)
		{
			Debug.Assert(rangesReady > 0);
			bool flag = rangesStart + rangesReady <= rangesCount;
			if (flag)
			{
				bool flag2 = !this.m_MockDevice;
				if (flag2)
				{
					this.DrawRanges(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), rangesReady), commandList);
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 1U;
			}
			else
			{
				int num = rangesCount - rangesStart;
				int num2 = rangesReady - num;
				bool flag3 = !this.m_MockDevice;
				if (flag3)
				{
					this.DrawRanges(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), num), commandList);
					this.DrawRanges(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)ranges, num2), commandList);
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 2U;
			}
			rangesStart = (rangesStart + rangesReady) & (rangesCount - 1);
			rangesReady = 0;
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x000A75B8 File Offset: 0x000A57B8
		private unsafe void DrawRanges(Utility.GPUBuffer<ushort> ib, Utility.GPUBuffer<Vertex> vb, NativeSlice<DrawBufferRange> ranges, CommandList commandList)
		{
			bool flag = commandList != null;
			checked
			{
				if (flag)
				{
					commandList.DrawRanges(ib, vb, ranges);
				}
				else
				{
					bool flag2 = !this.drawsInCameras;
					if (flag2)
					{
						IntPtr* ptr = stackalloc IntPtr[unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)];
						*ptr = vb.BufferPointer;
						Utility.DrawRanges(ib.BufferPointer, ptr, 1, new IntPtr(ranges.GetUnsafePtr<DrawBufferRange>()), ranges.Length, this.m_VertexDecl);
					}
				}
			}
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x000A7628 File Offset: 0x000A5828
		internal void WaitOnAllCpuFences()
		{
			for (int i = 0; i < this.m_Fences.Length; i++)
			{
				this.WaitOnCpuFence(this.m_Fences[i]);
			}
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000A765C File Offset: 0x000A585C
		private void WaitOnCpuFence(uint fence)
		{
			bool flag = fence != 0U && !Utility.CPUFencePassed(fence);
			if (flag)
			{
				Utility.WaitForCPUFencePassed(fence);
			}
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000A7688 File Offset: 0x000A5888
		public void AdvanceFrame()
		{
			this.m_FrameIndex += 1U;
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			bool flag = this.m_Fences != null;
			if (flag)
			{
				int num = (int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length));
				uint num2 = this.m_Fences[num];
				this.WaitOnCpuFence(num2);
				this.m_Fences[num] = 0U;
			}
			this.m_NextUpdateID = 1U;
			List<UIRenderDevice.AllocToFree> list = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToFree allocToFree in list)
			{
				bool vertices = allocToFree.vertices;
				if (vertices)
				{
					allocToFree.page.vertices.allocator.Free(allocToFree.alloc);
				}
				else
				{
					allocToFree.page.indices.allocator.Free(allocToFree.alloc);
				}
			}
			list.Clear();
			List<UIRenderDevice.AllocToUpdate> list2 = this.m_Updates[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToUpdate allocToUpdate in list2)
			{
				bool flag2 = allocToUpdate.meshHandle.updateAllocID == allocToUpdate.id && allocToUpdate.meshHandle.allocTime == allocToUpdate.allocTime;
				if (flag2)
				{
					NativeSlice<Vertex> nativeSlice = new NativeSlice<Vertex>(allocToUpdate.meshHandle.allocPage.vertices.cpuData, (int)allocToUpdate.meshHandle.allocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					NativeSlice<Vertex> nativeSlice2 = new NativeSlice<Vertex>(allocToUpdate.permPage.vertices.cpuData, (int)allocToUpdate.permAllocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					nativeSlice2.CopyFrom(nativeSlice);
					allocToUpdate.permPage.vertices.RegisterUpdate(allocToUpdate.permAllocVerts.start, allocToUpdate.meshHandle.allocVerts.size);
					bool copyBackIndices = allocToUpdate.copyBackIndices;
					if (copyBackIndices)
					{
						NativeSlice<ushort> nativeSlice3 = new NativeSlice<ushort>(allocToUpdate.meshHandle.allocPage.indices.cpuData, (int)allocToUpdate.meshHandle.allocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						NativeSlice<ushort> nativeSlice4 = new NativeSlice<ushort>(allocToUpdate.permPage.indices.cpuData, (int)allocToUpdate.permAllocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						int length = nativeSlice4.Length;
						int num3 = (int)(allocToUpdate.permAllocVerts.start - allocToUpdate.meshHandle.allocVerts.start);
						for (int i = 0; i < length; i++)
						{
							nativeSlice4[i] = (ushort)((int)nativeSlice3[i] + num3);
						}
						allocToUpdate.permPage.indices.RegisterUpdate(allocToUpdate.permAllocIndices.start, allocToUpdate.meshHandle.allocIndices.size);
					}
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocVerts,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = true
					});
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocIndices,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = false
					});
					allocToUpdate.meshHandle.allocVerts = allocToUpdate.permAllocVerts;
					allocToUpdate.meshHandle.allocIndices = allocToUpdate.permAllocIndices;
					allocToUpdate.meshHandle.allocPage = allocToUpdate.permPage;
					allocToUpdate.meshHandle.updateAllocID = 0U;
				}
			}
			list2.Clear();
			this.PruneUnusedPages();
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x000A7AE8 File Offset: 0x000A5CE8
		private void PruneUnusedPages()
		{
			Page page4;
			Page page3;
			Page page2;
			Page page = (page2 = (page3 = (page4 = null)));
			Page next;
			for (Page page5 = this.m_FirstPage; page5 != null; page5 = next)
			{
				bool flag = !page5.isEmpty;
				if (flag)
				{
					page5.framesEmpty = 0;
				}
				else
				{
					page5.framesEmpty++;
				}
				bool flag2 = page5.framesEmpty < 60;
				if (flag2)
				{
					bool flag3 = page2 != null;
					if (flag3)
					{
						page.next = page5;
					}
					else
					{
						page2 = page5;
					}
					page = page5;
				}
				else
				{
					bool flag4 = page3 != null;
					if (flag4)
					{
						page4.next = page5;
					}
					else
					{
						page3 = page5;
					}
					page4 = page5;
				}
				next = page5.next;
				page5.next = null;
			}
			this.m_FirstPage = page2;
			Page next2;
			for (Page page5 = page3; page5 != null; page5 = next2)
			{
				next2 = page5.next;
				page5.next = null;
				page5.Dispose();
			}
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000A7BC8 File Offset: 0x000A5DC8
		internal static void PrepareForGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount++;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000A7BD7 File Offset: 0x000A5DD7
		internal static void WrapUpGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount--;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000A7BE6 File Offset: 0x000A5DE6
		internal static void FlushAllPendingDeviceDisposes()
		{
			Utility.SyncRenderThread();
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000A7BF8 File Offset: 0x000A5DF8
		internal UIRenderDevice.AllocationStatistics GatherAllocationStatistics()
		{
			UIRenderDevice.AllocationStatistics allocationStatistics = default(UIRenderDevice.AllocationStatistics);
			allocationStatistics.completeInit = this.fullyCreated;
			allocationStatistics.freesDeferred = new int[this.m_DeferredFrees.Count];
			for (int i = 0; i < this.m_DeferredFrees.Count; i++)
			{
				allocationStatistics.freesDeferred[i] = this.m_DeferredFrees[i].Count;
			}
			int num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				num++;
			}
			allocationStatistics.pages = new UIRenderDevice.AllocationStatistics.PageStatistics[num];
			num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				allocationStatistics.pages[num].vertices = page.vertices.allocator.GatherStatistics();
				allocationStatistics.pages[num].indices = page.indices.allocator.GatherStatistics();
				num++;
			}
			return allocationStatistics;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000A7D04 File Offset: 0x000A5F04
		internal UIRenderDevice.DrawStatistics GatherDrawStatistics()
		{
			return this.m_DrawStats;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x000A7D1C File Offset: 0x000A5F1C
		public static void ProcessDeviceFreeQueue()
		{
			bool synchronousFree = UIRenderDevice.m_SynchronousFree;
			if (synchronousFree)
			{
				Utility.SyncRenderThread();
			}
			for (LinkedListNode<UIRenderDevice.DeviceToFree> linkedListNode = UIRenderDevice.m_DeviceFreeQueue.First; linkedListNode != null; linkedListNode = UIRenderDevice.m_DeviceFreeQueue.First)
			{
				bool flag = !Utility.CPUFencePassed(linkedListNode.Value.handle);
				if (flag)
				{
					break;
				}
				linkedListNode.Value.Dispose();
				UIRenderDevice.m_DeviceFreeQueue.RemoveFirst();
			}
			Debug.Assert(!UIRenderDevice.m_SynchronousFree || UIRenderDevice.m_DeviceFreeQueue.Count == 0);
			bool flag2 = UIRenderDevice.m_ActiveDeviceCount == 0 && UIRenderDevice.m_SubscribedToNotifications;
			if (flag2)
			{
				Utility.NotifyOfUIREvents(false);
				UIRenderDevice.m_SubscribedToNotifications = false;
			}
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x000A7DD1 File Offset: 0x000A5FD1
		private static void OnEngineUpdateGlobal()
		{
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x000A7DDA File Offset: 0x000A5FDA
		private static void OnFlushPendingResources()
		{
			UIRenderDevice.m_SynchronousFree = true;
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x0400150D RID: 5389
		internal const uint k_MaxQueuedFrameCount = 4U;

		// Token: 0x0400150E RID: 5390
		internal const int k_PruneEmptyPageFrameCount = 60;

		// Token: 0x0400150F RID: 5391
		private readonly bool m_MockDevice;

		// Token: 0x04001510 RID: 5392
		private IntPtr m_DefaultStencilState;

		// Token: 0x04001511 RID: 5393
		private IntPtr m_VertexDecl;

		// Token: 0x04001512 RID: 5394
		private Page m_FirstPage;

		// Token: 0x04001513 RID: 5395
		private uint m_NextPageVertexCount;

		// Token: 0x04001514 RID: 5396
		private uint m_LargeMeshVertexCount;

		// Token: 0x04001515 RID: 5397
		private float m_IndexToVertexCountRatio;

		// Token: 0x04001516 RID: 5398
		private List<List<UIRenderDevice.AllocToFree>> m_DeferredFrees;

		// Token: 0x04001517 RID: 5399
		private List<List<UIRenderDevice.AllocToUpdate>> m_Updates;

		// Token: 0x04001518 RID: 5400
		private List<CommandList>[] m_CommandLists;

		// Token: 0x04001519 RID: 5401
		private uint[] m_Fences;

		// Token: 0x0400151A RID: 5402
		private MaterialPropertyBlock m_ConstantProps;

		// Token: 0x0400151B RID: 5403
		private MaterialPropertyBlock m_BatchProps;

		// Token: 0x0400151C RID: 5404
		private uint m_FrameIndex;

		// Token: 0x0400151D RID: 5405
		private uint m_NextUpdateID = 1U;

		// Token: 0x0400151E RID: 5406
		private UIRenderDevice.DrawStatistics m_DrawStats;

		// Token: 0x0400151F RID: 5407
		private readonly LinkedPool<MeshHandle> m_MeshHandles = new LinkedPool<MeshHandle>(() => new MeshHandle(), delegate(MeshHandle mh)
		{
		}, 10000);

		// Token: 0x04001520 RID: 5408
		private readonly DrawParams m_DrawParams = new DrawParams();

		// Token: 0x04001521 RID: 5409
		private readonly TextureSlotManager m_TextureSlotManager = new TextureSlotManager();

		// Token: 0x04001522 RID: 5410
		private static LinkedList<UIRenderDevice.DeviceToFree> m_DeviceFreeQueue = new LinkedList<UIRenderDevice.DeviceToFree>();

		// Token: 0x04001523 RID: 5411
		private static int m_ActiveDeviceCount = 0;

		// Token: 0x04001524 RID: 5412
		private static bool m_SubscribedToNotifications;

		// Token: 0x04001525 RID: 5413
		private static bool m_SynchronousFree;

		// Token: 0x04001526 RID: 5414
		private static readonly int s_GradientSettingsTexID = Shader.PropertyToID("_GradientSettingsTex");

		// Token: 0x04001527 RID: 5415
		private static readonly int s_ShaderInfoTexID = Shader.PropertyToID("_ShaderInfoTex");

		// Token: 0x04001528 RID: 5416
		private static ProfilerMarker s_MarkerAllocate = new ProfilerMarker("UIR.Allocate");

		// Token: 0x04001529 RID: 5417
		private static ProfilerMarker s_MarkerFree = new ProfilerMarker("UIR.Free");

		// Token: 0x0400152A RID: 5418
		private static ProfilerMarker s_MarkerAdvanceFrame = new ProfilerMarker("UIR.AdvanceFrame");

		// Token: 0x0400152B RID: 5419
		private static ProfilerMarker s_MarkerFence = new ProfilerMarker("UIR.WaitOnFence");

		// Token: 0x0400152C RID: 5420
		private static ProfilerMarker s_MarkerBeforeDraw = new ProfilerMarker("UIR.BeforeDraw");

		// Token: 0x04001530 RID: 5424
		internal int currentFrameCommandListCount = 0;

		// Token: 0x02000621 RID: 1569
		internal struct AllocToUpdate
		{
			// Token: 0x04001532 RID: 5426
			public uint id;

			// Token: 0x04001533 RID: 5427
			public uint allocTime;

			// Token: 0x04001534 RID: 5428
			public MeshHandle meshHandle;

			// Token: 0x04001535 RID: 5429
			public Alloc permAllocVerts;

			// Token: 0x04001536 RID: 5430
			public Alloc permAllocIndices;

			// Token: 0x04001537 RID: 5431
			public Page permPage;

			// Token: 0x04001538 RID: 5432
			public bool copyBackIndices;
		}

		// Token: 0x02000622 RID: 1570
		private struct AllocToFree
		{
			// Token: 0x04001539 RID: 5433
			public Alloc alloc;

			// Token: 0x0400153A RID: 5434
			public Page page;

			// Token: 0x0400153B RID: 5435
			public bool vertices;
		}

		// Token: 0x02000623 RID: 1571
		private struct DeviceToFree
		{
			// Token: 0x06002E82 RID: 11906 RVA: 0x000A7DEC File Offset: 0x000A5FEC
			public void Dispose()
			{
				while (this.page != null)
				{
					Page page = this.page;
					this.page = this.page.next;
					page.Dispose();
				}
				bool flag = this.commandLists != null;
				if (flag)
				{
					for (int i = 0; i < this.commandLists.Length; i++)
					{
						foreach (CommandList commandList in this.commandLists[i])
						{
							commandList.Dispose();
						}
						this.commandLists[i] = null;
					}
				}
			}

			// Token: 0x0400153C RID: 5436
			public uint handle;

			// Token: 0x0400153D RID: 5437
			public Page page;

			// Token: 0x0400153E RID: 5438
			public List<CommandList>[] commandLists;
		}

		// Token: 0x02000624 RID: 1572
		private struct EvaluationState
		{
			// Token: 0x0400153F RID: 5439
			public CommandList activeCommandList;

			// Token: 0x04001540 RID: 5440
			public MaterialPropertyBlock constantProps;

			// Token: 0x04001541 RID: 5441
			public MaterialPropertyBlock batchProps;

			// Token: 0x04001542 RID: 5442
			public Material defaultMat;

			// Token: 0x04001543 RID: 5443
			public State curState;

			// Token: 0x04001544 RID: 5444
			public Page curPage;

			// Token: 0x04001545 RID: 5445
			public bool mustApplyMaterial;

			// Token: 0x04001546 RID: 5446
			public bool mustApplyBatchProps;

			// Token: 0x04001547 RID: 5447
			public bool mustApplyStencil;
		}

		// Token: 0x02000625 RID: 1573
		internal struct AllocationStatistics
		{
			// Token: 0x04001548 RID: 5448
			public UIRenderDevice.AllocationStatistics.PageStatistics[] pages;

			// Token: 0x04001549 RID: 5449
			public int[] freesDeferred;

			// Token: 0x0400154A RID: 5450
			public bool completeInit;

			// Token: 0x02000626 RID: 1574
			public struct PageStatistics
			{
				// Token: 0x0400154B RID: 5451
				internal HeapStatistics vertices;

				// Token: 0x0400154C RID: 5452
				internal HeapStatistics indices;
			}
		}

		// Token: 0x02000627 RID: 1575
		internal struct DrawStatistics
		{
			// Token: 0x0400154D RID: 5453
			public int currentFrameIndex;

			// Token: 0x0400154E RID: 5454
			public uint totalIndices;

			// Token: 0x0400154F RID: 5455
			public uint commandCount;

			// Token: 0x04001550 RID: 5456
			public uint skippedCommandCount;

			// Token: 0x04001551 RID: 5457
			public uint drawCommandCount;

			// Token: 0x04001552 RID: 5458
			public uint disableCommandCount;

			// Token: 0x04001553 RID: 5459
			public uint materialSetCount;

			// Token: 0x04001554 RID: 5460
			public uint drawRangeCount;

			// Token: 0x04001555 RID: 5461
			public uint drawRangeCallCount;

			// Token: 0x04001556 RID: 5462
			public uint immediateDraws;

			// Token: 0x04001557 RID: 5463
			public uint stencilRefChanges;
		}
	}
}
