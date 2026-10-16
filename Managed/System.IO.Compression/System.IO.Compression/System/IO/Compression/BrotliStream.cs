using System;
using System.Buffers;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x0200000B RID: 11
	public sealed class BrotliStream : Stream
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000223E File Offset: 0x0000043E
		public BrotliStream(Stream stream, CompressionMode mode)
			: this(stream, mode, false)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000224C File Offset: 0x0000044C
		public BrotliStream(Stream stream, CompressionMode mode, bool leaveOpen)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (mode != CompressionMode.Decompress)
			{
				if (mode != CompressionMode.Compress)
				{
					throw new ArgumentException("Enum value was out of legal range.", "mode");
				}
				if (!stream.CanWrite)
				{
					throw new ArgumentException("Stream does not support writing.", "stream");
				}
			}
			else if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this._mode = mode;
			this._stream = stream;
			this._leaveOpen = leaveOpen;
			this._buffer = new byte[65520];
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000022D9 File Offset: 0x000004D9
		private void EnsureNotDisposed()
		{
			if (this._stream == null)
			{
				throw new ObjectDisposedException("stream", "Can not access a closed Stream.");
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000022F4 File Offset: 0x000004F4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._stream != null)
				{
					if (this._mode == CompressionMode.Compress)
					{
						this.WriteCore(ReadOnlySpan<byte>.Empty, true);
					}
					if (!this._leaveOpen)
					{
						this._stream.Dispose();
					}
				}
			}
			finally
			{
				this._stream = null;
				this._encoder.Dispose();
				this._decoder.Dispose();
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000236C File Offset: 0x0000056C
		private static void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Positive number required.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Positive number required.");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset plus count is larger than the length of target array.");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023C2 File Offset: 0x000005C2
		public Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023CA File Offset: 0x000005CA
		public override bool CanRead
		{
			get
			{
				return this._mode == CompressionMode.Decompress && this._stream != null && this._stream.CanRead;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023E9 File Offset: 0x000005E9
		public override bool CanWrite
		{
			get
			{
				return this._mode == CompressionMode.Compress && this._stream != null && this._stream.CanWrite;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000240C File Offset: 0x0000060C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000240C File Offset: 0x0000060C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000240C File Offset: 0x0000060C
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

		// Token: 0x0600002F RID: 47 RVA: 0x0000240C File Offset: 0x0000060C
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002413 File Offset: 0x00000613
		private bool AsyncOperationIsActive
		{
			get
			{
				return this._activeAsyncOperation != 0;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000241E File Offset: 0x0000061E
		private void EnsureNoActiveAsyncOperation()
		{
			if (this.AsyncOperationIsActive)
			{
				BrotliStream.ThrowInvalidBeginCall();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000242D File Offset: 0x0000062D
		private void AsyncOperationStarting()
		{
			if (Interlocked.CompareExchange(ref this._activeAsyncOperation, 1, 0) != 0)
			{
				BrotliStream.ThrowInvalidBeginCall();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002443 File Offset: 0x00000643
		private void AsyncOperationCompleting()
		{
			Interlocked.CompareExchange(ref this._activeAsyncOperation, 0, 1);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002453 File Offset: 0x00000653
		private static void ThrowInvalidBeginCall()
		{
			throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000245F File Offset: 0x0000065F
		public override int Read(byte[] buffer, int offset, int count)
		{
			BrotliStream.ValidateParameters(buffer, offset, count);
			return this.Read(new Span<byte>(buffer, offset, count));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002478 File Offset: 0x00000678
		public override int Read(Span<byte> buffer)
		{
			if (this._mode != CompressionMode.Decompress)
			{
				throw new InvalidOperationException("Can not perform Read operations on a BrotliStream constructed with CompressionMode.Compress.");
			}
			this.EnsureNotDisposed();
			int num = 0;
			OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
			while (buffer.Length > 0 && operationStatus != OperationStatus.Done)
			{
				if (operationStatus == OperationStatus.NeedMoreData)
				{
					if (this._bufferCount > 0 && this._bufferOffset != 0)
					{
						this._buffer.AsSpan<byte>(this._bufferOffset, this._bufferCount).CopyTo(this._buffer);
					}
					this._bufferOffset = 0;
					int num2;
					while (this._bufferCount < this._buffer.Length && (num2 = this._stream.Read(this._buffer, this._bufferCount, this._buffer.Length - this._bufferCount)) > 0)
					{
						this._bufferCount += num2;
						if (this._bufferCount > this._buffer.Length)
						{
							throw new InvalidDataException("BrotliStream.BaseStream returned more bytes than requested in Read.");
						}
					}
					if (this._bufferCount <= 0)
					{
						break;
					}
				}
				int num3;
				int num4;
				operationStatus = this._decoder.Decompress(this._buffer.AsSpan<byte>(this._bufferOffset, this._bufferCount), buffer, out num3, out num4);
				if (operationStatus == OperationStatus.InvalidData)
				{
					throw new InvalidOperationException("Decoder ran into invalid data.");
				}
				if (num3 > 0)
				{
					this._bufferOffset += num3;
					this._bufferCount -= num3;
				}
				if (num4 > 0)
				{
					num += num4;
					buffer = buffer.Slice(num4);
				}
			}
			return num;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000025E4 File Offset: 0x000007E4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025FD File Offset: 0x000007FD
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002608 File Offset: 0x00000808
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			BrotliStream.ValidateParameters(buffer, offset, count);
			return this.ReadAsync(new Memory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002635 File Offset: 0x00000835
		public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._mode != CompressionMode.Decompress)
			{
				throw new InvalidOperationException("Can not perform Read operations on a BrotliStream constructed with CompressionMode.Compress.");
			}
			this.EnsureNoActiveAsyncOperation();
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return new ValueTask<int>(Task.FromCanceled<int>(cancellationToken));
			}
			return this.FinishReadAsyncMemory(buffer, cancellationToken);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002674 File Offset: 0x00000874
		private async ValueTask<int> FinishReadAsyncMemory(Memory<byte> buffer, CancellationToken cancellationToken)
		{
			this.AsyncOperationStarting();
			int num4;
			try
			{
				int totalWritten = 0;
				Memory<byte> empty = Memory<byte>.Empty;
				OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
				while (buffer.Length > 0 && operationStatus != OperationStatus.Done)
				{
					if (operationStatus == OperationStatus.NeedMoreData)
					{
						if (this._bufferCount > 0 && this._bufferOffset != 0)
						{
							this._buffer.AsSpan<byte>(this._bufferOffset, this._bufferCount).CopyTo(this._buffer);
						}
						this._bufferOffset = 0;
						int num = 0;
						do
						{
							bool flag = this._bufferCount < this._buffer.Length;
							if (flag)
							{
								flag = (num = await this._stream.ReadAsync(new Memory<byte>(this._buffer, this._bufferCount, this._buffer.Length - this._bufferCount), default(CancellationToken)).ConfigureAwait(false)) > 0;
							}
							if (!flag)
							{
								goto Block_8;
							}
							this._bufferCount += num;
						}
						while (this._bufferCount <= this._buffer.Length);
						throw new InvalidDataException("BrotliStream.BaseStream returned more bytes than requested in Read.");
						Block_8:
						if (this._bufferCount <= 0)
						{
							break;
						}
					}
					cancellationToken.ThrowIfCancellationRequested();
					int num2;
					int num3;
					operationStatus = this._decoder.Decompress(this._buffer.AsSpan<byte>(this._bufferOffset, this._bufferCount), buffer.Span, out num2, out num3);
					if (operationStatus == OperationStatus.InvalidData)
					{
						throw new InvalidOperationException("Decoder ran into invalid data.");
					}
					if (num2 > 0)
					{
						this._bufferOffset += num2;
						this._bufferCount -= num2;
					}
					if (num3 > 0)
					{
						totalWritten += num3;
						buffer = buffer.Slice(num3);
					}
				}
				num4 = totalWritten;
			}
			finally
			{
				this.AsyncOperationCompleting();
			}
			return num4;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026C7 File Offset: 0x000008C7
		public BrotliStream(Stream stream, CompressionLevel compressionLevel)
			: this(stream, compressionLevel, false)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026D2 File Offset: 0x000008D2
		public BrotliStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
			: this(stream, CompressionMode.Compress, leaveOpen)
		{
			this._encoder.SetQuality(BrotliUtils.GetQualityFromCompressionLevel(compressionLevel));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026EE File Offset: 0x000008EE
		public override void Write(byte[] buffer, int offset, int count)
		{
			BrotliStream.ValidateParameters(buffer, offset, count);
			this.WriteCore(new ReadOnlySpan<byte>(buffer, offset, count), false);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002707 File Offset: 0x00000907
		public override void Write(ReadOnlySpan<byte> buffer)
		{
			this.WriteCore(buffer, false);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002714 File Offset: 0x00000914
		internal void WriteCore(ReadOnlySpan<byte> buffer, bool isFinalBlock = false)
		{
			if (this._mode != CompressionMode.Compress)
			{
				throw new InvalidOperationException("Can not perform Write operations on a BrotliStream constructed with CompressionMode.Decompress.");
			}
			this.EnsureNotDisposed();
			OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
			Span<byte> span = new Span<byte>(this._buffer);
			while (operationStatus == OperationStatus.DestinationTooSmall)
			{
				int num = 0;
				int num2 = 0;
				operationStatus = this._encoder.Compress(buffer, span, out num, out num2, isFinalBlock);
				if (operationStatus == OperationStatus.InvalidData)
				{
					throw new InvalidOperationException("Encoder ran into invalid data.");
				}
				if (num2 > 0)
				{
					this._stream.Write(span.Slice(0, num2));
				}
				if (num > 0)
				{
					buffer = buffer.Slice(num);
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027A1 File Offset: 0x000009A1
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027BA File Offset: 0x000009BA
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027C4 File Offset: 0x000009C4
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			BrotliStream.ValidateParameters(buffer, offset, count);
			return this.WriteAsync(new ReadOnlyMemory<byte>(buffer, offset, count), cancellationToken).AsTask();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027F1 File Offset: 0x000009F1
		public override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._mode != CompressionMode.Compress)
			{
				throw new InvalidOperationException("Can not perform Write operations on a BrotliStream constructed with CompressionMode.Decompress.");
			}
			this.EnsureNoActiveAsyncOperation();
			this.EnsureNotDisposed();
			return new ValueTask(cancellationToken.IsCancellationRequested ? Task.FromCanceled<int>(cancellationToken) : this.WriteAsyncMemoryCore(buffer, cancellationToken));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002834 File Offset: 0x00000A34
		private async Task WriteAsyncMemoryCore(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken)
		{
			this.AsyncOperationStarting();
			try
			{
				OperationStatus lastResult = OperationStatus.DestinationTooSmall;
				while (lastResult == OperationStatus.DestinationTooSmall)
				{
					Memory<byte> memory = new Memory<byte>(this._buffer);
					int num = 0;
					int num2 = 0;
					lastResult = this._encoder.Compress(buffer, memory, out num, out num2, false);
					if (lastResult == OperationStatus.InvalidData)
					{
						throw new InvalidOperationException("Encoder ran into invalid data.");
					}
					if (num > 0)
					{
						buffer = buffer.Slice(num);
					}
					if (num2 > 0)
					{
						await this._stream.WriteAsync(new ReadOnlyMemory<byte>(this._buffer, 0, num2), cancellationToken).ConfigureAwait(false);
					}
				}
			}
			finally
			{
				this.AsyncOperationCompleting();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002888 File Offset: 0x00000A88
		public override void Flush()
		{
			this.EnsureNotDisposed();
			if (this._mode == CompressionMode.Compress)
			{
				if (this._encoder._state == null || this._encoder._state.IsClosed)
				{
					return;
				}
				OperationStatus operationStatus = OperationStatus.DestinationTooSmall;
				Span<byte> span = new Span<byte>(this._buffer);
				while (operationStatus == OperationStatus.DestinationTooSmall)
				{
					int num = 0;
					operationStatus = this._encoder.Flush(span, out num);
					if (operationStatus == OperationStatus.InvalidData)
					{
						throw new InvalidDataException("Encoder ran into invalid data.");
					}
					if (num > 0)
					{
						this._stream.Write(span.Slice(0, num));
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002916 File Offset: 0x00000B16
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.EnsureNoActiveAsyncOperation();
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			if (this._mode == CompressionMode.Compress)
			{
				return this.FlushAsyncCore(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000294C File Offset: 0x00000B4C
		private async Task FlushAsyncCore(CancellationToken cancellationToken)
		{
			this.AsyncOperationStarting();
			try
			{
				if (this._encoder._state != null && !this._encoder._state.IsClosed)
				{
					OperationStatus lastResult = OperationStatus.DestinationTooSmall;
					while (lastResult == OperationStatus.DestinationTooSmall)
					{
						Memory<byte> memory = new Memory<byte>(this._buffer);
						int num = 0;
						lastResult = this._encoder.Flush(memory, out num);
						if (lastResult == OperationStatus.InvalidData)
						{
							throw new InvalidDataException("Encoder ran into invalid data.");
						}
						if (num > 0)
						{
							await this._stream.WriteAsync(memory.Slice(0, num), cancellationToken).ConfigureAwait(false);
						}
					}
				}
			}
			finally
			{
				this.AsyncOperationCompleting();
			}
		}

		// Token: 0x04000080 RID: 128
		private const int DefaultInternalBufferSize = 65520;

		// Token: 0x04000081 RID: 129
		private Stream _stream;

		// Token: 0x04000082 RID: 130
		private readonly byte[] _buffer;

		// Token: 0x04000083 RID: 131
		private readonly bool _leaveOpen;

		// Token: 0x04000084 RID: 132
		private readonly CompressionMode _mode;

		// Token: 0x04000085 RID: 133
		private int _activeAsyncOperation;

		// Token: 0x04000086 RID: 134
		private BrotliDecoder _decoder;

		// Token: 0x04000087 RID: 135
		private int _bufferOffset;

		// Token: 0x04000088 RID: 136
		private int _bufferCount;

		// Token: 0x04000089 RID: 137
		private BrotliEncoder _encoder;
	}
}
