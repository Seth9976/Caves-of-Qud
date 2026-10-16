using System;

namespace System.IO.Compression
{
	// Token: 0x0200003A RID: 58
	internal sealed class WrappedStream : Stream
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000932B File Offset: 0x0000752B
		internal WrappedStream(Stream baseStream, bool closeBaseStream)
			: this(baseStream, closeBaseStream, null, null)
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009337 File Offset: 0x00007537
		private WrappedStream(Stream baseStream, bool closeBaseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
		{
			this._baseStream = baseStream;
			this._closeBaseStream = closeBaseStream;
			this._onClosed = onClosed;
			this._zipArchiveEntry = entry;
			this._isDisposed = false;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009363 File Offset: 0x00007563
		internal WrappedStream(Stream baseStream, ZipArchiveEntry entry, Action<ZipArchiveEntry> onClosed)
			: this(baseStream, false, entry, onClosed)
		{
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000936F File Offset: 0x0000756F
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Length;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009382 File Offset: 0x00007582
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009395 File Offset: 0x00007595
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._baseStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				this.ThrowIfCantSeek();
				this._baseStream.Position = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000093AF File Offset: 0x000075AF
		public override bool CanRead
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanRead;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000093C6 File Offset: 0x000075C6
		public override bool CanSeek
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanSeek;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000093DD File Offset: 0x000075DD
		public override bool CanWrite
		{
			get
			{
				return !this._isDisposed && this._baseStream.CanWrite;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000093F4 File Offset: 0x000075F4
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00009414 File Offset: 0x00007614
		private void ThrowIfCantRead()
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00009429 File Offset: 0x00007629
		private void ThrowIfCantWrite()
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support writing.");
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000943E File Offset: 0x0000763E
		private void ThrowIfCantSeek()
		{
			if (!this.CanSeek)
			{
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00009453 File Offset: 0x00007653
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantRead();
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000946F File Offset: 0x0000766F
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			return this._baseStream.Seek(offset, origin);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000948A File Offset: 0x0000768A
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantSeek();
			this.ThrowIfCantWrite();
			this._baseStream.SetLength(value);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000094AA File Offset: 0x000076AA
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000094C6 File Offset: 0x000076C6
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.ThrowIfCantWrite();
			this._baseStream.Flush();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000094E0 File Offset: 0x000076E0
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				Action<ZipArchiveEntry> onClosed = this._onClosed;
				if (onClosed != null)
				{
					onClosed(this._zipArchiveEntry);
				}
				if (this._closeBaseStream)
				{
					this._baseStream.Dispose();
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040001F4 RID: 500
		private readonly Stream _baseStream;

		// Token: 0x040001F5 RID: 501
		private readonly bool _closeBaseStream;

		// Token: 0x040001F6 RID: 502
		private readonly Action<ZipArchiveEntry> _onClosed;

		// Token: 0x040001F7 RID: 503
		private readonly ZipArchiveEntry _zipArchiveEntry;

		// Token: 0x040001F8 RID: 504
		private bool _isDisposed;
	}
}
