using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000023 RID: 35
	internal class OffsetStream : Stream, IDisposable
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003B82 File Offset: 0x00001D82
		public OffsetStream(Stream s)
		{
			this._originalPosition = s.Position;
			this._innerStream = s;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003B9D File Offset: 0x00001D9D
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this._innerStream.Read(buffer, offset, count);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003BAD File Offset: 0x00001DAD
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public override bool CanRead
		{
			get
			{
				return this._innerStream.CanRead;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003BC1 File Offset: 0x00001DC1
		public override bool CanSeek
		{
			get
			{
				return this._innerStream.CanSeek;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003BCE File Offset: 0x00001DCE
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BD1 File Offset: 0x00001DD1
		public override void Flush()
		{
			this._innerStream.Flush();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003BDE File Offset: 0x00001DDE
		public override long Length
		{
			get
			{
				return this._innerStream.Length;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003BEB File Offset: 0x00001DEB
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003BFF File Offset: 0x00001DFF
		public override long Position
		{
			get
			{
				return this._innerStream.Position - this._originalPosition;
			}
			set
			{
				this._innerStream.Position = this._originalPosition + value;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C14 File Offset: 0x00001E14
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._innerStream.Seek(this._originalPosition + offset, origin) - this._originalPosition;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C31 File Offset: 0x00001E31
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C38 File Offset: 0x00001E38
		void IDisposable.Dispose()
		{
			this.Close();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C40 File Offset: 0x00001E40
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x0400005B RID: 91
		private long _originalPosition;

		// Token: 0x0400005C RID: 92
		private Stream _innerStream;
	}
}
