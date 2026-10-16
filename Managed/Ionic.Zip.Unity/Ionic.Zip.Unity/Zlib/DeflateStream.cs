using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x02000053 RID: 83
	public class DeflateStream : Stream
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0001880E File Offset: 0x00016A0E
		public DeflateStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001881A File Offset: 0x00016A1A
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00018826 File Offset: 0x00016A26
		public DeflateStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00018832 File Offset: 0x00016A32
		public DeflateStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._innerStream = stream;
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.DEFLATE, leaveOpen);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00018856 File Offset: 0x00016A56
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00018863 File Offset: 0x00016A63
		public virtual FlushType FlushMode
		{
			get
			{
				return this._baseStream._flushMode;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00018884 File Offset: 0x00016A84
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00018894 File Offset: 0x00016A94
		public int BufferSize
		{
			get
			{
				return this._baseStream._bufferSize;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				if (this._baseStream._workingBuffer != null)
				{
					throw new ZlibException("The working buffer is already set.");
				}
				if (value < 1024)
				{
					throw new ZlibException(string.Format("Don't be silly. {0} bytes?? Use a bigger buffer, at least {1}.", value, 1024));
				}
				this._baseStream._bufferSize = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00018900 File Offset: 0x00016B00
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0001890D File Offset: 0x00016B0D
		public CompressionStrategy Strategy
		{
			get
			{
				return this._baseStream.Strategy;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				this._baseStream.Strategy = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001892E File Offset: 0x00016B2E
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00018940 File Offset: 0x00016B40
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00018954 File Offset: 0x00016B54
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x000189A0 File Offset: 0x00016BA0
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x000189C5 File Offset: 0x00016BC5
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000189C8 File Offset: 0x00016BC8
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("DeflateStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000189ED File Offset: 0x00016BED
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00018A0D File Offset: 0x00016C0D
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00018A14 File Offset: 0x00016C14
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00018A60 File Offset: 0x00016C60
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00018A67 File Offset: 0x00016C67
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00018A8A File Offset: 0x00016C8A
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00018A91 File Offset: 0x00016C91
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00018A98 File Offset: 0x00016C98
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("DeflateStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00018ABC File Offset: 0x00016CBC
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00018B04 File Offset: 0x00016D04
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00018B4C File Offset: 0x00016D4C
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00018B90 File Offset: 0x00016D90
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new DeflateStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x040002AB RID: 683
		internal ZlibBaseStream _baseStream;

		// Token: 0x040002AC RID: 684
		internal Stream _innerStream;

		// Token: 0x040002AD RID: 685
		private bool _disposed;
	}
}
