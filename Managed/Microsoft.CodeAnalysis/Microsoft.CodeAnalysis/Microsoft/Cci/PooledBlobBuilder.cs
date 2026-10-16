using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.Cci
{
	// Token: 0x020000BD RID: 189
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PooledBlobBuilder : BlobBuilder, IDisposable
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00016BF5 File Offset: 0x00014DF5
		private PooledBlobBuilder(int size)
			: base(size)
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00016BFE File Offset: 0x00014DFE
		public static PooledBlobBuilder GetInstance(int size = 1024)
		{
			return PooledBlobBuilder.s_chunkPool.Allocate();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00016C0A File Offset: 0x00014E0A
		protected override BlobBuilder AllocateChunk(int minimalSize)
		{
			if (minimalSize <= 1024)
			{
				return PooledBlobBuilder.s_chunkPool.Allocate();
			}
			return new BlobBuilder(minimalSize);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00016C25 File Offset: 0x00014E25
		protected override void FreeChunk()
		{
			PooledBlobBuilder.s_chunkPool.Free(this);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00016C32 File Offset: 0x00014E32
		public new void Free()
		{
			base.Free();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00016C3A File Offset: 0x00014E3A
		void IDisposable.Dispose()
		{
			this.Free();
		}

		// Token: 0x040001EF RID: 495
		private const int PoolSize = 128;

		// Token: 0x040001F0 RID: 496
		private const int ChunkSize = 1024;

		// Token: 0x040001F1 RID: 497
		private static readonly ObjectPool<PooledBlobBuilder> s_chunkPool = new ObjectPool<PooledBlobBuilder>(() => new PooledBlobBuilder(1024), 128);
	}
}
