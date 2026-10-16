using System;

namespace System.IO.Compression
{
	// Token: 0x0200003C RID: 60
	internal sealed class CheckSumAndSizeWriteStream : Stream
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x000096A0 File Offset: 0x000078A0
		public CheckSumAndSizeWriteStream(Stream baseStream, Stream baseBaseStream, bool leaveOpenOnClose, ZipArchiveEntry entry, EventHandler onClose, Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes)
		{
			this._baseStream = baseStream;
			this._baseBaseStream = baseBaseStream;
			this._position = 0L;
			this._checksum = 0U;
			this._leaveOpenOnClose = leaveOpenOnClose;
			this._canWrite = true;
			this._isDisposed = false;
			this._initialPosition = 0L;
			this._zipArchiveEntry = entry;
			this._onClose = onClose;
			this._saveCrcAndSizes = saveCrcAndSizes;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009705 File Offset: 0x00007905
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00009717 File Offset: 0x00007917
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00009705 File Offset: 0x00007905
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this._position;
			}
			set
			{
				this.ThrowIfDisposed();
				throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00002409 File Offset: 0x00000609
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00009725 File Offset: 0x00007925
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000972D File Offset: 0x0000792D
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString(), "A stream from ZipArchiveEntry has been disposed.");
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000974D File Offset: 0x0000794D
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support reading.");
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009705 File Offset: 0x00007905
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("This stream from ZipArchiveEntry does not support seeking.");
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000975F File Offset: 0x0000795F
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			throw new NotSupportedException("SetLength requires a stream that supports seeking and writing.");
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009774 File Offset: 0x00007974
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "The argument must be non-negative.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "The argument must be non-negative.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("The offset and length parameters are not valid for the array that was given.");
			}
			this.ThrowIfDisposed();
			if (count == 0)
			{
				return;
			}
			if (!this._everWritten)
			{
				this._initialPosition = this._baseBaseStream.Position;
				this._everWritten = true;
			}
			this._checksum = Crc32Helper.UpdateCrc32(this._checksum, buffer, offset, count);
			this._baseStream.Write(buffer, offset, count);
			this._position += (long)count;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009825 File Offset: 0x00007A25
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this._baseStream.Flush();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009838 File Offset: 0x00007A38
		protected override void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				if (!this._everWritten)
				{
					this._initialPosition = this._baseBaseStream.Position;
				}
				if (!this._leaveOpenOnClose)
				{
					this._baseStream.Dispose();
				}
				Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> saveCrcAndSizes = this._saveCrcAndSizes;
				if (saveCrcAndSizes != null)
				{
					saveCrcAndSizes(this._initialPosition, this.Position, this._checksum, this._baseBaseStream, this._zipArchiveEntry, this._onClose);
				}
				this._isDisposed = true;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040001FF RID: 511
		private readonly Stream _baseStream;

		// Token: 0x04000200 RID: 512
		private readonly Stream _baseBaseStream;

		// Token: 0x04000201 RID: 513
		private long _position;

		// Token: 0x04000202 RID: 514
		private uint _checksum;

		// Token: 0x04000203 RID: 515
		private readonly bool _leaveOpenOnClose;

		// Token: 0x04000204 RID: 516
		private bool _canWrite;

		// Token: 0x04000205 RID: 517
		private bool _isDisposed;

		// Token: 0x04000206 RID: 518
		private bool _everWritten;

		// Token: 0x04000207 RID: 519
		private long _initialPosition;

		// Token: 0x04000208 RID: 520
		private readonly ZipArchiveEntry _zipArchiveEntry;

		// Token: 0x04000209 RID: 521
		private readonly EventHandler _onClose;

		// Token: 0x0400020A RID: 522
		private readonly Action<long, long, uint, Stream, ZipArchiveEntry, EventHandler> _saveCrcAndSizes;
	}
}
