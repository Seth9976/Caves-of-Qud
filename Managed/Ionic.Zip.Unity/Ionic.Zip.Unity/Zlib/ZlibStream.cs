using System;
using System.IO;

namespace Ionic.Zlib
{
	// Token: 0x0200006E RID: 110
	public class ZlibStream : Stream
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0002128E File Offset: 0x0001F48E
		public ZlibStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0002129A File Offset: 0x0001F49A
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000212A6 File Offset: 0x0001F4A6
		public ZlibStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000212B2 File Offset: 0x0001F4B2
		public ZlibStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.ZLIB, leaveOpen);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000212CF File Offset: 0x0001F4CF
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000212DC File Offset: 0x0001F4DC
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
					throw new ObjectDisposedException("ZlibStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000212FD File Offset: 0x0001F4FD
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0002130C File Offset: 0x0001F50C
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
					throw new ObjectDisposedException("ZlibStream");
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00021378 File Offset: 0x0001F578
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0002138A File Offset: 0x0001F58A
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002139C File Offset: 0x0001F59C
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

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x000213E8 File Offset: 0x0001F5E8
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0002140D File Offset: 0x0001F60D
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00021410 File Offset: 0x0001F610
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("ZlibStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00021435 File Offset: 0x0001F635
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00021455 File Offset: 0x0001F655
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0002145C File Offset: 0x0001F65C
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000214A8 File Offset: 0x0001F6A8
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
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000214AF File Offset: 0x0001F6AF
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			return this._baseStream.Read(buffer, offset, count);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000214D2 File Offset: 0x0001F6D2
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000214D9 File Offset: 0x0001F6D9
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000214E0 File Offset: 0x0001F6E0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("ZlibStream");
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00021504 File Offset: 0x0001F704
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0002154C File Offset: 0x0001F74C
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00021594 File Offset: 0x0001F794
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000215D8 File Offset: 0x0001F7D8
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new ZlibStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x040003CF RID: 975
		internal ZlibBaseStream _baseStream;

		// Token: 0x040003D0 RID: 976
		private bool _disposed;
	}
}
