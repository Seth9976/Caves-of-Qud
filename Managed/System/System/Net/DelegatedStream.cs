using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000579 RID: 1401
	internal class DelegatedStream : Stream
	{
		// Token: 0x06002D2E RID: 11566 RVA: 0x0009AE9D File Offset: 0x0009909D
		protected DelegatedStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._stream = stream;
			this._netStream = stream as NetworkStream;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x0009AEC6 File Offset: 0x000990C6
		protected Stream BaseStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x0009AECE File Offset: 0x000990CE
		public override bool CanRead
		{
			get
			{
				return this._stream.CanRead;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002D31 RID: 11569 RVA: 0x0009AEDB File Offset: 0x000990DB
		public override bool CanSeek
		{
			get
			{
				return this._stream.CanSeek;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x0009AEE8 File Offset: 0x000990E8
		public override bool CanWrite
		{
			get
			{
				return this._stream.CanWrite;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002D33 RID: 11571 RVA: 0x0009AEF5 File Offset: 0x000990F5
		public override long Length
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				return this._stream.Length;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x0009AF15 File Offset: 0x00099115
		// (set) Token: 0x06002D35 RID: 11573 RVA: 0x0009AF35 File Offset: 0x00099135
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				return this._stream.Position;
			}
			set
			{
				if (!this.CanSeek)
				{
					throw new NotSupportedException("Seeking is not supported on this stream.");
				}
				this._stream.Position = value;
			}
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0009AF58 File Offset: 0x00099158
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			IAsyncResult asyncResult;
			if (this._netStream != null)
			{
				asyncResult = this._netStream.BeginRead(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this._stream.BeginRead(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x0009AFAC File Offset: 0x000991AC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			IAsyncResult asyncResult;
			if (this._netStream != null)
			{
				asyncResult = this._netStream.BeginWrite(buffer, offset, count, callback, state);
			}
			else
			{
				asyncResult = this._stream.BeginWrite(buffer, offset, count, callback, state);
			}
			return asyncResult;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0009AFFF File Offset: 0x000991FF
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x0009B00C File Offset: 0x0009920C
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.EndRead(asyncResult);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0009B02D File Offset: 0x0009922D
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			this._stream.EndWrite(asyncResult);
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0009B04E File Offset: 0x0009924E
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0009B05B File Offset: 0x0009925B
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._stream.FlushAsync(cancellationToken);
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0009B069 File Offset: 0x00099269
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.Read(buffer, offset, count);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0009B08C File Offset: 0x0009928C
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("Reading is not supported on this stream.");
			}
			return this._stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0009B0B1 File Offset: 0x000992B1
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("Seeking is not supported on this stream.");
			}
			return this._stream.Seek(offset, origin);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0009B0D3 File Offset: 0x000992D3
		public override void SetLength(long value)
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("Seeking is not supported on this stream.");
			}
			this._stream.SetLength(value);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0009B0F4 File Offset: 0x000992F4
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0009B117 File Offset: 0x00099317
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("Writing is not supported on this stream.");
			}
			return this._stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x040018B8 RID: 6328
		private readonly Stream _stream;

		// Token: 0x040018B9 RID: 6329
		private readonly NetworkStream _netStream;
	}
}
