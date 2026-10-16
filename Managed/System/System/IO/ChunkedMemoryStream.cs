using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020004F6 RID: 1270
	internal sealed class ChunkedMemoryStream : Stream
	{
		// Token: 0x06002973 RID: 10611 RVA: 0x0008E95F File Offset: 0x0008CB5F
		internal ChunkedMemoryStream()
		{
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0008E968 File Offset: 0x0008CB68
		public byte[] ToArray()
		{
			byte[] array = new byte[this._totalLength];
			int num = 0;
			for (ChunkedMemoryStream.MemoryChunk memoryChunk = this._headChunk; memoryChunk != null; memoryChunk = memoryChunk._next)
			{
				Buffer.BlockCopy(memoryChunk._buffer, 0, array, num, memoryChunk._freeOffset);
				num += memoryChunk._freeOffset;
			}
			return array;
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0008E9B4 File Offset: 0x0008CBB4
		public override void Write(byte[] buffer, int offset, int count)
		{
			while (count > 0)
			{
				if (this._currentChunk != null)
				{
					int num = this._currentChunk._buffer.Length - this._currentChunk._freeOffset;
					if (num > 0)
					{
						int num2 = Math.Min(num, count);
						Buffer.BlockCopy(buffer, offset, this._currentChunk._buffer, this._currentChunk._freeOffset, num2);
						count -= num2;
						offset += num2;
						this._totalLength += num2;
						this._currentChunk._freeOffset += num2;
						continue;
					}
				}
				this.AppendChunk((long)count);
			}
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x0008EA4E File Offset: 0x0008CC4E
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			this.Write(buffer, offset, count);
			return Task.CompletedTask;
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x0008EA70 File Offset: 0x0008CC70
		private void AppendChunk(long count)
		{
			int num = ((this._currentChunk != null) ? (this._currentChunk._buffer.Length * 2) : 1024);
			if (count > (long)num)
			{
				num = (int)Math.Min(count, 1048576L);
			}
			ChunkedMemoryStream.MemoryChunk memoryChunk = new ChunkedMemoryStream.MemoryChunk(num);
			if (this._currentChunk == null)
			{
				this._headChunk = (this._currentChunk = memoryChunk);
				return;
			}
			this._currentChunk._next = memoryChunk;
			this._currentChunk = memoryChunk;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002978 RID: 10616 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x00003062 File Offset: 0x00001262
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x0008EAE2 File Offset: 0x0008CCE2
		public override long Length
		{
			get
			{
				return (long)this._totalLength;
			}
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Flush()
		{
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0008EAEB File Offset: 0x0008CCEB
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x000044FA File Offset: 0x000026FA
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x000044FA File Offset: 0x000026FA
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

		// Token: 0x06002980 RID: 10624 RVA: 0x000044FA File Offset: 0x000026FA
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000044FA File Offset: 0x000026FA
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0008EAF2 File Offset: 0x0008CCF2
		public override void SetLength(long value)
		{
			if (this._currentChunk != null)
			{
				throw new NotSupportedException();
			}
			this.AppendChunk(value);
		}

		// Token: 0x04001608 RID: 5640
		private ChunkedMemoryStream.MemoryChunk _headChunk;

		// Token: 0x04001609 RID: 5641
		private ChunkedMemoryStream.MemoryChunk _currentChunk;

		// Token: 0x0400160A RID: 5642
		private const int InitialChunkDefaultSize = 1024;

		// Token: 0x0400160B RID: 5643
		private const int MaxChunkSize = 1048576;

		// Token: 0x0400160C RID: 5644
		private int _totalLength;

		// Token: 0x020004F7 RID: 1271
		private sealed class MemoryChunk
		{
			// Token: 0x06002983 RID: 10627 RVA: 0x0008EB09 File Offset: 0x0008CD09
			internal MemoryChunk(int bufferSize)
			{
				this._buffer = new byte[bufferSize];
			}

			// Token: 0x0400160D RID: 5645
			internal readonly byte[] _buffer;

			// Token: 0x0400160E RID: 5646
			internal int _freeOffset;

			// Token: 0x0400160F RID: 5647
			internal ChunkedMemoryStream.MemoryChunk _next;
		}
	}
}
