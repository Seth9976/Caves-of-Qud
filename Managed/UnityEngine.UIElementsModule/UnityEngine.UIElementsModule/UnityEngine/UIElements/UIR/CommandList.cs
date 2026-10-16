using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000619 RID: 1561
	internal class CommandList : IDisposable
	{
		// Token: 0x06002E25 RID: 11813 RVA: 0x000A351C File Offset: 0x000A171C
		public CommandList(VisualElement owner, IntPtr vertexDecl, IntPtr stencilState)
		{
			this.m_Owner = owner;
			this.m_VertexDecl = vertexDecl;
			this.m_StencilState = stencilState;
			this.m_DrawRanges = new NativeList<DrawBufferRange>(1024);
			this.handle = GCHandle.Alloc(this);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x000A359C File Offset: 0x000A179C
		public void Reset(VisualElement newOwner)
		{
			this.m_Owner = newOwner;
			this.m_Commands.Clear();
			this.m_DrawRanges.Clear();
			for (int i = 0; i < this.m_GpuTextureData.Length; i++)
			{
				this.m_GpuTextureData[i] = Vector4.zero;
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x000A35F4 File Offset: 0x000A17F4
		public unsafe void Execute()
		{
			IntPtr* ptr;
			checked
			{
				ptr = stackalloc IntPtr[unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)];
				Utility.SetPropertyBlock(this.constantProps);
				Utility.SetStencilState(this.m_StencilState, 0);
			}
			for (int i = 0; i < this.m_Commands.Count; i++)
			{
				SerializedCommand serializedCommand = this.m_Commands[i];
				switch (serializedCommand.type)
				{
				case SerializedCommandType.DrawRanges:
					*ptr = serializedCommand.vertexBuffer;
					Utility.DrawRanges(serializedCommand.indexBuffer, ptr, 1, new IntPtr(this.m_DrawRanges.GetSlice(serializedCommand.firstRange, serializedCommand.rangeCount).GetUnsafePtr<DrawBufferRange>()), serializedCommand.rangeCount, this.m_VertexDecl);
					break;
				case SerializedCommandType.SetTexture:
					this.batchProps.SetTexture(serializedCommand.textureName, serializedCommand.texture);
					this.m_GpuTextureData[serializedCommand.gpuDataOffset] = serializedCommand.gpuData0;
					this.m_GpuTextureData[serializedCommand.gpuDataOffset + 1] = serializedCommand.gpuData1;
					this.batchProps.SetVectorArray(TextureSlotManager.textureTableId, this.m_GpuTextureData);
					break;
				case SerializedCommandType.ApplyBatchProps:
					Utility.SetPropertyBlock(this.batchProps);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000A3734 File Offset: 0x000A1934
		public void SetTexture(int name, Texture texture, int gpuDataOffset, Vector4 gpuData0, Vector4 gpuData1)
		{
			SerializedCommand serializedCommand = new SerializedCommand
			{
				type = SerializedCommandType.SetTexture,
				textureName = name,
				texture = texture,
				gpuDataOffset = gpuDataOffset,
				gpuData0 = gpuData0,
				gpuData1 = gpuData1
			};
			this.m_Commands.Add(serializedCommand);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000A378C File Offset: 0x000A198C
		public void ApplyBatchProps()
		{
			SerializedCommand serializedCommand = new SerializedCommand
			{
				type = SerializedCommandType.ApplyBatchProps
			};
			this.m_Commands.Add(serializedCommand);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000A37BC File Offset: 0x000A19BC
		public void DrawRanges(Utility.GPUBuffer<ushort> ib, Utility.GPUBuffer<Vertex> vb, NativeSlice<DrawBufferRange> ranges)
		{
			SerializedCommand serializedCommand = new SerializedCommand
			{
				type = SerializedCommandType.DrawRanges,
				vertexBuffer = vb.BufferPointer,
				indexBuffer = ib.BufferPointer,
				firstRange = this.m_DrawRanges.Count,
				rangeCount = ranges.Length
			};
			this.m_Commands.Add(serializedCommand);
			this.m_DrawRanges.Add(ranges);
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000A3830 File Offset: 0x000A1A30
		// (set) Token: 0x06002E2C RID: 11820 RVA: 0x000A3838 File Offset: 0x000A1A38
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002E2D RID: 11821 RVA: 0x000A3841 File Offset: 0x000A1A41
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000A3854 File Offset: 0x000A1A54
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_DrawRanges.Dispose();
					this.m_DrawRanges = null;
					bool isAllocated = this.handle.IsAllocated;
					if (isAllocated)
					{
						this.handle.Free();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x040014F4 RID: 5364
		public VisualElement m_Owner;

		// Token: 0x040014F5 RID: 5365
		private readonly IntPtr m_VertexDecl;

		// Token: 0x040014F6 RID: 5366
		private readonly IntPtr m_StencilState;

		// Token: 0x040014F7 RID: 5367
		public MaterialPropertyBlock constantProps = new MaterialPropertyBlock();

		// Token: 0x040014F8 RID: 5368
		public MaterialPropertyBlock batchProps = new MaterialPropertyBlock();

		// Token: 0x040014F9 RID: 5369
		public GCHandle handle;

		// Token: 0x040014FA RID: 5370
		private List<SerializedCommand> m_Commands = new List<SerializedCommand>();

		// Token: 0x040014FB RID: 5371
		private Vector4[] m_GpuTextureData = new Vector4[TextureSlotManager.k_SlotSize * TextureSlotManager.k_SlotCount];

		// Token: 0x040014FC RID: 5372
		private NativeList<DrawBufferRange> m_DrawRanges;
	}
}
