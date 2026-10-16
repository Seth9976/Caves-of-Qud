using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class BlobBuildingStream : Stream
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000456D File Offset: 0x0000276D
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004570 File Offset: 0x00002770
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004573 File Offset: 0x00002773
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004576 File Offset: 0x00002776
		public override long Length
		{
			get
			{
				return (long)this._builder.Count;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004584 File Offset: 0x00002784
		public static BlobBuildingStream GetInstance()
		{
			return BlobBuildingStream.s_pool.Allocate();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004590 File Offset: 0x00002790
		private BlobBuildingStream()
		{
			this._builder = new BlobBuilder(32768);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000045A8 File Offset: 0x000027A8
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._builder.WriteBytes(buffer, offset, count);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000045B8 File Offset: 0x000027B8
		public override void WriteByte(byte value)
		{
			this._builder.WriteByte(value);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000045C6 File Offset: 0x000027C6
		public void WriteInt32(int value)
		{
			this._builder.WriteInt32(value);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045D4 File Offset: 0x000027D4
		public Blob ReserveBytes(int byteCount)
		{
			return this._builder.ReserveBytes(byteCount);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000045E2 File Offset: 0x000027E2
		[NullableContext(0)]
		public ImmutableArray<byte> ToImmutableArray()
		{
			return this._builder.ToImmutableArray();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000045EF File Offset: 0x000027EF
		public void Free()
		{
			this._builder.Clear();
			BlobBuildingStream.s_pool.Free(this);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004607 File Offset: 0x00002807
		public override void Flush()
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004609 File Offset: 0x00002809
		protected override void Dispose(bool disposing)
		{
			this.Free();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004611 File Offset: 0x00002811
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004618 File Offset: 0x00002818
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000461F File Offset: 0x0000281F
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004626 File Offset: 0x00002826
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x0000462D File Offset: 0x0000282D
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000032 RID: 50
		private static readonly ObjectPool<BlobBuildingStream> s_pool = new ObjectPool<BlobBuildingStream>(() => new BlobBuildingStream());

		// Token: 0x04000033 RID: 51
		private readonly BlobBuilder _builder;

		// Token: 0x04000034 RID: 52
		public const int ChunkSize = 32768;
	}
}
