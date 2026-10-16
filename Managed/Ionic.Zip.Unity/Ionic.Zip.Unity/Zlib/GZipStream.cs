using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000054 RID: 84
	public class GZipStream : Stream
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00018BD4 File Offset: 0x00016DD4
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x00018BDC File Offset: 0x00016DDC
		public string Comment
		{
			get
			{
				return this._Comment;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._Comment = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00018BF8 File Offset: 0x00016DF8
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00018C00 File Offset: 0x00016E00
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				this._FileName = value;
				if (this._FileName == null)
				{
					return;
				}
				if (this._FileName.IndexOf("/") != -1)
				{
					this._FileName = this._FileName.Replace("/", "\\");
				}
				if (this._FileName.EndsWith("\\"))
				{
					throw new Exception("Illegal filename");
				}
				if (this._FileName.IndexOf("\\") != -1)
				{
					this._FileName = Path.GetFileName(this._FileName);
				}
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00018C9F File Offset: 0x00016E9F
		public int Crc32
		{
			get
			{
				return this._Crc32;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00018CA7 File Offset: 0x00016EA7
		public GZipStream(Stream stream, CompressionMode mode)
			: this(stream, mode, CompressionLevel.Default, false)
		{
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00018CB3 File Offset: 0x00016EB3
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level)
			: this(stream, mode, level, false)
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00018CBF File Offset: 0x00016EBF
		public GZipStream(Stream stream, CompressionMode mode, bool leaveOpen)
			: this(stream, mode, CompressionLevel.Default, leaveOpen)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00018CCB File Offset: 0x00016ECB
		public GZipStream(Stream stream, CompressionMode mode, CompressionLevel level, bool leaveOpen)
		{
			this._baseStream = new ZlibBaseStream(stream, mode, level, ZlibStreamFlavor.GZIP, leaveOpen);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00018CE8 File Offset: 0x00016EE8
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00018CF5 File Offset: 0x00016EF5
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
					throw new ObjectDisposedException("GZipStream");
				}
				this._baseStream._flushMode = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00018D16 File Offset: 0x00016F16
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00018D24 File Offset: 0x00016F24
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
					throw new ObjectDisposedException("GZipStream");
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

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00018D90 File Offset: 0x00016F90
		public virtual long TotalIn
		{
			get
			{
				return this._baseStream._z.TotalBytesIn;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00018DA2 File Offset: 0x00016FA2
		public virtual long TotalOut
		{
			get
			{
				return this._baseStream._z.TotalBytesOut;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00018DB4 File Offset: 0x00016FB4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this._disposed)
				{
					if (disposing && this._baseStream != null)
					{
						this._baseStream.Close();
						this._Crc32 = this._baseStream.Crc32;
					}
					this._disposed = true;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00018E14 File Offset: 0x00017014
		public override bool CanRead
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanRead;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00018E39 File Offset: 0x00017039
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00018E3C File Offset: 0x0001703C
		public override bool CanWrite
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("GZipStream");
				}
				return this._baseStream._stream.CanWrite;
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00018E61 File Offset: 0x00017061
		public override void Flush()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			this._baseStream.Flush();
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00018E81 File Offset: 0x00017081
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00018E88 File Offset: 0x00017088
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x00018EE9 File Offset: 0x000170E9
		public override long Position
		{
			get
			{
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Writer)
				{
					return this._baseStream._z.TotalBytesOut + (long)this._headerByteCount;
				}
				if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Reader)
				{
					return this._baseStream._z.TotalBytesIn + (long)this._baseStream._gzipHeaderByteCount;
				}
				return 0L;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00018EF0 File Offset: 0x000170F0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			int num = this._baseStream.Read(buffer, offset, count);
			if (!this._firstReadDone)
			{
				this._firstReadDone = true;
				this.FileName = this._baseStream._GzipFileName;
				this.Comment = this._baseStream._GzipComment;
			}
			return num;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00018F51 File Offset: 0x00017151
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00018F58 File Offset: 0x00017158
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00018F60 File Offset: 0x00017160
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("GZipStream");
			}
			if (this._baseStream._streamMode == ZlibBaseStream.StreamMode.Undefined)
			{
				if (!this._baseStream._wantCompress)
				{
					throw new InvalidOperationException();
				}
				this._headerByteCount = this.EmitHeader();
			}
			this._baseStream.Write(buffer, offset, count);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00018FC0 File Offset: 0x000171C0
		private int EmitHeader()
		{
			byte[] array = ((this.Comment == null) ? null : GZipStream.iso8859dash1.GetBytes(this.Comment));
			byte[] array2 = ((this.FileName == null) ? null : GZipStream.iso8859dash1.GetBytes(this.FileName));
			int num = ((this.Comment == null) ? 0 : (array.Length + 1));
			int num2 = ((this.FileName == null) ? 0 : (array2.Length + 1));
			int num3 = 10 + num + num2;
			byte[] array3 = new byte[num3];
			int num4 = 0;
			array3[num4++] = 31;
			array3[num4++] = 139;
			array3[num4++] = 8;
			byte b = 0;
			if (this.Comment != null)
			{
				b ^= 16;
			}
			if (this.FileName != null)
			{
				b ^= 8;
			}
			array3[num4++] = b;
			if (this.LastModified == null)
			{
				this.LastModified = new DateTime?(DateTime.Now);
			}
			int num5 = (int)(this.LastModified.Value - GZipStream._unixEpoch).TotalSeconds;
			Array.Copy(BitConverter.GetBytes(num5), 0, array3, num4, 4);
			num4 += 4;
			array3[num4++] = 0;
			array3[num4++] = byte.MaxValue;
			if (num2 != 0)
			{
				Array.Copy(array2, 0, array3, num4, num2 - 1);
				num4 += num2 - 1;
				array3[num4++] = 0;
			}
			if (num != 0)
			{
				Array.Copy(array, 0, array3, num4, num - 1);
				num4 += num - 1;
				array3[num4++] = 0;
			}
			this._baseStream._stream.Write(array3, 0, array3.Length);
			return array3.Length;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00019164 File Offset: 0x00017364
		public static byte[] CompressString(string s)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressString(s, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000191AC File Offset: 0x000173AC
		public static byte[] CompressBuffer(byte[] b)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Compress, CompressionLevel.BestCompression);
				ZlibBaseStream.CompressBuffer(b, stream);
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000191F4 File Offset: 0x000173F4
		public static string UncompressString(byte[] compressed)
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				text = ZlibBaseStream.UncompressString(compressed, stream);
			}
			return text;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00019238 File Offset: 0x00017438
		public static byte[] UncompressBuffer(byte[] compressed)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(compressed))
			{
				Stream stream = new GZipStream(memoryStream, CompressionMode.Decompress);
				array = ZlibBaseStream.UncompressBuffer(compressed, stream);
			}
			return array;
		}

		// Token: 0x040002AE RID: 686
		public DateTime? LastModified;

		// Token: 0x040002AF RID: 687
		private int _headerByteCount;

		// Token: 0x040002B0 RID: 688
		internal ZlibBaseStream _baseStream;

		// Token: 0x040002B1 RID: 689
		private bool _disposed;

		// Token: 0x040002B2 RID: 690
		private bool _firstReadDone;

		// Token: 0x040002B3 RID: 691
		private string _FileName;

		// Token: 0x040002B4 RID: 692
		private string _Comment;

		// Token: 0x040002B5 RID: 693
		private int _Crc32;

		// Token: 0x040002B6 RID: 694
		internal static readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);

		// Token: 0x040002B7 RID: 695
		internal static readonly Encoding iso8859dash1 = Encoding.GetEncoding("UTF-8");
	}
}
