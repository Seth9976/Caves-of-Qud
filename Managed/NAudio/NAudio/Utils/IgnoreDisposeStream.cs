using System;
using System.IO;

namespace NAudio.Utils
{
	// Token: 0x02000113 RID: 275
	public class IgnoreDisposeStream : Stream
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000131F1 File Offset: 0x000113F1
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x000131F9 File Offset: 0x000113F9
		public Stream SourceStream { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00013202 File Offset: 0x00011402
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x0001320A File Offset: 0x0001140A
		public bool IgnoreDispose { get; set; }

		// Token: 0x060005E6 RID: 1510 RVA: 0x00013213 File Offset: 0x00011413
		public IgnoreDisposeStream(Stream sourceStream)
		{
			this.SourceStream = sourceStream;
			this.IgnoreDispose = true;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x00013229 File Offset: 0x00011429
		public override bool CanRead
		{
			get
			{
				return this.SourceStream.CanRead;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00013236 File Offset: 0x00011436
		public override bool CanSeek
		{
			get
			{
				return this.SourceStream.CanSeek;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00013243 File Offset: 0x00011443
		public override bool CanWrite
		{
			get
			{
				return this.SourceStream.CanWrite;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00013250 File Offset: 0x00011450
		public override void Flush()
		{
			this.SourceStream.Flush();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001325D File Offset: 0x0001145D
		public override long Length
		{
			get
			{
				return this.SourceStream.Length;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x0001326A File Offset: 0x0001146A
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00013277 File Offset: 0x00011477
		public override long Position
		{
			get
			{
				return this.SourceStream.Position;
			}
			set
			{
				this.SourceStream.Position = value;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013285 File Offset: 0x00011485
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.SourceStream.Read(buffer, offset, count);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00013295 File Offset: 0x00011495
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.SourceStream.Seek(offset, origin);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000132A4 File Offset: 0x000114A4
		public override void SetLength(long value)
		{
			this.SourceStream.SetLength(value);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000132B2 File Offset: 0x000114B2
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.SourceStream.Write(buffer, offset, count);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000132C2 File Offset: 0x000114C2
		protected override void Dispose(bool disposing)
		{
			if (!this.IgnoreDispose)
			{
				this.SourceStream.Dispose();
				this.SourceStream = null;
			}
		}
	}
}
