using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x02000018 RID: 24
	internal sealed class DeflateManagedStream : Stream
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003749 File Offset: 0x00001949
		internal DeflateManagedStream(Stream stream, ZipArchiveEntry.CompressionMethodValues method)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this.InitializeInflater(stream, false, null, method);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003784 File Offset: 0x00001984
		internal void InitializeInflater(Stream stream, bool leaveOpen, IFileFormatReader reader = null, ZipArchiveEntry.CompressionMethodValues method = ZipArchiveEntry.CompressionMethodValues.Deflate)
		{
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream does not support reading.", "stream");
			}
			this._inflater = new InflaterManaged(reader, method == ZipArchiveEntry.CompressionMethodValues.Deflate64);
			this._stream = stream;
			this._mode = CompressionMode.Decompress;
			this._leaveOpen = leaveOpen;
			this._buffer = new byte[8192];
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000037E4 File Offset: 0x000019E4
		internal void SetFileFormatWriter(IFileFormatWriter writer)
		{
			if (writer != null)
			{
				this._formatWriter = writer;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000037F0 File Offset: 0x000019F0
		public override bool CanRead
		{
			get
			{
				return this._stream != null && this._mode == CompressionMode.Decompress && this._stream.CanRead;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003811 File Offset: 0x00001A11
		public override bool CanWrite
		{
			get
			{
				return this._stream != null && this._mode == CompressionMode.Compress && this._stream.CanWrite;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003833 File Offset: 0x00001A33
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003833 File Offset: 0x00001A33
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003833 File Offset: 0x00001A33
		public override long Position
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000383F File Offset: 0x00001A3F
		public override void Flush()
		{
			this.EnsureNotDisposed();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003847 File Offset: 0x00001A47
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			this.EnsureNotDisposed();
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCanceled(cancellationToken);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003833 File Offset: 0x00001A33
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003833 File Offset: 0x00001A33
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003864 File Offset: 0x00001A64
		public override int Read(byte[] array, int offset, int count)
		{
			this.EnsureDecompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			int num = offset;
			int num2 = count;
			for (;;)
			{
				int num3 = this._inflater.Inflate(array, num, num2);
				num += num3;
				num2 -= num3;
				if (num2 == 0 || this._inflater.Finished())
				{
					goto IL_008A;
				}
				int num4 = this._stream.Read(this._buffer, 0, this._buffer.Length);
				if (num4 <= 0)
				{
					goto IL_008A;
				}
				if (num4 > this._buffer.Length)
				{
					break;
				}
				this._inflater.SetInput(this._buffer, 0, num4);
			}
			throw new InvalidDataException("Found invalid data while decoding.");
			IL_008A:
			return count - num2;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003900 File Offset: 0x00001B00
		private void ValidateParameters(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException("Offset plus count is larger than the length of target array.");
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000394C File Offset: 0x00001B4C
		private void EnsureNotDisposed()
		{
			if (this._stream == null)
			{
				DeflateManagedStream.ThrowStreamClosedException();
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000395B File Offset: 0x00001B5B
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowStreamClosedException()
		{
			throw new ObjectDisposedException(null, "Can not access a closed Stream.");
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003968 File Offset: 0x00001B68
		private void EnsureDecompressionMode()
		{
			if (this._mode != CompressionMode.Decompress)
			{
				DeflateManagedStream.ThrowCannotReadFromDeflateManagedStreamException();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003977 File Offset: 0x00001B77
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotReadFromDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Reading from the compression stream is not supported.");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003983 File Offset: 0x00001B83
		private void EnsureCompressionMode()
		{
			if (this._mode != CompressionMode.Compress)
			{
				DeflateManagedStream.ThrowCannotWriteToDeflateManagedStreamException();
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003993 File Offset: 0x00001B93
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCannotWriteToDeflateManagedStreamException()
		{
			throw new InvalidOperationException("Writing to the compression stream is not supported.");
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000025E4 File Offset: 0x000007E4
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.ReadAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000025FD File Offset: 0x000007FD
		public override int EndRead(IAsyncResult asyncResult)
		{
			return TaskToApm.End<int>(asyncResult);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000039A0 File Offset: 0x00001BA0
		public override Task<int> ReadAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.EnsureDecompressionMode();
			if (this._asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			Interlocked.Increment(ref this._asyncOperations);
			Task<int> task = null;
			Task<int> task2;
			try
			{
				int num = this._inflater.Inflate(array, offset, count);
				if (num != 0)
				{
					task2 = Task.FromResult<int>(num);
				}
				else if (this._inflater.Finished())
				{
					task2 = Task.FromResult<int>(0);
				}
				else
				{
					task = this._stream.ReadAsync(this._buffer, 0, this._buffer.Length, cancellationToken);
					if (task == null)
					{
						throw new InvalidOperationException("Stream does not support reading.");
					}
					task2 = this.ReadAsyncCore(task, array, offset, count, cancellationToken);
				}
			}
			finally
			{
				if (task == null)
				{
					Interlocked.Decrement(ref this._asyncOperations);
				}
			}
			return task2;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003A80 File Offset: 0x00001C80
		private async Task<int> ReadAsyncCore(Task<int> readTask, byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			int num2;
			try
			{
				int num;
				for (;;)
				{
					num = await readTask.ConfigureAwait(false);
					this.EnsureNotDisposed();
					if (num <= 0)
					{
						break;
					}
					if (num > this._buffer.Length)
					{
						goto Block_2;
					}
					cancellationToken.ThrowIfCancellationRequested();
					this._inflater.SetInput(this._buffer, 0, num);
					num = this._inflater.Inflate(array, offset, count);
					if (num != 0 || this._inflater.Finished())
					{
						goto IL_012C;
					}
					readTask = this._stream.ReadAsync(this._buffer, 0, this._buffer.Length, cancellationToken);
					if (readTask == null)
					{
						goto Block_5;
					}
				}
				return 0;
				Block_2:
				throw new InvalidDataException("Found invalid data while decoding.");
				Block_5:
				throw new InvalidOperationException("Stream does not support reading.");
				IL_012C:
				num2 = num;
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncOperations);
			}
			return num2;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003AED File Offset: 0x00001CED
		public override void Write(byte[] array, int offset, int count)
		{
			this.EnsureCompressionMode();
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			this.DoMaintenance(array, offset, count);
			this.WriteDeflaterOutput();
			this._deflater.SetInput(array, offset, count);
			this.WriteDeflaterOutput();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003B28 File Offset: 0x00001D28
		private void WriteDeflaterOutput()
		{
			while (!this._deflater.NeedsInput())
			{
				int deflateOutput = this._deflater.GetDeflateOutput(this._buffer);
				if (deflateOutput > 0)
				{
					this._stream.Write(this._buffer, 0, deflateOutput);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B70 File Offset: 0x00001D70
		private void DoMaintenance(byte[] array, int offset, int count)
		{
			if (count <= 0)
			{
				return;
			}
			this._wroteBytes = true;
			if (this._formatWriter == null)
			{
				return;
			}
			if (!this._wroteHeader)
			{
				byte[] header = this._formatWriter.GetHeader();
				this._stream.Write(header, 0, header.Length);
				this._wroteHeader = true;
			}
			this._formatWriter.UpdateWithBytesRead(array, offset, count);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003BCC File Offset: 0x00001DCC
		private void PurgeBuffers(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this._stream == null)
			{
				return;
			}
			this.Flush();
			if (this._mode != CompressionMode.Compress)
			{
				return;
			}
			if (this._wroteBytes)
			{
				this.WriteDeflaterOutput();
				bool flag;
				do
				{
					int num;
					flag = this._deflater.Finish(this._buffer, out num);
					if (num > 0)
					{
						this._stream.Write(this._buffer, 0, num);
					}
				}
				while (!flag);
			}
			else
			{
				bool flag2;
				do
				{
					int num2;
					flag2 = this._deflater.Finish(this._buffer, out num2);
				}
				while (!flag2);
			}
			if (this._formatWriter != null && this._wroteHeader)
			{
				byte[] footer = this._formatWriter.GetFooter();
				this._stream.Write(footer, 0, footer.Length);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C7C File Offset: 0x00001E7C
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.PurgeBuffers(disposing);
			}
			finally
			{
				try
				{
					if (disposing && !this._leaveOpen && this._stream != null)
					{
						this._stream.Dispose();
					}
				}
				finally
				{
					this._stream = null;
					try
					{
						DeflaterManaged deflater = this._deflater;
						if (deflater != null)
						{
							deflater.Dispose();
						}
						InflaterManaged inflater = this._inflater;
						if (inflater != null)
						{
							inflater.Dispose();
						}
					}
					finally
					{
						this._deflater = null;
						this._inflater = null;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D20 File Offset: 0x00001F20
		public override Task WriteAsync(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			this.EnsureCompressionMode();
			if (this._asyncOperations != 0)
			{
				throw new InvalidOperationException("Only one asynchronous reader or writer is allowed time at one time.");
			}
			this.ValidateParameters(array, offset, count);
			this.EnsureNotDisposed();
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<int>(cancellationToken);
			}
			return this.WriteAsyncCore(array, offset, count, cancellationToken);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003D74 File Offset: 0x00001F74
		private async Task WriteAsyncCore(byte[] array, int offset, int count, CancellationToken cancellationToken)
		{
			Interlocked.Increment(ref this._asyncOperations);
			try
			{
				await base.WriteAsync(array, offset, count, cancellationToken).ConfigureAwait(false);
			}
			finally
			{
				Interlocked.Decrement(ref this._asyncOperations);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000027A1 File Offset: 0x000009A1
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback asyncCallback, object asyncState)
		{
			return TaskToApm.Begin(this.WriteAsync(buffer, offset, count, CancellationToken.None), asyncCallback, asyncState);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000027BA File Offset: 0x000009BA
		public override void EndWrite(IAsyncResult asyncResult)
		{
			TaskToApm.End(asyncResult);
		}

		// Token: 0x040000C0 RID: 192
		internal const int DefaultBufferSize = 8192;

		// Token: 0x040000C1 RID: 193
		private Stream _stream;

		// Token: 0x040000C2 RID: 194
		private CompressionMode _mode;

		// Token: 0x040000C3 RID: 195
		private bool _leaveOpen;

		// Token: 0x040000C4 RID: 196
		private InflaterManaged _inflater;

		// Token: 0x040000C5 RID: 197
		private DeflaterManaged _deflater;

		// Token: 0x040000C6 RID: 198
		private byte[] _buffer;

		// Token: 0x040000C7 RID: 199
		private int _asyncOperations;

		// Token: 0x040000C8 RID: 200
		private IFileFormatWriter _formatWriter;

		// Token: 0x040000C9 RID: 201
		private bool _wroteHeader;

		// Token: 0x040000CA RID: 202
		private bool _wroteBytes;
	}
}
