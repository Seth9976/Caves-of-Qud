using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x02000025 RID: 37
	public class CountingStream : Stream
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004444 File Offset: 0x00002644
		public CountingStream(Stream stream)
		{
			this._s = stream;
			try
			{
				this._initialOffset = this._s.Position;
			}
			catch
			{
				this._initialOffset = 0L;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000448C File Offset: 0x0000268C
		public Stream WrappedStream
		{
			get
			{
				return this._s;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004494 File Offset: 0x00002694
		public long BytesWritten
		{
			get
			{
				return this._bytesWritten;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000449C File Offset: 0x0000269C
		public long BytesRead
		{
			get
			{
				return this._bytesRead;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000044A4 File Offset: 0x000026A4
		public void Adjust(long delta)
		{
			this._bytesWritten -= delta;
			if (this._bytesWritten < 0L)
			{
				throw new InvalidOperationException();
			}
			if (this._s is CountingStream)
			{
				((CountingStream)this._s).Adjust(delta);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000044E4 File Offset: 0x000026E4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this._s.Read(buffer, offset, count);
			this._bytesRead += (long)num;
			return num;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004510 File Offset: 0x00002710
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return;
			}
			this._s.Write(buffer, offset, count);
			this._bytesWritten += (long)count;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004533 File Offset: 0x00002733
		public override bool CanRead
		{
			get
			{
				return this._s.CanRead;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004540 File Offset: 0x00002740
		public override bool CanSeek
		{
			get
			{
				return this._s.CanSeek;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000454D File Offset: 0x0000274D
		public override bool CanWrite
		{
			get
			{
				return this._s.CanWrite;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000455A File Offset: 0x0000275A
		public override void Flush()
		{
			this._s.Flush();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004567 File Offset: 0x00002767
		public override long Length
		{
			get
			{
				return this._s.Length;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004574 File Offset: 0x00002774
		public long ComputedPosition
		{
			get
			{
				return this._initialOffset + this._bytesWritten;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004583 File Offset: 0x00002783
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004590 File Offset: 0x00002790
		public override long Position
		{
			get
			{
				return this._s.Position;
			}
			set
			{
				this._s.Seek(value, 0);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000045A0 File Offset: 0x000027A0
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._s.Seek(offset, origin);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000045AF File Offset: 0x000027AF
		public override void SetLength(long value)
		{
			this._s.SetLength(value);
		}

		// Token: 0x04000060 RID: 96
		private Stream _s;

		// Token: 0x04000061 RID: 97
		private long _bytesWritten;

		// Token: 0x04000062 RID: 98
		private long _bytesRead;

		// Token: 0x04000063 RID: 99
		private long _initialOffset;
	}
}
