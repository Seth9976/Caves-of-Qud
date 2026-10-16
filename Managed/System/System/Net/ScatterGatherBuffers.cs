using System;

namespace System.Net
{
	// Token: 0x0200063B RID: 1595
	internal class ScatterGatherBuffers
	{
		// Token: 0x06003241 RID: 12865 RVA: 0x000ADBB4 File Offset: 0x000ABDB4
		internal ScatterGatherBuffers()
		{
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000ADBC7 File Offset: 0x000ABDC7
		internal ScatterGatherBuffers(long totalSize)
		{
			if (totalSize > 0L)
			{
				this.currentChunk = this.AllocateMemoryChunk((totalSize > 2147483647L) ? int.MaxValue : ((int)totalSize));
			}
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000ADC00 File Offset: 0x000ABE00
		internal BufferOffsetSize[] GetBuffers()
		{
			if (this.Empty)
			{
				return null;
			}
			BufferOffsetSize[] array = new BufferOffsetSize[this.chunkCount];
			int num = 0;
			for (ScatterGatherBuffers.MemoryChunk next = this.headChunk; next != null; next = next.Next)
			{
				array[num] = new BufferOffsetSize(next.Buffer, 0, next.FreeOffset, false);
				num++;
			}
			return array;
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x000ADC53 File Offset: 0x000ABE53
		private bool Empty
		{
			get
			{
				return this.headChunk == null || this.chunkCount == 0;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x000ADC68 File Offset: 0x000ABE68
		internal int Length
		{
			get
			{
				return this.totalLength;
			}
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000ADC70 File Offset: 0x000ABE70
		internal void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				int num = (this.Empty ? 0 : (this.currentChunk.Buffer.Length - this.currentChunk.FreeOffset));
				if (num == 0)
				{
					ScatterGatherBuffers.MemoryChunk memoryChunk = this.AllocateMemoryChunk(count);
					if (this.currentChunk != null)
					{
						this.currentChunk.Next = memoryChunk;
					}
					this.currentChunk = memoryChunk;
				}
				int num2 = ((count < num) ? count : num);
				Buffer.BlockCopy(buffer, offset, this.currentChunk.Buffer, this.currentChunk.FreeOffset, num2);
				offset += num2;
				count -= num2;
				this.totalLength += num2;
				this.currentChunk.FreeOffset += num2;
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000ADD28 File Offset: 0x000ABF28
		private ScatterGatherBuffers.MemoryChunk AllocateMemoryChunk(int newSize)
		{
			if (newSize > this.nextChunkLength)
			{
				this.nextChunkLength = newSize;
			}
			ScatterGatherBuffers.MemoryChunk memoryChunk = new ScatterGatherBuffers.MemoryChunk(this.nextChunkLength);
			if (this.Empty)
			{
				this.headChunk = memoryChunk;
			}
			this.nextChunkLength *= 2;
			this.chunkCount++;
			return memoryChunk;
		}

		// Token: 0x04001D7D RID: 7549
		private ScatterGatherBuffers.MemoryChunk headChunk;

		// Token: 0x04001D7E RID: 7550
		private ScatterGatherBuffers.MemoryChunk currentChunk;

		// Token: 0x04001D7F RID: 7551
		private int nextChunkLength = 1024;

		// Token: 0x04001D80 RID: 7552
		private int totalLength;

		// Token: 0x04001D81 RID: 7553
		private int chunkCount;

		// Token: 0x0200063C RID: 1596
		private class MemoryChunk
		{
			// Token: 0x06003248 RID: 12872 RVA: 0x000ADD7D File Offset: 0x000ABF7D
			internal MemoryChunk(int bufferSize)
			{
				this.Buffer = new byte[bufferSize];
			}

			// Token: 0x04001D82 RID: 7554
			internal byte[] Buffer;

			// Token: 0x04001D83 RID: 7555
			internal int FreeOffset;

			// Token: 0x04001D84 RID: 7556
			internal ScatterGatherBuffers.MemoryChunk Next;
		}
	}
}
