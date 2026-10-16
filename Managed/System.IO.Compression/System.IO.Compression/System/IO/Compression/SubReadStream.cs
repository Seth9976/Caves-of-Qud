using System;

namespace System.IO.Compression
{
	// Token: 0x0200003B RID: 59
	internal sealed class SubReadStream : Stream
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00009530 File Offset: 0x00007730
		public SubReadStream(Stream superStream, long startPosition, long maxLength)
		{
			this._startInSuperStream = startPosition;
			this._positionInSuperStream = startPosition;
			this._endInSuperStream = startPosition + maxLength;
			this._superStream = superStream;
			this._canRead = true;
			this._isDisposed = false;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009564 File Offset: 0x00007764
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._endInSuperStream - this._startInSuperStream;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00009579 File Offset: 0x00007779
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000958E File Offset: 0x0000778E
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._positionInSuperStream - this._startInSuperStream;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000095A0 File Offset: 0x000077A0
		public override bool CanRead
		{
			get
			{
				return this._superStream.CanRead && this._canRead;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000095B7 File Offset: 0x000077B7
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009414 File Offset: 0x00007614
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000095D8 File Offset: 0x000077D8
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			if (this._superStream.Position != this._positionInSuperStream)
			{
				this._superStream.Seek(this._positionInSuperStream, SeekOrigin.Begin);
			}
			if (this._positionInSuperStream + (long)count > this._endInSuperStream)
			{
				count = (int)(this._endInSuperStream - this._positionInSuperStream);
			}
			int num = this._superStream.Read(buffer, offset, count);
			this._positionInSuperStream += (long)num;
			return num;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000958E File Offset: 0x0000778E
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009657 File Offset: 0x00007857
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009669 File Offset: 0x00007869
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009669 File Offset: 0x00007869
		public override void Flush()
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000967B File Offset: 0x0000787B
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				this._canRead = false;
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040001F9 RID: 505
		private readonly long _startInSuperStream;

		// Token: 0x040001FA RID: 506
		private long _positionInSuperStream;

		// Token: 0x040001FB RID: 507
		private readonly long _endInSuperStream;

		// Token: 0x040001FC RID: 508
		private readonly Stream _superStream;

		// Token: 0x040001FD RID: 509
		private bool _canRead;

		// Token: 0x040001FE RID: 510
		private bool _isDisposed;
	}
}
