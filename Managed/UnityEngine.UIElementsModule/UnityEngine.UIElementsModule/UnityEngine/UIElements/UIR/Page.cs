using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200062F RID: 1583
	internal class Page : IDisposable
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x000A8842 File Offset: 0x000A6A42
		public Page(uint vertexMaxCount, uint indexMaxCount, uint maxQueuedFrameCount, bool mockPage)
		{
			vertexMaxCount = Math.Min(vertexMaxCount, 65536U);
			this.vertices = new Page.DataSet<Vertex>(Utility.GPUBufferType.Vertex, vertexMaxCount, maxQueuedFrameCount, 32U, mockPage);
			this.indices = new Page.DataSet<ushort>(Utility.GPUBufferType.Index, indexMaxCount, maxQueuedFrameCount, 32U, mockPage);
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x000A887D File Offset: 0x000A6A7D
		// (set) Token: 0x06002E9D RID: 11933 RVA: 0x000A8885 File Offset: 0x000A6A85
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002E9E RID: 11934 RVA: 0x000A888E File Offset: 0x000A6A8E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000A88A0 File Offset: 0x000A6AA0
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.indices.Dispose();
					this.vertices.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002EA0 RID: 11936 RVA: 0x000A88E4 File Offset: 0x000A6AE4
		public bool isEmpty
		{
			get
			{
				return this.vertices.allocator.isEmpty && this.indices.allocator.isEmpty;
			}
		}

		// Token: 0x04001578 RID: 5496
		public Page.DataSet<Vertex> vertices;

		// Token: 0x04001579 RID: 5497
		public Page.DataSet<ushort> indices;

		// Token: 0x0400157A RID: 5498
		public Page next;

		// Token: 0x0400157B RID: 5499
		public int framesEmpty;

		// Token: 0x02000630 RID: 1584
		public class DataSet<T> : IDisposable where T : struct
		{
			// Token: 0x06002EA1 RID: 11937 RVA: 0x000A891C File Offset: 0x000A6B1C
			public DataSet(Utility.GPUBufferType bufferType, uint totalCount, uint maxQueuedFrameCount, uint updateRangePoolSize, bool mockBuffer)
			{
				bool flag = !mockBuffer;
				if (flag)
				{
					this.gpuData = new Utility.GPUBuffer<T>((int)totalCount, bufferType);
				}
				this.cpuData = new NativeArray<T>((int)totalCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.allocator = new GPUBufferAllocator(totalCount);
				bool flag2 = !mockBuffer;
				if (flag2)
				{
					this.m_ElemStride = (uint)this.gpuData.ElementStride;
				}
				this.m_UpdateRangePoolSize = updateRangePoolSize;
				uint num = this.m_UpdateRangePoolSize * maxQueuedFrameCount;
				this.updateRanges = new NativeArray<GfxUpdateBufferRange>((int)num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart = 0U;
			}

			// Token: 0x17000B51 RID: 2897
			// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000A89B6 File Offset: 0x000A6BB6
			// (set) Token: 0x06002EA3 RID: 11939 RVA: 0x000A89BE File Offset: 0x000A6BBE
			private protected bool disposed { protected get; private set; }

			// Token: 0x06002EA4 RID: 11940 RVA: 0x000A89C7 File Offset: 0x000A6BC7
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06002EA5 RID: 11941 RVA: 0x000A89DC File Offset: 0x000A6BDC
			public void Dispose(bool disposing)
			{
				bool disposed = this.disposed;
				if (!disposed)
				{
					if (disposing)
					{
						Utility.GPUBuffer<T> gpubuffer = this.gpuData;
						if (gpubuffer != null)
						{
							gpubuffer.Dispose();
						}
						this.cpuData.Dispose();
						this.updateRanges.Dispose();
					}
					this.disposed = true;
				}
			}

			// Token: 0x06002EA6 RID: 11942 RVA: 0x000A8A34 File Offset: 0x000A6C34
			public void RegisterUpdate(uint start, uint size)
			{
				Debug.Assert((ulong)(start + size) <= (ulong)((long)this.cpuData.Length));
				int num = (int)(this.m_UpdateRangesBatchStart + this.m_UpdateRangesEnqueued);
				bool flag = this.m_UpdateRangesEnqueued > 0U;
				if (flag)
				{
					int num2 = num - 1;
					GfxUpdateBufferRange gfxUpdateBufferRange = this.updateRanges[num2];
					uint num3 = start * this.m_ElemStride;
					bool flag2 = gfxUpdateBufferRange.offsetFromWriteStart + gfxUpdateBufferRange.size == num3;
					if (flag2)
					{
						this.updateRanges[num2] = new GfxUpdateBufferRange
						{
							source = gfxUpdateBufferRange.source,
							offsetFromWriteStart = gfxUpdateBufferRange.offsetFromWriteStart,
							size = gfxUpdateBufferRange.size + size * this.m_ElemStride
						};
						this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
						return;
					}
				}
				this.m_UpdateRangeMin = Math.Min(this.m_UpdateRangeMin, start);
				this.m_UpdateRangeMax = Math.Max(this.m_UpdateRangeMax, start + size);
				bool flag3 = this.m_UpdateRangesEnqueued == this.m_UpdateRangePoolSize;
				if (flag3)
				{
					this.m_UpdateRangesSaturated = true;
				}
				else
				{
					UIntPtr uintPtr = new UIntPtr(this.cpuData.Slice<T>((int)start, (int)size).GetUnsafeReadOnlyPtr<T>());
					this.updateRanges[num] = new GfxUpdateBufferRange
					{
						source = uintPtr,
						offsetFromWriteStart = start * this.m_ElemStride,
						size = size * this.m_ElemStride
					};
					this.m_UpdateRangesEnqueued += 1U;
				}
			}

			// Token: 0x06002EA7 RID: 11943 RVA: 0x000A8BC0 File Offset: 0x000A6DC0
			private bool HasMappedBufferRange()
			{
				return Utility.HasMappedBufferRange();
			}

			// Token: 0x06002EA8 RID: 11944 RVA: 0x000A8BD8 File Offset: 0x000A6DD8
			public void SendUpdates()
			{
				bool flag = this.HasMappedBufferRange();
				if (flag)
				{
					this.SendPartialRanges();
				}
				else
				{
					this.SendFullRange();
				}
			}

			// Token: 0x06002EA9 RID: 11945 RVA: 0x000A8C00 File Offset: 0x000A6E00
			public void SendFullRange()
			{
				uint num = (uint)((long)this.cpuData.Length * (long)((ulong)this.m_ElemStride));
				this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
				{
					source = new UIntPtr(this.cpuData.GetUnsafeReadOnlyPtr<T>()),
					offsetFromWriteStart = 0U,
					size = num
				};
				Utility.GPUBuffer<T> gpubuffer = this.gpuData;
				if (gpubuffer != null)
				{
					gpubuffer.UpdateRanges(this.updateRanges.Slice<GfxUpdateBufferRange>((int)this.m_UpdateRangesBatchStart, 1), 0, (int)num);
				}
				this.ResetUpdateState();
			}

			// Token: 0x06002EAA RID: 11946 RVA: 0x000A8C94 File Offset: 0x000A6E94
			public void SendPartialRanges()
			{
				bool flag = this.m_UpdateRangesEnqueued == 0U;
				if (!flag)
				{
					bool updateRangesSaturated = this.m_UpdateRangesSaturated;
					if (updateRangesSaturated)
					{
						uint num = this.m_UpdateRangeMax - this.m_UpdateRangeMin;
						this.m_UpdateRangesEnqueued = 1U;
						this.updateRanges[(int)this.m_UpdateRangesBatchStart] = new GfxUpdateBufferRange
						{
							source = new UIntPtr(this.cpuData.Slice<T>((int)this.m_UpdateRangeMin, (int)num).GetUnsafeReadOnlyPtr<T>()),
							offsetFromWriteStart = this.m_UpdateRangeMin * this.m_ElemStride,
							size = num * this.m_ElemStride
						};
					}
					uint num2 = this.m_UpdateRangeMin * this.m_ElemStride;
					uint num3 = this.m_UpdateRangeMax * this.m_ElemStride;
					bool flag2 = num2 > 0U;
					if (flag2)
					{
						for (uint num4 = 0U; num4 < this.m_UpdateRangesEnqueued; num4 += 1U)
						{
							int num5 = (int)(num4 + this.m_UpdateRangesBatchStart);
							this.updateRanges[num5] = new GfxUpdateBufferRange
							{
								source = this.updateRanges[num5].source,
								offsetFromWriteStart = this.updateRanges[num5].offsetFromWriteStart - num2,
								size = this.updateRanges[num5].size
							};
						}
					}
					Utility.GPUBuffer<T> gpubuffer = this.gpuData;
					if (gpubuffer != null)
					{
						gpubuffer.UpdateRanges(this.updateRanges.Slice<GfxUpdateBufferRange>((int)this.m_UpdateRangesBatchStart, (int)this.m_UpdateRangesEnqueued), (int)num2, (int)num3);
					}
					this.ResetUpdateState();
				}
			}

			// Token: 0x06002EAB RID: 11947 RVA: 0x000A8E28 File Offset: 0x000A7028
			private void ResetUpdateState()
			{
				this.m_UpdateRangeMin = uint.MaxValue;
				this.m_UpdateRangeMax = 0U;
				this.m_UpdateRangesEnqueued = 0U;
				this.m_UpdateRangesBatchStart += this.m_UpdateRangePoolSize;
				bool flag = (ulong)this.m_UpdateRangesBatchStart >= (ulong)((long)this.updateRanges.Length);
				if (flag)
				{
					this.m_UpdateRangesBatchStart = 0U;
				}
				this.m_UpdateRangesSaturated = false;
			}

			// Token: 0x0400157D RID: 5501
			public Utility.GPUBuffer<T> gpuData;

			// Token: 0x0400157E RID: 5502
			public NativeArray<T> cpuData;

			// Token: 0x0400157F RID: 5503
			public NativeArray<GfxUpdateBufferRange> updateRanges;

			// Token: 0x04001580 RID: 5504
			public GPUBufferAllocator allocator;

			// Token: 0x04001581 RID: 5505
			private readonly uint m_UpdateRangePoolSize;

			// Token: 0x04001582 RID: 5506
			private uint m_ElemStride;

			// Token: 0x04001583 RID: 5507
			private uint m_UpdateRangeMin;

			// Token: 0x04001584 RID: 5508
			private uint m_UpdateRangeMax;

			// Token: 0x04001585 RID: 5509
			private uint m_UpdateRangesEnqueued;

			// Token: 0x04001586 RID: 5510
			private uint m_UpdateRangesBatchStart;

			// Token: 0x04001587 RID: 5511
			private bool m_UpdateRangesSaturated;
		}
	}
}
