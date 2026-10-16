using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PooledBlobBuilder : BlobBuilder
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00007029 File Offset: 0x00005229
		private PooledBlobBuilder(int size)
			: base(size)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007032 File Offset: 0x00005232
		public static PooledBlobBuilder GetInstance()
		{
			return PooledBlobBuilder.s_chunkPool.Allocate();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000703E File Offset: 0x0000523E
		protected override BlobBuilder AllocateChunk(int minimalSize)
		{
			if (minimalSize <= 1024)
			{
				return PooledBlobBuilder.s_chunkPool.Allocate();
			}
			return new BlobBuilder(minimalSize);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007059 File Offset: 0x00005259
		protected override void FreeChunk()
		{
			PooledBlobBuilder.s_chunkPool.Free(this);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007066 File Offset: 0x00005266
		public new void Free()
		{
			base.Free();
		}

		// Token: 0x0400019C RID: 412
		private const int PoolSize = 128;

		// Token: 0x0400019D RID: 413
		private const int ChunkSize = 1024;

		// Token: 0x0400019E RID: 414
		private static readonly ObjectPool<PooledBlobBuilder> s_chunkPool = new ObjectPool<PooledBlobBuilder>(() => new PooledBlobBuilder(1024), 128);
	}
}
