using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO.Compression
{
	// Token: 0x0200002B RID: 43
	internal sealed class PositionPreservingWriteOnlyStreamWrapper : Stream
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00006534 File Offset: 0x00004734
		public PositionPreservingWriteOnlyStreamWrapper(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00006543 File Offset: 0x00004743
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006546 File Offset: 0x00004746
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00003833 File Offset: 0x00001A33
		public override long Position
		{
			get
			{
				return this._position;
			}
			set
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000654E File Offset: 0x0000474E
		public override void Write(byte[] buffer, int offset, int count)
		{
			this._position += (long)count;
			this._stream.Write(buffer, offset, count);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000656D File Offset: 0x0000476D
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this._position += (long)count;
			return this._stream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006590 File Offset: 0x00004790
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._stream.EndWrite(asyncResult);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000659E File Offset: 0x0000479E
		public override void WriteByte(byte value)
		{
			this._position += 1L;
			this._stream.WriteByte(value);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000065BB File Offset: 0x000047BB
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			this._position += (long)count;
			return this._stream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000065DC File Offset: 0x000047DC
		public override bool CanTimeout
		{
			get
			{
				return this._stream.CanTimeout;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000065E9 File Offset: 0x000047E9
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000065F6 File Offset: 0x000047F6
		public override int ReadTimeout
		{
			get
			{
				return this._stream.ReadTimeout;
			}
			set
			{
				this._stream.ReadTimeout = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006604 File Offset: 0x00004804
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006611 File Offset: 0x00004811
		public override int WriteTimeout
		{
			get
			{
				return this._stream.WriteTimeout;
			}
			set
			{
				this._stream.WriteTimeout = value;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000661F File Offset: 0x0000481F
		public override void Flush()
		{
			this._stream.Flush();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000662C File Offset: 0x0000482C
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._stream.FlushAsync(cancellationToken);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000663A File Offset: 0x0000483A
		public override void Close()
		{
			this._stream.Close();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006647 File Offset: 0x00004847
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._stream.Dispose();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003833 File Offset: 0x00001A33
		public override long Length
		{
			get
			{
				throw new NotSupportedException("This operation is not supported.");
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003833 File Offset: 0x00001A33
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003833 File Offset: 0x00001A33
		public override void SetLength(long value)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003833 File Offset: 0x00001A33
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("This operation is not supported.");
		}

		// Token: 0x0400016E RID: 366
		private readonly Stream _stream;

		// Token: 0x0400016F RID: 367
		private long _position;
	}
}
