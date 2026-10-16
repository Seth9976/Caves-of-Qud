using System;
using System.IO;
using System.Threading;

namespace NVorbis
{
	// Token: 0x02000002 RID: 2
	internal class BufferedReadStream : Stream
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public BufferedReadStream(Stream baseStream)
			: this(baseStream, 32768, 262144, false)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public BufferedReadStream(Stream baseStream, bool minimalRead)
			: this(baseStream, 32768, 262144, minimalRead)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002078 File Offset: 0x00000278
		public BufferedReadStream(Stream baseStream, int initialSize, int maxSize)
			: this(baseStream, initialSize, maxSize, false)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002084 File Offset: 0x00000284
		public BufferedReadStream(Stream baseStream, int initialSize, int maxBufferSize, bool minimalRead)
		{
			if (baseStream == null)
			{
				throw new ArgumentNullException("baseStream");
			}
			if (!baseStream.CanRead)
			{
				throw new ArgumentException("baseStream");
			}
			if (maxBufferSize < 1)
			{
				maxBufferSize = 1;
			}
			if (initialSize < 1)
			{
				initialSize = 1;
			}
			if (initialSize > maxBufferSize)
			{
				initialSize = maxBufferSize;
			}
			this._baseStream = baseStream;
			this._buffer = new StreamReadBuffer(baseStream, initialSize, maxBufferSize, minimalRead);
			this._buffer.MaxSize = maxBufferSize;
			this._buffer.MinimalRead = minimalRead;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002108 File Offset: 0x00000308
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this._buffer != null)
				{
					this._buffer.Dispose();
					this._buffer = null;
				}
				if (this.CloseBaseStream)
				{
					this._baseStream.Close();
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002144 File Offset: 0x00000344
		public void TakeLock()
		{
			Monitor.Enter(this._localLock);
			if (++this._lockCount == 1)
			{
				this._owningThread = Thread.CurrentThread;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000217B File Offset: 0x0000037B
		private void CheckLock()
		{
			if (this._owningThread != Thread.CurrentThread)
			{
				throw new SynchronizationLockException();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002190 File Offset: 0x00000390
		public void ReleaseLock()
		{
			this.CheckLock();
			if (--this._lockCount == 0)
			{
				this._owningThread = null;
			}
			Monitor.Exit(this._localLock);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021C8 File Offset: 0x000003C8
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021D0 File Offset: 0x000003D0
		public bool CloseBaseStream { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021D9 File Offset: 0x000003D9
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021E6 File Offset: 0x000003E6
		public bool MinimalRead
		{
			get
			{
				return this._buffer.MinimalRead;
			}
			set
			{
				this._buffer.MinimalRead = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002201 File Offset: 0x00000401
		public int MaxBufferSize
		{
			get
			{
				return this._buffer.MaxSize;
			}
			set
			{
				this.CheckLock();
				this._buffer.MaxSize = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002215 File Offset: 0x00000415
		public long BufferBaseOffset
		{
			get
			{
				return this._buffer.BaseOffset;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002222 File Offset: 0x00000422
		public int BufferBytesFilled
		{
			get
			{
				return this._buffer.BytesFilled;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000222F File Offset: 0x0000042F
		public void Discard(int bytes)
		{
			this.CheckLock();
			this._buffer.DiscardThrough(this._buffer.BaseOffset + (long)bytes);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002250 File Offset: 0x00000450
		public void DiscardThrough(long offset)
		{
			this.CheckLock();
			this._buffer.DiscardThrough(offset);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002264 File Offset: 0x00000464
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002267 File Offset: 0x00000467
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000226A File Offset: 0x0000046A
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000226D File Offset: 0x0000046D
		public override void Flush()
		{
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000226F File Offset: 0x0000046F
		public override long Length
		{
			get
			{
				return this._baseStream.Length;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000227C File Offset: 0x0000047C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002284 File Offset: 0x00000484
		public override long Position
		{
			get
			{
				return this._readPosition;
			}
			set
			{
				this.Seek(value, 0);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002290 File Offset: 0x00000490
		public override int ReadByte()
		{
			this.CheckLock();
			int num = this._buffer.ReadByte(this.Position);
			if (num > -1)
			{
				this.Seek(1L, 1);
			}
			return num;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022C4 File Offset: 0x000004C4
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckLock();
			int num = this._buffer.Read(this.Position, buffer, offset, count);
			this.Seek((long)num, 1);
			return num;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022F8 File Offset: 0x000004F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckLock();
			switch (origin)
			{
			case 1:
				offset += this.Position;
				break;
			case 2:
				offset += this._baseStream.Length;
				break;
			}
			if (!this._baseStream.CanSeek)
			{
				if (offset < this._buffer.BaseOffset)
				{
					throw new InvalidOperationException("Cannot seek to before the start of the buffer!");
				}
				if (offset >= this._buffer.BufferEndOffset)
				{
					throw new InvalidOperationException("Cannot seek to beyond the end of the buffer!  Discard some bytes.");
				}
			}
			return this._readPosition = offset;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002385 File Offset: 0x00000585
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000238C File Offset: 0x0000058C
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000001 RID: 1
		private const int DEFAULT_INITIAL_SIZE = 32768;

		// Token: 0x04000002 RID: 2
		private const int DEFAULT_MAX_SIZE = 262144;

		// Token: 0x04000003 RID: 3
		private Stream _baseStream;

		// Token: 0x04000004 RID: 4
		private StreamReadBuffer _buffer;

		// Token: 0x04000005 RID: 5
		private long _readPosition;

		// Token: 0x04000006 RID: 6
		private object _localLock = new object();

		// Token: 0x04000007 RID: 7
		private Thread _owningThread;

		// Token: 0x04000008 RID: 8
		private int _lockCount;
	}
}
