using System;
using System.IO;

namespace Ionic.Crc
{
	// Token: 0x02000070 RID: 112
	public class CrcCalculatorStream : Stream, IDisposable
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00021AD6 File Offset: 0x0001FCD6
		public CrcCalculatorStream(Stream stream)
			: this(true, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00021AE6 File Offset: 0x0001FCE6
		public CrcCalculatorStream(Stream stream, bool leaveOpen)
			: this(leaveOpen, CrcCalculatorStream.UnsetLengthLimit, stream, null)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00021AF6 File Offset: 0x0001FCF6
		public CrcCalculatorStream(Stream stream, long length)
			: this(true, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00021B12 File Offset: 0x0001FD12
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen)
			: this(leaveOpen, length, stream, null)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00021B2E File Offset: 0x0001FD2E
		public CrcCalculatorStream(Stream stream, long length, bool leaveOpen, CRC32 crc32)
			: this(leaveOpen, length, stream, crc32)
		{
			if (length < 0L)
			{
				throw new ArgumentException("length");
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00021B4B File Offset: 0x0001FD4B
		private CrcCalculatorStream(bool leaveOpen, long length, Stream stream, CRC32 crc32)
		{
			this._innerStream = stream;
			this._Crc32 = crc32 ?? new CRC32();
			this._lengthLimit = length;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00021B82 File Offset: 0x0001FD82
		public long TotalBytesSlurped
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00021B8F File Offset: 0x0001FD8F
		public int Crc
		{
			get
			{
				return this._Crc32.Crc32Result;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00021B9C File Offset: 0x0001FD9C
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		public bool LeaveOpen
		{
			get
			{
				return this._leaveOpen;
			}
			set
			{
				this._leaveOpen = value;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = count;
			if (this._lengthLimit != CrcCalculatorStream.UnsetLengthLimit)
			{
				if (this._Crc32.TotalBytesRead >= this._lengthLimit)
				{
					return 0;
				}
				long num2 = this._lengthLimit - this._Crc32.TotalBytesRead;
				if (num2 < (long)count)
				{
					num = (int)num2;
				}
			}
			int num3 = this._innerStream.Read(buffer, offset, num);
			if (num3 > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, num3);
			}
			return num3;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00021C1E File Offset: 0x0001FE1E
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > 0)
			{
				this._Crc32.SlurpBlock(buffer, offset, count);
			}
			this._innerStream.Write(buffer, offset, count);
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00021C40 File Offset: 0x0001FE40
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00021C4D File Offset: 0x0001FE4D
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00021C50 File Offset: 0x0001FE50
		public override bool CanWrite
		{
			get
			{
				return this._innerStream.CanWrite;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00021C5D File Offset: 0x0001FE5D
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00021C6A File Offset: 0x0001FE6A
		public override long Length
		{
			get
			{
				if (this._lengthLimit == CrcCalculatorStream.UnsetLengthLimit)
				{
					return this._innerStream.Length;
				}
				return this._lengthLimit;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00021C8B File Offset: 0x0001FE8B
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00021C98 File Offset: 0x0001FE98
		public override long Position
		{
			get
			{
				return this._Crc32.TotalBytesRead;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00021C9F File Offset: 0x0001FE9F
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00021CA6 File Offset: 0x0001FEA6
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00021CAD File Offset: 0x0001FEAD
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00021CB5 File Offset: 0x0001FEB5
		public override void Close()
		{
			base.Close();
			if (!this._leaveOpen)
			{
				this._innerStream.Close();
			}
		}

		// Token: 0x040003D7 RID: 983
		private static readonly long UnsetLengthLimit = -99L;

		// Token: 0x040003D8 RID: 984
		internal Stream _innerStream;

		// Token: 0x040003D9 RID: 985
		private CRC32 _Crc32;

		// Token: 0x040003DA RID: 986
		private long _lengthLimit = -99L;

		// Token: 0x040003DB RID: 987
		private bool _leaveOpen;
	}
}
